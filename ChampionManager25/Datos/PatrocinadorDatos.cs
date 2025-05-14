using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ChampionManager25.Datos
{
    public class PatrocinadorDatos : Conexion
    {
        private Metodos metodos;

        public PatrocinadorDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // -------------------------------------------------------------- Método para Mostrar la lista de patrocinadores
        public List<Patrocinador> MostrarListaPatrocinadores()
        {
            List<Patrocinador> oPatrocinador = new List<Patrocinador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT id_patrocinador, nombre, reputacion
                                     FROM patrocinadores";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear y agregar cada objeto Finanza a la lista
                                oPatrocinador.Add(new Patrocinador()
                                {
                                    IdPatrocinador = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Reputacion = dr.GetInt32(2)
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

            return oPatrocinador;
        }

        // -------------------------------------------------------------- Método para Mostrar las finanzas de un equipo
        public void AnadirUnPatrocinador(int patrocinador, int cantidad, int mensualidad, int duracion, int equipo, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO contratos_patrocinador (id_patrocinador, id_equipo, id_manager, cantidad, mensualidad, duracion_contrato) 
                                     VALUES (@IdPatrocinador, @IdEquipo, @IdManager, @Cantidad, @Mensualidad, @Duracion)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdPatrocinador", patrocinador);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@Mensualidad", mensualidad);
                        cmd.Parameters.AddWithValue("@Duracion", duracion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ----------------------------------------------------------------- Método para Mostrar los patrocinadores contratados
        public Patrocinador? PatrocinadoresContratados(int manager, int equipo)
        {
            Patrocinador? oPatrocinador = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT p.id_patrocinador, p.nombre, p.reputacion, c.cantidad, c.mensualidad, c.duracion_contrato
                                     FROM patrocinadores p
                                     INNER JOIN contratos_patrocinador c ON p.id_patrocinador = c.id_patrocinador
                                     WHERE c.id_manager = @IdManager AND c.id_equipo = @IdEquipo
                                     LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", manager);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear y agregar cada objeto Finanza a la lista
                                oPatrocinador = new Patrocinador
                                {
                                    IdPatrocinador = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Reputacion = dr.GetInt32(2),
                                    Cantidad = dr.GetInt32(3),
                                    Mensualidad = dr.GetInt32(4),
                                    DuracionContrato = dr.GetInt32(5)
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

            return oPatrocinador;
        }

        // ----------------------------------------------------------------- Método que devuelve el nombre de un Patrocinador
        public string NombrePatrocinador(int patrocinador)
        {
            string nombre = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = "SELECT nombre FROM patrocinadores WHERE id_patrocinador = @IdPatrocinador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdPatrocinador", patrocinador);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nombre = reader["nombre"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return nombre;
        }

        // -------------------------------------------------------------- Método para restar 1 año al Patrocinador contratado
        public void RestarAnioPatrocinador(int patrocinador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE contratos_patrocinador SET duracion_contrato = duracion_contrato - 1 WHERE id_patrocinador = @Patrocinador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Patrocinador", patrocinador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------- Método para elimina un Patrocinador contratado
        public void CancelarPatrocinador(int patrocinador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM contratos_patrocinador WHERE id_patrocinador = @Patrocinador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Patrocinador", patrocinador);
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
