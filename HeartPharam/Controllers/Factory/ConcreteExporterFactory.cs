using HeartPharam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeartPharam.Controllers.Factory
{
    public class ConcreteExporterFactory : ExporterFactory
    {
        public Exporter CreateFile(string type, List<PharmacyOrder> info)
        {
            if (type == "CSV") return new ConcreteCSV(info);
            else return new ConcreteJSON(info);
        }
    }
}