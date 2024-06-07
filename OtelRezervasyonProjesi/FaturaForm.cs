using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public partial class FaturaForm : Form
    {
        public FaturaForm()
        {
            InitializeComponent();
        }
        PdfManager pdfManager = new PdfManager();
        private void btnFaturaIndır_Click(object sender, EventArgs e)
        {
            int musteriId = Convert.ToInt32(tbxMusteriNumarasi.Text);
            pdfManager.OdemeBilgileriniIndir(musteriId);
        }

        private void FaturaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
