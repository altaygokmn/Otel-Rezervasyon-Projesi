using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace OtelRezervasyonProjesi
{
    public class RezervasyonDal : DbManager
    {
        DbManager _DbManager = new DbManager();
        

        public List<Rezervasyon> GetAllRezervasyon()
        {
            CreateConnection();
            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Rezervasyon> RezervasyonDataTable = new List<Rezervasyon>();

            while (reader.Read())
            {
                Rezervasyon rezervasyon = new Rezervasyon
                {
                    RezervasyonID = Convert.ToInt32(reader["RezervasyonID"]),
                    KisiSayisi = Convert.ToInt32(reader["KisiSayisi"]),
                    OdaTipi = Convert.ToString(reader["OdaTipi"]),
                    GirisTarihi = Convert.ToDateTime(reader["GirisTarihi"]),
                    CikisTarihi = Convert.ToDateTime(reader["CikisTarihi"]),
                    RezervasyonYapilisTarihi = Convert.ToDateTime(reader["RezervasyonYapilisTarihi"]),
                    OdaID = Convert.ToInt32(reader["OdaID"]),
                    MusteriId = Convert.ToInt32(reader["MusteriId"])
                };

                RezervasyonDataTable.Add(rezervasyon);
            }

            reader.Close();
            CloseConnection();

            return RezervasyonDataTable;
        }
        public List<Rezervasyon> GetGelecekRezervasyonlar()
        {
            CreateConnection();
            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID " +
                                              "WHERE R.GirisTarihi > GETDATE()", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Rezervasyon> gelecekRezervasyonlar = new List<Rezervasyon>();

            while (reader.Read())
            {
                Rezervasyon rezervasyon = MapRezervasyon(reader);
                gelecekRezervasyonlar.Add(rezervasyon);
            }

            reader.Close();
            CloseConnection();

            return gelecekRezervasyonlar;
        }

        public List<Rezervasyon> GetGecmisRezervasyonlar()
        {
            CreateConnection();
            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID " +
                                              "WHERE R.CikisTarihi < GETDATE()", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Rezervasyon> gecmisRezervasyonlar = new List<Rezervasyon>();

            while (reader.Read())
            {
                Rezervasyon rezervasyon = MapRezervasyon(reader);
                gecmisRezervasyonlar.Add(rezervasyon);
            }

            reader.Close();
            CloseConnection();

            return gecmisRezervasyonlar;
        }

        public List<Rezervasyon> GetAktifRezervasyonlar()
        {
            CreateConnection();
            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID " +
                                              "WHERE R.GirisTarihi <= GETDATE() AND R.CikisTarihi >= GETDATE()", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Rezervasyon> aktifRezervasyonlar = new List<Rezervasyon>();

            while (reader.Read())
            {
                Rezervasyon rezervasyon = MapRezervasyon(reader);
                aktifRezervasyonlar.Add(rezervasyon);
            }

            reader.Close();
            CloseConnection();

            return aktifRezervasyonlar;
        }
        public void IptalEdilenRezervasyonEkle(int RezervasyonID)
        {
          
            Rezervasyon iptalEdilenRezervasyon = GetRezervasyonByID(RezervasyonID);

            
            DateTime bugununTarihi = DateTime.Now.Date;

            
            if (iptalEdilenRezervasyon.GirisTarihi > bugununTarihi)
            {
               
                List<Rezervasyon> iptalEdilenRezervasyonlar = new List<Rezervasyon>
        {
            iptalEdilenRezervasyon
        };

            

                MessageBox.Show("Rezervasyon iptal edildi.");
            }
            else
            {
                MessageBox.Show("Seçilen rezervasyon iptal edilemez.");
            }
        }

        public Rezervasyon GetRezervasyonByID(int rezervasyonID)
        {
            CreateConnection();
            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID " +
                                              "WHERE R.RezervasyonID = @RezervasyonID", _connection);
            command.Parameters.AddWithValue("@RezervasyonID", rezervasyonID);
            SqlDataReader reader = command.ExecuteReader();

            Rezervasyon rezervasyon = null;

            if (reader.Read())
            {
                rezervasyon = MapRezervasyon(reader);
            }

            reader.Close();
            CloseConnection();

            return rezervasyon;
        }
        public void RezervasyonSil(int rezervasyonID)
        {


        
            string deleteQuery = "DELETE FROM RezervasyonTable WHERE RezervasyonID = @RezervasyonID";

            using (SqlCommand command = new SqlCommand(deleteQuery, _connection))
            {
                command.Parameters.AddWithValue("@RezervasyonID", rezervasyonID);

                try
                {
                    _connection.Open();
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Rezervasyon silindi.");
                    }
                    else
                    {
                        MessageBox.Show("Rezervasyon bulunamadı veya silinemedi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Rezervasyon silinirken bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    CloseConnection();
                }

            }
        }
        private Rezervasyon MapRezervasyon(SqlDataReader reader)
        {
            return new Rezervasyon
            {
                RezervasyonID = Convert.ToInt32(reader["RezervasyonID"]),
                KisiSayisi = Convert.ToInt32(reader["KisiSayisi"]),
                OdaTipi = Convert.ToString(reader["OdaTipi"]),
                GirisTarihi = Convert.ToDateTime(reader["GirisTarihi"]),
                CikisTarihi = Convert.ToDateTime(reader["CikisTarihi"]),
                RezervasyonYapilisTarihi = Convert.ToDateTime(reader["RezervasyonYapilisTarihi"]),
                OdaID = Convert.ToInt32(reader["OdaID"]),
                MusteriId = Convert.ToInt32(reader["MusteriId"])
            };
        }
        public List<Rezervasyon> SearchRezervasyonById(int rezervasyonId)
        {
            CreateConnection();

            SqlCommand command = new SqlCommand("SELECT R.*, O.OdaID, M.MusteriID FROM RezervasyonTable R " +
                                              "INNER JOIN OdaTable O ON R.OdaID = O.OdaID " +
                                              "INNER JOIN MusteriTable M ON R.MusteriID = M.MusteriID " +
                                              "WHERE R.RezervasyonID = @RezervasyonID", _connection);

            command.Parameters.AddWithValue("@RezervasyonID", rezervasyonId);

            SqlDataReader reader = command.ExecuteReader();

            List<Rezervasyon> rezervasyonDataTable = new List<Rezervasyon>();

            while (reader.Read())
            {
                Rezervasyon rezervasyon = MapRezervasyon(reader);
                rezervasyonDataTable.Add(rezervasyon);
            }

            reader.Close();
            CloseConnection();

            return rezervasyonDataTable;
        }

        public void SearchAndFillDgwRezervasyonlar(DataGridView dataGridView, int rezervasyonId)
        {
            
            dataGridView.DataSource = SearchRezervasyonById(rezervasyonId);
        }
        public void RezervasyonListesiDoldur(DataGridView dgwRezervasyonListesi)
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM RezervasyonTable", _connection))
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                   
                    dgwRezervasyonListesi.DataSource = dataTable;

                   
                    dgwRezervasyonListesi.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda bilgileri alınırken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateRezervasyonSayisiGrafigi(Chart rezervasyonSayisiGrafigi, string selectedOption)
        {
            CreateConnection();

            try
            {
               
                string sqlCommandText = "";

                if (selectedOption == "Bu Yıl")
                {
                    sqlCommandText = "SELECT GirisTarihi, COUNT(*) AS RezervasyonSayisi FROM RezervasyonTable WHERE YEAR(GirisTarihi) = YEAR(GETDATE()) GROUP BY GirisTarihi";
                }
                else
                {
                    sqlCommandText = "SELECT GirisTarihi, COUNT(*) AS RezervasyonSayisi FROM RezervasyonTable GROUP BY GirisTarihi";
                }

                SqlCommand countCommand = new SqlCommand(sqlCommandText, _connection);
                SqlDataReader reader = countCommand.ExecuteReader();

               
                rezervasyonSayisiGrafigi.Series.Clear();

               
                Series series = new Series("RezervasyonSayisi");
                while (reader.Read())
                {
                    DateTime girisTarihi = Convert.ToDateTime(reader["GirisTarihi"]);
                    int rezervasyonSayisi = Convert.ToInt32(reader["RezervasyonSayisi"]);

                    
                    string dataPointLabel = girisTarihi.ToString("MMM-yyyy");

                    series.Points.AddXY(dataPointLabel, rezervasyonSayisi);
                }

               
                series.ChartType = SeriesChartType.Column;

               
                rezervasyonSayisiGrafigi.Series.Add(series);

               
                rezervasyonSayisiGrafigi.Titles.Clear();
                rezervasyonSayisiGrafigi.Titles.Add("Rezervasyon Sayısı Grafiği");

                
                rezervasyonSayisiGrafigi.ChartAreas[0].AxisX.LabelStyle.Angle = -45;

               
                rezervasyonSayisiGrafigi.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rezervasyon sayısı grafiği güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        


    }
}
