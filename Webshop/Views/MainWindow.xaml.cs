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

            OrderRepo orderRepo = new OrderRepo("Server=localhost;Database=webshop2;Trusted_Connection=True;TrustServerCertificate=true;");

            lborders.ItemsSource = orderRepo.GetOrdersByCustomerId(1);
        }
    }
}