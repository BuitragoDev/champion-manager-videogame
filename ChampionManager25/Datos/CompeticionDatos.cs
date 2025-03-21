using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class CompeticionDatos : Conexion
    {
        // ===================================================================== Método para Mostrar el Nombre de la Competición
        public string MostrarNombreCompeticion(int competicion)
        {
            string nombreComp = "";
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();
                    string query = @"SELECT nombre
                                     FROM competiciones
                                     WHERE id_competicion = @IdCompeticion";

                    // Ejecutar la consulta
                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si encuentra un registro
                            {
                                nombreComp = reader["nombre"]?.ToString() ?? string.Empty;
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
            return nombreComp;
        }
    }
}
