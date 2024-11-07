using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Webshop.Models;

namespace Webshop.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly ProductRepo _productRepository; // Repository til håndtering af produkter
        private ObservableCollection<Product> _products; // Samling af produkter
        private Product _selectedProduct; // Den valgte produkt

        public ProductViewModel(ProductRepo productRepository)
        {
            _productRepository = productRepository;
            Products = new ObservableCollection<Product>();
            LoadProducts(); // Indlæs produkter ved initialisering
            //CreateProductCommand = new RelayCommand(CreateProduct);
            //UpdateProductCommand = new RelayCommand(UpdateProduct, CanUpdateProduct);
            //DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
        }

        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                //UpdateProductCommand.RaiseCanExecuteChanged(); // Opdaterer om knappen skal være aktiv
                //DeleteProductCommand.RaiseCanExecuteChanged(); // Opdaterer om knappen skal være aktiv
            }
        }

        public ICommand CreateProductCommand { get; }
        public ICommand UpdateProductCommand { get; }
        public ICommand DeleteProductCommand { get; }

        private void LoadProducts()
        {
            // Indlæs produkter fra databasen
            var products = _productRepository.GetAll(); // Antager GetAll() findes i ProductRepository
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private void CreateProduct()
        {
            // Antag at vi får produktdetaljer fra UI
            var newProduct = new Product
            {
                ProductName = "New Product", // Dette burde komme fra UI input
                Price = 0.0, // Eksempelværdi, bør hentes fra UI
                Stock = 0 // Eksempelværdi, bør hentes fra UI
            };

            _productRepository.Add(newProduct); // Opretter produkt i databasen
            LoadProducts(); // Genindlæs produkter for at opdatere visningen
        }

        private void UpdateProduct()
        {
            if (SelectedProduct != null)
            {
                // Opdater produktdetaljer, antag at vi får ændringer fra UI
                _productRepository.Update(SelectedProduct); // Opdaterer produkt i databasen
                LoadProducts(); // Genindlæs produkter for at opdatere visningen
            }
        }

        private void DeleteProduct()
        {
            if (SelectedProduct != null)
            {
                _productRepository.Remove(SelectedProduct.ProductId); // Sletter produkt
                LoadProducts(); // Genindlæs produkter for at opdatere visningen
            }
        }

        private bool CanUpdateProduct()
        {
            return SelectedProduct != null; // Kontrollerer, om der er et valgt produkt
        }

        private bool CanDeleteProduct()
        {
            return SelectedProduct != null; // Kontrollerer, om der er et valgt produkt
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
