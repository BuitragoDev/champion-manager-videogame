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
    public class TransferenciaDatos : Conexion
    {
        // Metodo que evalua una oferta por un jugador.
        public Transferencia EvaluarOfertaEquipo(int idJugador, int idEquipoComprador, int montoOferta, int tipoFichaje)
        {
            Transferencia oferta = new Transferencia();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT j.valor_mercado, j.status, j.situacion_mercado, j.moral, j.estado_animo, 
                                            c.fecha_fin, c.clausula_rescision, c.id_equipo AS equipoActual,
                                            e.presupuesto AS presupuestoEquipoVendedor, e.rival1, e.rival2,
                                            (SELECT presupuesto FROM equipos WHERE id_equipo = @idEquipoComprador) AS presupuestoEquipoComprador
                                     FROM jugadores j 
                                     JOIN contratos c ON j.id_jugador = c.id_jugador 
                                     JOIN equipos e ON e.id_equipo = c.id_equipo
                                     WHERE j.id_jugador = @idJugador";

                    using (var command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@idJugador", idJugador);
                        command.Parameters.AddWithValue("@idEquipoComprador", idEquipoComprador);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                oferta = new Transferencia()
                                {
                                    IdJugador = idJugador,
                                    IdEquipoOrigen = reader.GetInt32(reader.GetOrdinal("equipoActual")),
                                    IdEquipoDestino = idEquipoComprador,
                                    ClausulaRescision = reader.GetInt32(reader.GetOrdinal("clausula_rescision")),
                                    ValorMercado = reader.GetInt32(reader.GetOrdinal("valor_mercado")),
                                    SituacionMercado = reader.GetInt32(reader.GetOrdinal("situacion_mercado")),
                                    Moral = reader.GetInt32(reader.GetOrdinal("moral")),
                                    EstadoAnimo = reader.GetInt32(reader.GetOrdinal("estado_animo")),
                                    FinContrato = reader.GetString(reader.GetOrdinal("fecha_fin")),
                                    PresupuestoComprador = reader.GetInt32(reader.GetOrdinal("presupuestoEquipoComprador")),
                                    PresupuestoVendedor = reader.GetInt32(reader.GetOrdinal("presupuestoEquipoVendedor")),
                                    Rival1 = reader.GetInt32(reader.GetOrdinal("rival1")),
                                    Rival2 = reader.GetInt32(reader.GetOrdinal("rival2")),
                                    MontoOferta = montoOferta,
                                    TipoFichaje = tipoFichaje,
                                    Status = reader.GetInt32(reader.GetOrdinal("status")),
                                    FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
            return oferta;
        }

        // Metodo que registra una oferta por un jugador.
        public void RegistrarOferta(Transferencia oferta, int respuestaEquipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO transferencias (
                                        id_jugador, id_equipo_origen, id_equipo_destino, tipo_fichaje, 
                                        monto_oferta, fecha_oferta, respuesta_equipo
                                     ) 
                                     VALUES (
                                        @idJugador, @idEquipoOrigen, @idEquipoDestino, @tipoFichaje, 
                                        @montoOferta, @fechaOferta, @RespuestaEquipo
                                     );";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", oferta.IdJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", oferta.IdEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", oferta.IdEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", oferta.TipoFichaje);
                        command.Parameters.AddWithValue("@montoOferta", oferta.MontoOferta);
                        command.Parameters.AddWithValue("@fechaOferta", oferta.FechaOferta);
                        command.Parameters.AddWithValue("@RespuestaEquipo", respuestaEquipo);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Metodo que actualiza una oferta por un jugador.
        public void ActualizarOferta(Transferencia oferta, int respuestaEquipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE transferencias 
                                     SET monto_oferta = @montoOferta, 
                                         fecha_oferta = @fechaOferta, 
                                         respuesta_equipo = @RespuestaEquipo 
                                     WHERE id_jugador = @idJugador 
                                       AND id_equipo_origen = @idEquipoOrigen 
                                       AND id_equipo_destino = @idEquipoDestino 
                                       AND tipo_fichaje = @tipoFichaje;";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", oferta.IdJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", oferta.IdEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", oferta.IdEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", oferta.TipoFichaje);
                        command.Parameters.AddWithValue("@montoOferta", oferta.MontoOferta);
                        command.Parameters.AddWithValue("@fechaOferta", oferta.FechaOferta);
                        command.Parameters.AddWithValue("@RespuestaEquipo", respuestaEquipo);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // Metodo que comprueba si el jugador ya tiene una oferta aceptada por el equipo
        public bool ComprobarOfertaActiva(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            bool ofertaAceptada = false;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * 
                                     FROM transferencias 
                                     WHERE id_jugador = @idJugador 
                                        AND id_equipo_origen = @idEquipoVendedor 
                                        AND id_equipo_destino = @idEquipoComprador
                                        AND tipo_fichaje = 1
                                        AND (respuesta_equipo <> 1 OR respuesta_jugador <> 1)";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@idJugador", idJugador);
                        command.Parameters.AddWithValue("@idEquipoComprador", idEquipoComprador);
                        command.Parameters.AddWithValue("@idEquipoVendedor", idEquipoVendedor);

                        // Ejecutamos la consulta y obtenemos el número de coincidencias
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        ofertaAceptada = (count > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
            return ofertaAceptada;
        }

        // Metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaEquipo(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            int respuestaEquipo = 0;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT respuesta_equipo 
                                     FROM transferencias 
                                     WHERE id_jugador = @idJugador 
                                        AND id_equipo_origen = @idEquipoVendedor 
                                        AND id_equipo_destino = @idEquipoComprador
                                        AND tipo_fichaje = 1";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@idJugador", idJugador);
                        command.Parameters.AddWithValue("@idEquipoComprador", idEquipoComprador);
                        command.Parameters.AddWithValue("@idEquipoVendedor", idEquipoVendedor);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Si hay un resultado
                            {
                                respuestaEquipo = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
            return respuestaEquipo;
        }

        // Método que actualiza la fecha de traspaso
        public void ActualizarFechaTraspaso(int idJugador, int idEquipoDestino, int idEquipoOrigen, int tipoFichaje)
        {
            MessageBox.Show("Has entrado a ActualizarFechaTraspaso");
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE transferencias
                                     SET respuesta_jugador = 1, fecha_traspaso = 
                                          CASE 
                                            -- Si la oferta es en enero (01), julio (07) o agosto (08), el traspaso es un día después
                                            WHEN strftime('%m', fecha_oferta) IN ('01', '07', '08') 
                                            THEN date(fecha_oferta, '+1 day')

                                            -- Si la oferta es entre septiembre y diciembre, el traspaso será el 1 de enero del siguiente año
                                            WHEN strftime('%m', fecha_oferta) BETWEEN '09' AND '12'
                                            THEN strftime('%Y', fecha_oferta, '+1 year') || '-01-01' 
                        
                                            -- Si la oferta es entre febrero y junio, el traspaso será el 1 de julio del mismo año
                                            ELSE strftime('%Y', fecha_oferta) || '-07-01'
                                          END
                                     WHERE id_jugador = @idJugador 
                                          AND id_equipo_origen = @idEquipoOrigen 
                                          AND id_equipo_destino = @idEquipoDestino 
                                          AND tipo_fichaje = @tipoFichaje;";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", idJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", idEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", idEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", tipoFichaje);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

    }
}
