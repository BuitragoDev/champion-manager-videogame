using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Datos
{
    public class FechaDatos : Conexion
    {
        // ----------------------------------------------------------------------- Método que devuelve la fecha de hoy
        public Fecha? ObtenerFechaHoy()
        {
            Fecha? fecha = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT * FROM fechas WHERE id_fecha = 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                fecha = new Fecha
                                {
                                    Hoy = reader["hoy"].ToString() ?? "", // Mantener como string
                                    Anio = Convert.ToInt32(reader["anio"])
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

            return fecha;
        }

        // ----------------------------------------------------------------------- Método que suma 1 dia a la fecha
        public void AvanzarUnDia()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = "UPDATE fechas SET hoy = DATE(hoy, '+1 day') WHERE id_fecha = 1";

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

        // ----------------------------------------------------------------------- Método que suma 1 año al campo anio
        public void AvanzarUnAnio()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = "UPDATE fechas SET anio = anio + 1 WHERE id_fecha = 1";

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

        // ----------------------------------------------------------------------- Método que adelanta la fecha al 15 de julio
        public void AvanzarFecha(int temporada)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string nuevaFecha = $"{temporada}-07-15";
                    string query = "UPDATE fechas SET hoy = @Fecha WHERE id_fecha = 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Fecha", nuevaFecha);
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
