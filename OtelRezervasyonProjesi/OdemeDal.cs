using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
        public class OdemeDal : DbManager
    {
        DbManager _DbManager = new DbManager();
        

        public int AddOdeme(int musteriID, string odaTipi, DateTime girisTarihi, DateTime cikisTarihi)
        {
            try
            {
                CreateConnection();

              
                double ucret = 0;

                switch (odaTipi)
                {
                    case "Standart Oda":
                        ucret = 1250 * (cikisTarihi - girisTarihi).TotalDays;
                        break;

                    case "Suite":
                        ucret = 4850 * (cikisTarihi - girisTarihi).TotalDays;
                        break;

                    case "Deluxe Oda":
                        ucret = 6550 * (cikisTarihi - girisTarihi).TotalDays;
                        break;

                  

                    default:
                        MessageBox.Show("Geçersiz oda tipi seçildi.");
                        return -1;
                }

                
                DateTime odemeTarihi = DateTime.Now;

                
                bool odemeDurumu = true;

                
                SqlCommand cmd = new SqlCommand("INSERT INTO OdemeTable (Ucret, OdemeTarihi, OdemeDurumu, MusteriID) VALUES (@Ucret, @OdemeTarihi, @OdemeDurumu, @MusteriID); SELECT SCOPE_IDENTITY();", _connection);

                cmd.Parameters.AddWithValue("@Ucret", ucret);
                cmd.Parameters.AddWithValue("@OdemeTarihi", odemeTarihi);
                cmd.Parameters.AddWithValue("@OdemeDurumu", odemeDurumu);
                cmd.Parameters.AddWithValue("@MusteriID", musteriID);

               
                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }

                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme eklenirken bir hata oluştu: " + ex.Message);
                return -1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public double GetOdemeUcret(int odemeID)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT Ucret FROM OdemeTable WHERE OdemeID = @OdemeID", _connection);
                cmd.Parameters.AddWithValue("@OdemeID", odemeID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToDouble(result);
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ücret getirilirken bir hata oluştu: " + ex.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public double GetOdemeUcretByMusteriID(int musteriID)
        {
            try
            {
                CreateConnection();

                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Ucret FROM OdemeTable WHERE MusteriID = @MusteriID ORDER BY OdemeTarihi DESC", _connection);
                cmd.Parameters.AddWithValue("@MusteriID", musteriID);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToDouble(result);
                }

                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ücret getirilirken bir hata oluştu: " + ex.Message);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }



    }
}
