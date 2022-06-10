using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Orders.StatusConverter;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace ChannelEngine.Application.Tests.ExternalClientTests
{
    public class OrderStatusConverterTests
    {
        private readonly OrderStatusConverter _sut;

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
            inProgress.Should().Be(expectedResults.GetValueOrDefault(OrderStatus.InProgress));
            inBackorder.Should().Be(expectedResults.GetValueOrDefault(OrderStatus.InBackorder));
        }
    }
}
