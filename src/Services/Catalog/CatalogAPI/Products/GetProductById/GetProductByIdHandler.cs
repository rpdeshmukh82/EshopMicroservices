

namespace CatalogAPI.Products.GetProductById
{
    public record GetProductByIdResult(Product Product);
    public record GetProductByIdQuery(Guid ProductId): IQuery<GetProductByIdResult>;
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryhandler.Handle called with {@Query}", query);
            var product = await session.LoadAsync<Product>(query.ProductId, cancellationToken);
            if (product is null) {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
