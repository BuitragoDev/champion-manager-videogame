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
    public partial class UC_Menu_Competicion_Palmares_Jugadores : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        List<Equipo> equipos = new List<Equipo>();
        #endregion

        // Instancias de la LOGICA
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        EquipoLogica _logicaEquipos = new EquipoLogica();

        public UC_Menu_Competicion_Palmares_Jugadores(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            equipos = _logicaEquipos.ListarEquipos(0)
                        .Concat(_logicaEquipos.ListarEquipos(1))
                        .Concat(_logicaEquipos.ListarEquipos(2))
                        .ToList();
            Metodos metodos = new Metodos();
        }

        private void palmaresJugadores_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGridMejorJugador();
            ConfigurarDataGridHistorialMejorJugador();
        }

        // ------------------------------------------------------------------------------ Evento CLICK del boton BALON DE ORO
        private void imgLogoBalon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            ConfigurarDataGridMejorJugador();
            ConfigurarDataGridHistorialMejorJugador();
            imgLogoBalon.IsEnabled = false;
            imgLogoBota.IsEnabled = true;
            imgBanner.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/Recursos/img/bannerBalonOro.png"));
        }

        // ------------------------------------------------------------------------------ Evento CLICK del boton BOTA DE ORO
        private void imgLogoBota_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            ConfigurarDataGridMaximoGoleador();
            ConfigurarDataGridHistorialMaximoGoleador();
            imgLogoBalon.IsEnabled = true;
            imgLogoBota.IsEnabled = false;
            imgBanner.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/Recursos/img/bannerBotaOro.png"));
        }

        #region "Metodos"
        private void ConfigurarDataGridMejorJugador()
        {
            dgPalmaresJugadoresTotales.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPalmaresJugadoresTotales.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPalmaresJugadoresTotales.Columns.Clear(); // Limpiar cualquier columna previa

            List<PalmaresJugador> palmares = _logicaPalmares.MostrarPalmaresBalonOroTotal();
            dgPalmaresJugadoresTotales.ItemsSource = palmares;

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaJugador()
            });

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugador"),
                Header = "JUGADOR",
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

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTextColumn
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
            dgPalmaresJugadoresTotales.RowHeight = 35;
            dgPalmaresJugadoresTotales.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPalmaresJugadoresTotales.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPalmaresJugadoresTotales.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPalmaresJugadoresTotales.BorderThickness = new Thickness(0);
            dgPalmaresJugadoresTotales.CanUserAddRows = false;
            dgPalmaresJugadoresTotales.CanUserResizeColumns = false;
            dgPalmaresJugadoresTotales.CanUserResizeRows = false;
            dgPalmaresJugadoresTotales.SelectionMode = DataGridSelectionMode.Single;
            dgPalmaresJugadoresTotales.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPalmaresJugadoresTotales.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPalmaresJugadoresTotales.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPalmaresJugadoresTotales.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPalmaresJugadoresTotales.CellStyle = new Style(typeof(DataGridCell))
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

        private void ConfigurarDataGridHistorialMejorJugador()
        {
            dgPalmaresJugadores.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPalmaresJugadores.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPalmaresJugadores.Columns.Clear(); // Limpiar cualquier columna previa

            List<HistorialJugador> historial = _logicaPalmares.MostrarPalmaresBalonOro();
            dgPalmaresJugadores.ItemsSource = historial;

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
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


            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaOro()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorOro"),
                Header = "BALON DE ORO",
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

            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaPlata()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorPlata"),
                Header = "BALON DE PLATA",
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

            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaBronce()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorBronce"),
                Header = "BALON DE BRONCE",
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
            dgPalmaresJugadores.RowHeight = 35;
            dgPalmaresJugadores.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPalmaresJugadores.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPalmaresJugadores.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPalmaresJugadores.BorderThickness = new Thickness(0);
            dgPalmaresJugadores.CanUserAddRows = false;
            dgPalmaresJugadores.CanUserResizeColumns = false;
            dgPalmaresJugadores.CanUserResizeRows = false;
            dgPalmaresJugadores.SelectionMode = DataGridSelectionMode.Single;
            dgPalmaresJugadores.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPalmaresJugadores.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPalmaresJugadores.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPalmaresJugadores.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPalmaresJugadores.CellStyle = new Style(typeof(DataGridCell))
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

        private void ConfigurarDataGridMaximoGoleador()
        {
            dgPalmaresJugadoresTotales.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPalmaresJugadoresTotales.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPalmaresJugadoresTotales.Columns.Clear(); // Limpiar cualquier columna previa

            List<PalmaresJugador> palmares = _logicaPalmares.MostrarPalmaresBotaOroTotal();
            dgPalmaresJugadoresTotales.ItemsSource = palmares;

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaJugador()
            });

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugador"),
                Header = "JUGADOR",
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

            dgPalmaresJugadoresTotales.Columns.Add(new DataGridTextColumn
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
            dgPalmaresJugadoresTotales.RowHeight = 35;
            dgPalmaresJugadoresTotales.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPalmaresJugadoresTotales.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPalmaresJugadoresTotales.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPalmaresJugadoresTotales.BorderThickness = new Thickness(0);
            dgPalmaresJugadoresTotales.CanUserAddRows = false;
            dgPalmaresJugadoresTotales.CanUserResizeColumns = false;
            dgPalmaresJugadoresTotales.CanUserResizeRows = false;
            dgPalmaresJugadoresTotales.SelectionMode = DataGridSelectionMode.Single;
            dgPalmaresJugadoresTotales.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPalmaresJugadoresTotales.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPalmaresJugadoresTotales.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPalmaresJugadoresTotales.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPalmaresJugadoresTotales.CellStyle = new Style(typeof(DataGridCell))
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

        private void ConfigurarDataGridHistorialMaximoGoleador()
        {
            dgPalmaresJugadores.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPalmaresJugadores.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPalmaresJugadores.Columns.Clear(); // Limpiar cualquier columna previa

            List<HistorialJugador> historial = _logicaPalmares.MostrarPalmaresBotaOro();
            dgPalmaresJugadores.ItemsSource = historial;

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
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

            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaOro()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorOro"),
                Header = "BALON DE ORO",
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

            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaPlata()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorPlata"),
                Header = "BALON DE PLATA",
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

            dgPalmaresJugadores.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaBronce()
            });

            dgPalmaresJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreJugadorBronce"),
                Header = "BALON DE BRONCE",
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
            dgPalmaresJugadores.RowHeight = 35;
            dgPalmaresJugadores.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPalmaresJugadores.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPalmaresJugadores.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPalmaresJugadores.BorderThickness = new Thickness(0);
            dgPalmaresJugadores.CanUserAddRows = false;
            dgPalmaresJugadores.CanUserResizeColumns = false;
            dgPalmaresJugadores.CanUserResizeRows = false;
            dgPalmaresJugadores.SelectionMode = DataGridSelectionMode.Single;
            dgPalmaresJugadores.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPalmaresJugadores.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPalmaresJugadores.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPalmaresJugadores.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPalmaresJugadores.CellStyle = new Style(typeof(DataGridCell))
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

        private DataTemplate CrearPlantillaOro()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdJugadorOro")
            {
                Converter = new IdjugadorToFotoConverter()  
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private DataTemplate CrearPlantillaPlata()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdJugadorPlata")
            {
                Converter = new IdjugadorToFotoConverter()  
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private DataTemplate CrearPlantillaBronce()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdJugadorBronce")
            {
                Converter = new IdjugadorToFotoConverter()  
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private DataTemplate CrearPlantillaJugador()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdJugador")
            {
                Converter = new IdjugadorToFotoConverter()
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }
        #endregion
    }
}
