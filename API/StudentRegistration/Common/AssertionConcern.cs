using System;


namespace SweetPet.Common
{
    public static class AssertionConcern
    {

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf">CPF a ser validado</param>
        /// <returns>Valido / invalido (true / false)</returns>
        public static bool ValidaCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = Util.RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length < 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }

        internal static bool StringTimeIsValid(string v)
        {
            try
            {
                var date = TimeSpan.Parse(v);

                if (date == TimeSpan.MinValue) 
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static bool StringDateIsValid(string v, bool dateNow = false)
        {
            try
            {
                var date = DateTime.Parse(v);

                if (date == DateTime.MinValue) return false;
                else if (dateNow && date.Date < DateTime.Now.Date) return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
