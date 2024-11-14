using Microsoft.AspNetCore.Identity;

namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Сущность для работы с ролями.
    /// </summary>
    public class AppRoles : IdentityRole<int>
    {
        /// <summary>
        /// Идентификатор роли.
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Наименование роли.
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
        /// <summary>
        /// Описание роли.
        /// </summary>
        public string RoleDescription { get; set; } 

    }
}
