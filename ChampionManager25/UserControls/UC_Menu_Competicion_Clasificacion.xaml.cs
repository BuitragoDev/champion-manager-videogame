﻿using ChampionManager25.Entidades;
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
        }

        private void clasificacion_Loaded(object sender, RoutedEventArgs e)
        {
            // CARGAR DATAGRID CLASIFICACION
            ConfigurarDataGridClasificacion();

            // Mostrar Mejor Ataque
            Clasificacion mejorAtaque = _logicaClasificacion.MostrarMejorAtaque(_manager.IdManager);
            imgMejorAtaque.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + mejorAtaque.IdEquipo + ".png"));
            lblMejorAtaqueEquipo.Text = mejorAtaque.NombreEquipo;
            lblMejorAtaqueGoles.Text = mejorAtaque.GolesFavor.ToString();

            // Mostrar Mejor Defensa
            Clasificacion mejorDefensa = _logicaClasificacion.MostrarMejorDefensa(_manager.IdManager);
            imgMejorDefensa.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + mejorDefensa.IdEquipo + ".png"));
            lblMejorDefensaEquipo.Text = mejorDefensa.NombreEquipo;
            lblMejorDefensaGoles.Text = mejorDefensa.GolesContra.ToString();

            // Mostrar Mejor Racha
            Clasificacion mejorRacha = _logicaClasificacion.MostrarMejorRacha(_manager.IdManager);
            imgMejorRacha.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + mejorRacha.IdEquipo + ".png"));
            lblMejorRachaEquipo.Text = mejorRacha.NombreEquipo;
            lblMejorRachaPartidos.Text = mejorRacha.Racha.ToString();

            // Mejor Equipo Local
            Clasificacion mejorLocal = _logicaClasificacion.MostrarMejorEquipoLocal(_manager.IdManager);
            imgMejorEquipoLocal.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + mejorLocal.IdEquipo + ".png"));
            lblMejorLocalEquipo.Text = mejorLocal.NombreEquipo;
            lblMejorLocalGanados.Text = mejorLocal.LocalVictorias.ToString();

            // Mejor Equipo Visitante
            Clasificacion mejorVisitante = _logicaClasificacion.MostrarMejorEquipoVisitante(_manager.IdManager);
            imgMejorEquipoVisitante.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + mejorVisitante.IdEquipo + ".png"));
            lblMejorVisitanteEquipo.Text = mejorVisitante.NombreEquipo;
            lblMejorVisitanteGanados.Text = mejorVisitante.VisitanteVictorias.ToString();
        }

        #region "Métodos"
        private string NombreCompeticion(int id)
        {
            if (id == 1)
            {
                return " (Liga de Campeones)";
            }
            else
            {
                return " (Amistoso)";
            }
        }

        private void ConfigurarDataGridClasificacion()
        {
            dgClasificacion.SelectionChanged -= DgClasificacion_SelectionChanged;

            // Configuración del DataGrid
            dgClasificacion.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgClasificacion.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgClasificacion.Columns.Clear(); // Limpiar cualquier columna previa

            List<Clasificacion> clasificaciones = _logicaClasificacion.MostrarClasificacion(1, _manager.IdManager);

            // Asignar los datos al DataGrid
            dgClasificacion.ItemsSource = clasificaciones;

            // Estilo para los colores competiciones europeas, ascenso y descenso.
            if (_logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion == 1)
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
                                Value = 33,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 34,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 35,
                                Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                            },
                            new DataTrigger
                            {
                                Binding = new System.Windows.Data.Binding("Posicion"),
                                Value = 36,
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
                CellTemplate = CrearPlantillaLogo()
            });

            // Crear los convertidores pasando _equipo
            var foregroundConverter = new IdEquipoToForegroundConverter(_equipo);
            var fontWeightConverter = new IdEquipoToFontWeightConverter(_equipo);
            var fontFamilyConverter = new IdEquipoToFontFamilyConverter(_equipo);

            // Columna NOMBRE EQUIPO
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
                        // Estilo dinámico según IdEquipo
                        new Setter(TextBlock.ForegroundProperty, new Binding("IdEquipo") { Converter = foregroundConverter }),
                        new Setter(TextBlock.FontWeightProperty, new Binding("IdEquipo") { Converter = fontWeightConverter }),
                        new Setter(TextBlock.FontFamilyProperty, new Binding("IdEquipo") { Converter = fontFamilyConverter })
                    }
                }
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
            dgClasificacion.RowHeight = 37;
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

        private DataTemplate CrearPlantillaLogo()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 40.0);
            imageFactory.SetValue(Image.HeightProperty, 40.0);
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdEquipo")
            {
                Converter = new ImagePathConverter(),
                ConverterParameter = "/Recursos/img/escudos_equipos/80x80/"
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
