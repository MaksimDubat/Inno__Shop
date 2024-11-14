﻿using Microsoft.AspNetCore.Identity;
using UserServiceAPI.Infrastructure.Common;

namespace UserServiceAPI.Entities
{
    /// <summary>
    /// Сущность для работы с пользователем.
    /// </summary>
    public class AppUsers : IdentityUser<int>
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId {  get; set; }
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// Роль пользователя.
        /// </summary>
        public UserRole Role { get; set; }
        /// <summary>
        /// Дата создания пользователя.
        /// </summary>
        public DateTime CreatedDate { get; set; }

    }
}