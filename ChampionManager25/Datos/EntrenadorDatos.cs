using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class EntrenadorDatos : Conexion
    {
        // ======================================================= Método para obtener el ranking de entrenadores
        public List<Entrenador> obtenerRankingEntrenadores(int miManager)
        {
            List<Entrenador> rankingEntrenadores = new List<Entrenador>();

            // La consulta SQL para obtener el ranking de los entrenadores
            string query = @"SELECT 
                                ROW_NUMBER() OVER (ORDER BY e.puntos DESC) AS posicion, 
                                e.nombre || ' ' || e.apellido AS nombre_completo, 
                                eq.nombre AS nombre_equipo, 
                                e.reputacion, 
                                e.puntos
                             FROM 
                                (SELECT id_entrenador, nombre, apellido, reputacion, puntos, id_equipo
                                 FROM entrenadores
                                 UNION
                                 SELECT id_manager AS id_entrenador, nombre, apellido, reputacion, puntos, id_equipo
                                 FROM managers
                                 WHERE id_manager = @miManager) e
                             JOIN equipos eq ON e.id_equipo = eq.id_equipo
                             GROUP BY e.nombre, e.apellido, e.id_equipo
                             ORDER BY e.puntos DESC"
            ;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar el parámetro al comando
                        cmd.Parameters.AddWithValue("@miManager", miManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            // Leer los resultados y añadirlos a la lista
                            while (reader.Read())
                            {
                                Entrenador entrenador = new Entrenador
                                {
                                    Posicion = reader.GetInt32(0),
                                    NombreCompleto = reader.GetString(1),
                                    NombreEquipo = reader.GetString(2),
                                    Reputacion = reader.GetInt32(3),
                                    Puntos = reader.GetInt32(4)
                                };
                                rankingEntrenadores.Add(entrenador);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al obtener el ranking de entrenadores: " + ex.Message);
            }

            return rankingEntrenadores;
        }
    }
}
