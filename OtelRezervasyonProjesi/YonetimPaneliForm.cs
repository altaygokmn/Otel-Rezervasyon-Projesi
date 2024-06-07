using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Windows.Forms.DataVisualization.Charting;
namespace OtelRezervasyonProjesi
{
    public partial class YonetimPaneliForm : Form
    {
        public YonetimPaneliForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            ButonlaraOlayEkle(this);
            


              }
              private void ButonlaraOlayEkle(Control control)
              {
                  foreach (Control childControl in control.Controls)
                  {
                      if (childControl is Button)
                      {
                          Button button = (Button)childControl;
                          button.Click += Button_Click; 
                      }

                      if (childControl.Controls.Count > 0)
                      {
                          ButonlaraOlayEkle(childControl);
                      }
                  }
              }
        MusteriDal _MusteriDal = new MusteriDal();
        RezervasyonDal _RezervasyonDal = new RezervasyonDal();
        PersonelDal _PersonelDal = new PersonelDal();
        OdaDal _OdaDal = new OdaDal();
        
        private void YonetimPaneliForm_Load(object sender, EventArgs e)
            {
                
            List<Rezervasyon> rezervasyonListesi = _RezervasyonDal.GetAllRezervasyon();
            List<Personel> personelListesi = _PersonelDal.GetAllPersonel();
            List<Musteri> misafirListesi = _MusteriDal.GetAllMusteri();
            dgwRezervasyonlar.DataSource = rezervasyonListesi;
            dgwPersoneller.DataSource = personelListesi;
            dgwMisafirler.DataSource = misafirListesi;
            _MusteriDal.UpdateDgwOteldeBulunanMisafirler(dgwOteldeBulunanMisafirler);
            _MusteriDal.GetBugunGelecekMisafirler(dgwBugunGelecekMisafirler);
            // Listeyi DataGridView'e veri kaynağı olarak ata
            dgwRezervasyonlar.Refresh();
            _OdaDal.GetOdaDurumuStatistics(chrtOdaDoluluk);
            _MusteriDal.UpdateKonaklamaDurumu();
            _MusteriDal.UpdateOtelDolulukGrafigi2(OtelDolulukGrafigi2);
            _PersonelDal.UpdatePersonelCinsiyetGrafigi(personelCinsiyetGrafigi);
            _PersonelDal.UpdatePersonelGorevGrafigi(PersonelGorevGrafigi);
            _PersonelDal.UpdatePersonelMaasGrafigi(personelMaasGrafigi);
            string selectedOption = (cbxRezervasyonTarihi.SelectedItem != null) ? cbxRezervasyonTarihi.SelectedItem.ToString() : "Tüm Zamanlar";
            _RezervasyonDal.UpdateRezervasyonSayisiGrafigi(RezervasyonSayisiGrafigi, selectedOption);
            _MusteriDal.UpdateOdaDurumuForKonaklamaDurumuFalse();


        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (tabControlYonetimPaneli.SelectedTab == Odalar)
            {
                OdaForm odaForm = new OdaForm();
                Button clickedButton = (Button)sender;
                odaForm.odaNumarasi = Convert.ToInt32(clickedButton.Text);
                odaForm.Show();
              

            }
        }

            private void btnAktifRezervasyonlar_Click(object sender, EventArgs e)
            {
                
            List<Rezervasyon> rezervasyonListesi = _RezervasyonDal.GetAktifRezervasyonlar();

            dgwRezervasyonlar.DataSource = rezervasyonListesi;

            dgwRezervasyonlar.Refresh();

        }

            private void btnIptalEdilenRezervasyonlar_Click(object sender, EventArgs e)
            {
            List<Rezervasyon> iptalEdilenRezervasyonlar = new List<Rezervasyon>();

            foreach (DataGridViewRow row in dgwRezervasyonlar.Rows)
            {
                if (!row.Visible)
                {
                    int rezervasyonID = Convert.ToInt32(row.Cells["RezervasyonID"].Value);
                    Rezervasyon rezervasyon = _RezervasyonDal.GetRezervasyonByID(rezervasyonID);
                    iptalEdilenRezervasyonlar.Add(rezervasyon);
                }
            }

            dgwRezervasyonlar.DataSource = iptalEdilenRezervasyonlar;

            dgwRezervasyonlar.Refresh();

        }

            private void btnGecmisRezervasyonlar_Click(object sender, EventArgs e)
            {
                
            List<Rezervasyon> rezervasyonListesi = _RezervasyonDal.GetGecmisRezervasyonlar();

            dgwRezervasyonlar.DataSource = rezervasyonListesi;

            dgwRezervasyonlar.Refresh();
        }

            private void btnGelecekRezervasyonlar_Click(object sender, EventArgs e)
            {
                
               
            List<Rezervasyon> rezervasyonListesi = _RezervasyonDal.GetGelecekRezervasyonlar();

            dgwRezervasyonlar.DataSource = rezervasyonListesi;

            dgwRezervasyonlar.Refresh();
        }


        private void btnRezervasyonListesiIndir_Click(object sender, EventArgs e)
        {
            RezervasyonDal rezervasyonDal = new RezervasyonDal();
            List<Rezervasyon> rezervasyonListesi = rezervasyonDal.GetAllRezervasyon();
            PdfManager pdfManager = new PdfManager();
            pdfManager.RezervasyonListesiIndir(rezervasyonListesi);
            
        }

