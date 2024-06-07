using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public partial class YonetimPaneliGirisForm : Form
    {
        PersonelDal _personelDal = new PersonelDal();
        public YonetimPaneliGirisForm()
        {
            InitializeComponent();
        }
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;Initial Catalog=Dbo_OtelRezervasyon;Integrated Security=True";
        private void YonetimPaneliGirisForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
      

        private void btnAdminGiris_Click(object sender, EventArgs e)
        {
            
            try
            {
                string kullaniciAdi = tbxKullaniciAdi.Text;
                string sifre = tbxSifre.Text;

                // Validate the login
                if (_personelDal.ValidateLogin(kullaniciAdi, sifre))
                {
                    string personelGorevi = _personelDal.GetPersonelGorevi(kullaniciAdi);

                    bool veritabaniYetkisi = _personelDal.GetVeritabaniYetkisi(kullaniciAdi);

                    if (veritabaniYetkisi)
                    {
                        VeritabaniYetkilisiYonetimForm veritabaniYetkilisiYonetimForm = new VeritabaniYetkilisiYonetimForm();
                        this.Hide();
                        veritabaniYetkilisiYonetimForm.Show();
                    }
                    else
                    {
                        switch (personelGorevi)
                        {
                            case "Resepsiyonist":
                                YonetimPaneliForm yonetimPaneliForm = new YonetimPaneliForm();
                                this.Hide();
                                yonetimPaneliForm.Show();
                                break;

                            case "Admin":
                                VeritabaniYetkilisiYonetimForm veritabaniYetkilisiYonetimForm = new VeritabaniYetkilisiYonetimForm();
                                this.Hide();
                                veritabaniYetkilisiYonetimForm.Show();
                                break;
                                

                            case "Temizlikçi":
                                TemizlikHizmetiForm temizlikHizmetiForm = new TemizlikHizmetiForm();
                                this.Hide();
                                temizlikHizmetiForm.Show();
                                break;

                            default:
                                MessageBox.Show("Giriş yapmak için yetkiniz yok.");
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


    }
}
