using MediatR;

namespace ProductServiceAPI.Application.MediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для удаления продукта.
    /// </summary>
    public class DeleteProductCommand : IRequest<bool>
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
