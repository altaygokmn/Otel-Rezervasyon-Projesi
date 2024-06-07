using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtelRezervasyonProjesi
{
    public class PdfManager : DbManager
    {

        public void MusteriListesiIndir(List<Musteri> misafirListesi)
        {
            ExportToPdf(misafirListesi, "Misafir Listesi");
        }

        public void PersonelListesiIndir(List<Personel> personelListesi)
        {
            ExportToPdf(personelListesi, "Personel Listesi");
        }

        public void RezervasyonListesiIndir(List<Rezervasyon> rezervasyonListesi)
        {
            ExportToPdf(rezervasyonListesi, "Rezervasyon Listesi");
        }
        private void ExportToPdf<T>(List<T> dataList, string title) where T : class
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "Pdf Dosyası(*.pdf)|.pdf";
            if (file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (FileStream dosya = File.Open(file.FileName, FileMode.Create))
                    {


                        Document document = new Document();
                        PdfWriter.GetInstance(document, dosya);
                        document.Open();
                        Paragraph titleParagraph = new Paragraph(title);
                        titleParagraph.Alignment = Element.ALIGN_CENTER; // Başlığı ortala
                        document.Add(titleParagraph);
                        document.AddAuthor("Otel Yönetim Sistemi");
                        document.AddCreator("Otel Yönetim Sistemi");
                        document.AddTitle(title);
                        document.AddSubject(title);
                        document.AddCreationDate();

                        foreach (var data in dataList)
                        {
                            var properties = data.GetType().GetProperties();
                            Paragraph dataParagraph = new Paragraph();

                            foreach (var property in properties)
                            {
                                dataParagraph.Add($"{property.Name}:  {property.GetValue(data)}\n");

                            }

                            document.Add(dataParagraph);
                            document.Add(new Paragraph("\n\n"));
                        }

                        document.Close();
                        MessageBox.Show("PDF dosyası başarıyla oluşturuldu ve kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PDF oluşturma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

        }
        public void OdemeBilgileriniIndir(int musteriID)
        {
           
            List<Odeme> odemeListesi = GetOdemeListesiByMusteriID(musteriID);

            if (odemeListesi != null && odemeListesi.Count > 0)
            {
              
                ExportToPdf(odemeListesi, $"{musteriID} Numarali Müsteriye Ait Ödeme Bilgileri");
            }
            else
            {
                MessageBox.Show("Müşteriye ait odeme bilgisi bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<Odeme> GetOdemeListesiByMusteriID(int musteriID)
        {
            try
            {
                CreateConnection();

                List<Odeme> odemeListesi = new List<Odeme>();

                
                SqlCommand cmd = new SqlCommand("SELECT OdemeID, Ucret, OdemeTarihi, MusteriId FROM OdemeTable WHERE MusteriId = @MusteriId", _connection);
                cmd.Parameters.AddWithValue("@MusteriId", musteriID);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Odeme odeme = new Odeme
                    {
                        OdemeId = Convert.ToInt32(reader["OdemeID"]),
                      
                        Ucret = reader["Ucret"] != DBNull.Value ? Convert.ToDecimal(reader["Ucret"]) : default(decimal),
                        OdemeTarihi = Convert.ToDateTime(reader["OdemeTarihi"]),
                        OdemeDurumu = true,
                        MusteriId = Convert.ToInt32(reader["MusteriId"])
                    };

                    odemeListesi.Add(odeme);
                }

                return odemeListesi;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Odeme bilgileri getirilirken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
