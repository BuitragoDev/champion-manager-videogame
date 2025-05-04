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
    public class TelevisionDatos : Conexion
    {
        private Metodos metodos;

        public TelevisionDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para Mostrar la lista de Televisiones
        public List<Television> MostrarListaCadenasTV()
        {
            List<Television> oTelevision = new List<Television>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT id_cadenatv, nombre, reputacion
                                     FROM cadenastv";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear y agregar cada objeto Finanza a la lista
                                oTelevision.Add(new Television()
                                {
                                    IdTelevision = dr.GetInt32(0),
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

            return oTelevision;
        }

        // ===================================================================== Método para crear un contrato con una Television
        public void AnadirUnaCadenaTV(int cadenatv, int cantidad, int duracion, int equipo, int manager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO contratos_cadenastv (id_cadenatv, id_equipo, id_manager, cantidad, duracion_contrato) 
                                     VALUES (@IdCadenaTV, @IdEquipo, @IdManager, @Cantidad, @Duracion)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdCadenaTV", cadenatv);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);
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

        // ===================================================================== Método para Mostrar las televisiones contratadas
        public Television? TelevisionesContratadas(int manager, int equipo)
        {
            Television? oTelevision = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT 
                                        ca.id_cadenatv, 
                                        ca.nombre, 
                                        ca.reputacion, 
                                        c.cantidad, 
                                        c.duracion_contrato
                                     FROM 
                                        cadenastv AS ca
                                     INNER JOIN 
                                        contratos_cadenastv AS c 
                                     ON 
                                        ca.id_cadenatv = c.id_cadenatv
                                     WHERE 
                                        c.id_manager = @IdManager 
                                        AND c.id_equipo = @IdEquipo
                                     LIMIT 1;
                                     ";

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
                                oTelevision = new Television
                                {
                                    IdTelevision = dr.GetInt32(0),
                                    Nombre = dr.GetString(1),
                                    Reputacion = dr.GetInt32(2),
                                    Cantidad = dr.GetInt32(3),
                                    DuracionContrato = dr.GetInt32(4)
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

            return oTelevision;
        }

        // ----------------------------------------------------------------- Método que devuelve el nombre de una Cadena de TV
        public string NombreCadenaTV(int cadena)
        {
            string nombre = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = "SELECT nombre FROM cadenastv WHERE id_cadenatv = @IdCadena";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCadena", cadena);

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

        // -------------------------------------------------------------- Método para restar 1 año a la Television contratada
        public void RestarAnioCadenaTV(int cadenatv)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE contratos_cadenastv SET duracion_contrato = duracion_contrato - 1 WHERE id_cadenatv = @CadenaTV";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@CadenaTV", cadenatv);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------- Método para elimina una Cadena de TV contratada
        public void CancelarCadenaTV(int cadenatv)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM contratos_cadenastv WHERE id_cadenatv = @CadenaTV";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@CadenaTV", cadenatv);
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
