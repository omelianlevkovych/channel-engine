using ChannelEngine.Application.External.Orders;
using ChannelEngine.Application.External.Orders.StatusConverter;
using ChannelEngine.Application.External.Orders.StatusQueryFactory;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using Xunit;

namespace ChannelEngine.Application.Tests.StatusQueryFactoryTests
{
    public class OrderStatusQueryFactoryTests
    {
        private readonly OrderStatusQueryFactory _sut;

        private readonly IOrderStatusConverter _statusConverter = Substitute.For<IOrderStatusConverter>();

        public OrderStatusQueryFactoryTests()
        {
            _sut = new OrderStatusQueryFactory(_statusConverter);
        }

        /*
            (this logic is done only it terms of test project)
            TODO: Endpoint probably should not have dublicates in query parameters!
            Its hard to rely on this endpoint behavior and some results can be unexpected.
            Lets assume this test was added jsut for backward compatibility.
         */
        [Fact]
        public void Should_BuildQuery_Correctly()
        {
            // Arrange.
            const string inProgressQuery = "IN_PROGRESS";
            const string inBackorderQuery = "IN_BACKORDER";

            const string inputUrl = "https://dev-test/orders";
            List<OrderStatus> orderStatuses = new()
            {
                { OrderStatus.InProgress },
                { OrderStatus.InBackorder },
                { OrderStatus.InProgress },
            };

            const string expectedUrlResult = $"{inputUrl}?statuses={inProgressQuery}&statuses={inBackorderQuery}";

            _statusConverter.Convert(OrderStatus.InProgress).Returns(inProgressQuery);

            _statusConverter.Convert(OrderStatus.InBackorder).Returns(inBackorderQuery);

            // Act.
            var result = _sut.CreateUrl(inputUrl, orderStatuses);

            // Assert.
            result.Should().Be(expectedUrlResult);
        }
    }
}
