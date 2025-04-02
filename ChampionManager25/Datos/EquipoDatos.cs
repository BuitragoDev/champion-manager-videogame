using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;

namespace ChampionManager25.Datos
{
    public class EquipoDatos : Conexion
    {
        // ===================================================================== Método para Mostrar los equipos de una competición
        public List<Equipo> ListarEquiposCompeticion(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                    e.id_equipo,
                    e.nombre,
                    e.nombre_corto,
                    e.presidente,
                    e.ciudad,
                    e.estadio,
                    e.objetivo,
                    e.aforo,
                    e.reputacion,
                    e.rival,
                    t.nombre || ' ' || t.apellido AS entrenador,
                    t.reputacion AS reputacion_entrenador
                FROM 
                    equipos e
                LEFT JOIN 
                    entrenadores t
                ON 
                    e.id_equipo = t.id_equipo
                WHERE 
                    e.id_competicion = @idCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                    Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                    Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                    Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                    Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                    Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                    Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                    Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                    Entrenador = dr["entrenador"] as string ?? "Sin asignar",
                                    ReputacionEntrenador = dr["reputacion_entrenador"] != DBNull.Value ? Convert.ToInt32(dr["reputacion_entrenador"]) : 0
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

            return oEquipo;
        }

        // ===================================================================== Método para Mostrar los datos de un EQUIPO por su ID
        public Equipo ListarDetallesEquipo(int id)
        {
            Equipo oEquipo = new Equipo();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT id_equipo, nombre, nombre_corto, presidente, ciudad, estadio, aforo, reputacion, objetivo, rival, id_competicion
                                 FROM equipos WHERE id_equipo = @idEquipo";

                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idEquipo", id);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            oEquipo = new Equipo()
                            {
                                // Usamos el operador de coalescencia nula para evitar la asignación de null
                                IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                IdCompeticion = dr["id_competicion"] != DBNull.Value ? Convert.ToInt32(dr["id_competicion"]) : 0
                            };
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return oEquipo;
        }

        // ===================================================================== Método para Mostrar el Listado de todos los equipos
        public List<Equipo> ListarEquipos(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta con JOIN para obtener datos de entrenadores
                    string query = @"
                SELECT 
                    id_equipo,
                    nombre,
                    nombre_corto,
                    presidente,
                    ciudad,
                    estadio,
                    objetivo,
                    aforo,
                    reputacion,
                    rival,
                FROM 
                    equipos
                WHERE 
                    id_competicion = @IdCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"] as string ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"] as string ?? string.Empty,
                                    Presidente = dr["presidente"] as string ?? string.Empty,
                                    Ciudad = dr["ciudad"] as string ?? string.Empty,
                                    Estadio = dr["pabellon"] as string ?? string.Empty,
                                    Objetivo = dr["objetivo"] as string ?? string.Empty,
                                    Aforo = dr["capacidad"] != DBNull.Value ? Convert.ToInt32(dr["capacidad"]) : 0,
                                    Reputacion = dr["reputacion"] != DBNull.Value ? Convert.ToInt32(dr["reputacion"]) : 0,
                                    Rival = dr["rival"] != DBNull.Value ? Convert.ToInt32(dr["rival"]) : 0,
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

            return oEquipo;
        }

        // ===================================================================== Método que devuelve la asistencia a un partido
        public int CalcularAsistencia(int idEquipoLocal)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta
                    string query = "SELECT aforo, reputacion FROM equipos WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipoLocal);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int aforo = Convert.ToInt32(reader["aforo"]);
                                int reputacion = Convert.ToInt32(reader["reputacion"]);
                                return (int)(aforo * (reputacion / 100.0));
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return 0;
        }
    }
}
