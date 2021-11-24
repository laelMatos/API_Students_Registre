using API.Model;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentRegistration.Domain;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize("Adm")]
    public class StudentController : Controller
    {
        /// <summary>
        /// Busca todos os alunos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Retorna todos os alunos cadastrados</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="401">Não authorizado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentMd))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult GetAll([FromServices] IStudentService studentService)
        {
            try
            {
                var result = studentService.GetAll();
                if (result == null)
                    return Ok();

                return Ok(BindObject.BindRange<StudentMd>(result));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification()
                {
                    Title = "Erro interno",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }

        }
        /// <summary>
        /// Busca um aluno pelo RA
        /// </summary>
        /// <param name="model">Código do aluno</param>
        /// <returns></returns>
        /// <response code="200">Retorna o novo aluno solicitado</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="401">Não authorizado</response>
        /// <response code="500">Erro interno</response>
        [HttpGet]
        [Route("byRa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentMd))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult GetByRA(
            [FromServices] IStudentService studentService, [FromQuery] StudentRA model)
        {
            try
            {
                Student result = studentService.GetByRA(model.RA);

                if (!result.NOTIFICATION.Success)
                {
                    Response.StatusCode = (int)result.NOTIFICATION.HttpStatusCode;
                    return Json(result.NOTIFICATION);
                }
                   

                return Ok(BindObject.Bind<StudentMd>(result));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification()
                {
                    Title = "Erro interno",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }

        /// <summary>
        /// Cadastra um novo aluno
        /// </summary>
        /// <param name="model">Dados do aluno</param>
        /// <returns></returns>
        /// <response code="200">Retorna o novo aluno cadastrado</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="401">Não authorizado</response>
        /// <response code="500">Erro interno</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentMd))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult Add(
            [FromServices]IStudentService studentService, 
            [FromBody] StudentMd model)
        {
            try
            {
                Student newStudent = new Student(model.RA, model.CPF, model.Name, model.Email);

                var result = studentService.Add(newStudent);

                if (!result.NOTIFICATION.Success)
                {
                    Response.StatusCode = (int)result.NOTIFICATION.HttpStatusCode;
                    return Json(result.NOTIFICATION);
                }

                return Ok(BindObject.Bind<StudentMd>(result));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification()
                {
                    Title="Erro interno",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }

        /// <summary>
        /// Atualiza os dados do aluno
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o novo aluno cadastrado</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentMd))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult Updade(
            [FromServices] IStudentService studentService,
            [FromBody] StudentUpdateMd model)
        {
            try
            {
                var result = studentService.Update(model.RA, model.Email, model.Name);

                if (!result.NOTIFICATION.Success)
                {
                    Response.StatusCode = (int)result.NOTIFICATION.HttpStatusCode;
                    return Json(result.NOTIFICATION);
                }

                return Ok(BindObject.Bind<StudentMd>(result));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification()
                {
                    Title= "Erro interno",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }

        /// <summary>
        /// Remove um aluno especificado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Retorna o novo aluno cadastrado</response>
        /// <response code="400">Inconsistencia de dados</response>
        /// <response code="500">Erro interno</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentMd))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Notification))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Notification))]
        public IActionResult Delete(
            [FromServices] IStudentService studentService,
            [FromBody] StudentRA model)
        {
            try
            {
                var result = studentService.Delete(model.RA);

                Response.StatusCode = (int)result.HttpStatusCode;
                return Json(result);
                 
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return Json(new Notification()
                {
                    Title = "Erro interno",
                    Messages = new List<Messages> { new Messages {
                            Message = ex.Message,
                            ErrorField = "", }}
                });
            }
        }
    }
}
