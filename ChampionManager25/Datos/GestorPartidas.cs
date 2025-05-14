using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Datos
{
    public static class GestorPartidas
    {
        private static readonly string RutaPartidas = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ChampionsManager",
            "Partidas");
        public static string RutaMisDocumentos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ChampionsManager");
        public static string RutaRecursosUsuario = Path.Combine(RutaMisDocumentos, "Recursos", "img");

        // Método para crear una nueva partida
        public static string CrearNuevaPartida()
        {
            if (!Directory.Exists(RutaPartidas))
            {
                Directory.CreateDirectory(RutaPartidas);
            }

            string nombreArchivo = $"Partida_Temporal_{Guid.NewGuid()}.db";
            string rutaCompleta = Path.Combine(RutaPartidas, nombreArchivo);

            // Ruta de la base personalizada
            string rutaBasePersonalizada = Path.Combine(RutaMisDocumentos, "database", "basePersonalizada.db");

            // Ruta de la base original
            string rutaBaseOriginal = "championsManagerDB.db";

            // Determinar cuál base de datos usar
            string rutaBaseOrigen = File.Exists(rutaBasePersonalizada) ? rutaBasePersonalizada : rutaBaseOriginal;

            // Copiar la base elegida a la nueva ruta de partida
            File.Copy(rutaBaseOrigen, rutaCompleta);

            return rutaCompleta;
        }


        // Método para confirmar la partida (copiar el archivo temporal a la ubicación final)
        public static string ConfirmarPartida(string rutaTemporal)
        {
            // Validar si la ruta temporal es nula o vacía
            if (string.IsNullOrEmpty(rutaTemporal))
            {
                throw new ArgumentNullException(nameof(rutaTemporal), "La ruta de la partida temporal no puede ser nula o vacía.");
            }

            // Cerrar todas las conexiones antes de realizar cualquier operación con archivos
            Conexion.CerrarTodasLasConexiones();

            string nombreFinal = $"Partida_{DateTime.Now:yyyyMMdd_HHmmss}.db";
            string rutaFinal = Path.Combine(RutaPartidas, nombreFinal);

            // Verificar si el archivo está siendo usado por otro proceso
            if (IsFileLocked(rutaTemporal))
            {
                // Aquí cerramos el proceso que está bloqueando el archivo
                KillProcessUsingFile(rutaTemporal);
            }

            int intentos = 0;
            while (intentos < 3)
            {
                try
                {
                    // Si el archivo final ya existe, eliminarlo
                    if (File.Exists(rutaFinal))
                    {
                        File.Delete(rutaFinal);
                    }

                    // Copiar el archivo temporal a la nueva ubicación
                    File.Copy(rutaTemporal, rutaFinal, true);

                    // Eliminar el archivo temporal
                    File.Delete(rutaTemporal);

                    return rutaFinal; // Devuelve la ruta final de la partida confirmada
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Error al intentar copiar el archivo {rutaTemporal} a {rutaFinal}: {ex.Message}");
                    intentos++;
                    System.Threading.Thread.Sleep(3000 * intentos); // Espera más tiempo entre intentos
                }
            }

            throw new IOException("No se pudo confirmar la partida después de varios intentos");
        }

        // Método para comprobar si un archivo está bloqueado por otro proceso
        private static bool IsFileLocked(string filePath)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // El archivo está bloqueado por otro proceso
                return true;
            }
            finally
            {
                fs?.Close();
            }
            return false;
        }

        // Método para obtener todas las partidas guardadas (sin archivos temporales)
        public static List<string> ObtenerPartidasGuardadas()
        {
            if (!Directory.Exists(RutaPartidas))
                return new List<string>();

            return Directory.GetFiles(RutaPartidas, "Partida_*.db")
                           .Where(f => !f.Contains("_Temporal_"))
                           .ToList();
        }

        // Método para matar el proceso que tiene abierto el archivo
        private static void KillProcessUsingFile(string filePath)
        {
            // Obtener todos los procesos en ejecución
            var processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                try
                {
                    // Intentar acceder a los módulos del proceso
                    foreach (ProcessModule module in process.Modules)
                    {
                        // Verificar si el módulo corresponde al archivo bloqueado
                        if (module.FileName.Equals(filePath, StringComparison.InvariantCultureIgnoreCase))
                        {
                            Console.WriteLine($"El proceso {process.ProcessName} con ID {process.Id} está utilizando el archivo.");

                            // Matar el proceso (esto es arriesgado)
                            process.Kill();
                            return; // Detener la búsqueda después de matar el proceso
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Puede lanzar una excepción si no tienes acceso al proceso o módulo
                    Console.WriteLine($"No se pudo acceder al proceso {process.ProcessName}: {ex.Message}");
                }
            }
        }

        public static void CopiarRecursosSiNoExiste()
        {
            try
            {
                // Verificar si la carpeta "ChampionsManager" existe en "Mis Documentos"
                if (!Directory.Exists(RutaMisDocumentos))
                {
                    Directory.CreateDirectory(RutaMisDocumentos);  // Crear la carpeta ChampionsManager
                }

                // Verificar si la carpeta "Recursos" existe dentro de "ChampionsManager"
                if (!Directory.Exists(RutaRecursosUsuario))
                {
                    // Copiar la carpeta "Recursos" desde la ubicación original (suponiendo que está en la carpeta bin del proyecto)
                    string rutaRecursosOriginal = Path.Combine(Directory.GetCurrentDirectory(), "Recursos", "img");

                    // Verificar que la carpeta original "Recursos/img" exista
                    if (Directory.Exists(rutaRecursosOriginal))
                    {
                        CopiarDirectorio(rutaRecursosOriginal, RutaRecursosUsuario);
                    }
                    else
                    {
                        MessageBox.Show("No se encuentra la carpeta 'Recursos' original.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al copiar la carpeta de recursos: " + ex.Message);
            }
        }

        // Método para copiar recursivamente los archivos y subcarpetas de una carpeta a otra
        private static void CopiarDirectorio(string rutaOrigen, string rutaDestino)
        {
            // Crear la carpeta de destino si no existe
            Directory.CreateDirectory(rutaDestino);

            // Copiar los archivos de la carpeta origen a la destino
            foreach (string archivo in Directory.GetFiles(rutaOrigen))
            {
                string archivoDestino = Path.Combine(rutaDestino, Path.GetFileName(archivo));
                File.Copy(archivo, archivoDestino, true);  // true = sobreescribir
            }

            // Copiar las subcarpetas de la carpeta origen a la destino
            foreach (string subdirectorio in Directory.GetDirectories(rutaOrigen))
            {
                string subdirectorioDestino = Path.Combine(rutaDestino, Path.GetFileName(subdirectorio));
                CopiarDirectorio(subdirectorio, subdirectorioDestino);
            }
        }
    }
}
