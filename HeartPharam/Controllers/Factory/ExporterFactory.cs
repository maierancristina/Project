using HeartPharam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartPharam.Controllers.Factory
{
    public interface ExporterFactory
    {
        Exporter CreateFile(string type, List<PharmacyOrder> info);
    }
}
