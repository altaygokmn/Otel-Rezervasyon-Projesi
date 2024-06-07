using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelRezervasyonProjesi
{
    public class Fatura
    {
        public int FaturaID { get; set; }
        public DateTime FaturaTarihi { get; set; }
        public int MusteriId { get; set; }

    }
}
