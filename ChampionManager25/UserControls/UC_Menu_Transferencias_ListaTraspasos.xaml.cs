using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
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
    public partial class UC_Menu_Transferencias_ListaTraspasos : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();

        public UC_Menu_Transferencias_ListaTraspasos(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarJugadoresTraspasados();
        }

        #region "Metodos"
        private void CargarJugadoresTraspasados()
        {
            List<Transferencia> listaTraspasos = _logicaTransferencia.ListarTraspasos();
            WrapPanel wrapPanel = new WrapPanel();

            foreach (var traspaso in listaTraspasos)
            {
                Jugador jugador = _logicaJugador.MostrarDatosJugador(traspaso.IdJugador);
                Border border = new Border
                {
                    Background = Brushes.LightGray,
                    Width = 1790,
                    Height = 40,
                    Margin = new Thickness(0, 4, 0, 4),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Child = new Grid()
                };

                Grid grid = (Grid)border.Child;
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });

                // Añadir el nombre del jugador
                TextBlock nombreJugador = new TextBlock
                {
                    Text = $"{jugador.Nombre} {jugador.Apellido}",
                    FontSize = 16,
                    Margin = new Thickness(15, 0, 0, 0),
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(nombreJugador, 0);
                grid.Children.Add(nombreJugador);
                // ------------------------------------------------

                // Añadir imagen con el escudo del equipo vendedor
                var converter = new IdEquipoToEscudoBuscadorConverter();
                var uriVendedor = (Uri)converter.Convert(traspaso.IdEquipoOrigen, typeof(Uri), null, System.Globalization.CultureInfo.CurrentCulture);

                var escudoVendedorImage = new Image
                {
                    Source = new BitmapImage(uriVendedor),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                };
                grid.Children.Add(escudoVendedorImage);
                Grid.SetColumn(escudoVendedorImage, 1);
                // ------------------------------------------------

                // Añadir el nombre del equipo vendedor
                TextBlock equipoVendedor = new TextBlock
                {
                    Text = $"{_logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoOrigen).Nombre}",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(equipoVendedor, 2);
                grid.Children.Add(equipoVendedor);
                // ------------------------------------------------

                // Añadir imagen con el escudo del equipo comprador
                var uriComprador = (Uri)converter.Convert(traspaso.IdEquipoDestino, typeof(Uri), null, System.Globalization.CultureInfo.CurrentCulture);

                var escudoCompradorImage = new Image
                {
                    Source = new BitmapImage(uriComprador),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                };
                grid.Children.Add(escudoCompradorImage);
                Grid.SetColumn(escudoCompradorImage, 3);
                // ------------------------------------------------

                // Añadir el nombre del equipo comprador
                TextBlock equipoComprador = new TextBlock
                {
                    Text = $"{_logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoDestino).Nombre}",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(equipoComprador, 4);
                grid.Children.Add(equipoComprador);
                // ------------------------------------------------

                // Añadir el valor del traspaso
                TextBlock valor = new TextBlock
                {
                    Text = traspaso.MontoOferta != 0
                                ? $"{traspaso.MontoOferta.ToString("N0", new CultureInfo("es-ES"))}€"
                                : "",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(valor, 5);
                grid.Children.Add(valor);
                // ------------------------------------------------

                // Añadir la demarcacion
                PosicionToStringConverter converterPosicion = new PosicionToStringConverter();

                TextBlock demarcacion = new TextBlock
                {
                    Text = (string)converterPosicion.Convert(jugador.RolId, typeof(string), null, CultureInfo.CurrentCulture),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold
                };

                Grid.SetColumn(demarcacion, 6);
                grid.Children.Add(demarcacion);
                // ------------------------------------------------

                // Añadir la edad
                TextBlock edad = new TextBlock
                {
                    Text = $"{jugador.Edad}",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                Grid.SetColumn(edad, 7);
                grid.Children.Add(edad);
                // ------------------------------------------------

                // Añadir la media
                TextBlock media = new TextBlock
                {
                    Text = jugador.Media.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = DeterminarColorMedia(jugador.Media)
                };

                Grid.SetColumn(media, 8);
                grid.Children.Add(media);
                // ------------------------------------------------

                // Añadir tipo de oferta
                TextBlock tipo = new TextBlock
                {
                    Text = traspaso.TipoFichaje == 1
                                ? "TRASPASO"
                                : "CESIÓN",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = Brushes.Black,
                };

                Grid.SetColumn(tipo, 9);
                grid.Children.Add(tipo);
                // ------------------------------------------------

                // Añadir la fecha del traspaso
                grid.Children.Add(new TextBlock
                {
                    Text = DateTime.Parse(traspaso.FechaTraspaso).ToString("dd/MM/yyyy"),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 10);
                // ------------------------------------------------

                wrapPanel.Children.Add(border);
            }

            wrapPanelTraspasos.Children.Clear();
            wrapPanelTraspasos.Children.Add(wrapPanel);
        }

        private UC_PantallaPrincipal FindParentUC_PantallaPrincipal(DependencyObject obj)
        {
            // Buscar recursivamente en la jerarquía de controles hasta encontrar UC_PantallaPrincipal
            while (obj != null)
            {
                if (obj is UC_PantallaPrincipal)
                    return obj as UC_PantallaPrincipal;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null; // No se encontró el UserControl UC_PantallaPrincipal
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
