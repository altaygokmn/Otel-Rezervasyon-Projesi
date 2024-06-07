using OfficeOpenXml;
using OtelRezervasyonProjesi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OtelRezervasyonProjesi
{
        public  class PersonelDal : DbManager 
    {
        DbManager _DbManager = new DbManager();
        


        public List<Personel> GetAllPersonel()
        {
            CreateConnection();

            SqlCommand command = new SqlCommand("spGetAllPersonel", _connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();

            List<Personel> PersonelDataTable = new List<Personel>();

            while (reader.Read())
            {
                Personel personel = new Personel
                {
                    PersonelID = Convert.ToInt32(reader["PersonelID"]),
                    PersonelAd = reader["PersonelAd"].ToString(),
                    PersonelSoyad = reader["PersonelSoyad"].ToString(),
                    PersonelCinsiyet = Convert.ToBoolean(reader["PersonelCinsiyet"]),
                    PersonelGorevi = reader["PersonelGorevi"].ToString(),
                    PersonelMaas = Convert.ToDecimal(reader["PersonelMaas"]),
                    PersonelEposta = reader["PersonelEposta"].ToString(),
                    PersonelTelefon = reader["PersonelTelefon"].ToString(),
                };

                PersonelDataTable.Add(personel);
            }

            reader.Close();
            CloseConnection();

            return PersonelDataTable;
        }

        public void PersonelListesiniDoldur(DataGridView dgwPersonelListesi)
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM PersonelTable", _connection))
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                   
                    dgwPersonelListesi.DataSource = dataTable;

                    
                    dgwPersonelListesi.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel bilgileri alınırken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void VeritabaniYetkilendir(int personelID)
        {
            try
            {
                CreateConnection();
                SqlCommand cmd = new SqlCommand("SELECT VeritabaniYetkisi FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", personelID);

                int currentYetki = Convert.ToInt32(cmd.ExecuteScalar());

                if (currentYetki == 1)
                {
                    MessageBox.Show("Bu kişi zaten yetkilendirilmiş.");
                }
                else
                {
                    
                    cmd = new SqlCommand("UPDATE PersonelTable SET VeritabaniYetkisi = 1 WHERE PersonelID = @PersonelID", _connection);
                    cmd.Parameters.AddWithValue("@PersonelID", personelID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Veritabanı yetkilendirme başarıyla gerçekleştirildi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı yetkilendirme sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void VeritabaniYetkisiSil(int personelID)
        {
            try
            {
                CreateConnection();
                SqlCommand cmd = new SqlCommand("SELECT VeritabaniYetkisi FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", personelID);

                int currentYetki = Convert.ToInt32(cmd.ExecuteScalar());

                if (currentYetki == 0)
                {
                    MessageBox.Show("Bu kişinin zaten yetkisi yok.");
                }
                else
                {
                   
                    cmd = new SqlCommand("UPDATE PersonelTable SET VeritabaniYetkisi = 0 WHERE PersonelID = @PersonelID", _connection);
                    cmd.Parameters.AddWithValue("@PersonelID", personelID);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Veritabanı yetkisi başarıyla kaldırıldı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı yetkisi silme sırasında bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }


      
        public void AddPersonelWithSP(string personelAd, string personelSoyad, bool personelCinsiyet, string personelGorevi,
    decimal personelMaas, string personelEposta, string personelSifre, string personelTelefon, bool veritabaniYetkisi)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("spPersonelEkle", _connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PersonelAd", personelAd);
                cmd.Parameters.AddWithValue("@PersonelSoyad", personelSoyad);
                cmd.Parameters.AddWithValue("@PersonelCinsiyet", personelCinsiyet);
                cmd.Parameters.AddWithValue("@PersonelGorevi", personelGorevi);
                cmd.Parameters.AddWithValue("@PersonelMaas", personelMaas);
                cmd.Parameters.AddWithValue("@PersonelEposta", personelEposta);
                cmd.Parameters.AddWithValue("@PersonelSifre", personelSifre);
                cmd.Parameters.AddWithValue("@PersonelTelefon", personelTelefon);
                cmd.Parameters.AddWithValue("@VeritabaniYetkisi", veritabaniYetkisi);

                

                cmd.ExecuteNonQuery();

                

            }
            catch (Exception ex)
            {
                throw new Exception("Personel eklenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdatePersonel(Personel personel)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand(@"UPDATE PersonelTable SET 
                PersonelAd = @PersonelAd,
                PersonelSoyad = @PersonelSoyad,
                PersonelCinsiyet = @PersonelCinsiyet,
                PersonelGorevi = @PersonelGorevi,
                PersonelMaas = @PersonelMaas,
                PersonelEposta = @PersonelEposta,
                PersonelSifre = @PersonelSifre,
                PersonelTelefon = @PersonelTelefon,
                VeritabaniYetkisi = @VeritabaniYetkisi
                WHERE PersonelID = @PersonelID;", _connection);

                cmd.Parameters.AddWithValue("@PersonelAd", personel.PersonelAd);
                cmd.Parameters.AddWithValue("@PersonelSoyad", personel.PersonelSoyad);
                cmd.Parameters.AddWithValue("@PersonelCinsiyet", personel.PersonelCinsiyet);
                cmd.Parameters.AddWithValue("@PersonelGorevi", personel.PersonelGorevi);
                cmd.Parameters.AddWithValue("@PersonelMaas", personel.PersonelMaas);
                cmd.Parameters.AddWithValue("@PersonelEposta", personel.PersonelEposta);
                cmd.Parameters.AddWithValue("@PersonelSifre", personel.PersonelSifre);
                cmd.Parameters.AddWithValue("@PersonelTelefon", personel.PersonelTelefon);
                cmd.Parameters.AddWithValue("@VeritabaniYetkisi", personel.VeritabaniYetkisi);
                cmd.Parameters.AddWithValue("@PersonelID", personel.PersonelID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Personel güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void DeletePersonel(int personelID)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("DELETE FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", personelID);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Personel başarıyla silindi.");
                }
                else
                {
                    MessageBox.Show("Silinecek personel bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Personel silinirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool ValidateLogin(string kullaniciAdi, string sifre)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT PersonelID, PersonelSifre, PersonelGorevi FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", kullaniciAdi);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string storedSifre = reader["PersonelSifre"].ToString();
                    string personelGorevi = reader["PersonelGorevi"].ToString();

                    
                    if (storedSifre == sifre)
                    {
                        
                        return true;
                    }
                }

              
                return false;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Login validation failed: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public string GetPersonelGorevi(string kullaniciAdi)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT PersonelGorevi FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", kullaniciAdi);

                string personelGorevi = cmd.ExecuteScalar()?.ToString();

                return personelGorevi;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Failed to get personnel role: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool GetVeritabaniYetkisi(string kullaniciAdi)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT VeritabaniYetkisi FROM PersonelTable WHERE PersonelID = @PersonelID", _connection);
                cmd.Parameters.AddWithValue("@PersonelID", kullaniciAdi);

                bool veritabaniYetkisi = Convert.ToBoolean(cmd.ExecuteScalar());

                return veritabaniYetkisi;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Failed to get database authorization: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdatePersonelCinsiyetGrafigi(Chart personelCinsiyetGrafigi)
        {
            CreateConnection();

            try
            {
               
                SqlCommand countCommand = new SqlCommand("SELECT PersonelCinsiyet, COUNT(*) FROM PersonelTable GROUP BY PersonelCinsiyet", _connection);
                SqlDataReader reader = countCommand.ExecuteReader();

                
                personelCinsiyetGrafigi.Series.Clear();

              
                Series series = new Series("PersonelCinsiyet");
                while (reader.Read())
                {
                    bool cinsiyet = Convert.ToBoolean(reader["PersonelCinsiyet"]);
                    int count = Convert.ToInt32(reader[1]);

                   
                    string cinsiyetText = cinsiyet ? "Erkek" : "Kadın";

                    series.Points.AddXY(cinsiyetText, count);
                }

                
                series.ChartType = SeriesChartType.Pie;

               
                personelCinsiyetGrafigi.Series.Add(series);

                
                personelCinsiyetGrafigi.Titles.Clear();
                personelCinsiyetGrafigi.Titles.Add("Personel Cinsiyet Grafiği");

               
                personelCinsiyetGrafigi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel cinsiyet grafiği güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdatePersonelGorevGrafigi(Chart personelGorevGrafigi)
        {
            CreateConnection();

            try
            {
               
                SqlCommand countCommand = new SqlCommand("SELECT PersonelGorevi, COUNT(*) FROM PersonelTable GROUP BY PersonelGorevi", _connection);
                SqlDataReader reader = countCommand.ExecuteReader();

                
                personelGorevGrafigi.Series.Clear();

               
                Series series = new Series("PersonelGorev");
                while (reader.Read())
                {
                    string gorev = reader["PersonelGorevi"].ToString();
                    int count = Convert.ToInt32(reader[1]);

                    series.Points.AddXY(gorev, count);
                }

               
                series.ChartType = SeriesChartType.Pie;

                
                personelGorevGrafigi.Series.Add(series);

               
                personelGorevGrafigi.Titles.Clear();
                personelGorevGrafigi.Titles.Add("Personel Görev Grafiği");

                
                personelGorevGrafigi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel görev grafiği güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdatePersonelMaasGrafigi(Chart personelMaasGrafigi)
        {
            CreateConnection();

            try
            {
               
                SqlCommand countCommand = new SqlCommand("SELECT PersonelMaas, COUNT(*) FROM PersonelTable GROUP BY PersonelMaas", _connection);
                SqlDataReader reader = countCommand.ExecuteReader();

               
                personelMaasGrafigi.Series.Clear();

                
                Series series = new Series("PersonelMaas");
                while (reader.Read())

                {
                    decimal maas = Convert.ToDecimal(reader["PersonelMaas"]);
                    int count = Convert.ToInt32(reader[1]);
                    series.Points.AddXY(maas.ToString("C"), count);
                }

                
                series.ChartType = SeriesChartType.Column;

               
                personelMaasGrafigi.Series.Add(series);

                
                personelMaasGrafigi.Titles.Clear();
                personelMaasGrafigi.Titles.Add("Personel Maaş Grafiği");

              
                personelMaasGrafigi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel maaş grafiği güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
     

        public void PersonelListesiOku()
        {
            string dosyaYolu = "C:/Users/Acer/Desktop/Personeller.txt";

            try
            {
                CreateConnection();

               
                using (FileStream fileStream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string satir;
                    while ((satir = reader.ReadLine()) != null)
                    {
                       
                        string[] bilgiler = satir.Split(',');

                       
                        string bilgiDizisi = string.Join(", ", bilgiler);

                       
                        Personel personel = new Personel
                        {
                            PersonelAd = bilgiler[0],
                            PersonelSoyad = bilgiler[1],
                            PersonelCinsiyet = Convert.ToBoolean(bilgiler[2]),
                            PersonelGorevi = bilgiler[3],
                            PersonelMaas = Convert.ToDecimal(bilgiler[4]),
                            PersonelEposta = bilgiler[5],
                            PersonelSifre = bilgiler[6],
                            PersonelTelefon = bilgiler[7],
                            VeritabaniYetkisi = Convert.ToBoolean(bilgiler[8])
                        };

                       
                        AddPersonelWithSP(personel.PersonelAd,personel.PersonelSoyad,personel.PersonelCinsiyet,personel.PersonelGorevi,personel.PersonelMaas,personel.PersonelEposta, personel.PersonelSifre, personel.PersonelTelefon,personel.VeritabaniYetkisi);
                    }

                    MessageBox.Show("Personel bilgileri başarıyla okundu ve spPersonelEkle stored procedure'ü ile PersonelTable'a eklenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Personel bilgileri okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PersonelTablosunuYeniTxtOlustur()
        {
            try
            {
                
                string dosyaAdi = $"PersonelBilgileri_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

                using (StreamWriter sw = new StreamWriter(dosyaAdi))
                {
                   
                    sw.WriteLine("Personel ID\t--Ad\t--Soyad\t--Cinsiyet\t--Görev\t--Maaş\t--Eposta\t--Telefon       ");

                   
                    List<Personel> personelListesi = new PersonelDal().GetAllPersonel();

                    foreach (var personel in personelListesi)
                    {
                       
                        sw.WriteLine($"{personel.PersonelID}\t--{personel.PersonelAd}\t--{personel.PersonelSoyad}\t--{personel.PersonelCinsiyet}\t--{personel.PersonelGorevi}\t--{personel.PersonelMaas}\t--{personel.PersonelEposta}\t--{personel.PersonelTelefon}");
                    }

                    MessageBox.Show($"Personel bilgileri başarıyla {dosyaAdi} adlı txt dosyasına yazıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}





