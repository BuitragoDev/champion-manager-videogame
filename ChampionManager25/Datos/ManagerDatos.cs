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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    bool tieneImagen = manager.RutaImagen != "Recursos/img/managers/";

                    string columnas = @"nombre, apellido, nacionalidad, fechaNacimiento, 
                                cDirectiva, cFans, cJugadores, partidosJugados, partidosGanados, 
                                partidosEmpatados, partidosPerdidos, reputacion, puntos, tactica, despedido";

                    string valores = @"@Nombre, @Apellido, @Nacionalidad, @FechaNacimiento, 
                               50, 50, 50, 0, 0, 0, 0, 0, 0, @Tactica, 0";

                    if (tieneImagen)
                    {
                        columnas += ", ruta_imagen";
                        valores += ", @RutaImagen";
                    }

                    string query = $"INSERT INTO managers ({columnas}) VALUES ({valores})";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", manager.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", manager.Apellido);
                        cmd.Parameters.AddWithValue("@Nacionalidad", manager.Nacionalidad);
                        cmd.Parameters.AddWithValue("@FechaNacimiento", manager.FechaNacimiento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@Tactica", "4-4-2");

                        if (tieneImagen)
                            cmd.Parameters.AddWithValue("@RutaImagen", manager.RutaImagen);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            string idQuery = "SELECT last_insert_rowid()";
                            using (var idCmd = new SQLiteCommand(idQuery, conn))
                            {
                                object idObj = idCmd.ExecuteScalar();
                                return idObj != null ? Convert.ToInt32(idObj) : 0;
                            }
                        }

                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el Manager: " + ex.Message);
                return -1;
            }
        }


        // Método que elimina al manager con el ID recibido por parámetro.
        public void eliminarManager(int idManager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // ---------------------------------------------------------------- Método que actualiza los puntos de Directiva, Fans y Jugadores.
        public void ActualizarConfianza(int idManager, int directiva, int fans, int jugadores)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // ---------------------------------------------------------------- Método que actualiza la reputacion del Manager
        public void ActualizarReputacion(int idManager, int valor)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET " +
                                   "reputacion = MAX(reputacion + @Reputacion, 0) " +
                                   "WHERE id_manager = @IdManager;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Reputacion", valor); 
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                                    Despedido = reader.GetInt32(reader.GetOrdinal("despedido")),
                                    RutaImagen = reader["ruta_imagen"]?.ToString() ?? string.Empty
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

        // Método que cambia la tactica del manager
        public void CambiarTactica(int idManager, string tactica)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET tactica = @Tactica WHERE id_manager = @IdManager";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tactica", tactica); // Parámetro para el cDirectiva
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

        // ---------------------------------------------------------------- Método que actualiza los partidos de un manager
        public void ActualizarResultadoManager(int idManager, int jugados, int ganados, int empatados, int perdidos, int puntos)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET " +
                                   "partidosJugados = partidosJugados + @PartidosJugados, " +
                                   "partidosGanados = partidosGanados + @PartidosGanados, " +
                                   "partidosEmpatados = partidosEmpatados + @PartidosEmpatados, " +
                                   "partidosPerdidos = partidosPerdidos + @PartidosPerdidos, " +
                                   "puntos = puntos + @Puntos " +
                                   "WHERE id_manager = @IdManager;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PartidosJugados", jugados);
                        cmd.Parameters.AddWithValue("@PartidosGanados", ganados); 
                        cmd.Parameters.AddWithValue("@PartidosEmpatados", empatados);
                        cmd.Parameters.AddWithValue("@PartidosPerdidos", perdidos);
                        cmd.Parameters.AddWithValue("@Puntos", puntos);
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

        // ---------------------------------------------------------------- Método que actualiza la tabla historial_manager_temp
        public void ActualizarManagerTemporal(Historial historial)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE historial_manager_temp SET " +
                                   "partidosJugados = partidosJugados + @PartidosJugados, " +
                                   "partidosGanados = partidosGanados + @PartidosGanados, " +
                                   "partidosEmpatados = partidosEmpatados + @PartidosEmpatados, " +
                                   "partidosPerdidos = partidosPerdidos + @PartidosPerdidos, " +
                                   "golesMarcados = golesMarcados + @GolesMarcados, " +
                                   "golesRecibidos = golesRecibidos + @GolesRecibidos " +
                                   "WHERE id_historial = @IdHistorial;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdHistorial", 1);
                        cmd.Parameters.AddWithValue("@PartidosJugados", historial.PartidosJugados);
                        cmd.Parameters.AddWithValue("@PartidosGanados", historial.PartidosGanados);
                        cmd.Parameters.AddWithValue("@PartidosEmpatados", historial.PartidosEmpatados);
                        cmd.Parameters.AddWithValue("@PartidosPerdidos", historial.PartidosPerdidos);
                        cmd.Parameters.AddWithValue("@GolesMarcados", historial.GolesMarcados);
                        cmd.Parameters.AddWithValue("@GolesRecibidos", historial.GolesRecibidos);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el Manager: " + ex.Message);
            }
        }

        // ---------------------------------------------------------------- Método que despide un manager
        public void DespedirManager(int idManager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE managers SET despedido = 1 WHERE id_manager = @IdManager";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el Manager: " + ex.Message);
            }
        }
    }
}
