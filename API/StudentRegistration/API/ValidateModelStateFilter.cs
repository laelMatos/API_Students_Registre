using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentRegistration.Domain;
using System.Collections.Generic;
using System.Linq;

namespace StudentRegistration.API
{
    /// <summary>
    /// Define o objeto de retorno para identificar os erros.
    /// </summary>
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    Notification notification = new Notification() { Title= "Inconsistência de dados" };
                    List<Messages>  Messages = new List<Messages>();

                    foreach (var modelStateKey in context.ModelState.Keys)
                    {
                        var modelStateVal = context.ModelState[modelStateKey];
                        if (modelStateVal.Errors.Count > 0)
                        {
                            var response = new Messages()
                            {
                                ErrorField = modelStateKey,
                                Message = modelStateVal.Errors.Select(c => c.ErrorMessage).First(),   
                            };
                            Messages.Add(response);
                        }
                    }
                    notification.Messages = Messages;

                    context.Result = new JsonResult(notification)
                    {
                        StatusCode = 400
                    };
                }
            }
            catch (System.Exception)
            {
                List<object> errors = new List<object>() { new {
                    Title = "Falha na validação dos dados",
                    Success = false,
                    Messages = new List<object>(){new{ Message="Falha ao verificar validações", ErrorField = "" } }
                }};

                context.Result = new JsonResult(errors)
                {
                    StatusCode = 500
                };
            }
        }
    }
}