using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Musteri
    {
        public int MusteriId { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriTelefon { get; set; }
        public string MusteriEposta { get; set; }
        public bool KonaklamaDurumu { get; set; }
        public int OdaID { get; set; }

    }
}
