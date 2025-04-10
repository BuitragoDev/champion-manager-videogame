using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static System.Resources.ResXFileRef;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChampionManager25.UserControls
{
    public partial class UC_CargarPartida : UserControl
    {
        private List<PartidaGuardada> _partidas;
        private Border borderSeleccionado = null; // Variable para almacenar el Border seleccionado


        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        ManagerLogica _logicaManager = new ManagerLogica();

        public UC_CargarPartida()
        {
            InitializeComponent();
        }

        private void cargarPartida_Loaded(object sender, RoutedEventArgs e)
        {
            CargarListaPartidas();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR PARTIDA
        private void btnCargarPartida_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (borderSeleccionado != null && borderSeleccionado.Tag is PartidaGuardada partidaSeleccionada)
            {
                try
                {
                    Conexion.CerrarTodasLasConexiones();
                    Conexion.EstablecerConexionPartida(partidaSeleccionada.Ruta);

                    var logicaManager = new ManagerLogica();
                    var manager = logicaManager.MostrarManager(partidaSeleccionada.IdManager);
                    var idEquipo = manager.IdEquipo ?? 0;

                    if (idEquipo == 0)
                    {
                        MessageBox.Show("Esta partida no tiene equipo asignado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.PartidaEnProgreso = true;
                    mainWindow.RutaPartidaActual = partidaSeleccionada.Ruta;

                    mainWindow.CargarPantallaPrincipal(manager, idEquipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar la partida: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una partida para cargar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton ELIMINAR PARTIDA
        private void btnEliminarPartida_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (borderSeleccionado != null && borderSeleccionado.Tag is PartidaGuardada partidaSeleccionada)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "¿Estás seguro de quieres borrar esta partida guardada?";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 1);

                bool? resultado = ventanaEmergente.ShowDialog();
                if (resultado == true)
                {
                    try
                    {
                        Conexion.CerrarTodasLasConexiones();
                        _partidas.Remove(partidaSeleccionada);

                        if (File.Exists(partidaSeleccionada.Ruta))
                        {
                            File.Delete(partidaSeleccionada.Ruta);
                        }

                        // Actualizar la vista de las partidas cargadas
                        CargarListaPartidas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar la partida: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "Por favor, selecciona una partida para eliminar";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                bool? resultado = ventanaEmergente.ShowDialog();
            }
        }


        #region "Metodos"
        private void CargarListaPartidas()
        {
            try
            {
                _partidas = new List<PartidaGuardada>();
                var rutasPartidas = GestorPartidas.ObtenerPartidasGuardadas();

                // Limpiar el contenedor antes de agregar nuevos elementos
                wrapPartidasCargadas.Children.Clear();

                foreach (var ruta in rutasPartidas)
                {
                    // Establecer conexión temporal para leer los datos
                    Conexion.EstablecerConexionPartida(ruta);

                    var logicaManager = new ManagerLogica();
                    var manager = logicaManager.MostrarManager(1);
                    var logicaEquipo = new EquipoLogica();
                    string nombreEquipo = manager.IdEquipo.HasValue
                                            ? logicaEquipo.ListarDetallesEquipo(manager.IdEquipo.Value).Nombre
                                            : "Sin equipo";

                    var partida = new PartidaGuardada
                    {
                        Ruta = ruta,
                        IdManager = manager.IdManager,
                        Nombre = Path.GetFileNameWithoutExtension(ruta),
                        NombreManager = manager != null ? $"{manager.Nombre} {manager.Apellido}" : "Manager desconocido",
                        Equipo = nombreEquipo,
                        FechaCreacion = File.GetCreationTime(ruta),
                        UltimaModificacion = File.GetLastWriteTime(ruta)
                    };

                    _partidas.Add(partida);

                    // Crear un Border que contendrá el Grid
                    Border border = new Border
                    {
                        Background = Brushes.LightGray,
                        BorderBrush = new SolidColorBrush(Colors.DarkGray),
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(5),
                        Cursor = Cursors.Hand,
                        Width = 1010,
                        Height = 98,
                        Tag = partida // Asocia la partida al Border
                    };

                    // Agregar eventos MouseEnter, MouseLeave
                    border.MouseEnter += (s, e) =>
                    {
                        if (border != borderSeleccionado)
                        {
                            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a0be99"));
                        }
                    };
                    border.MouseLeave += (s, e) =>
                    {
                        if (border != borderSeleccionado)
                        {
                            border.Background = new SolidColorBrush(Colors.LightGray);
                        }
                    };

                    // Agregar evento MouseDown para seleccionar el Border
                    border.MouseDown += (s, e) =>
                    {
                        Metodos.ReproducirSonidoClick();
                        // Si ya hay un Border seleccionado, restaurar su color
                        if (borderSeleccionado != null && borderSeleccionado != border)
                        {
                            borderSeleccionado.Background = new SolidColorBrush(Colors.LightGray);
                        }

                        // Establecer el nuevo Border como seleccionado y cambiar su color
                        borderSeleccionado = border;
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a0be99")); // Color de selección
                    };

                    // Crear Grid dentro del Border
                    Grid grid = new Grid();
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(680, GridUnitType.Star) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220, GridUnitType.Star) });

                    // Crear la imagen para el escudo del equipo
                    Image imgEscudo = new Image
                    {
                        Width = 80,
                        Height = 80,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    string escudoPath = $"/Recursos/img/escudos_equipos/80x80/{manager.IdEquipo}.png";
                    imgEscudo.Source = new BitmapImage(new Uri(escudoPath, UriKind.RelativeOrAbsolute));

                    Grid.SetColumn(imgEscudo, 0);
                    grid.Children.Add(imgEscudo);

                    // Crear TextBlock para el nombre del manager
                    TextBlock nombrePartidaTextBlock = new TextBlock
                    {
                        Text = $"{manager.Nombre} {manager.Apellido} ({nombreEquipo})",
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontWeight = FontWeights.Bold,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        FontSize = 18,
                    };
                    Grid.SetColumn(nombrePartidaTextBlock, 1);
                    grid.Children.Add(nombrePartidaTextBlock);

                    // Crear TextBlock para la fecha de creación de la partida
                    TextBlock fechaTextBlock = new TextBlock
                    {
                        Text = File.GetCreationTime(ruta).ToString("dd/MM/yyyy"),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        FontSize = 18,
                    };
                    Grid.SetColumn(fechaTextBlock, 2);
                    grid.Children.Add(fechaTextBlock);

                    border.Child = grid;
                    wrapPartidasCargadas.Children.Add(border);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las partidas: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
