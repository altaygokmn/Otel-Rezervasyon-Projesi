using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Rezervasyon
    {
        public int RezervasyonID { get; set; }
        public int KisiSayisi { get; set; }
        public string OdaTipi { get; set; }
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }
        public DateTime RezervasyonYapilisTarihi { get; set; }
        public int OdaID { get; set; }
        public int MusteriId { get; set; }

    }
}
