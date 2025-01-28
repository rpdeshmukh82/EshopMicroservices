
namespace CatalogAPI.Products.GetProducts
{
    public record GetProdutsResult(IEnumerable<Product> Products);
    public record GetProductsQuery() : IQuery<GetProdutsResult>;
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProdutsResult>
    {
        public async Task<GetProdutsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryhandler.Handle called with {@Query}", query);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetProdutsResult(products);
        }
    }
}
