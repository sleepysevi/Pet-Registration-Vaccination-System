using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class DatabaseConfig
    {
        private static readonly string connectionString = BuildConnectionString();

        private static string BuildConnectionString()
        {
            var fromEnv = Environment.GetEnvironmentVariable("ALAGATRACK_DB_CONNECTION");
            if (!string.IsNullOrWhiteSpace(fromEnv))
            {
                return fromEnv;
            }

            return "Server=localhost;Database=AlagaTrack;Uid=root;Pwd=sleepyzaire123;";
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        // Helper method pang test the connection
        public static void TestConnection()
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection Successful!", "Database Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}