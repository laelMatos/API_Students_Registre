using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Util
    {
        /// <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text">Texto a separar apenas os numericos</param>
        /// <returns>Numeros do texto</returns>
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Converte um objeto em Json (string)
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser convertido</typeparam>
        /// <param name="obj">Objeto a ser convertido</param>
        /// <returns>Json no formato string</returns>
        public static string ConvertObjectToJSon<T>(T obj)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Converte Json em objeto
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="jsonString">Json no formato string</param>
        /// <returns>objeto serializado</returns>
        public static T ConvertJSonToObject<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)serializer.ReadObject(ms);
                return obj;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Encriptar usando algoritmo AES
        /// </summary>
        /// <param name="textToEncrypt"></param>
        /// <param name="publickey"></param>
        /// <param name="secretkey"></param>
        /// <returns></returns>
        public static string Encrypt(string textToEncrypt, string publickey, string secretkey)
        {
            try
            {    
                byte[] encrypted;
                byte[] Key = System.Text.Encoding.UTF8.GetBytes(publickey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(secretkey);
                using (AesManaged aes = new AesManaged())
                {
                    ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                                sw.Write(textToEncrypt);
                            encrypted = ms.ToArray();
                        }
                    }
                }

                return Convert.ToBase64String(encrypted);
            }
            catch 
            {
                throw;
            }
        }

        /// <summary>
        /// decriptar cifra feita com algoritmo AES
        /// </summary>
        /// <param name="textToDecrypt"></param>
        /// <param name="publickey"></param>
        /// <param name="secretkey"></param>
        /// <returns></returns>
        public static string Decrypt(string textToDecrypt, string publickey, string secretkey)
        {
            try
            {              
                string plaintext = null;
                byte[] value = Convert.FromBase64String(textToDecrypt);
                byte[] Key = System.Text.Encoding.UTF8.GetBytes(publickey);
                byte[] IV = System.Text.Encoding.UTF8.GetBytes(secretkey);

                using (AesManaged aes = new AesManaged())
                {
                    ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(cs))
                                plaintext = reader.ReadToEnd();
                        }
                    }
                }
                return plaintext;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Gera um código com letras e números
        /// </summary>
        /// <param name="lenght">Quantidade de carecteres</param>
        /// <returns>Codigo gerado</returns>
        public static string GenerateAlphanumericCode(int lenght)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, lenght)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }

        public static string GenerateHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static string GenerateSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
