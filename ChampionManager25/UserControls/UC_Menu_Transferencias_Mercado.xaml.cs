using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Transferencias_Mercado : UserControl
    {
        private Manager _manager;
        private int _equipo;

        #region "Variables filtros"
        int tipoStart = 1;
        int tipoEnd = 2;
        int mediaStart = 0;
        int mediaEnd = 100;
        int posicionStart = 1;
        int posicionEnd = 10;
        #endregion

        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_Menu_Transferencias_Mercado(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón TRANSFERIBLE
        private void btnTransferibles_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnTransferibles.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnTransferibles.Foreground = Brushes.WhiteSmoke;
            btnCedibles.Background = Brushes.WhiteSmoke;
            btnCedibles.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosTipo.Background = Brushes.LightGray;
            btnTodosTipo.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnTransferibles.IsEnabled = false;
            btnCedibles.IsEnabled = true;
            btnTodosTipo.IsEnabled = true;

            tipoStart = 1;
            tipoEnd = 1;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón CEDIBLE
        private void btnCedibles_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnCedibles.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnCedibles.Foreground = Brushes.WhiteSmoke;
            btnTransferibles.Background = Brushes.WhiteSmoke;
            btnTransferibles.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosTipo.Background = Brushes.LightGray;
            btnTodosTipo.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnCedibles.IsEnabled = false;
            btnTransferibles.IsEnabled = true;
            btnTodosTipo.IsEnabled = true;

            tipoStart = 2;
            tipoEnd = 2;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón TODOS (TIPO)
        private void btnTodosTipo_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnTodosTipo.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnTodosTipo.Foreground = Brushes.WhiteSmoke;
            btnTransferibles.Background = Brushes.WhiteSmoke;
            btnTransferibles.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnCedibles.Background = Brushes.WhiteSmoke;
            btnCedibles.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnTodosTipo.IsEnabled = false;
            btnTransferibles.IsEnabled = true;
            btnCedibles.IsEnabled = true;

            tipoStart = 1;
            tipoEnd = 2;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón 0-50
        private void btnMedia0050_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia0050.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedia0050.Foreground = Brushes.WhiteSmoke;
            btnMedia5165.Background = Brushes.WhiteSmoke;
            btnMedia5165.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia6680.Background = Brushes.WhiteSmoke;
            btnMedia6680.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia8190.Background = Brushes.WhiteSmoke;
            btnMedia8190.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia91100.Background = Brushes.WhiteSmoke;
            btnMedia91100.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = Brushes.LightGray;
            btnMediaTodos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnMedia0050.IsEnabled = false;
            btnMedia5165.IsEnabled = true;
            btnMedia6680.IsEnabled = true;
            btnMedia8190.IsEnabled = true;
            btnMedia91100.IsEnabled = true;
            btnMediaTodos.IsEnabled = true;

            mediaStart = 0;
            mediaEnd = 50;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón 51-65
        private void btnMedia5165_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia5165.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedia5165.Foreground = Brushes.WhiteSmoke;
            btnMedia0050.Background = Brushes.WhiteSmoke;
            btnMedia0050.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia6680.Background = Brushes.WhiteSmoke;
            btnMedia6680.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia8190.Background = Brushes.WhiteSmoke;
            btnMedia8190.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia91100.Background = Brushes.WhiteSmoke;
            btnMedia91100.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = Brushes.LightGray;
            btnMediaTodos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnMedia0050.IsEnabled = true;
            btnMedia5165.IsEnabled = false;
            btnMedia6680.IsEnabled = true;
            btnMedia8190.IsEnabled = true;
            btnMedia91100.IsEnabled = true;
            btnMediaTodos.IsEnabled = true;

            mediaStart = 51;
            mediaEnd = 65;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón 66-80
        private void btnMedia6680_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia6680.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedia6680.Foreground = Brushes.WhiteSmoke;
            btnMedia5165.Background = Brushes.WhiteSmoke;
            btnMedia5165.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia0050.Background = Brushes.WhiteSmoke;
            btnMedia0050.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia8190.Background = Brushes.WhiteSmoke;
            btnMedia8190.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia91100.Background = Brushes.WhiteSmoke;
            btnMedia91100.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = Brushes.LightGray;
            btnMediaTodos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnMedia0050.IsEnabled = true;
            btnMedia5165.IsEnabled = true;
            btnMedia6680.IsEnabled = false;
            btnMedia8190.IsEnabled = true;
            btnMedia91100.IsEnabled = true;
            btnMediaTodos.IsEnabled = true;

            mediaStart = 66;
            mediaEnd = 80;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón 81-90
        private void btnMedia8190_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia8190.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedia8190.Foreground = Brushes.WhiteSmoke;
            btnMedia5165.Background = Brushes.WhiteSmoke;
            btnMedia5165.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia6680.Background = Brushes.WhiteSmoke;
            btnMedia6680.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia0050.Background = Brushes.WhiteSmoke;
            btnMedia0050.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia91100.Background = Brushes.WhiteSmoke;
            btnMedia91100.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = Brushes.LightGray;
            btnMediaTodos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnMedia0050.IsEnabled = true;
            btnMedia5165.IsEnabled = true;
            btnMedia6680.IsEnabled = true;
            btnMedia8190.IsEnabled = false;
            btnMedia91100.IsEnabled = true;
            btnMediaTodos.IsEnabled = true;

            mediaStart = 81;
            mediaEnd = 90;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón 91-100
        private void btnMedia91100_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia91100.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedia91100.Foreground = Brushes.WhiteSmoke;
            btnMedia5165.Background = Brushes.WhiteSmoke;
            btnMedia5165.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia6680.Background = Brushes.WhiteSmoke;
            btnMedia6680.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia8190.Background = Brushes.WhiteSmoke;
            btnMedia8190.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia0050.Background = Brushes.WhiteSmoke;
            btnMedia0050.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = Brushes.LightGray;
            btnMediaTodos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnMedia0050.IsEnabled = true;
            btnMedia5165.IsEnabled = true;
            btnMedia6680.IsEnabled = true;
            btnMedia8190.IsEnabled = true;
            btnMedia91100.IsEnabled = false;
            btnMediaTodos.IsEnabled = true;

            mediaStart = 91;
            mediaEnd = 100;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón TODOS (MEDIA)
        private void btnMediaTodos_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedia91100.Background = Brushes.WhiteSmoke;
            btnMedia91100.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia5165.Background = Brushes.WhiteSmoke;
            btnMedia5165.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia6680.Background = Brushes.WhiteSmoke;
            btnMedia6680.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia8190.Background = Brushes.WhiteSmoke;
            btnMedia8190.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedia0050.Background = Brushes.WhiteSmoke;
            btnMedia0050.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMediaTodos.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMediaTodos.Foreground = Brushes.WhiteSmoke;

            btnMedia0050.IsEnabled = true;
            btnMedia5165.IsEnabled = true;
            btnMedia6680.IsEnabled = true;
            btnMedia8190.IsEnabled = true;
            btnMedia91100.IsEnabled = true;
            btnMediaTodos.IsEnabled = false;

            mediaStart = 0;
            mediaEnd = 100;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón PORTERO
        private void btnPorteros_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnPorteros.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnPorteros.Foreground = Brushes.WhiteSmoke;
            btnDefensas.Background = Brushes.WhiteSmoke;
            btnDefensas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedios.Background = Brushes.WhiteSmoke;
            btnMedios.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnDelanteros.Background = Brushes.WhiteSmoke;
            btnDelanteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosPosicion.Background = Brushes.LightGray;
            btnTodosPosicion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnPorteros.IsEnabled = false;
            btnDefensas.IsEnabled = true;
            btnMedios.IsEnabled = true;
            btnDelanteros.IsEnabled = true;
            btnTodosPosicion.IsEnabled = true;

            posicionStart = 1;
            posicionEnd = 1;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón DEFENSA
        private void btnDefensas_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnDefensas.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnDefensas.Foreground = Brushes.WhiteSmoke;
            btnPorteros.Background = Brushes.WhiteSmoke;
            btnPorteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedios.Background = Brushes.WhiteSmoke;
            btnMedios.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnDelanteros.Background = Brushes.WhiteSmoke;
            btnDelanteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosPosicion.Background = Brushes.LightGray;
            btnTodosPosicion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnPorteros.IsEnabled = true;
            btnDefensas.IsEnabled = false;
            btnMedios.IsEnabled = true;
            btnDelanteros.IsEnabled = true;
            btnTodosPosicion.IsEnabled = true;

            posicionStart = 2;
            posicionEnd = 4;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón MEDIO
        private void btnMedios_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnMedios.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnMedios.Foreground = Brushes.WhiteSmoke;
            btnDefensas.Background = Brushes.WhiteSmoke;
            btnDefensas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnPorteros.Background = Brushes.WhiteSmoke;
            btnPorteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnDelanteros.Background = Brushes.WhiteSmoke;
            btnDelanteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosPosicion.Background = Brushes.LightGray;
            btnTodosPosicion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnPorteros.IsEnabled = true;
            btnDefensas.IsEnabled = true;
            btnMedios.IsEnabled = false;
            btnDelanteros.IsEnabled = true;
            btnTodosPosicion.IsEnabled = true;

            posicionStart = 5;
            posicionEnd = 7;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón DELANTERO
        private void btnDelanteros_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnDelanteros.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnDelanteros.Foreground = Brushes.WhiteSmoke;
            btnDefensas.Background = Brushes.WhiteSmoke;
            btnDefensas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedios.Background = Brushes.WhiteSmoke;
            btnMedios.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnPorteros.Background = Brushes.WhiteSmoke;
            btnPorteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosPosicion.Background = Brushes.LightGray;
            btnTodosPosicion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btnPorteros.IsEnabled = true;
            btnDefensas.IsEnabled = true;
            btnMedios.IsEnabled = true;
            btnDelanteros.IsEnabled = false;
            btnTodosPosicion.IsEnabled = true;

            posicionStart = 8;
            posicionEnd = 10;

            ConfigurarDataGrid();
        }

        // ------------------------------------------------------------------- Evento CLICK del botón TODOS (POSICION)
        private void btnTodosPosicion_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnDelanteros.Background = Brushes.WhiteSmoke;
            btnDelanteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnDefensas.Background = Brushes.WhiteSmoke;
            btnDefensas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMedios.Background = Brushes.WhiteSmoke;
            btnMedios.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnPorteros.Background = Brushes.WhiteSmoke;
            btnPorteros.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTodosPosicion.Background = new SolidColorBrush(Color.FromRgb(29, 106, 125));
            btnTodosPosicion.Foreground = Brushes.WhiteSmoke;

            btnPorteros.IsEnabled = true;
            btnDefensas.IsEnabled = true;
            btnMedios.IsEnabled = true;
            btnDelanteros.IsEnabled = true;
            btnTodosPosicion.IsEnabled = false;

            posicionStart = 1;
            posicionEnd = 10;

            ConfigurarDataGrid();
        }

        #region "Métodos"
        // --------------------------------------------------------------------------------- Método que configura el DataGrid.
        private void ConfigurarDataGrid()
        {
            dgMercado.SelectionChanged -= dgMercado_SelectionChanged;

            dgMercado.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgMercado.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgMercado.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.ListadoJugadoresMercado(_equipo, tipoStart, tipoEnd, mediaStart, mediaEnd, posicionStart, posicionEnd);
            dgMercado.ItemsSource = jugador;

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("IdJugador"),
                Header = "IdJugador",
                Visibility = Visibility.Collapsed // Ocultar la columna
            });

            // Configurar columnas específicas
            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Dorsal"),
                Header = "Nº",
                Width = new DataGridLength(80, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });


            // Crear la columna de tipo DataGridTemplateColumn para la BANDERA
            DataGridTemplateColumn banderaColumna = new DataGridTemplateColumn
            {
                Header = "", // Título de la columna
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel)
            };

            // Crear un DataTemplate para la celda de la columna
            DataTemplate template = new DataTemplate(typeof(Image));

            // Crear un FrameworkElementFactory para la imagen
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetBinding(Image.SourceProperty, new Binding("Nacionalidad")
            {
                Converter = new NacionalidadToFlagConverter() // El convertidor convierte la nacionalidad a la imagen de la bandera
            });

            // Asignar el 'FrameworkElementFactory' al DataTemplate
            template.VisualTree = imageFactory;

            // Asignar el DataTemplate a la columna
            banderaColumna.CellTemplate = template;

            // Finalmente, agregar la columna al DataGrid
            dgMercado.Columns.Add(banderaColumna);

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "JUGADOR",
                Width = new DataGridLength(290, DataGridLengthUnitType.Pixel),
                HeaderStyle = new Style(typeof(DataGridColumnHeader))
                {
                    Setters =
                    {
                        new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Left), // Alineación a la izquierda
                        new Setter(BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))), // Fondo
                        new Setter(ForegroundProperty, Brushes.Black), // Color de texto
                        new Setter(FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")) // Fuente
                    }
                },
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Crear una instancia del convertidor
            var posicionConverter = new PosicionToStringConverter();

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("RolId")
                {
                    Converter = posicionConverter // Asignamos el convertidor aquí
                },
                Header = "DEM",
                Width = new DataGridLength(80, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });


            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("AlturaEnMetros")
                {
                    StringFormat = "0.00 'm'"  // Formato con dos decimales
                },
                Header = "ALTURA",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });


            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Edad"),
                Header = "EDAD",
                Width = new DataGridLength(80, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Media"), // Esta es la propiedad que queremos mostrar
                Header = "MED",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.BackgroundProperty, Brushes.Transparent), // Fondo transparente
                        new Setter(TextBlock.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")) // Fuente Arial Rounded MT Bold
                    }
                },
                // Estilo de celda con alternancia de color de fondo
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        // Fondo transparente por defecto
                        new Setter(DataGridCell.BackgroundProperty, Brushes.Transparent),
                        new Setter(DataGridCell.ForegroundProperty, new Binding
                        {
                            Path = new PropertyPath("Media"),
                            Converter = new MediaToColorConverter() // Convertidor para cambiar el color del texto
                        })
                    },
                    Triggers =
                    {
                        // Establecer el fondo para filas impares
                        new DataTrigger
                        {
                            Binding = new Binding("IsOddRow"),
                            Value = true,
                            Setters =
                            {
                                new Setter(DataGridCell.BackgroundProperty, Brushes.WhiteSmoke) // Fondo para filas impares
                            }
                        },
                        // Establecer el fondo para filas pares
                        new DataTrigger
                        {
                            Binding = new Binding("IsOddRow"),
                            Value = false,
                            Setters =
                            {
                                new Setter(DataGridCell.BackgroundProperty, Brushes.LightGray) // Fondo para filas pares
                            }
                        }
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Moral"),
                Header = "MO",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("EstadoForma"),
                Header = "EF",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            DataGridTemplateColumn columnaLesion = new DataGridTemplateColumn
            {
                Header = "LES",
                Width = new DataGridLength(60, DataGridLengthUnitType.Pixel)
            };

            // Crear la plantilla de celda
            DataTemplate template2 = new DataTemplate();
            FrameworkElementFactory imageFactory2 = new FrameworkElementFactory(typeof(Image));

            // Enlazar la propiedad Source de la imagen al convertidor
            Binding binding = new Binding("Lesion")
            {
                Converter = new LesionToImageConverter()
            };

            imageFactory2.SetBinding(Image.SourceProperty, binding);
            imageFactory2.SetValue(Image.WidthProperty, 30.0);
            imageFactory2.SetValue(Image.HeightProperty, 30.0);
            imageFactory2.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            imageFactory2.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            // Asignar la visualización al template
            template2.VisualTree = imageFactory2;
            columnaLesion.CellTemplate = template2;

            // Agregar la columna al DataGrid
            dgMercado.Columns.Add(columnaLesion);

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("SalarioTemporada")
                {
                    StringFormat = "{0:N0} €",   // Aplica el formato de números sin decimales
                    ConverterCulture = new System.Globalization.CultureInfo("es-ES") // Asegura el uso de puntos como separadores
                },
                Header = "SALARIO",
                Width = new DataGridLength(200, DataGridLengthUnitType.Pixel), // Ajusta el ancho según tu diseño
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("ClausulaRescision")
                {
                    StringFormat = "{0:N0} €",   // Aplica el formato de números sin decimales
                    ConverterCulture = new System.Globalization.CultureInfo("es-ES") // Asegura el uso de puntos como separadores
                },
                Header = "CLAÚSULA",
                Width = new DataGridLength(200, DataGridLengthUnitType.Pixel), // Ajusta el ancho según tu diseño
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("ValorMercado")
                {
                    StringFormat = "{0:N0} €",   // Aplica el formato de números sin decimales
                    ConverterCulture = new System.Globalization.CultureInfo("es-ES") // Asegura el uso de puntos como separadores
                },
                Header = "VALOR MERCADO",
                Width = new DataGridLength(200, DataGridLengthUnitType.Pixel), // Ajusta el ancho según tu diseño
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("AniosContrato"),
                Header = "AÑOS",
                Width = new DataGridLength(80, DataGridLengthUnitType.Pixel), // Ajusta el ancho según tu diseño
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgMercado.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("SituacionMercado")
                {
                    Converter = new SituacionMercadoConverter()
                },
                Header = "TIPO",
                Width = new DataGridLength(125, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                            {
                                new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                                new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                            }
                }
            });

            dgMercado.SelectionChanged += dgMercado_SelectionChanged;

            // Configurar altura de filas y estilos generales
            dgMercado.RowHeight = 35;
            dgMercado.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgMercado.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgMercado.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgMercado.BorderThickness = new Thickness(0);
            dgMercado.CanUserAddRows = false;
            dgMercado.CanUserResizeColumns = false;
            dgMercado.CanUserResizeRows = false;
            dgMercado.SelectionMode = DataGridSelectionMode.Single;
            dgMercado.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgMercado.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgMercado.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 16.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(5)));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgMercado.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgMercado.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Transparent)),
                    new Setter(Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(35, 40, 45))),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")),
                    new Setter(Control.FontSizeProperty, 16.0),
                    new Setter(Control.PaddingProperty, new Thickness(5)),
                    new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent),
                    new Setter(DataGridCell.BorderThicknessProperty, new Thickness(0))
                }
            };
        }

        private void dgMercado_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Obtener el DockPanel padre
            DockPanel? parentDockPanel = this.Parent as DockPanel;

            if (dgMercado.SelectedItem == null)
                return; // Evita errores si no hay selección

            // Buscar el UserControl UC_PantallaPrincipal en la ventana principal
            UC_PantallaPrincipal pantallaPrincipal = FindParentUC_PantallaPrincipal(this);

            if (pantallaPrincipal == null)
            {
                MessageBox.Show("No se pudo encontrar UC_PantallaPrincipal.");
                return;
            }

            // Verifica el tipo de objeto de la fila seleccionada
            if (dgMercado.SelectedItem is Jugador jugadorSeleccionado)
            {
                if (parentDockPanel != null)
                {
                    // Limpiar el contenido actual
                    parentDockPanel.Children.Clear();

                    // Crear el nuevo UserControl y agregarlo al DockPanel
                    UC_FichaJugador fichaJugador = new UC_FichaJugador(jugadorSeleccionado.IdJugador, _equipo, _manager, 2, pantallaPrincipal);
                    parentDockPanel.Children.Add(fichaJugador);
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
        #endregion
    }
}
