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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmNegociacionesJugador : Window
    {
        private Jugador _jugador;
        private Manager _manager;
        private int _equipo;
        private int _tipoNegociacion;
        private UC_PantallaPrincipal _pantallaPrincipal;

        JugadorLogica _logicaJugador = new JugadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        MensajeLogica _logicaMensaje = new MensajeLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();
        OjearLogica _logicaOjear = new OjearLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();

        bool desplegableActivado = false;

        private static MediaPlayer mediaPlayer = new MediaPlayer(); // Inicialización al declarar

        public frmNegociacionesJugador(Jugador jugador, Manager manager, int equipo, int tipoNegociacion, UC_PantallaPrincipal pantallaPrincipal)
        {
            InitializeComponent();
            _jugador = jugador;
            _manager = manager;
            _equipo = equipo;
            _tipoNegociacion = tipoNegociacion;
            _pantallaPrincipal = pantallaPrincipal;

            // Código que inicializa el sonido de fondo 
            try
            {
                Metodos.ReproducirMusica("backgroundMusic2.wav");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Comprobar si el jugador es de mi equipo
            bool miJugador = _logicaJugador.EsDeMiEquipo(_jugador.IdJugador, _equipo);

            // Comprobar si el jugador ha sido ojeado
            Transferencia jugadorConOferta = _logicaTransferencia.MostrarDetallesOferta(_jugador.IdJugador);

            if (miJugador == true || jugadorConOferta != null)
            {
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

                // -------------------------------------------- CARGAR DEMANDA DEL JUGADOR ----------------------------------------
                var demanda = GenerarDemandaContrato(_jugador);

                txtDemandaSalario.Text = $"{demanda.SalarioDeseado:N0} €";
                txtDemandaClausula.Text = $"{demanda.ClausulaDeseada:N0} €";
                txtDemandaAnios.Text = demanda.DuracionContrato.ToString();
                txtDemandaRol.Text = ObtenerNombreRol(demanda.RolDeseado);

                // Bonuses
                int? bonusGolesActual = _jugador.BonusGoles;

                if (_jugador.RolId >= 7)
                { // Si es un jugador atacante pedirá clausula por goles
                    chkDemandaBonusGoles.IsChecked = true;
                }
                if (bonusGolesActual > 0)
                { // Si el jugador ya tiene clausula por goles también la pedirá
                    chkDemandaBonusGoles.IsChecked = true;
                }

                if (_jugador.Status >= 3)
                { // Si el jugador no es titular pedirá la claúsula
                    chkDemandaBonusPartido.IsChecked = true;
                }
            }
            else
            {
                if (_jugador.IdEquipo == 0)
                {
                    // Cargar datos del Jugador
                    txtDorsal.Text = _jugador.Dorsal.ToString();
                    if (_jugador.NombreCompleto != null)
                    {
                        txtNombreJugador.Text = _jugador.NombreCompleto.ToUpper();
                    }

                    imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + _jugador.IdJugador + ".png"));
                    imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + _jugador.IdEquipo + ".png"));
                    lblAverage.Text = "?";
                    elipseMedia.Stroke = Brushes.DarkGray;

                    txtEquipoJugador.Text = _logicaEquipo.ListarDetallesEquipo(_jugador.IdEquipo).Nombre;
                    txtPosicionJugador.Text = _jugador.Rol;
                    int status = _jugador.Status;
                    txtStatusJugador.Text = "";

                    txtSalarioJugador.Text = "";
                    txtClausulaJugador.Text = "";
                    txtAniosJugador.Text = "";

                    // -------------------------------------------- CARGAR DEMANDA DEL JUGADOR VACIA ----------------------------------------
                    txtDemandaSalario.Text = $"{_logicaJugador.SalarioMedioJugadores(_jugador.IdJugador):N0} €";
                    txtDemandaClausula.Text = $"{_logicaJugador.ClausulaMediaJugadores(_jugador.IdJugador):N0} €";
                    if (_jugador.Edad > 30)
                    {
                        txtDemandaAnios.Text = "1";
                    }
                    else
                    {
                        txtDemandaAnios.Text = "3";
                    }
                    txtDemandaRol.Text = "Rotación";
                    chkDemandaBonusGoles.IsChecked = false;
                    chkDemandaBonusPartido.IsChecked = false;
                }
                else
                {
                    // Cargar datos del Jugador
                    txtDorsal.Text = _jugador.Dorsal.ToString();
                    if (_jugador.NombreCompleto != null)
                    {
                        txtNombreJugador.Text = _jugador.NombreCompleto.ToUpper();
                    }

                    imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + _jugador.IdJugador + ".png"));
                    imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + _jugador.IdEquipo + ".png"));
                    lblAverage.Text = "?";
                    elipseMedia.Stroke = Brushes.DarkGray;

                    txtEquipoJugador.Text = _logicaEquipo.ListarDetallesEquipo(_jugador.IdEquipo).Nombre;
                    txtPosicionJugador.Text = _jugador.Rol;
                    int status = _jugador.Status;
                    txtStatusJugador.Text = "";

                    txtSalarioJugador.Text = "";
                    txtClausulaJugador.Text = "";
                    txtAniosJugador.Text = "";

                    // -------------------------------------------- CARGAR DEMANDA DEL JUGADOR VACIA ----------------------------------------
                    txtDemandaSalario.Text = "";
                    txtDemandaClausula.Text = "";
                    txtDemandaAnios.Text = "";
                    txtDemandaRol.Text = "";
                    chkDemandaBonusGoles.IsChecked = false;
                    chkDemandaBonusPartido.IsChecked = false;
                }   
            }      
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
        }

        // ---------------------------------------------------- Evento CLICK del botón CANCELAR NEGOCIACIÓN
        private void btnCancelarNegociacion_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string titulo = "INFORMACIÓN";
            string mensaje = $"¿Estás seguro de que deseas cancelar las negociaciones con {(_jugador.NombreCompleto?.ToUpper() ?? "este jugador")}?" +
                 " Si lo haces, no podrás volver a negociar con el jugador en las próximas 2 semanas.";


            // Crear y mostrar la ventana emergente (3 = Cancelar Negociaciones)
            frmVentanaRenovarJugador mensajeCancelarNegociaciones = new frmVentanaRenovarJugador(titulo, mensaje, _jugador.IdJugador, 3);

            bool? resultado = mensajeCancelarNegociaciones.ShowDialog(); // Capturar el resultado del diálogo

            if (resultado == true) // Solo ejecutar si se pulsó Aceptar
            {
                Metodos.ReproducirSonidoTransicion();
                this.Close();
            }
        }

        // ---------------------------------------------------- Evento CLICK del botón IGUALAR OFERTA
        private void btnIgualarOferta_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Salario
            string textoDemandaSalario = txtDemandaSalario.Text.Trim();

            if (string.IsNullOrEmpty(textoDemandaSalario))
            {
                txtOfertaSalario.Text = "0 ";
            }
            else
            {
                string textoDemandaSalarioSinSimbolos = System.Text.RegularExpressions.Regex.Replace(textoDemandaSalario, @"[^\d]", "");

                if (int.TryParse(textoDemandaSalarioSinSimbolos, out int demandaSalario))
                {
                    txtOfertaSalario.Text = string.Format(new CultureInfo("es-ES"), "{0:N0}", demandaSalario);
                }
                else
                {
                    txtOfertaSalario.Text = "0";
                }
            }

            // Claúsula de Rescisión
            string textoDemandaClausula = txtDemandaClausula.Text.Trim();

            if (string.IsNullOrEmpty(textoDemandaClausula))
            {
                txtOfertaClausula.Text = "0";
            }
            else
            {
                string textoDemandaClausulaSinSimbolos = System.Text.RegularExpressions.Regex.Replace(textoDemandaClausula, @"[^\d]", "");

                if (int.TryParse(textoDemandaClausulaSinSimbolos, out int demandaClausula))
                {
                    txtOfertaClausula.Text = string.Format(new CultureInfo("es-ES"), "{0:N0}", demandaClausula);
                }
                else
                {
                    txtOfertaClausula.Text = "0";
                }
            }


            // Años Contrato
            string textoAnios = txtDemandaAnios.Text.Trim();

            if (string.IsNullOrEmpty(textoAnios))
            {
                txtOfertaAnios.Text = "1";
            }
            else
            {
                string textoSoloNumeros = System.Text.RegularExpressions.Regex.Replace(textoAnios, @"[^\d]", "");

                if (int.TryParse(textoSoloNumeros, out int demandaAnios))
                {
                    txtOfertaAnios.Text = demandaAnios.ToString();
                }
                else
                {
                    txtOfertaAnios.Text = "1";
                }
            }

            txtOfertaRol.Text = string.IsNullOrEmpty(txtDemandaRol.Text) ? "Ocasional" : txtDemandaRol.Text;

            int? salario = _jugador.SalarioTemporada;

            if (chkDemandaBonusPartido.IsChecked == true)
            {
                txtOfertaBonusPartidos.Text = string.Format(new CultureInfo("es-ES"), "{0:N0}", (salario / 50));
            }

            if (chkDemandaBonusGoles.IsChecked == true)
            {
                txtOfertaBonusGoles.Text = string.Format(new CultureInfo("es-ES"), "{0:N0}", (salario / 35));
            }
        }

        // ---------------------------------------------------- Evento CLICK del botón ENVIAR OFERTA
        private void btnEnviarOferta_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Valores demandados por el jugador.
            string textoDemandaSalario = txtDemandaSalario.Text; // Texto original con puntos y símbolo €
            string textoDemandaSalarioSinSimbolos = System.Text.RegularExpressions.Regex.Replace(textoDemandaSalario, @"[^\d]", ""); // Elimina todo lo que no sea un dígito
            int demandaSalario = int.Parse(textoDemandaSalarioSinSimbolos); // Convierte el texto limpio a int

            string textoDemandaClausula = txtDemandaClausula.Text; // Texto original con puntos y símbolo €
            string textoDemandaClausulaSinSimbolos = System.Text.RegularExpressions.Regex.Replace(textoDemandaClausula, @"[^\d]", ""); // Elimina todo lo que no sea un dígito
            int demandaClausula = int.Parse(textoDemandaClausulaSinSimbolos); // Convierte el texto limpio a int

            string textoAnios = txtDemandaAnios.Text; // Texto original con años
            string textoSoloNumeros = System.Text.RegularExpressions.Regex.Replace(textoAnios, @"[^\d]", ""); // Elimina todo lo que no sea un dígito
            int demandaAnios = int.Parse(textoSoloNumeros); // Convierte el texto limpio a int

            string demandaRol = txtDemandaRol.Text;
            int? demandaBonusPartidos = chkDemandaBonusPartido.IsChecked == true ? ((_jugador.SalarioTemporada / 38) * 13) : 0;
            int? demandaBonusGoles = chkDemandaBonusGoles.IsChecked == true ? (_jugador.SalarioTemporada / 50) : 0;

            // Valores ofrecidos por el equipo.
            int ofertaSalario = int.Parse(txtOfertaSalario.Text.Replace(".", ""));
            int ofertaClausula = int.Parse(txtOfertaClausula.Text.Replace(".", ""));
            int ofertaAnios = int.Parse(txtOfertaAnios.Text);
            string ofertaRol = txtOfertaRol.Text;
            int ofertaBonusPartidos = int.Parse(txtOfertaBonusPartidos.Text.Replace(".", ""));
            int ofertaBonusGoles = int.Parse(txtOfertaBonusGoles.Text.Replace(".", ""));

            int satisfaccionTotal = CalcularPorcentajeSatisfaccion(demandaSalario, demandaClausula, demandaAnios, demandaRol, demandaBonusPartidos, demandaBonusGoles,
                                                                   ofertaSalario, ofertaClausula, ofertaAnios, ofertaRol, ofertaBonusPartidos, ofertaBonusGoles);

            // Mostrar en la barra de progreso del Estado de la Negociación
            barraProgreso.Value = satisfaccionTotal;
            txtPorcentajeNegociacion.Text = (int)satisfaccionTotal + "%";
            if (satisfaccionTotal == 100)
            {
                txtEstadoNegociacion.Text = "El jugador acepta la oferta realizada.";
                barraProgreso.Foreground = new SolidColorBrush(Colors.DarkGreen);

                barraPaciencia.Value = barraPaciencia.Value - 0;
                ColorBarraPaciencia((int)barraPaciencia.Value);
            }
            else if (satisfaccionTotal >= 85)
            {
                txtEstadoNegociacion.Text = "El jugador está muy contento con la oferta.";
                barraProgreso.Foreground = new SolidColorBrush(Colors.LightGreen);

                barraPaciencia.Value = barraPaciencia.Value - 10;
                ColorBarraPaciencia((int)barraPaciencia.Value);
            }
            else if (satisfaccionTotal >= 60)
            {
                txtEstadoNegociacion.Text = "El jugador está receptivo, pero espera algo más.";
                barraProgreso.Foreground = new SolidColorBrush(Colors.DarkOrange);

                barraPaciencia.Value = barraPaciencia.Value - 15;
                ColorBarraPaciencia((int)barraPaciencia.Value);
            }
            else if (satisfaccionTotal >= 50)
            {
                txtEstadoNegociacion.Text = "El jugador considera alejada de sus pretensiones.";
                barraProgreso.Foreground = new SolidColorBrush(Colors.Orange);

                barraPaciencia.Value = barraPaciencia.Value - 25;
                ColorBarraPaciencia((int)barraPaciencia.Value);
            }
            else
            {
                txtEstadoNegociacion.Text = "El jugador considera la oferta ridícula.";
                barraProgreso.Foreground = new SolidColorBrush(Colors.DarkRed);

                barraPaciencia.Value = barraPaciencia.Value - 50;
                ColorBarraPaciencia((int)barraPaciencia.Value);
            }
            txtPorcentajePaciencia.Text = barraPaciencia.Value.ToString() + "%";

            // Comprobar si el estado de la negociación ha llegado a 100% de interés del jugador.
            if (satisfaccionTotal == 100)
            {
                btnEnviarOferta.Visibility = Visibility.Collapsed;
                btnConfirmarOferta.Visibility = Visibility.Visible;
                btnIgualarOferta.IsEnabled = false;
                txtOfertaSalario.IsEnabled = false;
                txtOfertaClausula.IsEnabled = false;
                txtOfertaAnios.IsEnabled = false;
                txtOfertaRol.IsEnabled = false;
                gridRol.IsEnabled = false;
                txtOfertaBonusPartidos.IsEnabled = false;
                txtOfertaBonusGoles.IsEnabled = false;

                // Ventana emergente diciendo que el jugador ya no quiere negociar.
                string titulo = "INFORMACIÓN";
                string nombreJugador = _jugador?.NombreCompleto ?? "El jugador";
                string mensaje = $"{nombreJugador.ToUpper()} y el {_logicaEquipo.ListarDetallesEquipo(_equipo).Nombre.ToUpper()} han llegado a un acuerdo.";

                // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                frmVentanaEmergenteDosBotones mensajeContratoAceptado = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                mensajeContratoAceptado.ShowDialog();
                
                // Si es un jugador sin equipo registramos la oferta aceptada
                if (_jugador.IdEquipo == 0)
                {
                    bool enNegociacion = _logicaTransferencia.ComprobarOfertaActiva(_jugador.IdJugador, _equipo, _jugador.IdEquipo);

                    // Verificar disponibilidad para la cesion.
                    DateTime hoy = Metodos.hoy;

                    // Ventanas de fichajes
                    DateTime inicioVerano = new DateTime(hoy.Year, 7, 1);
                    DateTime finVerano = new DateTime(hoy.Year, 8, 30);
                    DateTime inicioInvierno = new DateTime(hoy.Year, 1, 1);
                    DateTime finInvierno = new DateTime(hoy.Year, 1, 31);

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
                        IdJugador = _jugador.IdJugador,
                        IdEquipoOrigen = _jugador.IdEquipo,
                        IdEquipoDestino = _equipo,
                        TipoFichaje = 1,
                        FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                        FechaTraspaso = fechaTraspaso.ToString("yyyy-MM-dd"),
                        RespuestaEquipo = 1,
                        RespuestaJugador = 1,
                        MontoOferta = 0,
                        SalarioAnual = ofertaSalario,
                        ClausulaRescision = ofertaClausula,
                        Duracion = ofertaAnios,
                        BonoPorGoles = ofertaBonusGoles,
                        BonoPorPartidos = ofertaBonusPartidos,
                    };

                    if (enNegociacion != true)
                    {
                        _logicaTransferencia.RegistrarOferta(oferta);
                    }
                    else
                    {
                        _logicaTransferencia.ActualizarOferta(oferta);
                    }
                }
            }

            // Comprobar si la paciencia del jugador ha llegado a cero y no dejarle hacer mas ofertas.
            if (barraPaciencia.Value == 0)
            {
                btnEnviarOferta.IsEnabled = false;
                btnIgualarOferta.IsEnabled = false;
                txtOfertaSalario.IsEnabled = false;
                txtOfertaClausula.IsEnabled = false;
                txtOfertaAnios.IsEnabled = false;
                txtOfertaRol.IsEnabled = false;
                gridRol.IsEnabled = false;
                txtOfertaBonusPartidos.IsEnabled = false;
                txtOfertaBonusGoles.IsEnabled = false;

                // Ventana emergente diciendo que el jugador ya no quiere negociar.
                string titulo = "INFORMACIÓN";
                string nombreJugador = _jugador?.NombreCompleto ?? "El jugador";
                string mensaje = nombreJugador.ToUpper() + " se ha cansado de negociar contigo y ha decidido no volver a reunirse contigo durante las próximas 2 semanas.";

                // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                frmVentanaEmergenteDosBotones mensajeRenovacion = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                mensajeRenovacion.ShowDialog();

                // Si es un jugador sin equipo registramos la oferta rechazada
                if (_jugador.IdEquipo == 0)
                {
                    bool enNegociacion = _logicaTransferencia.ComprobarOfertaActiva(_jugador.IdJugador, _equipo, _jugador.IdEquipo);

                    Transferencia oferta = new Transferencia
                    {
                        IdJugador = _jugador.IdJugador,
                        IdEquipoOrigen = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).IdEquipo,
                        IdEquipoDestino = _equipo,
                        TipoFichaje = 1,
                        MontoOferta = 0,
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
                        _logicaTransferencia.RegistrarOferta(oferta);
                    }
                    else
                    {
                        _logicaTransferencia.ActualizarOferta(oferta);
                    }
                }

                // Cancelar las negociaciones en la base de datos.
                _logicaJugador.NegociacionCancelada(_jugador.IdJugador, 14);

                // Crear el mensaje confirmando que el jugador rechaza la oferta de traspaso
                Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

                Mensaje mensajeJugadorRechazaOferta = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                    Asunto = "Negociación fallida",
                    Contenido = $"Lamentablemente, no ha sido posible llegar a un acuerdo con {nombreJugador.ToUpper()} tras la negociación de los términos contractuales.\n\nEl jugador y su representante han rechazado nuestra propuesta y han decidido no continuar con las conversaciones. Esto significa que, por el momento, el fichaje queda descartado.\n\nPuedes optar por realizar una nueva oferta en 2 semanas para modificar las condiciones o centrarte en otras alternativas disponibles en el mercado.",
                    TipoMensaje = "Notificación",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = _jugador.IdJugador
                };

                _logicaMensaje.crearMensaje(mensajeJugadorRechazaOferta);

                Metodos.ReproducirSonidoTransicion();
                this.Close();
            }
        }

        // ---------------------------------------------------- Evento CLICK del botón CONFIRMAR OFERTA
        private void btnConfirmarOferta_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Actualizar el contrato del jugador.
            int ofertaSalario = int.Parse(txtOfertaSalario.Text.Replace(".", ""));
            int ofertaClausula = int.Parse(txtOfertaClausula.Text.Replace(".", ""));
            int ofertaAnios = int.Parse(txtOfertaAnios.Text);
            int ofertaRol = 0;
            if (txtOfertaRol.Text == "Clave")
            {
                ofertaRol = 1;
            }
            else if (txtOfertaRol.Text == "Importante")
            {
                ofertaRol = 2;
            }
            else if (txtOfertaRol.Text == "Rotación")
            {
                ofertaRol = 3;
            }
            else
            {
                ofertaRol = 4;
            }
            int ofertaBonusPartidos = int.Parse(txtOfertaBonusPartidos.Text.Replace(".", ""));
            int ofertaBonusGoles = int.Parse(txtOfertaBonusGoles.Text.Replace(".", ""));

            if (_tipoNegociacion == 1)
            {
                _logicaJugador.RenovarStatusJugador(_jugador.IdJugador, ofertaRol);
                _logicaJugador.RenovarContratoJugador(_jugador.IdJugador, ofertaSalario, ofertaClausula, ofertaAnios, ofertaBonusPartidos, ofertaBonusGoles);
                // Cancelar las negociaciones en la base de datos durante 6 meses.
                _logicaJugador.NegociacionCancelada(_jugador.IdJugador, 180);

                // Crear el mensaje con la Renovación de Jugador
                string nombreJugador = _jugador.Nombre + " " + _jugador.Apellido;
                string nombreManager = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " +
                                       _logicaManager.MostrarManager(_manager.IdManager).Apellido;
                Mensaje mensajeRenovacion = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : "Desconocido",
                    Asunto = "Renovación de Jugador",
                    Contenido = $"Has renovado a {_jugador.NombreCompleto.ToUpper()}. El nuevo contrato tiene una duración de {ofertaAnios} años, un salario de {txtOfertaSalario.Text} € y su nueva claúsula de rescisión es de {txtOfertaClausula.Text} €.",
                    TipoMensaje = "Respuesta del Jugador",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = _jugador.IdJugador // Distinto de 0 es icono de jugador
                };
                _logicaMensaje.crearMensaje(mensajeRenovacion);
            }
            else
            {
                bool enNegociacion = _logicaTransferencia.ComprobarOfertaActiva(_jugador.IdJugador, _equipo, _jugador.IdEquipo);

                // Verificar disponibilidad para la cesion.
                DateTime hoy = Metodos.hoy;

                // Ventanas de fichajes
                DateTime inicioVerano = new DateTime(hoy.Year, 7, 1);
                DateTime finVerano = new DateTime(hoy.Year, 8, 30);
                DateTime inicioInvierno = new DateTime(hoy.Year, 1, 1);
                DateTime finInvierno = new DateTime(hoy.Year, 1, 31);

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
                    IdJugador = _jugador.IdJugador,
                    IdEquipoOrigen = _jugador.IdEquipo,
                    IdEquipoDestino = _equipo,
                    TipoFichaje = 1,
                    FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                    FechaTraspaso = fechaTraspaso.ToString("yyyy-MM-dd"),
                    RespuestaEquipo = 1,
                    RespuestaJugador = 1,
                    MontoOferta = _logicaTransferencia.MostrarDetallesOferta(_jugador.IdJugador).MontoOferta,
                    SalarioAnual = ofertaSalario,
                    ClausulaRescision = ofertaClausula,
                    Duracion = ofertaAnios,
                    BonoPorGoles = ofertaBonusGoles,
                    BonoPorPartidos = ofertaBonusPartidos,
                };

                if (enNegociacion != true)
                {
                    _logicaTransferencia.RegistrarOferta(oferta);
                }
                else
                {
                    _logicaTransferencia.ActualizarOferta(oferta);
                }

                // Agregar el traspaso a la tabla transferencias
                _logicaTransferencia.RegistrarTransferencia(oferta);

                // Cancelar las negociaciones en la base de datos durante 6 meses.
                _logicaJugador.NegociacionCancelada(_jugador.IdJugador, 180);

                // Crear el mensaje confirmando que el jugador acepta la oferta de traspaso
                Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                string nombreJugador = _logicaJugador.MostrarDatosJugador(_jugador.IdJugador).NombreCompleto;

                Mensaje mensajeJugadorAceptaOferta = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                    Asunto = "Traspaso confirmado",
                    Contenido = $"{nombreJugador.ToUpper()} ha aceptado los términos personales que le propusimos " +
                                $"y se ha convertido oficialmente en nuevo jugador del {_logicaEquipo.ListarDetallesEquipo(_jugador.IdEquipo).Nombre.ToUpper().ToUpper()}.\n\n" +
                                "Los detalles finales del acuerdo son los siguientes:\n\n" +
                                $"• Tipo de operación: Traspaso\n" +
                                $"• Coste del fichaje: {_logicaTransferencia.MostrarDetallesOferta(_jugador.IdJugador).MontoOferta.ToString("N0", new CultureInfo("es-ES"))}€\n" +
                                $"• Duración del contrato: {ofertaAnios} temporadas\n" +
                                $"• Salario acordado: {ofertaSalario.ToString("N0", new CultureInfo("es-ES"))}€\n" +
                                $"• Rol en la plantilla: {txtOfertaRol.Text}\n" +
                                $"• Bonus por partidos: {ofertaBonusPartidos.ToString("N0", new CultureInfo("es-ES"))}€\n" +
                                $"• Bonus por goles: {ofertaBonusGoles.ToString("N0", new CultureInfo("es-ES"))}€\n\n" +
                                "El jugador se incorporará de inmediato a los entrenamientos y estará disponible para el próximo partido, salvo indicaciones médicas.",
                    TipoMensaje = "Notificación",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = _jugador.IdJugador
                };

                _logicaMensaje.crearMensaje(mensajeJugadorAceptaOferta);

                // Crear el gasto del fichaje
                int cantidadFichaje = _logicaTransferencia.MostrarDetallesOferta(_jugador.IdJugador).MontoOferta;
                Finanza nuevoGasto = new Finanza
                {
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Temporada = Metodos.temporadaActual.ToString(),
                    IdConcepto = 18,
                    Tipo = 2,
                    Cantidad = cantidadFichaje,
                    Fecha = Metodos.hoy.Date
                };
                _logicaFinanza.CrearGasto(nuevoGasto);

                // Restar la indemnización al Presupuesto
                _logicaEquipo.RestarCantidadAPresupuesto(_equipo, cantidadFichaje);
            }

            // Llamar al método ActualizarPresupuesto() de UC_PantallaPrincipal
            _pantallaPrincipal.ActualizarPresupuesto();

            Metodos.ReproducirSonidoTransicion();
            this.Close();
        }

        // ---------------------------------------------------- Eventos de las Cajas de Texto
        // SALARIO
        private void txtOfertaSalario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtOfertaSalario_GotFocus(object sender, RoutedEventArgs e)
        {
            // Quita los puntos cuando el usuario entra en la caja de texto
            if (txtOfertaSalario.Text.Contains("."))
            {
                txtOfertaSalario.Text = txtOfertaSalario.Text.Replace(".", "");
            }
        }

        private void txtOfertaSalario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOfertaSalario.Text, out int valor))
            {
                // Formatea con puntos de miles al salir
                txtOfertaSalario.Text = valor.ToString("N0", new CultureInfo("es-ES"));
            }
        }

        // CLAÚSULA DE RESCISIÓN
        private void txtOfertaClausula_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtOfertaClausula_GotFocus(object sender, RoutedEventArgs e)
        {
            // Quita los puntos cuando el usuario entra en la caja de texto
            if (txtOfertaClausula.Text.Contains("."))
            {
                txtOfertaClausula.Text = txtOfertaClausula.Text.Replace(".", "");
            }
        }

        private void txtOfertaClausula_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOfertaClausula.Text, out int valor))
            {
                // Formatea con puntos de miles al salir
                txtOfertaClausula.Text = valor.ToString("N0", new CultureInfo("es-ES"));
            }
        }

        // AÑOS DE CONTRATO
        private void txtOfertaAnios_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[1-5]$");
        }

        //ROL
        private void gridRol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (desplegableActivado == false)
            {
                borderRolDesplegable.Visibility = Visibility.Visible;
                desplegableActivado = true;
            }
            else
            {
                borderRolDesplegable.Visibility = Visibility.Hidden;
                desplegableActivado = false;
            }
        }

        private void txtClave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borderRolDesplegable.Visibility = Visibility.Hidden;
            desplegableActivado = false;
            txtOfertaRol.Text = "Clave";
        }

        private void txtImportante_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borderRolDesplegable.Visibility = Visibility.Hidden;
            desplegableActivado = false;
            txtOfertaRol.Text = "Importante";
        }

        private void txtRotacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borderRolDesplegable.Visibility = Visibility.Hidden;
            desplegableActivado = false;
            txtOfertaRol.Text = "Rotación";
        }

        private void txtOcasional_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borderRolDesplegable.Visibility = Visibility.Hidden;
            desplegableActivado = false;
            txtOfertaRol.Text = "Ocasional";
        }

        // BONUS PARTIDOS
        private void txtOfertaBonusPartidos_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtOfertaBonusPartidos_GotFocus(object sender, RoutedEventArgs e)
        {
            // Quita los puntos cuando el usuario entra en la caja de texto
            if (txtOfertaBonusPartidos.Text.Contains("."))
            {
                txtOfertaBonusPartidos.Text = txtOfertaBonusPartidos.Text.Replace(".", "");
            }
        }

        private void txtOfertaBonusPartidos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOfertaBonusPartidos.Text, out int valor))
            {
                // Formatea con puntos de miles al salir
                txtOfertaBonusPartidos.Text = valor.ToString("N0", new CultureInfo("es-ES"));
            }
        }

        // BONUS POR GOL
        private void txtOfertaBonusGoles_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtOfertaBonusGoles_GotFocus(object sender, RoutedEventArgs e)
        {
            // Quita los puntos cuando el usuario entra en la caja de texto
            if (txtOfertaBonusGoles.Text.Contains("."))
            {
                txtOfertaBonusGoles.Text = txtOfertaBonusGoles.Text.Replace(".", "");
            }
        }

        private void txtOfertaBonusGoles_LostFocus(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOfertaBonusGoles.Text, out int valor))
            {
                // Formatea con puntos de miles al salir
                txtOfertaBonusGoles.Text = valor.ToString("N0", new CultureInfo("es-ES"));
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

        private int CalcularPorcentajeSatisfaccion(int demandaSalario, int demandaClausula, int demandaAnios, string demandaRol, int? demandaBonusPartidos, int? demandaBonusGoles,
                                                   int ofertaSalario, int ofertaClausula, int ofertaAnios, string ofertaRol, int ofertaBonusPartidos, int ofertaBonusGoles)
        {
            int porcentajeSatisfaccion = 0;

            Dictionary<string, int> nivelesRol = new Dictionary<string, int>
                                                 {
                                                    { "Clave", 3 },
                                                    { "Importante", 2 },
                                                    { "Rotación", 1 },
                                                    { "Ocasional", 0 }
                                                 };

            // Obtener el nivel de los roles demandado y ofertado
            int nivelDemanda = nivelesRol[demandaRol];
            int nivelOferta = nivelesRol[ofertaRol];

            // Calcular la diferencia de niveles
            int diferenciaRol = nivelOferta - nivelDemanda;

            // Comparar el salario (35%)
            double satisfaccionSalario = ((ofertaSalario * 40) / demandaSalario) + diferenciaRol;
            porcentajeSatisfaccion += (int)satisfaccionSalario;

            // Comparar la cláusula de rescisión (20%)
            double porcentajeOferta = ((double)ofertaClausula / demandaClausula) * 100;

            // Diccionario con los umbrales de satisfacción según el rol
            var satisfaccionPorRolClausula = new Dictionary<string, List<(int umbral, int satisfaccion)>>
                                    {
                                        { "Clave",       new List<(int, int)> { (75, 20), (55, 10), (35, 5), (0, 0) } },
                                        { "Importante",  new List<(int, int)> { (80, 20), (65, 10), (50, 5), (0, 0) } },
                                        { "Rotación",    new List<(int, int)> { (90, 20), (70, 10), (50, 5), (0, 0) } },
                                        { "Ocasional",   new List<(int, int)> { (95, 20), (75, 10), (55, 5), (0, 0) } }
                                    };

            // Buscar el valor de satisfacción correspondiente
            if (satisfaccionPorRolClausula.TryGetValue(demandaRol, out var reglas))
            {
                foreach (var (umbral, sat) in reglas)
                {
                    if (porcentajeOferta >= umbral)
                    {
                        porcentajeSatisfaccion += sat;
                        break;
                    }
                }
            }

            // Comparar los años de contrato (10%)
            int diferencia = ofertaAnios - demandaAnios;

            if (diferencia == 0)
            {
                porcentajeSatisfaccion += 15;
            }
            else if ((demandaRol == "Clave" && diferencia <= 2) ||
                     (demandaRol == "Importante" && diferencia <= 1) ||
                     (demandaRol == "Rotación" && diferencia >= -1) ||
                     (demandaRol == "Ocasional" && diferencia >= -2))
            {
                porcentajeSatisfaccion += 12; // Dentro del rango ideal
            }
            else if ((demandaRol == "Clave" && diferencia <= 3) ||
                       (demandaRol == "Importante" && diferencia <= 2) ||
                       (demandaRol == "Rotación" && diferencia >= -2) ||
                       (demandaRol == "Ocasional" && diferencia >= -3))
            {
                porcentajeSatisfaccion += 5;
            }
            else
            {
                porcentajeSatisfaccion += 0;
            }

            // Comparar el rol (24%)
            int diferenciaPorRol = nivelDemanda - nivelOferta; // Calcular la diferencia de niveles

            var satisfaccionPorRol = new Dictionary<string, List<(int diferenciaPorRol, int satisfaccion)>>
                                    {
                                        { "Clave",       new List<(int, int)> { (0, 24), (1, 20), (int.MaxValue, 10) } },
                                        { "Importante",  new List<(int, int)> { (0, 24), (1, 20), (int.MaxValue, 10) } },
                                        { "Rotación",    new List<(int, int)> { (-2, 30), (0, 24), (int.MaxValue, 10) } },
                                        { "Ocasional",   new List<(int, int)> { (-3, 30), (-2, 30), (-1, 24), (int.MaxValue, 24) } }
                                    };

            // Buscar el valor de satisfacción correspondiente
            if (satisfaccionPorRol.TryGetValue(demandaRol, out var rules))
            {
                foreach (var (dif, sat) in rules)
                {
                    if (diferencia <= dif)
                    {
                        porcentajeSatisfaccion += sat;
                        break;
                    }
                }
            }

            // Comparar bonus por partidos (5%)
            int? diffBonusPartidos = demandaBonusPartidos - ofertaBonusPartidos;

            if (diffBonusPartidos <= 0)
            {
                porcentajeSatisfaccion += 5;
            }
            else
            {
                if (diffBonusPartidos > 0 && diffBonusPartidos < (demandaBonusPartidos * 0.25))
                {
                    porcentajeSatisfaccion += 3;
                }
                else
                {
                    porcentajeSatisfaccion += 0;
                }
            }

            // Comparar bonus por goles (5%)
            int? diffBonusGoles = demandaBonusGoles - ofertaBonusGoles;

            if (diffBonusGoles <= 0)
            {
                porcentajeSatisfaccion += 5;
            }
            else
            {
                if (diffBonusGoles > 0 && diffBonusGoles < (demandaBonusGoles * 0.25))
                {
                    porcentajeSatisfaccion += 3;
                }
                else
                {
                    porcentajeSatisfaccion += 0;
                }
            }

            // Evitamos valores por debajo de 0 o de 100.
            if (porcentajeSatisfaccion < 0)
            {
                porcentajeSatisfaccion = 0;
            }
            else if (porcentajeSatisfaccion > 100)
            {
                porcentajeSatisfaccion = 100;
            }

            return (int)porcentajeSatisfaccion;
        }

        private void ColorBarraPaciencia(int valor)
        {
            if (valor == 100)
            {
                barraPaciencia.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else if (valor >= 80)
            {
                barraPaciencia.Foreground = new SolidColorBrush(Colors.LightGreen);
            }
            else if (valor >= 65)
            {
                barraPaciencia.Foreground = new SolidColorBrush(Colors.DarkOrange);
            }
            else if (valor >= 50)
            {
                barraPaciencia.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                barraPaciencia.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
        }

        public DemandaContrato GenerarDemandaContrato(Jugador jugador)
        {
            var demanda = new DemandaContrato();

            // ----------------------------------------------------------------------- SALARIO
            int salarioActual = jugador.SalarioTemporada ?? 1000000;
            double porcentajeAjuste;

            // Ajuste según edad: los jóvenes piden más, los veteranos menos
            if (jugador.Edad <= 22)
                porcentajeAjuste = 1.05; // Pide un 5% más
            else if (jugador.Edad <= 27)
                porcentajeAjuste = 1.15; // Pide un 15% más
            else if (jugador.Edad <= 31)
                porcentajeAjuste = 1.05; // Pide un 5% más
            else if (jugador.Edad <= 34)
                porcentajeAjuste = 0.90; // Acepta un 10% menos
            else
                porcentajeAjuste = 0.75; // Acepta un 25% menos

            // Ajuste adicional por media
            if (jugador.Media >= 90)
                porcentajeAjuste += 0.2;
            else if (jugador.Media >= 85)
                porcentajeAjuste += 0.1;
            else if (jugador.Media >= 80)
                porcentajeAjuste += 0.05;

            demanda.SalarioDeseado = (int)(salarioActual * porcentajeAjuste);

            // ----------------------------------------------------------------------- CLAUSULA DE RESCISION
            // FACTORES
            double factorEdad;
            if (jugador.Edad <= 21)
                factorEdad = 1.1;
            else if (jugador.Edad <= 25)
                factorEdad = 1.2;
            else if (jugador.Edad <= 30)
                factorEdad = 1.0;
            else if (jugador.Edad <= 34)
                factorEdad = 0.7;
            else
                factorEdad = 0.5;

            double factorRol;
            switch (jugador.Status)
            {
                case 1: // Clave
                    factorRol = 1.5;
                    break;
                case 2: // Importante
                    factorRol = 1.2;
                    break;
                case 3: // Rotación
                    factorRol = 1.0;
                    break;
                case 4: // Ocasional
                    factorRol = 0.8;
                    break;
                default:
                    factorRol = 1.0;
                    break;
            }

            // CÁLCULO FINAL
            int clausulaMaxima = 1_000_000_000;
            int clausulaAnterior = jugador.ClausulaRescision ?? 1000000;
            int clausulaBase = (int)(clausulaAnterior * factorEdad * factorRol);

            // Aplica el límite máximo
            demanda.ClausulaDeseada = Math.Min(clausulaBase, clausulaMaxima);

            // ----------------------------------------------------------------------- DURACION DEL CONTRATO
            if (jugador.Edad < 24)
                demanda.DuracionContrato = 5;
            else if (jugador.Edad < 28)
                demanda.DuracionContrato = 4;
            else if (jugador.Edad < 32)
                demanda.DuracionContrato = 3;
            else
                demanda.DuracionContrato = 2;

            // ----------------------------------------------------------------------- ROL DESEADO
            int valor = jugador.Media;
            if (valor >= 88)
                demanda.RolDeseado = 1; // Clave
            else if (valor >= 80)
                demanda.RolDeseado = 2; // Importante
            else if (valor >= 70)
                demanda.RolDeseado = 3; // Rotación
            else
                demanda.RolDeseado = 4; // Ocasional

            return demanda;
        }


        private string ObtenerNombreRol(int rolId)
        {
            return rolId switch
            {
                1 => "Clave",
                2 => "Importante",
                3 => "Rotación",
                4 => "Ocasional",
                _ => "Desconocido"
            };
        }
        #endregion
    }
}
