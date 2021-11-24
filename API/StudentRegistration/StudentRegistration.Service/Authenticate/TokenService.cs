using Common;
using Microsoft.IdentityModel.Tokens;
using StudentRegistration.Domain;
using StudentRegistration.Domain.Enuns;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentRegistration.Service
{
    public static class TokenService
    {
        /// <summary>
        /// Tempo de validade do token
        /// </summary>
        private const double EXPIRE_HOURS = 2.0;

        /// <summary>
        /// Gera o token de autorização de usuário
        /// </summary>
        /// <param name="user">Usuário a ser autorizado</param>
        /// <returns>Token</returns>
        public static string GenerateToken(User user)
        {
            try
            {
                //Valida se o usuario e os dados são existentes
                if (user == null || user.Name == null || user.Id == 0)
                {
                    return string.Empty;
                }

                //cria chave para encriptação do token
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
                var tokenHandler = new JwtSecurityTokenHandler();
                
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("TypeUser", user.Type.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(descriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception)
            {
                throw new Exception("Falha na autenticação do usuário");
            }

        }

        /// <summary>
        /// Pega os dados do token JWT
        /// </summary>
        /// <param name="jwt">Token</param>
        /// <returns>Dados</returns>
        public static JwtSecurityToken GetAutorizeData(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                return handler.ReadJwtToken(jwt);
            }
            catch (Exception)
            {
                throw new Exception("Falha na leitura do token");
            }
            
        }

        /// <summary>
        /// Gera o token para recuperação de senha via web
        /// </summary>
        /// <param name="token">Código de autorização</param>
        /// <param name="expirate">data de expiração</param>
        /// <returns>Token encriptado</returns>
        public static string GenerateTokenReplacePass(string token, DateTime expirate)
        {
            var t = new tokenReplacePass()
            {
                Token = token,
                ValidateTime = expirate
            };

            var tokenJson = Util.ConvertObjectToJSon(t);

            return Util.Encrypt(tokenJson, Settings.publickey, Settings.Secretkey);
        }

        /// <summary>
        /// Dados do token
        /// </summary>
        /// <param name="token">Token encriptado</param>
        /// <returns>Objeto com os dados</returns>
        public static tokenReplacePass GetDataTokenReplacePass(string token)
        {
            var tokenData = Util.Decrypt(token, Settings.publickey, Settings.Secretkey);
            return Util.ConvertJSonToObject<tokenReplacePass>(tokenData);
        }

        public static string GetCodeToken(tokenReplacePass tokenData)
        {
            return Util.Decrypt(tokenData.Token, Settings.publickey, Settings.Secretkey);
        }

        public struct tokenReplacePass
        {
            public string Token { get; set; }
            public DateTime ValidateTime { get; set; }
        }
    }
}
