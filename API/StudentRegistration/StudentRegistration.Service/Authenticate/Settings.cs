using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Service
{
    public static class Settings
    {
        /// <summary>
        /// Chave de incriptação do JWT
        /// </summary>
        public static string Secret = "c7b3b39063724a178de82b41d74756570513e60a8078480e880f6ea719880232";

        ///// <summary>
        ///// Chave de 8 bits
        ///// </summary>
        //public const string secretkey = "9jd3dg6e";

        ///// <summary>
        ///// Chave de 8 bits
        ///// </summary>
        //public  const string publickey = "s4c6f4da";

        /// <summary>
        /// Chave de 32 bits
        /// </summary>
        public const string publickey = "4Riv9UH56xWtAy7cM5Yr3jrPm1s26W4B";

        /// <summary>
        /// Chave de 16 bits
        /// </summary>
        public const string Secretkey = "Cn7suBr34o297Fw8";

        /// <summary>
        /// Chave 32 bits Flutter app
        /// </summary>
        public const string Appkey = "4Rtv9UH56xWtAyNcS5Yr3jrPmWs26Wa6";

        /// <summary>
        /// Chave interna 8 bits Flutter app
        /// </summary>
        public const string AppIvKey = "anhsudrmfo29xuc8";
    }
}
