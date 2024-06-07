using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public partial class TemizlikHizmetiForm : Form
    {
        private OdaDal _odaDal;
        public TemizlikHizmetiForm()
        {
            InitializeComponent();
            _odaDal = new OdaDal();
        }


        
        private void TemizlikHizmetiForm_Load(object sender, EventArgs e)
        {
            _odaDal.FillDgwTemizlikDurumu(dgwTemizlikDurumu);
        }

        private void dgwTemizlikDurumu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgwTemizlikDurumu.Columns["OdaTemizlikDurumu"].Index)
            {
                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "Temiz";
                }
                else if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "Temiz Değil";
                }
            }
            else if (e.ColumnIndex == dgwTemizlikDurumu.Columns["OdaDurumu"].Index)
            {
                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "Dolu";
                }
                else if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "Boş";
                }
            }
        }

        private void btnTemizlikHizmeti_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tbxOdaNumarasi.Text, out int odaID))
            {
                _odaDal.OdaTemizlikIslemi(odaID, true);

                _odaDal.FillDgwTemizlikDurumu(dgwTemizlikDurumu);
                MessageBox.Show( "Temizlik işlemi tamamlandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Geçerli bir Oda ID giriniz.");
            }
        }
    }
}
