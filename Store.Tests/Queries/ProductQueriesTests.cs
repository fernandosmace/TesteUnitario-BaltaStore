using Store.Domain.Entities;
using Store.Domain.Queries;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Store.Tests.Queries
{
    public class ProductQueriesTests
    {
        private IList<Product> _products;

        public ProductQueriesTests()
        {
            _products = new List<Product>();
            _products.Add(new Product("Produto 01", 10, true));
            _products.Add(new Product("Produto 02", 20, true));
            _products.Add(new Product("Produto 03", 30, true));
            _products.Add(new Product("Produto 04", 40, false));
            _products.Add(new Product("Produto 05", 50, false));
        }

        [Fact]
        [Trait("Category", "Queries")]
        public void Dado_A_Consulta_De_Produtos_Ativos_Deve_Retornar_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.Equal(result.Count(), 3);
        }

        [Fact]
        [Trait("Category", "Queries")]
        public void Dado_A_Consulta_De_Produtos_Inativos_Deve_Retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.Equal(result.Count(), 2);
        }
    }
}
