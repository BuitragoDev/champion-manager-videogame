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
    public partial class UC_Menu_Transferencias_BuscarPorFiltro : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Transferencias_BuscarPorFiltro(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar los ComboBox
            CargarNacionalidades();

            List<Competicion> competiciones = _logicaCompeticion.MostrarCompeticiones();
            cbCompeticion.ItemsSource = competiciones;
            cbCompeticion.DisplayMemberPath = "Nombre";
            cbCompeticion.SelectedValuePath = "IdCompeticion";

            ComboBox[] comboBoxes = new ComboBox[]
            {
                cbEdadMin, cbEdadMax,
                cbMediaMin, cbMediaMax,
                cbCalidadMin, cbCalidadMax,
                cbVelocidadMin, cbVelocidadMax,
                cbResistenciaMin, cbResistenciaMax,
                cbAgresividadMin, cbAgresividadMax
            };

            // Llenar del 1 al 99
            for (int i = 1; i <= 99; i++)
            {
                foreach (ComboBox cb in comboBoxes)
                {
                    cb.Items.Add(i);
                }
            }

            // Establecer valores por defecto
            foreach (ComboBox cb in comboBoxes)
            {
                if (cb.Name.EndsWith("Min"))
                    cb.SelectedItem = 1;
                else if (cb.Name.EndsWith("Max"))
                    cb.SelectedItem = 99;
            }
        }

        // ----------------------------------------------------------------------------- Eventos de los CHECKBOX
        private void cb1_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbNacionalidad.IsEnabled = true;
        }

        private void cb1_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbNacionalidad.IsEnabled = false;
            cbNacionalidad.SelectedIndex = -1; // Dejar vacío el ComboBox
        }

        private void cb2_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbCompeticion.IsEnabled = true;
        }

        private void cb2_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbCompeticion.IsEnabled = false;
            cbCompeticion.SelectedIndex = -1;
        }

        private void cb3_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbPosicion.IsEnabled = true;
        }

        private void cb3_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbPosicion.IsEnabled = false;
            cbPosicion.SelectedIndex = -1;
        }

        private void cb4_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbEdadMin.IsEnabled = true;
            cbEdadMax.IsEnabled = true;
        }

        private void cb4_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbEdadMin.IsEnabled = false;
            cbEdadMax.IsEnabled = false;
            cbEdadMin.SelectedIndex = 0;
            cbEdadMax.SelectedIndex = 99;
        }

        private void cb5_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbMediaMin.IsEnabled = true;
            cbMediaMax.IsEnabled = true;
        }

        private void cb5_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbMediaMin.IsEnabled = false;
            cbMediaMax.IsEnabled = false;
            cbMediaMin.SelectedIndex = 0;
            cbMediaMax.SelectedIndex = 99;
        }

        private void cb6_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbCalidadMin.IsEnabled = true;
            cbCalidadMax.IsEnabled = true;
        }

        private void cb6_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbCalidadMin.IsEnabled = false;
            cbCalidadMax.IsEnabled = false;
            cbCalidadMin.SelectedIndex = 0;
            cbCalidadMax.SelectedIndex = 99;
        }

        private void cb7_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbVelocidadMin.IsEnabled = true;
            cbVelocidadMax.IsEnabled = true;
        }

        private void cb7_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbVelocidadMin.IsEnabled = false;
            cbVelocidadMax.IsEnabled = false;
            cbVelocidadMin.SelectedIndex = 0;
            cbVelocidadMax.SelectedIndex = 99;
        }

        private void cb8_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbResistenciaMin.IsEnabled = true;
            cbResistenciaMax.IsEnabled = true;
        }

        private void cb8_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbResistenciaMin.IsEnabled = false;
            cbResistenciaMax.IsEnabled = false;
            cbResistenciaMin.SelectedIndex = 0;
            cbResistenciaMax.SelectedIndex = 99;
        }

        private void cb9_Checked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbAgresividadMin.IsEnabled = true;
            cbAgresividadMax.IsEnabled = true;
        }

        private void cb9_Unchecked(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            cbAgresividadMin.IsEnabled = false;
            cbAgresividadMax.IsEnabled = false;
            cbAgresividadMin.SelectedIndex = 0;
            cbAgresividadMax.SelectedIndex = 99;
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton BUSCAR
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string nacionalidad = cbNacionalidad.Text;
            int competicion = cbCompeticion.SelectedValue != null
                                ? (int)cbCompeticion.SelectedValue
                                : 0; 
            string posicion = cbPosicion.Text;
            int edadMin = (int)cbEdadMin.SelectedItem;
            int edadMax = (int)cbEdadMax.SelectedItem;
            int mediaMin = (int)cbMediaMin.SelectedItem;
            int mediaMax = (int)cbMediaMax.SelectedItem;
            int calidadMin = (int)cbCalidadMin.SelectedItem;
            int calidadMax = (int)cbCalidadMax.SelectedItem;
            int velocidadMin = (int)cbVelocidadMin.SelectedItem;
            int velocidadMax = (int)cbVelocidadMax.SelectedItem;
            int resistenciaMin = (int)cbResistenciaMin.SelectedItem;
            int resistenciaMax = (int)cbResistenciaMax.SelectedItem;
            int agresividadMin = (int)cbAgresividadMin.SelectedItem;
            int agresividadMax = (int)cbAgresividadMax.SelectedItem;


            ConfigurarDataGrid(nacionalidad, competicion, posicion, edadMin, edadMax, mediaMin, mediaMax, calidadMin, calidadMax, 
                               velocidadMin, velocidadMax, resistenciaMin, resistenciaMax, agresividadMin, agresividadMax);
            dgJugadoresTexto.Visibility = Visibility.Hidden;
            dgJugadores.Visibility = Visibility.Visible;
        }

        #region "Metodos"
        private void CargarNacionalidades()
        {
            // Obtener todas las nacionalidades del método ObtenerCodigoBanderas
            var nacionalidades = ObtenerTodasLasNacionalidades();

            // Asignar al ComboBox
            cbNacionalidad.ItemsSource = nacionalidades;
        }

        private List<string> ObtenerTodasLasNacionalidades()
        {
            // Extraer todas las nacionalidades recorriendo las claves del switch
            return new List<string>
            {
                "", "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita",
                "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Bangladesh",
                "Barbados", "Baréin", "Bélgica", "Belice", "Benín", "Bielorrusia", "Birmania", "Bolivia",
                "Bosnia y Herzegovina", "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi",
                "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre",
                "Colombia", "Comoras", "Corea del Norte",
                "Corea del Sur", "Costa Rica", "Costa de Marfil", "Croacia", "Cuba", "Curazao", "Dinamarca",
                "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eritrea", "Escocia",
                "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiji",
                "Filipinas", "Finlandia", "Francia", "Gabón", "Gales", "Gambia", "Georgia", "Ghana", "Granada", "Grecia",
                "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "India", "Indonesia",
                "Inglaterra", "Irak", "Irán", "Irlanda", "Islandia", "Islas Feroe", "Islas Marshall", "Islas Salomón",
                "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati",
                "Kosovo", "Kuwait", "Laos", "Lesoto", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein",
                "Lituania", "Luxemburgo", "Macedonia del Norte", "Madagascar", "Malasia", "Malawi", "Maldivas",
                "Mali", "Malta", "Moldavia", "Marruecos", "Martinica", "Mauricio", "Mauritania", "México",
                "Micronesia", "Mónaco", "Mongolia", "Montenegro", "Mozambique", "Namibia", "Nauru", "Nepal",
                "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Países Bajos", "Pakistán",
                "Palaos", "Panamá", "Papúa Nueva Guinea", "Paraguay", "Perú", "Polonia", "Portugal",
                "Reino Unido", "República Checa", "República del Congo", "República Dominicana", "Ruanda",
                "Rumanía", "Rusia", "Samoa", "San Cristóbal y Nieves", "San Marino", "Santo Tomé y Príncipe",
                "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sri Lanka",
                "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Surinam", "Sáhara Occidental",
                "Tailandia", "Tayikistán", "Tanzania", "Togo", "Tonga", "Trinidad y Tobago", "Túnez",
                "Turkmenistán", "Turquía", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu",
                "Vaticano", "Venezuela", "Vietnam", "Yemen", "Yibuti", "Zambia", "Zimbabue"
            };
        }

        private void ConfigurarDataGrid(string nacionalidad, int competicion, string posicion, int edadMin, int edadMax, int mediaMin, int mediaMax, int calidadMin, int calidadMax,
                                        int velocidadMin, int velocidadMax, int resistenciaMin, int resistenciaMax, int agresividadMin, int agresividadMax)
        {
            dgJugadores.SelectionChanged -= dgJugadores_SelectionChanged;

            dgJugadores.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgJugadores.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgJugadores.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.ListadoJugadoresPorFiltro(_equipo, nacionalidad, competicion, posicion, edadMin, edadMax, mediaMin, mediaMax, calidadMin, calidadMax,
                                                                             velocidadMin, velocidadMax, resistenciaMin, resistenciaMax, agresividadMin, agresividadMax);
            dgJugadores.ItemsSource = jugador;

            dgJugadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("IdJugador"),
                Header = "IdJugador",
                Visibility = Visibility.Collapsed // Ocultar la columna
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
            imageFactory.SetBinding(Image.SourceProperty, new Binding("IdEquipo")
            {
                Converter = new IdEquipoToEscudoBuscadorConverter()
            });

            imageFactory.SetValue(Image.WidthProperty, 20.0);
            imageFactory.SetValue(Image.HeightProperty, 20.0);
            imageFactory.SetValue(Image.StretchProperty, Stretch.Uniform);

            // Asignar el 'FrameworkElementFactory' al DataTemplate
            template.VisualTree = imageFactory;

            // Asignar el DataTemplate a la columna
            banderaColumna.CellTemplate = template;

            // Finalmente, agregar la columna al DataGrid
            dgJugadores.Columns.Add(banderaColumna);

            dgJugadores.Columns.Add(new DataGridTextColumn
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
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Crear una instancia del convertidor
            var posicionConverter = new PosicionToStringConverter();

            dgJugadores.Columns.Add(new DataGridTextColumn
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


            dgJugadores.Columns.Add(new DataGridTextColumn
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


            dgJugadores.Columns.Add(new DataGridTextColumn
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

            dgJugadores.Columns.Add(new DataGridTextColumn
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

            dgJugadores.Columns.Add(new DataGridTextColumn
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

            dgJugadores.Columns.Add(new DataGridTextColumn
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

            dgJugadores.SelectionChanged += dgJugadores_SelectionChanged;

            // Configurar altura de filas y estilos generales
            dgJugadores.RowHeight = 35;
            dgJugadores.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgJugadores.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgJugadores.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgJugadores.BorderThickness = new Thickness(0);
            dgJugadores.CanUserAddRows = false;
            dgJugadores.CanUserResizeColumns = false;
            dgJugadores.CanUserResizeRows = false;
            dgJugadores.SelectionMode = DataGridSelectionMode.Single;
            dgJugadores.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgJugadores.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgJugadores.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

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

            dgJugadores.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgJugadores.CellStyle = new Style(typeof(DataGridCell))
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

        private void dgJugadores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Obtener el DockPanel padre
            DockPanel? parentDockPanel = this.Parent as DockPanel;

            if (dgJugadores.SelectedItem == null)
                return; // Evita errores si no hay selección

            // Buscar el UserControl UC_PantallaPrincipal en la ventana principal
            UC_PantallaPrincipal pantallaPrincipal = FindParentUC_PantallaPrincipal(this);

            if (pantallaPrincipal == null)
            {
                MessageBox.Show("No se pudo encontrar UC_PantallaPrincipal.");
                return;
            }

            // Verifica el tipo de objeto de la fila seleccionada
            if (dgJugadores.SelectedItem is Jugador jugadorSeleccionado)
            {
                if (parentDockPanel != null)
                {
                    // Limpiar el contenido actual
                    parentDockPanel.Children.Clear();

                    // Crear el nuevo UserControl y agregarlo al DockPanel (Opcion 4: Busqueda por Filtro)
                    UC_FichaJugador fichaJugador = new UC_FichaJugador(jugadorSeleccionado.IdJugador, _equipo, _manager, 5, pantallaPrincipal);
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
