using Store.Domain.Entities;
using Store.Domain.Enums;
using System;
using Xunit;

namespace Store.Tests.Entities
{
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Teste", "teste@teste.com.br");
        private readonly Product _product = new Product("Produto 1", 10, true);
        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [Fact]
        public void Dado_Um_Novo_Pedido_Valido_Ele_Deve_Gerar_Um_Novo_Numero_Com_8_Caracteres()
        {
            var order = new Order(_customer, 0, null);

            Assert.Equal(order.Number.Length, 8);
        }

        [Fact]
        public void Dado_Um_Novo_Pedido_Seu_Status_Deve_Ser_AguardandoPagamento()
        {
            var order = new Order(_customer, 0, null);

            Assert.Equal(order.Status, EOrderStatus.WaitingPayment);
        }

        [Fact]
        public void Dado_Um_Pagamento_Do_Pedido_Seu_Status_Deve_Ser_Aguardando_Entrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(10);

            Assert.Equal(order.Status, EOrderStatus.WaitingDelivery);
        }

        [Fact]
        public void Dado_Um_Pedido_Cancelado_Seu_Status_Deve_Ser_Cancelado()
        {
            var order = new Order(_customer, 0, null);
            order.Cancel();

            Assert.Equal(order.Status, EOrderStatus.Canceled);
        }


    }
}
