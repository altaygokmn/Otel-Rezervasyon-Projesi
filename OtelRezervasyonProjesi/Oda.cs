using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Oda
    {
        public int OdaID { get; set; }
        public string OdaTuru { get; set; }
        public bool OdaDurumu { get; set; }
        public bool EngelliDostu { get; set; }
        public bool OdaTemizlikDurumu { get; set; }
    }
}
