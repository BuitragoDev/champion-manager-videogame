using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class PalmaresDatos : Conexion
    {
        // ===================================================================== Método para Mostrar Número de trofeos de un equipo por competición
        public int numTitulosEquipoCompeticion(int equipo, int competicion)
        {
            int titulos = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT titulos FROM palmares
                                     WHERE id_equipo = @IdEquipo AND id_competicion = @IdCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                titulos = dr.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return titulos;
        }

        // ===================================================================== Método para Mostrar el Palmarés del Manager
        public List<PalmaresManager> MostrarPalmaresManager(int equipo, int manager)
        {
            List<PalmaresManager> listadoPalmares = new List<PalmaresManager>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT id_palmares, id_competicion, id_equipo, id_manager, temporada
                                     FROM palmares_manager
                                     WHERE id_equipo = @IdEquipo AND id_manager = @IdManager
                                     ORDER BY id_palmares DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", manager);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoPalmares.Add(new PalmaresManager()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    IdManager = dr["id_manager"] != DBNull.Value ? Convert.ToInt32(dr["id_manager"]) : 0,
                                    IdCompeticion = dr["id_competicion"] != DBNull.Value ? Convert.ToInt32(dr["id_competicion"]) : 0,
                                    Temporada = dr["temporada"]?.ToString() ?? string.Empty
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

            return listadoPalmares;
        }

        // ===================================================================== Método para Mostrar el Palmarés Completo
        public List<Palmares> MostrarPalmaresCompleto()
        {
            List<Palmares> listadoPalmares = new List<Palmares>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_equipo, p.titulos, e.nombre AS NombreEquipo
                                     FROM palmares p
                                     JOIN equipos e ON e.id_equipo = p.id_equipo
                                     ORDER BY titulos DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoPalmares.Add(new Palmares()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Titulos = dr["titulos"] != DBNull.Value ? Convert.ToInt32(dr["titulos"]) : 0,
                                    NombreEquipo = dr.GetString(dr.GetOrdinal("NombreEquipo"))
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

            return listadoPalmares;
        }

        // ===================================================================== Método para Mostrar el Historial de las Finales
        public List<HistorialFinales> MostrarHistorialFinales()
        {
            List<HistorialFinales> listadoHistorial = new List<HistorialFinales>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT h.id_historial,
                                        h.temporada, 
                                        h.id_equipo_campeon,
                                        h.id_equipo_finalista,
                                        e1.nombre AS equipo_campeon, 
                                        e2.nombre AS equipo_finalista
                                     FROM historial_finales h
                                     JOIN equipos e1 ON h.id_equipo_campeon = e1.id_equipo
                                     JOIN equipos e2 ON h.id_equipo_finalista = e2.id_equipo
                                     ORDER BY h.id_historial DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoHistorial.Add(new HistorialFinales()
                                {
                                    Temporada = dr["temporada"]?.ToString() ?? string.Empty,
                                    IdEquipoCampeon = dr["id_equipo_campeon"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo_campeon"]) : 0,
                                    IdEquipoFinalista = dr["id_equipo_finalista"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo_finalista"]) : 0,
                                    NombreEquipoCampeon = dr["equipo_campeon"]?.ToString() ?? string.Empty,
                                    NombreEquipoFinalista = dr["equipo_finalista"]?.ToString() ?? string.Empty
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al manager si gana la Liga
        public void AnadirTituloManager(int competicion, int equipo, int manager, int temporada)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string temporadaFormateada = $"{temporada}/{(temporada + 1) % 100:D2}";
                    string query = @"INSERT INTO palmares_manager (id_competicion, id_equipo, id_manager, temporada)
                                     VALUES (@IdCompeticion, @IdEquipo, @IdManager, @Temporada)";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdCompeticion", competicion);
                        command.Parameters.AddWithValue("@IdEquipo", equipo);
                        command.Parameters.AddWithValue("@IdManager", manager);
                        command.Parameters.AddWithValue("@Temporada", temporadaFormateada);
                        command.ExecuteNonQuery(); // Ejecutar la consulta de inserción
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------- Metodo que suma un titulo al campeon de Liga
        public void AnadirTituloCampeon(int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
  
                    string query = @"UPDATE palmares SET titulos = titulos + 1 WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdEquipo", equipo);
                        command.ExecuteNonQuery(); // Ejecutar la consulta
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------- Metodo que agrega el campeon y subcampeon de una temporada
        public void AnadirCampeonFinalista(int temporada, int campeon, int finalista)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string temporadaFormateada = $"{temporada}-{(temporada + 1) % 100:D2}";
                    string query = @"INSERT INTO historial_finales (temporada, id_equipo_campeon, id_equipo_finalista)
                                     VALUES (@Temporada, @Campeon, @Finalista)";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Temporada", temporadaFormateada);
                        command.Parameters.AddWithValue("@Campeon", campeon);
                        command.Parameters.AddWithValue("@Finalista", finalista);
                        command.ExecuteNonQuery(); // Ejecutar la consulta de inserción
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
