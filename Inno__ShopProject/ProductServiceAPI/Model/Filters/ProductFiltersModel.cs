namespace ProductServiceAPI.Model.Filters
{
    /// <summary>
    /// Модель фильтрации товаров.
    /// </summary>
    public class ProductFiltersModel
    {
        /// <summary>
        /// Наименование товара.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Максимальная цена.
        /// </summary>
        public decimal? MaxPrice { get; set; }
        /// <summary>
        /// Минимальная цена.
        /// </summary>
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// Доступность товара.
        /// </summary>
        public bool? IsActive { get; set; } 
        /// <summary>
        /// Создан после.
        /// </summary>
        public DateTime? CreatedAfter { get; set; }
        /// <summary>
        /// Создан до.
        /// </summary>
        public DateTime? CreatedBefore { get; set; }
    }
}
