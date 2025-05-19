using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
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
    public partial class UC_Menu_Competicion_Clasificacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        Equipo equipo;
        List<Equipo> equipos;
        private int numeroEquipo;
        int miCompeticion;
        #endregion

        // Instancias de la LOGICA
        PartidoLogica _logicaPartidos = new PartidoLogica();
        EquipoLogica _logicaEquipos = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Competicion_Clasificacion(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
            miCompeticion = _logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion;
            equipos = _logicaEquipos.ListarEquipos(1)
                        .Concat(_logicaEquipos.ListarEquipos(2))
                        .ToList();
            numeroEquipo = equipos.Count;
        }

        private void clasificacion_Loaded(object sender, RoutedEventArgs e)
        {
            Equipo miEquipo = _logicaEquipos.ListarDetallesEquipo(_equipo);

            // Etiqueta Clasificacion - Nombre Competicion
            lblClasificacion.Text += $" {_logicaCompeticion.MostrarNombreCompeticion(miCompeticion).ToUpper()}";

            string ruta_liga1 = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoLiga1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga1));
            string ruta_liga2 = _logicaCompeticion.ObtenerCompeticion(2).RutaImagen80;
            imgLogoLiga2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga2));

            // CARGAR DATAGRID CLASIFICACION
            ConfigurarDataGridClasificacion();
            CargarMejoresEquipos();
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton LIGA 1
        private void imgLogoLiga1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            miCompeticion = 1;
            lblClasificacion.Text = $"CLASIFICACIÓN {_logicaCompeticion.MostrarNombreCompeticion(miCompeticion).ToUpper()}";
            ConfigurarDataGridClasificacion();
            CargarMejoresEquipos();
            imgLogoLiga1.IsEnabled = false;
            imgLogoLiga2.IsEnabled = true;
        }

        // --------------------------------------------------------------------------------- Evento CLICK del boton LIGA 2
        private void imgLogoLiga2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            miCompeticion = 2;
            lblClasificacion.Text = $"CLASIFICACIÓN {_logicaCompeticion.MostrarNombreCompeticion(miCompeticion).ToUpper()}";
            ConfigurarDataGridClasificacion();
            CargarMejoresEquipos();
            imgLogoLiga2.IsEnabled = false;
            imgLogoLiga1.IsEnabled = true;
        }

        #region "Métodos"
        private void ConfigurarDataGridClasificacion()
        {
            dgClasificacion.SelectionChanged -= DgClasificacion_SelectionChanged;

            // Configuración del DataGrid
            dgClasificacion.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgClasificacion.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgClasificacion.Columns.Clear(); // Limpiar cualquier columna previa

            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 14.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(5)));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgClasificacion.ColumnHeaderStyle = columnHeaderStyle;

            List<Clasificacion> clasificaciones;

            if (miCompeticion == 1)
            {
                clasificaciones = _logicaClasificacion.MostrarClasificacion(miCompeticion, _manager.IdManager);
            }
            else
            {
                clasificaciones = _logicaClasificacion.MostrarClasificacion2(miCompeticion, _manager.IdManager);
            }
                
            ImagePathConverter.Equipos = equipos;

            // Asignar los datos al DataGrid
            dgClasificacion.ItemsSource = clasificaciones;

            // Estilo para los colores competiciones europeas, ascenso y descenso.
            if (miCompeticion == 1)
            {
                // Columna VACÍA (Antes de la posición)
                dgClasificacion.Columns.Add(new DataGridTextColumn
                {
                    Binding = new System.Windows.Data.Binding("ColumnaAuxiliar"),
                    Header = "",
                    Width = new DataGridLength(10, DataGridLengthUnitType.Pixel),
                    ElementStyle = new Style(typeof(TextBlock))
                    {
                        Setters =
                        {
                            new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                            new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                        }
                    },
                    CellStyle = new Style(typeof(DataGridCell))
                    {
                        Setters =
                        {
                            new Setter(DataGridCell.BackgroundProperty, Brushes.Transparent), // Fondo por defecto transparente
                        },
                            Triggers =
                        {
                            new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = 1,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.SteelBlue) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 2,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.SteelBlue) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 3,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.SteelBlue) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 4,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.SteelBlue) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 5,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 6,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 23,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 22,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 21,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 20,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            }
                        }
                    }
                });
            }
            else
            {
                // Columna VACÍA (Antes de la posición)
                dgClasificacion.Columns.Add(new DataGridTextColumn
                {
                    Binding = new System.Windows.Data.Binding("ColumnaAuxiliar"),
                    Header = "",
                    Width = new DataGridLength(10, DataGridLengthUnitType.Pixel),
                    ElementStyle = new Style(typeof(TextBlock))
                    {
                        Setters =
                        {
                            new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                            new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                        }
                    },
                    CellStyle = new Style(typeof(DataGridCell))
                    {
                        Setters =
                        {
                            new Setter(DataGridCell.BackgroundProperty, Brushes.Transparent), // Fondo por defecto transparente
                        },
                            Triggers =
                        {
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 1,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 2,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 3,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 4,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkGreen) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 23,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 22,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 21,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = numeroEquipo - 20,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            }
                        }
                    }
                });
            }

            // Columna POSICIÓN
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Posicion"),
                Header = "POS",
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

            // Columna LOGO
            dgClasificacion.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(70, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaLogo()  // Pasamos los equipos
            });

            // Crear los convertidores pasando _equipo
            var foregroundConverter = new IdEquipoToForegroundConverter(_equipo);
            var fontWeightConverter = new IdEquipoToFontWeightConverter(_equipo);
            var fontFamilyConverter = new IdEquipoToFontFamilyConverter(_equipo);

            // Columna NOMBRE EQUIPO
            Style headerStyleIzquierda = new Style(typeof(DataGridColumnHeader), columnHeaderStyle);
            headerStyleIzquierda.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Left));

            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipo"),
                Header = "EQUIPO",
                Width = new DataGridLength(330, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.ForegroundProperty, new Binding("IdEquipo") { Converter = foregroundConverter }),
                        new Setter(TextBlock.FontWeightProperty, new Binding("IdEquipo") { Converter = fontWeightConverter }),
                        new Setter(TextBlock.FontFamilyProperty, new Binding("IdEquipo") { Converter = fontFamilyConverter })
                    }
                },
                HeaderStyle = headerStyleIzquierda // ✅ Aquí aplicas el nuevo estilo solo para esta columna
            });

            // Crear instancia del convertidor
            var porcentajeConverter = new PorcentajeConverter();

            // Columna PARTIDOS JUGADOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Jugados"),
                Header = "PJ",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna PARTIDOS GANADOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Ganados"),
                Header = "PG",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna PARTIDOS EMPATADOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Empatados"),
                Header = "PE",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna PARTIDOS PERDIDOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Perdidos"),
                Header = "PP",
                Width = new DataGridLength(50, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna GOLES MARCADOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("GolesFavor"),
                Header = "GF",
                Width = new DataGridLength(60, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna GOLES ENCAJADOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("GolesContra"),
                Header = "GC",
                Width = new DataGridLength(60, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            // Columna PUNTOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Puntos"),
                Header = "PTS",
                Width = new DataGridLength(70, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.FontWeightProperty, FontWeights.Bold),
                        new Setter(TextBlock.FontSizeProperty, 16.0)
                    }
                },
                CellStyle = new Style(typeof(DataGridCell))
                {
                    Setters =
                    {
                        new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold),
                        new Setter(DataGridCell.FontSizeProperty, 16.0)
                    }
                }
            });

            dgClasificacion.SelectionChanged += DgClasificacion_SelectionChanged;


            // Configurar altura de filas y estilos generales
            dgClasificacion.RowHeight = 40;
            dgClasificacion.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgClasificacion.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgClasificacion.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgClasificacion.BorderThickness = new Thickness(0);
            dgClasificacion.CanUserAddRows = false;
            dgClasificacion.CanUserResizeColumns = false;
            dgClasificacion.CanUserResizeRows = false;
            dgClasificacion.SelectionMode = DataGridSelectionMode.Single;
            dgClasificacion.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgClasificacion.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgClasificacion.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            // Estilo de celdas
            dgClasificacion.CellStyle = new Style(typeof(DataGridCell))
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

        private void CargarMejoresEquipos()
        {
            // Mostrar Mejor Ataque
            Clasificacion mejorAtaque = _logicaClasificacion.MostrarMejorAtaque(_manager.IdManager, miCompeticion);
            equipo = _logicaEquipos.ListarDetallesEquipo(mejorAtaque.IdEquipo);
            imgMejorAtaque.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            lblMejorAtaqueEquipo.Text = mejorAtaque.NombreEquipo;
            lblMejorAtaqueGoles.Text = mejorAtaque.GolesFavor.ToString();

            // Mostrar Mejor Defensa
            Clasificacion mejorDefensa = _logicaClasificacion.MostrarMejorDefensa(_manager.IdManager, miCompeticion);
            equipo = _logicaEquipos.ListarDetallesEquipo(mejorDefensa.IdEquipo);
            imgMejorDefensa.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            lblMejorDefensaEquipo.Text = mejorDefensa.NombreEquipo;
            lblMejorDefensaGoles.Text = mejorDefensa.GolesContra.ToString();

            // Mostrar Mejor Racha
            Clasificacion mejorRacha = _logicaClasificacion.MostrarMejorRacha(_manager.IdManager, miCompeticion);
            equipo = _logicaEquipos.ListarDetallesEquipo(mejorRacha.IdEquipo);
            imgMejorRacha.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            lblMejorRachaEquipo.Text = mejorRacha.NombreEquipo;
            lblMejorRachaPartidos.Text = mejorRacha.Racha.ToString();

            // Mejor Equipo Local
            Clasificacion mejorLocal = _logicaClasificacion.MostrarMejorEquipoLocal(_manager.IdManager, miCompeticion);
            equipo = _logicaEquipos.ListarDetallesEquipo(mejorLocal.IdEquipo);
            imgMejorEquipoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            lblMejorLocalEquipo.Text = mejorLocal.NombreEquipo;
            lblMejorLocalGanados.Text = mejorLocal.LocalVictorias.ToString();

            // Mejor Equipo Visitante
            Clasificacion mejorVisitante = _logicaClasificacion.MostrarMejorEquipoVisitante(_manager.IdManager, miCompeticion);
            equipo = _logicaEquipos.ListarDetallesEquipo(mejorVisitante.IdEquipo);
            imgMejorEquipoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            lblMejorVisitanteEquipo.Text = mejorVisitante.NombreEquipo;
            lblMejorVisitanteGanados.Text = mejorVisitante.VisitanteVictorias.ToString();
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
                Converter = new ImagePathConverter()  // No necesitamos pasar la lista aquí
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private void DgClasificacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (dgClasificacion.SelectedItem == null)
                return; // Evita errores si no hay selección

            // Asegúrate de que el elemento seleccionado es un objeto de tipo 'Clasificacion' o el tipo que uses
            if (dgClasificacion.SelectedItem is Clasificacion clasificacionSeleccionada)
            {
                // Aquí ya puedes acceder al idEquipo del equipo seleccionado
                int idEquipoSeleccionado = clasificacionSeleccionada.IdEquipo;

                frmVerDetallesEquipo detallesEquipoWindow = new frmVerDetallesEquipo(idEquipoSeleccionado);
                detallesEquipoWindow.ShowDialog();

                ConfigurarDataGridClasificacion();
            }
        }
        #endregion
    }
}
