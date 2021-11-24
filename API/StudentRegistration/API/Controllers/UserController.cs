using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common;
using System;
using System.Collections.Generic;
using StudentRegistration.Service;
using API.Model;
using StudentRegistration.Domain;
using API.Model.User;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        
        /// <summary>
        /// Responsável por inserir um novo usuário
        /// </summary>
        /// <param name="model">Dados do usuario</param>
        /// <returns>O novo usuário criado</returns>
        /// <response code="200">Retorna o novo usuário criado</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]     
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult CreateUser(
        [FromBody] UserRequest model,
        [FromServices] IUserService UserService)
        {
            try
            {
                var userModel = new User(model.Name, model.Email, model.Password, model.eType);

                var result = UserService.Insert(userModel);

                if (!result.NOTIFICATION.Success)
                {
                    Response.StatusCode = (int)result.NOTIFICATION.HttpStatusCode;
                    return Json(result.NOTIFICATION);
                }

                return Ok( BindObject.Bind<UserResponse>(result));

            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification
                {
                    Title = "Falha ao criar um novo usuário",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }
    }
}
