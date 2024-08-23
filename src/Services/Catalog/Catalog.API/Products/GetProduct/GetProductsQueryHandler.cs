namespace Catalog.API.Products.GetProduct
{
    public record GetProductsQuery(int? pageIndex=1, int? pageSize=10): IQuery<GetProductResult> { }
    public record GetProductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.pageIndex ?? 1, query.pageSize ?? 10, cancellationToken);

            return new GetProductResult(products);
        }
    }
}
