using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories.Interfaces;
using Store.Tests.Repositories;
using System;
using Xunit;

namespace Store.Tests.Handlers
{
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _productRepository = new FakeProductRepository();
            _orderRepository = new FakeOrderRepository();
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Cliente_Inexistente_O_Pedido_Nao_Deve_Ser_Gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, false);
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Cep_Invalido_O_Pedido_Deve_Ser_Gerado_Normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678911";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, true);
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Promocode_Inexistente_O_Pedido_Deve_Ser_Gerado_Normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678911";
            command.ZipCode = "12345678";
            command.PromoCode = "11111511";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, true);
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Pedido_Sem_Itens_O_Mesmo_Nao_Deve_Ser_Gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678911";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, false);
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Comando_Invalido_O_Pedido_Nao_Deve_Ser_Gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, false);
        }

        [Fact]
        [Trait("Category", "Handlers")]
        public void Dado_Um_Comando_Valido_O_Pedido_Deve_Ser_Gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12345678911";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, _discountRepository, _productRepository, _orderRepository);
            handler.Handle(command);

            Assert.Equal(handler.Valid, true);
        }
    }
}
