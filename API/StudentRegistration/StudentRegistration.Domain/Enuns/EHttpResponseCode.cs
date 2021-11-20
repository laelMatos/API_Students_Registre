using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistration.Domain
{
    public enum EHttpResponseCode
    {
        //---1xx------------------------------------------------------------------------

        //---2xx------------------------------------------------------------------------
        /// <summary>
        /// Estas requisição foi bem sucedida. O significado do sucesso varia de acordo com o método HTTP
        /// </summary>
        OK = 200,
        /// <summary>
        /// A requisição foi bem sucedida e um novo recurso foi criado como resultado. Esta é uma tipica resposta enviada após uma requisição POST
        /// </summary>
        Created = 201,

        //---3xx------------------------------------------------------------------------

        //---4xx------------------------------------------------------------------------
        /// <summary>
        /// Essa resposta significa que o servidor não entendeu a requisição pois está com uma sintaxe inválida.
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Embora o padrão HTTP especifique "unauthorized", semanticamente, essa resposta significa "unauthenticated". Ou seja, o cliente deve se autenticar para obter a resposta solicitada.
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// O cliente não tem direitos de acesso ao conteúdo portanto o servidor está rejeitando dar a resposta. Diferente do código 401, aqui a identidade do cliente é conhecida.
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// O servidor não pode encontrar o recurso solicitado. Este código de resposta talvez seja o mais famoso devido à frequência com que acontece na web.
        /// </summary>
        NotFound = 404,

        //---5xx------------------------------------------------------------------------
    }
}
