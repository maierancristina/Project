using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeartPharam.Models
{
    public class PharmacyOrder
    {
         public int ID { get; set; }
         public string DrugName { get; set; }
         public string PharmacistName { get; set; }
         public float UnitCost { get; set; }
         public int Quantity { get; set; }
         public float TotalCost { get; set; }
    }


    public class PharmOrdDBContext : DbContext
    {
        public DbSet<PharmacyOrder> PharmOrderList { get; set; }
    }
}