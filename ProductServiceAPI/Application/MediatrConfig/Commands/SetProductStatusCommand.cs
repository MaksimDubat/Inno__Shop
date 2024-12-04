using MediatR;

namespace ProductServiceAPI.Application.MediatrConfig.Commands
{
    /// <summary>
    /// Модель команды для изменения статуса продукта.
    /// </summary>
    public class SetProductStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// Идентификатор продукта.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Активен ли продукт.
        /// </summary>
        public bool IsActive { get; set; }

        public SetProductStatusCommand(int id, bool isActive)
        {
            Id = id;
            IsActive = isActive;
        }
    }
}
