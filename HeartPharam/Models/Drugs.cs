using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeartPharam.Models
{
    public class Drugs
    {
        public int ID { get; set; }
        public string DrugName { get; set; }
        public string Dosage { get; set; }
        public string Instructions { get; set; }
        public int Stock { get; set; }
    }

    public class MedicationDBContext : DbContext
    {
        public DbSet<Drugs> DrugsList { get; set; }
    }
}