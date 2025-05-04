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
    public class RemodelacionDatos : Conexion
    {
        private Metodos metodos;

        public RemodelacionDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // Método que comprueba si hay una remodelación en marcha
        public Remodelacion? ComprobarRemodelacion(int equipo, int idManager)
        {
            Remodelacion? obra = null;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta 
                    string query = @"SELECT id_remodelacion, fecha_inicio, fecha_final, tipo_remodelacion, id_equipo, id_manager 
                                     FROM remodelaciones 
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
                                obra = new Remodelacion
                                {
                                    IdRemodelacion = reader.GetInt32(reader.GetOrdinal("id_remodelacion")),
                                    FechaInicio = reader.GetDateTime(reader.GetOrdinal("fecha_inicio")),
                                    FechaFinal = reader.GetDateTime(reader.GetOrdinal("fecha_final")),
                                    TipoRemodelacion = reader.GetInt32(reader.GetOrdinal("tipo_remodelacion")),
                                    IdEquipo = reader.GetInt32(reader.GetOrdinal("id_equipo")),
                                    IdManager = reader.GetInt32(reader.GetOrdinal("id_manager")),
                                };

                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return obra;
        }

        // Método que crea una nueva remodelación.
        public void CrearNuevaRemodelacion(int equipo, int idManager, DateTime fecha, int tipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar los datos en la tabla "taquilla"
                    string query = @"INSERT INTO remodelaciones (fecha_inicio, fecha_final, tipo_remodelacion, id_equipo, id_manager) 
                                   VALUES (@FechaInicio, @FechaFinal, @TipoRemodelacion, @IdEquipo, @IdManager);";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetro
                        cmd.Parameters.AddWithValue("@FechaInicio", Metodos.hoy.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@FechaFinal", fecha.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@TipoRemodelacion", tipo);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@IdManager", idManager);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Método que elimina una remodelación.
        public void EliminarRemodelacion(int id)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Insertar los datos en la tabla "taquilla"
                    string query = @"DELETE FROM remodelaciones WHERE id_remodelacion = @IdRemodelacion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Pasar el idEquipo y idManager como parámetro
                        cmd.Parameters.AddWithValue("@IdRemodelacion", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }
    }
}
