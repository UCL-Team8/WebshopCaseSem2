using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Webshop.Models;


namespace Webshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            string? ConnectionString = config.GetConnectionString("DefaultConnection");



            string connectionString = ConnectionString;
            IRepository<Product> productRepository = new ProductRepository(connectionString);



            // Adding a new product
            productRepository.Add(new Product { ProductName = "2024", CategoryID = 1 });

            // Getting all products
            var products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"Product ID: {product.ProductID}, ProductName: {product.ProductName}");
            }

            // Updating a product
            var firstProduct = productRepository.GetById(1);
            if (firstProduct != null)
            {
                firstProduct.CategoryID = 1;
                productRepository.Update(firstProduct);
            }

            // Deleting a product
            productRepository.Delete(1);



            lbproducts.ItemsSource = productRepository.GetAll();

        }
    }
}