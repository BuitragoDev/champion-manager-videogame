﻿using ChampionManager25.Entidades;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class EstadisticaDatos : Conexion
    {
        // ------------------------------------------------------ MÉTODO QUE INSERTA UNA FILA DE ESTADÍSTICA POR CADA JUGADOR DE LIGA 
        public void InsertarEstadisticasJugadores(int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Obtener todos los id_jugador de la tabla jugadores
                    string selectQuery = "SELECT id_jugador FROM jugadores WHERE id_jugador < 5000";
                    List<int> listaJugadores = new List<int>();

                    using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, conn))
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaJugadores.Add(reader.GetInt32(0));
                        }
                    }

                    // Insertar una fila en estadisticas_jugadores por cada jugador
                    string insertQuery = @"INSERT INTO estadisticas_jugadores (id_jugador, id_manager)
                                           VALUES (@IdJugador, @IdManager)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCommand.Parameters.Add(new SQLiteParameter("@IdJugador"));
                        insertCommand.Parameters.Add(new SQLiteParameter("@IdManager", manager));

                        foreach (int idJugador in listaJugadores)
                        {
                            insertCommand.Parameters["@IdJugador"].Value = idJugador;
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ------------------------------------------------------ MÉTODO QUE INSERTA UNA FILA DE ESTADÍSTICA POR CADA JUGADOR DE EQUIPO EUROPEO
        public void InsertarEstadisticasJugadoresEuropa(int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta combinada: jugadores con id_jugador >= 5000 O jugadores cuyo equipo compite en Europa
                    string selectQuery = @"SELECT j.id_jugador
                                           FROM jugadores j
                                           INNER JOIN equipos e ON j.id_equipo = e.id_equipo
                                           WHERE j.id_jugador >= 5000 OR e.competicion_europea != 0";

                    List<int> listaJugadores = new List<int>();

                    using (SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, conn))
                    using (SQLiteDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaJugadores.Add(reader.GetInt32(0));
                        }
                    }

                    // Insertar una fila en estadisticas_jugadores_europa por cada jugador
                    string insertQuery = @"INSERT INTO estadisticas_jugadores_europa (id_jugador, id_manager)
                                           VALUES (@IdJugador, @IdManager)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCommand.Parameters.Add(new SQLiteParameter("@IdJugador"));
                        insertCommand.Parameters.Add(new SQLiteParameter("@IdManager", manager));

                        foreach (int idJugador in listaJugadores)
                        {
                            insertCommand.Parameters["@IdJugador"].Value = idJugador;
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


        // ------------------------------------------------------ MÉTODO QUE INSERTA UNA FILA DE ESTADÍSTICA DE UN JUGADOR DEL JUEGO
        public void InsertarLineaEstadisticaJugador(int numJugador, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO estadisticas_jugadores (id_jugador, id_manager)
                             VALUES (@IdJugador, @IdManager)";

                    // Ejecutar la inserción de numJugadores registros
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.Clear(); // Limpiar los parámetros antes de cada inserción
                        command.Parameters.AddWithValue("@IdJugador", numJugador); // Asignar el valor de i a id_equipo
                        command.Parameters.AddWithValue("@IdManager", manager);
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

        // ------------------------------------------------------------------- Método que devuelve el jugador con más Goles
        public Estadistica MostrarJugadorConMasGoles(int equipo)
        {
            Estadistica stats = new Estadistica
            {
                IdJugador = 0,
                Goles = 0,
                PartidosJugados = 0
            };

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        ej.id_jugador, ej.goles, ej.partidosJugados
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_equipo = @IdEquipo
                                     ORDER BY ej.goles DESC
                                     LIMIT 1;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                stats.IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                stats.Goles = reader.GetInt32(reader.GetOrdinal("goles"));
                                stats.PartidosJugados = reader.GetInt32(reader.GetOrdinal("partidosJugados"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return stats; // Devuelve el objeto con valores por defecto si no hay datos
        }

        // ------------------------------------------------------------------- Método que devuelve el jugador con más Asistencias
        public Estadistica MostrarJugadorConMasAsistencias(int equipo)
        {
            Estadistica stats = new Estadistica
            {
                IdJugador = 0,
                Asistencias = 0,
                PartidosJugados = 0
            };

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        ej.id_jugador, ej.asistencias, ej.partidosJugados
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_equipo = @IdEquipo
                                     ORDER BY ej.asistencias DESC
                                     LIMIT 1;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                stats.IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                stats.Asistencias = reader.GetInt32(reader.GetOrdinal("asistencias"));
                                stats.PartidosJugados = reader.GetInt32(reader.GetOrdinal("partidosJugados"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return stats; // Devuelve el objeto con valores por defecto si no hay datos
        }

        // ------------------------------------------------------------------- Método que devuelve el jugador con más MVP
        public Estadistica MostrarJugadorConMasMvp(int equipo)
        {
            Estadistica stats = new Estadistica
            {
                IdJugador = 0,
                MVP = 0,
                PartidosJugados = 0
            };

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        ej.id_jugador, ej.mvp, ej.partidosJugados
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_equipo = @IdEquipo
                                     ORDER BY ej.mvp DESC
                                     LIMIT 1;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                stats.IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                stats.MVP = reader.GetInt32(reader.GetOrdinal("mvp"));
                                stats.PartidosJugados = reader.GetInt32(reader.GetOrdinal("partidosJugados"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return stats; // Devuelve el objeto con valores por defecto si no hay datos
        }

        // ------------------------------------------------------------------- Método que devuelve el jugador con más Tarjetas Amarillas
        public Estadistica MostrarJugadorConMasTarjetasAmarillas(int equipo)
        {
            Estadistica stats = new Estadistica
            {
                IdJugador = 0,
                TarjetasAmarillas = 0,
                PartidosJugados = 0
            };

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        ej.id_jugador, ej.tarjetasAmarillas, ej.partidosJugados
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_equipo = @IdEquipo
                                     ORDER BY ej.tarjetasAmarillas DESC
                                     LIMIT 1;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                stats.IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                stats.TarjetasAmarillas = reader.GetInt32(reader.GetOrdinal("tarjetasAmarillas"));
                                stats.PartidosJugados = reader.GetInt32(reader.GetOrdinal("partidosJugados"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return stats; // Devuelve el objeto con valores por defecto si no hay datos
        }

        // ------------------------------------------------------------------- Método que devuelve el jugador con más Tarjetas Rojas
        public Estadistica MostrarJugadorConMasTarjetasRojas(int equipo)
        {
            Estadistica stats = new Estadistica
            {
                IdJugador = 0,
                TarjetasRojas = 0,
                PartidosJugados = 0
            };

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        ej.id_jugador, ej.tarjetasRojas, ej.partidosJugados
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_equipo = @IdEquipo
                                     ORDER BY ej.tarjetasRojas DESC
                                     LIMIT 1;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si se encuentra un registro
                            {
                                stats.IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                stats.TarjetasRojas = reader.GetInt32(reader.GetOrdinal("tarjetasRojas"));
                                stats.PartidosJugados = reader.GetInt32(reader.GetOrdinal("partidosJugados"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return stats; // Devuelve el objeto con valores por defecto si no hay datos
        }

        // ------------------------------------------------------------------- Método que devuelve la ESTADÍSTICA HISTÓRICA DE UN EQUIPO
        public EstadisticaHistorica MostrarEstadisticaHistoricaEquipo(int equipo, string categoria)
        {
            EstadisticaHistorica estadisticaHistorica = new EstadisticaHistorica();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT id_estadistica, nombreJugador, temporada, categoria, valor 
                             FROM estadisticas_historico_temporada
                             WHERE id_equipo = @IdEquipo AND categoria = @Categoria
                             ORDER BY valor DESC"; // Se ordena por valor de récord en orden descendente

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@Categoria", categoria);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                estadisticaHistorica = new EstadisticaHistorica()
                                {
                                    IdEstadistica = dr.GetInt32(0), // id_estadistica
                                    IdEquipo = equipo, // Se obtiene del parámetro
                                    NombreJugador = dr.GetString(1), // nombreJugador
                                    Temporada = dr.GetString(2), // temporada
                                    Categoria = dr.GetString(3), // categoria
                                    Valor = dr.GetInt32(4) // valor (cantidad de goles, asistencias, tarjetas, etc.)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return estadisticaHistorica; // Devuelve la lista con las estadísticas encontradas
        }

        // ------------------------------------------------------------------- Método que devuelve la ESTADÍSTICA HISTÓRICA TOTAL DE UN EQUIPO
        public EstadisticaHistorica MostrarEstadisticaHistoricaTotalEquipo(int equipo, string categoria)
        {
            EstadisticaHistorica estadisticaHistorica = new EstadisticaHistorica();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT id_estadistica, nombreJugador, categoria, valor 
                             FROM estadisticas_historico_totales
                             WHERE id_equipo = @IdEquipo AND categoria = @Categoria
                             ORDER BY valor DESC"; // Se ordena por valor de récord en orden descendente

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@Categoria", categoria);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                estadisticaHistorica = new EstadisticaHistorica()
                                {
                                    IdEstadistica = dr.GetInt32(0), // id_estadistica
                                    IdEquipo = equipo, // Se obtiene del parámetro
                                    NombreJugador = dr.GetString(1), // nombreJugador
                                    Categoria = dr.GetString(2), // categoria
                                    Valor = dr.GetInt32(3) // valor (cantidad de goles, asistencias, tarjetas, etc.)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return estadisticaHistorica; // Devuelve la lista con las estadísticas encontradas
        }

        // ======================================================= Método para mostrar las estadísticas de un Equipo
        public List<Estadistica> MostrarEstadisticasEquipo(int id, int manager)
        {
            List<Estadistica> lista = new List<Estadistica>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT 
                                        j.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.dorsal,
                                        j.nacionalidad,
                                        j.rol_id,
                                        e.partidosJugados,
                                        e.goles,
                                        e.asistencias,
                                        e.tarjetasAmarillas,
                                        e.tarjetasRojas,
                                        e.mvp
                                    FROM jugadores j
                                    LEFT JOIN estadisticas_jugadores e ON j.id_jugador = e.id_jugador
                                    WHERE j.id_equipo = @idEquipo AND e.id_manager = @idManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);
                        cmd.Parameters.AddWithValue("@idManager", manager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Estadistica
                                {
                                    IdJugador = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Dorsal = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Nacionalidad = reader.GetString(4),
                                    RolId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    PartidosJugados = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                                    Goles = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                    Asistencias = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                    TarjetasAmarillas = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                    TarjetasRojas = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                    MVP = reader.IsDBNull(11) ? 0 : reader.GetInt32(11)
                                });
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

            return lista;
        }

        // ======================================================= Método para mostrar las estadísticas de toda la competicion
        public List<Estadistica> MostrarEstadisticasTotales(int manager, int filtro, int competicion)
        {
            List<Estadistica> lista = new List<Estadistica>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    
                    string filtrocadena = "";
                    if (filtro == 1)
                    {
                        filtrocadena = "e.goles"; 
                    }
                    else if (filtro == 2)
                    {
                        filtrocadena = "e.asistencias";
                    }
                    else if (filtro == 3)
                    {
                        filtrocadena = "e.tarjetasAmarillas";
                    }
                    else if (filtro == 4)
                    {
                        filtrocadena = "e.tarjetasRojas";
                    }
                    else if (filtro == 5)
                    {
                        filtrocadena = "e.mvp";
                    }

                    // Consulta
                    string query = @"SELECT j.id_jugador, j.nombre, j.apellido, j.dorsal, j.nacionalidad, j.rol_id, e.partidosJugados, e.goles,
                                            e.asistencias, e.tarjetasAmarillas, e.tarjetasRojas, e.mvp, j.id_equipo
                                     FROM jugadores j
                                     LEFT JOIN equipos eq ON eq.id_equipo = j.id_equipo
                                     LEFT JOIN estadisticas_jugadores e ON j.id_jugador = e.id_jugador
                                     WHERE e.id_manager = @idManager AND eq.id_competicion = @Competicion
                                     ORDER BY " + filtrocadena + " DESC " +
                                     "LIMIT 25";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idManager", manager);
                        cmd.Parameters.AddWithValue("@Competicion", competicion);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Estadistica
                                {
                                    IdJugador = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Dorsal = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    Nacionalidad = reader.GetString(4),
                                    RolId = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    PartidosJugados = reader.IsDBNull(6) ? 0 : reader.GetInt32(6),
                                    Goles = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                    Asistencias = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                    TarjetasAmarillas = reader.IsDBNull(9) ? 0 : reader.GetInt32(9),
                                    TarjetasRojas = reader.IsDBNull(10) ? 0 : reader.GetInt32(10),
                                    MVP = reader.IsDBNull(12) ? 0 : reader.GetInt32(11),
                                    IdEquipo = reader.GetInt32(12)
                                });
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

            return lista;
        }

        // ======================================================= Método para mostrar las estadísticas de un Jugador
        public Estadistica MostrarEstadisticasJugador(int id, int manager)
        {
            Estadistica stats = new Estadistica();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT 
                                        id_jugador,
                                        partidosJugados,
                                        goles,
                                        asistencias,
                                        tarjetasAmarillas,
                                        tarjetasRojas,
                                        mvp
                                    FROM estadisticas_jugadores
                                    WHERE id_jugador = @idJugador AND id_manager = @idManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idJugador", id);
                        cmd.Parameters.AddWithValue("@idManager", manager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                stats = new Estadistica
                                {
                                    IdJugador = reader.GetInt32(0),
                                    PartidosJugados = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                                    Goles = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Asistencias = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                    TarjetasAmarillas = reader.IsDBNull(4) ? 0 : reader.GetInt32(4),
                                    TarjetasRojas = reader.IsDBNull(5) ? 0 : reader.GetInt32(5),
                                    MVP = reader.IsDBNull(6) ? 0 : reader.GetInt32(6)
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

            return stats;
        }

        // --------------------------------------------------------------- Método para actualizar las estadisticas de los jugadores
        public void ActualizarEstadisticas(Estadistica estadistica)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE estadisticas_jugadores 
                                     SET partidosJugados = partidosJugados + @PartidosJugados,
                                         goles = goles + @Goles,
                                         asistencias = asistencias + @Asistencias,
                                         tarjetasAmarillas = tarjetasAmarillas + @TarjetasAmarillas,
                                         tarjetasRojas = tarjetasRojas + @TarjetasRojas,
                                         mvp = mvp + @MVPs
                                     WHERE id_jugador = @IdJugador";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", estadistica.IdJugador);
                        cmd.Parameters.AddWithValue("@Goles", estadistica.Goles);
                        cmd.Parameters.AddWithValue("@Asistencias", estadistica.Asistencias);
                        cmd.Parameters.AddWithValue("@TarjetasAmarillas", estadistica.TarjetasAmarillas);
                        cmd.Parameters.AddWithValue("@TarjetasRojas", estadistica.TarjetasRojas);
                        cmd.Parameters.AddWithValue("@MVPs", estadistica.MVP);
                        cmd.Parameters.AddWithValue("@PartidosJugados", estadistica.PartidosJugados);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // --------------------------------------------------------------- Método para actualizar las estadisticas de los jugadores
        public void ActualizarEstadisticasEuropa(Estadistica estadistica)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE estadisticas_jugadores_europa
                                     SET partidosJugados = partidosJugados + @PartidosJugados,
                                         goles = goles + @Goles,
                                         asistencias = asistencias + @Asistencias,
                                         tarjetasAmarillas = tarjetasAmarillas + @TarjetasAmarillas,
                                         tarjetasRojas = tarjetasRojas + @TarjetasRojas,
                                         mvp = mvp + @MVPs
                                     WHERE id_jugador = @IdJugador";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", estadistica.IdJugador);
                        cmd.Parameters.AddWithValue("@Goles", estadistica.Goles);
                        cmd.Parameters.AddWithValue("@Asistencias", estadistica.Asistencias);
                        cmd.Parameters.AddWithValue("@TarjetasAmarillas", estadistica.TarjetasAmarillas);
                        cmd.Parameters.AddWithValue("@TarjetasRojas", estadistica.TarjetasRojas);
                        cmd.Parameters.AddWithValue("@MVPs", estadistica.MVP);
                        cmd.Parameters.AddWithValue("@PartidosJugados", estadistica.PartidosJugados);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // --------------------------------------------------------------- Método para resetea la estadistica de un jugador
        public void ResetearEstadisticaJugador(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE estadisticas_jugadores 
                                     SET partidosJugados = 0,
                                         goles = 0,
                                         asistencias = 0,
                                         tarjetasAmarillas = 0,
                                         tarjetasRojas = 0,
                                         mvp = 0
                                     WHERE id_jugador = @IdJugador";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------- Metodo que resetea las estadisticas de los jugadores
        public void ResetearEstadisticas()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE estadisticas_jugadores
                                     SET partidosJugados = 0,
                                         goles = 0,
                                         asistencias = 0,
                                         tarjetasAmarillas = 0,
                                         tarjetasRojas = 0,
                                         mvp = 0,
                                         semanas_lesionado = 0";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
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

        // -------------------------------------------------------------------- Metodo que resetea las estadisticas de los jugadores de Europa
        public void ResetearEstadisticasEuropa()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE estadisticas_jugadores_europa
                                     SET partidosJugados = 0,
                                         goles = 0,
                                         asistencias = 0,
                                         tarjetasAmarillas = 0,
                                         tarjetasRojas = 0,
                                         mvp = 0,
                                         semanas_lesionado = 0";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
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

        // --------------------------------------------------------------- Metodo que muestra los 3 mejores jugadores de la temporada en la Liga
        public List<Jugador> BalonDeOro()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        ej.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.ruta_imagen,
                                        j.id_equipo,
                                        CAST( 
                                            (ej.goles * 2) +
                                            (ej.asistencias * 2.5) +
                                            (ej.partidosJugados) +
                                            (ej.mvp * 3) -
                                            (ej.tarjetasAmarillas * 0.5) -
                                            (ej.tarjetasRojas * 2) 
                                        AS INTEGER) AS puntos
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_competicion = 1
                                     ORDER BY puntos DESC
                                     LIMIT 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Jugador
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen")),
                                    Valoracion = reader.GetInt32(reader.GetOrdinal("puntos")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
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

            return lista;
        }

        // --------------------------------------------------------------- Metodo que muestra los 3 mejores jugadores de la Copa Europa 1
        public List<Jugador> BalonDeOroEuropa1()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        ej.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.ruta_imagen,
                                        j.id_equipo,
                                        CAST( 
                                            (ej.goles * 2) +
                                            (ej.asistencias * 2.5) +
                                            (ej.partidosJugados) +
                                            (ej.mvp * 3) -
                                            (ej.tarjetasAmarillas * 0.5) -
                                            (ej.tarjetasRojas * 2) 
                                        AS INTEGER) AS puntos
                                     FROM estadisticas_jugadores_europa ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_competicion = 5 OR e.competicion_europea = 5
                                     ORDER BY puntos DESC
                                     LIMIT 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Jugador
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen")),
                                    Valoracion = reader.GetInt32(reader.GetOrdinal("puntos")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
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

            return lista;
        }

        // --------------------------------------------------------------- Metodo que muestra los 3 mejores jugadores de la Copa Europa 2
        public List<Jugador> BalonDeOroEuropa2()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        ej.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.ruta_imagen,
                                        j.id_equipo,
                                        CAST( 
                                            (ej.goles * 2) +
                                            (ej.asistencias * 2.5) +
                                            (ej.partidosJugados) +
                                            (ej.mvp * 3) -
                                            (ej.tarjetasAmarillas * 0.5) -
                                            (ej.tarjetasRojas * 2) 
                                        AS INTEGER) AS puntos
                                     FROM estadisticas_jugadores_europa ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_competicion = 6 OR e.competicion_europea = 6
                                     ORDER BY puntos DESC
                                     LIMIT 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Jugador
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen")),
                                    Valoracion = reader.GetInt32(reader.GetOrdinal("puntos")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
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

            return lista;
        }

        // --------------------------------------------------------------------- Metodo que muestra los 3 maximo goleadores de la temporada
        public List<Jugador> BotaDeOro()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        ej.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.ruta_imagen,
                                        j.id_equipo,
                                        ej.goles
                                     FROM estadisticas_jugadores ej
                                     JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE e.id_competicion = 1
                                     ORDER BY ej.goles DESC
                                     LIMIT 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Jugador
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen")),
                                    Valoracion = reader.GetInt32(reader.GetOrdinal("goles")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
                                });
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

            return lista;
        }

        // --------------------------------------------------------------------- Metodo que muestra el mejor 11 de la temporada
        public List<Jugador> MejorOnceTemporada()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                                        j.rol,
                                        j.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.ruta_imagen,
                                        j.id_equipo,
                                        e.nombre AS nombre_equipo,
                                        (
                                            ej.goles * 2 +
                                            ej.asistencias +
                                            ej.partidosJugados +
                                            ej.mvp * 3 -
                                            ej.tarjetasAmarillas -
                                            ej.tarjetasRojas * 2
                                        ) AS puntos
                                    FROM estadisticas_jugadores ej
                                    JOIN jugadores j ON ej.id_jugador = j.id_jugador
                                    JOIN equipos e ON j.id_equipo = e.id_equipo
                                    WHERE j.rol_id IS NOT NULL
                                      AND e.id_competicion = 1
                                      AND j.id_jugador IN (
                                        SELECT ej2.id_jugador
                                        FROM estadisticas_jugadores ej2
                                        JOIN jugadores j2 ON ej2.id_jugador = j2.id_jugador
                                        JOIN equipos e2 ON j2.id_equipo = e2.id_equipo
                                        WHERE j2.rol_id = j.rol_id
                                          AND e2.id_competicion = 1
                                        ORDER BY (
                                            ej2.goles * 2 +
                                            ej2.asistencias +
                                            ej2.partidosJugados +
                                            ej2.mvp * 3 -
                                            ej2.tarjetasAmarillas -
                                            ej2.tarjetasRojas * 2
                                        ) DESC
                                        LIMIT 1
                                    )
                                    GROUP BY j.rol_id
                                    ORDER BY j.rol_id";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            lista.Clear();  // Asegura que la lista esté vacía antes de llenarla

                            while (reader.Read())
                            {
                                lista.Add(new Jugador
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    RutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen")),
                                    Rol = reader.GetString(reader.GetOrdinal("rol")),
                                    Valoracion = reader.GetInt32(reader.GetOrdinal("puntos")),
                                    NombreEquipo = reader.GetString(reader.GetOrdinal("nombre_equipo")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo")),
                                });
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

            return lista;
        }
    }
}
