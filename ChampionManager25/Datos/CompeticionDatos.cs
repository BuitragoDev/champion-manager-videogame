using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChampionManager25.Datos
{
    public class CompeticionDatos : Conexion
    {
        // ===================================================================== Método para Mostrar el Nombre de la Competición
        public string MostrarNombreCompeticion(int competicion)
        {
            string nombreComp = "";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT nombre, id_competicion
                                     FROM competiciones
                                     WHERE id_competicion = @IdCompeticion";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                nombreComp = reader["nombre"]?.ToString() ?? string.Empty;
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
            return nombreComp;
        }

        // --------------------------------------------------------------------- Método para Mostrar los datos de la competicion
        public Competicion ObtenerCompeticion(int idCompeticion)
        {
            Competicion competicion = null;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT nombre, id_competicion, ruta_imagen, ruta_imagen80
                                     FROM competiciones
                                     WHERE id_competicion = @IdCompeticion";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdCompeticion", idCompeticion);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                competicion = new Competicion()
                                {
                                    IdCompeticion = Convert.ToInt32(reader["id_competicion"]),
                                    Nombre = reader["nombre"]?.ToString() ?? string.Empty,
                                    RutaImagen = reader["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = reader["ruta_imagen80"]?.ToString() ?? string.Empty
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
            return competicion;
        }

        // ---------------------------------------------------------------- Método que cambia el nombre de la competicion
        public void CambiarNombreCompeticion(int idCompeticion, string nombre)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE competiciones SET nombre = @Nombre WHERE id_competicion = @IdCompeticion";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCompeticion", idCompeticion);
                        cmd.Parameters.AddWithValue("@Nombre", nombre);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Manager: " + ex.Message);
            }
        }
    }
}
