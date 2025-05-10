using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.UserControls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmTraspasoJugador : Window
    {
        private Jugador _jugador;
        private Manager _manager;
        private int _equipo;

        JugadorLogica _logicaJugador = new JugadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        MensajeLogica _logicaMensaje = new MensajeLogica();
        OjearLogica _logicaOjear = new OjearLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        private UC_PantallaPrincipal _pantallaPrincipal;

        bool desplegableActivado = false;
        int porcentajeNegociacion = 0;
        int porcentajePaciencia = 0;

        private static MediaPlayer mediaPlayer = new MediaPlayer(); // Inicialización al declarar

        public frmTraspasoJugador(Jugador jugador, Manager manager, int equipo, UC_PantallaPrincipal pantallaPrincipal)
        {
            InitializeComponent();
            _jugador = jugador;
            _manager = manager;
            _equipo = equipo;
            _pantallaPrincipal = pantallaPrincipal;

            // Código que inicializa el sonido de fondo 
            try
            {
                Metodos.ReproducirMusica("backgroundMusic2.wav");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Comprobar si el jugador ha sido ojeado.
            bool ojeado = _logicaOjear.ComprobarJugadorOjeado(_jugador.IdJugador, _manager);

            // Cargar datos del Jugador
            txtDorsal.Text = _jugador.Dorsal.ToString();
            if (_jugador.NombreCompleto != null)
            {
                txtNombreJugador.Text = _jugador.NombreCompleto.ToUpper();
            }

            imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + _jugador.IdJugador + ".png"));
            imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + _jugador.IdEquipo + ".png"));
            lblAverage.Text = _jugador.Media.ToString();
            elipseMedia.Stroke = DeterminarColorElipse(_jugador.Media);

            txtEquipoJugador.Text = _logicaEquipo.ListarDetallesEquipo(_jugador.IdEquipo).Nombre;
            txtPosicionJugador.Text = _jugador.Rol;

            if (ojeado == true)
            {
                int status = _jugador.Status;
                if (status == 1)
                {
                    txtStatusJugador.Text = "Clave";
                }
                else if (status == 2)
                {
                    txtStatusJugador.Text = "Importante";
                }
                else if (status == 3)
                {
                    txtStatusJugador.Text = "Rotación";
                }
                else if (status == 4)
                {
                    txtStatusJugador.Text = "Ocasional";
                }
                txtSalarioJugador.Text = string.Format(new CultureInfo("es-ES"), "{0:N0} €", _jugador.SalarioTemporada);
                txtClausulaJugador.Text = string.Format(new CultureInfo("es-ES"), "{0:N0} €", _jugador.ClausulaRescision);

                int? anios = _jugador.AniosContrato;
                if (anios == 1)
                {
                    txtAniosJugador.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                txtAniosJugador.Text = _jugador.AniosContrato.ToString();
                txtValorMercado.Text = string.Format(new CultureInfo("es-ES"), "{0:N0} €", _jugador.ValorMercado);
            }
            else
            {
                txtStatusJugador.Text = "";
                txtSalarioJugador.Text = "";
                txtClausulaJugador.Text = "";
                txtAniosJugador.Text = "";
                txtValorMercado.Text = "";
            }

            // COMPROBAR SI EL JUGADOR TIENE OFERTAS DE CESION SOBRE EL
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador);

            // Comprobar si el jugador ya tiene una oferta de mi equipo
            int comprobacion = _logicaTransferencia.ComprobarRespuestaEquipoCesion(jugador.IdJugador, _equipo, jugador.IdEquipo);
            if (comprobacion == 1)
            {
                btnCesion.Visibility = Visibility.Hidden;
            } else
            {
                btnCesion.Visibility = Visibility.Visible;
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
        }

        // ---------------------------------------------------- Evento CLICK del botón CANCELAR NEGOCIACIÓN
        private void btnCancelarNegociacion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // ---------------------------------------------------- Evento CLICK del botón ENVIAR OFERTA
        private void btnEnviarOferta_Click(object sender, RoutedEventArgs e)
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador);
            // Recogemos el valor de la oferta de la caja de texto
            string textoMontoOferta = txtOfertaTraspaso.Text; // Texto original con puntos y símbolo €
            string textoMontoOfertaSinSimbolos = System.Text.RegularExpressions.Regex.Replace(textoMontoOferta, @"[^\d]", ""); // Elimina todo lo que no sea un dígito
            int ofertaEquipo = int.Parse(textoMontoOfertaSinSimbolos); // Convierte el texto limpio a int

            Transferencia oferta = _logicaTransferencia.EvaluarOfertaEquipo(_jugador.IdJugador, _equipo, ofertaEquipo, 1);

            // Recogida de datos
            int valorMercado = oferta.ValorMercado;
            int situacionMercado = oferta.SituacionMercado;
            int moral = oferta.Moral;
            int estadoAnimo = oferta.EstadoAnimo;
            string fechaFinContrato = oferta.FinContrato;
            int clausulaRescision = oferta.ClausulaRescision;
            int equipoActual = oferta.IdEquipoOrigen;
            int presupuestoEquipoVendedor = oferta.PresupuestoVendedor;
            int rival = oferta.Rival;
            int presupuestoEquipoComprador = oferta.PresupuestoComprador;
            int montoOferta = oferta.MontoOferta;
            int equipoOrigen = oferta.IdEquipoOrigen;
            int equipoDestino = oferta.IdEquipoDestino;
            int status = oferta.Status;

            // 📌 Sistema de Puntuación
            int puntosOferta = 0;
            int tipoRechazo = 0;

            // ✔ Factor 1: Comparación con el valor de mercado
            if (montoOferta >= valorMercado * 1.5) puntosOferta += 3;
            else if (montoOferta >= valorMercado * 1.2) puntosOferta += 2;
            else if (montoOferta >= valorMercado * 1.0) puntosOferta += 1;
            else puntosOferta -= 2; // Oferta demasiado baja

            // ✔ Factor 2: Situación en el mercado (jugador transferible o cedible)
            if (situacionMercado == 1 || situacionMercado == 2) puntosOferta += 3; // Más fácil de aceptar si es transferible

            // ✔ Factor 3: Estado del jugador
            if (estadoAnimo < 30 || moral < 30) puntosOferta += 2; // Jugador infeliz, más probable que lo vendan

            // ✔ Factor 4: Necesidad económica del equipo
            if (presupuestoEquipoVendedor < 5000000 && montoOferta > valorMercado * 0.8) puntosOferta += 3;

            // ✔ Factor 5: Duración del contrato
            DateTime fechaFin = DateTime.Parse(fechaFinContrato);
            DateTime hoy = DateTime.Today;
            int mesesRestantes = ((fechaFin.Year - hoy.Year) * 12) + fechaFin.Month - hoy.Month;

            if (mesesRestantes <= 6 && montoOferta > valorMercado * 0.50) puntosOferta += 2;

            // ✔ Factor 6: Status en el Equipo
            if (status >= 3 && montoOferta > valorMercado * 0.75) puntosOferta += 2;

            // ❌ Factor 7: Rivalidad entre equipos
            if (equipoOrigen == rival)
            {
                puntosOferta -= 5;
            }

            // 📌 Factor 8: Cláusula de rescisión (aceptación instantánea)
            if (montoOferta >= clausulaRescision)
            {
                puntosOferta = 5;
            }

            // ❌ Factor 9: Presupuesto del comprador
            if (presupuestoEquipoComprador < montoOferta)
            {
                puntosOferta = 0;
                tipoRechazo = 1;
            }

            // ✔ Decisión final: ¿La oferta es suficientemente buena?
            if (puntosOferta >= 5)
            {
                // Comprobar si el equipo ya esta negociando
                bool enNegociacion = _logicaTransferencia.ComprobarOfertaActiva(_jugador.IdJugador, _equipo, _jugador.IdEquipo);
                Transferencia ofertaAceptada = new Transferencia
                {
                    IdJugador = oferta.IdJugador,
                    IdEquipoOrigen = _logicaJugador.MostrarDatosJugador(oferta.IdJugador).IdEquipo,
                    IdEquipoDestino = _equipo,
                    TipoFichaje = 1,
                    MontoOferta = ofertaEquipo,
                    FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                    FechaTraspaso = "",
                    RespuestaEquipo = 1,
                    RespuestaJugador = 0,
                    SalarioAnual = 0,
                    ClausulaRescision = 0,
                    Duracion = 0,
                    BonoPorGoles = 0,
                    BonoPorPartidos = 0,
                };

                if (enNegociacion != true)
                {
                    _logicaTransferencia.RegistrarOferta(ofertaAceptada);
                }
                else
                {
                    _logicaTransferencia.ActualizarOferta(ofertaAceptada);
                }

                // Crear el mensaje confirmando la aceptacion de un oferta de traspaso
                Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                string nombreJugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto;

                Mensaje mensajeEquipoAceptaOferta = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                    Asunto = "Oferta aceptada",
                    Contenido = $"El {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre.ToUpper()} ha aceptado nuestra oferta de {ofertaEquipo.ToString("N0", new CultureInfo("es-ES"))}€ por {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto.ToUpper()}. Nos han autorizado a iniciar conversaciones directas con el jugador y su representante para negociar los términos personales del contrato.\n\nPuedes comenzar la negociación en cuanto lo consideres oportuno. Te recomendamos actuar con agilidad para cerrar el acuerdo antes de que otros clubes se interpongan.",
                    TipoMensaje = "Notificación",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = jugador.IdJugador
                };

                _logicaMensaje.crearMensaje(mensajeEquipoAceptaOferta);

                // Ventana emergente diciendo que el equipo ha aceptado la oferta.
                string titulo = "INFORMACIÓN";
                string mensaje = $"El {_logicaEquipo.ListarDetallesEquipo(equipoOrigen).Nombre.ToUpper()} ha aceptado tu oferta por {_logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto.ToUpper()}. Ahora ya puedes negociar con el jugador.";
                frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                mensajeEmergente.ShowDialog();

                this.Close();
            }
            else
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "";
                if (tipoRechazo == 0)
                {
                    mensaje = $"La oferta ha sido rechazada por el {_logicaEquipo.ListarDetallesEquipo(equipoOrigen).Nombre.ToUpper()}. La considera insuficiente";
                }
                else if (tipoRechazo == 1)
                {
                    mensaje = $"La oferta ha sido cancelada ya que no dispones de suficiente dinero.";
                }
                frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                mensajeEmergente.ShowDialog();

                // Comprobar si el equipo ya esta negociando
                bool enNegociacion = _logicaTransferencia.ComprobarOfertaActiva(_jugador.IdJugador, _equipo, _jugador.IdEquipo);
                Transferencia ofertaRechazada = new Transferencia
                {
                    IdJugador = oferta.IdJugador,
                    IdEquipoOrigen = _logicaJugador.MostrarDatosJugador(oferta.IdJugador).IdEquipo,
                    IdEquipoDestino = _equipo,
                    TipoFichaje = 1,
                    MontoOferta = ofertaEquipo,
                    FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                    FechaTraspaso = "",
                    RespuestaEquipo = 0,
                    RespuestaJugador = 0,
                    SalarioAnual = 0,
                    ClausulaRescision = 0,
                    Duracion = 0,
                    BonoPorGoles = 0,
                    BonoPorPartidos = 0,
                };

                
                if (enNegociacion != true)
                {
                    _logicaTransferencia.RegistrarOferta(ofertaRechazada);
                }
                else
                {
                    _logicaTransferencia.ActualizarOferta(ofertaRechazada);
                }

                // No querer negociar en 2 semanas
                _logicaJugador.NegociacionCancelada(oferta.IdJugador, 14);

                // Crear el mensaje confirmando el rechazo de la oferta de traspaso
                Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                string nombreJugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto;

                Mensaje mensajeEquipoAceptaOferta = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                    Asunto = "Oferta rechazada",
                    Contenido = $"El {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre.ToUpper()} ha rechazado nuestra oferta de {ofertaEquipo.ToString("N0", new CultureInfo("es-ES"))}€ por {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto.ToUpper()}. El club considera que las condiciones económicas ofrecidas no se ajustan a sus expectativas o al valor que otorgan al futbolista en su plantilla.\n\nSi deseas, podemos revisar y ajustar la propuesta para intentar una nueva ofensiva dentro de 2 semanas, o explorar otras opciones en el mercado que se ajusten a las necesidades del equipo.",
                    TipoMensaje = "Notificación",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = jugador.IdJugador
                };

                _logicaMensaje.crearMensaje(mensajeEquipoAceptaOferta);

                btnEnviarOferta.Visibility = Visibility.Collapsed;
                btnCancelarNegociacion.Content = "ABANDONAR NEGOCIACIÓN";
                btnCancelarNegociacion.Background = (Brush)new BrushConverter().ConvertFromString("#569704");
                btnCesion.IsEnabled = false;
                txtOfertaTraspaso.IsEnabled = false;
            }
        }

        // ---------------------------------------------------- Evento CLICK del botón PEDIR CESIÓN
        private void btnCesion_Click(object sender, RoutedEventArgs e)
        {
            Transferencia transaccion = _logicaTransferencia.EvaluarOfertaEquipo(_jugador.IdJugador, _equipo, 0, 2);
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador);
            DateTime? proximaNegociacion = jugador.ProximaNegociacion;
            string mensajeProximaNegociacion = proximaNegociacion.HasValue
                ? proximaNegociacion.Value.ToString("dd/MM/yyyy")
                : "(No hay una fecha disponible)";

            if (jugador.ProximaNegociacion > Metodos.hoy)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "En estos momentos el " + (jugador.NombreEquipo ?? "el equipo") + " no quiere reunirse contigo. A partir del próximo " + (proximaNegociacion?.ToString("dd/MM/yyyy") ?? "No disponible") + " puedes volver a intentarlo.";
                frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                mensajeEmergente.ShowDialog();
            }
            else
            {
                // Verificar disponibilidad para la cesion.
                DateTime hoy = Metodos.hoy;

                // Ventanas de fichajes
                DateTime inicioVerano = new DateTime(hoy.Year, 7, 1);
                DateTime finVerano = new DateTime(hoy.Year, 8, 30);
                DateTime inicioInvierno = new DateTime(hoy.Year, 1, 1);
                DateTime finInvierno = new DateTime(hoy.Year, 1, 31);

                DateTime siguienteFecha = DateTime.MinValue;

                if ((hoy >= inicioVerano && hoy <= finVerano) || (hoy >= inicioInvierno && hoy <= finInvierno))
                {
                    // 📌 Sistema de Puntuación
                    int puntosCesion = 0;
                    int tipoRechazoCesion = 0;
                    Random rnd = new Random();

                    if (transaccion.Status == 1 || transaccion.Status == 2)
                    {
                        puntosCesion += 0;
                        tipoRechazoCesion = 1;
                    }
                    else if (transaccion.Status == 3)
                    {
                        int numero = rnd.Next(1, 5); // Genera 1 a 4 (25% opciones)
                        if (numero == 2)
                        {
                            puntosCesion += 1;
                        }
                    }
                    else if (transaccion.Status == 4)
                    {
                        int numero = rnd.Next(1, 3); // Genera 1 o 2 (50% opciones)
                        if (numero == 1)
                        {
                            puntosCesion += 1;
                        }
                    }

                    if (puntosCesion > 0)
                    {
                        // Comprobamos la fecha de Traspaso dentro de los periodos de fichajes
                        DateTime fechaTraspaso;

                        // Comprobar si hoy está en un rango válido
                        bool enRangoEnero = hoy >= inicioInvierno && hoy <= finInvierno;
                        bool enRangoVerano = hoy >= inicioVerano && hoy <= finVerano;

                        if (enRangoEnero || enRangoVerano)
                        {
                            fechaTraspaso = hoy.AddDays(1);
                        }
                        else
                        {
                            // Determinar el próximo rango válido
                            if (hoy < inicioInvierno)
                            {
                                fechaTraspaso = inicioInvierno;
                            }
                            else if (hoy < inicioVerano)
                            {
                                fechaTraspaso = inicioVerano;
                            }
                            else
                            {
                                // Después del 30 de agosto, ir al enero del siguiente año
                                fechaTraspaso = new DateTime(hoy.Year + 1, 1, 1);
                            }
                        }


                        Transferencia oferta = new Transferencia
                        {
                            IdJugador = jugador.IdJugador,
                            IdEquipoOrigen = jugador.IdEquipo,
                            IdEquipoDestino = _equipo,
                            TipoFichaje = 2,
                            MontoOferta = 0,
                            FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                            FechaTraspaso = fechaTraspaso.ToString("yyyy-MM-dd"),
                            RespuestaEquipo = 1,
                            RespuestaJugador = 1,
                            SalarioAnual = 0,
                            ClausulaRescision = 0,
                            Duracion = 0,
                            BonoPorGoles = 0,
                            BonoPorPartidos = 0,
                        };

                        // Comprobar si el jugador ya tiene una oferta de mi equipo
                        bool comprobacion = _logicaTransferencia.ComprobarOfertaActiva(jugador.IdJugador, _equipo, jugador.IdEquipo);
                        if (comprobacion == true)
                        {
                            // Actualizar la oferta
                            _logicaTransferencia.ActualizarOferta(oferta);
                        }
                        else
                        {
                            // Registrar la oferta
                            _logicaTransferencia.RegistrarOferta(oferta);
                        }


                        string titulo = "INFORMACIÓN";
                        string mensaje = $"El {jugador.NombreEquipo} ha aceptado la oferta de cesión por {jugador.NombreCompleto}.";
                        frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        mensajeEmergente.ShowDialog();

                        btnEnviarOferta.Visibility = Visibility.Collapsed;
                        btnCancelarNegociacion.Content = "CONFIRMAR CESIÓN";
                        btnCancelarNegociacion.Background = (Brush)new BrushConverter().ConvertFromString("#569704");
                        btnCesion.IsEnabled = false;
                        txtOfertaTraspaso.IsEnabled = false;

                        // Registrar la transferencia ya confirmada
                        _logicaTransferencia.RegistrarTransferencia(oferta);

                        // Crear el mensaje confirmando la cesión de un jugador
                        Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                        string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                        string nombreJugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto;

                        Mensaje mensajeJugadorCedido = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                            Asunto = "Confirmación de cesión",
                            Contenido = $"Nos complace informarte que hemos cerrado satisfactoriamente la cesión de {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto.ToUpper()} procedente del {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre.ToUpper()}. El acuerdo es válido hasta el final de la presente temporada y el jugador estará disponible en nuestra plantilla a partir de mañana.\n\nCreemos que esta cesión será beneficiosa para su desarrollo, ya que contará con más minutos en un entorno competitivo. Estaremos atentos a su evolución durante su estancia en su nuevo club.\n\nAgradecemos tu gestión y quedamos a tu disposición para cualquier consulta adicional.",
                            TipoMensaje = "Notificación",
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Leido = false,
                            Icono = jugador.IdJugador
                        };

                        _logicaMensaje.crearMensaje(mensajeJugadorCedido);
                    }
                    else
                    {
                        if (tipoRechazoCesion == 1)
                        {
                            string titulo = "INFORMACIÓN";
                            string mensaje = $"El {jugador.NombreEquipo} ha rechazado la oferta de cesión por {jugador.NombreCompleto} ya que lo considera un jugador clave de su equipo.";
                            frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            mensajeEmergente.ShowDialog();
                        }
                        else
                        {
                            string titulo = "INFORMACIÓN";
                            string mensaje = $"El {jugador.NombreEquipo} no quiere ceder a su jugador {jugador.NombreCompleto} en estos momentos.";
                            frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            mensajeEmergente.ShowDialog();
                        }

                        Transferencia oferta = new Transferencia
                        {
                            IdJugador = jugador.IdJugador,
                            IdEquipoOrigen = jugador.IdEquipo,
                            IdEquipoDestino = _equipo,
                            TipoFichaje = 2,
                            MontoOferta = 0,
                            FechaOferta = Metodos.hoy.ToString(),
                            RespuestaEquipo = 0,
                            RespuestaJugador = 0
                        };

                        // Comprobar si el jugador ya tiene una oferta de mi equipo
                        bool comprobacion = _logicaTransferencia.ComprobarOfertaActiva(jugador.IdJugador, _equipo, jugador.IdEquipo);
                        if (comprobacion == true)
                        {
                            // Actualizar la oferta
                            _logicaTransferencia.ActualizarOferta(oferta);
                        }
                        else
                        {
                            // Registrar la oferta
                            _logicaTransferencia.RegistrarOferta(oferta);
                        }

                        _logicaJugador.NegociacionCancelada(jugador.IdJugador, 7);

                        // Crear el mensaje confirmando la cesión de un jugador
                        Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                        string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                        string nombreJugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto;

                        Mensaje mensajeJugadorCedido = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                            Asunto = "Oferta de cesión rechazada",
                            Contenido = $"Lamentamos informarte que el {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre.ToUpper()} ha rechazado nuestra oferta de cesión por {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto.ToUpper()}.\n\nTras evaluar la propuesta, nos han comunicado que el jugador es parte de sus planes a corto plazo y no contemplan su salida en este momento.",
                            TipoMensaje = "Notificación",
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Leido = false,
                            Icono = jugador.IdJugador
                        };

                        _logicaMensaje.crearMensaje(mensajeJugadorCedido);

                        btnEnviarOferta.Visibility = Visibility.Collapsed;
                        btnCancelarNegociacion.Content = "ABANDONAR NEGOCIACIÓN";
                        btnCancelarNegociacion.Background = (Brush)new BrushConverter().ConvertFromString("#569704");
                        btnCesion.IsEnabled = false;
                        txtOfertaTraspaso.IsEnabled = false;
                    }
                }
                else
                {
                    // Determinar la próxima fecha disponible
                    if (hoy < inicioInvierno)
                    {
                        siguienteFecha = inicioInvierno;
                    }
                    else if (hoy > finInvierno && hoy < inicioVerano)
                    {
                        siguienteFecha = inicioVerano;
                    }
                    else // hoy > finVerano
                    {
                        siguienteFecha = new DateTime(hoy.Year + 1, 1, 1);
                    }

                    string titulo = "INFORMACIÓN";
                    string mensaje = $"En este momento el mercado de traspasos está cerrado.\nInténtalo a partir del próximo {siguienteFecha:dd MMMM yyyy}.";
                    frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                    mensajeEmergente.ShowDialog();
                }
            }  
        }

        #region "Métodos"
        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos is int mediaValue)
            {
                if (mediaValue > 90)
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
                if (mediaValue >= 80)
                    return Brushes.DarkGreen; // Verde oscuro
                if (mediaValue >= 65)
                    return Brushes.Orange; // Naranja
                return Brushes.Red; // Rojo
            }
            return Brushes.Black; // Por defecto
        }
        #endregion
    }
}
