using ChampionManager25.Datos;
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
    public partial class UC_Menu_Competicion_Palmares : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DockPanel _panelCentral;
        List<Equipo> equipos = new List<Equipo>();
        #endregion

        // Instancias de la LOGICA
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        EquipoLogica _logicaEquipos = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Competicion_Palmares(Manager manager, int equipo, DockPanel panelCentral)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _panelCentral = panelCentral;
            equipos = _logicaEquipos.ListarEquipos(0)
                        .Concat(_logicaEquipos.ListarEquipos(1))
                        .Concat(_logicaEquipos.ListarEquipos(2))
                        .Concat(_logicaEquipos.ListarEquipos(3))
                        .ToList();
            Metodos metodos = new Metodos();
        }

        private void palmares_Loaded(object sender, RoutedEventArgs e)
        {
            ImagePathConverterPalmares.Equipos = equipos;
            ConfigurarDataGridPalmares();
            ConfigurarDataGridHistorial();

            string ruta_liga = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            lblTitulo.Text += " (" + _logicaCompeticion.MostrarNombreCompeticion(1).ToUpper() + ")";
            imgLogoLiga.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga));
            string ruta_copa = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
            imgLogoCopa.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_copa));
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton LIGA
        private void imgLogoLiga_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_Palmares ucLiga = new UC_Menu_Competicion_Palmares(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucLiga);

            imgLogoLiga.IsEnabled = false;
            imgLogoCopa.IsEnabled = true;
        }

        // ------------------------------------------------------------------------------- Evento CLICK del boton COPA
        private void imgLogoCopa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_PalmaresCopa ucCopa = new UC_Menu_Competicion_PalmaresCopa(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucCopa);

            imgLogoLiga.IsEnabled = true;
            imgLogoCopa.IsEnabled = false;
        }

        #region "Metodos"
        private void ConfigurarDataGridPalmares()
        {
            dgPalmares.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPalmares.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPalmares.Columns.Clear(); // Limpiar cualquier columna previa

            List<Palmares> palmares = _logicaPalmares.MostrarPalmaresCompleto();
            dgPalmares.ItemsSource = palmares;

            // Crear la columna de tipo DataGridTemplateColumn para el ESCUDO
            dgPalmares.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaLogo()  // Pasamos los equipos
            });

            dgPalmares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipo"),
                Header = "EQUIPO",
                Width = new DataGridLength(430, DataGridLengthUnitType.Pixel),
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

            dgPalmares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Titulos"),
                Header = "TITULOS",
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

            // Configurar altura de filas y estilos generales
            dgPalmares.RowHeight = 35;
            dgPalmares.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPalmares.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPalmares.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPalmares.BorderThickness = new Thickness(0);
            dgPalmares.CanUserAddRows = false;
            dgPalmares.CanUserResizeColumns = false;
            dgPalmares.CanUserResizeRows = false;
            dgPalmares.SelectionMode = DataGridSelectionMode.Single;
            dgPalmares.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPalmares.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPalmares.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPalmares.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPalmares.CellStyle = new Style(typeof(DataGridCell))
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

        private void ConfigurarDataGridHistorial()
        {
            dgHistorialFinales.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgHistorialFinales.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgHistorialFinales.Columns.Clear(); // Limpiar cualquier columna previa

            List<HistorialFinales> historial = _logicaPalmares.MostrarHistorialFinales();
            dgHistorialFinales.ItemsSource = historial;

            dgHistorialFinales.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Temporada"),
                Header = "TEMPORADA",
                Width = new DataGridLength(150, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Crear la columna de tipo DataGridTemplateColumn para el ESCUDO
            dgHistorialFinales.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaLogoCampeon() 
            });

            dgHistorialFinales.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipoCampeon"),
                Header = "CAMPEÓN",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
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

            // Crear la columna de tipo DataGridTemplateColumn para el ESCUDO
            dgHistorialFinales.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaLogoFinalista()  // Pasamos los equipos
            });

            dgHistorialFinales.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipoFinalista"),
                Header = "SUBCAMPEÓN",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
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

            // Configurar altura de filas y estilos generales
            dgHistorialFinales.RowHeight = 35;
            dgHistorialFinales.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgHistorialFinales.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgHistorialFinales.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgHistorialFinales.BorderThickness = new Thickness(0);
            dgHistorialFinales.CanUserAddRows = false;
            dgHistorialFinales.CanUserResizeColumns = false;
            dgHistorialFinales.CanUserResizeRows = false;
            dgHistorialFinales.SelectionMode = DataGridSelectionMode.Single;
            dgHistorialFinales.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgHistorialFinales.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgHistorialFinales.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgHistorialFinales.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgHistorialFinales.CellStyle = new Style(typeof(DataGridCell))
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

        private DataTemplate CrearPlantillaLogo()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdEquipo")
            {
                Converter = new ImagePathConverterPalmares()  // No necesitamos pasar la lista aquí
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private DataTemplate CrearPlantillaLogoCampeon()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdEquipoCampeon")
            {
                Converter = new ImagePathConverterPalmares()  // No necesitamos pasar la lista aquí
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private DataTemplate CrearPlantillaLogoFinalista()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdEquipoFinalista")
            {
                Converter = new ImagePathConverterPalmares()  // No necesitamos pasar la lista aquí
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }
        #endregion
    }
}
