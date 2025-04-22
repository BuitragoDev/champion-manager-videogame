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
    public partial class UC_EditorJugadores : UserControl
    {
        #region "Variables"
        private int equipoSeleccionado;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_EditorJugadores(int equipo)
        {
            InitializeComponent();
            equipoSeleccionado = equipo;
        }

        private void editorJugadores_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.EstablecerConexionPartida("./championsManagerDB.db");
            string ruta_competicion_principal = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen;
            string ruta_competicion_dos = _logicaCompeticion.ObtenerCompeticion(2).RutaImagen;
            string ruta_competicion_reserva = _logicaCompeticion.ObtenerCompeticion(3).RutaImagen;
            imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_principal));
            imgCompeticion2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_dos));
            imgCompeticion3.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_reserva));

            CargarEscudos(1);
            ConfigurarDataGrid(equipoSeleccionado);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorHome();
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION PRINCIPAL
        private void imgCompeticion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            imgCompeticion.IsEnabled = false;
            imgCompeticion2.IsEnabled = true;
            imgCompeticion3.IsEnabled = true;

            CargarEscudos(1);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION 2
        private void imgCompeticionReserva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            imgCompeticion.IsEnabled = true;
            imgCompeticion2.IsEnabled = false;
            imgCompeticion3.IsEnabled = true;

            CargarEscudos(2);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION RESERVA
        private void imgCompeticion3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            imgCompeticion.IsEnabled = true;
            imgCompeticion2.IsEnabled = true;
            imgCompeticion3.IsEnabled = false;

            CargarEscudos(3);
        }

        #region "Métodos"
        private void CargarEscudos(int competicion)
        {
            wrapPanelEquipos.Children.Clear();
            List<Equipo> oEquipos = _logicaEquipo.ListarEquiposCompeticion(competicion);

            Border? contenedorSeleccionado = null;
            Border? contenedorCoincidente = null;

            foreach (var equipo in oEquipos)
            {
                string imagePath = $"{GestorPartidas.RutaMisDocumentos}/{equipo.RutaImagen120}";

                Image img = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
                    Width = 90,
                    Height = 90,
                    Cursor = Cursors.Hand
                };

                Border contenedor = new Border
                {
                    Child = img,
                    Width = 100,
                    Height = 100,
                    Margin = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    BorderBrush = Brushes.Transparent,
                    Tag = equipo.IdEquipo
                };

                contenedor.MouseLeftButtonDown += (s, e) =>
                {
                    Metodos.ReproducirSonidoClick();

                    if (contenedorSeleccionado is not null)
                    {
                        contenedorSeleccionado.BorderThickness = new Thickness(0);
                        contenedorSeleccionado.BorderBrush = Brushes.Transparent;
                        contenedorSeleccionado.Background = Brushes.Transparent;
                    }

                    contenedorSeleccionado = contenedor;
                    contenedor.BorderThickness = new Thickness(2);
                    contenedor.BorderBrush = new BrushConverter().ConvertFromString("#9b8b5a") as Brush;
                    contenedor.Background = new BrushConverter().ConvertFromString("#CCCCCC") as Brush;

                    equipoSeleccionado = (int)contenedor.Tag;
                    ConfigurarDataGrid(equipoSeleccionado);
                };

                // Verificamos si este es el equipo que debe seleccionarse al inicio
                if (equipo.IdEquipo == equipoSeleccionado)
                {
                    contenedorCoincidente = contenedor;
                }

                wrapPanelEquipos.Children.Add(contenedor);
            }

            // Si se encontró un border con el equipo seleccionado, lo marcamos
            if (contenedorCoincidente is not null)
            {
                contenedorSeleccionado = contenedorCoincidente;
                contenedorCoincidente.BorderThickness = new Thickness(2);
                contenedorCoincidente.BorderBrush = new BrushConverter().ConvertFromString("#9b8b5a") as Brush;
                contenedorCoincidente.Background = new BrushConverter().ConvertFromString("#CCCCCC") as Brush;

                // Ya está asignado desde el constructor
                ConfigurarDataGrid(equipoSeleccionado);
            }
        }


        // DATAGRID DE LA OPCION PUNTUACION.
        private void ConfigurarDataGrid(int equipo)
        {
            dgPlantilla.SelectionChanged -= DgPlantilla_SelectionChanged;

            dgPlantilla.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgPlantilla.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgPlantilla.Columns.Clear(); // Limpiar cualquier columna previa

            List<Jugador> jugador = _logicaJugador.ListadoJugadoresCompleto(equipo);
            dgPlantilla.ItemsSource = jugador;

            dgPlantilla.Columns.Add(new DataGridTextColumn
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
            dgPlantilla.Columns.Add(banderaColumna);

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "JUGADOR",
                Width = new DataGridLength(400, DataGridLengthUnitType.Pixel),
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

            dgPlantilla.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Rol"),
                Header = "DEMARCACIÓN",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
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

            dgPlantilla.SelectionChanged += DgPlantilla_SelectionChanged;

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

        private void DgPlantilla_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Verifica el tipo de objeto de la fila seleccionada
            if (dgPlantilla.SelectedItem is Jugador jugadorSeleccionado)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.CargarEditorJugador(jugadorSeleccionado.IdJugador, equipoSeleccionado);

            }
        }
        #endregion
    }
}
