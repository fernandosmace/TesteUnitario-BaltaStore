using Store.Domain.Commands;
using System;
using Xunit;

namespace Store.Tests.Commands
{
    public class CreateOrderCommandTests
    {
        [Fact]
        [Trait("Category", "Commands")]
        public void Dado_Um_Comand_Invalido_O_Pedido_Deve_Nao_Deve_Ser_Gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.Equal(command.Valid, false);
        }
    }
}
