-- Run this entire script in MySQL on your AlagaTrack database (e.g. USE alagatrack; then paste).
-- It upgrades older installs (adds missing columns) and creates any missing tables.

-- ========== 1) Upgrade existing `vaccinations` (older DBs may lack `notes` / `created_at`) ==========
SET @dbname = DATABASE();

SET @col_notes := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
  WHERE TABLE_SCHEMA = @dbname AND TABLE_NAME = 'vaccinations' AND COLUMN_NAME = 'notes'
);
SET @tbl_vacc := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
  WHERE TABLE_SCHEMA = @dbname AND TABLE_NAME = 'vaccinations'
);
SET @sql_notes := IF(@tbl_vacc > 0 AND @col_notes = 0,
  'ALTER TABLE vaccinations ADD COLUMN notes TEXT NULL',
  'SELECT 1');
PREPARE stmt_notes FROM @sql_notes;
EXECUTE stmt_notes;
DEALLOCATE PREPARE stmt_notes;

SET @col_ca := (
  SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
  WHERE TABLE_SCHEMA = @dbname AND TABLE_NAME = 'vaccinations' AND COLUMN_NAME = 'created_at'
);
SET @sql_ca := IF(@tbl_vacc > 0 AND @col_ca = 0,
  'ALTER TABLE vaccinations ADD COLUMN created_at TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP',
  'SELECT 1');
PREPARE stmt_ca FROM @sql_ca;
EXECUTE stmt_ca;
DEALLOCATE PREPARE stmt_ca;

-- ========== 2) Create tables when missing (new installs or missing extensions) ==========

CREATE TABLE IF NOT EXISTS vaccinations (
  vaccinationID INT AUTO_INCREMENT PRIMARY KEY,
  petID INT NOT NULL,
  dateGiven DATE NOT NULL,
  nextDueDate DATE NULL,
  notes TEXT NULL,
  created_at TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT fk_vacc_pet FOREIGN KEY (petID) REFERENCES pets(petID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS lost_pet_reports (
  reportID INT AUTO_INCREMENT PRIMARY KEY,
  ownerName VARCHAR(120) NOT NULL,
  petName VARCHAR(120) NOT NULL,
  dateLost DATETIME NOT NULL,
  lastSeenLocation VARCHAR(255) NULL,
  description TEXT NULL,
  status VARCHAR(20) NOT NULL DEFAULT 'active',
  created_at TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS staff_members (
  staffID INT AUTO_INCREMENT PRIMARY KEY,
  username VARCHAR(80) NOT NULL UNIQUE,
  fullName VARCHAR(120) NOT NULL,
  role VARCHAR(40) NOT NULL DEFAULT 'Staff',
  phone VARCHAR(50) NULL,
  email VARCHAR(120) NULL,
  atPin VARCHAR(40) NULL,
  created_at TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP
);
