using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace OtelRezervasyonProjesi
{
    public class OdaDal : DbManager
    {
        DbManager _DbManager = new DbManager();
    
        public void GetOdaDurumuStatistics(Chart chart)
        {
            try
            {
                CreateConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) AS DoluOdaSayisi FROM OdaTable WHERE OdaDurumu = 1", _connection))
                {
                    int doluOdaSayisi = Convert.ToInt32(cmd.ExecuteScalar());
                    int bosOdaSayisi = 25 - doluOdaSayisi;

                   
                    chart.Series["OdaDurumu"].Points.AddXY("Dolu", doluOdaSayisi);
                    chart.Series["OdaDurumu"].Points.AddXY("Boş", bosOdaSayisi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri alınırken hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void UpdateOdaDurumu(int odaNumarasi)
        {
            try
            {
                CreateConnection();

               
                string updateQuery = "UPDATE OdaTable SET OdaDurumu = 0 WHERE  = @OdaID ";

                using (SqlCommand cmd = new SqlCommand(updateQuery, _connection))
                {
                    cmd.Parameters.AddWithValue("@OdaID", odaNumarasi);

                   
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda durumu güncellenirken bir hata oluştu: " + ex.ToString());
            }
            finally
            {
                CloseConnection();
            }
        }

        public void GetOdaBilgileri(int OdaID, OdaForm odaForm)
        {
            try
            {
                CreateConnection();
                using (SqlCommand cmd = new SqlCommand("SELECT OdaTuru, OdaDurumu, EngelliDostu, OdaTemizlikDurumu FROM OdaTable WHERE OdaID = @OdaID", _connection))
                {
                    cmd.Parameters.AddWithValue("@OdaID", OdaID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                           
                            odaForm.lblOdaTuru.Text = reader["OdaTuru"].ToString();

                           
                            bool odaDurumu = Convert.ToBoolean(reader["OdaDurumu"]);
                            odaForm.lblOdaDurumu.Text = odaDurumu ? "Dolu" : "Boş";

                           
                            bool engelliDostu = Convert.ToBoolean(reader["EngelliDostu"]);
                            odaForm.lblEngelliDostu.Text = engelliDostu ? "Evet" : "Hayır";

                           
                 
                            bool odaTemizlikDurumu = Convert.ToBoolean(reader["OdaTemizlikDurumu"]);
                            odaForm.lblOdaTemizlikDurumu.Text = odaTemizlikDurumu ? "Temiz" : "Temiz Değil";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda bilgileri alınırken hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateOdaTemizlikDurumu(int odaNumarasi)
        {
            
            
                try
                {
                    _connection.Open();

                    using (SqlCommand cmd = new SqlCommand("UPDATE OdaTable SET OdaTemizlikDurumu = 0 WHERE OdaId = @OdaID", _connection))
                    {
                        cmd.Parameters.AddWithValue("@OdaID", odaNumarasi);
                        cmd.ExecuteNonQuery();
                    }
                MessageBox.Show("Temizlik talebiniz alınmıştır. Teşekkür ederiz!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Oda temizlik durumu güncellenirken bir hata oluştu: " + ex.Message);
                }
                finally { _connection.Close(); }
            
        }
        public void FillDgwTemizlikDurumu(DataGridView dgwTemizlikDurumu)
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT OdaID, OdaTemizlikDurumu , OdaDurumu FROM OdaTable", _connection))
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    
                    dgwTemizlikDurumu.DataSource = dataTable;

                    
                    dgwTemizlikDurumu.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Temizlik durumu bilgileri alınırken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public void OdalarListesiniDoldur(DataGridView dgwOdalarListesi)
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM OdaTable", _connection))
                {

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    
                    dgwOdalarListesi.DataSource = dataTable;

                   
                    dgwOdalarListesi.Refresh();
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
        public void OdaTemizlikIslemi(int odaID, bool temizMi)
        {
            try
            {
                CreateConnection();

                using (SqlCommand cmd = new SqlCommand("UPDATE OdaTable SET OdaTemizlikDurumu = @TemizlikDurumu WHERE OdaID = @OdaID", _connection))
                {
                    cmd.Parameters.AddWithValue("@OdaID", odaID);
                    cmd.Parameters.AddWithValue("@TemizlikDurumu", temizMi);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Oda temizlik durumu güncellenirken bir hata oluştu: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetAvailableRoom(string odaTipi, DateTime checkInDate, DateTime checkOutDate)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 OdaID FROM OdaTable WHERE OdaDurumu = 0 AND OdaTipi = @OdaTipi", _connection);
                cmd.Parameters.AddWithValue("@OdaTipi", odaTipi);
                int odaId = Convert.ToInt32(cmd.ExecuteScalar());

                return odaId;
            }
            finally
            {
                CloseConnection();
            }
        }


        public void UpdateRoomStatus(int odaId, bool odaDurumu)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("UPDATE OdaTable SET OdaDurumu = @OdaDurumu WHERE OdaID = @OdaID", _connection);
                cmd.Parameters.AddWithValue("@OdaDurumu", odaDurumu);
                cmd.Parameters.AddWithValue("@OdaID", odaId);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                CloseConnection();
            }
        }
        
    }
}

