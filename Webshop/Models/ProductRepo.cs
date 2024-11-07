using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class ProductRepo
    {

        public ObservableCollection<Product> Products { get; set; }

        private readonly string connectionString;

        public SqlConnection connection;

        public ProductRepo()
        {
            Products = new ObservableCollection<Product>();
        }

        public ProductRepo(string connectionString)
        {
            Products = new ObservableCollection<Product>();
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public void Add(Product product)
        {

        }


        //public ObservableCollection<Product> GetOrdersByProductId(int customerId)
        //{
        //    var query = "SELECT * FROM SHOPPINGCART WHERE CustomerId = @customerId";

        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        var command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@customerID", customerId);
        //        connection.Open();
        //        using (SqlCommand cmd = new SqlCommand(query, connection))
        //        {

        //            var reader = command.ExecuteReader();
        //            var orders = new ObservableCollection<Order>();
        //            while (reader.Read())
        //            {
        //                orders.Add(new Order
        //                {
        //                    OrderID = (int)reader["OrderID"],
        //                    OrderDate = (DateTime)reader["OrderDate"],
        //                    // udfyld øvrige felter her
        //                });
        //            }
        //            return orders;
        //        }
        //    }
        //}

        //public Order GetById(int id)
        //{
        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        //var command = new SqlCommand(query, connection);
        //        //command.Parameters.AddWithValue("@customerID", customerId);
        //        connection.Open();
        //        using (var command = new SqlCommand("uspGetOrderById", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@OrderId", id);

        //            using (var reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    return new Order
        //                    {
        //                        OrderID = (int)reader["OrderID"],
        //                        CustomerID = (int)reader["CustomerID"],
        //                        OrderDate = (DateTime)reader["OrderDate"],
        //                        PointsUsed = (int)reader["PointsUsed"],
        //                        OrderStatusID = (int)reader["OrderStatusID"]
        //                    };
        //                }
        //            }
        //        }
        //        return null;
        //    }

        //}

        public Product GetAll(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                using (var command = new SqlCommand("upsGetProducts", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    //command.Parameters.AddWithValue("@OrderId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = (string)reader["ProductName"],
                                Description = (string)reader["Description"],
                                Price = (double)reader["Price"],
                                Stock = (int)reader["StockQuantity"],
                                CategoryID = (int)reader["CateegoryID"]
                            };
                        }
                    }
                }
                return null;
            }

        }

        public IEnumerable<Product> GetAll()
        {
            var products = new ObservableCollection<Product>();
            string query = "SELECT * FROM ASSIGMENTS";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"],
                            Description = (string)reader["Description"],
                            Price = (double)reader["Price"],
                            Stock = (int)reader["StockQuantity"],
                            CategoryID = (int)reader["CateegoryID"]
                        });
                    }
                }
            }

            return products;
        }



        //public void Add(Order order)
        //{


        //    using (var connection = new SqlConnection(connectionString))
        //    {
        //        //var command = new SqlCommand(query, connection);
        //        //command.Parameters.AddWithValue("@customerID", customerId);
        //        connection.Open();
        //        using (var command = new SqlCommand("uspCreateOrder", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
        //            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
        //            command.Parameters.AddWithValue("@PointsUsed", order.PointsUsed);
        //            command.Parameters.AddWithValue("@OrderStatusID", order.OrderStatusID);
        //            command.Parameters.AddWithValue("@PaymentMethodID", order.PaymentMethodID);



        //            SqlParameter orderIdParam = new SqlParameter("@OrderID", SqlDbType.Int) { Direction = ParameterDirection.Output };
        //            command.Parameters.Add(orderIdParam);

        //            command.ExecuteNonQuery();
        //            order.OrderID = (int)orderIdParam.Value;
        //        }

        //    }

        //}
    }
}
