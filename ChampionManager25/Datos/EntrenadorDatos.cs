using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

        // ===================================================================== Método para mostrar un entrenador
        public Entrenador MostrarEntrenador(int id)
        {
            Entrenador oEntrenador = null; // Inicializamos la variable de tipo Jugador como null

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT id_entrenador, nombre, apellido, tactica_favorita, puntos, reputacion, id_equipo,
                                     nombre || ' ' || apellido AS nombre_completo 
                                     FROM entrenadores
                                     WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Crear un nuevo objeto Jugador con los datos del jugador con mayor media
                                oEntrenador = new Entrenador
                                {
                                    IdEntrenador = reader.GetInt32(reader.GetOrdinal("id_entrenador")),
                                    Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                                    Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                                    NombreCompleto = reader.GetString(reader.GetOrdinal("nombre_completo")),
                                    TacticaFavorita = reader.GetString(reader.GetOrdinal("tactica_favorita")),
                                    Puntos = reader.GetInt32(reader.GetOrdinal("puntos")),
                                    Reputacion = reader.GetInt32(reader.GetOrdinal("reputacion")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo"))
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
           
            return oEntrenador;
        }

        // ---------------------------------------------------------------- Método que actualiza los puntos de los entrenadores
        public void ActualizarResultadoManager(int entrenador, int puntos)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "UPDATE entrenadores SET " +
                                   "puntos = puntos + @Puntos " +
                                   "WHERE id_entrenador = @Entrenador;";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Puntos", puntos);
                        cmd.Parameters.AddWithValue("@Entrenador", entrenador); // Parámetro para el idManager
                        cmd.ExecuteNonQuery(); // Ejecuta la consulta
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el Manager: " + ex.Message);
            }
        }
    }
}
