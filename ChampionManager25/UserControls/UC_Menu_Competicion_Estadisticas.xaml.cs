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
    public partial class UC_Menu_Competicion_Estadisticas : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        private JugadorLogica _logicaJugador = new JugadorLogica();
        private EquipoLogica _logicaEquipo = new EquipoLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();

        public UC_Menu_Competicion_Estadisticas(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void estadisticas_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGridEstadisticas(1);
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton GOLES
        private void btnGoles_Click(object sender, RoutedEventArgs e)
        {
            btnGoles.IsEnabled = false;
            btnAsistencias.IsEnabled = true;
            btnTA.IsEnabled = true;
            btnTR.IsEnabled = true;
            btnMVP.IsEnabled = true;

            btnGoles.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnAsistencias.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTA.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTR.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnMVP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            ConfigurarDataGridEstadisticas(1);
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton ASISTENCIAS
        private void btnAsistencias_Click(object sender, RoutedEventArgs e)
        {
            btnGoles.IsEnabled = true;
            btnAsistencias.IsEnabled = false;
            btnTA.IsEnabled = true;
            btnTR.IsEnabled = true;
            btnMVP.IsEnabled = true;

            btnGoles.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnAsistencias.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTA.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTR.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnMVP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            ConfigurarDataGridEstadisticas(2);
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton TARJETAS AMARILLAS
        private void btnTA_Click(object sender, RoutedEventArgs e)
        {
            btnGoles.IsEnabled = true;
            btnAsistencias.IsEnabled = true;
            btnTA.IsEnabled = false;
            btnTR.IsEnabled = true;
            btnMVP.IsEnabled = true;

            btnGoles.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnAsistencias.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTA.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnTR.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnMVP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            ConfigurarDataGridEstadisticas(3);
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton TARJETAS ROJAS
        private void btnTR_Click(object sender, RoutedEventArgs e)
        {
            btnGoles.IsEnabled = true;
            btnAsistencias.IsEnabled = true;
            btnTA.IsEnabled = true;
            btnTR.IsEnabled = false;
            btnMVP.IsEnabled = true;

            btnGoles.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnAsistencias.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTA.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTR.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btnMVP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            ConfigurarDataGridEstadisticas(4);
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton MVPS
        private void btnMVP_Click(object sender, RoutedEventArgs e)
        {
            btnGoles.IsEnabled = true;
            btnAsistencias.IsEnabled = true;
            btnTA.IsEnabled = true;
            btnTR.IsEnabled = true;
            btnMVP.IsEnabled = false;

            btnGoles.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnAsistencias.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTA.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnTR.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            btnMVP.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            ConfigurarDataGridEstadisticas(5);
        }

        #region "Metodos"
        // DATAGRID DE LA OPCION ESTADISTICAS.
        private void ConfigurarDataGridEstadisticas(int filtro)
        {
            dgEstadisticas.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgEstadisticas.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgEstadisticas.Columns.Clear(); // Limpiar cualquier columna previa

            List<Estadistica> jugador = _logicaEstadistica.MostrarEstadisticasTotales(_manager.IdManager, filtro);
            dgEstadisticas.ItemsSource = jugador;

            // Crear la columna de tipo DataGridTemplateColumn para la foto del Futbolista
            DataGridTemplateColumn fotoColumna = new DataGridTemplateColumn
            {
                Header = "", // Título de la columna
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel)
            };

            // Crear un DataTemplate para la celda de la columna
            DataTemplate template = new DataTemplate(typeof(Image));

            // Crear un FrameworkElementFactory para la imagen
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetBinding(Image.SourceProperty, new Binding("IdJugador")
            {
                Converter = new IdjugadorToFotoConverter()
            });

            imageFactory.SetValue(Image.WidthProperty, 64.0);
            imageFactory.SetValue(Image.HeightProperty, 64.0);
            imageFactory.SetValue(Image.StretchProperty, Stretch.Uniform);

            // Asignar el 'FrameworkElementFactory' al DataTemplate
            template.VisualTree = imageFactory;

            // Asignar el DataTemplate a la columna
            fotoColumna.CellTemplate = template;

            // Finalmente, agregar la columna al DataGrid
            dgEstadisticas.Columns.Add(fotoColumna);

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "JUGADOR",
                Width = new DataGridLength(320, DataGridLengthUnitType.Pixel),
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
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 18.0) 
                    }
                }
            });

            // Crear la columna de tipo DataGridTemplateColumn para el ESCUDO
            DataGridTemplateColumn escudoColumna = new DataGridTemplateColumn
            {
                Header = "", // Título de la columna
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel)
            };

            // Crear un DataTemplate para la celda de la columna
            DataTemplate templateEscudo = new DataTemplate(typeof(Image));

            // Crear un FrameworkElementFactory para la imagen
            FrameworkElementFactory imageFactoryEscudo = new FrameworkElementFactory(typeof(Image));
            imageFactoryEscudo.SetBinding(Image.SourceProperty, new Binding("IdEquipo")
            {
                Converter = new IdEquipoToEscudoConverter()
            });

            imageFactoryEscudo.SetValue(Image.WidthProperty, 32.0);
            imageFactoryEscudo.SetValue(Image.HeightProperty, 32.0);
            imageFactoryEscudo.SetValue(Image.StretchProperty, Stretch.Uniform);

            // Asignar el 'FrameworkElementFactory' al DataTemplate
            templateEscudo.VisualTree = imageFactoryEscudo;

            // Asignar el DataTemplate a la columna
            escudoColumna.CellTemplate = templateEscudo;

            // Finalmente, agregar la columna al DataGrid
            dgEstadisticas.Columns.Add(escudoColumna);

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PartidosJugados"),
                Header = "P. JUGADOS",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Goles"),
                Header = "GOLES",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Asistencias"),
                Header = "ASISTENCIAS",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("TarjetasAmarillas"),
                Header = "AMARILLAS",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("TarjetasRojas"),
                Header = "ROJAS",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            dgEstadisticas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("MVP"),
                Header = "MVP's",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontSizeProperty, 24.0)
                    }
                }
            });

            // Configurar altura de filas y estilos generales
            dgEstadisticas.RowHeight = 76;
            dgEstadisticas.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgEstadisticas.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgEstadisticas.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgEstadisticas.BorderThickness = new Thickness(0);
            dgEstadisticas.CanUserAddRows = false;
            dgEstadisticas.CanUserResizeColumns = false;
            dgEstadisticas.CanUserResizeRows = false;
            dgEstadisticas.SelectionMode = DataGridSelectionMode.Single;
            dgEstadisticas.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgEstadisticas.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgEstadisticas.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgEstadisticas.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgEstadisticas.CellStyle = new Style(typeof(DataGridCell))
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
        #endregion
    }
}
