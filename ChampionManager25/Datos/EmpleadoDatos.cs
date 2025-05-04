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
    public class EmpleadoDatos : Conexion
    {
        private Metodos metodos;

        public EmpleadoDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para Mostrar la lista de empleados disponibles
        public List<Empleado> MostrarListaEmpleadosDisponibles(int puesto)
        {
            List<Empleado> oEmpleado = new List<Empleado>();

            string[] posicion = {
                "Segundo Entrenador",
                "Delegado",
                "Director Técnico",
                "Preparador Físico",
                "Psicólogo",
                "Financiero",
                "Equipo Médico",
                "Encargado Campo"
            };
            string nombrePuesto = "";

            if (puesto >= 1 && puesto <= 8)
            {
                nombrePuesto = posicion[puesto - 1];
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT id_empleadoDisponible, nombre, puesto, categoria, salario, id_manager, id_equipo
                                     FROM empleados_disponibles 
                                     WHERE puesto = @Puesto
                                     ORDER BY RANDOM()
                                     LIMIT 5";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Puesto", nombrePuesto);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEmpleado.Add(new Empleado()
                                {
                                    IdEmpleado = dr.GetInt32(0), // Obtenemos el Id como entero
                                    Nombre = dr.GetString(1), // Nombre es un string
                                    Puesto = dr.GetString(2), // Puesto es un string
                                    Categoria = dr.GetInt32(3), // Categoria es un entero
                                    Salario = dr.GetInt32(4), // Salario es un entero
                                    IdManager = dr.IsDBNull(5) ? (int?)null : dr.GetInt32(5), // Si es NULL, asignamos null
                                    IdEquipo = dr.IsDBNull(6) ? (int?)null : dr.GetInt32(6)
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

            return oEmpleado;
        }

        // ===================================================================== Método para Mostrar la lista de empleados contratados
        public List<Empleado> MostrarListaEmpleadosContratados(int equipo, int manager)
        {
            List<Empleado> oEmpleado = new List<Empleado>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT id_empleado, nombre, puesto, categoria, salario, id_manager, id_equipo
                                     FROM empleados WHERE id_equipo = @Equipo AND id_manager = @Manager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Equipo", equipo);
                        cmd.Parameters.AddWithValue("@Manager", manager);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEmpleado.Add(new Empleado()
                                {
                                    IdEmpleado = dr.GetInt32(0), // Obtenemos el Id como entero
                                    Nombre = dr.GetString(1), // Nombre es un string
                                    Puesto = dr.GetString(2), // Puesto es un string
                                    Categoria = dr.GetInt32(3), // Categoria es un entero
                                    Salario = dr.GetInt32(4), // Salario es un entero
                                    IdManager = dr.IsDBNull(5) ? (int?)null : dr.GetInt32(5), // Si es NULL, asignamos null
                                    IdEquipo = dr.IsDBNull(6) ? (int?)null : dr.GetInt32(6)
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

            return oEmpleado;
        }

        // Método que cambia el campo Contratado de 0 a 1.
        public void FicharEmpleado(int equipo, int idManager, int idEmpleado)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener los datos del empleado desde empleados_disponibles
                    string querySelect = @"SELECT nombre, puesto, categoria, salario, id_manager, id_equipo
                                           FROM empleados_disponibles
                                           WHERE id_empleadoDisponible = @IdEmpleado";

                    using (SQLiteCommand cmdSelect = new SQLiteCommand(querySelect, conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                        using (SQLiteDataReader dr = cmdSelect.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                // Obtener los datos del empleado
                                string nombre = dr.GetString(0);
                                string puesto = dr.GetString(1);
                                int categoria = dr.GetInt32(2);
                                int salario = dr.GetInt32(3);
                                int? idManagerDisponible = dr.IsDBNull(4) ? (int?)null : dr.GetInt32(4);
                                int? idEquipoDisponible = dr.IsDBNull(5) ? (int?)null : dr.GetInt32(5);

                                // Insertar los datos del empleado en la tabla empleados
                                string queryInsert = @"INSERT INTO empleados (nombre, puesto, categoria, salario, id_manager, id_equipo)
                                                        VALUES (@Nombre, @Puesto, @Categoria, @Salario, @IdManager, @IdEquipo)";

                                using (SQLiteCommand cmdInsert = new SQLiteCommand(queryInsert, conn))
                                {
                                    cmdInsert.Parameters.AddWithValue("@Nombre", nombre);
                                    cmdInsert.Parameters.AddWithValue("@Puesto", puesto);
                                    cmdInsert.Parameters.AddWithValue("@Categoria", categoria);
                                    cmdInsert.Parameters.AddWithValue("@Salario", salario);
                                    cmdInsert.Parameters.AddWithValue("@IdManager", idManager); // Usamos el idManager del parámetro
                                    cmdInsert.Parameters.AddWithValue("@IdEquipo", equipo); // Usamos el idEquipo del parámetro

                                    // Ejecutar el insert
                                    cmdInsert.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Empleado no encontrado en empleados_disponibles.");
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
            }
        }

        // ===================================================================== Método que despide a un empleado
        public void DespedirEmpleado(int puesto)
        {
            string[] posicion = {
                "Segundo Entrenador",
                "Delegado",
                "Director Técnico",
                "Preparador Físico",
                "Psicólogo",
                "Financiero",
                "Equipo Médico",
                "Encargado Campo"
            };
            string nombrePuesto = "";

            if (puesto >= 1 && puesto <= 8)
            {
                nombrePuesto = posicion[puesto - 1];
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM empleados WHERE puesto = @Puesto";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Puesto", nombrePuesto);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para Calcular Salario Total Empleados
        public int SalarioTotalEmpleados(int equipo, int manager)
        {
            int total = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT salario
                                     FROM empleados 
                                     WHERE id_equipo = @Equipo AND id_manager = @Manager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Equipo", equipo);
                        cmd.Parameters.AddWithValue("@Manager", manager);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                total += dr.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return total;
        }

        // ===================================================================== Método para Mostrar la Categoria de un empleado
        public Empleado? MostrarCategoriaEmpleado(string nombre)
        {
            Empleado? oEmpleado = null;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT categoria, puesto
                                     FROM empleados WHERE nombre = @Nombre";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", nombre);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEmpleado = new Empleado()
                                {
                                    Categoria = dr.GetInt32(0),
                                    Puesto = dr.GetString(1),
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

            return oEmpleado;
        }

        // =================================================================== Método que devuelve TRUE si hay un empleado contratado
        public bool EmpleadoEncontrado(string tarea, int manager)
        {
            bool encontrado = false;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para verificar si existe al menos un empleado con ese puesto
                    string query = @"SELECT COUNT(*) FROM empleados WHERE puesto = @Tarea AND id_manager = @IdManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tarea", tarea);
                        cmd.Parameters.AddWithValue("@IdManager", manager);

                        // Ejecutamos la consulta y obtenemos el número de coincidencias
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Si el count es mayor que 0, significa que encontramos un empleado
                        if (count > 0)
                        {
                            encontrado = true;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return encontrado;
        }


        // =================================================================== Método que devuelve un objeto empleado por su Puesto
        public Empleado? ObtenerEmpleadoPorPuesto(string tarea)
        {
            Empleado? empleado = null; // Si no encuentra nada, devuelve null

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = "SELECT * FROM empleados WHERE puesto = @Tarea LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Tarea", tarea);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read()) // Si hay resultados, creamos el objeto
                            {
                                empleado = new Empleado
                                {
                                    IdEmpleado = dr.GetInt32(0),
                                    Nombre = dr.IsDBNull(1) ? null : dr.GetString(1),
                                    Puesto = dr.IsDBNull(2) ? null : dr.GetString(2),
                                    Categoria = dr.GetInt32(3),
                                    Salario = dr.GetInt32(4),
                                    IdManager = dr.IsDBNull(5) ? null : dr.GetInt32(5),
                                    IdEquipo = dr.IsDBNull(6) ? null : dr.GetInt32(6)
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return empleado;
        }

    }
}
