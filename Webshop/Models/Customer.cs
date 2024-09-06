using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop.Models
{
    public class Customer
    {      
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Points { get; set; }

        //Added for the purpose of unit testing
        public static int NextID = 1;

        public Customer()
        {
            CustomerID = NextID;
            NextID++;
        }
    }
}
