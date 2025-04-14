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
    public partial class UC_Menu_Entrenador_Alineacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        string tactica;

        private int jugadorMarcado = 0;
        private int idJugadorUno = 0;
        private int idJugadorDos = 0;
        private int posicionUno = 0;
        private int posicionDos = 0;

        private bool primerJugadorSeleccionado = false; // Controla si ya se ha marcado un primer jugador
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        ManagerLogica _logicaManager = new ManagerLogica();

        

        public UC_Menu_Entrenador_Alineacion(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        private void alineacion_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGridTitulares();
            ConfigurarDataGridReservas();
        }
        
        // --------------------------------------------------------------------------------- Evento CLICK del boton 5-4-1
        private void btn541_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btn532.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn451.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn442.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn433.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn352.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));

            btn541.IsEnabled = false;
            btn532.IsEnabled = true;
            btn451.IsEnabled = true;
            btn442.IsEnabled = true;
            btn433.IsEnabled = true;
            btn352.IsEnabled = true;

            _logicaManager.CambiarTactica(_manager.IdManager, "5-4-1");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton 5-3-2
        private void btn532_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn532.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btn451.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn442.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn433.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn352.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));

            btn541.IsEnabled = true;
            btn532.IsEnabled = false;
            btn451.IsEnabled = true;
            btn442.IsEnabled = true;
            btn433.IsEnabled = true;
            btn352.IsEnabled = true;

            _logicaManager.CambiarTactica(_manager.IdManager, "5-3-2");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton 4-5-1
        private void btn451_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn532.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn451.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btn442.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn433.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn352.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));

            btn541.IsEnabled = true;
            btn532.IsEnabled = true;
            btn451.IsEnabled = false;
            btn442.IsEnabled = true;
            btn433.IsEnabled = true;
            btn352.IsEnabled = true;

            _logicaManager.CambiarTactica(_manager.IdManager, "4-5-1");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton 4-2-2
        private void btn442_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn532.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn451.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn442.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btn433.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn352.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));

            btn541.IsEnabled = true;
            btn532.IsEnabled = true;
            btn451.IsEnabled = true;
            btn442.IsEnabled = false;
            btn433.IsEnabled = true;
            btn352.IsEnabled = true;

            _logicaManager.CambiarTactica(_manager.IdManager, "4-4-2");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton 4-3-3
        private void btn433_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn532.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn451.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn442.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn433.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));
            btn352.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));

            btn541.IsEnabled = true;
            btn532.IsEnabled = true;
            btn451.IsEnabled = true;
            btn442.IsEnabled = true;
            btn433.IsEnabled = false;
            btn352.IsEnabled = true;

            _logicaManager.CambiarTactica(_manager.IdManager, "4-4-3");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton 3-5-2
        private void btn352_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btn541.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn532.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn451.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn442.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn433.Background = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            btn352.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            btn541.IsEnabled = true;
            btn532.IsEnabled = true;
            btn451.IsEnabled = true;
            btn442.IsEnabled = true;
            btn433.IsEnabled = true;
            btn352.IsEnabled = false;

            _logicaManager.CambiarTactica(_manager.IdManager, "3-5-2");
            tactica = _logicaManager.MostrarManager(_manager.IdManager).Tactica;
        }

        #region "Métodos"
        // --------------------------------------------------------------------------------- Método que configura el DataGrid.
        private void ConfigurarDataGridTitulares()
        {
            dgTitulares.SelectionChanged -= dgTitulares_SelectionChanged;

            dgTitulares.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgTitulares.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgTitulares.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.MostrarAlineacion(1, 11);
            dgTitulares.ItemsSource = jugador;

            // Columnas Ocultas
            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("IdJugador"),
                Header = "IdJugador",
                Visibility = Visibility.Collapsed // Ocultar la columna
            });

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PosicionAlineacion"),
                Header = "Posicion",
                Visibility = Visibility.Collapsed // Ocultar la columna
            });

            // Configurar columnas específicas
            dgTitulares.Columns.Add(new DataGridTextColumn
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
            dgTitulares.Columns.Add(banderaColumna);

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "JUGADOR",
                Width = new DataGridLength(325, DataGridLengthUnitType.Pixel),
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

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Rol"),
                Header = "DEMARCACIÓN",
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

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Media"), // Esta es la propiedad que queremos mostrar
                Header = "MED",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
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

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Moral"),
                Header = "MO",
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

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("EstadoForma"),
                Header = "EF",
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

            // Lesion
            var templateColumn = new DataGridTemplateColumn
            {
                Header = "LES",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel)
            };

            var factory = new FrameworkElementFactory(typeof(Image));
            factory.SetValue(Image.WidthProperty, 16.0);
            factory.SetValue(Image.HeightProperty, 16.0);
            factory.SetBinding(Image.SourceProperty, new Binding("Lesion")
            {
                Converter = new LesionToImageConverter()
            });
            factory.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            factory.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            templateColumn.CellTemplate = new DataTemplate { VisualTree = factory };

            dgTitulares.Columns.Add(templateColumn);

            // Sancion
            var templateColumnSancion = new DataGridTemplateColumn
            {
                Header = "SAN",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel)
            };

            var factorySancion = new FrameworkElementFactory(typeof(Image));
            factorySancion.SetValue(Image.WidthProperty, 16.0);
            factorySancion.SetValue(Image.HeightProperty, 16.0);
            factorySancion.SetBinding(Image.SourceProperty, new Binding("Sancionado")
            {
                Converter = new SancionToImageConverter()
            });
            factorySancion.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            factorySancion.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            templateColumnSancion.CellTemplate = new DataTemplate { VisualTree = factorySancion };

            dgTitulares.Columns.Add(templateColumnSancion);


            dgTitulares.SelectionChanged += dgTitulares_SelectionChanged;

            // Configurar altura de filas y estilos generales
            dgTitulares.RowHeight = 35;
            dgTitulares.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgTitulares.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgTitulares.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgTitulares.BorderThickness = new Thickness(0);
            dgTitulares.CanUserAddRows = false;
            dgTitulares.CanUserResizeColumns = false;
            dgTitulares.CanUserResizeRows = false;
            dgTitulares.SelectionMode = DataGridSelectionMode.Single;
            dgTitulares.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgTitulares.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgTitulares.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgTitulares.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgTitulares.CellStyle = new Style(typeof(DataGridCell))
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

        private void ConfigurarDataGridReservas()
        {
            dgReservas.SelectionChanged -= dgReservas_SelectionChanged;

            dgReservas.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgReservas.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgReservas.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.MostrarAlineacion(12, 40);
            dgReservas.ItemsSource = jugador;

            // Columnas Ocultas
            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("IdJugador"),
                Header = "IdJugador",
                Visibility = Visibility.Collapsed // Ocultar la columna
            });

            dgTitulares.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("PosicionAlineacion"),
                Header = "Posicion",
                Visibility = Visibility.Collapsed // Ocultar la columna
            });

            // Configurar columnas específicas
            dgReservas.Columns.Add(new DataGridTextColumn
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
            dgReservas.Columns.Add(banderaColumna);

            dgReservas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "JUGADOR",
                Width = new DataGridLength(325, DataGridLengthUnitType.Pixel),
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

            dgReservas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Rol"),
                Header = "DEMARCACIÓN",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgReservas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Media"), // Esta es la propiedad que queremos mostrar
                Header = "MED",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
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

            dgReservas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Moral"),
                Header = "MO",
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

            dgReservas.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("EstadoForma"),
                Header = "EF",
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

            // Lesion
            var templateColumn = new DataGridTemplateColumn
            {
                Header = "LES",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel)
            };

            var factory = new FrameworkElementFactory(typeof(Image));
            factory.SetValue(Image.WidthProperty, 16.0);
            factory.SetValue(Image.HeightProperty, 16.0);
            factory.SetBinding(Image.SourceProperty, new Binding("Lesion")
            {
                Converter = new LesionToImageConverter()
            });
            factory.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            factory.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            templateColumn.CellTemplate = new DataTemplate { VisualTree = factory };

            dgReservas.Columns.Add(templateColumn);

            // Sancion
            var templateColumnSancion = new DataGridTemplateColumn
            {
                Header = "SAN",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel)
            };

            var factorySancion = new FrameworkElementFactory(typeof(Image));
            factorySancion.SetValue(Image.WidthProperty, 16.0);
            factorySancion.SetValue(Image.HeightProperty, 16.0);
            factorySancion.SetBinding(Image.SourceProperty, new Binding("Sancionado")
            {
                Converter = new SancionToImageConverter()
            });
            factorySancion.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            factorySancion.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);

            templateColumnSancion.CellTemplate = new DataTemplate { VisualTree = factorySancion };

            dgReservas.Columns.Add(templateColumnSancion);


            dgReservas.SelectionChanged += dgReservas_SelectionChanged;

            // Configurar altura de filas y estilos generales
            dgReservas.RowHeight = 35;
            dgReservas.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgReservas.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgReservas.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgReservas.BorderThickness = new Thickness(0);
            dgReservas.CanUserAddRows = false;
            dgReservas.CanUserResizeColumns = false;
            dgReservas.CanUserResizeRows = false;
            dgReservas.SelectionMode = DataGridSelectionMode.Single;
            dgReservas.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgReservas.HeadersVisibility = DataGridHeadersVisibility.None; // Mostrar cabeceras
            dgReservas.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgReservas.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgReservas.CellStyle = new Style(typeof(DataGridCell))
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

        private void dgTitulares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeleccionarJugador(dgTitulares.SelectedItem as Jugador);
        }

        private void dgReservas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SeleccionarJugador(dgReservas.SelectedItem as Jugador);
        }

        private void SeleccionarJugador(Jugador jugadorSeleccionado)
        {
            if (jugadorSeleccionado == null) return;

            Metodos.ReproducirSonidoClick();

            if (!primerJugadorSeleccionado)
            {
                // Primer jugador seleccionado
                idJugadorUno = jugadorSeleccionado.IdJugador;
                posicionUno = jugadorSeleccionado.PosicionAlineacion;
                jugadorMarcado = 1;
                primerJugadorSeleccionado = true;
            }
            else
            {
                // Segundo jugador seleccionado
                idJugadorDos = jugadorSeleccionado.IdJugador;
                posicionDos = jugadorSeleccionado.PosicionAlineacion;
                jugadorMarcado = 0;
                primerJugadorSeleccionado = false; // Se resetea para futuras selecciones

                // Intercambiar posiciones en la base de datos
                _logicaJugador.IntercambioPosicion(idJugadorUno, idJugadorDos, posicionUno, posicionDos);

                // Recargar los DataGrid
                ConfigurarDataGridTitulares();
                ConfigurarDataGridReservas();

                // Vaciar imágenes y textos
                LimpiarSeleccion();
            }
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Eventos de hover
            e.Row.MouseEnter += Row_MouseEnter;
            e.Row.MouseLeave += Row_MouseLeave;

            // Colorear las primeras 5 filas de amarillo
            if (e.Row.GetIndex() % 2 == 0)
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ceeecb"));

            else
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bcd5ba"));

        }

        private void dg_LoadingRowReservas(object sender, DataGridRowEventArgs e)
        {
            // Eventos de hover
            e.Row.MouseEnter += Row_MouseEnter;
            e.Row.MouseLeave += Row_MouseLeave;

            // Colorear las primeras 5 filas de amarillo
            if (e.Row.GetIndex() < 5)
            {
                // Alternar color para el resto de filas (opcional)
                if (e.Row.GetIndex() % 2 == 0)
                    e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ceeecb"));
                else
                    e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bcd5ba"));
            }
            else
            {
                // Alternar color para el resto de filas (opcional)
                if (e.Row.GetIndex() % 2 == 0)
                    e.Row.Background = new SolidColorBrush(Colors.LightGray);
                else
                    e.Row.Background = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }


        private void Row_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridRow row && row.Item is Jugador jugador)
            {
                if (!primerJugadorSeleccionado)
                {
                    // Antes de seleccionar el primer jugador, mostrar en imgCambio1
                    MostrarDatosCambio1(jugador);
                }
                else
                {
                    // Después de seleccionar el primer jugador, mostrar en imgCambio2
                    MostrarDatosCambio2(jugador);
                }
            }
        }

        private void Row_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!primerJugadorSeleccionado)
            {
                // Antes de seleccionar el primer jugador, limpiar imgCambio1
                LimpiarCambio1();
            }
            else
            {
                // Después de seleccionar el primer jugador, limpiar imgCambio2
                LimpiarCambio2();
            }
        }

        private void MostrarDatosCambio1(Jugador jugador)
        {
            bordeCambio1.Background = Brushes.LightGreen;
            imgFotoCambio1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtCambio1.Text = jugador.NombreCompleto;

            imgFotoJugador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugador.Text = jugador.NombreCompleto;
            txtDemarcacionJugador.Text = jugador.Rol;
            txtPortero.Text = jugador.Portero.ToString();
            txtPase.Text = jugador.Pase.ToString();
            txtRegate.Text = jugador.Regate.ToString();
            txtRemate.Text = jugador.Remate.ToString();
            txtEntradas.Text = jugador.Entradas.ToString();
            txtTiro.Text = jugador.Tiro.ToString();
            imgDemarcacionIdeal.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/demarcaciones/" + jugador.RolId + ".png"));

            // Diccionario que mapea cada táctica con sus respectivas posiciones
            var posicionesPorTactica = new Dictionary<string, Dictionary<int, int>>
            {
                ["5-4-1"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 4 }, { 5, 2 }, { 6, 3 }, { 7, 6 }, { 8, 6 }, { 9, 11 }, { 10, 12 }, { 11, 10 } },
                ["5-3-2"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 4 }, { 5, 2 }, { 6, 3 }, { 7, 5 }, { 8, 6 }, { 9, 6 }, { 10, 10 }, { 11, 10 } },
                ["4-5-1"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 2 }, { 5, 3 }, { 6, 5 }, { 7, 6 }, { 8, 6 }, { 9, 11 }, { 10, 12 }, { 11, 10 } },
                ["4-4-2"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 2 }, { 5, 3 }, { 6, 5 }, { 7, 6 }, { 8, 6 }, { 9, 7 }, { 10, 10 }, { 11, 10 } },
                ["4-4-3"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 2 }, { 5, 3 }, { 6, 5 }, { 7, 6 }, { 8, 6 }, { 9, 8 }, { 10, 9 }, { 11, 10 } },
                ["3-5-2"] = new Dictionary<int, int> { { 1, 1 }, { 2, 4 }, { 3, 4 }, { 4, 4 }, { 5, 5 }, { 6, 6 }, { 7, 6 }, { 8, 11 }, { 9, 12 }, { 10, 10 }, { 11, 10 } }
            };

            // Obtener la posición según la táctica y la alineación del jugador
            int posicion = posicionesPorTactica.TryGetValue(tactica, out var posiciones) && posiciones.TryGetValue(jugador.PosicionAlineacion, out var pos)
                ? pos
                : 0;

            // Asignar la imagen de la demarcación
            imgDemarcacionActual.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/demarcaciones/p{posicion}.png"));

        }

        private void MostrarDatosCambio2(Jugador jugador)
        {
            bordeCambio2.Background = Brushes.IndianRed;
            imgFotoCambio2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtCambio2.Text = jugador.NombreCompleto;
        }

        private void LimpiarCambio1()
        {
            bordeCambio1.Background = Brushes.LightGray;
            imgFotoCambio1.Source = null;
            txtCambio1.Text = string.Empty;

            imgFotoJugador.Source = null;
            txtNombreJugador.Text = "";
            txtDemarcacionJugador.Text = "";
            txtPortero.Text = "";
            txtPase.Text = "";
            txtRegate.Text = "";
            txtRemate.Text = "";
            txtEntradas.Text = "";
            txtTiro.Text = "";
            imgDemarcacionIdeal.Source = null;
            imgDemarcacionActual.Source = null;
        }

        private void LimpiarCambio2()
        {
            bordeCambio2.Background = Brushes.LightGray;
            imgFotoCambio2.Source = null;
            txtCambio2.Text = string.Empty;
        }

        private void LimpiarSeleccion()
        {
            LimpiarCambio1();
            LimpiarCambio2();
        }
        #endregion
    }
}
