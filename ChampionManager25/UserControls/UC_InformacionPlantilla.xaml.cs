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
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;

namespace ChampionManager25.UserControls
{
    public partial class UC_InformacionPlantilla : UserControl
    {
        #region "Variables"
        Equipo equipo;
        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_InformacionPlantilla(Equipo eqp)
        {
            InitializeComponent();
            this.equipo = eqp;
        }

        private void informacionPlantilla_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGrid();
        }

        #region "Métodos"
        // --------------------------------------------------------------------------------- Método que configura el DataGrid.
        private void ConfigurarDataGrid()
        {
            dgPlantilla.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPlantilla.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPlantilla.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.ListadoJugadoresCompleto(equipo.IdEquipo);
            dgPlantilla.ItemsSource = jugador;

            // Configurar columnas específicas
            dgPlantilla.Columns.Add(new DataGridTextColumn
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
            dgPlantilla.Columns.Add(banderaColumna);

            dgPlantilla.Columns.Add(new DataGridTextColumn
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
                        new Setter(FontFamilyProperty, new FontFamily("Arial Rounded MT Bold")) // Fuente
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

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("RolId")
                {
                    Converter = posicionConverter // Asignamos el convertidor aquí
                },
                Header = "DEM",
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


            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("AlturaEnMetros")
                {
                    StringFormat = "0.00 'm'"  // Formato con dos decimales
                },
                Header = "ALTURA",
                Width = new DataGridLength(120, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Peso")
                {
                    StringFormat = "0 'kg'"
                },
                Header = "PESO",
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


            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Edad"),
                Header = "EDAD",
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

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Media"), // Esta es la propiedad que queremos mostrar
                Header = "MED",
                Width = new DataGridLength(70, DataGridLengthUnitType.Pixel),
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

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Moral"),
                Header = "MO",
                Width = new DataGridLength(70, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("EstadoForma"),
                Header = "EF",
                Width = new DataGridLength(70, DataGridLengthUnitType.Pixel),
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
            dgPlantilla.RowHeight = 35;
            dgPlantilla.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgPlantilla.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgPlantilla.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgPlantilla.BorderThickness = new Thickness(0);
            dgPlantilla.CanUserAddRows = false;
            dgPlantilla.CanUserResizeColumns = false;
            dgPlantilla.CanUserResizeRows = false;
            dgPlantilla.SelectionMode = DataGridSelectionMode.Single;
            dgPlantilla.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgPlantilla.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgPlantilla.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgPlantilla.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgPlantilla.CellStyle = new Style(typeof(DataGridCell))
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
