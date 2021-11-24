using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common;
using StudentRegistration.Domain;
using StudentRegistration.Service;
using System;
using System.Collections.Generic;
using API.Model;
using System.Security.Principal;

namespace SweetPet.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Rota que autentica o usuário
        /// </summary>
        /// <returns>Retorna o login do usuário e o token de autenticação  
        /// que deve ser informado nas outras requisições através do header Authorization Bearer</returns>
        /// <response code="200">Retorna o token de autenticação e o usuário</response>
        /// <response code="400">Informações inconsistentes</response>
        /// <response code="401">Login ou senha incorretos.</response>
        /// <response code="500">Erro interno.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult Autenticator(
            [FromBody] LoginRequest model,
            [FromServices] IAuthService authService)
       {
            try
            {
                var userAuthenticated = authService.ValidAuthentication(model.Login, model.Password);

                if (userAuthenticated == null)
                    return BadRequest(new Notification
                    {
                        Title = "Falha ao autenticar o usuário",
                        Messages = new List<Messages> { new Messages {
                            Message = "Verifique seu login e senha",
                            ErrorField = "", }}
                    });

                var token = TokenService.GenerateToken(userAuthenticated);

                return Ok(new LoginResponse(){
                    Token = token,
                    User = BindObject.Bind<UserResponse>(userAuthenticated)});
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification
                {
                    Title = "Falha ao autenticar o usuário",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }

        /// <summary>
        /// Rota para sessão de usuário e autorização
        /// </summary>
        /// <param name="model"></param>
        /// <param name="authService"></param>
        /// <returns>Retorna o novo token de autenticação e dados da sessão do usuário</returns>
        /// <response code="200">Retorna a sessão com o token do usuário</response>
        /// <response code="400">Informações inconsistentes</response>
        /// <response code="401">Token inválido</response>
        /// <response code="500">Erro interno.</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SessionRespose))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult Session(
            [FromServices] IAuthService authService,
            [FromServices] IUserService userService)
        {
            try
            {
                int id = int.Parse(HttpContext.User.Identity.Name);
                User userDb = userService.GetById(id);
                
                return Ok(new SessionRespose()
                {
                    User = BindObject.Bind<UserResponse>(userDb),
                    SessionData = new {desciption="Dados de configuração do usuário"}
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification
                {
                    Title = "Falha ao autenticar o usuário",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }
    }
}
