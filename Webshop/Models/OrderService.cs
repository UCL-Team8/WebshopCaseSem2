//using Microsoft.Data.SqlClient;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;

//namespace Webshop.Models
//{
//    public class OrderService
//    {
//        private readonly OrderRepo _orderRepository;
//        private readonly OrderItemRepository _orderItemRepository;
//        private readonly ProductRepository _productRepository;

//        public OrderService(SqlConnection connection)
//        {
//            _orderRepository = new OrderRepository(connection);
//            _orderItemRepository = new OrderItemRepository(connection);
//            _productRepository = new ProductRepository(connection);
//        }

//        public void CreateOrder(Order order, List<OrderItem> orderItems)
//        {
//            using (var transaction = _orderRepository.Connection.BeginTransaction())
//            {
//                try
//                {
//                    // Opret ordre
//                    _orderRepository.Add(order);

//                    // Opret ordrelinjer
//                    foreach (var item in orderItems)
//                    {
//                        item.OrderId = order.OrderId; // Sæt OrderId for ordrelinjen
//                        _orderItemRepository.Add(item);
//                    }
//                    // Opdater lagerbeholdning
//                    var newStockQuantity = _productRepository.GetStockQuantity(item.ProductId) - item.Quantity;
//                    _productRepository.UpdateStockQuantity(item.ProductId, newStockQuantity);
//                }

//                // Commit transaction
//                transaction.Commit();
//            }
//            catch (Exception e)
//            {
//                // Rul tilbage transaktionen ved fejl
//                transaction.Rollback();
//                throw; // Re-throw exception to handle it upstream
//            }
//        }
//    }

//    // Yderligere metoder til at opdatere ordre, beregne point osv.
//}
