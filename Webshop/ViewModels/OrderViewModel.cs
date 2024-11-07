//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using Webshop.Models;

//namespace Webshop.ViewModels
//{
//    public class OrderViewModel : INotifyPropertyChanged
//    {
//        private readonly OrderRepo _orderRepository; // Repository til håndtering af produkter
//        private ObservableCollection<Order> orders; // Samling af produkter
//        private Product _selectedOrder; // Den valgte produkt

//        public OrderViewModel(OrderRepo orderRepository)
//        {
//            _orderRepository = orderRepository;
//            Orders = new ObservableCollection<Order>();
//            LoadProducts(); // Indlæs produkter ved initialisering
//            CreateProductCommand = new RelayCommand(CreateProduct);
//            UpdateProductCommand = new RelayCommand(UpdateProduct, CanUpdateProduct);
//            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
//        }

//        public ObservableCollection<Order> Orders
//        {
//            get => _orders;
//            set
//            {
//                _orders = value;
//                OnPropertyChanged(nameof(Orders));
//            }
//        }

//        public Order SelectedOrder
//        {
//            get => _selectedOrder;
//            set
//            {
//                _selectedOrder = value;
//                OnPropertyChanged(nameof(SelectedOrder));
//                UpdateProductCommand.RaiseCanExecuteChanged(); // Opdaterer om knappen skal være aktiv
//                DeleteProductCommand.RaiseCanExecuteChanged(); // Opdaterer om knappen skal være aktiv
//            }
//        }

//        public ICommand CreateOrderCommand { get; }
//        public ICommand UpdateOrderCommand { get; }
//        public ICommand DeleteOrderCommand { get; }

//        private void LoadProducts()
//        {
//            // Indlæs produkter fra databasen
//            var products = _orderRepository.GetAll(); // Antager GetAll() findes i ProductRepository
//            Orders.Clear();
//            foreach (var order in orders)
//            {
//                Orders.Add(order);
//            }
//        }

//        private void CreateOrder()
//        {
//            // Antag at vi får produktdetaljer fra UI
//            var newOrder = new Order
//            {
//                OrderName = "New Product", // Dette burde komme fra UI input
//                Price = 0.0m, // Eksempelværdi, bør hentes fra UI
//                StockQuantity = 0 // Eksempelværdi, bør hentes fra UI
//            };

//            _productRepository.Add(newProduct); // Opretter produkt i databasen
//            LoadProducts(); // Genindlæs produkter for at opdatere visningen
//        }

//        private void UpdateProduct()
//        {
//            if (SelectedProduct != null)
//            {
//                // Opdater produktdetaljer, antag at vi får ændringer fra UI
//                _productRepository.Update(SelectedProduct); // Opdaterer produkt i databasen
//                LoadProducts(); // Genindlæs produkter for at opdatere visningen
//            }
//        }

//        private void DeleteProduct()
//        {
//            if (SelectedProduct != null)
//            {
//                _productRepository.Remove(SelectedProduct.ProductId); // Sletter produkt
//                LoadProducts(); // Genindlæs produkter for at opdatere visningen
//            }
//        }

//        private bool CanUpdateProduct()
//        {
//            return SelectedProduct != null; // Kontrollerer, om der er et valgt produkt
//        }

//        private bool CanDeleteProduct()
//        {
//            return SelectedProduct != null; // Kontrollerer, om der er et valgt produkt
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        protected virtual void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//    }
//}
