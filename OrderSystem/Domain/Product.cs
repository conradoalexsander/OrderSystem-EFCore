using OrderSystem.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public ProductType ProductType { get; set; }
        public bool IsActive { get; set; }
    }
}
