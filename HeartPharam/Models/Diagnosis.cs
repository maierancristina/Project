using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeartPharam.Models
{
    public class Diagnosis
    {
        public int ID { get; set; }
        public string Notes { get; set; }
    }

    public class DiagnosisDBContext : DbContext
    {
        public DbSet<Diagnosis> DiagnosList { get; set; }
    }
}