using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentRegistration.Domain;
using StudentRegistration.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StudentRegistration.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Compressão das respostas em json
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            });

            services.AddCors();
            
            
            services.AddControllers(options => {
                //Filtro de validações
                options.Filters.Add(typeof(ValidateModelStateFilter));
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //Desativar filtro nativo para que utilize apenas o filtro personalizado
                options.SuppressModelStateInvalidFilter = true;
            });

            #region Conexão com o banco
            //Captura a connection string para se conectar com o banco de dados
            string mySqlConnectionStr = Configuration.GetConnectionString("ConnectionString");

            //Conecta no banco especificado
            var dataBaseType = Configuration.GetValue(typeof(EDBType), "DatabaseType");
            switch (dataBaseType)
            {
                //Banco de dados em memória
                case EDBType.InMemory:
                    services.AddDbContext<ConnectionEf>(options =>
                    options.UseInMemoryDatabase("Database"));
                    break;
                //Conexão com banco SqlServer
                case EDBType.SqlServer:
                    services.AddDbContext<ConnectionEf>(options =>
                    options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
                    break;
                //Conexão com banco Postgress
                case EDBType.Postgres:
                    services.AddDbContext<ConnectionEf>(options =>
                    options.UseNpgsql(mySqlConnectionStr));
                    break;
                //Conexão com banco MySql
                case EDBType.Mysql:
                    services.AddDbContext<ConnectionEf>(options =>
                    options.UseSqlServer(mySqlConnectionStr));
                    break;
            }
            #endregion

            //Injeção de dependencias
            var dependency = new Dependencys(services);

            #region Configurações Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cadastro de alunos",
                    Version = "v1",
                    Description = "Teste API"
                });

                //Adiciona autenticação para usar dento do Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT usando o esquema Bearer. " +
                    "\r\n\r\n Digite 'Bearer' [espaço] e, em seguida, seu token na entrada de texto abaixo." +
                    "\r\n\r\nExemplo: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                //Atribuir token de autenticação a todas as requisições
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                //Mostrar textos da documentação no Swagger
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "StudentRegistration.Domain.xml"));

            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    // Desabilitar swagger schemas
                    c.DefaultModelsExpandDepth(-1);
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentRegistration.API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(x => x
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
