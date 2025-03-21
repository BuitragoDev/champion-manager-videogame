using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class JugadorDatos : Conexion
    {
        private Metodos metodos;

        public JugadorDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para mostrar el jugador con más Media
        public Jugador? MejorJugador(int id)
        {
            Jugador? oJugador = null; // Inicializamos la variable de tipo Jugador como null

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT id_jugador, nombre, apellido, rol_id, velocidad, resistencia, agresividad, calidad, estado_forma, moral
                                     FROM jugadores
                                     WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            double maxMedia = -1; // Variable para almacenar la máxima media
                            int idJugadorMaximo = 0; // Variable para almacenar el id del jugador con la mayor media

                            while (reader.Read())
                            {
                                // Leer los valores de la base de datos
                                int velocidad = reader.IsDBNull(reader.GetOrdinal("velocidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("velocidad"));
                                int resistencia = reader.IsDBNull(reader.GetOrdinal("resistencia")) ? 0 : reader.GetInt32(reader.GetOrdinal("resistencia"));
                                int agresividad = reader.IsDBNull(reader.GetOrdinal("agresividad")) ? 0 : reader.GetInt32(reader.GetOrdinal("agresividad"));
                                int calidad = reader.IsDBNull(reader.GetOrdinal("calidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("calidad"));
                                int estado_forma = reader.IsDBNull(reader.GetOrdinal("estado_forma")) ? 0 : reader.GetInt32(reader.GetOrdinal("estado_forma"));
                                int moral = reader.IsDBNull(reader.GetOrdinal("moral")) ? 0 : reader.GetInt32(reader.GetOrdinal("moral"));

                                // Calcular la media
                                double media = (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0;

                                // Si la media actual es mayor que la máxima encontrada, actualizar el máximo y el jugador correspondiente
                                if (media > maxMedia)
                                {
                                    maxMedia = media;
                                    idJugadorMaximo = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                                    string apellido = reader.GetString(reader.GetOrdinal("apellido"));

                                    // Crear un nuevo objeto Jugador con los datos del jugador con mayor media
                                    oJugador = new Jugador
                                    {
                                        IdJugador = idJugadorMaximo,
                                        Nombre = nombre,
                                        Apellido = apellido,
                                        Velocidad = velocidad,
                                        Resistencia = resistencia,
                                        Agresividad = agresividad,
                                        Calidad = calidad,
                                        EstadoForma = estado_forma,
                                        Moral = moral,
                                    };
                                }
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

            return oJugador;
        }

        // ======================================================= Método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> ListadoJugadoresCompleto(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT *
                                    FROM jugadores
                                    WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
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

        // ===================================================================== Método para mostrar la Media del equipo
        public double ObtenerMediaEquipo(int idEquipo)
        {
            double media = 0;

            using (SQLiteConnection conn = new SQLiteConnection(cadena))
            {
                conn.Open();

                string query = @"SELECT velocidad, resistencia, agresividad, calidad, estado_forma, moral 
                                 FROM jugadores 
                                 WHERE id_equipo = @IdEquipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEquipo", idEquipo);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var medias = new List<double>();

                        while (reader.Read())
                        {
                            int velocidad = reader.GetInt32(0);
                            int resistencia = reader.GetInt32(1);
                            int agresividad = reader.GetInt32(2);
                            int calidad = reader.GetInt32(3);
                            int estadoForma = reader.GetInt32(4);
                            int moral = reader.GetInt32(5);

                            double mediaJugador = (velocidad + resistencia + agresividad + calidad + estadoForma + moral) / 6.0;

                            medias.Add(mediaJugador);
                        }

                        if (medias.Any())
                        {
                            media = medias.Average();
                        }
                    }
                }
            }

            return media;
        }

        // ===================================================================== Método para mostrar el numero total de jugadores del juego
        public int NumeroJugadoresTotales()
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM jugadores";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // ======================================================= Método para mostrar los datos de un jugador
        public Jugador MostrarDatosJugador(int id)
        {
            Jugador jugador = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT *
                                    FROM jugadores
                                    WHERE id_jugador = @IdJugador;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@IdJugador", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status"))
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

            return jugador;
        }

        // ===================================================================== Método que renueva el status de un jugador
        public void RenovarStatusJugador(int jugador, int rol)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET status = @Status WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Status", rol);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


        // ===================================================================== Método que cambia el equipo del jugador
        public void CambiarDeEquipo(int jugador, int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET id_equipo = @IdEquipo
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        cmd.ExecuteNonQuery();
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
