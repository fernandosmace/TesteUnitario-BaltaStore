using Store.Domain.Entities;
using Xunit;

namespace Store.Tests.Entities
{
    public class OrderTests
    {
        [Fact]
        public void Dado_Um_Novo_Pedido_Valido_Ele_Deve_Gerar_Um_Novo_Numero_Com_8_Caracteres()
        {
            var customer = new Customer("Teste", "teste@teste.com.br");

            var order = new Order(customer, 15.00m, null);

            Assert.Equal(8, order.Number.Length);

        }

    }
}
