using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public partial class VeritabaniYetkilisiYonetimForm : Form
    {
        public VeritabaniYetkilisiYonetimForm()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        OdaDal _odaDal = new OdaDal();
        PersonelDal _PersonelDal = new PersonelDal();
        RezervasyonDal _RezervasyonDal = new RezervasyonDal();
        private void VeritabaniYetkilisiYonetimForm_Load(object sender, EventArgs e)
        {
            _odaDal.OdalarListesiniDoldur(dgwOdalarListesi);
            _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
            _PersonelDal.PersonelListesiniDoldur(dgwPersonelIslemleri);
            _RezervasyonDal.RezervasyonListesiDoldur(dgwRezervasyonListesi);
        }

        private void VeritabanıYetkilisiOdalar_Click(object sender, EventArgs e)
        {

        }

        private void btnVeritabaniniGeriyeDondur_Click(object sender, EventArgs e)
        {

        }

        private void dgwOdalarListesi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgwOdalarListesi.Columns["OdaTemizlikDurumu"].Index)
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
            else if (e.ColumnIndex == dgwOdalarListesi.Columns["OdaDurumu"].Index)
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
            else if (e.ColumnIndex == dgwOdalarListesi.Columns["EngelliDostu"].Index)
            {
                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "Evet";
                }
                else if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "Hayır";
                }
            }
        }

        private void dgwPersonelListesi_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgwPersonelListesi.Columns["PersonelCinsiyet"].Index)
            {
                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "Erkek";
                }
                else if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "Kadın";
                }
            }
            else if (e.ColumnIndex == dgwPersonelListesi.Columns["VeritabaniYetkisi"].Index)
            {
                if (e.Value != null && e.Value.ToString() == "True")
                {
                    e.Value = "Var";
                }
                else if (e.Value != null && e.Value.ToString() == "False")
                {
                    e.Value = "Yok";
                }
            }

        }

        private void btnVeritabaniYetkilendir_Click(object sender, EventArgs e)
        {
            try
            {
                int personelID = Convert.ToInt32(tbxVeritabaniYetkilendir.Text);
                PersonelDal personelDal = new PersonelDal();
                personelDal.VeritabaniYetkilendir(personelID);
                _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnVeritabaniYetkisiSil_Click(object sender, EventArgs e)
        {

            try
            {
                int personelID = Convert.ToInt32(tbxVeritabaniYetkilendir.Text);
                PersonelDal personelDal = new PersonelDal();
                personelDal.VeritabaniYetkisiSil(personelID);
                _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }
        private void ClearPersonelInputs()
        {
            tbxPersonelNumarasiEkle.Text = "";
            tbxPersonelAd.Text = "";
            tbxPersonelSoyad.Text = "";
            cbxPersonelCinsiyet.SelectedIndex = -1; 
            tbxPersonelGorev.Text = "";
            tbxPersonelMaas.Text = "";
            tbxPersonelEposta.Text = "";
            mtbxPersonelTelefon.Text = "";
        }
        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            try
            {
                PersonelDal personelDal = new PersonelDal();

                Personel newPersonel = new Personel
                {
                    PersonelAd = tbxPersonelAd.Text,
                    PersonelSoyad = tbxPersonelSoyad.Text,
                    PersonelCinsiyet = cbxPersonelCinsiyet.SelectedIndex == 0, // 0 = Kadın, 1 = Erkek
                    PersonelGorevi = tbxPersonelGorev.Text,
                    PersonelMaas = Convert.ToDecimal(tbxPersonelMaas.Text),
                    PersonelEposta = tbxPersonelEposta.Text,
                    PersonelTelefon = mtbxPersonelTelefon.Text,
                    PersonelSifre = tbxPersonelSifre.Text, 
                    VeritabaniYetkisi = cbxPersonelVeritabaniYetkisi.SelectedIndex == 0 // 0 = Yok, 1 = Var
                };

                personelDal.AddPersonelWithSP(newPersonel.PersonelAd, newPersonel.PersonelSoyad, newPersonel.PersonelCinsiyet, newPersonel.PersonelGorevi, newPersonel.PersonelMaas, newPersonel.PersonelEposta, newPersonel.PersonelTelefon, newPersonel.PersonelSifre, newPersonel.VeritabaniYetkisi);

                _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
                _PersonelDal.PersonelListesiniDoldur(dgwPersonelIslemleri);

                ClearPersonelInputs();
                MessageBox.Show("Personel ekleme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbManager.CloseConnection();
            }

        }

        private void btnPersonelGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                PersonelDal personelDal = new PersonelDal();

                Personel updatedPersonel = new Personel
                {
                    PersonelID = Convert.ToInt32(tbxPersonelNumarasiEkle.Text),
                    PersonelAd = tbxPersonelAd.Text,
                    PersonelSoyad = tbxPersonelSoyad.Text,
                    PersonelCinsiyet = cbxPersonelCinsiyet.SelectedIndex == 0,
                    PersonelGorevi = tbxPersonelGorev.Text,
                    PersonelMaas = Convert.ToDecimal(tbxPersonelMaas.Text),
                    PersonelEposta = tbxPersonelEposta.Text,
                    PersonelTelefon = tbxPersonelSifre.Text,
                    PersonelSifre = mtbxPersonelTelefon.Text,
                    VeritabaniYetkisi = cbxPersonelVeritabaniYetkisi.SelectedIndex == 0
                };

                personelDal.UpdatePersonel(updatedPersonel);

                _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
                _PersonelDal.PersonelListesiniDoldur(dgwPersonelIslemleri);

                ClearPersonelInputs();
                MessageBox.Show("Personel güncelleme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void dgwPersonelIslemleri_SelectionChanged(object sender, EventArgs e)
        {
            if (dgwPersonelIslemleri.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgwPersonelIslemleri.SelectedRows[0];

                tbxPersonelNumarasiEkle.Text = selectedRow.Cells["PersonelID"].Value.ToString();
                tbxPersonelAd.Text = selectedRow.Cells["PersonelAd2"].Value.ToString();
                tbxPersonelSoyad.Text = selectedRow.Cells["PersonelSoyad2"].Value.ToString();
                cbxPersonelCinsiyet.SelectedIndex = (bool)selectedRow.Cells["PersonelCinsiyet2"].Value ? 0 : 1;
                tbxPersonelGorev.Text = selectedRow.Cells["PersonelGorevi2"].Value.ToString();
                tbxPersonelMaas.Text = selectedRow.Cells["PersonelMaas2"].Value.ToString();
                tbxPersonelEposta.Text = selectedRow.Cells["PersonelEposta2"].Value.ToString();
                tbxPersonelSifre.Text = selectedRow.Cells["PersonelTelefon2"].Value.ToString();
                mtbxPersonelTelefon.Text = selectedRow.Cells["PersonelSifre2"].Value.ToString();
                cbxPersonelVeritabaniYetkisi.SelectedIndex = (bool)selectedRow.Cells["VeritabaniYetkisi2"].Value ? 1 : 0;
            }
        }

        private void btnPersonelSil_Click(object sender, EventArgs e)
        {
            try
            {
                int personelIDToDelete = Convert.ToInt32(tbxPersonelNumarasiSil.Text);

                DialogResult result = MessageBox.Show("Seçili personeli silmek istediğinizden emin misiniz?", "Personel Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _PersonelDal.DeletePersonel(personelIDToDelete);

                    _PersonelDal.PersonelListesiniDoldur(dgwPersonelListesi);
                    _PersonelDal.PersonelListesiniDoldur(dgwPersonelIslemleri);

                    tbxPersonelNumarasiSil.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnResepsiyonPanelineGit_Click(object sender, EventArgs e)
        {
            this.Hide();
            YonetimPaneliForm yonetimPaneliForm = new YonetimPaneliForm();
            this.Hide();
            yonetimPaneliForm.Show();
        }
        DbManager dbManager = new DbManager();
        private void btnVeritabaniYedegiAl_Click(object sender, EventArgs e)
        {
            dbManager.BackupDatabase();
        }

        private void btnDosyaKonumuSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Browse Text Files";

            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            openFileDialog.DefaultExt = "BAK";
            openFileDialog.Filter = "Text files (*.bak) | *.bak";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            openFileDialog.ReadOnlyChecked = true;
            openFileDialog.ShowReadOnly = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbxDosyaKonumu.Text = openFileDialog.FileName;

            }
        }

        private void btnYedektenDon_Click(object sender, EventArgs e)
        {
            string servername = tbxServerYedektenDon.Text;
            string dbname = tbxDatabaseYedektenDon.Text;

            string connectionstr = @"Data Source=" + servername + ";Initial Catalog=master;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionstr);

            connection.Open();

            string str1 = "USE master;";
            string str2 = "ALTER DATABASE " + dbname + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE;";
            string str3 = "RESTORE DATABASE " + dbname + " FROM DISK = '" + tbxDosyaKonumu.Text + "' WITH REPLACE;";
            SqlCommand cmd1 = new SqlCommand(str1, connection);
            SqlCommand cmd2 = new SqlCommand(str2, connection);
            SqlCommand cmd3 = new SqlCommand(str3, connection);
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            MessageBox.Show("Veritabanınızı yedekten döndürme işlemi başarıyla gerçekleşti.");

            connection.Close();
            Application.Exit();
            


        }
    }
}


