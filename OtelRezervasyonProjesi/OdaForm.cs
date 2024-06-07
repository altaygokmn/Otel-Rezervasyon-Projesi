using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public partial class OdaForm : Form
    {
        private OdaDal _OdaDal;
        private MusteriDal _MusteriDal;
        public int musteriID;
        public OdaForm()
        {
            InitializeComponent();
            _OdaDal = new OdaDal();
            
            _MusteriDal = new MusteriDal();

        }
        
        

        public int odaNumarasi;
        private void OdaForm_Load(object sender, EventArgs e)
        {
            lblOdaNumarasi.Text = odaNumarasi.ToString();
            _OdaDal.GetOdaBilgileri(odaNumarasi, this);
            _MusteriDal.OdadakiMusteriGoster(odaNumarasi, lblMisafirBilgileri);
            


        }

        private void btnTemizlikTalebi_Click(object sender, EventArgs e)
        {
            
            _OdaDal.UpdateOdaTemizlikDurumu(odaNumarasi);
            
        }

        private void btnOdaBosalt_Click(object sender, EventArgs e)
        {
           

        }

    }
}
