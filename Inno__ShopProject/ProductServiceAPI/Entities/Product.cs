namespace ProductServiceAPI.Entities
{
    /// <summary>
    /// Сущность продукт.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// НАименование.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Доступен ли.
        /// </summary>
        public bool IsAvaliable { get; set; } 
        /// <summary>
        /// Идентификатор пользователя, который взаимодействует с товаром.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Активен ли продукт.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
