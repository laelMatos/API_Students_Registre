using Microsoft.Extensions.DependencyInjection;
using StudentRegistration.Domain;
using StudentRegistration.Repository;
using StudentRegistration.Service;
using System;

namespace StudentRegistration.API
{
    internal class Dependencys
    {
        private IServiceCollection services;

        public Dependencys(IServiceCollection services)
        {
            this.services = services;
            SetDependencys();
        }

        private void SetDependencys()
        {
            //scoped - apenas uma dependencia por requisiçao
            //transient - a cada requisição é criado uma nova dependencia
            //singleton - apenas uma dependencia para toda a aplicação

            services.AddScoped<ConnectionEf, ConnectionEf>();

            //Repositorio genérico
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            #region Injeção de dependencias dos Repositorios
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region Injeção de dependencias dos Serviços
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();

            #endregion
        }
    }
}