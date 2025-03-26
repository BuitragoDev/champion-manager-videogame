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

        private int jugadorMarcado = 0;
        private int idJugadorUno = 0;
        private int idJugadorDos = 0;
        private int posicionUno = 0;
        private int posicionDos = 0;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_Menu_Entrenador_Alineacion(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void alineacion_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGridTitulares();
            ConfigurarDataGridReservas();
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
                Width = new DataGridLength(375, DataGridLengthUnitType.Pixel),
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
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
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
                Width = new DataGridLength(375, DataGridLengthUnitType.Pixel),
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
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
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
            Metodos.ReproducirSonidoClick();

            if (dgTitulares.SelectedItem is Jugador jugadorSeleccionado)
            {
                if (jugadorMarcado == 0)
                {
                    idJugadorUno = jugadorSeleccionado.IdJugador;
                    posicionUno = jugadorSeleccionado.PosicionAlineacion;
                    jugadorMarcado = 1;
                }
                else
                {
                    idJugadorDos = jugadorSeleccionado.IdJugador;
                    posicionDos = jugadorSeleccionado.PosicionAlineacion;
                    jugadorMarcado = 0;

                    // Actualizamos posiciones en la Base de Datos
                    _logicaJugador.IntercambioPosicion(idJugadorUno, idJugadorDos, posicionUno, posicionDos);

                    // Recargar los DataGrid
                    ConfigurarDataGridTitulares();
                    ConfigurarDataGridReservas();
                }
            }
            
        }

        private void dgReservas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (dgReservas.SelectedItem is Jugador jugadorSeleccionado)
            {
                if (jugadorMarcado == 0)
                {
                    idJugadorUno = jugadorSeleccionado.IdJugador;
                    posicionUno = jugadorSeleccionado.PosicionAlineacion;
                    jugadorMarcado = 1;
                }
                else
                {
                    idJugadorDos = jugadorSeleccionado.IdJugador;
                    posicionDos = jugadorSeleccionado.PosicionAlineacion;
                    jugadorMarcado = 0;

                    // Actualizamos posiciones en la Base de Datos
                    _logicaJugador.IntercambioPosicion(idJugadorUno, idJugadorDos, posicionUno, posicionDos);

                    // Recargar los DataGrid
                    ConfigurarDataGridTitulares();
                    ConfigurarDataGridReservas();
                }
            }
        }
        #endregion
    }
}