        private void btnMisafirSil_Click(object sender, EventArgs e)
        {
            if (dgwMisafirler.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir misafir seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            DataGridViewCell selectedCell = dgwMisafirler.SelectedCells[0];
            int selectedRowIndex = selectedCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgwMisafirler.Rows.Count)
            {
                int musteriId = Convert.ToInt32(dgwMisafirler.Rows[selectedRowIndex].Cells["MusteriId3"].Value);

                _MusteriDal.MisafirSil(musteriId);

                List<Musteri> musteriListesi = _MusteriDal.GetAllMusteri();
                dgwMisafirler.DataSource = musteriListesi;
                dgwMisafirler.Refresh();
            }
            else
            {
                MessageBox.Show("Geçersiz seçim. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnFaturaOlustur_Click(object sender, EventArgs e)
        {
            FaturaForm faturaForm = new FaturaForm();
            faturaForm.Show();

        }

       

        private void btnRezervasyonReddet_Click(object sender, EventArgs e)
        {

            if (dgwRezervasyonlar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir rezervasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int selectedRowIndex = dgwRezervasyonlar.SelectedRows[0].Index;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgwRezervasyonlar.Rows.Count)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dgwRezervasyonlar.Rows[selectedRowIndex];

                selectedRow.DefaultCellStyle.BackColor = Color.Red; 

                selectedRow.Selected = false;

                dgwRezervasyonlar.Refresh();
            }
            else
            {
                MessageBox.Show("Geçersiz seçim. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnRezervasyonuSil_Click(object sender, EventArgs e)
        {
            if (dgwRezervasyonlar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir rezervasyon seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            DataGridViewCell selectedCell = dgwRezervasyonlar.SelectedCells[0];
            int selectedRowIndex = selectedCell.RowIndex;

            if (selectedRowIndex >= 0 && selectedRowIndex < dgwRezervasyonlar.Rows.Count)
            {
                int rezervasyonID = Convert.ToInt32(dgwRezervasyonlar.Rows[selectedRowIndex].Cells["RezervasyonID"].Value);

                // Rezervasyonu sil
                _RezervasyonDal.RezervasyonSil(rezervasyonID);

                List<Rezervasyon> rezervasyonListesi = _RezervasyonDal.GetAllRezervasyon();
                dgwRezervasyonlar.DataSource = rezervasyonListesi;
                dgwRezervasyonlar.Refresh();
            }
            else
            {
                MessageBox.Show("Geçersiz seçim. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        private void cbxKonaklamaDurumu_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool konaklamaDurumu = cbxKonaklamaDurumu.SelectedIndex == 0;


            // DataGridView'i güncelle
            if (konaklamaDurumu)
            {
                _MusteriDal.UpdateDgwOteldeBulunanMisafirler(dgwMisafirler);
            }
            else
            {
                List<Musteri> konaklamayanMisafirler = _MusteriDal.GetAllMusteri().Where(m => m.KonaklamaDurumu == false).ToList();
                dgwMisafirler.DataSource = konaklamayanMisafirler;
            }
        }

        private void tbxMisafirArama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string aramaMetni = tbxMisafirArama.Text;

                MusteriDal _MusteriDal = new MusteriDal();

                _MusteriDal.SearchAndFillDgwMisafirler(dgwMisafirler, aramaMetni);

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnMisafirAra_Click(object sender, EventArgs e)
        {
            string aramaMetni = tbxMisafirArama.Text;

            MusteriDal _MusteriDal = new MusteriDal();

            _MusteriDal.SearchAndFillDgwMisafirler(dgwMisafirler, aramaMetni);
        }

        private void btnRezervasyonAra_Click(object sender, EventArgs e)
        {
            int rezervasyonId;
            if (int.TryParse(tbxRezervasyonArama.Text, out rezervasyonId))
            {
                RezervasyonDal _RezervasyonDal = new RezervasyonDal();

                _RezervasyonDal.SearchAndFillDgwRezervasyonlar(dgwRezervasyonlar, rezervasyonId);
            }
            else
            {
                MessageBox.Show("Geçerli bir Rezervasyon ID giriniz.");
            }
        }

        private void tbxRezervasyonArama_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                int rezervasyonId;
                if (int.TryParse(tbxRezervasyonArama.Text, out rezervasyonId))
                {
                    RezervasyonDal _RezervasyonDal = new RezervasyonDal();

                    _RezervasyonDal.SearchAndFillDgwRezervasyonlar(dgwRezervasyonlar, rezervasyonId);
                }
                else
                {
                    MessageBox.Show("Geçerli bir Rezervasyon ID giriniz.");
                }
            }
        }

        private void btnMisafirListesiIndir_Click(object sender, EventArgs e)
        {
            try
            {
                MusteriDal musteriDal = new MusteriDal();
                List<Musteri> misafirListesi = musteriDal.GetAllMusteri();
                
                PdfManager pdfManager = new PdfManager();
                pdfManager.MusteriListesiIndir(misafirListesi);  
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnPersonelListesiIndir_Click(object sender, EventArgs e)
        {
            PersonelDal personelDal = new PersonelDal();
            List<Personel> personelListesi = personelDal.GetAllPersonel();
            PdfManager pdfManager = new PdfManager();
            pdfManager.PersonelListesiIndir(personelListesi);
        }

        private void cbxRezervasyonTarihi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = (cbxRezervasyonTarihi.SelectedItem != null) ? cbxRezervasyonTarihi.SelectedItem.ToString() : "Tüm Zamanlar";
            _RezervasyonDal.UpdateRezervasyonSayisiGrafigi(RezervasyonSayisiGrafigi, selectedOption);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            _PersonelDal.PersonelListesiOku();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            _PersonelDal.PersonelTablosunuYeniTxtOlustur();
        }
    }
}
