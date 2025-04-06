using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Datos
{
    public static class SQLiteDatabaseManager
    {
        public static void CreateManagerDatabase(string managerId)
        {
            string originalDbPath = GetOriginalDbPath();
            string newDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"manager{managerId}.db");

            try
            {
                // 1. Verificar existencia de la base de datos original
                if (!File.Exists(originalDbPath))
                {
                    throw new FileNotFoundException($"Archivo original no encontrado: {originalDbPath}");
                }

                // 2. Eliminar copia previa si existe
                if (File.Exists(newDbPath))
                {
                    File.Delete(newDbPath);
                }

                // 3. Usar el método de copia de SQLite (el más fiable)
                SQLiteConnection.CreateFile(newDbPath);

                using (var source = new SQLiteConnection($"Data Source={originalDbPath};Version=3;"))
                using (var destination = new SQLiteConnection($"Data Source={newDbPath};Version=3;"))
                {
                    source.Open();
                    destination.Open();
                    source.BackupDatabase(destination, "main", "main", -1, null, 0);
                }

                // 4. Verificar la copia
                if (!VerifyDatabaseCopy(newDbPath))
                {
                    throw new Exception("La copia de la base de datos no es válida");
                }

                // 5. Actualizar configuración
                UpdateConnectionString(newDbPath);
            }
            catch (Exception ex)
            {
                // Manejo avanzado de errores
                HandleDatabaseError(ex, originalDbPath, newDbPath);
                throw;
            }
        }

        private static string GetOriginalDbPath()
        {
            // Buscar en varios lugares posibles
            string[] possiblePaths = {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "championsManagerDB.db"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\championsManagerDB.db"),
            Path.Combine(Environment.CurrentDirectory, "championsManagerDB.db")
        };

            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return Path.GetFullPath(path);
                }
            }

            throw new FileNotFoundException("No se pudo encontrar la base de datos original en ninguna ubicación conocida");
        }

        private static bool VerifyDatabaseCopy(string dbPath)
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type='table'", conn))
                    {
                        int tableCount = Convert.ToInt32(cmd.ExecuteScalar());
                        return tableCount > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private static void UpdateConnectionString(string dbPath)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.ConnectionStrings.ConnectionStrings["cadena"].ConnectionString = $"Data Source={dbPath};Version=3;";
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la cadena de conexión", ex);
            }
        }

        private static void HandleDatabaseError(Exception ex, string originalPath, string newPath)
        {
            string errorDetails = $@"Error al copiar base de datos:
                                        Original: {originalPath} ({(File.Exists(originalPath) ? "Existe" : "No existe")})
                                        Nueva: {newPath}
                                        Tamaño original: {(File.Exists(originalPath) ? new FileInfo(originalPath).Length + " bytes" : "N/A")}
                                        Error: {ex.Message}";

            MessageBox.Show(errorDetails, "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);

            // Opcional: Loggear el error en un archivo
            File.AppendAllText("database_errors.log", $"{DateTime.Now}: {errorDetails}\n\n");
        }
    }
}
