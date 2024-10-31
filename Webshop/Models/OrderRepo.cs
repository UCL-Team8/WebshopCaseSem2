using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Webshop.Models
{
    public class OrderRepo
    {
        public ObservableCollection<Order> Orders { get; set; }

        private readonly string connectionString;

        public OrderRepo()
        {
            Orders = new ObservableCollection<Order>();
        }

        public OrderRepo(string connectionString)
        {
            Orders = new ObservableCollection<Order>();
            this.connectionString = connectionString;
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
                            OrderID = (int)reader["OrderId"],
                            OrderDate = (DateTime)reader["OrderDate"],
                            // udfyld øvrige felter her
                        });
                    }
                    return orders;
                }
            }
        }
    }
}

