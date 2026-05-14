using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class LostReportManager
    {
        private const string LostTable = "lost_pet_reports";

        private static string SafeString(MySqlDataReader r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? "" : r.GetString(i);
        }

        private static bool TableExists(MySqlConnection conn, string table)
        {
            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @t", conn);
            cmd.Parameters.AddWithValue("@t", table);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public List<LostReportRecord> GetReports(string? search = null)
        {
            var list = new List<LostReportRecord>();
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!TableExists(conn, LostTable))
                    return list;

                string q = @"
                    SELECT reportID, ownerName, petName, dateLost, lastSeenLocation, description, status
                    FROM lost_pet_reports
                    WHERE @search IS NULL
                       OR ownerName LIKE CONCAT('%', @search, '%')
                       OR petName LIKE CONCAT('%', @search, '%')
                       OR lastSeenLocation LIKE CONCAT('%', @search, '%')
                    ORDER BY reportID DESC";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@search", string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim());
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new LostReportRecord
                    {
                        ReportID = reader.GetInt32("reportID"),
                        OwnerName = SafeString(reader, "ownerName"),
                        PetName = SafeString(reader, "petName"),
                        DateLost = reader.GetDateTime("dateLost").ToString("yyyy-MM-dd HH:mm"),
                        LastSeenLocation = SafeString(reader, "lastSeenLocation"),
                        Description = SafeString(reader, "description"),
                        Status = SafeString(reader, "status")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lost reports: " + ex.Message + "\n\nRun Backend/schema_extensions.sql on this database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return list;
        }

        public bool AddReport(LostReportRecord r)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!TableExists(conn, LostTable))
                {
                    MessageBox.Show(
                        "The lost_pet_reports table is missing.\n\nRun Backend/schema_extensions.sql in MySQL (same database as the app), then try again.",
                        "Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                using var cmd = new MySqlCommand(@"
                    INSERT INTO lost_pet_reports (ownerName, petName, dateLost, lastSeenLocation, description, status)
                    VALUES (@o, @p, @d, @l, @desc, 'active')", conn);
                cmd.Parameters.AddWithValue("@o", r.OwnerName);
                cmd.Parameters.AddWithValue("@p", r.PetName);
                if (!DateTime.TryParse(r.DateLost, out var lostDt))
                    lostDt = DateTime.Now;
                cmd.Parameters.AddWithValue("@d", lostDt);
                cmd.Parameters.AddWithValue("@l", string.IsNullOrWhiteSpace(r.LastSeenLocation) ? DBNull.Value : r.LastSeenLocation);
                cmd.Parameters.AddWithValue("@desc", string.IsNullOrWhiteSpace(r.Description) ? DBNull.Value : r.Description);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool SetStatus(int reportId, string status)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!TableExists(conn, LostTable))
                    return false;

                using var cmd = new MySqlCommand("UPDATE lost_pet_reports SET status = @s WHERE reportID = @id", conn);
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@id", reportId);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public int CountByStatus(string status)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!TableExists(conn, LostTable))
                    return 0;

                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM lost_pet_reports WHERE status = @s", conn);
                cmd.Parameters.AddWithValue("@s", status);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
        }

        public int GetTotalReports()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!TableExists(conn, LostTable))
                    return 0;

                return Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM lost_pet_reports", conn).ExecuteScalar());
            }
            catch { return 0; }
        }
    }
}
