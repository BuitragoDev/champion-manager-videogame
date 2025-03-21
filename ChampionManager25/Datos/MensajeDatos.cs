using ChampionManager25.Entidades;
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
    public class MensajeDatos : Conexion
    {
        // Método que devuelve todos los mensajes de mi manager.
        public List<Mensaje> MostrarMisMensajes(int idManager)
        {
            List<Mensaje> oMensaje = new List<Mensaje>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    string query = @"SELECT *
                                     FROM mensajes
                                     WHERE id_manager = @IdManager
                                     ORDER BY leido ASC, fecha;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oMensaje.Add(new Mensaje()
                                {
                                    IdMensaje = reader.GetInt32(0), // id_mensaje
                                    Fecha = reader.GetDateTime(1),  // fecha
                                    Remitente = reader.GetString(2), // remitente
                                    Asunto = reader.GetString(3),    // asunto
                                    Contenido = reader.GetString(4), // contenido
                                    TipoMensaje = reader.GetString(5), // tipo_mensaje
                                    IdEquipo = reader.IsDBNull(6) ? null : reader.GetInt32(6), // id_equipo (nullable)
                                    IdManager = reader.IsDBNull(7) ? null : reader.GetInt32(7), // id_manager (nullable)
                                    Leido = reader.GetBoolean(8), // leido
                                    Icono = reader.GetInt32(9)
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

            return oMensaje;
        }

        // Método que marca un mensaje como leído
        public void MarcarComoLeido(int idMensaje)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    string query = @"UPDATE mensajes SET leido = 1
                                     WHERE id_mensaje = @IdMensaje;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdMensaje", idMensaje);
                        cmd.ExecuteReader();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Método que marca un mensaje como leído
        public void eliminarMensaje(int idMensaje)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    string query = @"DELETE FROM mensajes
                                     WHERE id_mensaje = @IdMensaje;";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdMensaje", idMensaje);
                        cmd.ExecuteReader();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Método que crea un mensaje.
        public void crearMensaje(Mensaje msg)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    // Consulta SQL para insertar un nuevo mensaje
                    string query = @"INSERT INTO mensajes (fecha, remitente, asunto, contenido, tipo_mensaje, id_equipo, id_manager, leido, icono)
                                     VALUES (@fecha, @remitente, @asunto, @contenido, @tipo_mensaje, @id_equipo, @id_manager, @leido, @icono)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar los parámetros con las propiedades del objeto Mensaje
                        cmd.Parameters.AddWithValue("@fecha", msg.Fecha);
                        cmd.Parameters.AddWithValue("@remitente", msg.Remitente);
                        cmd.Parameters.AddWithValue("@asunto", msg.Asunto);
                        cmd.Parameters.AddWithValue("@contenido", msg.Contenido);
                        cmd.Parameters.AddWithValue("@tipo_mensaje", msg.TipoMensaje);
                        cmd.Parameters.AddWithValue("@id_equipo", msg.IdEquipo.HasValue ? (object)msg.IdEquipo.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@id_manager", msg.IdManager.HasValue ? (object)msg.IdManager.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@leido", msg.Leido ? 1 : 0); // Convertir bool a 0 o 1
                        cmd.Parameters.AddWithValue("@icono", msg.Icono);

                        // Ejecutar la consulta
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Mostrar un mensaje en caso de error
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Método que devuelve el número de mensajes no leídos.
        public int MensajesNoLeidos(int idManager)
        {
            int numero = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(cadena))
                {
                    conn.Open();

                    string query = @"SELECT *
                                     FROM mensajes
                                     WHERE id_manager = @IdManager AND leido = false";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdManager", idManager);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                numero++;
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return numero;
        }
    }
}
