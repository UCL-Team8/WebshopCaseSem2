using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace Webshop.Models
{
    public class OrderRepo
    {
        public ObservableCollection<Order> Orders { get; set; }

        private readonly string connectionString;

        public SqlConnection connection;

        public OrderRepo()
        {
            Orders = new ObservableCollection<Order>();
        }

        public OrderRepo(string connectionString)
        {
            Orders = new ObservableCollection<Order>();
            this.connectionString = connectionString;
            connection = new SqlConnection(connectionString);
        }

        public ObservableCollection<Order> GetOrdersByCustomerId(int customerId)
        {
            var query = "SELECT * FROM SHOPPINGCART WHERE CustomerId = @customerId";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    
                    var reader = command.ExecuteReader();
                    var orders = new ObservableCollection<Order>();
                    while (reader.Read())
                    {
                        orders.Add(new Order
                        {
                            OrderID = (int)reader["OrderID"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            // udfyld øvrige felter her
                        });
                    }
                    return orders;
                }
            }
        }

        public Order GetById(int id)
        {
              using (var connection = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                using (var command = new SqlCommand("uspGetOrderById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@OrderId", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Order
                            {
                                OrderID = (int)reader["OrderID"],
                                CustomerID = (int)reader["CustomerID"],
                                OrderDate = (DateTime)reader["OrderDate"],
                                PointsUsed = (int)reader["PointsUsed"],
                                OrderStatusID = (int)reader["OrderStatusID"]
                            };
                        }
                    }
                }
                return null;
            }


            //using (var command = new SqlCommand("uspGetOrderById", connection))
            //{
            //    command.CommandType = CommandType.StoredProcedure;
            //    command.Parameters.AddWithValue("@OrderId", id);

            //    using (var reader = command.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //            return new Order
            //            {
            //                OrderID = (int)reader["OrderID"],
            //                CustomerID = (int)reader["CustomerID"],
            //                OrderDate = (DateTime)reader["OrderDate"],
            //                PointsUsed = (int)reader["PointsUsed"],
            //                OrderStatusID = (int)reader["OrderStatusID"]
            //            };
            //        }
            //    }
            //}
            //return null;
        }

        public void Add(Order order)
        {


            using (var connection = new SqlConnection(connectionString))
            {
                //var command = new SqlCommand(query, connection);
                //command.Parameters.AddWithValue("@customerID", customerId);
                connection.Open();
                using (var command = new SqlCommand("uspCreateOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@PointsUsed", order.PointsUsed);
                    command.Parameters.AddWithValue("@OrderStatusID", order.OrderStatusID);
                    command.Parameters.AddWithValue("@PaymentMethodID", order.PaymentMethodID);
                    


                    SqlParameter orderIdParam = new SqlParameter("@OrderID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(orderIdParam);

                    command.ExecuteNonQuery();
                    order.OrderID = (int)orderIdParam.Value;
                }
                
            }

        }


    }
}

