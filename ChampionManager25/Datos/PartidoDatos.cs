using System;
using System.Windows;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionManager25.MisMetodos;
using ChampionManager25.Entidades;
using System.Configuration;
using ChampionManager25.Logica;

namespace ChampionManager25.Datos
{
    public class PartidoDatos : Conexion
    {
        private Metodos metodos;

        public PartidoDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para insertar un nuevo partido de Liga
        public int crearPartido(int local, int visitante, string fecha, int competicion, int jornada, int idMng)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para insertar el partido en la tabla "partidos"
                    string query = @"INSERT INTO partidos (fecha, id_equipo_local, id_equipo_visitante, id_competicion, jornada, id_manager) 
                             VALUES (@Fecha, @IdEquipoLocal, @IdEquipoVisitante, @Competicion, @Jornada, @idManager)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros para la consulta SQL
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@IdEquipoLocal", local);
                        cmd.Parameters.AddWithValue("@IdEquipoVisitante", visitante);
                        cmd.Parameters.AddWithValue("@Competicion", competicion);
                        cmd.Parameters.AddWithValue("@Jornada", jornada);
                        cmd.Parameters.AddWithValue("@idManager", idMng);

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }

                    // Obtener el último id insertado
                    long idPartido = conn.LastInsertRowId;
                    return (int)idPartido; // Retornar el id del partido insertado
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                return -1; // Devolver un valor de error
            }
        }

        // ===================================================================== Método para insertar un nuevo partido de Copa
        public int crearPartidoCopa(int local, int visitante, string fecha, int competicion, int ronda, int partidoVuelta,  int idMng)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para insertar el partido en la tabla "partidos_CopaNacional"
                    string query = @"INSERT INTO partidos_CopaNacional (partido_vuelta, fecha, id_equipo_local, id_equipo_visitante, id_competicion, id_ronda, id_manager) 
                             VALUES (@PartidoVuelta, @Fecha, @IdEquipoLocal, @IdEquipoVisitante, @Competicion, @Ronda, @idManager)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros para la consulta SQL
                        cmd.Parameters.AddWithValue("@PartidoVuelta", partidoVuelta);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        cmd.Parameters.AddWithValue("@IdEquipoLocal", local);
                        cmd.Parameters.AddWithValue("@IdEquipoVisitante", visitante);
                        cmd.Parameters.AddWithValue("@Competicion", competicion);
                        cmd.Parameters.AddWithValue("@Ronda", ronda);
                        cmd.Parameters.AddWithValue("@idManager", idMng);

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }

                    // Obtener el último id insertado
                    long idPartido = conn.LastInsertRowId;
                    return (int)idPartido; // Retornar el id del partido insertado
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                return -1; // Devolver un valor de error
            }
        }

        // ===================================================================== Método para eliminar un partido
        public void eliminarPartidos(List<int> idsPartidos)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Recorrer los IDs de los partidos y eliminarlos
                    foreach (int id in idsPartidos)
                    {
                        string query = @"DELETE FROM partidos WHERE id_partido = @IdPartido";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdPartido", id);
                            cmd.ExecuteNonQuery();
                        }

                        string queryCopa = @"DELETE FROM partidos_copaNacional WHERE id_partido = @IdPartido";

                        using (SQLiteCommand cmd = new SQLiteCommand(queryCopa, conn))
                        {
                            cmd.Parameters.AddWithValue("@IdPartido", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al eliminar los partidos: {ex.Message}");
            }
        }

        // ----------------------------------------------------------------- METODO PARA OBTENER LOS EQUIPOS DE LA LIGA DE FORMA ALEATORIA
        private List<int> ObtenerEquiposLiga(int idCompeticion)
        {
            List<int> equipos = new List<int>();
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();
                string query = "SELECT id_equipo FROM equipos WHERE id_competicion = @idCompeticion ORDER BY RANDOM()";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idCompeticion", idCompeticion);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipos.Add(reader.GetInt32(0));
                        }
                    }
                }
            }
            return equipos;
        }

        // -------------------------------------------------------------------- METODO PARA CREAR EL CALENDARIO
        public void GenerarCalendario(int temporadaActual, int idManager, int idCompeticion)
        {
            List<int> equipos = ObtenerEquiposLiga(idCompeticion);
            if (equipos.Count < 2)
            {
                throw new Exception("No hay suficientes equipos para generar un calendario.");
            }

            List<List<Tuple<int, int>>> calendario = GenerarRoundRobin(equipos);

            GuardarCalendario(calendario, temporadaActual, idManager, idCompeticion);
        }

        private List<List<Tuple<int, int>>> GenerarRoundRobin(List<int> equipos)
        {
            int numEquipos = equipos.Count;

            // Si el número de equipos es impar, añadimos un equipo "fantasma"
            bool esImpar = numEquipos % 2 != 0;
            if (esImpar)
            {
                equipos.Add(-1); // ID -1 representa descanso
                numEquipos++;
            }

            List<List<Tuple<int, int>>> jornadasIda = new List<List<Tuple<int, int>>>();

            // Crear una copia de los equipos sin el primero (que se fija)
            List<int> rotables = equipos.Skip(1).ToList();
            int equipoFijo = equipos[0];

            // Generar la primera vuelta (ida)
            for (int i = 0; i < numEquipos - 1; i++)
            {
                List<Tuple<int, int>> jornada = new List<Tuple<int, int>>();

                if (i % 2 == 0)
                    jornada.Add(new Tuple<int, int>(equipoFijo, rotables[0]));
                else
                    jornada.Add(new Tuple<int, int>(rotables[0], equipoFijo));

                for (int j = 1; j < numEquipos / 2; j++)
                {
                    int local = rotables[j];
                    int visitante = rotables[numEquipos - 1 - j];

                    if (i % 2 == 0)
                        jornada.Add(new Tuple<int, int>(local, visitante));
                    else
                        jornada.Add(new Tuple<int, int>(visitante, local));
                }

                jornadasIda.Add(jornada);

                // Rotar
                rotables.Insert(0, rotables[^1]);
                rotables.RemoveAt(rotables.Count - 1);
            }

            // Generar la segunda vuelta (vuelta) invirtiendo local y visitante
            List<List<Tuple<int, int>>> jornadasVuelta = jornadasIda
                .Select(jornada => jornada
                    .Select(partido => new Tuple<int, int>(partido.Item2, partido.Item1)).ToList())
                .ToList();

            // Unir ambas vueltas
            return jornadasIda.Concat(jornadasVuelta).ToList();
        }


        private void GuardarCalendario(List<List<Tuple<int, int>>> calendario, int temporada, int idManager, int idCompeticion)
        {
            DateTime fechaInicio = ObtenerTercerSabadoDeAgosto(temporada);
            int jornadaNum = 1;

            foreach (var jornada in calendario)
            {
                // Calcula las dos fechas para los partidos de la jornada
                DateTime fechaPrimerDia = fechaInicio.AddDays((jornadaNum - 1) * 7); // Día 1 de la jornada (Sabado)
                DateTime fechaSegundoDia = fechaPrimerDia.AddDays(1); // Día 2 de la jornada (Domingo)

                // Dividir los partidos de la jornada en dos grupos
                for (int i = 0; i < jornada.Count; i++)
                {
                    // Si es el primer grupo de 5 partidos, asignar fechaPrimerDia
                    DateTime fechaPartido = (i < 5) ? fechaPrimerDia : fechaSegundoDia;
                    var partido = jornada[i];

                    // Llamamos a tu método para insertar en la BD
                    crearPartido(partido.Item1, partido.Item2, fechaPartido.ToString("yyyy-MM-dd"), idCompeticion, jornadaNum, idManager);
                }

                jornadaNum++;
            }
        }

        public static DateTime ObtenerTercerSabadoDeAgosto(int anio)
        {
            DateTime fecha = new DateTime(anio, 8, 1);
            int sabadosEncontrados = 0;

            while (fecha.Month == 8)
            {
                if (fecha.DayOfWeek == DayOfWeek.Saturday)
                {
                    sabadosEncontrados++;
                    if (sabadosEncontrados == 3)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        // ----------------------------------------------------------------------- Método que devuelve el próximo partido de mi equipo.
        public Partido ObtenerProximoPartido(int equipo, int idManager, DateTime hoy)
        {
            Partido partido = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT * FROM (SELECT 
                                                        p.id_partido,
                                                        p.fecha, 
                                                        p.jornada,
                                                        NULL AS id_ronda,
                                                        NULL AS partido_vuelta, 
                                                        el.nombre AS nombreEquipoLocal, 
                                                        ev.nombre AS nombreEquipoVisitante, 
                                                        p.id_equipo_local, 
                                                        p.id_equipo_visitante,
                                                        p.goles_local,
                                                        p.goles_visitante,
                                                        p.id_competicion
                                                    FROM partidos p
                                                    JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                                    WHERE 
                                                        (p.id_equipo_local = @IdEquipo OR p.id_equipo_visitante = @IdEquipo)
                                                        AND p.id_manager = @IdManager
                                                        AND DATE(p.fecha) >= DATE(@Hoy)

                                                    UNION ALL

                                                    SELECT 
                                                        pc.id_partido,
                                                        pc.fecha, 
                                                        NULL AS jornada,
                                                        pc.id_ronda,
                                                        pc.partido_vuelta,
                                                        el.nombre AS nombreEquipoLocal, 
                                                        ev.nombre AS nombreEquipoVisitante, 
                                                        pc.id_equipo_local, 
                                                        pc.id_equipo_visitante,
                                                        pc.goles_local,
                                                        pc.goles_visitante,
                                                        pc.id_competicion
                                                    FROM partidos_copaNacional pc
                                                    JOIN equipos el ON pc.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON pc.id_equipo_visitante = ev.id_equipo
                                                    WHERE 
                                                        (pc.id_equipo_local = @IdEquipo OR pc.id_equipo_visitante = @IdEquipo)
                                                        AND pc.id_manager = @IdManager
                                                        AND DATE(pc.fecha) >= DATE(@Hoy)
                                                )
                                                ORDER BY fecha ASC
                                                LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Hoy", hoy);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partido = new Partido
                                {
                                    IdPartido = Convert.ToInt32(reader["id_partido"]),
                                    FechaPartido = reader["fecha"] != DBNull.Value && DateTime.TryParse(reader["fecha"].ToString(), out DateTime fecha)
                                                   ? fecha
                                                   : DateTime.Parse("2000-01-01"),
                                    Jornada = reader["jornada"] != DBNull.Value ? Convert.ToInt32(reader["jornada"]) : 0,
                                    Ronda = reader["id_ronda"] != DBNull.Value ? Convert.ToInt32(reader["id_ronda"]) : 0,
                                    PartidoVuelta = reader["partido_vuelta"] != DBNull.Value ? Convert.ToInt32(reader["partido_vuelta"]) : 0,
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return partido;
        }

        // Método que devuelve el último partido de mi equipo.
        public Partido ObtenerUltimoPartido(int equipo, int idManager, DateTime hoy)
        {
            Partido partido = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, jornada
                                     FROM (
                                        SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, jornada
                                        FROM partidos
                                        WHERE 
                                            (id_equipo_local = @IdEquipo OR id_equipo_visitante = @IdEquipo)
                                            AND id_manager = @IdManager
                                            AND DATE(fecha) < DATE(@Hoy)

                                        UNION ALL

                                        SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, id_ronda
                                        FROM partidos_copaNacional
                                        WHERE 
                                            (id_equipo_local = @IdEquipo OR id_equipo_visitante = @IdEquipo)
                                            AND id_manager = @IdManager
                                            AND DATE(fecha) < DATE(@Hoy)
                                     )
                                     ORDER BY fecha DESC
                                     LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Hoy", hoy.Date);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partido = new Partido
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return partido;
        }

        // Método que devuelve todos los partidos de mi equipo.
        public List<Partido> MostrarMisPartidos(int equipo, int idManager)
        {
            List<Partido> oPartido = new List<Partido>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        p.fecha, 
                                        el.nombre AS nombreEquipoLocal, 
                                        ev.nombre AS nombreEquipoVisitante, 
                                        p.id_equipo_local, 
                                        p.id_equipo_visitante,
                                        p.goles_local,
                                        p.goles_visitante,
                                        p.id_competicion
                                    FROM partidos p
                                    JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                    JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                    WHERE 
                                        (p.id_equipo_local = @IdEquipo OR p.id_equipo_visitante = @IdEquipo)
                                        AND p.id_manager = @IdManager

                                    UNION ALL

                                    SELECT 
                                        pc.fecha, 
                                        el.nombre AS nombreEquipoLocal, 
                                        ev.nombre AS nombreEquipoVisitante, 
                                        pc.id_equipo_local, 
                                        pc.id_equipo_visitante,
                                        pc.goles_local,
                                        pc.goles_visitante,
                                        pc.id_competicion
                                    FROM partidos_copaNacional pc
                                    JOIN equipos el ON pc.id_equipo_local = el.id_equipo
                                    JOIN equipos ev ON pc.id_equipo_visitante = ev.id_equipo
                                    WHERE 
                                        (pc.id_equipo_local = @IdEquipo OR pc.id_equipo_visitante = @IdEquipo)
                                        AND pc.id_manager = @IdManager

                                    ORDER BY fecha";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPartido.Add(new Partido()
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
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

            return oPartido;
        }

        // Método que devuelve todos los partidos de mi equipo en Copa
        public List<Partido> MostrarMisPartidosCopaNacional(int equipo, int idManager)
        {
            List<Partido> oPartido = new List<Partido>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT 
                                        p.fecha, 
                                        el.nombre AS nombreEquipoLocal, 
                                        ev.nombre AS nombreEquipoVisitante, 
                                        p.id_equipo_local, 
                                        p.id_equipo_visitante,
                                        p.goles_local,
                                        p.goles_visitante,
                                        p.id_competicion
                                     FROM partidos_copaNacional p
                                     JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                     JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                     WHERE 
                                        (p.id_equipo_local = @IdEquipo OR p.id_equipo_visitante = @IdEquipo)
                                     AND p.id_manager = @IdManager
                                     ORDER BY p.fecha;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPartido.Add(new Partido()
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
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

            return oPartido;
        }

        // Método que devuelve un partido en base a una fecha
        public Partido? MostrarDetallesPartido(int equipo, int idManager, DateTime fecha)
        {
            Partido? partido = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, jornada
                                     FROM (
                                        SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, jornada
                                        FROM partidos
                                        WHERE 
                                            (id_equipo_local = @IdEquipo OR id_equipo_visitante = @IdEquipo)
                                            AND id_manager = @IdManager
                                            AND DATE(fecha) = DATE(@Fecha)

                                        UNION ALL

                                        SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, id_competicion, id_ronda
                                        FROM partidos_copaNacional
                                        WHERE 
                                            (id_equipo_local = @IdEquipo OR id_equipo_visitante = @IdEquipo)
                                            AND id_manager = @IdManager
                                            AND DATE(fecha) = DATE(@Fecha)
                                     )
                                     ORDER BY fecha DESC
                                     LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partido = new Partido
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0,
                                    Jornada = reader["jornada"] != DBNull.Value ? Convert.ToInt32(reader["jornada"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return partido;
        }

        // Método que devuelve mi próximo partido en casa
        public Partido? MostrarProximoPartidoLocal(int equipo, int idManager, DateTime fecha)
        {
            Partido? partido = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT * FROM (SELECT 
                                                        p.*, 
                                                        el.nombre AS nombre_local, 
                                                        ev.nombre AS nombre_visitante 
                                                    FROM partidos p
                                                    JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                                    WHERE p.id_equipo_local = @IdEquipo 
                                                      AND p.id_manager = @IdManager 
                                                      AND p.fecha > @Fecha

                                                    UNION ALL

                                                    SELECT 
                                                        pc.*, 
                                                        el.nombre AS nombre_local, 
                                                        ev.nombre AS nombre_visitante 
                                                    FROM partidos_copaNacional pc
                                                    JOIN equipos el ON pc.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON pc.id_equipo_visitante = ev.id_equipo
                                                    WHERE pc.id_equipo_local = @IdEquipo 
                                                      AND pc.id_manager = @IdManager 
                                                      AND pc.fecha > @Fecha
                                                    )
                                                    ORDER BY fecha ASC
                                                    LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partido = new Partido
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0,
                                    Jornada = reader["jornada"] != DBNull.Value ? Convert.ToInt32(reader["jornada"]) : 0,
                                    NombreEquipoLocal = reader["nombre_local"]?.ToString() ?? "",
                                    NombreEquipoVisitante = reader["nombre_visitante"]?.ToString() ?? ""

                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return partido;
        }

        // Metodo que carga los partidos de una jornada
        public List<Partido> CargarJornada(int jornada, int manager, int competicion)
        {
            List<Partido> oPartido = new List<Partido>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT fecha, id_equipo_local, id_equipo_visitante, goles_local, goles_visitante, estado, id_competicion
                                     FROM partidos
                                     WHERE jornada = @jornada AND id_manager = @IdManager AND id_competicion = @IdCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@jornada", jornada);
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPartido.Add(new Partido()
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    Estado = reader.GetString(reader.GetOrdinal("estado")),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
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

            return oPartido;
        }

        // --------------------------------------------------------- Método que devuelve los ultimos 5 partidos de un equipo
        public List<Partido> UltimosCincoPartidos(int equipo, int idManager)
        {
            List<Partido> oPartido = new List<Partido>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT * FROM (SELECT 
                                                            p.fecha, 
                                                            el.nombre AS nombreEquipoLocal, 
                                                            ev.nombre AS nombreEquipoVisitante, 
                                                            p.id_equipo_local, 
                                                            p.id_equipo_visitante,
                                                            p.goles_local,
                                                            p.goles_visitante,
                                                            p.id_competicion
                                                        FROM partidos p
                                                        JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                                        JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                                        WHERE p.fecha < @Hoy
                                                          AND (p.id_equipo_local = @IdEquipo OR p.id_equipo_visitante = @IdEquipo)
                                                          AND p.id_manager = @IdManager

                                                        UNION ALL

                                                        SELECT 
                                                            pc.fecha, 
                                                            el.nombre AS nombreEquipoLocal, 
                                                            ev.nombre AS nombreEquipoVisitante, 
                                                            pc.id_equipo_local, 
                                                            pc.id_equipo_visitante,
                                                            pc.goles_local,
                                                            pc.goles_visitante,
                                                            pc.id_competicion
                                                        FROM partidos_copaNacional pc
                                                        JOIN equipos el ON pc.id_equipo_local = el.id_equipo
                                                        JOIN equipos ev ON pc.id_equipo_visitante = ev.id_equipo
                                                        WHERE pc.fecha < @Hoy
                                                          AND (pc.id_equipo_local = @IdEquipo OR pc.id_equipo_visitante = @IdEquipo)
                                                          AND pc.id_manager = @IdManager
                                                    )
                                                    ORDER BY fecha DESC, id_partido DESC
                                                    LIMIT 5";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Hoy", Metodos.hoy);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPartido.Add(new Partido()
                                {
                                    FechaPartido = DateTime.Parse(reader["fecha"]?.ToString() ?? "2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
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

            return oPartido;
        }

        // --------------------------------------------------------- Método que devuelve los partidos de hoy que NO juega mi equipo
        public List<Partido> PartidosHoy(int miEquipo, int idManager)
        {
            List<Partido> oPartido = new List<Partido>();
            
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT * FROM (SELECT 
                                                        p.id_partido,
                                                        p.fecha, 
                                                        NULL AS id_ronda, 
                                                        NULL AS partido_vuelta, 
                                                        p.jornada,
                                                        el.nombre AS nombreEquipoLocal, 
                                                        ev.nombre AS nombreEquipoVisitante, 
                                                        p.id_equipo_local, 
                                                        p.id_equipo_visitante,
                                                        p.goles_local,
                                                        p.goles_visitante,
                                                        p.id_competicion
                                                    FROM partidos p
                                                    JOIN equipos el ON p.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON p.id_equipo_visitante = ev.id_equipo
                                                    WHERE p.fecha = @Hoy
                                                      AND (p.id_equipo_local <> @IdEquipo AND p.id_equipo_visitante <> @IdEquipo)
                                                      AND p.id_manager = @IdManager

                                                    UNION ALL

                                                    SELECT 
                                                        pc.id_partido,
                                                        pc.fecha, 
                                                        pc.id_ronda,
                                                        pc.partido_vuelta,
                                                        NULL AS jornada,
                                                        el.nombre AS nombreEquipoLocal, 
                                                        ev.nombre AS nombreEquipoVisitante, 
                                                        pc.id_equipo_local, 
                                                        pc.id_equipo_visitante,
                                                        pc.goles_local,
                                                        pc.goles_visitante,
                                                        pc.id_competicion
                                                    FROM partidos_copaNacional pc
                                                    JOIN equipos el ON pc.id_equipo_local = el.id_equipo
                                                    JOIN equipos ev ON pc.id_equipo_visitante = ev.id_equipo
                                                    WHERE pc.fecha = @Hoy
                                                      AND (pc.id_equipo_local <> @IdEquipo AND pc.id_equipo_visitante <> @IdEquipo)
                                                      AND pc.id_manager = @IdManager
                                                )
                                                ORDER BY fecha DESC, id_partido ASC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdEquipo", miEquipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@Hoy", Metodos.hoy.ToString("yyyy-MM-dd"));

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPartido.Add(new Partido()
                                {
                                    IdPartido = Convert.ToInt32(reader["id_partido"]),
                                    Jornada = reader["jornada"] != DBNull.Value ? Convert.ToInt32(reader["jornada"]) : 0,
                                    Ronda = reader["id_ronda"] != DBNull.Value ? Convert.ToInt32(reader["id_ronda"]) : 0,
                                    PartidoVuelta = reader["partido_vuelta"] != DBNull.Value ? Convert.ToInt32(reader["partido_vuelta"]) : 0,
                                    FechaPartido = reader["fecha"] != DBNull.Value && DateTime.TryParse(reader["fecha"].ToString(), out DateTime fecha)
                                                   ? fecha
                                                   : DateTime.Parse("2000-01-01"),
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0,
                                    NombreEquipoLocal = reader.GetString(reader.GetOrdinal("nombreEquipoLocal")),
                                    NombreEquipoVisitante = reader.GetString(reader.GetOrdinal("nombreEquipoVisitante"))
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
           
            return oPartido;
        }

        // -------------------------------------------------------------------------- Método que actualiza con el resultado de un partido de Liga
        public void ActualizarPartido(Partido partido)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE partidos 
                                     SET goles_local = @GolesLocal, goles_visitante = @GolesVisitante, asistencia = @Asistencia, estado = @Estado
                                     WHERE id_partido = @IdPartido";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GolesLocal", partido.GolesLocal); 
                        cmd.Parameters.AddWithValue("@GolesVisitante", partido.GolesVisitante);
                        cmd.Parameters.AddWithValue("@Asistencia", partido.Asistencia);
                        cmd.Parameters.AddWithValue("@Estado", "Finalizado");
                        cmd.Parameters.AddWithValue("@IdPartido", partido.IdPartido);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // -------------------------------------------------------------------------- Método que actualiza con el resultado de un partido de Copa
        public void ActualizarPartidoCopaNacional(Partido partido)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE partidos_copaNacional 
                                     SET goles_local = @GolesLocal, goles_visitante = @GolesVisitante, asistencia = @Asistencia, estado = @Estado
                                     WHERE id_partido = @IdPartido";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@GolesLocal", partido.GolesLocal);
                        cmd.Parameters.AddWithValue("@GolesVisitante", partido.GolesVisitante);
                        cmd.Parameters.AddWithValue("@Asistencia", partido.Asistencia);
                        cmd.Parameters.AddWithValue("@Estado", "Finalizado");
                        cmd.Parameters.AddWithValue("@IdPartido", partido.IdPartido);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // ===================================================================== Método para obtener la fecha del ultimo partido
        public string ultimoPartidoCalendario()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT fecha FROM (SELECT fecha FROM partidos
                                                        UNION ALL
                                                        SELECT fecha FROM partidos_copaNacional
                                                        )
                                                        ORDER BY fecha DESC
                                                        LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            string fecha = resultado.ToString();
                            return fecha;
                        }
                        else
                        {
                            return ""; // No hay partidos en la tabla
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                return "";
            }
        }

        // -------------------------------------------------------------------------- Método que resetea la tabla partidos
        public void ResetearPartidos()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"DELETE FROM partidos";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }

                    string queryCopa = @"DELETE FROM partidos_copaNacional";
                    using (var cmd = new SQLiteCommand(queryCopa, conn))
                    {
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // ----------------------------------------------------------------- METODO PARA OBTENER EL NOMBRE DE UNA RONDA DE COPA
        public string ObtenerNombreRonda(int idRonda)
        {
            string nombre = "";
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();
                string query = "SELECT nombre FROM rondas_copaNacional WHERE id_ronda = @idRonda";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idRonda", idRonda);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nombre = reader["nombre"].ToString();
                        }
                    }
                }
            }
            return nombre;
        }

        // ----------------------------------------------------------------- Metodo que devuelve una lista con los equipos clasificados de Copa
        public List<Equipo> ObtenerEquiposClasificados(int idRonda, int idCompeticion, int idManager)
        {
            List<Equipo> clasificados = new List<Equipo>();

            using (var conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT 
                                    ida.id_equipo_local AS equipo_ida_local,
                                    ida.id_equipo_visitante AS equipo_ida_visitante,
                                    ida.goles_local AS goles_ida_local,
                                    ida.goles_visitante AS goles_ida_visitante,
                                    vuelta.goles_local AS goles_vuelta_local,
                                    vuelta.goles_visitante AS goles_vuelta_visitante,
                                    vuelta.id_equipo_local AS equipo_vuelta_local,
                                    vuelta.id_equipo_visitante AS equipo_vuelta_visitante
                                 FROM partidos_copaNacional ida
                                 JOIN partidos_copaNacional vuelta
                                    ON ida.id_ronda = vuelta.id_ronda 
                                 AND ida.id_competicion = vuelta.id_competicion
                                 AND ida.partido_vuelta = 0
                                 AND vuelta.partido_vuelta = 1
                                 AND ida.id_equipo_local = vuelta.id_equipo_visitante
                                 AND ida.id_equipo_visitante = vuelta.id_equipo_local
                                 WHERE ida.id_ronda = @IdRonda AND ida.id_competicion = @IdCompeticion AND ida.id_manager = @IdManager";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdRonda", idRonda);
                    cmd.Parameters.AddWithValue("@IdCompeticion", idCompeticion);
                    cmd.Parameters.AddWithValue("@IdManager", idManager);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idaLocal = Convert.ToInt32(reader["goles_ida_local"]);
                            int idaVisitante = Convert.ToInt32(reader["goles_ida_visitante"]);
                            int vueltaLocal = Convert.ToInt32(reader["goles_vuelta_local"]);
                            int vueltaVisitante = Convert.ToInt32(reader["goles_vuelta_visitante"]);

                            int equipoA = Convert.ToInt32(reader["equipo_ida_local"]);
                            int equipoB = Convert.ToInt32(reader["equipo_ida_visitante"]);

                            int globalA = idaLocal + vueltaVisitante;
                            int globalB = idaVisitante + vueltaLocal;

                            int clasificadoId;

                            if (globalA > globalB)
                                clasificadoId = equipoA;
                            else if (globalB > globalA)
                                clasificadoId = equipoB;
                            else
                            {
                                // Desempate (penales, sorteo, etc.)
                                // Aquí puedes hacer lo que prefieras, por ejemplo:
                                clasificadoId = new Random().Next(0, 2) == 0 ? equipoA : equipoB;
                            }

                            // Obtener objeto Equipo (ejemplo simple si tienes un método GetEquipoPorId)
                            // Instancias de la LOGICA
                            EquipoLogica _logicaEquipo = new EquipoLogica();
                            Equipo clasificado = _logicaEquipo.ListarDetallesEquipo(clasificadoId);
                            clasificados.Add(clasificado);
                        }
                    }
                }
            }

            return clasificados;
        }

        // ----------------------------------------------------------------------- Método que devuelve la final de Copa Nacional
        public Partido ObtenerFinalCopa()
        {
            Partido partido = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT id_partido,
                                            fecha, 
                                            id_ronda,
                                            partido_vuelta,
                                            id_equipo_local, 
                                            id_equipo_visitante,
                                            goles_local,
                                            goles_visitante,
                                            id_competicion
                                     FROM partidos_copaNacional
                                     WHERE id_ronda = 5";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                partido = new Partido
                                {
                                    IdPartido = Convert.ToInt32(reader["id_partido"]),
                                    FechaPartido = reader["fecha"] != DBNull.Value && DateTime.TryParse(reader["fecha"].ToString(), out DateTime fecha)
                                                   ? fecha
                                                   : DateTime.Parse("2000-01-01"),
                                    Ronda = reader["id_ronda"] != DBNull.Value ? Convert.ToInt32(reader["id_ronda"]) : 0,
                                    PartidoVuelta = reader["partido_vuelta"] != DBNull.Value ? Convert.ToInt32(reader["partido_vuelta"]) : 0,
                                    IdEquipoLocal = Convert.ToInt32(reader["id_equipo_local"]),
                                    IdEquipoVisitante = Convert.ToInt32(reader["id_equipo_visitante"]),
                                    GolesLocal = reader["goles_local"] != DBNull.Value ? Convert.ToInt32(reader["goles_local"]) : 0,
                                    GolesVisitante = reader["goles_visitante"] != DBNull.Value ? Convert.ToInt32(reader["goles_visitante"]) : 0,
                                    IdCompeticion = reader["id_competicion"] != DBNull.Value ? Convert.ToInt32(reader["id_competicion"]) : 0
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return partido;
        }

        // ----------------------------------------------------------------- Metodo que devuelve la ultima ronda de mi equipo en Copa nacional
        public string ObtenerUltimaRondaEquipo(int equipo)
        {
            string nombre = "";
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();
                string query = "SELECT r.nombre " +
                               "FROM partidos_copaNacional p " +
                               "JOIN rondas_copaNacional r ON r.id_ronda = p.id_ronda " +
                               "WHERE id_equipo_local = @IdEquipo OR id_equipo_visitante = @IdEquipo " +
                               "ORDER BY p.id_ronda DESC " +
                               "LIMIT 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nombre = reader["nombre"].ToString();
                        }
                    }
                }
            }
            return nombre;
        }
    }
}
