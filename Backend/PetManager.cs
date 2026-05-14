using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AlagaTrack
{
    public class PetManager
    {
        private static string SafeString(MySqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index) ? string.Empty : reader.GetString(index);
        }

        public List<PetRecord> GetPets(string? search = null)
        {
            var pets = new List<PetRecord>();
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT p.petID,
                               COALESCE(p.petName, '') AS petName,
                               COALESCE(p.species, '') AS species,
                               COALESCE(p.breed, '') AS breed,
                               COALESCE(p.color, '') AS color,
                               COALESCE(p.age, 0) AS age,
                               COALESCE(p.petsPhoto, '') AS petsPhoto,
                               COALESCE(o.name, '') AS ownerName,
                               COALESCE(o.contactNumber, '') AS ownerContact,
                               COALESCE(p.ownerID, 0) AS ownerID
                        FROM pets p
                        LEFT JOIN owners o ON o.ownerID = p.ownerID
                        WHERE @search IS NULL
                           OR p.petName LIKE CONCAT('%', @search, '%')
                           OR o.name LIKE CONCAT('%', @search, '%')
                           OR p.breed LIKE CONCAT('%', @search, '%')
                        ORDER BY p.petID DESC";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", string.IsNullOrWhiteSpace(search) ? DBNull.Value : search.Trim());
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pets.Add(new PetRecord
                                {
                                    PetID = reader.GetInt32("petID"),
                                    PetName = SafeString(reader, "petName"),
                                    Species = SafeString(reader, "species"),
                                    Breed = SafeString(reader, "breed"),
                                    Color = SafeString(reader, "color"),
                                    Age = Convert.ToInt32(reader["age"]),
                                    PetsPhoto = SafeString(reader, "petsPhoto"),
                                    OwnerName = SafeString(reader, "ownerName"),
                                    OwnerContact = SafeString(reader, "ownerContact"),
                                    OwnerID = Convert.ToInt32(reader["ownerID"])
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

            return pets;
        }

        public bool AddPet(PetRecord pet, out int petId)
        {
            petId = 0;
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    int ownerId = ResolveOwnerId(conn, pet.OwnerName, pet.OwnerContact);
                    if (ownerId <= 0)
                    {
                        MessageBox.Show("Owner not found in database. Please refresh and select a valid owner.", "Owner Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    const string query = @"
                        INSERT INTO pets (petName, species, breed, color, age, ownerID, petsPhoto)
                        VALUES (@petName, @species, @breed, @color, @age, @ownerID, @petsPhoto);
                        SELECT LAST_INSERT_ID();";

                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@petName", pet.PetName);
                        cmd.Parameters.AddWithValue("@species", pet.Species);
                        cmd.Parameters.AddWithValue("@breed", string.IsNullOrWhiteSpace(pet.Breed) ? DBNull.Value : pet.Breed);
                        cmd.Parameters.AddWithValue("@color", string.IsNullOrWhiteSpace(pet.Color) ? DBNull.Value : pet.Color);
                        cmd.Parameters.AddWithValue("@age", pet.Age);
                        cmd.Parameters.AddWithValue("@ownerID", ownerId);
                        cmd.Parameters.AddWithValue("@petsPhoto", string.IsNullOrWhiteSpace(pet.PetsPhoto) ? "default_pet.png" : pet.PetsPhoto);
                        petId = Convert.ToInt32(cmd.ExecuteScalar());
                        return petId > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeletePet(int petId)
        {
            try
            {
                using (var conn = DatabaseConfig.GetConnection())
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("DELETE FROM pets WHERE petID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", petId);
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

        /// <summary>Owners with their pets for vaccination / lost-pet combo dialogs.</summary>
        public List<OwnerPetGroup> GetOwnerPetGroups()
        {
            var map = new Dictionary<int, OwnerPetGroup>();
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                string q = @"
                    SELECT o.ownerID, o.name, o.contactNumber, p.petID, p.petName
                    FROM owners o
                    LEFT JOIN pets p ON p.ownerID = o.ownerID
                    ORDER BY o.ownerID DESC, p.petID";
                using var cmd = new MySqlCommand(q, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int oid = reader.GetInt32("ownerID");
                    if (!map.TryGetValue(oid, out var grp))
                    {
                        string name = reader.IsDBNull(reader.GetOrdinal("name")) ? "" : reader.GetString("name");
                        string contact = reader.IsDBNull(reader.GetOrdinal("contactNumber")) ? "" : reader.GetString("contactNumber");
                        grp = new OwnerPetGroup
                        {
                            DisplayLabel = $"{name}  {contact}".Trim(),
                            OwnerId = oid,
                            Pets = new List<(int, string)>()
                        };
                        map[oid] = grp;
                    }
                    int petOrdinal = reader.GetOrdinal("petID");
                    if (!reader.IsDBNull(petOrdinal))
                    {
                        int pid = reader.GetInt32("petID");
                        string pnm = reader.IsDBNull(reader.GetOrdinal("petName")) ? "" : reader.GetString("petName");
                        grp.Pets.Add((pid, pnm));
                    }
                }
            }
            catch { }
            return new List<OwnerPetGroup>(map.Values);
        }

        public int GetTotalPetCount()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                return Convert.ToInt32(new MySqlCommand("SELECT COUNT(*) FROM pets", conn).ExecuteScalar());
            }
            catch { return 0; }
        }

        public int GetPetsRegisteredTodayCount()
        {
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                if (!ColumnExists(conn, "pets", "created_at"))
                    return 0;
                using var cmd = new MySqlCommand("SELECT COUNT(*) FROM pets WHERE DATE(created_at) = CURDATE()", conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch { return 0; }
        }

        public List<PetRecord> GetRecentPets(int take)
        {
            var list = new List<PetRecord>();
            try
            {
                using var conn = DatabaseConfig.GetConnection();
                conn.Open();
                string q = @"
                    SELECT p.petID,
                           COALESCE(p.petName, '') AS petName,
                           COALESCE(p.species, '') AS species,
                           COALESCE(p.breed, '') AS breed,
                           COALESCE(p.color, '') AS color,
                           COALESCE(p.age, 0) AS age,
                           COALESCE(p.petsPhoto, '') AS petsPhoto,
                           COALESCE(o.name, '') AS ownerName,
                           COALESCE(o.contactNumber, '') AS ownerContact,
                           COALESCE(p.ownerID, 0) AS ownerID
                    FROM pets p
                    LEFT JOIN owners o ON o.ownerID = p.ownerID
                    ORDER BY p.petID DESC
                    LIMIT @take";
                using var cmd = new MySqlCommand(q, conn);
                cmd.Parameters.AddWithValue("@take", take);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new PetRecord
                    {
                        PetID = reader.GetInt32("petID"),
                        PetName = SafeString(reader, "petName"),
                        Species = SafeString(reader, "species"),
                        Breed = SafeString(reader, "breed"),
                        Color = SafeString(reader, "color"),
                        Age = Convert.ToInt32(reader["age"]),
                        PetsPhoto = SafeString(reader, "petsPhoto"),
                        OwnerName = SafeString(reader, "ownerName"),
                        OwnerContact = SafeString(reader, "ownerContact"),
                        OwnerID = Convert.ToInt32(reader["ownerID"])
                    });
                }
            }
            catch { }
            return list;
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

        private static int ResolveOwnerId(MySqlConnection conn, string ownerName, string ownerContact)
        {
            const string query = @"
                SELECT ownerID
                FROM owners
                WHERE name = @name AND contactNumber = @contact
                ORDER BY ownerID DESC
                LIMIT 1";

            using (var cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@name", ownerName);
                cmd.Parameters.AddWithValue("@contact", ownerContact);
                var result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }
    }
}
