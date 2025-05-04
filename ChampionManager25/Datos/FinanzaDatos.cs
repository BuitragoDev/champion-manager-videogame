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
    public class FinanzaDatos : Conexion
    {
        private Metodos metodos;

        public FinanzaDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para Mostrar las finanzas de un equipo
        public List<Finanza> MostrarFinanzasEquipo(int manager, int temporada)
        {
            List<Finanza> oFinanzas = new List<Finanza>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"SELECT id_finanza, id_equipo, id_manager, temporada, id_concepto, tipo, cantidad, fecha
                                     FROM finanzas
                                     WHERE id_manager = @IdManager
                                     AND temporada = @Temporada";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdManager", manager);
                        cmd.Parameters.AddWithValue("@Temporada", temporada);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear y agregar cada objeto Finanza a la lista
                                oFinanzas.Add(new Finanza()
                                {
                                    IdFinanza = dr.GetInt32(0),
                                    IdEquipo = dr.GetInt32(1),
                                    IdManager = dr.GetInt32(2),
                                    Temporada = dr.GetString(3),
                                    IdConcepto = dr.GetInt32(4),
                                    Tipo = dr.GetInt32(5),
                                    Cantidad = dr.GetDouble(6),
                                    Fecha = DateTime.Parse(dr.GetString(7))
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

            return oFinanzas;
        }

        // ===================================================================== Método que crea un GASTO
        public void CrearGasto(Finanza finanza)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO finanzas (id_equipo, id_manager, temporada, id_concepto, tipo, cantidad, fecha)
                                     VALUES (@IdEquipo, @IdManager, @Temporada, @IdConcepto, @Tipo, @Cantidad, @Fecha)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", finanza.IdEquipo);
                        cmd.Parameters.AddWithValue("@IdManager", finanza.IdManager);
                        cmd.Parameters.AddWithValue("@Temporada", finanza.Temporada);
                        cmd.Parameters.AddWithValue("@IdConcepto", finanza.IdConcepto);
                        cmd.Parameters.AddWithValue("@Tipo", finanza.Tipo);
                        cmd.Parameters.AddWithValue("@Cantidad", finanza.Cantidad);
                        cmd.Parameters.AddWithValue("@Fecha", finanza.Fecha.ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que crea un INGRESO
        public void CrearIngreso(Finanza finanza)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO finanzas (id_equipo, id_manager, temporada, id_concepto, tipo, cantidad, fecha)
                                     VALUES (@IdEquipo, @IdManager, @Temporada, @IdConcepto, @Tipo, @Cantidad, @Fecha)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", finanza.IdEquipo);
                        cmd.Parameters.AddWithValue("@IdManager", finanza.IdManager);
                        cmd.Parameters.AddWithValue("@Temporada", finanza.Temporada);
                        cmd.Parameters.AddWithValue("@IdConcepto", finanza.IdConcepto);
                        cmd.Parameters.AddWithValue("@Tipo", finanza.Tipo);
                        cmd.Parameters.AddWithValue("@Cantidad", finanza.Cantidad);
                        cmd.Parameters.AddWithValue("@Fecha", finanza.Fecha.ToString("yyyy-MM-dd"));

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
