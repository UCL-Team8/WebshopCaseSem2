using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();
            string query = "SELECT * FROM PRODUCTS";

            using (SqlConnection connection = new SqlConnection(_connectionString))
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
                            CategoryID = (int)reader["CategoryID"]
                        });
                    }
                }
            }

            return products;
        }

        public Product GetById(int id)
        {
            Product product = null;
            string query = "SELECT * FROM PRODUCTS WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Product
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = (string)reader["ProductName"]
                        };
                    }
                }
            }

            return product;
        }

        public void Add(Product product)
        {
            string query = "INSERT INTO PRODUCTS (CategoryID) VALUES (@CategoryID)";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            string query = "UPDATE PRODUCTS SET Number = @Number WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@ProductID", product.ProductID);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM PRODUCTS WHERE ProductID = @ProductID";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
