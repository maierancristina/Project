using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HeartPharam.Models
{
    public class PatientDiagnosis
    {
        public int ID { get; set; }
        public int DiagnosisRank { get; set; }
        public string PatientName { get; set; }
        public string PatDiagnos { get; set; }
    }

    public class PatientDDBContext : DbContext
    {
        public DbSet<PatientDiagnosis> PatDiagnosList { get; set; }
    }
}