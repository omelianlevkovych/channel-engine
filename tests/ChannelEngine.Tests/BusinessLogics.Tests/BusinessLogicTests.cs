using ChannelEngine.Application.BusinessLogics;
using ChannelEngine.Application.External.Client.Interfaces;
using ChannelEngine.Application.External.Responses;
using ChannelEngine.Application.Models;
using ChannelEngine.Application.Storage.Interfaces;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ChannelEngine.Application.Tests.BusinessLogics.Tests
{
    public class BusinessLogicTests
    {
        private readonly BusinessLogic _sut;
        private readonly IInMemoryStorage _storage = Substitute.For<IInMemoryStorage>();
        private readonly IChannelEngineApiClient _apiClient = Substitute.For<IChannelEngineApiClient>();

        public BusinessLogicTests()
        {
            _sut = new BusinessLogic(_storage, _apiClient);
        }

        [Fact]
        public async Task GetTopProductsDesc_Should_Return_Correctly()
        {
            // Arrange.
            const int takeCount = 5;
            const string productName = "default-name";

            _apiClient.GetProduct(Arg.Any<string>()).Returns(new ProductResponse
            {
                Content = new ProductContentResponse()
                {
                    Name = productName,
                }
            });

            var order101 = new OrderModel
            {
                Id = 101,
                Status = "status-101",
            };
            var product11 = new ProductModel("id-11", "gtn-11", 2);
            product11.SetName(productName);

            var products101 = new List<ProductModel>
            {
                product11,
            };
            order101.AddProducts(products101);

            var order102 = new OrderModel
            {
                Id = 102,
                Status = "status-102",
            };

            var product21 = new ProductModel("id-21", "gtn-21", 36);
            product21.SetName(productName);
            var product22 = new ProductModel("id-22", "gtn-22", 1);
            product22.SetName(productName);
            var product23 = new ProductModel("id-23", "gtn-23", 6);
            product23.SetName(productName);

            var products102 = new List<ProductModel>
            {
                product21,
                product22,
                product23,
            };
            order102.AddProducts(products102);

            // second order

            var ordersInProgress = new List<OrderModel>
            {
                order101,
                order102,
            };

            _storage.OrdersInProgress.Returns(ordersInProgress);

            var expectedResult = new List<ProductModel>
            {
                product11,
                product21,
                product22,
                product23,
            };

            // Act.
            var result = await _sut.GetTopProductsDesc(takeCount);

            // Assert.
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
