using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class StaffManager
    {
        private static string SafeString(MySqlDataReader r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? "" : r.GetString(i);
        }

        public List<StaffRecord> GetStaff(string? search = null)
        {
            var list = new List<StaffRecord>();
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                string q = @"
                    SELECT staffID, username, fullName, role, phone, email, atPin
                    FROM staff_members
                    WHERE @search IS NULL
                       OR username LIKE CONCAT('%', @search, '%')
                       OR fullName LIKE CONCAT('%', @search, '%')
                    ORDER BY staffID DESC";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@search", string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim());
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StaffRecord
                    {
                        StaffID = reader.GetInt32("staffID"),
                        Username = SafeString(reader, "username"),
                        FullName = SafeString(reader, "fullName"),
                        Role = SafeString(reader, "role"),
                        Phone = SafeString(reader, "phone"),
                        Email = SafeString(reader, "email"),
                        AtPin = SafeString(reader, "atPin")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Staff table: " + ex.Message + "\n\nRun Backend/schema_extensions.sql if needed.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return list;
        }

        public bool AddStaff(StaffRecord s)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand(@"
                    INSERT INTO staff_members (username, fullName, role, phone, email, atPin)
                    VALUES (@u, @f, @r, @ph, @e, @pin)", conn);
                cmd.Parameters.AddWithValue("@u", s.Username);
                cmd.Parameters.AddWithValue("@f", s.FullName);
                cmd.Parameters.AddWithValue("@r", string.IsNullOrWhiteSpace(s.Role) ? "Staff" : s.Role);
                cmd.Parameters.AddWithValue("@ph", string.IsNullOrWhiteSpace(s.Phone) ? DBNull.Value : s.Phone);
                cmd.Parameters.AddWithValue("@e", string.IsNullOrWhiteSpace(s.Email) ? DBNull.Value : s.Email);
                cmd.Parameters.AddWithValue("@pin", string.IsNullOrWhiteSpace(s.AtPin) ? DBNull.Value : s.AtPin);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteStaff(int id)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("DELETE FROM staff_members WHERE staffID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public int GetTotalStaff()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                return Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM staff_members", conn).ExecuteScalar());
            }
            catch { return 0; }
        }
    }
}
