using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PdfSharp.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


namespace OtelRezervasyonProjesi
{
    
    public class MusteriDal : DbManager
    {
        DbManager _DbManager = new DbManager();
        public List<Musteri> GetAllMusteri()
        {
            CreateConnection();

            SqlCommand command = new SqlCommand("spGetAllMusteriler", _connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();

            List<Musteri> MusteriDataTable = new List<Musteri>();

            while (reader.Read())
            {
                Musteri musteri = new Musteri
                {
                    MusteriId = Convert.ToInt32(reader["MusteriId"]),
                    MusteriAd = reader["MusteriAd"].ToString(),
                    MusteriSoyad = reader["MusteriSoyad"].ToString(),
                    MusteriTelefon = reader["MusteriTelefon"].ToString(),
                    MusteriEposta = reader["MusteriEposta"].ToString(),
                    KonaklamaDurumu = Convert.ToBoolean(reader["KonaklamaDurumu"]),
                    OdaID = Convert.ToInt32(reader["OdaID"])
                };

                MusteriDataTable.Add(musteri);
            }

            reader.Close();
            CloseConnection();

            return MusteriDataTable;
        }


        public void UpdateDgwOteldeBulunanMisafirler(DataGridView dataGridView)
        {
            CreateConnection();

        
            SqlCommand command = new SqlCommand("SELECT* FROM MusteriTable WHERE KonaklamaDurumu = 1", _connection);

            SqlDataReader reader = command.ExecuteReader();

            List<Musteri> MusteriDataTable = new List<Musteri>();

            while (reader.Read())
            {
                Musteri musteri = new Musteri
                {
                    MusteriId = Convert.ToInt32(reader["MusteriId"]),
                    MusteriAd = reader["MusteriAd"].ToString(),
                    MusteriSoyad = reader["MusteriSoyad"].ToString(),
                    MusteriTelefon = reader["MusteriTelefon"].ToString(),
                    MusteriEposta = reader["MusteriEposta"].ToString(),
                    KonaklamaDurumu = Convert.ToBoolean(reader["KonaklamaDurumu"]),
                    OdaID = Convert.ToInt32(reader["OdaID"])
                };
               
                MusteriDataTable.Add(musteri);
            }

            reader.Close();
            CloseConnection();

           
            dataGridView.DataSource = MusteriDataTable;
            

        }
        public void GetBugunGelecekMisafirler(DataGridView dataGridView)
        {
            CreateConnection();

           
            DateTime bugununTarihi = DateTime.Now.Date;

           
            SqlCommand command = new SqlCommand("SELECT MusteriTable.MusteriId, MusteriAd, MusteriSoyad, MusteriTelefon, MusteriEposta " +
                                               "FROM RezervasyonTable " +
                                               "JOIN MusteriTable ON RezervasyonTable.MusteriId = MusteriTable.MusteriId " +
                                               "WHERE CONVERT(date, RezervasyonTable.GirisTarihi) = @BugununTarihi", _connection);

        
            command.Parameters.AddWithValue("@BugununTarihi", bugununTarihi);

            SqlDataReader reader = command.ExecuteReader();

            List<Musteri> MusteriDataTable = new List<Musteri>();

            while (reader.Read())
            {
                Musteri musteri = new Musteri
                {
                    MusteriId = Convert.ToInt32(reader["MusteriId"]),
                    MusteriAd = reader["MusteriAd"].ToString(),
                    MusteriSoyad = reader["MusteriSoyad"].ToString(),
                    MusteriTelefon = reader["MusteriTelefon"].ToString(),
                    MusteriEposta = reader["MusteriEposta"].ToString()
                    
                };

                MusteriDataTable.Add(musteri);
            }

            reader.Close();
            CloseConnection();

           
            dataGridView.DataSource = MusteriDataTable;
        }
        public void MisafirSil(int musteriId)
        {


           
            string deleteQuery = "DELETE FROM MusteriTable WHERE MusteriId = @MusteriId";

            using (SqlCommand command = new SqlCommand(deleteQuery, _connection))
            {
                command.Parameters.AddWithValue("@MusteriId", musteriId);

                try
                {
                    _connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Misafir silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Misafir bulunamadı veya silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Misafir silinirken bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }

            }
        }
        public List<Musteri> SearchMusteri(string aramaMetni)
        {
            CreateConnection();

           
            SqlCommand command = new SqlCommand("SELECT * FROM MusteriTable WHERE MusteriAd + ' ' + MusteriSoyad LIKE @AramaMetni", _connection);

           
            command.Parameters.AddWithValue("@AramaMetni", "%" + aramaMetni + "%");

            SqlDataReader reader = command.ExecuteReader();

            List<Musteri> MusteriDataTable = new List<Musteri>();

            while (reader.Read())
            {
                Musteri musteri = new Musteri
                {
                    MusteriId = Convert.ToInt32(reader["MusteriId"]),
                    MusteriAd = reader["MusteriAd"].ToString(),
                    MusteriSoyad = reader["MusteriSoyad"].ToString(),
                    MusteriTelefon = reader["MusteriTelefon"].ToString(),
                    MusteriEposta = reader["MusteriEposta"].ToString(),
                    KonaklamaDurumu = Convert.ToBoolean(reader["KonaklamaDurumu"])
                };

                MusteriDataTable.Add(musteri);
            }

            reader.Close();
            CloseConnection();

            return MusteriDataTable;
        }

        public void SearchAndFillDgwMisafirler(DataGridView dataGridView, string aramaMetni)
        {
         
            dataGridView.DataSource = SearchMusteri(aramaMetni);
        }
        public void OdadakiMusteriGoster(int odaID, Label lblMisafirBilgileri)
        {
            try
            {
                CreateConnection();

               
                int odaDurumu;
                using (SqlCommand checkCmd = new SqlCommand("SELECT OdaDurumu FROM OdaTable WHERE OdaId = @OdaID", _connection))
                {
                    checkCmd.Parameters.AddWithValue("@OdaID", odaID);
                    odaDurumu = Convert.ToInt32(checkCmd.ExecuteScalar());
                }

                if (odaDurumu == 1)
                {
                    
                    using (SqlCommand cmd = new SqlCommand("SELECT MusteriAd, MusteriSoyad, MusteriTelefon, MusteriEposta FROM MusteriTable WHERE OdaID = @OdaID", _connection))
                    {
                        cmd.Parameters.AddWithValue("@OdaID", odaID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            
                            lblMisafirBilgileri.Text = "";

                            while (reader.Read())
                            {
                                
                                lblMisafirBilgileri.Text += $" Misafir Adı: {reader["MusteriAd"]}\n Misafir Soyadı: {reader["MusteriSoyad"]}\n Telefon Numarası: {reader["MusteriTelefon"]}\n E-Posta: {reader["MusteriEposta"]}";
                            }
                        }
                    }
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri bilgileri alınırken hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateKonaklamaDurumu()
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("UPDATE MusteriTable SET KonaklamaDurumu = 0 WHERE MusteriId IN (SELECT MusteriId FROM RezervasyonTable WHERE CikisTarihi < GETDATE() OR GETDATE() < GirisTarihi)", _connection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("UPDATE MusteriTable SET KonaklamaDurumu = 1 WHERE MusteriId IN (SELECT MusteriId FROM RezervasyonTable WHERE CikisTarihi > GETDATE() AND GETDATE() > GirisTarihi)", _connection))
                {
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE OdaTable SET OdaDurumu = 0 WHERE OdaId IN (SELECT OdaID FROM RezervasyonTable WHERE CikisTarihi < GETDATE() OR GETDATE() < GirisTarihi)", _connection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("UPDATE OdaTable SET OdaDurumu = 1 WHERE OdaId IN (SELECT OdaID FROM RezervasyonTable WHERE CikisTarihi > GETDATE() AND GETDATE() > GirisTarihi)", _connection))
                {
                    cmd.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Konaklama durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
       public void UpdateOtelDolulukGrafigi2(Chart OtelDolulukGrafigi2)
        {
            CreateConnection();

            try
            {
                SqlCommand countCommand = new SqlCommand("SELECT COUNT(*) FROM MusteriTable WHERE KonaklamaDurumu = 1", _connection);
                int kalanMusteriSayisi = Convert.ToInt32(countCommand.ExecuteScalar());

                int toplamOtelKapasitesi = 100;

                int kalanKapasite = toplamOtelKapasitesi - kalanMusteriSayisi;

                OtelDolulukGrafigi2.Series.Clear();

                Series series = new Series("OtelDoluluk");
                series.Points.AddXY("Kalan Kapasite", kalanKapasite);
                series.Points.AddXY("Toplam Müşteri", kalanMusteriSayisi);

                series.ChartType = SeriesChartType.Pie;

                OtelDolulukGrafigi2.Series.Add(series);

                OtelDolulukGrafigi2.Titles.Clear();
                OtelDolulukGrafigi2.Titles.Add("Otel Doluluk Grafiği");

                OtelDolulukGrafigi2.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Otel doluluk grafiği güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdateMusteriOdaID(int odaID, int musteriID)
        {
            try
            {
                CreateConnection();
                using (SqlCommand cmd = new SqlCommand("UPDATE MusteriTable SET OdaID = @OdaID WHERE MusteriID = @MusteriID", _connection))
                {
                    cmd.Parameters.AddWithValue("@OdaID", odaID);
                    cmd.Parameters.AddWithValue("@MusteriID", musteriID);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteriye ait OdaID güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public int AddMusteriAndGetID(Musteri musteri)
        {
            try
            {
               CreateConnection();

                SqlCommand cmd = new SqlCommand("INSERT INTO MusteriTable (MusteriAd, MusteriSoyad, MusteriTelefon, MusteriEposta, OdaID) " +
                                               "VALUES (@MusteriAd, @MusteriSoyad, @MusteriTelefon, @MusteriEposta, @OdaID);" +
                                               "SELECT SCOPE_IDENTITY();", _connection);

                cmd.Parameters.AddWithValue("@MusteriAd", musteri.MusteriAd);
                cmd.Parameters.AddWithValue("@MusteriSoyad", musteri.MusteriSoyad);
                cmd.Parameters.AddWithValue("@MusteriTelefon", musteri.MusteriTelefon);
                cmd.Parameters.AddWithValue("@MusteriEposta", musteri.MusteriEposta);
                cmd.Parameters.AddWithValue("@OdaID", musteri.OdaID);

                int musteriID = Convert.ToInt32(cmd.ExecuteScalar());

                return musteriID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Müşteri eklenirken bir hata oluştu: " + ex.Message);
                return -1; 
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdateOdaDurumuForKonaklamaDurumuFalse()
        {
            try
            {
                CreateConnection();

             
                string updateQuery = "UPDATE OdaTable SET OdaDurumu = 0 WHERE OdaID IN (SELECT OdaID FROM MusteriTable WHERE KonaklamaDurumu = 0)";

                using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                {
                
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
