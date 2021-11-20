using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentRegistration.Domain
{
    public class Notification
    {
        public Notification()
        {
            HttpStatusCode = EHttpResponseCode.OK;
            Success = true;
        }
        /// <summary>
        /// Titulo da menssagem
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Indica sucesso(true) ou não(false)
        /// </summary>
        public bool Success { get; protected set; }

        [JsonIgnore]
        public EHttpResponseCode HttpStatusCode { get; set; }
        /// <summary>
        /// Lista de Mensagens
        /// </summary>
        public IEnumerable<Messages> Messages { get { return _Messages; } set { SetMessages(value); } }

        private IEnumerable<Messages> _Messages;

        private void SetMessages(IEnumerable<Messages> msgs)
        {
            if(msgs.Count() < 1)
            {
                throw new Exception("O objeto é inválido!");
            }
            
            Success = false;
            _Messages = msgs;
        }
    }

    public struct Messages
    {
        /// <summary>
        /// Mensagem do resultado
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Campo com erro
        /// </summary>
        public string ErrorField { get; set; }
    }

}
