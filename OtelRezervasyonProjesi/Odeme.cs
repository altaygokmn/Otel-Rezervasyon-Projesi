using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Odeme
    {
        public int OdemeId { get; set; }
        public decimal Ucret { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public bool OdemeDurumu { get; set; }
        public int MusteriId { get; set; }
    }
}
