using Webshop.Models;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void WhenCustomersAreAdded_ShouldIncrementCustomerID()
        {
            //Arrange
            Customer customer1 = new Customer();
            Customer customer2 = new Customer();
            Customer customer3 = new Customer();

            //Act

            //Assert
            Assert.AreEqual(1, customer1.CustomerID);
            Assert.AreEqual(2, customer2.CustomerID);
            Assert.AreEqual(3, customer3.CustomerID);
        }
    }
}