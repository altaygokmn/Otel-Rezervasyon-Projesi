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
    public partial class RezervasyonForm : Form
    {
        private MusteriDal musteriDal = new MusteriDal();
        private OdemeDal odemeDal = new OdemeDal();
        private RezervasyonDal rezervasyonDal = new RezervasyonDal();
        private DbManager dbManager = new DbManager();
        
        private OdaDal odaDal = new OdaDal();

        public RezervasyonForm()
        {
            InitializeComponent();
            dbManager.CreateConnection();
        }

        private void btnOYS_Click(object sender, EventArgs e)
        {
            this.Hide();
            YonetimPaneliGirisForm yonetimPaneliGirisForm = new YonetimPaneliGirisForm();
            yonetimPaneliGirisForm.Show();
        }

        private void btnRezervasyonYap_Click(object sender, EventArgs e)
        {
            tabControlDuzceOtel.SelectedTab = tbpMusteriBilgileri;
        }

        private void RezervasyonForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
        
        private void btnMisafirBilgileri_Click(object sender, EventArgs e)
        {
            tabControlDuzceOtel.SelectedTab = tbpRezervasyonBilgileri;
        }

        private void btnRezervasyonuTamamla_Click(object sender, EventArgs e)
        {
            try
            {
           
                int musteriID = musteriDal.AddMusteriAndGetID(new Musteri
                {
                    MusteriAd = tbxMusteriAd.Text,
                    MusteriSoyad = tbxMusteriSoyad.Text,
                    MusteriTelefon = tbxMusteriTelefon.Text,
                    MusteriEposta = tbxMusteriEposta.Text,
                    OdaID = Convert.ToInt32(cbxOdaNumarasi.SelectedItem)
                });

                if (musteriID != -1)
                {
                    
                    Rezervasyon rezervasyon = new Rezervasyon
                    {
                        KisiSayisi = Convert.ToInt32(cbxYetiskinSayisi.SelectedItem) + Convert.ToInt32(cbxCocukSayisi.SelectedItem),
                        OdaTipi = cbxOdaTipi.SelectedItem.ToString(),
                        GirisTarihi = dtpGirisTarihi.Value,
                        CikisTarihi = dtpCikisTarihi.Value,
                        RezervasyonYapilisTarihi = DateTime.Now,
                        OdaID = Convert.ToInt32(cbxOdaNumarasi.SelectedItem),
                        MusteriId = musteriID
                    };

                   
                    AddRezervasyon(rezervasyon);

                    double ucret = odemeDal.GetOdemeUcretByMusteriID(musteriID);
                    lblTutar.Text = $" {ucret:C2}";
                }
                else
                {
                    MessageBox.Show("Müşteri eklenirken bir hata oluştu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void cbxOdaTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOdaTipi = cbxOdaTipi.SelectedItem.ToString();

           
            List<int> uygunOdaIDList = GetUygunOdaIDList(selectedOdaTipi);

            
            cbxOdaNumarasi.Items.Clear();

            foreach (int odaID in uygunOdaIDList)
            {
                cbxOdaNumarasi.Items.Add(odaID);
            }

            if (cbxOdaNumarasi.Items.Count > 0)
            {
                cbxOdaNumarasi.SelectedIndex = 0;
            }
        }
        public List<int> GetUygunOdaIDList(string odaTuru)
        {
            string query = "SELECT OdaID FROM OdaTable WHERE OdaTuru = @OdaTuru AND OdaDurumu = 0";

            List<int> uygunOdaIDList = new List<int>();

            using (SqlCommand cmd = new SqlCommand(query, dbManager._connection))
            {
                cmd.Parameters.AddWithValue("@OdaTuru", odaTuru);

                try
                {
                    dbManager.CreateConnection();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int odaID = Convert.ToInt32(reader["OdaID"]);
                        uygunOdaIDList.Add(odaID);
                    }
                    dbManager.CloseConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Uygun odalar getirilirken bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    dbManager.CloseConnection();
                }
            }

            return uygunOdaIDList;
        }
        public void AddRezervasyon(Rezervasyon rezervasyon)
        {
            try
            {
                dbManager.CreateConnection();

                int musteriID = GetMusteriID();

                if (musteriID == -1)
                {
                    MessageBox.Show("Müşteri bulunamadı.");
                    return;
                }

                int selectedOdaID = Convert.ToInt32(cbxOdaNumarasi.SelectedItem);

                if (selectedOdaID != rezervasyon.OdaID)
                {
                    MessageBox.Show("Müşterinin seçtiği oda ile rezervasyondaki oda uyuşmuyor.");
                    return;
                }

                if (GetMusteriTelefonByID(musteriID) != tbxMusteriTelefon.Text)
                {
                    MessageBox.Show("Müşteri telefonu uyuşmuyor.");
                    return;
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO RezervasyonTable (KisiSayisi, OdaTipi, GirisTarihi, CikisTarihi, RezervasyonYapilisTarihi, OdaID, MusteriID) VALUES (@KisiSayisi, @OdaTipi, @GirisTarihi, @CikisTarihi, @RezervasyonYapilisTarihi, @OdaID, @MusteriID);", dbManager._connection);
                cmd.Parameters.AddWithValue("@KisiSayisi", rezervasyon.KisiSayisi);
                cmd.Parameters.AddWithValue("@OdaTipi", rezervasyon.OdaTipi);
                cmd.Parameters.AddWithValue("@GirisTarihi", rezervasyon.GirisTarihi);
                cmd.Parameters.AddWithValue("@CikisTarihi", rezervasyon.CikisTarihi);
                cmd.Parameters.AddWithValue("@RezervasyonYapilisTarihi", rezervasyon.RezervasyonYapilisTarihi);
                cmd.Parameters.AddWithValue("@OdaID", selectedOdaID);
                cmd.Parameters.AddWithValue("@MusteriID", musteriID);
                dbManager.CreateConnection();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rezervasyonunuz oluşturuldu ve ödemeniz alındı. \nBizi tercih ettiğiniz için teşekkür ederiz!");
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }


       

        public int GetMusteriID()
        {
            int musteriID = -1;

            try
            {
                dbManager.CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT MusteriID FROM MusteriTable WHERE OdaID = @OdaID AND MusteriTelefon = @MusteriTelefon", dbManager._connection);
                cmd.Parameters.AddWithValue("@OdaID", Convert.ToInt32(cbxOdaNumarasi.SelectedItem));
                cmd.Parameters.AddWithValue("@MusteriTelefon", tbxMusteriTelefon.Text);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    musteriID = Convert.ToInt32(result);
                }
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return musteriID;
        }

        public string GetMusteriTelefonByID(int musteriID)
        {
            string musteriTelefon = null;

            try
            {
                dbManager.CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT MusteriTelefon FROM MusteriTable WHERE MusteriID = @MusteriID", dbManager._connection);
                cmd.Parameters.AddWithValue("@MusteriID", musteriID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    musteriTelefon = result.ToString();
                }
            }
            finally
            {
                dbManager.CloseConnection();
            }

            return musteriTelefon;
        }

        private List<int> GetMusaitOdaNumaralari(DateTime girisTarihi, DateTime cikisTarihi, string odaTipi)
        {
            try
            {
                dbManager.CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT OdaID FROM OdaTable WHERE OdaDurumu = 0 AND OdaTuru = @OdaTuru AND OdaID NOT IN (SELECT OdaID FROM RezervasyonTable WHERE (@GirisTarihi BETWEEN GirisTarihi AND CikisTarihi OR @CikisTarihi BETWEEN GirisTarihi AND CikisTarihi) OR (GirisTarihi BETWEEN @GirisTarihi AND @CikisTarihi) OR (CikisTarihi BETWEEN @GirisTarihi AND @CikisTarihi));", dbManager._connection);

                cmd.Parameters.AddWithValue("@OdaTuru", odaTipi);
                cmd.Parameters.AddWithValue("@GirisTarihi", girisTarihi);
                cmd.Parameters.AddWithValue("@CikisTarihi", cikisTarihi);

                SqlDataReader reader = cmd.ExecuteReader();

                List<int> musaitOdaNumaralari = new List<int>();

                while (reader.Read())
                {
                    int odaID = Convert.ToInt32(reader["OdaID"]);
                    musaitOdaNumaralari.Add(odaID);
                }

                return musaitOdaNumaralari;
            }
            finally
            {
                dbManager.CloseConnection();
            }
        }
    }


}




