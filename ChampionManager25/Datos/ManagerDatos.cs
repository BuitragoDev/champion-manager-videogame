using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using ChampionManager25.Entidades;

namespace ChampionManager25.Datos
{
    public class ManagerDatos : Conexion
    {
        // Método que crea un nuevo Manager
        public int CrearNuevoManager(Manager manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO managers (nombre, apellido, nacionalidad, fechaNacimiento, 
                                     cDirectiva, cFans, cJugadores, partidosJugados, partidosGanados, partidosEmpatados, partidosPerdidos, reputacion, puntos, tactica) 
                                     VALUES (@Nombre, @Apellido, @Nacionalidad, @FechaNacimiento, 50, 50, 50, 0, 0, 0, 0, 0, 0, @Tactica)";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", manager.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", manager.Apellido);
                        cmd.Parameters.AddWithValue("@Nacionalidad", manager.Nacionalidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", manager.FechaNacimiento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Tactica", "4-4-2");
                        int result = cmd.ExecuteNonQuery(); // Ejecuta la consulta de inserción

                        if (result > 0) // Verifica si se afectó alguna fila
                        {
                            // Recuperar el último ID generado
                            string idQuery = "SELECT last_insert_rowid()";
                            using (var idCmd = new SQLiteCommand(idQuery, conn))
                            {
                                object idObj = idCmd.ExecuteScalar();
                                int idManager = idObj != null ? Convert.ToInt32(idObj) : 0;
                                return idManager;
                            }
                        }
                        else
                        {
                            return 0; // Indica fallo
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el Manager: " + ex.Message);
                return -1; // Devuelve -1 para errores
            }
        }

        // Método que elimina al manager con el ID recibido por parámetro.
        public void eliminarManager(int idManager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = "DELETE FROM managers WHERE id_manager = @IdManager";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", idManager); // Parámetro para el idManager
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el Manager: " + ex.Message);
            }
        }

        // Método que añade el equipo al mánager creado.
        public void AnadirEquipoSeleccionado(int idManager, int idEquipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET id_equipo = @IdEquipo WHERE id_manager = @IdManager";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", idEquipo); // Parámetro para el idEquipo
                        cmd.Parameters.AddWithValue("@IdManager", idManager); // Parámetro para el idManager
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // Método que actualiza los puntos de Directiva, Fans y Jugadores.
        public void ActualizarConfianza(int idManager, int directiva, int fans, int jugadores)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET " +
                                   "cDirectiva = cDirectiva + @Directiva, cFans = cFans + @Fans, cJugadores = cJugadores + @Jugadores " +
                                   "WHERE id_manager = @IdManager;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Directiva", directiva); // Parámetro para el cDirectiva
                        cmd.Parameters.AddWithValue("@Fans", fans); // Parámetro para el cFans
                        cmd.Parameters.AddWithValue("@Jugadores", jugadores); // Parámetro para el cJugadores
                        cmd.Parameters.AddWithValue("@IdManager", idManager); // Parámetro para el idManager
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el Manager: " + ex.Message);
            }
        }

        // Método que muestra los datos de un Manager
        public Manager MostrarManager(int idManager)
        {
            Manager coach = null; // Inicializamos la variable como null

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    string query = "SELECT * FROM managers WHERE id_manager = @IdManager";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                coach = new Manager
                                {
                                    IdManager = reader.GetInt32(reader.GetOrdinal("id_manager")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    Nacionalidad = reader.GetString(reader.GetOrdinal("nacionalidad")),
                                    FechaNacimiento = DateTime.Parse(reader.GetString(reader.GetOrdinal("fechaNacimiento"))),
                                    IdEquipo = reader.IsDBNull(reader.GetOrdinal("id_equipo")) ? 0 : reader.GetInt32(reader.GetOrdinal("id_equipo")),
                                    CDirectiva = reader.IsDBNull(reader.GetOrdinal("cDirectiva")) ? 0 : reader.GetInt32(reader.GetOrdinal("cDirectiva")),
                                    CFans = reader.IsDBNull(reader.GetOrdinal("cFans")) ? 0 : reader.GetInt32(reader.GetOrdinal("cFans")),
                                    CJugadores = reader.IsDBNull(reader.GetOrdinal("cJugadores")) ? 0 : reader.GetInt32(reader.GetOrdinal("cJugadores")),
                                    Reputacion = reader.IsDBNull(reader.GetOrdinal("reputacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("reputacion")),
                                    PartidosJugados = reader.IsDBNull(reader.GetOrdinal("partidosJugados")) ? 0 : reader.GetInt32(reader.GetOrdinal("partidosJugados")),
                                    PartidosGanados = reader.IsDBNull(reader.GetOrdinal("partidosGanados")) ? 0 : reader.GetInt32(reader.GetOrdinal("partidosGanados")),
                                    PartidosPerdidos = reader.IsDBNull(reader.GetOrdinal("partidosPerdidos")) ? 0 : reader.GetInt32(reader.GetOrdinal("partidosPerdidos")),
                                    Puntos = reader.IsDBNull(reader.GetOrdinal("puntos")) ? 0 : reader.GetInt32(reader.GetOrdinal("puntos")),
                                    Tactica = reader.GetString(reader.GetOrdinal("tactica")),
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener el Manager: " + ex.Message);
            }

            return coach; // Devuelve el objeto Manager o null si no se encontró
        }
    }
}
