using MediatR;

namespace ProductServiceAPI.Application.MediatrConfig.Commands
{
    /// <summary>
    /// Модель команды добавления продукта.
    /// </summary>
    public class AddProductCommand : IRequest<string>
    {
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
        /// Идентификтор пользователя.
        /// </summary>
        public int UserID { get; set; }
    }
}
