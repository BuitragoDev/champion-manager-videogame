using ChampionManager25.Datos;
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

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Transferencias_EstadoOfertas : UserControl
    {
        private Manager _manager;
        private int _equipo;
        int reputacionDirectorTecnico = 0;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();

        public UC_Menu_Transferencias_EstadoOfertas(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");

            if (director != null)
            {
                txtNombreDirectorTecnico.Text = director.Nombre;
                reputacionDirectorTecnico = director.Categoria;
                MostrarEstrellas(reputacionDirectorTecnico);

                string[] seguimiento = {
                    "Nuestro Director Técnico nos permite hacer una operación al mismo tiempo.",
                    "Nuestro Director Técnico nos permite hacer hasta dos operaciones al mismo tiempo.",
                    "Nuestro Director Técnico nos permite hacer hasta tres operaciones al mismo tiempo.",
                    "Nuestro Director Técnico nos permite hacer hasta cuatro operaciones al mismo tiempo.",
                    "Nuestro Director Técnico nos permite hacer hasta cinco operaciones al mismo tiempo."
                };

                if (reputacionDirectorTecnico >= 1 && reputacionDirectorTecnico <= 5)
                {
                    txtNivelDirectorTecnico.Text = seguimiento[reputacionDirectorTecnico - 1];
                }

                CargarJugadoresConOfertas();
            }
            else
            {
                txtNombreDirectorTecnico.Text = "No tienes ningún Director Técnico contratado";
            } 
        }

        #region "Metodos"
        private void CargarJugadoresConOfertas()
        {
            List<Transferencia> ofertas = _logicaTransferencia.ListarOfertasSinFinalizar();
            WrapPanel wrapPanel = new WrapPanel();

            foreach (var oferta in ofertas)
            {
                Jugador jugador = _logicaJugador.MostrarDatosJugador(oferta.IdJugador);
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
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });

                string imagePath = $"{GestorPartidas.RutaMisDocumentos}/{jugador.RutaImagen}";
                grid.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
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
                    HorizontalAlignment = HorizontalAlignment.Left,
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

                    // Crear el nuevo UserControl y agregarlo al DockPanel (Opcion 6: Cartera)
                    UC_FichaJugador fichaJugador = new UC_FichaJugador(jugador.IdJugador, _equipo, _manager, 7, pantallaPrincipal);
                    parentDockPanel.Children.Add(fichaJugador);

                };

                Grid.SetColumn(nombreJugador, 1);
                grid.Children.Add(nombreJugador);

                grid.Children.Add(new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre,
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 2);

                grid.Children.Add(new TextBlock
                {
                    Text = jugador.Velocidad.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 3);

                grid.Children.Add(new TextBlock
                {
                    Text = jugador.Resistencia.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 4);

                grid.Children.Add(new TextBlock
                {
                    Text = jugador.Agresividad.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 5);


                grid.Children.Add(new TextBlock
                {
                    Text = jugador.Calidad.ToString(),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 6);

                grid.Children.Add(new TextBlock
                {
                    Text = oferta.MontoOferta != 0 ? oferta.MontoOferta.ToString("N0") + " €" : "",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 7);

                grid.Children.Add(new TextBlock
                {
                    Text = oferta.SalarioAnual != 0 ? oferta.SalarioAnual.ToString("N0") + " €" : "",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 8);

                grid.Children.Add(new TextBlock
                {
                    Text = oferta.Duracion != 0 ? oferta.Duracion.ToString() : "",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 9);

                grid.Children.Add(new TextBlock
                {
                    Text = DateTime.Parse(oferta.FechaOferta).ToString("dd/MM/yyyy"),
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 10);

                grid.Children.Add(new TextBlock
                {
                    Text = oferta.TipoFichaje == 1 ? "TRASPASO" : "CESIÓN",
                    FontSize = 16,
                    Foreground = Brushes.Black,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold")
                });
                Grid.SetColumn(grid.Children[^1], 11);

                string rutaImagen = oferta.RespuestaEquipo == 1
                                        ? "pack://application:,,,/Recursos/img/icons/aceptado32px.png"
                                        : "pack://application:,,,/Recursos/img/icons/rechazado32px.png";

                Image imagenRespuesta = new Image
                {
                    Source = new BitmapImage(new Uri(rutaImagen, UriKind.Absolute)),
                    Width = 32,
                    Height = 32,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                grid.Children.Add(imagenRespuesta);
                Grid.SetColumn(imagenRespuesta, 12);

                // Botón de borrar jugador
                Image btnBorrar = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/papelera_icon.png")),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Cursor = Cursors.Hand,
                    Tag = jugador.IdJugador // Guardamos el ID del jugador en la propiedad Tag
                };

                // Asignamos el evento de clic al botón de borrar
                btnBorrar.MouseLeftButtonDown += BtnBorrar_MouseLeftButtonDown;

                Grid.SetColumn(btnBorrar, 13);
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
                string mensaje = $"¿Estás seguro de que quieres eliminar la oferta por {_logicaJugador.MostrarDatosJugador(idJugador).NombreCompleto.ToUpper()}?";

                // Crear y mostrar la ventana emergente (1 = Despedir jugador)
                frmVentanaEmergenteQuitarCartera mensajeQuitarDeCartera = new frmVentanaEmergenteQuitarCartera(titulo, mensaje, idJugador, 4);

                bool? resultado = mensajeQuitarDeCartera.ShowDialog(); // Capturar el resultado del diálogo

                if (resultado == true) // Solo ejecutar si se pulsó Aceptar
                {
                    _logicaTransferencia.BorrarOferta(idJugador);
                    CargarJugadoresConOfertas();
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

        private void MostrarEstrellas(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellas.Children.Clear();

            // Cargar las imágenes de recursos
            ImageSource estrellaON = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOn.png"));
            ImageSource estrellaOFF = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOff.png"));

            // Determinar el número de estrellas según la reputación
            int numeroEstrellas = 0;

            if (reputacion == 5)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion == 4)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion == 3)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion == 2)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion == 1)
            {
                numeroEstrellas = 1;
            }
            else
            {
                numeroEstrellas = 0;
            }

            // Dibujar 5 estrellas (activas e inactivas)
            for (int i = 0; i < 5; i++)
            {
                // Crear la imagen de la estrella
                Image estrella = new Image
                {
                    Width = 32, // Ancho de cada estrella
                    Height = 32,
                    Source = i < numeroEstrellas ? estrellaON : estrellaOFF
                };

                // Colocar la estrella en el Canvas
                Canvas.SetLeft(estrella, i * 35); // Separación horizontal entre estrellas
                Canvas.SetTop(estrella, 0);

                // Agregar la estrella al Canvas
                canvasEstrellas.Children.Add(estrella);
            }
        }
        #endregion
    }
}
