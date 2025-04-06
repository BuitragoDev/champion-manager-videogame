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
    public partial class UC_Menu_Manager_Palmares : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        HistorialLogica _logicaHistorial = new HistorialLogica();

        public UC_Menu_Manager_Palmares(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // ELEMENTOS DEL PALMARÉS
            List<PalmaresManager> palmares = _logicaPalmares.MostrarPalmaresManager(_equipo, _manager.IdManager);
            int contador = 0;

            foreach (var copa in palmares)
            {
                contador++;
            }
            txtPalmares.Text += " (" + contador + ")";

            // Recorrer la lista de premios
            foreach (var copa in palmares)
            {
                // Comprobar que la Competición tenga trofeo.
                if (copa.IdCompeticion <= 9)
                {
                    // Crear el Border que contendrá el Grid
                    Border border = new Border
                    {
                        //BorderBrush = new SolidColorBrush(Colors.Black),
                        BorderThickness = new Thickness(0),
                        Margin = new Thickness(2),
                        Padding = new Thickness(0),
                        VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Height = 190,
                        Width = 200
                    };

                    // Crear el Grid con 1 columna y 2 filas
                    Grid grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) }); // Fila 1 (Imagen del trofeo)
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });  // Fila 2 (Competicion)
                    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(35) });  // Fila 3 (Temporada)

                    // Crear la imagen del trofeo en la fila 0
                    Image imagenTrofeo = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/trofeos/" + copa.IdCompeticion + ".png")),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Height = 100, // Ajusta el tamaño según sea necesario
                        Width = 100,  // Ajusta el tamaño según sea necesario
                        Stretch = Stretch.Uniform
                    };

                    Grid.SetRow(imagenTrofeo, 0); // Colocamos la imagen en la primera fila
                    grid.Children.Add(imagenTrofeo);

                    // Crear el texto de la competicion
                    string textoComp;
                    switch (copa.IdCompeticion)
                    {
                        case 1:
                            textoComp = "Liga de Campeones";
                            break;
                        default:
                            textoComp = "Amistoso";
                            break;
                    }
                    TextBlock textoCompeticion = new TextBlock
                    {
                        Text = textoComp,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        FontFamily = new FontFamily("Cascadia Code Light"),
                        FontSize = 14
                    };

                    Grid.SetRow(textoCompeticion, 1); // Colocamos el texto en la segunda fila
                    grid.Children.Add(textoCompeticion);

                    // Crear el texto de la temporada
                    TextBlock textoTemporada = new TextBlock
                    {
                        Text = copa.Temporada,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        FontWeight = FontWeights.Bold,
                        FontFamily = new FontFamily("Cascadia Code Light"),
                        FontSize = 18
                    };

                    Grid.SetRow(textoTemporada, 2); // Colocamos el texto en la segunda fila
                    grid.Children.Add(textoTemporada);

                    // Agregar el Grid al Border
                    border.Child = grid;

                    // Agregar el Border al WrapPanel
                    panelPalmares.Children.Add(border);
                }
            }

            // ELEMENTOS DEL HISTORIAL DEL MÁNAGER
            // Cargar el DataGrid
            ConfigurarDataGridHistorial();
            CargarTotales();
        }

        #region "Métodos"
        // --------------------------------------------------------------------------------- Método que configura el DataGrid.
        private void ConfigurarDataGridHistorial()
        {
            dgHistorial.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgHistorial.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgHistorial.Columns.Clear(); // Limpiar cualquier columna previa

            List<Historial> historial = _logicaHistorial.MostrarHistorialManager(_manager.IdManager);
            dgHistorial.ItemsSource = historial;

            // Configurar columnas específicas
            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Temporada"),
                Header = "TEMPORADA",
                Width = new DataGridLength(200, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipo"),
                Header = "EQUIPO",
                Width = new DataGridLength(450, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PosicionLiga"),
                Header = "POS",
                Width = new DataGridLength(60, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PartidosJugados"),
                Header = "PJ",
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

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PartidosGanados"),
                Header = "PG",
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

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PartidosEmpatados"),
                Header = "PE",
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

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PartidosPerdidos"),
                Header = "PP",
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

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("GolesMarcados"),
                Header = "GF",
                Width = new DataGridLength(90, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("GolesRecibidos"),
                Header = "GC",
                Width = new DataGridLength(90, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("TitulosInternacionales"),
                Header = "TITULOS",
                Width = new DataGridLength(180, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("CDirectiva"),
                Header = "cDIRE",
                Width = new DataGridLength(110, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("CFans"),
                Header = "cFANS",
                Width = new DataGridLength(110, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });
            dgHistorial.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("CJugadores"),
                Header = "cJUGA",
                Width = new DataGridLength(110, DataGridLengthUnitType.Pixel),
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
            dgHistorial.RowHeight = 35;
            dgHistorial.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgHistorial.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgHistorial.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgHistorial.BorderThickness = new Thickness(0);
            dgHistorial.CanUserAddRows = false;
            dgHistorial.CanUserResizeColumns = false;
            dgHistorial.CanUserResizeRows = false;
            dgHistorial.SelectionMode = DataGridSelectionMode.Single;
            dgHistorial.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgHistorial.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgHistorial.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            // Estilo general de las cabeceras
            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6c9aa5"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 14.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgHistorial.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgHistorial.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Transparent)),
                    new Setter(Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(35, 40, 45))),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code Light")),
                    new Setter(Control.FontSizeProperty, 18.0),
                    new Setter(Control.PaddingProperty, new Thickness(5)),
                    new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent),
                    new Setter(DataGridCell.BorderThicknessProperty, new Thickness(0))
                }
            };
        }

        private void CargarTotales()
        {
            List<Historial> historial = _logicaHistorial.MostrarHistorialManager(_manager.IdManager);

            int pj = 0;
            int pg = 0;
            int pe = 0;
            int pp = 0;
            int gf = 0;
            int gc = 0;
            int tn = 0;
            int ti = 0;

            foreach (var valor in historial)
            {
                pj += valor.PartidosJugados;
                pg += valor.PartidosGanados;
                pe += valor.PartidosEmpatados;
                pp += valor.PartidosPerdidos;
                gf += valor.GolesMarcados;
                gc += valor.GolesRecibidos;
                ti += valor.TitulosInternacionales;
            }

            txtPJtotales.Text = pj.ToString("N0");
            txtPGtotales.Text = pg.ToString("N0");
            txtPEtotales.Text = pe.ToString("N0");
            txtPPtotales.Text = pp.ToString("N0");
            txtGFtotales.Text = gf.ToString("N0");
            txtGCtotales.Text = gc.ToString("N0");
            txtInternacionalesTotales.Text = ti.ToString("N0");
        }
        #endregion
    }
}
