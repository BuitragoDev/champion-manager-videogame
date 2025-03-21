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
        // Método que devuelve la fecha de hoy
        public Fecha? ObtenerFechaHoy()
        {
            Fecha? fecha = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
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
    }
}
