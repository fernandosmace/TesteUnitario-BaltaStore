﻿using Store.Domain.Entities;
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

        [Fact]
        public void Dado_Um_Novo_Item_Sem_Produto_O_Mesmo_Nao_Deve_Ser_Adicionado()
        {
            var order = new Order(_customer, 0, null);
            var orderCount = order.Items.Count;

            order.AddItem(null, 1);

            Assert.Equal(orderCount, order.Items.Count);

        }

        [Fact]
        public void Dado_Um_Novo_Item_Com_Quantidade_Zero_Ou_Menor_O_Mesmo_Nao_Deve_Ser_Adicionado()
        {
            var order = new Order(_customer, 0, null);
            var orderCount = order.Items.Count;

            order.AddItem(_product, -1);

            order.AddItem(_product, 0);

            Assert.Equal(orderCount, order.Items.Count);

        }

        [Fact]
        public void Dado_Um_Novo_Pedido_Valido_Seu_Total_Deve_Ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);

            Assert.Equal(order.Total(), 50);

        }

        [Fact]
        public void Dado_Um_Desconto_Expirado_O_Valor_Do_Pedido_Deve_Ser_60()
        {
            var expiredDiscount = new Discount(10, DateTime.Now.AddDays(-5));
            var order = new Order(_customer, 10, expiredDiscount);
            order.AddItem(_product, 5);

            Assert.Equal(order.Total(), 60);
        }

        [Fact]
        public void Dado_Um_Desconto_Invalido_O_Valor_Do_Pedido_Deve_Ser_60()
        {
            var order = new Order(_customer, 10, null);
            order.AddItem(_product, 5);

            Assert.Equal(order.Total(), 60);
        }

        [Fact]
        public void Dado_Um_Desconto_De_10_O_Valor_Do_Pedido_Deve_Ser_50()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 5);

            Assert.Equal(order.Total(), 50);
        }

        [Fact]
        public void Dado_Uma_Taxa_De_Entrega_De_10_O_Valor_Do_Pedido_De_Ser_60()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product, 6);

            Assert.Equal(order.Total(), 60);
        }

        [Fact]
        public void Dado_Um_Pedido_Sem_Cliente_O_Mesmo_Deve_Ser_Invalido()
        {
            var order = new Order(null, 10, _discount);

            Assert.Equal(order.Valid, false);
        }
    }
}
