
namespace CatalogAPI.Products.DeleteProduct
{
    public record DeleteProductResult(bool IsSuccess);
    public record DeleteProductCommand(Guid Id): ICommand<DeleteProductResult>;
    internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var product = session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product != null) { 
                session.Delete<Product>(command.Id);
                await session.SaveChangesAsync(cancellationToken);
                return new DeleteProductResult(true);
            }
            throw new ProductNotFoundException();
        }
    }
}
