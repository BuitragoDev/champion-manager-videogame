using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Transferencias_OfertasRecibidas : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        OjearLogica _logicaOjear = new OjearLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        public UC_Menu_Transferencias_OfertasRecibidas(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarJugadoresOfertados();
        }

        #region "Metodos"
        private void CargarJugadoresOfertados()
        {
            List<Transferencia> listaOfertas = _logicaTransferencia.ListarOfertasRecibidas();
            WrapPanel wrapPanel = new WrapPanel();

            foreach (var jugador in listaOfertas)
            {
                Jugador oJugador = _logicaJugador.MostrarDatosJugador(jugador.IdJugador);
                Equipo oEquipo = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipoDestino);
                Border border = new Border
                {
                    Background = Brushes.LightGray,
                    Width = 1790,
                    Margin = new Thickness(0, 4, 0, 4),
                    Child = new Grid(),
                    Effect = new DropShadowEffect
                    {
                        Color = Colors.Black, // Color de la sombra
                        Direction = 320, // Dirección de la sombra (ángulo en grados)
                        ShadowDepth = 5, // Distancia de la sombra
                        BlurRadius = 10, // Radio de difuminado de la sombra
                        Opacity = 0.5 // Opacidad de la sombra
                    }
                };

                Grid grid = (Grid)border.Child;
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });

                // Añadir el nombre del jugador
                TextBlock nombreJugador = new TextBlock
                {
                    Text = $"{oJugador.Nombre} {oJugador.Apellido}",
                    FontSize = 18,
                    Margin = new Thickness(20, 0, 0, 0), 
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(nombreJugador, 0);
                grid.Children.Add(nombreJugador);

                // Mostrar la demarcacion
                grid.Children.Add(new TextBlock
                {
                    Text = Metodos.ConvertirDemarcacion(oJugador.RolId),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 1);

                // Mostrar la edad
                grid.Children.Add(new TextBlock
                {
                    Text = oJugador.Edad.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 2);

                // Mostrar la media
                grid.Children.Add(new TextBlock
                {
                    Text = oJugador.Media.ToString(),
                    FontSize = 16,
                    Foreground = DeterminarColorMedia(oJugador.Media),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 3);

                // Escudo equipo
                grid.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + oEquipo.RutaImagen64)),
                    Width = 64,
                    Height = 64,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
                Grid.SetColumn(grid.Children[^1], 4);

                // Nombre Equipo
                grid.Children.Add(new TextBlock
                {
                    Text = oEquipo.Nombre,
                    FontSize = 18,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 5);

                // Cantidad ofrecida
                grid.Children.Add(new TextBlock
                {
                    Text = jugador.MontoOferta.ToString("N0") + " €",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 6);

                // Tipo oferta
                grid.Children.Add(new TextBlock
                {
                    Text = jugador.TipoFichaje == 1 ? "TRASPASO" : "CESIÓN",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 7);

                // Fecha oferta
                grid.Children.Add(new TextBlock
                {
                    Text = DateTime.Parse(jugador.FechaOferta).ToString("dd/MM/yyyy"),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 8);

                // Botones
                // StackPanel para los botones ACEPTAR y RECHAZAR
                StackPanel stackBotones = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 5)
                };

                // Botón ACEPTAR
                Button btnAceptar = new Button
                {
                    Content = "ACEPTAR",
                    Background = (Brush)new BrushConverter().ConvertFromString("#1d6a7d"),
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Margin = new Thickness(4),
                    Padding = new Thickness(10, 5, 10, 5),
                    Cursor = Cursors.Hand,
                    BorderThickness = new Thickness(0),
                    FocusVisualStyle = null,
                    Width = 120,
                    Height = 30,
                    Template = (ControlTemplate)Application.Current.FindResource("NoHoverButtonTemplate"),
                    Tag = jugador.IdJugador
                };
                btnAceptar.Click += (sender, e) => AceptarOferta_Click(sender, e, jugador);

                // Botón RECHAZAR
                Button btnRechazar = new Button
                {
                    Content = "RECHAZAR",
                    Background = Brushes.DarkRed,
                    Foreground = Brushes.White,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Margin = new Thickness(4),
                    Padding = new Thickness(10, 5, 10, 5),
                    Cursor = Cursors.Hand,
                    BorderThickness = new Thickness(0),
                    FocusVisualStyle = null,
                    Width = 120,
                    Height = 30,
                    Template = (ControlTemplate)Application.Current.FindResource("NoHoverButtonTemplate"),
                    Tag = jugador.IdJugador
                };
                btnRechazar.Click += (sender, e) => RechazarOferta_Click(sender, e, jugador);

                // Agregar los botones al StackPanel
                stackBotones.Children.Add(btnAceptar);
                stackBotones.Children.Add(btnRechazar);

                // Agregar StackPanel al Grid
                grid.Children.Add(stackBotones);
                Grid.SetColumn(stackBotones, 9);


                wrapPanel.Children.Add(border);
            }

            wrapPanelOfertas.Children.Clear();
            wrapPanelOfertas.Children.Add(wrapPanel);
        }

        // ----------------------------------------------------------------------- Evento para el boton aceptar
        private void AceptarOferta_Click(object sender, RoutedEventArgs e, Transferencia jugador)
        {
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

            string titulo = "INFORMACIÓN";
            string mensaje = "¿Estás seguro de que quieres aceptar la oferta de " + jugador.MontoOferta.ToString("N0", new CultureInfo("es-ES")) + "€ por " + _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto;

            // Crear y mostrar la ventana emergente
            frmVentanaEmergenteAceptarOferta mensajeAceptarOferta = new frmVentanaEmergenteAceptarOferta(titulo, mensaje);

            bool? resultado = mensajeAceptarOferta.ShowDialog(); // Capturar el resultado del diálogo

            if (resultado == true) // Solo ejecutar si se pulsó Aceptar
            {
                if (jugador.TipoFichaje == 1)
                {
                    // Simular si el jugador acepta o rechaza la oferta de traspaso
                    var rnd = new Random();
                    if (rnd.Next(0, 100) >= 25)
                    {
                        // Simular oferta de contrato al jugador
                        int valorMercadoJugador = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).ValorMercado;
                        int salario = valorMercadoJugador / 10;
                        int duracion = rnd.Next(2, 5);
                        int clausula = valorMercadoJugador * 2;

                        // Mostrar mensaje
                        string mensajeJugadorAcepta = $"Tanto nuestro equipo como {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto} hemos aceptado la oferta procedente del {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipoDestino).Nombre}.";
                        frmVentanaEmergenteDosBotones ventanaJugadorAcepta = new frmVentanaEmergenteDosBotones(titulo, mensajeJugadorAcepta, 2);
                        ventanaJugadorAcepta.ShowDialog();

                        // Crear la transferencia
                        Transferencia oferta = new Transferencia
                        {
                            IdJugador = jugador.IdJugador,
                            IdEquipoOrigen = _equipo,
                            IdEquipoDestino = jugador.IdEquipoDestino,
                            TipoFichaje = 1,
                            MontoOferta = jugador.MontoOferta,
                            FechaOferta = Metodos.hoy.ToString("yyyy-MM-dd"),
                            FechaTraspaso = fechaTraspaso.ToString("yyyy-MM-dd"),
                            RespuestaEquipo = 1,
                            RespuestaJugador = 1,
                            SalarioAnual = salario,
                            ClausulaRescision = clausula,
                            Duracion = duracion,
                            BonoPorGoles = 0,
                            BonoPorPartidos = 0,
                        };

                        // Agregar el traspaso a la tabla transferencias
                        _logicaTransferencia.RegistrarTransferencia(oferta);

                        // Crear el mensaje confirmando la aceptacion de un oferta de traspaso
                        Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                        string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                        string nombreJugador = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto;

                        Mensaje mensajeEquipoAceptaOferta = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                            Asunto = "Oferta aceptada",
                            Contenido = $"{nombreJugador.ToUpper()} ha aceptado la oferta de traspaso de {jugador.MontoOferta.ToString("N0", new CultureInfo("es-ES"))}€ procedente del {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipoDestino).Nombre.ToUpper()}. El jugador en su nuevo equipo tendrá un salario de {salario.ToString("N0", new CultureInfo("es-ES"))}€, una claúsula de rescisión de {clausula.ToString("N0", new CultureInfo("es-ES"))}€ y el nuevo contrato tendrá una duración de {duracion} años. El traspaso se hará efectivo el próximo {fechaTraspaso.ToString("dd-MM-yyyy")}.",
                            TipoMensaje = "Notificación",
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Leido = false,
                            Icono = jugador.IdJugador
                        };

                        _logicaMensajes.crearMensaje(mensajeEquipoAceptaOferta);
                    }
                    else
                    {
                        // Mostrar mensaje
                        string mensajeJugadorRechaza = $"Aunque hemos aceptado la oferta por nuestro jugador, {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto} no ha aceptado la oferta del {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipoDestino).Nombre}.";
                        frmVentanaEmergenteDosBotones ventanaJugadorRechaza = new frmVentanaEmergenteDosBotones(titulo, mensajeJugadorRechaza, 2);
                        ventanaJugadorRechaza.ShowDialog();
                    }
                }
                else
                {
                    // Simular si el jugador acepta o rechaza la oferta de cesion
                    var rnd = new Random();
                    if (rnd.Next(0, 100) >= 25)
                    {    
                        // Mostrar mensaje
                        string mensajeJugadorAcepta = $"Tanto nuestro equipo como {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto} han aceptado la petición de cesión por el jugador.";
                        frmVentanaEmergenteDosBotones ventanaJugadorAcepta = new frmVentanaEmergenteDosBotones(titulo, mensajeJugadorAcepta, 2);
                        ventanaJugadorAcepta.ShowDialog();

                        // Crear la transferencia
                        Transferencia oferta = new Transferencia
                        {
                            IdJugador = jugador.IdJugador,
                            IdEquipoOrigen = _equipo,
                            IdEquipoDestino = jugador.IdEquipoDestino,
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

                        // Agregar el traspaso a la tabla transferencias
                        _logicaTransferencia.RegistrarTransferencia(oferta);

                        // Crear el mensaje confirmando la aceptacion de un oferta de traspaso
                        Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");
                        string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
                        string nombreJugador = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto;

                        Mensaje mensajeEquipoAceptaOferta = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = nombreJugador != null ? nombreJugador : director.Nombre,
                            Asunto = "Oferta aceptada",
                            Contenido = $"{nombreJugador.ToUpper()} ha aceptado la oferta de cesión de 1 año procedente del {_logicaEquipo.ListarDetallesEquipo(jugador.IdEquipoDestino).Nombre.ToUpper()}. La cesión se hará efectiva el próximo {fechaTraspaso.ToString("dd-MM-yyyy")}.",
                            TipoMensaje = "Notificación",
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Leido = false,
                            Icono = jugador.IdJugador
                        };

                        _logicaMensajes.crearMensaje(mensajeEquipoAceptaOferta);
                    }
                    else
                    {
                        // Mostrar mensaje
                        string mensajeJugadorRechaza = $"Aunque hemos aceptado la petición de cesión por nuestro jugador, {_logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto} no está dispuesto ha marcharse del equipo cedido.";
                        frmVentanaEmergenteDosBotones ventanaJugadorRechaza = new frmVentanaEmergenteDosBotones(titulo, mensajeJugadorRechaza, 2);
                        ventanaJugadorRechaza.ShowDialog();
                    }
                }

                _logicaTransferencia.BorrarOfertaRecibida(jugador.IdJugador, jugador.IdEquipoDestino);
                CargarJugadoresOfertados();
            }
        }

        private void RechazarOferta_Click(object sender, RoutedEventArgs e, Transferencia jugador)
        {
            string titulo = "INFORMACIÓN";
            string mensaje = "¿Estás seguro de que quieres rechazar la oferta de " + jugador.MontoOferta.ToString("N0", new CultureInfo("es-ES")) + "€ por " + _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto;

            // Crear y mostrar la ventana emergente
            frmVentanaEmergenteAceptarOferta mensajeAceptarOferta = new frmVentanaEmergenteAceptarOferta(titulo, mensaje);

            bool? resultado = mensajeAceptarOferta.ShowDialog(); // Capturar el resultado del diálogo

            if (resultado == true) // Solo ejecutar si se pulsó Aceptar
            {
                _logicaTransferencia.BorrarOfertaRecibida(jugador.IdJugador, jugador.IdEquipoDestino);
                CargarJugadoresOfertados();
            }
        }

        private SolidColorBrush DeterminarColorMedia(int puntos)
        {
            if (puntos > 90)
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
            if (puntos >= 80)
                return Brushes.DarkGreen; // Verde oscuro
            if (puntos >= 65)
                return Brushes.Orange; // Naranja
            return new SolidColorBrush(Colors.Red);
        }
        #endregion
    }
}
