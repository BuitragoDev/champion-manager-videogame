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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // --------------------------------------------------------------- Método para Mostrar el Palmarés Completo de Copa Nacional
        public List<Palmares> MostrarPalmaresCompletoCopa()
        {
            List<Palmares> listadoPalmares = new List<Palmares>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_equipo, p.titulos, e.nombre AS NombreEquipo
                                     FROM palmaresCopa p
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

        // --------------------------------------------------------------- Método para Mostrar el Palmarés Completo de Copa Europa 1
        public List<Palmares> MostrarPalmaresCompletoCopaEuropa1()
        {
            List<Palmares> listadoPalmares = new List<Palmares>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_equipo, p.titulos, e.nombre AS NombreEquipo
                                     FROM palmaresCopaEuropa1 p
                                     JOIN equipos e ON e.id_equipo = p.id_equipo
                                     WHERE e.id_equipo <> 0
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

        // --------------------------------------------------------------- Método para Mostrar el Palmarés Completo de Copa Europa 2
        public List<Palmares> MostrarPalmaresCompletoCopaEuropa2()
        {
            List<Palmares> listadoPalmares = new List<Palmares>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_equipo, p.titulos, e.nombre AS NombreEquipo
                                     FROM palmaresCopaEuropa2 p
                                     JOIN equipos e ON e.id_equipo = p.id_equipo
                                     WHERE e.id_equipo <> 0
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

        // ===================================================================== Método para Mostrar el Palmarés del Balon de Oro
        public List<PalmaresJugador> MostrarPalmaresBalonOroTotal()
        {
            List<PalmaresJugador> listadoPalmares = new List<PalmaresJugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_jugador, 
                                            p.titulos, 
                                            j.nombre || ' ' || j.apellido AS nombreJugador
                                     FROM palmaresJugadores p
                                     JOIN jugadores j ON j.id_jugador = p.id_jugador
                                     ORDER BY titulos DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoPalmares.Add(new PalmaresJugador()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdJugador = dr["id_jugador"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador"]) : 0,
                                    Titulos = dr["titulos"] != DBNull.Value ? Convert.ToInt32(dr["titulos"]) : 0,
                                    NombreJugador = dr.GetString(dr.GetOrdinal("nombreJugador"))
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

        // ===================================================================== Método para Mostrar el Palmarés de la Bota de Oro
        public List<PalmaresJugador> MostrarPalmaresBotaOroTotal()
        {
            List<PalmaresJugador> listadoPalmares = new List<PalmaresJugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT p.id_jugador, 
                                            p.titulos, 
                                            j.nombre || ' ' || j.apellido AS nombreJugador
                                     FROM palmaresGoleadores p
                                     JOIN jugadores j ON j.id_jugador = p.id_jugador
                                     ORDER BY titulos DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoPalmares.Add(new PalmaresJugador()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdJugador = dr["id_jugador"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador"]) : 0,
                                    Titulos = dr["titulos"] != DBNull.Value ? Convert.ToInt32(dr["titulos"]) : 0,
                                    NombreJugador = dr.GetString(dr.GetOrdinal("nombreJugador"))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // ----------------------------------------------------------------- Método para Mostrar el Historial de las Finales de Copa Nacional
        public List<HistorialFinales> MostrarHistorialFinalesCopa()
        {
            List<HistorialFinales> listadoHistorial = new List<HistorialFinales>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT h.id_historial,
                                        h.temporada, 
                                        h.id_equipo_campeon,
                                        h.id_equipo_finalista,
                                        e1.nombre AS equipo_campeon, 
                                        e2.nombre AS equipo_finalista
                                     FROM historial_finalesCopa h
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

        // ----------------------------------------------------------------- Método para Mostrar el Historial de las Finales de Copa Europa 1
        public List<HistorialFinales> MostrarHistorialFinalesCopaEuropa1()
        {
            List<HistorialFinales> listadoHistorial = new List<HistorialFinales>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT h.id_historial,
                                        h.temporada, 
                                        h.id_equipo_campeon,
                                        h.id_equipo_finalista,
                                        e1.nombre AS equipo_campeon, 
                                        e2.nombre AS equipo_finalista
                                     FROM historial_finalesCopaEuropa1 h
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

        // ----------------------------------------------------------------- Método para Mostrar el Historial de las Finales de Copa Europa 2
        public List<HistorialFinales> MostrarHistorialFinalesCopaEuropa2()
        {
            List<HistorialFinales> listadoHistorial = new List<HistorialFinales>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT h.id_historial,
                                        h.temporada, 
                                        h.id_equipo_campeon,
                                        h.id_equipo_finalista,
                                        e1.nombre AS equipo_campeon, 
                                        e2.nombre AS equipo_finalista
                                     FROM historial_finalesCopaEuropa2 h
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // ===================================================================== Método para Mostrar el Historial del Balon de Oro
        public List<HistorialJugador> MostrarPalmaresBalonOro()
        {
            List<HistorialJugador> listadoHistorial = new List<HistorialJugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        h.temporada,
                                        h.id_jugador_oro,
                                        h.id_jugador_plata,
                                        h.id_jugador_bronce,
                                        oro.nombre || ' ' || oro.apellido AS jugador_oro,
                                        plata.nombre || ' ' || plata.apellido AS jugador_plata,
                                        bronce.nombre || ' ' || bronce.apellido AS jugador_bronce
                                     FROM 
                                        historial_mejorJugador h
                                     JOIN 
                                        jugadores oro ON h.id_jugador_oro = oro.id_jugador
                                     JOIN 
                                        jugadores plata ON h.id_jugador_plata = plata.id_jugador
                                     JOIN 
                                        jugadores bronce ON h.id_jugador_bronce = bronce.id_jugador
                                     ORDER BY 
                                        h.temporada DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoHistorial.Add(new HistorialJugador()
                                {
                                    Temporada = dr["temporada"]?.ToString() ?? string.Empty,
                                    IdJugadorOro = dr["id_jugador_oro"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_oro"]) : 0,
                                    IdJugadorPlata = dr["id_jugador_plata"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_plata"]) : 0,
                                    IdJugadorBronce = dr["id_jugador_bronce"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_bronce"]) : 0,
                                    NombreJugadorOro = dr["jugador_oro"]?.ToString() ?? string.Empty,
                                    NombreJugadorPlata = dr["jugador_plata"]?.ToString() ?? string.Empty,
                                    NombreJugadorBronce = dr["jugador_bronce"]?.ToString() ?? string.Empty
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

        // ===================================================================== Método para Mostrar el Historial de la Bota de Oro
        public List<HistorialJugador> MostrarPalmaresBotaOro()
        {
            List<HistorialJugador> listadoHistorial = new List<HistorialJugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        h.temporada,
                                        h.id_jugador_oro,
                                        h.id_jugador_plata,
                                        h.id_jugador_bronce,
                                        oro.nombre || ' ' || oro.apellido AS jugador_oro,
                                        plata.nombre || ' ' || plata.apellido AS jugador_plata,
                                        bronce.nombre || ' ' || bronce.apellido AS jugador_bronce
                                     FROM 
                                        historial_maximoGoleador h
                                     JOIN 
                                        jugadores oro ON h.id_jugador_oro = oro.id_jugador
                                     JOIN 
                                        jugadores plata ON h.id_jugador_plata = plata.id_jugador
                                     JOIN 
                                        jugadores bronce ON h.id_jugador_bronce = bronce.id_jugador
                                     ORDER BY 
                                        h.temporada DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                listadoHistorial.Add(new HistorialJugador()
                                {
                                    Temporada = dr["temporada"]?.ToString() ?? string.Empty,
                                    IdJugadorOro = dr["id_jugador_oro"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_oro"]) : 0,
                                    IdJugadorPlata = dr["id_jugador_plata"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_plata"]) : 0,
                                    IdJugadorBronce = dr["id_jugador_bronce"] != DBNull.Value ? Convert.ToInt32(dr["id_jugador_bronce"]) : 0,
                                    NombreJugadorOro = dr["jugador_oro"]?.ToString() ?? string.Empty,
                                    NombreJugadorPlata = dr["jugador_plata"]?.ToString() ?? string.Empty,
                                    NombreJugadorBronce = dr["jugador_bronce"]?.ToString() ?? string.Empty
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al jugador para el Balon de Oro
        public void AnadirTituloBalonOro(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Paso 1: Insertar si no existe
                    string insertQuery = @"INSERT OR IGNORE INTO palmaresJugadores (id_jugador, titulos) VALUES (@IdJugador, 0)";
                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@IdJugador", jugador);
                        insertCmd.ExecuteNonQuery();
                    }

                    // Paso 2: Sumar título
                    string updateQuery = @"UPDATE palmaresJugadores SET titulos = titulos + 1 WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@IdJugador", jugador);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------- Metodo que agrega los 3 premiados al balon de Oro, Plata y Bronce
        public void AnadirPremiosMejorJugador(int temporada, int oro, int plata, int bronce)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO historial_mejorJugador (temporada, id_jugador_oro, id_jugador_plata, id_jugador_bronce)
                                     VALUES (@Temporada, @Oro, @Plata, @Bronce)";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Temporada", temporada);
                        command.Parameters.AddWithValue("@Oro", oro);
                        command.Parameters.AddWithValue("@Plata", plata);
                        command.Parameters.AddWithValue("@Bronce", bronce);
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al jugador para la Bota de Oro
        public void AnadirTituloBotaOro(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Paso 1: Insertar si no existe
                    string insertQuery = @"INSERT OR IGNORE INTO palmaresGoleadores (id_jugador, titulos) VALUES (@IdJugador, 0)";
                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@IdJugador", jugador);
                        insertCmd.ExecuteNonQuery();
                    }

                    // Paso 2: Sumar título
                    string updateQuery = @"UPDATE palmaresGoleadores SET titulos = titulos + 1 WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@IdJugador", jugador);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------- Metodo que agrega los 3 premiados al Bota de Oro, Plata y Bronce
        public void AnadirPremiosMaximoGoleador(int temporada, int oro, int plata, int bronce)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO historial_maximoGoleador (temporada, id_jugador_oro, id_jugador_plata, id_jugador_bronce)
                                     VALUES (@Temporada, @Oro, @Plata, @Bronce)";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Temporada", temporada);
                        command.Parameters.AddWithValue("@Oro", oro);
                        command.Parameters.AddWithValue("@Plata", plata);
                        command.Parameters.AddWithValue("@Bronce", bronce);
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al campeon de Copa Nacional
        public void AnadirTituloCampeonCopa(int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"UPDATE palmaresCopa SET titulos = titulos + 1 WHERE id_equipo = @IdEquipo";

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

        // -------------------------------------------------------------------- Metodo que agrega el campeon y subcampeon de una Copa Nacional
        public void AnadirCampeonFinalistaCopa(int temporada, int campeon, int finalista)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string temporadaFormateada = $"{temporada}-{(temporada + 1) % 100:D2}";
                    string query = @"INSERT INTO historial_finalesCopa (temporada, id_equipo_campeon, id_equipo_finalista)
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al campeon de Copa Europa 1
        public void AnadirTituloCampeonCopaEuropa1(int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"UPDATE palmaresCopaEuropa1 SET titulos = titulos + 1 WHERE id_equipo = @IdEquipo";

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

        // -------------------------------------------------------------------- Metodo que agrega el campeon y subcampeon de una Copa Europa 1
        public void AnadirCampeonFinalistaCopaEuropa1(int temporada, int campeon, int finalista)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string temporadaFormateada = $"{temporada}-{(temporada + 1) % 100:D2}";
                    string query = @"INSERT INTO historial_finalesCopaEuropa1 (temporada, id_equipo_campeon, id_equipo_finalista)
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

        // -------------------------------------------------------------------- Metodo que suma un titulo al campeon de Copa Europa 2
        public void AnadirTituloCampeonCopaEuropa2(int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"UPDATE palmaresCopaEuropa2 SET titulos = titulos + 1 WHERE id_equipo = @IdEquipo";

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

        // -------------------------------------------------------------------- Metodo que agrega el campeon y subcampeon de una Copa Europa 2
        public void AnadirCampeonFinalistaCopaEuropa2(int temporada, int campeon, int finalista)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string temporadaFormateada = $"{temporada}-{(temporada + 1) % 100:D2}";
                    string query = @"INSERT INTO historial_finalesCopaEuropa2 (temporada, id_equipo_campeon, id_equipo_finalista)
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
