using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                            h.partidosGanados, h.partidosEmpatados, h.partidosPerdidos, h.golesMarcados, h.golesRecibidos, h.titulosNacionales, h.titulosInternacionales,
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
                                    TitulosNacionales = dr.IsDBNull(11) ? 0 : dr.GetInt32(11),
                                    TitulosInternacionales = dr.IsDBNull(12) ? 0 : dr.GetInt32(12),
                                    CDirectiva = dr.IsDBNull(14) ? 0 : dr.GetInt32(14),
                                    CFans = dr.IsDBNull(15) ? 0 : dr.GetInt32(15),
                                    CJugadores = dr.IsDBNull(16) ? 0 : dr.GetInt32(16),
                                    NombreEquipo = dr.IsDBNull(dr.GetOrdinal("nombre_equipo")) ? string.Empty : dr.GetString(dr.GetOrdinal("nombre_equipo"))
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
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
