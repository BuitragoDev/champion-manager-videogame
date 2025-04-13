using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT id_jugador, nombre, apellido, rol_id, velocidad, resistencia, agresividad, calidad, estado_forma, moral, ruta_imagen
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
                                    string rutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen"));

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
                                        RutaImagen = rutaImagen
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oJugador;
        }

        // ======================================================= Método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> ListadoJugadoresCompleto(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    Entrenamiento = dr.GetInt32(dr.GetOrdinal("entrenamiento")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método para mostrar la Media del equipo
        public double ObtenerMediaEquipo(int idEquipo)
        {
            double media = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return jugador;
        }

        // ======================================================= Método para mostrar todos los jugadores de la base de datos
        public List<Jugador> MostrarListaTotalJugadores()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT * FROM jugadores";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
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
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    Entrenamiento = dr.GetInt32(dr.GetOrdinal("entrenamiento")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método que renueva el status de un jugador
        public void RenovarStatusJugador(int jugador, int rol)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


        // ===================================================================== Método que cambia el equipo del jugador
        public void CambiarDeEquipo(int jugador, int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ======================================================= Método para mostrar los titulares del equipo
        public void CrearAlineacion(string tactica, int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = "";

                    if (tactica == "5-4-1")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador; 
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador; 
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador;
                                posiciones[4] = jugadores.Where(j => j.rol_id == 4).Skip(2).Take(1).First().id_jugador; 
                                posiciones[5] = jugadores.First(j => j.rol_id == 2).id_jugador; 
                                posiciones[6] = jugadores.First(j => j.rol_id == 3).id_jugador; 
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador; 
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador; 
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador; 
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(3).Take(1).First().id_jugador; 
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador; 

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else if (tactica == "5-3-2")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador; 
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador; 
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador; 
                                posiciones[4] = jugadores.Where(j => j.rol_id == 4).Skip(2).Take(1).First().id_jugador; 
                                posiciones[5] = jugadores.First(j => j.rol_id == 2).id_jugador; 
                                posiciones[6] = jugadores.First(j => j.rol_id == 3).id_jugador; 
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador; 
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador; 
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador; 
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador; 
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Skip(1).Take(1).First().id_jugador; 

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else if (tactica == "4-5-1")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador;
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador;
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador;
                                posiciones[4] = jugadores.First(j => j.rol_id == 2).id_jugador;
                                posiciones[5] = jugadores.First(j => j.rol_id == 3).id_jugador;
                                posiciones[6] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador;
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador;
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador;
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(3).Take(1).First().id_jugador;
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(4).Take(1).First().id_jugador;
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador;

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else if (tactica == "4-4-2")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador;
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador;
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador;
                                posiciones[4] = jugadores.First(j => j.rol_id == 2).id_jugador;
                                posiciones[5] = jugadores.First(j => j.rol_id == 3).id_jugador;
                                posiciones[6] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador;
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador;
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador;
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(3).Take(1).First().id_jugador;
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador;
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Skip(1).Take(1).First().id_jugador;

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else if (tactica == "4-3-3")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador;
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador;
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador;
                                posiciones[4] = jugadores.First(j => j.rol_id == 2).id_jugador;
                                posiciones[5] = jugadores.First(j => j.rol_id == 3).id_jugador;
                                posiciones[6] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador;
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador;
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador;
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador;
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Skip(1).Take(1).First().id_jugador;
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Skip(2).Take(1).First().id_jugador;

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                    else if (tactica == "3-5-2")
                    {
                        // Query para obtener jugadores ordenados por su media de atributos
                        query = @"SELECT id_jugador, rol_id,
                                         (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                  FROM jugadores 
                                  WHERE id_equipo = @equipo
                                  ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1  -- Portero
                                            WHEN rol_id = 4 THEN 2  -- Centrales
                                            WHEN rol_id = 2 THEN 3  -- Lateral Derecho
                                            WHEN rol_id = 3 THEN 4  -- Lateral Izquierdo
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5  -- Mediocampistas
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6  -- Delanteros
                                            ELSE 7 
                                        END,
                                        media DESC";

                        using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@equipo", equipo);
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                List<(int id_jugador, int rol_id, double media)> jugadores = new();

                                while (reader.Read())
                                {
                                    jugadores.Add((
                                        reader.GetInt32(0), // id_jugador
                                        reader.GetInt32(1), // rol_id
                                        reader.GetDouble(2) // media
                                    ));
                                }

                                if (jugadores.Count == 0) return;

                                // Definir posiciones titulares
                                Dictionary<int, int> posiciones = new();
                                posiciones[1] = jugadores.First(j => j.rol_id == 1).id_jugador;
                                posiciones[2] = jugadores.Where(j => j.rol_id == 4).Take(1).First().id_jugador;
                                posiciones[3] = jugadores.Where(j => j.rol_id == 4).Skip(1).Take(1).First().id_jugador;
                                posiciones[4] = jugadores.Where(j => j.rol_id == 4).Skip(2).Take(1).First().id_jugador;
                                posiciones[5] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Take(1).First().id_jugador;
                                posiciones[6] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(1).Take(1).First().id_jugador;
                                posiciones[7] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(2).Take(1).First().id_jugador;
                                posiciones[8] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(3).Take(1).First().id_jugador;
                                posiciones[9] = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).Skip(4).Take(1).First().id_jugador;
                                posiciones[10] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Take(1).First().id_jugador;
                                posiciones[11] = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).Skip(1).Take(1).First().id_jugador;

                                // Insertar titulares
                                foreach (var kvp in posiciones)
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                        insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }

                                // Insertar suplentes (posición 12 en adelante)
                                int pos = 12;
                                foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                                {
                                    string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                    using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                    {
                                        insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                        insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
        
        // ======================================================= Método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> MostrarAlineacion(int inicio, int final)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.*, a.posicion
                                     FROM jugadores j
                                     JOIN alineacion a ON j.id_jugador = a.id_jugador 
                                     WHERE a.posicion >= @inicio AND a.posicion <= @final 
                                     ORDER BY a.posicion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@inicio", inicio);
                        cmd.Parameters.AddWithValue("@final", final);

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
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    PosicionAlineacion = dr.GetInt32(dr.GetOrdinal("posicion")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método que cambia las posiciones entre 2 jugadores
        public void IntercambioPosicion(int jugador1, int jugador2, int posicion1, int posicion2)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SQLiteCommand cmd = conn.CreateCommand())
                            {
                                // Actualizar la posición del primer jugador
                                cmd.CommandText = @"UPDATE alineacion 
                                                    SET posicion = @PosicionDos
                                                    WHERE id_jugador = @IdJugadorUno";
                                cmd.Parameters.AddWithValue("@IdJugadorUno", jugador1);
                                cmd.Parameters.AddWithValue("@PosicionDos", posicion2);
                                cmd.ExecuteNonQuery();

                                // Limpiar parámetros para la segunda consulta
                                cmd.Parameters.Clear();

                                // Actualizar la posición del segundo jugador
                                cmd.CommandText = @"UPDATE alineacion 
                                                    SET posicion = @PosicionUno
                                                    WHERE id_jugador = @IdJugadorDos";
                                cmd.Parameters.AddWithValue("@IdJugadorDos", jugador2);
                                cmd.Parameters.AddWithValue("@PosicionUno", posicion1);
                                cmd.ExecuteNonQuery();
                            }

                            // Confirmar los cambios en la base de datos
                            transaction.Commit();
                        }
                        catch (SQLiteException ex)
                        {
                            // Revertir cambios si hay error
                            transaction.Rollback();
                            MessageBox.Show($"Error en la transacción: {ex.Message}");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para mostrar el entrenamiento de un jugador
        public int EntrenamientoJugador(int jugador)
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT entrenamiento FROM jugadores WHERE id_jugador = @IdJugador";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);

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

        // ===================================================================== Método que selecciona un entrenamiento para un jugador
        public void EntrenarJugador(int jugador, int tipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET entrenamiento = @Entrenamiento
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Entrenamiento", tipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ======================================================= Método para mostrar la lista de Jugadores de un equipo SIN PORTEROS
        public List<Jugador> PlantillaSinPorteros(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT *
                                    FROM jugadores
                                    WHERE id_equipo = @idEquipo AND rol_id <> 1 AND lesion = 0";

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
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
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
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ======================================================= Método para crear los 16 jugadores que jugaran el partido
        public List<Jugador> JugadoresJueganPartido(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener un portero y los 15 mejores jugadores de campo sin estar lesionados ni sancionados
                    string query = @"SELECT * FROM (
                                        SELECT *, 
                                            (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                        FROM jugadores 
                                        WHERE id_equipo = @equipo AND lesion = 0 AND sancionado = 0 AND rol_id = 1
                                        ORDER BY media DESC 
                                        LIMIT 1
                                     )
                                     UNION ALL
                                     SELECT * FROM (
                                        SELECT *, 
                                            (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                        FROM jugadores 
                                        WHERE id_equipo = @equipo AND lesion = 0 AND sancionado = 0 AND rol_id BETWEEN 2 AND 10
                                        ORDER BY media DESC 
                                        LIMIT 15
                                     )";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@equipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
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
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            string jugadoresLocalInfo = "Jugadores del equipo local:\n";
            foreach (var jugador in lista)
            {
                jugadoresLocalInfo += $"ID: {jugador.IdJugador}, Nombre: {jugador.Nombre} {jugador.Apellido}, Rol: {jugador.Rol}\n";
                
            }
          
            return lista;
        }

        // ===================================================================== Método que pone a un jugador como lesionado
        public void PonerJugadorLesionado(int jugador, int duracion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET lesion = @Duracion
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Duracion", duracion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que selecciona un entrenamiento para un jugador
        public void PonerJugadorSancionado(int jugador, int duracion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET sancionado = @Duracion
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Duracion", duracion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para decir si un jugador pertenece a mi equipo
        public bool EsDeMiEquipo(int jugador, int equipo)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM jugadores WHERE id_jugador = @IdJugador AND id_equipo = @IdEquipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);
                    cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // ===================================================================== Método que incrementa/decrementa la moral y el estado de forma
        public void ActualizarMoralEstadoForma(int jugador, int moral, int estadoForma)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET moral = MAX(0, MIN(99, moral + @Moral)),
                                         estado_forma = MAX(0, MIN(99, estado_forma + @EstadoForma))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Moral", moral);
                        cmd.Parameters.AddWithValue("@EstadoForma", estadoForma);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que actualiza los atributos por entrenamiento
        public void ActualizarAtributosEntrenamiento(int jugador, int portero, int pase, int regate, int remate, int entradas, int tiro)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET portero = MAX(0, MIN(99, portero + @Portero)),
                                         pase = MAX(0, MIN(99, pase + @Pase)),
                                         regate = MAX(0, MIN(99, regate + @Regate)),
                                         remate = MAX(0, MIN(99, remate + @Remate)),
                                         entradas = MAX(0, MIN(99, entradas + @Entradas)),
                                         tiro = MAX(0, MIN(99, tiro + @Tiro))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Portero", portero);
                        cmd.Parameters.AddWithValue("@Pase", pase);
                        cmd.Parameters.AddWithValue("@Regate", regate);
                        cmd.Parameters.AddWithValue("@Remate", remate);
                        cmd.Parameters.AddWithValue("@Entradas", entradas);
                        cmd.Parameters.AddWithValue("@Tiro", tiro);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        public void ActualizarOtrosAtributosEntrenamiento(int jugador, int velocidad, int resistencia, int agresividad, int calidad)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET velocidad = MAX(0, MIN(99, velocidad + @Velocidad)),
                                         resistencia = MAX(0, MIN(99, resistencia + @Resistencia)),
                                         agresividad = MAX(0, MIN(99, agresividad + @Agresividad)),
                                         calidad = MAX(0, MIN(99, calidad + @Calidad))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Velocidad", velocidad);
                        cmd.Parameters.AddWithValue("@Resistencia", resistencia);
                        cmd.Parameters.AddWithValue("@Agresividad", agresividad);
                        cmd.Parameters.AddWithValue("@Calidad", calidad);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que resetea la moral y el estado de forma
        public void ResetearMoralEstadoForma()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET moral = 50, estado_forma = 50";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que actualiza un jugador
        public void ActualizarJugador(Jugador ojugadorEditar)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Construir consulta base
                    string query = @"UPDATE jugadores 
                                     SET nombre = @nombre,
                                        apellido = @apellido,
                                        id_equipo = @id_equipo,
                                        dorsal = @dorsal,
                                        status = @status,
                                        rol = @rol,
                                        rol_id = @rol_id,
                                        velocidad = @velocidad,
                                        resistencia = @resistencia,
                                        agresividad = @agresividad,
                                        calidad = @calidad,
                                        estado_forma = @estado_forma,
                                        moral = @moral,
                                        potencial = @potencial,
                                        portero = @portero,
                                        pase = @pase,
                                        regate = @regate,
                                        remate = @remate,
                                        entradas = @entradas,
                                        tiro = @tiro,
                                        fecha_nacimiento = @fecha_nacimiento,
                                        peso = @peso,
                                        altura = @altura,
                                        nacionalidad = @nacionalidad";

                    // Si hay ruta de imagen, añadirla a la consulta
                    if (ojugadorEditar.RutaImagen != "Recursos/img/jugadores/")
                    {
                        query += ", ruta_imagen = @ruta_imagen";
                    }

                    // Cerrar la consulta con WHERE
                    query += " WHERE id_jugador = @id_jugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros obligatorios
                        cmd.Parameters.AddWithValue("@nombre", ojugadorEditar.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", ojugadorEditar.Apellido);
                        cmd.Parameters.AddWithValue("@id_equipo", ojugadorEditar.IdEquipo);
                        cmd.Parameters.AddWithValue("@dorsal", ojugadorEditar.Dorsal);
                        cmd.Parameters.AddWithValue("@status", ojugadorEditar.Status);
                        cmd.Parameters.AddWithValue("@rol", ojugadorEditar.Rol);
                        cmd.Parameters.AddWithValue("@rol_id", ojugadorEditar.RolId);
                        cmd.Parameters.AddWithValue("@velocidad", ojugadorEditar.Velocidad);
                        cmd.Parameters.AddWithValue("@resistencia", ojugadorEditar.Resistencia);
                        cmd.Parameters.AddWithValue("@agresividad", ojugadorEditar.Agresividad);
                        cmd.Parameters.AddWithValue("@calidad", ojugadorEditar.Calidad);
                        cmd.Parameters.AddWithValue("@estado_forma", ojugadorEditar.EstadoForma);
                        cmd.Parameters.AddWithValue("@moral", ojugadorEditar.Moral);
                        cmd.Parameters.AddWithValue("@potencial", ojugadorEditar.Potencial);
                        cmd.Parameters.AddWithValue("@portero", ojugadorEditar.Portero);
                        cmd.Parameters.AddWithValue("@pase", ojugadorEditar.Pase);
                        cmd.Parameters.AddWithValue("@regate", ojugadorEditar.Regate);
                        cmd.Parameters.AddWithValue("@remate", ojugadorEditar.Remate);
                        cmd.Parameters.AddWithValue("@entradas", ojugadorEditar.Entradas);
                        cmd.Parameters.AddWithValue("@tiro", ojugadorEditar.Tiro);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", ojugadorEditar.FechaNacimiento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@peso", ojugadorEditar.Peso);
                        cmd.Parameters.AddWithValue("@altura", ojugadorEditar.Altura);
                        cmd.Parameters.AddWithValue("@nacionalidad", ojugadorEditar.Nacionalidad ?? "Desconocida");
                        cmd.Parameters.AddWithValue("@id_jugador", ojugadorEditar.IdJugador);

                        // Parámetro opcional
                        if (!string.IsNullOrEmpty(ojugadorEditar.RutaImagen))
                        {
                            cmd.Parameters.AddWithValue("@ruta_imagen", ojugadorEditar.RutaImagen);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

    }
}
