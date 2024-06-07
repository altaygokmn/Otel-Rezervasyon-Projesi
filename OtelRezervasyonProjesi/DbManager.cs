using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtelRezervasyonProjesi
{
    public class DbManager
    {
        public SqlConnection _connection;

        

        public void CreateConnection()
{
    if (_connection == null)
    {
        _connection = new SqlConnection(@"server=(localdb)\MSSQLLocalDB;Initial Catalog=Dbo_OtelRezervasyon;Integrated Security=True");
    }

    if (_connection.State == ConnectionState.Closed)
    {
        _connection.Open();
    }
}

        public void CloseConnection()
        {
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }
        public void BackupDatabase()
        {
            try
            {
                CreateConnection();

                DateTime dateTime = DateTime.Now;
                string date = dateTime.Day + "-" + dateTime.Month;
                string databaseName = "MyDatabase.db";

                string command1 = "USE " + databaseName + ";";
                string command2 = "BACKUP DATABASE " + databaseName + " TO DISK = 'C:\\database\\" + databaseName + "_" + date + ".Bak' WITH FORMAT, MEDIANAME = 'Z_SQLServerBackups', NAME = 'Full Backup of " + databaseName + "';";

                SqlCommand cmd1 = new SqlCommand(command1, _connection);
                SqlCommand cmd2 = new SqlCommand(command2, _connection);

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Yedekleme işlemi başarılı!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yedekleme hatası: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }


}
