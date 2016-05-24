using HeartPharam.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace HeartPharam.Controllers.Factory
{
    public class ConcreteJSON : Exporter
    {
        public List<PharmacyOrder> info;

        public ConcreteJSON(List<PharmacyOrder> info)
        {
            this.info = info;
        }

        public void ExportShowsToFile()
        {
            List<PharmacyOrder> po = new List<PharmacyOrder>();
            using (PharmOrdDBContext poDb = new PharmOrdDBContext())
            {
                po = poDb.PharmOrderList.ToList();
            }


            if (po.Count > 0)
            {
                string header = @"""ID"",""DrugName"",""PharmacistName"",""UnitCost"",""Quantity"",""TotalCost""";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(header);


                foreach (var i in po)
                {
                    sb.AppendLine(string.Join(",",
                        string.Format(@"""{0}""", i.ID),
                        string.Format(@"""{0}""", i.DrugName),
                        string.Format(@"""{0}""", i.PharmacistName),
                        string.Format(@"""{0}""", i.UnitCost),
                        string.Format(@"""{0}""", i.Quantity),
                        string.Format(@"""{0}""", i.TotalCost)));
                }

                HttpContext context = HttpContext.Current;
                string json = JsonConvert.SerializeObject(po, Formatting.Indented);
                File.WriteAllText(@"c:\Users\Cristina\pharmOrd.json", json);

                context.Response.Write(sb.ToString());
                context.Response.ContentType = "application/json";
                context.Response.AddHeader("Content-Disposition", "attachment;filename=pharmOrd.json");
                context.Response.End();

            }

        }
    }
}