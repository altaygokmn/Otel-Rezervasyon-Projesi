using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Personel
    {
        public int PersonelID { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set;}
        public bool PersonelCinsiyet { get; set; }
        public string PersonelGorevi { get; set; }
        public decimal PersonelMaas { get; set; }
        public string PersonelEposta { get; set; }
        public string PersonelSifre { get; set; }
        public string PersonelTelefon { get; set; }
        public bool VeritabaniYetkisi { get; set; }

        
    }
    
}
