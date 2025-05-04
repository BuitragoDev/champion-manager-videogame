using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;

namespace ChampionManager25.Datos
{
    public class OjearDatos : Conexion
    {
        // ===================================================================== Método que pone un jugador en la CARTERA
        public void PonerJugadorCartera(int jugador, Manager manager, int dias)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO ojear_jugadores (id_jugador, id_manager, fecha_agregado, fecha_informe) 
                                     VALUES (@IdJugador, @IdManager, @FechaAgregado, @FechaInforme);";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdManager", manager.IdManager);
                        cmd.Parameters.AddWithValue("@FechaAgregado", Metodos.hoy.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@FechaInforme", Metodos.hoy.AddDays(dias).ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que QUITA un jugador de la CARTERA
        public void QuitarJugadorCartera(int jugador, Manager manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM ojear_jugadores WHERE id_jugador = @IdJugador AND id_manager = @IdManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdManager", manager.IdManager);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que dice si un jugador ya está siendo ojeado
        public bool ComprobarSiJugadorEnCartera(int jugador, Manager manager)
        {
            bool encontrado = false;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para verificar si existe al menos un empleado con ese puesto
                    string query = @"SELECT COUNT(*) 
                                     FROM ojear_jugadores
                                     WHERE id_jugador = @IdJugador AND id_manager = @IdManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdManager", manager.IdManager);

                        // Ejecutamos la consulta y obtenemos el número de coincidencias
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Si count > 0, significa que al menos un empleado tiene ese puesto
                        encontrado = (count > 0);
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return encontrado;
        }

        // ===================================================================== Método que dice si un jugador ya ha sido ojeado
        public bool ComprobarJugadorOjeado(int jugador, Manager manager)
        {
            bool ojeado = false;
            DateTime hoy = Metodos.hoy;
            DateTime fechaInforme = DateTime.MinValue; // Valor por defecto en caso de que no haya informe

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT fecha_informe 
                                     FROM ojear_jugadores
                                     WHERE id_jugador = @IdJugador AND id_manager = @IdManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdManager", manager.IdManager);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null && resultado != DBNull.Value)
                        {
                            fechaInforme = Convert.ToDateTime(resultado);
                            if (fechaInforme < hoy)
                            {
                                ojeado = true;
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return ojeado;
        }

        // ======================================================= Método para mostrar la lista detallada de los jugadores ojeados
        public List<Jugador> ListadoJugadoresOjeados(int idManager)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.*, o.fecha_informe, c.salario_anual, c.clausula_rescision, c.duracion
                                     FROM jugadores j
                                     INNER JOIN contratos c ON j.id_jugador = c.id_jugador
                                     INNER JOIN ojear_jugadores o ON j.id_jugador = o.id_jugador
                                     WHERE o.id_manager = @idManager
                                     ORDER BY o.fecha_informe ASC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idManager", idManager);

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
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    AniosContrato = dr.IsDBNull(dr.GetOrdinal("duracion")) ? null : dr.GetInt32(dr.GetOrdinal("duracion")),
                                    SalarioTemporada = dr.IsDBNull(dr.GetOrdinal("salario_anual")) ? null : dr.GetInt32(dr.GetOrdinal("salario_anual")),
                                    ClausulaRescision = dr.IsDBNull(dr.GetOrdinal("clausula_rescision")) ? null : dr.GetInt32(dr.GetOrdinal("clausula_rescision")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    FechaInforme = dr.GetString(dr.GetOrdinal("fecha_informe")),
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
    }
}
