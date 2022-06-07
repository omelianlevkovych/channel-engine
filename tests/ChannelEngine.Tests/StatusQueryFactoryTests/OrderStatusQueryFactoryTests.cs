using ChannelEngine.Application.ChannalEngineApi.Orders;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusConverter;
using ChannelEngine.Application.ChannalEngineApi.Orders.StatusQueryFactory;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ChannelEngine.Tests.StatusQueryFactoryTests
{
    public class OrderStatusQueryFactoryTests
    {
        private readonly IOrderStatusQueryFactory _sut;

        private readonly Mock<IOrderStatusConverter> _statusConverterMoq;

        public OrderStatusQueryFactoryTests()
        {
            _statusConverterMoq = new Mock<IOrderStatusConverter>();
            _sut = new OrderStatusQueryFactory(_statusConverterMoq.Object);
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

            _statusConverterMoq.Setup(x => x.Convert(OrderStatus.InProgress))
                .Returns(inProgressQuery);
            _statusConverterMoq.Setup(x => x.Convert(OrderStatus.InBackorder))
                .Returns(inBackorderQuery);

            // Act.
            var result = _sut.CreateUrl(inputUrl, orderStatuses);

            // Assert.
            Assert.Equal(expectedUrlResult, result);
        }
    }
}
