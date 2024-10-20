using Microsoft.EntityFrameworkCore;
using Moq;
using SalesOrders.Shared;
using SalesOrders.Shared.Orders;
using SalesOrders.Shared.Orders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesOrders.XUnitTests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderService> _orderServiceMock;

        public OrderServiceTests()
        {
            _orderServiceMock = new Mock<IOrderService>();
        }

        [Fact]
        public async Task GetOrders_ShouldReturnOrders_WhenFiltersAreApplied()
        {
            _orderServiceMock.Setup(service => service.GetOrders(It.IsAny<viewOrdersFilters>()))
                 .ReturnsAsync((viewOrdersFilters filters) =>
                 {
                     // Apply filter logic for "Normal" orderType
                     var filteredOrders = new List<viewOrdersVM>
                     {
                        new viewOrdersVM
                        {
                            orderId = 1,
                            orderNumber = "ORD001",
                            orderType = "Normal",
                            orderStatus = "New",
                            customerName = "John Doe",
                            createdDate = DateTime.Now.AddDays(-10),
                            orderLines = new List<OrderLineVM>
                            {
                                new OrderLineVM { lineId = 1, lineNumber = 1, productCode = "P001", productType = "TypeA", costPrice = 100, salesPrice = 120, quantity = 10 },
                                new OrderLineVM { lineId = 2, lineNumber = 2, productCode = "P002", productType = "TypeB", costPrice = 150, salesPrice = 180, quantity = 5 }
                            }
                        },
                        new viewOrdersVM
                        {
                            orderId = 4,
                            orderNumber = "ORD004",
                            orderType = "Normal",
                            orderStatus = "Shipped",
                            customerName = "David Johnson",
                            createdDate = DateTime.Now.AddDays(-12),
                            orderLines = new List<OrderLineVM>
                            {
                                new OrderLineVM { lineId = 6, lineNumber = 1, productCode = "P006", productType = "TypeB", costPrice = 90, salesPrice = 110, quantity = 20 }
                            }
                        },
                        new viewOrdersVM
                        {
                            orderId = 8,
                            orderNumber = "ORD008",
                            orderType = "Test",
                            orderStatus = "New",
                            customerName = "Chris Black",
                            createdDate = DateTime.Now.AddDays(-2),
                            orderLines = new List<OrderLineVM>
                            {
                                new OrderLineVM { lineId = 11, lineNumber = 1, productCode = "P011", productType = "TypeA", costPrice = 220, salesPrice = 250, quantity = 9 }
                            }
                        }
                     };

                     // Apply filter based on the passed filters
                     if (!string.IsNullOrEmpty(filters.orderType))
                     {
                         filteredOrders = filteredOrders.Where(x => x.orderType == filters.orderType).ToList();
                     }

                     return new ServiceResponse<List<viewOrdersVM>>
                     {
                         Data = filteredOrders
                     };
                 });



            // Act
            var filters = new viewOrdersFilters { orderType = "Normal" };
            var result = await _orderServiceMock.Object.GetOrders(filters);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Data.Count); // Should return 2 orders based on the "Normal" filter
            

        }

    }

}
