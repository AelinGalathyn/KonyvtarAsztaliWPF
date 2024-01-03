using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KonyvtarAsztaliWPF
{
    public class Statisztika
    {
        private List<Konyv> konyvek;
        private MySqlConnectionStringBuilder connectionStringBuilder;

        public Statisztika()
        {
            konyvek = new List<Konyv>();
            connectionStringBuilder = new MySqlConnectionStringBuilder();
            Beolvas();
        }

        private void Beolvas()
        {
            connectionStringBuilder.Server = "localhost";
            connectionStringBuilder.Port = 3306;
            connectionStringBuilder.Database = "vizsga-2022-14s-wip-db";
            connectionStringBuilder.UserID = "root";
            connectionStringBuilder.Password = "";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM books";
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            konyvek.Add(new Konyv(reader.GetInt32("id"), reader.GetString("title"), reader.GetString("author"), reader.GetInt32("publish_year"), reader.GetInt32("page_count")));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occurred. The application will now close.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

        public bool Torles(Konyv konyv)
        {
            bool isDeleted = false;

            using (MySqlConnection connection = new MySqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                string sql = $"DELETE FROM books WHERE id = {konyv.Id}";
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = sql;

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    isDeleted = true;
                }
            }
            return isDeleted;
        }

        public List<Konyv> Konyvek { get => konyvek; set => konyvek = value; }
    }
}
