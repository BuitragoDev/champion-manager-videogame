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
                                            e.presupuesto AS presupuestoEquipoVendedor, e.rival,
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
                                    Rival = reader.GetInt32(reader.GetOrdinal("rival")),
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

        // ------------------------------------------------------------- Metodo que registra una oferta por un jugador.
        public void RegistrarOferta(Transferencia oferta)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO ofertas (
                                        id_jugador, id_equipo_origen, id_equipo_destino, tipo_fichaje, 
                                        monto_oferta, fecha_oferta, fecha_traspaso, respuesta_equipo, respuesta_jugador,
                                        salario_anual, clausula_rescision, duracion, bono_por_goles, bono_por_partidos
                                     ) 
                                     VALUES (
                                        @idJugador, @idEquipoOrigen, @idEquipoDestino, @tipoFichaje, 
                                        @montoOferta, @fechaOferta, @fechaTraspaso, @RespuestaEquipo, @RespuestaJugador,
                                        @salario, @clausula, @duracion, @bonoGoles, @bonoPartidos
                                     )";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", oferta.IdJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", oferta.IdEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", oferta.IdEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", oferta.TipoFichaje);
                        command.Parameters.AddWithValue("@montoOferta", oferta.MontoOferta);
                        command.Parameters.AddWithValue("@fechaOferta", oferta.FechaOferta);
                        command.Parameters.AddWithValue("@fechaTraspaso", oferta.FechaTraspaso);
                        command.Parameters.AddWithValue("@RespuestaEquipo", oferta.RespuestaEquipo);
                        command.Parameters.AddWithValue("@RespuestaJugador", oferta.RespuestaJugador);
                        command.Parameters.AddWithValue("@salario", oferta.SalarioAnual);
                        command.Parameters.AddWithValue("@clausula", oferta.ClausulaRescision);
                        command.Parameters.AddWithValue("@duracion", oferta.Duracion);
                        command.Parameters.AddWithValue("@bonoGoles", oferta.BonoPorGoles);
                        command.Parameters.AddWithValue("@bonoPartidos", oferta.BonoPorPartidos);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ------------------------------------------------------------- Metodo que registra una transferencia por un jugador.
        public void RegistrarTransferencia(Transferencia transfer)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"INSERT INTO transferencias (
                                        id_jugador, id_equipo_origen, id_equipo_destino, tipo_fichaje, 
                                        monto_oferta, fecha_oferta, fecha_traspaso, respuesta_equipo, respuesta_jugador,
                                        salario_anual, clausula_rescision, duracion, bono_por_goles, bono_por_partidos
                                     ) 
                                     VALUES (
                                        @idJugador, @idEquipoOrigen, @idEquipoDestino, @tipoFichaje, 
                                        @montoOferta, @fechaOferta, @fechaTraspaso, @RespuestaEquipo, @RespuestaJugador,
                                        @salario, @clausula, @duracion, @bonoGoles, @bonoPartidos
                                     )";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", transfer.IdJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", transfer.IdEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", transfer.IdEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", transfer.TipoFichaje);
                        command.Parameters.AddWithValue("@montoOferta", transfer.MontoOferta);
                        command.Parameters.AddWithValue("@fechaOferta", transfer.FechaOferta);
                        command.Parameters.AddWithValue("@fechaTraspaso", transfer.FechaTraspaso);
                        command.Parameters.AddWithValue("@RespuestaEquipo", transfer.RespuestaEquipo);
                        command.Parameters.AddWithValue("@RespuestaJugador", transfer.RespuestaJugador);
                        command.Parameters.AddWithValue("@salario", transfer.SalarioAnual);
                        command.Parameters.AddWithValue("@clausula", transfer.ClausulaRescision);
                        command.Parameters.AddWithValue("@duracion", transfer.Duracion);
                        command.Parameters.AddWithValue("@bonoGoles", transfer.BonoPorGoles);
                        command.Parameters.AddWithValue("@bonoPartidos", transfer.BonoPorPartidos);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------------------- Metodo que actualiza una oferta por un jugador.
        public void ActualizarOferta(Transferencia oferta)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"UPDATE ofertas 
                                     SET monto_oferta = @montoOferta, 
                                         fecha_oferta = @fechaOferta, 
                                         fecha_traspaso = @fechaTraspaso,
                                         salario_anual = @salario,
                                         clausula_rescision = @clausula,
                                         duracion = @duracion,
                                         bono_por_goles = @bonoGoles,
                                         bono_por_partidos = @bonoPartidos,
                                         respuesta_equipo = @RespuestaEquipo,
                                         respuesta_jugador = @RespuestaJugador
                                     WHERE id_jugador = @idJugador 
                                       AND id_equipo_origen = @idEquipoOrigen 
                                       AND id_equipo_destino = @idEquipoDestino 
                                       AND tipo_fichaje = @tipoFichaje";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@idJugador", oferta.IdJugador);
                        command.Parameters.AddWithValue("@idEquipoOrigen", oferta.IdEquipoOrigen);
                        command.Parameters.AddWithValue("@idEquipoDestino", oferta.IdEquipoDestino);
                        command.Parameters.AddWithValue("@tipoFichaje", oferta.TipoFichaje);
                        command.Parameters.AddWithValue("@montoOferta", oferta.MontoOferta);
                        command.Parameters.AddWithValue("@fechaOferta", oferta.FechaOferta);
                        command.Parameters.AddWithValue("@fechaTraspaso", oferta.FechaTraspaso);
                        command.Parameters.AddWithValue("@RespuestaEquipo", oferta.RespuestaEquipo);
                        command.Parameters.AddWithValue("@RespuestaJugador", oferta.RespuestaJugador);
                        command.Parameters.AddWithValue("@salario", oferta.SalarioAnual);
                        command.Parameters.AddWithValue("@clausula", oferta.ClausulaRescision);
                        command.Parameters.AddWithValue("@duracion", oferta.Duracion);
                        command.Parameters.AddWithValue("@bonoGoles", oferta.BonoPorGoles);
                        command.Parameters.AddWithValue("@bonoPartidos", oferta.BonoPorPartidos);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ----------------------------------------------------- Metodo que comprueba si el jugador ya tiene una oferta aceptada por el equipo
        public bool ComprobarOfertaActiva(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            bool ofertaAceptada = false;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * 
                                     FROM ofertas 
                                     WHERE id_jugador = @idJugador 
                                        AND id_equipo_origen = @idEquipoVendedor 
                                        AND id_equipo_destino = @idEquipoComprador";

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

        // --------------------------------------------------- Metodo que comprueba si el jugador ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaJugador(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            int? respuestaJugador = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT respuesta_jugador 
                             FROM ofertas 
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
                            if (reader.Read())
                            {
                                respuestaJugador = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            // Si no se encontró ningún registro, devolvemos -1
            return respuestaJugador ?? -1;
        }

        // --------------------------------------------------- Metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaEquipo(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            int? respuestaEquipo = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT respuesta_equipo 
                             FROM ofertas 
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
                            if (reader.Read())
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

            // Si no se encontró ningún registro, devolvemos -1
            return respuestaEquipo ?? -1;
        }

        // Metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta de CESION por el jugador
        public int ComprobarRespuestaEquipoCesion(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            int? respuestaEquipo = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT respuesta_equipo 
                                     FROM ofertas 
                                     WHERE id_jugador = @idJugador 
                                        AND id_equipo_origen = @idEquipoVendedor 
                                        AND id_equipo_destino = @idEquipoComprador
                                        AND tipo_fichaje = 2";

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
            // Si no se encontró ningún registro, devolvemos -1
            return respuestaEquipo ?? -1;
        }

        // ---------------------------------------------------------------------- Método para Mostrar la Lista de Ofertas
        public List<Transferencia> ListarOfertas()
        {
            List<Transferencia> oTransferencia = new List<Transferencia>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * FROM ofertas";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oTransferencia.Add(new Transferencia()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    IdEquipoOrigen = reader.GetInt32(reader.GetOrdinal("id_equipo_origen")),
                                    IdEquipoDestino = reader.GetInt32(reader.GetOrdinal("id_equipo_destino")),
                                    TipoFichaje = reader.GetInt32(reader.GetOrdinal("tipo_fichaje")),
                                    FechaOferta = reader["fecha_oferta"]?.ToString() ?? string.Empty,
                                    FechaTraspaso = reader["fecha_traspaso"]?.ToString() ?? string.Empty,
                                    RespuestaEquipo = reader.IsDBNull(reader.GetOrdinal("respuesta_equipo"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_equipo")),
                                    RespuestaJugador = reader.IsDBNull(reader.GetOrdinal("respuesta_jugador"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_jugador")),
                                    MontoOferta = reader.GetInt32(reader.GetOrdinal("monto_oferta")),
                                    SalarioAnual = reader.GetInt32(reader.GetOrdinal("salario_anual")),
                                    ClausulaRescision = reader.GetInt32(reader.GetOrdinal("clausula_rescision")),
                                    Duracion = reader.GetInt32(reader.GetOrdinal("duracion")),
                                    BonoPorGoles = reader.GetInt32(reader.GetOrdinal("bono_por_goles")),
                                    BonoPorPartidos = reader.GetInt32(reader.GetOrdinal("bono_por_partidos")),
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

            return oTransferencia;
        }

        // ---------------------------------------------------------------------- Método para Mostrar la Lista de Traspasos y Cesiones
        public List<Transferencia> ListarTraspasos()
        {
            List<Transferencia> oTransferencia = new List<Transferencia>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * FROM transferencias";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oTransferencia.Add(new Transferencia()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    IdEquipoOrigen = reader.GetInt32(reader.GetOrdinal("id_equipo_origen")),
                                    IdEquipoDestino = reader.GetInt32(reader.GetOrdinal("id_equipo_destino")),
                                    TipoFichaje = reader.GetInt32(reader.GetOrdinal("tipo_fichaje")),
                                    FechaOferta = reader["fecha_oferta"]?.ToString() ?? string.Empty,
                                    FechaTraspaso = reader["fecha_traspaso"]?.ToString() ?? string.Empty,
                                    RespuestaEquipo = reader.IsDBNull(reader.GetOrdinal("respuesta_equipo"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_equipo")),
                                    RespuestaJugador = reader.IsDBNull(reader.GetOrdinal("respuesta_jugador"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_jugador")),
                                    MontoOferta = reader.GetInt32(reader.GetOrdinal("monto_oferta")),
                                    SalarioAnual = reader.GetInt32(reader.GetOrdinal("salario_anual")),
                                    ClausulaRescision = reader.GetInt32(reader.GetOrdinal("clausula_rescision")),
                                    Duracion = reader.GetInt32(reader.GetOrdinal("duracion")),
                                    BonoPorGoles = reader.GetInt32(reader.GetOrdinal("bono_por_goles")),
                                    BonoPorPartidos = reader.GetInt32(reader.GetOrdinal("bono_por_partidos")),
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

            return oTransferencia;
        }

        // ---------------------------------------------------------------------- Método para Mostrar la Lista de Ofertas Sin Finalizar
        public List<Transferencia> ListarOfertasSinFinalizar()
        {
            List<Transferencia> oTransferencia = new List<Transferencia>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * FROM ofertas WHERE respuesta_jugador = 0";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oTransferencia.Add(new Transferencia()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    IdEquipoOrigen = reader.GetInt32(reader.GetOrdinal("id_equipo_origen")),
                                    IdEquipoDestino = reader.GetInt32(reader.GetOrdinal("id_equipo_destino")),
                                    TipoFichaje = reader.GetInt32(reader.GetOrdinal("tipo_fichaje")),
                                    FechaOferta = reader["fecha_oferta"]?.ToString() ?? string.Empty,
                                    FechaTraspaso = reader["fecha_traspaso"]?.ToString() ?? string.Empty,
                                    RespuestaEquipo = reader.IsDBNull(reader.GetOrdinal("respuesta_equipo"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_equipo")),
                                    RespuestaJugador = reader.IsDBNull(reader.GetOrdinal("respuesta_jugador"))
                                                        ? (int?)null
                                                        : reader.GetInt32(reader.GetOrdinal("respuesta_jugador")),
                                    MontoOferta = reader.GetInt32(reader.GetOrdinal("monto_oferta")),
                                    SalarioAnual = reader.GetInt32(reader.GetOrdinal("salario_anual")),
                                    ClausulaRescision = reader.GetInt32(reader.GetOrdinal("clausula_rescision")),
                                    Duracion = reader.GetInt32(reader.GetOrdinal("duracion")),
                                    BonoPorGoles = reader.GetInt32(reader.GetOrdinal("bono_por_goles")),
                                    BonoPorPartidos = reader.GetInt32(reader.GetOrdinal("bono_por_partidos")),
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

            return oTransferencia;
        }

        // ------------------------------------------------------------- Metodo que borra una oferta por un jugador
        public void BorrarOferta(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"DELETE FROM ofertas WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand command = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetros
                        command.Parameters.AddWithValue("@IdJugador", jugador);

                        // Ejecutar la consulta
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método para Mostrar los detalles de la oferta de un jugador
        public Transferencia MostrarDetallesOferta(int jugador)
        {
            Transferencia transferencia = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT * FROM ofertas WHERE id_jugador = @IdJugador ORDER BY fecha_oferta DESC LIMIT 1";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transferencia = new Transferencia()
                                {
                                    IdJugador = reader.GetInt32(reader.GetOrdinal("id_jugador")),
                                    IdEquipoOrigen = reader.GetInt32(reader.GetOrdinal("id_equipo_origen")),
                                    IdEquipoDestino = reader.GetInt32(reader.GetOrdinal("id_equipo_destino")),
                                    TipoFichaje = reader.GetInt32(reader.GetOrdinal("tipo_fichaje")),
                                    FechaOferta = reader["fecha_oferta"]?.ToString() ?? string.Empty,
                                    FechaTraspaso = reader["fecha_traspaso"]?.ToString() ?? string.Empty,
                                    RespuestaEquipo = reader.IsDBNull(reader.GetOrdinal("respuesta_equipo"))
                                        ? (int?)null
                                        : reader.GetInt32(reader.GetOrdinal("respuesta_equipo")),
                                    RespuestaJugador = reader.IsDBNull(reader.GetOrdinal("respuesta_jugador"))
                                        ? (int?)null
                                        : reader.GetInt32(reader.GetOrdinal("respuesta_jugador")),
                                    MontoOferta = reader.GetInt32(reader.GetOrdinal("monto_oferta")),
                                    SalarioAnual = reader.GetInt32(reader.GetOrdinal("salario_anual")),
                                    ClausulaRescision = reader.GetInt32(reader.GetOrdinal("clausula_rescision")),
                                    Duracion = reader.GetInt32(reader.GetOrdinal("duracion")),
                                    BonoPorGoles = reader.GetInt32(reader.GetOrdinal("bono_por_goles")),
                                    BonoPorPartidos = reader.GetInt32(reader.GetOrdinal("bono_por_partidos")),
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

            return transferencia;
        }

    }
}
