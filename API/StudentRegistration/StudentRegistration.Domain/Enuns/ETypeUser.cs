using System.ComponentModel;

namespace StudentRegistration.Domain.Enuns
{
    /// <summary>
    /// Tipo de usuario
    /// </summary>
    public enum ETypeUser
    {
        /// <summary>
        /// Usuário
        /// </summary>
        [Description("Usuário")]
        User = 1,
        /// <summary>
        /// Administrador
        /// </summary>
        [Description("Administrador")]
        Adm = 2
    }
}
