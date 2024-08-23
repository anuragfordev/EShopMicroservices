using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;


            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product()
            {
              Id= new Guid("019178d1-8986-42dc-92eb-e2208c317aa7"),
              Name= "New Product A",
              Category= new List<string>{"c1", "c2" },
              Description= "Description Product A",
              ImageFile= "Image file for Product A",
              Price= 10
            },
            new Product()
            {
              Id= new Guid("019178d1-8986-42dc-92eb-e2208c318bb8"),
              Name= "New Product B",
              Category= new List<string>{"c3", "c4" },
              Description= "Description Product B",
              ImageFile= "Image file for Product B",
              Price= 90
            },
            new Product()
            {
              Id= new Guid("019178d1-8986-42dc-92eb-e2208c323cc3"),
              Name= "New Product C",
              Category= new List<string>{"c5", "c6" },
              Description= "Description Product C",
              ImageFile= "Image file for Product C",
              Price= 999
            }
        };
    }
}
