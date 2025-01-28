namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductResult(bool IsSuccess);
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
        : ICommand<UpdateProductResult>;
    internal class UpdateProducCommandtHandler(IDocumentSession session) 
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
                var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product != null)
            {
                product.Name = command.Name;
                product.Description = command.Description;
                product.Category = command.Category;
                product.Price = command.Price;
                product.ImageFile = command.ImageFile;

                session.Update(product);
                await session.SaveChangesAsync(cancellationToken);
                return new UpdateProductResult(true);
            }
            throw new ProductNotFoundException();
        }
    }
}
