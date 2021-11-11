using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SweetPet.Common
{
    /// <summary>
    /// Validação customizada para CPF
    /// </summary>
    public class CustomValidationCPFAttribute : ValidationAttribute
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public CustomValidationCPFAttribute() { }

        /// <summary>
        /// Validação server
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            //Se não tiver valor ignora a validação
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            bool valido = AssertionConcern.ValidaCPF(value.ToString());
            return valido;
        }

    }
}
