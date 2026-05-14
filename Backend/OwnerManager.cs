using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class OwnerManager
    {
        private static string SafeString(MySqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }

        private static bool ColumnExists(MySqlConnection conn, string table, string column)
        {
            const string query = @"
                SELECT COUNT(*)
                FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE()
                  AND TABLE_NAME = @table
                  AND COLUMN_NAME = @column";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@table", table);
                cmd.Parameters.AddWithValue("@column", column);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }

        private static string ResolveCreatedColumn(MySqlConnection conn)
        {
            if (ColumnExists(conn, "owners", "createdAt"))
            {
                return "createdAt";
            }

            if (ColumnExists(conn, "owners", "created_at"))
            {
                return "created_at";
            }

            return string.Empty;
        }

        public List<OwnerRecord> GetOwners(string? search = null)
        {
            var owners = new List<OwnerRecord>();

            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    var hasEmail = ColumnExists(conn, "owners", "email");
                    var hasNotes = ColumnExists(conn, "owners", "notes");
                    string query = @"
                        SELECT ownerID, name, contactNumber, address,
                               " + (hasEmail ? "COALESCE(email, '')" : "''") + @" AS email,
                               " + (hasNotes ? "COALESCE(notes, '')" : "''") + @" AS notes
                        FROM owners
                        WHERE @search IS NULL
                           OR name LIKE CONCAT('%', @search, '%')
                           OR contactNumber LIKE CONCAT('%', @search, '%')
                           OR address LIKE CONCAT('%', @search, '%')
                        ORDER BY ownerID DESC";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim());

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                owners.Add(new OwnerRecord
                                {
                                    OwnerID = reader.GetInt32("ownerID"),
                                    Name = SafeString(reader, "name"),
                                    ContactNumber = SafeString(reader, "contactNumber"),
                                    Address = SafeString(reader, "address"),
                                    Email = SafeString(reader, "email"),
                                    Notes = SafeString(reader, "notes")
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return owners;
        }

        public (int TotalOwners, int NewToday, double AvgPetsPerOwner) GetOwnerStats()
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    string createdColumn = ResolveCreatedColumn(conn);

                    int totalOwners = 0;
                    int newToday = 0;
                    double avgPets = 0;

                    using (var totalCmd = new MySqlCommand("SELECT COUNT(*) FROM owners", conn))
                    {
                        totalOwners = Convert.ToInt32(totalCmd.ExecuteScalar());
                    }

                    if (!string.IsNullOrWhiteSpace(createdColumn))
                    {
                        using (var todayCmd = new MySqlCommand($"SELECT COUNT(*) FROM owners WHERE DATE({createdColumn}) = CURDATE()", conn))
                        {
                            try
                            {
                                newToday = Convert.ToInt32(todayCmd.ExecuteScalar());
                            }
                            catch
                            {
                                newToday = 0;
                            }
                        }
                    }
                    else
                    {
                        newToday = 0;
                    }

                    using (var avgCmd = new MySqlCommand("SELECT IFNULL(AVG(petCount), 0) FROM (SELECT COUNT(*) AS petCount FROM pets GROUP BY ownerID) t", conn))
                    {
                        avgPets = Convert.ToDouble(avgCmd.ExecuteScalar());
                    }

                    return (totalOwners, newToday, Math.Round(avgPets, 1));
                }
            }
            catch
            {
                return (0, 0, 0);
            }
        }

        public bool AddOwner(string name, string address, string contact, string? email = null, string? notes = null)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    var hasEmail = ColumnExists(conn, "owners", "email");
                    var hasNotes = ColumnExists(conn, "owners", "notes");

                    string columns = "name, address, contactNumber";
                    string values = "@name, @address, @contact";

                    if (hasEmail)
                    {
                        columns += ", email";
                        values += ", @email";
                    }

                    if (hasNotes)
                    {
                        columns += ", notes";
                        values += ", @notes";
                    }

                    string query = $"INSERT INTO owners ({columns}) VALUES ({values})";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@address", address);
                        cmd.Parameters.AddWithValue("@contact", contact);
                        if (hasEmail)
                        {
                            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email.Trim());
                        }
                        if (hasNotes)
                        {
                            cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? DBNull.Value : notes.Trim());
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateOwner(OwnerRecord owner)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    var hasEmail = ColumnExists(conn, "owners", "email");
                    var hasNotes = ColumnExists(conn, "owners", "notes");
                    string query = @"
                        UPDATE owners
                        SET name = @name,
                            address = @address,
                            contactNumber = @contact"
                        + (hasEmail ? ", email = @email" : "")
                        + (hasNotes ? ", notes = @notes" : "")
                        + @"
                        WHERE ownerID = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", owner.OwnerID);
                        cmd.Parameters.AddWithValue("@name", owner.Name);
                        cmd.Parameters.AddWithValue("@address", owner.Address);
                        cmd.Parameters.AddWithValue("@contact", owner.ContactNumber);
                        if (hasEmail)
                        {
                            cmd.Parameters.AddWithValue("@email", string.IsNullOrWhiteSpace(owner.Email) ? DBNull.Value : owner.Email.Trim());
                        }
                        if (hasNotes)
                        {
                            cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(owner.Notes) ? DBNull.Value : owner.Notes.Trim());
                        }
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteOwner(int ownerID)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM owners WHERE ownerID = @id";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", ownerID);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException mex)
            {
                // MySQL foreign key constraint failure number is 1451
                if (mex.Number == 1451)
                {
                    MessageBox.Show("Cannot delete owner, they still have pets registered.", "Dependencies Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Database Error: " + mex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}