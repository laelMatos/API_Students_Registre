using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Atribui as propriedades de tipos e nomes iguais de um objeto em outro
    /// </summary>
    public static class BindObject
    {
        public static IEnumerable<T> BindRange<T>(IEnumerable<object> objs) where T : class
        {
            List<T> newObjReturn = (List<T>)Activator.CreateInstance(typeof(List<T>));
            foreach (var obj in objs)
            {
                newObjReturn.Add((T)Bind<T>(obj));
            }

            return newObjReturn;
        }
        
        /// <summary>
        /// Atribui as propriedades de tipos e nomes iguais de um objeto em outro
        /// </summary>
        /// <param name="obj">Objeto que contem os valores</param>
        /// <returns>Objeto do tipo '<T>'</returns>
        public static T Bind<T>(object obj) where T : class
        {
            Object objReturn = Activator.CreateInstance(typeof(T));
            foreach (PropertyInfo prop in objReturn.GetType().GetProperties())
            {
                //Se não encontrar a propriedade pula para proxima
                var propName = obj.GetType().GetProperty(prop.Name);
                if (propName == null)
                    continue;
                try
                {
                objReturn.GetType().GetProperty(prop.Name).SetValue(objReturn, propName.GetValue(obj));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return (T)objReturn;
        }
    }
}
