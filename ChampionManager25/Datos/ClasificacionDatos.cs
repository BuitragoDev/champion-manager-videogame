using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class ClasificacionDatos : Conexion
    {
        // ===================================================================== Método para Rellenar la Clasificación
        public void RellenarClasificacion(int competicion, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta para obtener los ID de los equipos de la competición
                    string queryEquipos = @"SELECT id_equipo FROM equipos WHERE id_competicion = @competicion";
                    List<int> equipos = new List<int>();

                    using (SQLiteCommand command = new SQLiteCommand(queryEquipos, conn))
                    {
                        command.Parameters.AddWithValue("@competicion", competicion);
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                equipos.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // Insertar cada equipo en la tabla clasificacion
                    string queryInsert = @"INSERT INTO clasificacion (id_equipo, id_manager) VALUES (@idEquipo, @idManager)";

                    using (SQLiteCommand insertCommand = new SQLiteCommand(queryInsert, conn))
                    {
                        foreach (int idEquipo in equipos)
                        {
                            insertCommand.Parameters.Clear();
                            insertCommand.Parameters.AddWithValue("@idEquipo", idEquipo);
                            insertCommand.Parameters.AddWithValue("@idManager", manager);
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


        // ===================================================================== Método para Mostrar la Clasificacion
        public List<Clasificacion> MostrarClasificacion(int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
                                c.id_equipo AS IdEquipo,
                                c.jugados AS Jugados,
                                c.ganados AS Ganados,
                                c.empatados AS Empatados,
                                c.perdidos AS Perdidos,
                                c.puntos AS Puntos,
                                c.local_victorias AS LocalVictorias,
                                c.local_derrotas AS LocalDerrotas,
                                c.visitante_victorias AS VisitanteVictorias,
                                c.visitante_derrotas AS VisitanteDerrotas,
                                c.goles_favor AS GolesFavor,
                                c.goles_contra AS GolesContra,
                                c.racha AS Racha,
                                e.nombre AS NombreEquipo
                             FROM clasificacion c
                             INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                             WHERE e.id_competicion = @competicion AND c.id_manager = @manager
                             ORDER BY c.puntos DESC";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@competicion", competicion);
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clasificaciones.Add(new Clasificacion
                                {
                                    Posicion = reader.GetInt32(0),
                                    IdEquipo = reader.GetInt32(1),
                                    Jugados = reader.GetInt32(2),
                                    Ganados = reader.GetInt32(3),
                                    Empatados = reader.GetInt32(4),
                                    Perdidos = reader.GetInt32(5),
                                    Puntos = reader.GetInt32(6),
                                    LocalVictorias = reader.GetInt32(7),
                                    LocalDerrotas = reader.GetInt32(8),
                                    VisitanteVictorias = reader.GetInt32(9),
                                    VisitanteDerrotas = reader.GetInt32(10),
                                    GolesFavor = reader.GetInt32(11),
                                    GolesContra = reader.GetInt32(12),
                                    Racha = reader.GetInt32(13),
                                    NombreEquipo = reader.GetString(14)
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

            return clasificaciones;
        }


        // ===================================================================== Método para Crear el Objeto de la Clasificacion de un equipo
        public Clasificacion? MostrarClasificacionPorEquipo(int equipo, int manager)
        {
            Clasificacion? clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
                                        c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_equipo = @IdEquipo AND c.id_manager = @manager
                                     ORDER BY c.puntos DESC";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdEquipo", equipo);
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    Posicion = reader.GetInt32(0),
                                    IdEquipo = reader.GetInt32(1),
                                    Jugados = reader.GetInt32(2),
                                    Ganados = reader.GetInt32(3),
                                    Empatados = reader.GetInt32(4),
                                    Perdidos = reader.GetInt32(5),
                                    Puntos = reader.GetInt32(6),
                                    LocalVictorias = reader.GetInt32(7),
                                    LocalDerrotas = reader.GetInt32(8),
                                    VisitanteVictorias = reader.GetInt32(9),
                                    VisitanteDerrotas = reader.GetInt32(10),
                                    GolesFavor = reader.GetInt32(11),
                                    GolesContra = reader.GetInt32(12),
                                    Racha = reader.GetInt32(13),
                                    NombreEquipo = reader.GetString(14)
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
            return clasificacionEquipo;
        }

        // ===================================================================== Método para Mostrar el equipo con MAS GOLES A FAVOR
        public Clasificacion MostrarMejorAtaque(int manager)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_favor DESC
                                     LIMIT 1";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    IdEquipo = reader.GetInt32(0),
                                    Jugados = reader.GetInt32(1),
                                    Ganados = reader.GetInt32(2),
                                    Empatados = reader.GetInt32(3),
                                    Perdidos = reader.GetInt32(4),
                                    Puntos = reader.GetInt32(5),
                                    LocalVictorias = reader.GetInt32(6),
                                    LocalDerrotas = reader.GetInt32(7),
                                    VisitanteVictorias = reader.GetInt32(8),
                                    VisitanteDerrotas = reader.GetInt32(9),
                                    GolesFavor = reader.GetInt32(10),
                                    GolesContra = reader.GetInt32(11),
                                    Racha = reader.GetInt32(12),
                                    NombreEquipo = reader.GetString(13)
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
            return clasificacionEquipo;
        }

        // ===================================================================== Método para Mostrar el equipo con MAS MENOS GOLES EN CONTRA
        public Clasificacion MostrarMejorDefensa(int manager)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_contra ASC
                                     LIMIT 1";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    IdEquipo = reader.GetInt32(0),
                                    Jugados = reader.GetInt32(1),
                                    Ganados = reader.GetInt32(2),
                                    Empatados = reader.GetInt32(3),
                                    Perdidos = reader.GetInt32(4),
                                    Puntos = reader.GetInt32(5),
                                    LocalVictorias = reader.GetInt32(6),
                                    LocalDerrotas = reader.GetInt32(7),
                                    VisitanteVictorias = reader.GetInt32(8),
                                    VisitanteDerrotas = reader.GetInt32(9),
                                    GolesFavor = reader.GetInt32(10),
                                    GolesContra = reader.GetInt32(11),
                                    Racha = reader.GetInt32(12),
                                    NombreEquipo = reader.GetString(13)
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
            return clasificacionEquipo;
        }

        // ===================================================================== Método para Mostrar el equipo con la MEJOR RACHA
        public Clasificacion MostrarMejorRacha(int manager)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.racha DESC
                                     LIMIT 1";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    IdEquipo = reader.GetInt32(0),
                                    Jugados = reader.GetInt32(1),
                                    Ganados = reader.GetInt32(2),
                                    Empatados = reader.GetInt32(3),
                                    Perdidos = reader.GetInt32(4),
                                    Puntos = reader.GetInt32(5),
                                    LocalVictorias = reader.GetInt32(6),
                                    LocalDerrotas = reader.GetInt32(7),
                                    VisitanteVictorias = reader.GetInt32(8),
                                    VisitanteDerrotas = reader.GetInt32(9),
                                    GolesFavor = reader.GetInt32(10),
                                    GolesContra = reader.GetInt32(11),
                                    Racha = reader.GetInt32(12),
                                    NombreEquipo = reader.GetString(13)
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
            return clasificacionEquipo;
        }

        // ===================================================================== Método para Mostrar el equipo con MAS VICTORIAS COMO LOCAL
        public Clasificacion MostrarMejorEquipoLocal(int manager)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.local_victorias DESC
                                     LIMIT 1";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    IdEquipo = reader.GetInt32(0),
                                    Jugados = reader.GetInt32(1),
                                    Ganados = reader.GetInt32(2),
                                    Empatados = reader.GetInt32(3),
                                    Perdidos = reader.GetInt32(4),
                                    Puntos = reader.GetInt32(5),
                                    LocalVictorias = reader.GetInt32(6),
                                    LocalDerrotas = reader.GetInt32(7),
                                    VisitanteVictorias = reader.GetInt32(8),
                                    VisitanteDerrotas = reader.GetInt32(9),
                                    GolesFavor = reader.GetInt32(10),
                                    GolesContra = reader.GetInt32(11),
                                    Racha = reader.GetInt32(12),
                                    NombreEquipo = reader.GetString(13)
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
            return clasificacionEquipo;
        }

        // ===================================================================== Método para Mostrar el equipo con MAS VICTORIAS COMO LOCAL
        public Clasificacion MostrarMejorEquipoVisitante(int manager)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT c.id_equipo AS IdEquipo,
                                        c.jugados AS Jugados,
                                        c.ganados AS Ganados,
                                        c.empatados AS Empatados,
                                        c.perdidos AS Perdidos,
                                        c.puntos AS Puntos,
                                        c.local_victorias AS LocalVictorias,
                                        c.local_derrotas AS LocalDerrotas,
                                        c.visitante_victorias AS VisitanteVictorias,
                                        c.visitante_derrotas AS VisitanteDerrotas,
                                        c.goles_favor AS PuntosFavor,
                                        c.goles_contra AS PuntosContra,
                                        c.racha AS Racha,
                                        e.nombre AS NombreEquipo
                                     FROM clasificacion c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.visitante_victorias DESC
                                     LIMIT 1";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@manager", manager);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                clasificacionEquipo = new Clasificacion
                                {
                                    IdEquipo = reader.GetInt32(0),
                                    Jugados = reader.GetInt32(1),
                                    Ganados = reader.GetInt32(2),
                                    Empatados = reader.GetInt32(3),
                                    Perdidos = reader.GetInt32(4),
                                    Puntos = reader.GetInt32(5),
                                    LocalVictorias = reader.GetInt32(6),
                                    LocalDerrotas = reader.GetInt32(7),
                                    VisitanteVictorias = reader.GetInt32(8),
                                    VisitanteDerrotas = reader.GetInt32(9),
                                    GolesFavor = reader.GetInt32(10),
                                    GolesContra = reader.GetInt32(11),
                                    Racha = reader.GetInt32(12),
                                    NombreEquipo = reader.GetString(13)
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
            return clasificacionEquipo;
        }
    }
}
