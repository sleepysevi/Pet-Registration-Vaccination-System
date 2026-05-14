using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class VaccinationManager
    {
        private static string SafeString(MySqlDataReader r, string col)
        {
            int i = r.GetOrdinal(col);
            return r.IsDBNull(i) ? "" : r.GetString(i);
        }

        private static bool ColumnExists(MySqlConnection conn, string table, string column)
        {
            using var cmd = new MySqlCommand(@"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
                WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = @t AND COLUMN_NAME = @c", conn);
            cmd.Parameters.AddWithValue("@t", table);
            cmd.Parameters.AddWithValue("@c", column);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public List<VaccinationRecord> GetVaccinations(string? search = null)
        {
            var list = new List<VaccinationRecord>();
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                bool hasNotes = ColumnExists(conn, "vaccinations", "notes");
                string notesSelect = hasNotes ? "v.notes" : "CAST('' AS CHAR) AS notes";
                string q = $@"
                    SELECT v.vaccinationID, v.petID, v.dateGiven, v.nextDueDate, {notesSelect},
                           COALESCE(p.petName,'') AS petName, COALESCE(o.name,'') AS ownerName
                    FROM vaccinations v
                    JOIN pets p ON p.petID = v.petID
                    LEFT JOIN owners o ON o.ownerID = p.ownerID
                    WHERE @search IS NULL
                       OR p.petName LIKE CONCAT('%', @search, '%')
                       OR o.name LIKE CONCAT('%', @search, '%')
                    ORDER BY v.vaccinationID DESC";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@search", string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim());
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new VaccinationRecord
                    {
                        VaccinationID = reader.GetInt32("vaccinationID"),
                        PetID = reader.GetInt32("petID"),
                        DateGiven = reader.GetDateTime("dateGiven").ToString("yyyy-MM-dd"),
                        NextDueDate = reader.IsDBNull(reader.GetOrdinal("nextDueDate"))
                            ? "" : reader.GetDateTime("nextDueDate").ToString("yyyy-MM-dd"),
                        Notes = SafeString(reader, "notes"),
                        PetName = SafeString(reader, "petName"),
                        OwnerName = SafeString(reader, "ownerName")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Vaccinations table: " + ex.Message + "\n\nRun Backend/schema_extensions.sql in MySQL if needed.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return list;
        }

        public bool AddVaccination(int petId, DateTime dateGiven, DateTime? nextDue, string? notes)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                bool hasNotes = ColumnExists(conn, "vaccinations", "notes");
                string sql = hasNotes
                    ? "INSERT INTO vaccinations (petID, dateGiven, nextDueDate, notes) VALUES (@p, @d, @n, @notes)"
                    : "INSERT INTO vaccinations (petID, dateGiven, nextDueDate) VALUES (@p, @d, @n)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@p", petId);
                cmd.Parameters.AddWithValue("@d", dateGiven.Date);
                cmd.Parameters.AddWithValue("@n", nextDue.HasValue ? nextDue.Value.Date : (object)DBNull.Value);
                if (hasNotes)
                    cmd.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? DBNull.Value : notes.Trim());
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteVaccination(int id)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("DELETE FROM vaccinations WHERE vaccinationID = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public int GetTotalShots()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM vaccinations", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
        }

        public int GetDueSoonCount(int withinDays = 30)
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                string q = @"SELECT COUNT(*) FROM vaccinations
                    WHERE nextDueDate IS NOT NULL
                      AND nextDueDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL @d DAY)";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@d", withinDays);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
        }

        public int GetOverdueCount()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                using var cmd = new MySqlCommand(
                    "SELECT COUNT(*) FROM vaccinations WHERE nextDueDate IS NOT NULL AND nextDueDate < CURDATE()", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
        }

        /// <summary>Shots recorded per calendar month for chart (Jan..Dec).</summary>
        public int[] GetShotsPerMonthForYear(int year)
        {
            var arr = new int[12];
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                string q = @"SELECT MONTH(dateGiven) AS m, COUNT(*) AS c FROM vaccinations
                    WHERE YEAR(dateGiven) = @y GROUP BY MONTH(dateGiven)";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@y", year);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                {
                    int m = r.GetInt32("m");
                    if (m >= 1 && m <= 12) arr[m - 1] = Convert.ToInt32(r["c"]);
                }
            }
            catch { }
            return arr;
        }

        public (int vaccinatedPets, int totalPets) GetVaccinationCoverage()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                int total = Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM pets", conn).ExecuteScalar());
                int vac = Convert.ToInt32(new MySqlCommand(
                    "SELECT COUNT(DISTINCT petID) FROM vaccinations", conn).ExecuteScalar());
                return (vac, total);
            }
            catch { return (0, 0); }
        }

        /// <summary>Latest shot per pet (by max vaccinationID) for dashboard rows.</summary>
        public Dictionary<int, (string lastDate, string status)> GetLatestVaccinationSummaryByPetIds(IEnumerable<int> petIds)
        {
            var result = new Dictionary<int, (string lastDate, string status)>();
            var ids = petIds?.Distinct().Where(id => id > 0).ToList() ?? new List<int>();
            if (ids.Count == 0) return result;

            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                var prms = new List<string>();
                for (int i = 0; i < ids.Count; i++)
                    prms.Add("@p" + i);

                string sql = $@"
                    SELECT v.petID, v.dateGiven, v.nextDueDate
                    FROM vaccinations v
                    INNER JOIN (
                        SELECT petID, MAX(vaccinationID) AS maxId
                        FROM vaccinations
                        WHERE petID IN ({string.Join(",", prms)})
                        GROUP BY petID
                    ) t ON t.petID = v.petID AND t.maxId = v.vaccinationID";

                using var cmd = new MySqlCommand(sql, conn);
                for (int i = 0; i < ids.Count; i++)
                    cmd.Parameters.AddWithValue("@p" + i, ids[i]);

                using var reader = cmd.ExecuteReader();
                var today = DateTime.Today;
                while (reader.Read())
                {
                    int pid = reader.GetInt32("petID");
                    var dg = reader.GetDateTime("dateGiven");
                    string last = dg.ToString("yyyy-MM-dd");
                    string status = "OK";
                    int nextOrd = reader.GetOrdinal("nextDueDate");
                    if (!reader.IsDBNull(nextOrd))
                    {
                        var nd = reader.GetDateTime(nextOrd);
                        if (nd < today) status = "Overdue";
                        else if (nd <= today.AddDays(30)) status = "Due soon";
                    }

                    result[pid] = (last, status);
                }
            }
            catch { }

            return result;
        }
    }
}
