using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Datos
{
    public class HistorialDatos : Conexion
    {
        // ===================================================================== Método para Mostrar el Historial del Mánager
        public List<Historial> MostrarHistorialManager(int manager)
        {
            List<Historial> listadoHistorial = new List<Historial>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT h.id_historial, h.id_equipo, h.id_manager, h.temporada, h.posicionLiga, h.partidosJugados, 
                                            h.partidosGanados, h.partidosEmpatados, h.partidosPerdidos, h.golesMarcados, h.golesRecibidos, h.titulosInternacionales,
                                            e.nombre AS nombre_equipo, h.cDirectiva, h.cFans, h.cJugadores
                                     FROM historial_manager h
                                     JOIN equipos e 
                                         ON h.id_equipo = e.id_equipo
                                     WHERE id_manager = @IdManager
                                     ORDER BY id_historial ASC";


                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", manager);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoHistorial.Add(new Historial()
                                {
                                    IdHistorial = dr.GetInt32(0),
                                    IdEquipo = dr.GetInt32(1),
                                    IdManager = dr.GetInt32(2),
                                    Temporada = dr.GetString(3),
                                    PosicionLiga = dr.IsDBNull(4) ? 0 : dr.GetInt32(4),
                                    PartidosJugados = dr.IsDBNull(5) ? 0 : dr.GetInt32(5),
                                    PartidosGanados = dr.IsDBNull(6) ? 0 : dr.GetInt32(6),
                                    PartidosEmpatados = dr.IsDBNull(7) ? 0 : dr.GetInt32(7),
                                    PartidosPerdidos = dr.IsDBNull(8) ? 0 : dr.GetInt32(8),
                                    GolesMarcados = dr.IsDBNull(9) ? 0 : dr.GetInt32(9),
                                    GolesRecibidos = dr.IsDBNull(10) ? 0 : dr.GetInt32(10),
                                    TitulosInternacionales = dr.IsDBNull(11) ? 0 : dr.GetInt32(11),
                                    CDirectiva = dr.IsDBNull(13) ? 0 : dr.GetInt32(13),
                                    CFans = dr.IsDBNull(14) ? 0 : dr.GetInt32(14),
                                    CJugadores = dr.IsDBNull(15) ? 0 : dr.GetInt32(15),
                                    NombreEquipo = dr.IsDBNull(dr.GetOrdinal("nombre_equipo")) ? string.Empty : dr.GetString(dr.GetOrdinal("nombre_equipo"))
                                });

                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return listadoHistorial;
        }

        // ===================================================================== Método para insertar un nuevo Historial de Mánager vacío
        public void CrearLineaHistorial(int manager, int equipo, string temporada)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta SQL para insertar el partido en la tabla "partidos"
                    string query = @"INSERT INTO historial_manager (id_equipo, id_manager, temporada) 
                                     VALUES (@IdEquipo, @idManager, @Temporada)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros para la consulta SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@idManager", manager);
                        cmd.Parameters.AddWithValue("@Temporada", temporada);

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para copiar los partidos y goles del historial temporal
        public void CopiarPartidosHistorialManager(int temporada)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Construir la cadena para la temporada
                    string temporadaStr = $"{temporada}/{temporada + 1}";

                    // Comando para insertar los datos de historial_manager_temp en historial_manager
                    string queryInsert = @"UPDATE historial_manager 
                                           SET 
                                                partidosJugados = hmt.partidosJugados,
                                                partidosGanados = hmt.partidosGanados,
                                                partidosEmpatados = hmt.partidosEmpatados,
                                                partidosPerdidos = hmt.partidosPerdidos,
                                                golesMarcados = hmt.golesMarcados,
                                                golesRecibidos = hmt.golesRecibidos
                                           FROM historial_manager_temp hmt
                                           WHERE historial_manager.temporada = @temporada";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryInsert, conn))
                    {
                        // Agregar el parámetro temporada
                        cmd.Parameters.AddWithValue("@temporada", temporadaStr); 

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para copiar los datos de las confianzas
        public void CopiarConfianzasManager(int temporada)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Construir la cadena para la temporada
                    string temporadaStr = $"{temporada}/{temporada + 1}";

                    // Comando para insertar los datos de managers en historial_manager
                    string queryInsert = @"UPDATE historial_manager 
                                           SET cDirectiva = m.cDirectiva,
                                               cFans = m.cFans,
                                               cJugadores = m.cJugadores
                                           FROM managers m
                                           WHERE historial_manager.temporada = @temporada";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryInsert, conn))
                    {
                        // Agregar el parámetro temporada
                        cmd.Parameters.AddWithValue("@temporada", temporadaStr);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para actualizar la posicion final en la Liga
        public void CopiarPosicionLigaManager(int temporada, int posicion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Construir la cadena para la temporada
                    string temporadaStr = $"{temporada}/{temporada + 1}";

                    // Comando para insertar los datos de managers en historial_manager
                    string queryInsert = @"UPDATE historial_manager 
                                           SET posicionLiga = @Posicion
                                           WHERE temporada = @temporada";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@Posicion", posicion);
                        cmd.Parameters.AddWithValue("@temporada", temporadaStr);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para resetear el historial temporal
        public void ResetearHistorialTemporal()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Comando para insertar los datos de managers en historial_manager
                    string queryInsert = @"UPDATE historial_manager_temp
                                           SET posicionLiga = 0,
                                               partidosJugados = 0,
                                               partidosGanados = 0,
                                               partidosEmpatados = 0,
                                               partidosPerdidos = 0,
                                               golesMarcados = 0,
                                               golesRecibidos = 0,
                                               titulosInternacionales = 0,
                                               cDirectiva = 0,
                                               cFans = 0,
                                               cJugadores = 0
                                           WHERE id_historial = 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(queryInsert, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
