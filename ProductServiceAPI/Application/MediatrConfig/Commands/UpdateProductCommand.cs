using MediatR;
using ProductServiceAPI.Domain.Entities;

namespace ProductServiceAPI.Application.MediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для обновления продукта.
    /// </summary>
    public class UpdateProductCommand : IRequest<Product>
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Наименование продукта.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание продукта.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Цена продукта.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Активен ли продукт.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
