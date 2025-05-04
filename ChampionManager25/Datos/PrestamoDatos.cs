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
    public class PrestamoDatos : Conexion
    {
        private Metodos metodos;

        public PrestamoDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para Mostrar los Préstamos pedidos
        public List<Prestamo> MostrarPrestamos(int manager, int equipo)
        {
            List<Prestamo> oPrestamo = new List<Prestamo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener los prestamos
                    string query = @"SELECT id_prestamo, orden_prestamo, fecha, capital, capital_restante, semanas, semanas_restantes, tasa_interes, pago_semanal, id_manager, id_equipo
                                     FROM prestamos
                                     WHERE id_manager = @IdManager AND id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear y agregar cada objeto Prestamo a la lista
                                oPrestamo.Add(new Prestamo()
                                {
                                    IdPrestamo = dr.GetInt32(0),
                                    Orden = dr.GetInt32(1),
                                    Fecha = dr.GetString(2),
                                    Capital = dr.GetInt32(3),
                                    CapitalRestante = dr.GetInt32(4),
                                    Semanas = dr.GetInt32(5),
                                    SemanasRestantes = dr.GetInt32(6),
                                    TasaInteres = dr.GetInt32(7),
                                    PagoSemanal = dr.GetInt32(8),
                                    IdManager = dr.GetInt32(9),
                                    IdEquipo = dr.GetInt32(10)
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

            return oPrestamo;
        }

        // ===================================================================== Método para Mostrar el orden de los Préstamos
        public List<int> OrdenPrestamos(int manager, int equipo)
        {
            List<int> orden = new List<int>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener los prestamos
                    string query = @"SELECT orden_prestamo
                                     FROM prestamos
                                     WHERE id_manager = @IdManager AND id_equipo = @IdEquipo
                                     ORDER BY orden_prestamo ASC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())  // Lee los resultados de la consulta
                        {
                            while (dr.Read())  // Recorre los resultados de la consulta
                            {
                                // Añadir cada valor de orden_prestamo a la lista
                                orden.Add(dr.GetInt32(0));  // Obtén el valor de la columna 'orden_prestamo'
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return orden;
        }

        // ===================================================================== Método para crear un Préstamo
        public void AnadirPrestamo(Prestamo prestamo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO prestamos (orden_prestamo, fecha, capital, capital_restante, semanas, semanas_restantes, tasa_interes, pago_semanal, id_manager, id_equipo) 
                                     VALUES (@Orden, @Fecha, @Capital, @CapitalRestante, @Semanas, @SemanasRestantes, @TasaInteres, @PagoSemanal, @IdManager, @IdEquipo)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Orden", prestamo.Orden);
                        cmd.Parameters.AddWithValue("@Fecha", prestamo.Fecha);
                        cmd.Parameters.AddWithValue("@Capital", prestamo.Capital);
                        cmd.Parameters.AddWithValue("@CapitalRestante", prestamo.CapitalRestante);
                        cmd.Parameters.AddWithValue("@Semanas", prestamo.Semanas);
                        cmd.Parameters.AddWithValue("@SemanasRestantes", prestamo.SemanasRestantes);
                        cmd.Parameters.AddWithValue("@TasaInteres", prestamo.TasaInteres);
                        cmd.Parameters.AddWithValue("@PagoSemanal", prestamo.PagoSemanal);
                        cmd.Parameters.AddWithValue("@IdManager", prestamo.IdManager);
                        cmd.Parameters.AddWithValue("@IdEquipo", prestamo.IdEquipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para eliminar un Préstamo
        public void EliminarPrestamo(int manager, int equipo, int orden)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM prestamos 
                                     WHERE id_manager = @IdManager AND id_equipo = @IdEquipo AND orden_prestamo = @Orden";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Orden", orden);
                        cmd.Parameters.AddWithValue("@IdManager", manager);
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

        // --------------------------------------------------------------------- Método para restar una semana a un Préstamo
        public void RestarSemana(int orden)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE prestamos SET semanas_restantes = semanas_restantes - 1 WHERE orden_prestamo = @Orden";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@Orden", orden);
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
