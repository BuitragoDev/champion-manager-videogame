using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace ChampionManager25.Datos
{
    public class TaquillaDatos : Conexion
    {
        // ----------------------------------------------------------------- Método que crea un registro para la taquilla del equipo
        public void GenerarTaquilla(int equipo, int idManager)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar los datos en la tabla "taquilla"
                    string query = @"INSERT INTO taquilla (id_equipo, id_manager) VALUES (@IdEquipo, @IdManager);";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetro
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ----------------------------------------------------------------- Método que comprueba si se ha establecido precio de abonos
        public bool ComprobarAbono(int equipo, int idManager)
        {
            bool verificado = false;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para comprobar si existen precios de abonos establecidos
                    string query = @"SELECT COUNT(*) FROM taquilla 
                                     WHERE id_equipo = @IdEquipo 
                                        AND id_manager = @IdManager
                                        AND (precio_abono_general <> 0
                                             OR precio_abono_tribuna <> 0
                                             OR precio_abono_vip <> 0);";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetros
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        // Ejecutar la consulta y obtener el resultado
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Si el resultado es mayor que 0, se encontró un registro
                        if (count > 0)
                        {
                            verificado = true;
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return verificado;
        }

        // ----------------------------------------------------------------- Método que crea el precio de las entradas
        public void EstablecerPrecioEntradas(int equipo, int idManager, int precioGeneral, int precioTribuna, int precioVip)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar los datos en la tabla "taquilla"
                    string query = @"UPDATE taquilla SET 
                                                     precio_entrada_general = @PrecioGeneral, 
                                                     precio_entrada_tribuna = @PrecioTribuna,
                                                     precio_entrada_vip = @PrecioVip
                                     WHERE id_manager = @IdManager AND id_equipo = @IdEquipo;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetro
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@PrecioGeneral", precioGeneral);
                        cmd.Parameters.AddWithValue("@PrecioTribuna", precioTribuna);
                        cmd.Parameters.AddWithValue("@PrecioVip", precioVip);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ----------------------------------------------------------------- Método que crea el precio de los abonos
        public void EstablecerPrecioAbonos(int equipo, int idManager, int precioGeneral, int precioTribuna, int precioVip)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar los datos en la tabla "taquilla"
                    string query = @"UPDATE taquilla SET 
                                                     precio_abono_general = @PrecioGeneral, 
                                                     precio_abono_tribuna = @PrecioTribuna,
                                                     precio_abono_vip = @PrecioVip
                                     WHERE id_manager = @IdManager AND id_equipo = @IdEquipo;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetro
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.Parameters.AddWithValue("@PrecioGeneral", precioGeneral);
                        cmd.Parameters.AddWithValue("@PrecioTribuna", precioTribuna);
                        cmd.Parameters.AddWithValue("@PrecioVip", precioVip);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ----------------------------------------------------------------- Método que devuelve los precios de la taquilla.
        public Taquilla? RecuperarPreciosTaquilla(int equipo, int idManager)
        {
            Taquilla? taquilla = null;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para comprobar si existen precios de abonos establecidos
                    string query = @"SELECT id_precio, id_equipo, precio_entrada_general, 
                                            precio_entrada_tribuna, precio_entrada_vip,
                                            precio_abono_general, precio_abono_tribuna, 
                                            precio_abono_vip, abonos_vendidos 
                                     FROM taquilla 
                                     WHERE id_equipo = @IdEquipo 
                                     AND id_manager = @IdManager";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetros
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                taquilla = new Taquilla
                                {
                                    IdPrecio = reader.GetInt32(reader.GetOrdinal("id_precio")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo")),
                                    PrecioEntradaGeneral = reader.IsDBNull(reader.GetOrdinal("precio_entrada_general")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_entrada_general")),
                                    PrecioEntradaTribuna = reader.IsDBNull(reader.GetOrdinal("precio_entrada_tribuna")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_entrada_tribuna")),
                                    PrecioEntradaVip = reader.IsDBNull(reader.GetOrdinal("precio_entrada_vip")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_entrada_vip")),
                                    PrecioAbonoGeneral = reader.IsDBNull(reader.GetOrdinal("precio_abono_general")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_abono_general")),
                                    PrecioAbonoTribuna = reader.IsDBNull(reader.GetOrdinal("precio_abono_tribuna")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_abono_tribuna")),
                                    PrecioAbonoVip = reader.IsDBNull(reader.GetOrdinal("precio_abono_vip")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("precio_abono_vip")),
                                    AbonosVendidos = reader.IsDBNull(reader.GetOrdinal("abonos_vendidos")) ? 0 : reader.GetInt32(reader.GetOrdinal("abonos_vendidos"))
                                };

                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return taquilla;
        }
    }
}
