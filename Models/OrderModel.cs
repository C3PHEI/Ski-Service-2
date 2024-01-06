using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski_Service_2.Orders
{
    public class OrderModel
    {
        public int BestellungsID { get; set; }
        public string Prioritaet { get; set; }
        public string Dienstleistung { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public DateTime DatumEinreichung { get; set; }
        public decimal Preis { get; set; }
        public string Status { get; set; }
        public string Mitarbeiter { get; set; }
    }
}
