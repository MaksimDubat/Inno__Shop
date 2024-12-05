using MediatR;
using ProductServiceAPI.Application.MediatrConfig.Commands;
using ProductServiceAPI.Interface;
using UserServiceAPI.Domain.Interface;

namespace ProductServiceAPI.Application.MediatrConfig.Hendlers
{
    /// <summary>
    /// Обработчик команды для удаления продукта.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repository;
        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            return true;
        }
    }
}
