using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Models;

namespace TestProject1
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private SqlConnection _connection;
        private OrderRepo _orderRepository;

        [TestInitialize]
        public void Setup()
        {
            _connection = new SqlConnection("Server=localhost;Database=webshop2;Trusted_Connection=True;TrustServerCertificate=true;");
            _orderRepository = new OrderRepo("Server=localhost;Database=webshop2;Trusted_Connection=True;TrustServerCertificate=true;");
            _connection.Open();
        }


        [TestMethod]
        public void AddOrder_ShouldAddOrderSuccessfully()
        {
            // Arrange
            var order = new Order
            {
                CustomerID = 1,
                OrderDate = DateTime.Now,
                PointsUsed = 0,
                OrderStatusID = 1,
                PaymentMethodID = 1
            };

            // Act
            _orderRepository.Add(order);

            // Assert
            var retrievedOrder = _orderRepository.GetById(order.OrderID);
            Assert.IsNotNull(retrievedOrder);
            Assert.AreEqual(order.CustomerID, retrievedOrder.CustomerID);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }
    }
}
