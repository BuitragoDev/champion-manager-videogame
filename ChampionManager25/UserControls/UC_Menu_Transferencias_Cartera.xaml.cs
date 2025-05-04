using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
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
using System.Globalization;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Transferencias_Cartera : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        OjearLogica _logicaOjear = new OjearLogica();

        public UC_Menu_Transferencias_Cartera(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarJugadoresOjeados();
        }

        #region "Metodos"
        private void CargarJugadoresOjeados()
        {
            List<Jugador> listaJugadores = _logicaOjear.ListadoJugadoresOjeados(_manager.IdManager);
            string rutaStatus = "";
            DateTime fechaHoy = Metodos.hoy;

            WrapPanel wrapPanel = new WrapPanel();

            foreach (var jugador in listaJugadores)
            {
                // Comprobar la ruta del status
                if (jugador.Status == 1)
                {
                    rutaStatus = "pack://application:,,,/Recursos/img/icons/rol1_icon.png";
                }
                else if (jugador.Status == 2)
                {
                    rutaStatus = "pack://application:,,,/Recursos/img/icons/rol2_icon.png";
                }
                else if (jugador.Status == 3)
                {
                    rutaStatus = "pack://application:,,,/Recursos/img/icons/rol3_icon.png";
                }
                else if (jugador.Status == 4)
                {
                    rutaStatus = "pack://application:,,,/Recursos/img/icons/rol4_icon.png";
                }

                Border border = new Border
                {
                    Background = Brushes.LightGray,
                    Width = 1805,
                    Margin = new Thickness(0, 8, 0, 8),
                    BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#FF23282D"),
                    BorderThickness = new Thickness(1),
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
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });

                grid.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + jugador.IdJugador + ".png")),
                    Width = 80,
                    Height = 80,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(5)
                });
                Grid.SetColumn(grid.Children[^1], 0);

                // Añadir el nombre del jugador con evento de clic
                TextBlock nombreJugador = new TextBlock
                {
                    Text = $"{jugador.Nombre} {jugador.Apellido}",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Cursor = Cursors.Hand
                };

                // Asignar evento de clic al nombre
                nombreJugador.MouseLeftButtonDown += (sender, e) =>
                {
                    Metodos.ReproducirSonidoClick();

                    // Obtener el DockPanel padre
                    DockPanel? parentDockPanel = this.Parent as DockPanel;
                    // Buscar el UserControl UC_PantallaPrincipal en la ventana principal
                    UC_PantallaPrincipal pantallaPrincipal = FindParentUC_PantallaPrincipal(this);

                    // Limpiar el contenido actual
                    parentDockPanel.Children.Clear();

                    // Crear el nuevo UserControl y agregarlo al DockPanel (Opcion 4: Busqueda por Filtro)
                    UC_FichaJugador fichaJugador = new UC_FichaJugador(jugador.IdJugador, _equipo, _manager, 6, pantallaPrincipal);
                    parentDockPanel.Children.Add(fichaJugador);

                };

                Grid.SetColumn(nombreJugador, 1);
                grid.Children.Add(nombreJugador);

                grid.Children.Add(new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre,
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 2);

                grid.Children.Add(new TextBlock
                {
                    Text = Metodos.ConvertirDemarcacion(jugador.RolId),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 3);

                grid.Children.Add(new TextBlock
                {
                    Text = jugador.Edad.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 4);

                if (DateTime.TryParse(jugador.FechaInforme, out DateTime fechaInforme))
                {
                    if (fechaInforme <= fechaHoy)
                    {
                        grid.Children.Add(new Image
                        {
                            Source = new BitmapImage(new Uri(rutaStatus)),
                            Width = 32,
                            Height = 32,
                            VerticalAlignment = VerticalAlignment.Center,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(10)
                        });
                        Grid.SetColumn(grid.Children[^1], 5);

                        grid.Children.Add(new TextBlock
                        {
                            Text = jugador.Media.ToString(),
                            FontSize = 16,
                            Foreground = DeterminarColorMedia(jugador.Media),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontFamily = new FontFamily("Cascadia Code SemiBold")
                        });
                        Grid.SetColumn(grid.Children[^1], 6);

                        grid.Children.Add(new TextBlock
                        {
                            Text = jugador.SalarioTemporada.HasValue
                                ? jugador.SalarioTemporada.Value.ToString("N0") + " €"
                                : "Desconocido",
                            FontSize = 16,
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontFamily = new FontFamily("Cascadia Code SemiBold")
                        });
                        Grid.SetColumn(grid.Children[^1], 7);

                        grid.Children.Add(new TextBlock
                        {
                            Text = jugador.ClausulaRescision.HasValue
                                        ? jugador.ClausulaRescision.Value.ToString("N0") + " €"
                                        : "Sin cláusula",
                            FontSize = 16,
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontFamily = new FontFamily("Cascadia Code SemiBold")
                        });
                        Grid.SetColumn(grid.Children[^1], 8);

                        grid.Children.Add(new TextBlock
                        {
                            Text = jugador.AniosContrato.ToString(),
                            FontSize = 16,
                            Foreground = Brushes.Black,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            FontFamily = new FontFamily("Cascadia Code SemiBold")
                        });
                        Grid.SetColumn(grid.Children[^1], 9);
                    }
                    else
                    {
                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                        });
                        Grid.SetColumn(grid.Children[^1], 5);

                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                        });
                        Grid.SetColumn(grid.Children[^1], 6);

                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                        });
                        Grid.SetColumn(grid.Children[^1], 7);

                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                        });
                        Grid.SetColumn(grid.Children[^1], 8);

                        grid.Children.Add(new TextBlock
                        {
                            Text = "",
                        });
                        Grid.SetColumn(grid.Children[^1], 9);
                    }
                }
                
                grid.Children.Add(new TextBlock
                {
                    Text = DateTime.Parse(jugador.FechaInforme).ToString("dd/MM/yyyy"),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 10);

                // Botón de borrar jugador
                Image btnBorrar = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/borrar_icon.png")),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Cursor = Cursors.Hand,
                    Tag = jugador.IdJugador // Guardamos el ID del jugador en la propiedad Tag
                };

                // Asignamos el evento de clic al botón de borrar
                btnBorrar.MouseLeftButtonDown += BtnBorrar_MouseLeftButtonDown;

                Grid.SetColumn(btnBorrar, 11);
                grid.Children.Add(btnBorrar);

                wrapPanel.Children.Add(border);
            }

            wrapPanelCartera.Children.Clear();
            wrapPanelCartera.Children.Add(wrapPanel);
        }

        // Evento para borrar un jugador al hacer clic en el botón
        private void BtnBorrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Image btn = sender as Image;
            if (btn != null && btn.Tag is int idJugador)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = $"¿Estás seguro de que quieres quitar de la Cartera a {_logicaJugador.MostrarDatosJugador(idJugador).NombreCompleto.ToUpper()}?";

                // Crear y mostrar la ventana emergente (1 = Despedir jugador)
                frmVentanaEmergenteQuitarCartera mensajeQuitarDeCartera = new frmVentanaEmergenteQuitarCartera(titulo, mensaje, idJugador, 4);

                bool? resultado = mensajeQuitarDeCartera.ShowDialog(); // Capturar el resultado del diálogo

                if (resultado == true) // Solo ejecutar si se pulsó Aceptar
                {
                    _logicaOjear.QuitarJugadorCartera(idJugador, _manager);
                    CargarJugadoresOjeados();
                }
            }
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
