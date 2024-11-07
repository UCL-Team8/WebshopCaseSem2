using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public double OrderAmount { get; set; }
        public int Bonus { get; set; }
        public int ProductAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public int PointsUsed { get; set; }
        public int OrderStatusID { get; set; }

        public int PaymentMethodID { get; set; }

        public int LatestID { get; set; }
    }
}
