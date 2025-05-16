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
    public partial class UC_Menu_Transferencias_BuscarPorEquipo : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Transferencias_BuscarPorEquipo(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();

            // Cargar los escudos de primera división.
            CargarEscudos(1);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo1 = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLiga1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo1));
            string ruta_logo2 = _logicaCompeticion.ObtenerCompeticion(2).RutaImagen80;
            imgLiga2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo2));
            string ruta_logo3 = _logicaCompeticion.ObtenerCompeticion(3).RutaImagen80;
            imgLiga3.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo3));
            string ruta_logo4 = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
            imgLiga4.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo4));
        }

        // --------------------------------------------- EVENTO CLICK DEL BOTON DIVISIÓN 1 -------------------------------------------------
        private void imgLiga1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            imgLiga1.IsEnabled = false;
            imgLiga2.IsEnabled = true;
            imgLiga3.IsEnabled = true;
            imgLiga4.IsEnabled = true;
            imgLiga5.IsEnabled = true;

            dgJugadores.ItemsSource = null;
            CargarEscudos(1);
        }

        // --------------------------------------------- EVENTO CLICK DEL BOTON DIVISIÓN 2 -------------------------------------------------
        private void imgLiga2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            imgLiga1.IsEnabled = true;
            imgLiga2.IsEnabled = false;
            imgLiga3.IsEnabled = true;
            imgLiga4.IsEnabled = true;
            imgLiga5.IsEnabled = true;

            dgJugadores.ItemsSource = null;
            CargarEscudos(2);
        }

        // --------------------------------------------- EVENTO CLICK DEL BOTON DIVISIÓN 3 -------------------------------------------------
        private void imgLiga3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            imgLiga1.IsEnabled = true;
            imgLiga2.IsEnabled = true;
            imgLiga3.IsEnabled = false;
            imgLiga4.IsEnabled = true;
            imgLiga5.IsEnabled = true;

            dgJugadores.ItemsSource = null;
            CargarEscudos(3);
        }

        // --------------------------------------------- EVENTO CLICK DEL BOTON DIVISIÓN 4 -------------------------------------------------
        private void imgLiga4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            imgLiga1.IsEnabled = true;
            imgLiga2.IsEnabled = true;
            imgLiga3.IsEnabled = true;
            imgLiga4.IsEnabled = false;
            imgLiga5.IsEnabled = true;

            dgJugadores.ItemsSource = null;
            CargarEscudos(5);
        }

        // --------------------------------------------- EVENTO CLICK DEL BOTON FREE AGENTS -------------------------------------------------
        private void imgLiga5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            imgLiga1.IsEnabled = true;
            imgLiga2.IsEnabled = true;
            imgLiga3.IsEnabled = true;
            imgLiga4.IsEnabled = true;
            imgLiga5.IsEnabled = false;

            wrapPanelEquipos.Children.Clear();
            dgJugadoresTexto.Visibility = Visibility.Hidden;
            dgJugadores.Visibility = Visibility.Visible;
            txtListadoJugadores.Text = "LISTADO DE JUGADORES SIN EQUIPO";
            ConfigurarDataGrid(0);
        }

        #region "Métodos"
        private void CargarEscudos(int competicion)
        {
            // Limpiar el WrapPanel por si ya tiene escudos
            wrapPanelEquipos.Children.Clear();

            // Recogemos la lista de equipo.
            List<Equipo> oEquipos = new List<Equipo>();
            oEquipos = _logicaEquipo.ListarEquiposCompeticion(competicion);

            // Variable para rastrear el Border seleccionado
            Border? contenedorSeleccionado = null;

            // Recorrer los datos para crear las imágenes
            foreach (var equipo in oEquipos)
            {
                if (equipo.IdEquipo != _equipo)
                {
                    // Ruta de la imagen
                    string imagePath = $"{GestorPartidas.RutaMisDocumentos}/{equipo.RutaImagen64}";

                    // Crear la imagen
                    Image img = new Image
                    {
                        Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
                        Width = 64,
                        Height = 64,
                        Cursor = Cursors.Hand // Cursor de mano
                    };

                    // Crear el contenedor Border
                    Border contenedor = new Border
                    {
                        Child = img,
                        Width = 70, // Ajustado al ItemWidth del WrapPanel
                        Height = 70, // Ajustado al ItemHeight del WrapPanel
                        Margin = new Thickness(0),
                        BorderThickness = new Thickness(0),
                        BorderBrush = Brushes.Transparent,
                        Tag = equipo.IdEquipo // Guardar el IdEquipo en Tag
                    };

                    // Asignar un evento de clic al Border
                    contenedor.MouseLeftButtonDown += (s, e) =>
                    {
                        Metodos.ReproducirSonidoClick();

                        // Eliminar el borde verde del contenedor previamente seleccionado, si existe
                        if (contenedorSeleccionado is not null)
                        {
                            contenedorSeleccionado.BorderThickness = new Thickness(0);
                            contenedorSeleccionado.BorderBrush = Brushes.Transparent;
                            contenedorSeleccionado.Background = Brushes.Transparent;
                        }

                        // Establecer borde verde en el contenedor actual
                        contenedorSeleccionado = contenedor;
                        contenedor.BorderThickness = new Thickness(2);
                        contenedor.BorderBrush = new BrushConverter().ConvertFromString("#9b8b5a") as Brush;
                        contenedor.Background = new BrushConverter().ConvertFromString("#CCCCCC") as Brush;

                        ConfigurarDataGrid(equipo.IdEquipo);
                        dgJugadoresTexto.Visibility = Visibility.Hidden;
                        dgJugadores.Visibility = Visibility.Visible;
                        txtListadoJugadores.Text = " LISTADO DE JUGADORES DEL " + _logicaEquipo.ListarDetallesEquipo(equipo.IdEquipo).Nombre.ToUpper();
                    };

                    // Agregar el contenedor al WrapPanel
                    wrapPanelEquipos.Children.Add(contenedor);
                }
            }
        }

        private void ConfigurarDataGrid(int equipoBusqueda)
        {
            dgJugadores.SelectionChanged -= dgJugadores_SelectionChanged;

            dgJugadores.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgJugadores.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgJugadores.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.ListadoJugadoresCompleto(equipoBusqueda);
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
            imageFactory.SetBinding(Image.SourceProperty, new Binding("Nacionalidad")
            {
                Converter = new NacionalidadToFlagConverter() // El convertidor convierte la nacionalidad a la imagen de la bandera
            });

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

                    // Crear el nuevo UserControl y agregarlo al DockPanel (Opcion 4: Busqueda por Equipo)
                    UC_FichaJugador fichaJugador = new UC_FichaJugador(jugadorSeleccionado.IdJugador, _equipo, _manager, 4, pantallaPrincipal);
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
