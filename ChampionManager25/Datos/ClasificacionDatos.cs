using ChampionManager25.Entidades;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Datos
{
    public class ClasificacionDatos : Conexion
    {
        // --------------------------------------------------------------------- Método para Rellenar la Clasificación de Primera Division
        public void RellenarClasificacion(int competicion, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

                    // Eliminar clasificación anterior del manager
                    string queryDelete = @"DELETE FROM clasificacion WHERE id_manager = @idManager";
                    using (SQLiteCommand deleteCommand = new SQLiteCommand(queryDelete, conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@idManager", manager);
                        deleteCommand.ExecuteNonQuery();
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

        // --------------------------------------------------------------------- Método para Rellenar la Clasificación de Segunda Division
        public void RellenarClasificacion2(int competicion, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

                    // Eliminar clasificación anterior del manager
                    string queryDelete = @"DELETE FROM clasificacion2 WHERE id_manager = @idManager";
                    using (SQLiteCommand deleteCommand = new SQLiteCommand(queryDelete, conn))
                    {
                        deleteCommand.Parameters.AddWithValue("@idManager", manager);
                        deleteCommand.ExecuteNonQuery();
                    }

                    // Insertar cada equipo en la tabla clasificacion
                    string queryInsert = @"INSERT INTO clasificacion2 (id_equipo, id_manager) VALUES (@idEquipo, @idManager)";
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

        // --------------------------------------------------------------------- Método para Rellenar la Clasificación de Copa de Europa 1
        public void RellenarClasificacionEuropa1(int manager, List<Equipo> equiposEuropa1)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar cada equipo en la tabla clasificacion
                    string query = @"INSERT INTO clasificacion_europa1 (id_equipo, id_manager) VALUES (@idEquipo, @idManager)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(query, conn))
                    {
                        foreach (Equipo equipo in equiposEuropa1)
                        {
                            insertCommand.Parameters.Clear();
                            insertCommand.Parameters.AddWithValue("@idEquipo", equipo.IdEquipo);
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

        // --------------------------------------------------------------------- Método para Rellenar la Clasificación de Copa de Europa 2
        public void RellenarClasificacionEuropa2(int manager, List<Equipo> equiposEuropa2)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar cada equipo en la tabla clasificacion
                    string query = @"INSERT INTO clasificacion_europa2 (id_equipo, id_manager) VALUES (@idEquipo, @idManager)";
                    using (SQLiteCommand insertCommand = new SQLiteCommand(query, conn))
                    {
                        foreach (Equipo equipo in equiposEuropa2)
                        {
                            insertCommand.Parameters.Clear();
                            insertCommand.Parameters.AddWithValue("@idEquipo", equipo.IdEquipo);
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


        // ------------------------------------------------------------------- Método para Mostrar la Clasificacion Liga 1
        public List<Clasificacion> MostrarClasificacion(int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();

            string tablaClasificacion;

            // Determina la tabla según el valor de la competición
            if (competicion == 1)
                tablaClasificacion = "clasificacion";
            else if (competicion == 2)
                tablaClasificacion = "clasificacion2";
            else
                throw new ArgumentException("Competición no válida");

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = $@"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC) AS Posicion,
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
                                      FROM {tablaClasificacion} c
                                      INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                      WHERE e.id_competicion = @competicion AND c.id_manager = @manager
                                      ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC";

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

        // ------------------------------------------------------------------- Método para Mostrar la Clasificacion Liga 2
        public List<Clasificacion> MostrarClasificacion2(int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC) AS Posicion,
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
                             FROM clasificacion2 c
                             INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                             WHERE e.id_competicion = @competicion AND c.id_manager = @manager
                             ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC";

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

        // ------------------------------------------------------------------- Método para Mostrar la Clasificacion Europa 1
        public List<Clasificacion> MostrarClasificacionCopaEuropa1(int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC) AS Posicion,
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
                             FROM clasificacion_europa1 c
                             INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                             WHERE c.id_manager = @manager
                             ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC";

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

        // ------------------------------------------------------------------- Método para Mostrar la Clasificacion Europa 2
        public List<Clasificacion> MostrarClasificacionCopaEuropa2(int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = new List<Clasificacion>();
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC) AS Posicion,
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
                             FROM clasificacion_europa2 c
                             INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                             WHERE c.id_manager = @manager
                             ORDER BY c.puntos DESC, (c.goles_favor - c.goles_contra) DESC";

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
        public Clasificacion? MostrarClasificacionPorEquipo(int equipo, int manager, int competicion)
        {
            Clasificacion? clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = null;
                    if (competicion == 1)
                    {
                        query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_equipo = @IdEquipo AND c.id_manager = @manager
                                     ORDER BY c.puntos DESC";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_equipo = @IdEquipo AND c.id_manager = @manager
                                     ORDER BY c.puntos DESC";
                    }
                    else
                    {
                        query = @"SELECT ROW_NUMBER() OVER (ORDER BY c.puntos DESC) AS Posicion,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_equipo = @IdEquipo AND c.id_manager = @manager
                                     ORDER BY c.puntos DESC";
                    }
                        

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
        public Clasificacion MostrarMejorAtaque(int manager, int competicion)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_favor DESC
                                     LIMIT 1";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_favor DESC
                                     LIMIT 1";
                    }
                    else
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_favor DESC
                                     LIMIT 1";
                    }

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
        public Clasificacion MostrarMejorDefensa(int manager, int competicion)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_contra ASC
                                     LIMIT 1";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_contra ASC
                                     LIMIT 1";
                    }
                    else
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.goles_contra ASC
                                     LIMIT 1";
                    }
                        
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
        public Clasificacion MostrarMejorRacha(int manager, int competicion)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.racha DESC
                                     LIMIT 1";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.racha DESC
                                     LIMIT 1";
                    }
                    else
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.racha DESC
                                     LIMIT 1";
                    }

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
        public Clasificacion MostrarMejorEquipoLocal(int manager, int competicion)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.local_victorias DESC
                                     LIMIT 1";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.local_victorias DESC
                                     LIMIT 1";
                    }
                    else
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.local_victorias DESC
                                     LIMIT 1";
                    }
 
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
        public Clasificacion MostrarMejorEquipoVisitante(int manager, int competicion)
        {
            Clasificacion clasificacionEquipo = null; // Cambiado a null para detectar si no se encuentra
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                    }
                    else if (competicion == 5)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa1 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.visitante_victorias DESC
                                     LIMIT 1";
                    }
                    else if (competicion == 6)
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion_europa2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.visitante_victorias DESC
                                     LIMIT 1";
                    }
                    else
                    {
                        query = @"SELECT c.id_equipo AS IdEquipo,
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
                                     FROM clasificacion2 c
                                     INNER JOIN equipos e ON c.id_equipo = e.id_equipo
                                     WHERE c.id_manager = @manager
                                     ORDER BY c.visitante_victorias DESC
                                     LIMIT 1";
                    }
                        
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

        // ===================================================================== Método para actualizar la Clasificacion
        public void ActualizarClasificacion(Clasificacion clasificacion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE clasificacion 
                                    SET jugados = jugados + @Jugados,
                                        ganados = ganados + @Ganados,
                                        empatados = empatados + @Empatados,
                                        perdidos = perdidos + @Perdidos,
                                        puntos = puntos + @Puntos,
                                        local_victorias = local_victorias + @LocalVictorias,
                                        local_derrotas = local_derrotas + @LocalDerrotas,
                                        visitante_victorias = visitante_victorias + @VisitanteVictorias,
                                        visitante_derrotas = visitante_derrotas + @VisitanteDerrotas,
                                        goles_favor = goles_favor + @GolesFavor,
                                        goles_contra = goles_contra + @GolesContra,
                                        racha = CASE
                                                    WHEN @Racha = 0 THEN 0
                                                    WHEN (racha > 0 AND @Racha > 0) OR (racha < 0 AND @Racha < 0) THEN racha + @Racha
                                                    ELSE @Racha
                                                END
                                WHERE id_equipo = @IdEquipo";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jugados", clasificacion.Jugados);
                        cmd.Parameters.AddWithValue("@Ganados", clasificacion.Ganados);
                        cmd.Parameters.AddWithValue("@Empatados", clasificacion.Empatados);
                        cmd.Parameters.AddWithValue("@Perdidos", clasificacion.Perdidos);
                        cmd.Parameters.AddWithValue("@Puntos", clasificacion.Puntos);
                        cmd.Parameters.AddWithValue("@LocalVictorias", clasificacion.LocalVictorias);
                        cmd.Parameters.AddWithValue("@LocalDerrotas", clasificacion.LocalDerrotas);
                        cmd.Parameters.AddWithValue("@VisitanteVictorias", clasificacion.VisitanteVictorias);
                        cmd.Parameters.AddWithValue("@VisitanteDerrotas", clasificacion.VisitanteDerrotas);
                        cmd.Parameters.AddWithValue("@GolesFavor", clasificacion.GolesFavor);
                        cmd.Parameters.AddWithValue("@GolesContra", clasificacion.GolesContra);
                        cmd.Parameters.AddWithValue("@Racha", clasificacion.Racha);
                        cmd.Parameters.AddWithValue("@IdEquipo", clasificacion.IdEquipo);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        public void ActualizarClasificacion2(Clasificacion clasificacion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE clasificacion2 
                                    SET jugados = jugados + @Jugados,
                                        ganados = ganados + @Ganados,
                                        empatados = empatados + @Empatados,
                                        perdidos = perdidos + @Perdidos,
                                        puntos = puntos + @Puntos,
                                        local_victorias = local_victorias + @LocalVictorias,
                                        local_derrotas = local_derrotas + @LocalDerrotas,
                                        visitante_victorias = visitante_victorias + @VisitanteVictorias,
                                        visitante_derrotas = visitante_derrotas + @VisitanteDerrotas,
                                        goles_favor = goles_favor + @GolesFavor,
                                        goles_contra = goles_contra + @GolesContra,
                                        racha = CASE
                                                    WHEN @Racha = 0 THEN 0
                                                    WHEN (racha > 0 AND @Racha > 0) OR (racha < 0 AND @Racha < 0) THEN racha + @Racha
                                                    ELSE @Racha
                                                END
                                WHERE id_equipo = @IdEquipo";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jugados", clasificacion.Jugados);
                        cmd.Parameters.AddWithValue("@Ganados", clasificacion.Ganados);
                        cmd.Parameters.AddWithValue("@Empatados", clasificacion.Empatados);
                        cmd.Parameters.AddWithValue("@Perdidos", clasificacion.Perdidos);
                        cmd.Parameters.AddWithValue("@Puntos", clasificacion.Puntos);
                        cmd.Parameters.AddWithValue("@LocalVictorias", clasificacion.LocalVictorias);
                        cmd.Parameters.AddWithValue("@LocalDerrotas", clasificacion.LocalDerrotas);
                        cmd.Parameters.AddWithValue("@VisitanteVictorias", clasificacion.VisitanteVictorias);
                        cmd.Parameters.AddWithValue("@VisitanteDerrotas", clasificacion.VisitanteDerrotas);
                        cmd.Parameters.AddWithValue("@GolesFavor", clasificacion.GolesFavor);
                        cmd.Parameters.AddWithValue("@GolesContra", clasificacion.GolesContra);
                        cmd.Parameters.AddWithValue("@Racha", clasificacion.Racha);
                        cmd.Parameters.AddWithValue("@IdEquipo", clasificacion.IdEquipo);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // ------------------------------------------------------------------- Metodo para actualizar la clasificacion de la Copa de Europa 1
        public void ActualizarClasificacionEuropa1(Clasificacion clasificacion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE clasificacion_europa1 
                                    SET jugados = jugados + @Jugados,
                                        ganados = ganados + @Ganados,
                                        empatados = empatados + @Empatados,
                                        perdidos = perdidos + @Perdidos,
                                        puntos = puntos + @Puntos,
                                        local_victorias = local_victorias + @LocalVictorias,
                                        local_derrotas = local_derrotas + @LocalDerrotas,
                                        visitante_victorias = visitante_victorias + @VisitanteVictorias,
                                        visitante_derrotas = visitante_derrotas + @VisitanteDerrotas,
                                        goles_favor = goles_favor + @GolesFavor,
                                        goles_contra = goles_contra + @GolesContra,
                                        racha = CASE
                                                    WHEN @Racha = 0 THEN 0
                                                    WHEN (racha > 0 AND @Racha > 0) OR (racha < 0 AND @Racha < 0) THEN racha + @Racha
                                                    ELSE @Racha
                                                END
                                WHERE id_equipo = @IdEquipo";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jugados", clasificacion.Jugados);
                        cmd.Parameters.AddWithValue("@Ganados", clasificacion.Ganados);
                        cmd.Parameters.AddWithValue("@Empatados", clasificacion.Empatados);
                        cmd.Parameters.AddWithValue("@Perdidos", clasificacion.Perdidos);
                        cmd.Parameters.AddWithValue("@Puntos", clasificacion.Puntos);
                        cmd.Parameters.AddWithValue("@LocalVictorias", clasificacion.LocalVictorias);
                        cmd.Parameters.AddWithValue("@LocalDerrotas", clasificacion.LocalDerrotas);
                        cmd.Parameters.AddWithValue("@VisitanteVictorias", clasificacion.VisitanteVictorias);
                        cmd.Parameters.AddWithValue("@VisitanteDerrotas", clasificacion.VisitanteDerrotas);
                        cmd.Parameters.AddWithValue("@GolesFavor", clasificacion.GolesFavor);
                        cmd.Parameters.AddWithValue("@GolesContra", clasificacion.GolesContra);
                        cmd.Parameters.AddWithValue("@Racha", clasificacion.Racha);
                        cmd.Parameters.AddWithValue("@IdEquipo", clasificacion.IdEquipo);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // ------------------------------------------------------------------- Metodo para actualizar la clasificacion de la Copa de Europa 2
        public void ActualizarClasificacionEuropa2(Clasificacion clasificacion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE clasificacion_europa2 
                                    SET jugados = jugados + @Jugados,
                                        ganados = ganados + @Ganados,
                                        empatados = empatados + @Empatados,
                                        perdidos = perdidos + @Perdidos,
                                        puntos = puntos + @Puntos,
                                        local_victorias = local_victorias + @LocalVictorias,
                                        local_derrotas = local_derrotas + @LocalDerrotas,
                                        visitante_victorias = visitante_victorias + @VisitanteVictorias,
                                        visitante_derrotas = visitante_derrotas + @VisitanteDerrotas,
                                        goles_favor = goles_favor + @GolesFavor,
                                        goles_contra = goles_contra + @GolesContra,
                                        racha = CASE
                                                    WHEN @Racha = 0 THEN 0
                                                    WHEN (racha > 0 AND @Racha > 0) OR (racha < 0 AND @Racha < 0) THEN racha + @Racha
                                                    ELSE @Racha
                                                END
                                WHERE id_equipo = @IdEquipo";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Jugados", clasificacion.Jugados);
                        cmd.Parameters.AddWithValue("@Ganados", clasificacion.Ganados);
                        cmd.Parameters.AddWithValue("@Empatados", clasificacion.Empatados);
                        cmd.Parameters.AddWithValue("@Perdidos", clasificacion.Perdidos);
                        cmd.Parameters.AddWithValue("@Puntos", clasificacion.Puntos);
                        cmd.Parameters.AddWithValue("@LocalVictorias", clasificacion.LocalVictorias);
                        cmd.Parameters.AddWithValue("@LocalDerrotas", clasificacion.LocalDerrotas);
                        cmd.Parameters.AddWithValue("@VisitanteVictorias", clasificacion.VisitanteVictorias);
                        cmd.Parameters.AddWithValue("@VisitanteDerrotas", clasificacion.VisitanteDerrotas);
                        cmd.Parameters.AddWithValue("@GolesFavor", clasificacion.GolesFavor);
                        cmd.Parameters.AddWithValue("@GolesContra", clasificacion.GolesContra);
                        cmd.Parameters.AddWithValue("@Racha", clasificacion.Racha);
                        cmd.Parameters.AddWithValue("@IdEquipo", clasificacion.IdEquipo);
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al añadir el equipo al Manager: " + ex.Message);
            }
        }

        // ===================================================================== Método para RESETEAR la Clasificación
        public void ResetearClasificacion(int competicion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query;
                    if (competicion == 1)
                    {
                        query = @"UPDATE clasificacion 
                                  SET jugados = 0,
                                    ganados = 0,
                                    empatados = 0,
                                    perdidos = 0,
                                    puntos = 0,
                                    local_victorias = 0,
                                    local_derrotas = 0,
                                    visitante_victorias = 0,
                                    visitante_derrotas = 0,
                                    goles_favor = 0,
                                    goles_contra = 0,
                                    racha = 0";
                    }
                    else if (competicion == 5)
                    {
                        query = @"UPDATE clasificacion_europa1
                                  SET jugados = 0,
                                    ganados = 0,
                                    empatados = 0,
                                    perdidos = 0,
                                    puntos = 0,
                                    local_victorias = 0,
                                    local_derrotas = 0,
                                    visitante_victorias = 0,
                                    visitante_derrotas = 0,
                                    goles_favor = 0,
                                    goles_contra = 0,
                                    racha = 0";
                    }
                    else if (competicion == 6)
                    {
                        query = @"UPDATE clasificacion_europa2 
                                  SET jugados = 0,
                                    ganados = 0,
                                    empatados = 0,
                                    perdidos = 0,
                                    puntos = 0,
                                    local_victorias = 0,
                                    local_derrotas = 0,
                                    visitante_victorias = 0,
                                    visitante_derrotas = 0,
                                    goles_favor = 0,
                                    goles_contra = 0,
                                    racha = 0";
                    }
                    else
                    {
                        query = @"UPDATE clasificacion2 
                                  SET jugados = 0,
                                    ganados = 0,
                                    empatados = 0,
                                    perdidos = 0,
                                    puntos = 0,
                                    local_victorias = 0,
                                    local_derrotas = 0,
                                    visitante_victorias = 0,
                                    visitante_derrotas = 0,
                                    goles_favor = 0,
                                    goles_contra = 0,
                                    racha = 0";
                    }
                        
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
