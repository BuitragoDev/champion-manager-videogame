using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public void EditarCompeticion(Competicion competicion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Construcción dinámica del SET
                    List<string> campos = new List<string>();
                    var cmd = new SQLiteCommand();
                    cmd.Connection = conn;

                    if (!string.IsNullOrWhiteSpace(competicion.Nombre))
                    {
                        campos.Add("nombre = @Nombre");
                        cmd.Parameters.AddWithValue("@Nombre", competicion.Nombre);
                    }

                    if (competicion.RutaImagen != "Recursos/img/logos_competiciones/")
                    {
                        campos.Add("ruta_imagen = @RutaImagen");
                        cmd.Parameters.AddWithValue("@RutaImagen", competicion.RutaImagen);
                    }

                    if (competicion.RutaImagen80 != "Recursos/img/logos_competiciones/80x80/")
                    {
                        campos.Add("ruta_imagen80 = @RutaImagen80");
                        cmd.Parameters.AddWithValue("@RutaImagen80", competicion.RutaImagen80);
                    }

                    if (campos.Count == 0)
                    {
                        return;
                    }

                    string query = $"UPDATE competiciones SET {string.Join(", ", campos)} WHERE id_competicion = @IdCompeticion";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@IdCompeticion", competicion.IdCompeticion);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar la competición: " + ex.Message);
            }
        }


        // ---------------------------------------------------------------- Método que muestra las 2 competiciones
        public List<Competicion> MostrarCompeticiones()
        {
            List<Competicion> oCompeticion = new List<Competicion>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * FROM competiciones WHERE id_competicion BETWEEN 1 AND 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oCompeticion.Add(new Competicion()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdCompeticion = dr["id_competicion"] != DBNull.Value ? Convert.ToInt32(dr["id_competicion"]) : 0,
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oCompeticion;
        }
    }
}
