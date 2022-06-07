using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using System.Collections.Generic;
using Xunit;

namespace ChannelEngine.Tests.ChannelEngineApiTests
{
    public class OrderStatusConverterTests
    {
        private readonly IOrderStatusConverter _sut;

        public OrderStatusConverterTests()
        {
            _sut = new OrderStatusConverter();
        }

        [Fact]
        public void Should_ConvertCorrectly()
        {
            // Arrange.
            Dictionary<OrderStatus, string> expectedResults = new()
            {
                { OrderStatus.InProgress, "IN_PROGRESS" },
                { OrderStatus.InBackorder, "IN_BACKORDER" },
            };

            // Act.
            string inProgress = _sut.Convert(OrderStatus.InProgress);
            string inBackorder = _sut.Convert(OrderStatus.InBackorder);

            // Assert.
            Assert.Equal(expectedResults.GetValueOrDefault(OrderStatus.InProgress), inProgress);
            Assert.Equal(expectedResults.GetValueOrDefault(OrderStatus.InBackorder), inBackorder);
        }
    }
}
