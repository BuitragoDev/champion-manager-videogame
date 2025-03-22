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
    public partial class UC_Menu_Manager_Ficha : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        EntrenadorLogica _logicaEntrenador = new EntrenadorLogica();

        public UC_Menu_Manager_Ficha(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Nombre del Mánager
            txtNombreManager.Text = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " + _logicaManager.MostrarManager(_manager.IdManager).Apellido;

            // Obtener la nacionalidad.
            NacionalidadToFlagConverter converter = new NacionalidadToFlagConverter(); // Crear instancia del convertidor
            Uri banderaUri = (Uri)converter.Convert(_manager.Nacionalidad, typeof(Uri), null, null);
            imgBandera.Source = new BitmapImage(banderaUri);
            txtNacionalidad.Text = _logicaManager.MostrarManager(_manager.IdManager).Nacionalidad;

            // Obtener la fecha de nacimiento y Edad
            DateTime fechaNacimiento = _logicaManager.MostrarManager(_manager.IdManager).FechaNacimiento;
            int edad = Metodos.hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento > Metodos.hoy.AddYears(-edad))
            {
                edad--;
            }
            txtFechaNacimiento.Text = fechaNacimiento.ToString("dd/MM/yyyy") + " (" + edad + " años)";

            // Obtener Resumen de partidos.
            txtPartidosJugadosValor.Text = _logicaManager.MostrarManager(_manager.IdManager).PartidosJugados.ToString();
            txtPartidosGanadosValor.Text = _logicaManager.MostrarManager(_manager.IdManager).PartidosGanados.ToString();
            txtPartidosEmpatadosValor.Text = _logicaManager.MostrarManager(_manager.IdManager).PartidosEmpatados.ToString();
            txtPartidosPerdidosValor.Text = _logicaManager.MostrarManager(_manager.IdManager).PartidosPerdidos.ToString();
            if (_logicaManager.MostrarManager(_manager.IdManager).PartidosJugados != 0)
            {
                txtPorcentajeValor.Text = ((_logicaManager.MostrarManager(_manager.IdManager).PartidosGanados * 100) / _logicaManager.MostrarManager(_manager.IdManager).PartidosJugados).ToString() + " %";
            }
            else
            {
                txtPorcentajeValor.Text = "0 %";
            }


            // Obtener Reputación
            MostrarEstrellas(_manager.Reputacion);

            // Obtener Ranking.
            string miNombre = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " + _logicaManager.MostrarManager(_manager.IdManager).Apellido;
            int miPosicion = -1;
            List<Entrenador> listado = _logicaEntrenador.obtenerRankingEntrenadores(_manager.IdManager);
            foreach (var entrenador in listado)
            {
                if (entrenador.NombreCompleto == miNombre)
                {
                    miPosicion = entrenador.Posicion;
                    break; // Salimos del bucle una vez encontrado
                }
            }
            txtRanking.Text += miPosicion + "º";

            // Cargar Seccion Relaciones.
            int cDirectiva = _logicaManager.MostrarManager(_manager.IdManager).CDirectiva;
            int cFans = _logicaManager.MostrarManager(_manager.IdManager).CFans;
            int cJugadores = _logicaManager.MostrarManager(_manager.IdManager).CJugadores;

            elipseDirectiva.Stroke = DeterminarColorElipse(cDirectiva);
            elipseFans.Stroke = DeterminarColorElipse(cFans);
            elipseJugadores.Stroke = DeterminarColorElipse(cJugadores);

            txtDirectiva.Text = _logicaManager.MostrarManager(_manager.IdManager).CDirectiva.ToString();
            txtFans.Text = _logicaManager.MostrarManager(_manager.IdManager).CFans.ToString();
            txtJugadores.Text = _logicaManager.MostrarManager(_manager.IdManager).CJugadores.ToString();

            // Textos con las opiniones.
            // Directiva
            if (cDirectiva >= 90)
            {
                txtOpinionDirectiva.Text = "La directiva está completamente impresionada con tu gestión. Tu capacidad para dirigir al equipo y tomar decisiones clave ha fortalecido su confianza y apoyo de manera notable.";
            }
            else if (cDirectiva >= 70)
            {
                txtOpinionDirectiva.Text = "La directiva está muy contenta con tu rendimiento. Aunque aún hay pequeños aspectos a mejorar, confían en que seguirás guiando al equipo hacia el éxito con un liderazgo firme.";
            }
            else if (cDirectiva >= 50)
            {
                txtOpinionDirectiva.Text = "La relación con la directiva es estable, pero existen ciertas preocupaciones. A veces se cuestionan algunas decisiones, pero hay una disposición a seguir adelante y ver cómo evolucionan las cosas.";
            }
            else if (cDirectiva >= 30)
            {
                txtOpinionDirectiva.Text = "La directiva ha comenzado a perder confianza en tu liderazgo. Si no mejoras los resultados pronto, podrían reconsiderar tu posición, lo que pondría en riesgo tu continuidad al mando del equipo.";
            }
            else
            {
                txtOpinionDirectiva.Text = "La relación con la directiva es extremadamente tensa. La falta de resultados y las decisiones cuestionadas han erosionado casi por completo su confianza, y tu futuro en el equipo está seriamente comprometido.";
            }

            // Fans
            if (cFans >= 90)
            {
                txtOpinionFans.Text = "Los fans están absolutamente encantados con tu gestión. Cada victoria, cada decisión que tomas es celebrada con entusiasmo. Tienes su total apoyo, y la conexión con ellos es más fuerte que nunca.";
            }
            else if (cFans >= 70)
            {
                txtOpinionFans.Text = "Los fans están muy satisfechos con tu rendimiento. Hay una gran admiración por lo que has hecho hasta ahora, aunque algunos aún esperan un poco más de consistencia. En general, te respaldan completamente.";
            }
            else if (cFans >= 50)
            {
                txtOpinionFans.Text = "La relación con los fans es moderada. Muchos aprecian tus esfuerzos, pero otros empiezan a mostrar algo de desconfianza debido a los altibajos en los resultados. Aún hay tiempo para ganar más apoyo si logras mejorar.";
            }
            else if (cFans >= 30)
            {
                txtOpinionFans.Text = "La relación con los fans se ha deteriorado en las últimas semanas. Están empezando a frustrarse con la falta de victorias y algunos de tus movimientos. La presión está aumentando y necesitarás trabajar más para recuperar su apoyo.";
            }
            else
            {
                txtOpinionFans.Text = "Los fans están completamente decepcionados con tu gestión. Los resultados no son los esperados y tu liderazgo está siendo severamente criticado. La relación está en su punto más bajo y será un desafío revertir esta situación.";
            }

            // Jugadores
            if (cJugadores >= 90)
            {
                txtOpinionJugadores.Text = "Los jugadores están totalmente a tu favor. La moral del equipo está por las nubes y todos confían plenamente en tus decisiones. El vestuario está unido y motivado, listos para alcanzar grandes logros bajo tu dirección.";
            }
            else if (cJugadores >= 70)
            {
                txtOpinionJugadores.Text = "La relación con los jugadores es muy positiva. Confían en tus decisiones y saben que los llevas por el buen camino. Aunque algunos todavía tienen dudas menores, el equipo en general está muy comprometido con tus objetivos.";
            }
            else if (cJugadores >= 50)
            {
                txtOpinionJugadores.Text = "La confianza de los jugadores es moderada. La mayoría sigue tu liderazgo, pero algunos empiezan a mostrar señales de desconfianza debido a ciertas decisiones tácticas. El equipo sigue unido, pero hay aspectos que necesitan mejorar para evitar que crezca la incertidumbre.";
            }
            else if (cJugadores >= 30)
            {
                txtOpinionJugadores.Text = "La moral del equipo está baja. Los jugadores están empezando a dudar de tus métodos y algunos cuestionan tus decisiones. Si no logras mejorar los resultados y darles más razones para confiar en ti, la situación podría empeorar.";
            }
            else
            {
                txtOpinionJugadores.Text = "La relación con los jugadores es crítica. La confianza en tu liderazgo es prácticamente nula y muchos ya están perdiendo la fe en tus decisiones. El vestuario está dividido y, sin cambios inmediatos, podrías enfrentar una rebelión dentro del equipo.";
            }

            // Cargar el DataGrid del Ranking de entrenadores.
            ConfigurarDataGrid();
        }

        #region "Métodos"
        private void MostrarEstrellas(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellas.Children.Clear();

            // Cargar las imágenes de recursos
            ImageSource estrellaON = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOn.png"));
            ImageSource estrellaOFF = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOff.png"));

            // Determinar el número de estrellas según la reputación
            int numeroEstrellas = 0;

            if (reputacion == 5)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion == 4)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion == 3)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion == 2)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion == 1)
            {
                numeroEstrellas = 1;
            }
            else
            {
                numeroEstrellas = 0;
            }

            // Dibujar 5 estrellas (activas e inactivas)
            for (int i = 0; i < 5; i++)
            {
                // Crear la imagen de la estrella
                Image estrella = new Image
                {
                    Width = 32, // Ancho de cada estrella
                    Height = 32,
                    Source = i < numeroEstrellas ? estrellaON : estrellaOFF
                };

                // Colocar la estrella en el Canvas
                Canvas.SetLeft(estrella, i * 35); // Separación horizontal entre estrellas
                Canvas.SetTop(estrella, 0);

                // Agregar la estrella al Canvas
                canvasEstrellas.Children.Add(estrella);
            }
        }

        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos > 70)
                return new SolidColorBrush(Colors.Green);
            else if (puntos >= 50)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x76, 0x17));
            else
                return new SolidColorBrush(Colors.Red);
        }

        // --------------------------------------------------------------------------------- Método que configura el DataGrid.
        private void ConfigurarDataGrid()
        {
            dgRankingEntrenadores.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgRankingEntrenadores.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgRankingEntrenadores.Columns.Clear(); // Limpiar cualquier columna previa

            List<Entrenador> entrenadores = _logicaEntrenador.obtenerRankingEntrenadores(_manager.IdManager);
            dgRankingEntrenadores.ItemsSource = entrenadores;

            // Configurar columnas específicas
            dgRankingEntrenadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Posicion"),
                Header = "",
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

            dgRankingEntrenadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "ENTRENADOR",
                Width = new DataGridLength(375, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            dgRankingEntrenadores.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipo"),
                Header = "EQUIPO",
                Width = new DataGridLength(375, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Columna PUNTOS con cabecera centrada
            var puntosColumn = new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Puntos"),
                Header = "PUNTOS",
                Width = new DataGridLength(150, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Center),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            };

            // Estilo específico para la cabecera de la columna "PUNTOS"
            puntosColumn.HeaderStyle = new Style(typeof(DataGridColumnHeader))
            {
                Setters =
                {
                    new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center), // Centrar el texto de la cabecera
                    new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6c9aa5"))),
                    new Setter(Control.ForegroundProperty, Brushes.Black),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")),
                    new Setter(Control.FontSizeProperty, 16.0),
                    new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center),
                    new Setter(Control.BorderBrushProperty, Brushes.Transparent),
                    new Setter(Control.BorderThicknessProperty, new Thickness(0))
                }
            };

            // Agregar la columna de "PUNTOS"
            dgRankingEntrenadores.Columns.Add(puntosColumn);

            // Configurar altura de filas y estilos generales
            dgRankingEntrenadores.RowHeight = 35;
            dgRankingEntrenadores.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgRankingEntrenadores.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgRankingEntrenadores.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgRankingEntrenadores.BorderThickness = new Thickness(0);
            dgRankingEntrenadores.CanUserAddRows = false;
            dgRankingEntrenadores.CanUserResizeColumns = false;
            dgRankingEntrenadores.CanUserResizeRows = false;
            dgRankingEntrenadores.SelectionMode = DataGridSelectionMode.Single;
            dgRankingEntrenadores.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgRankingEntrenadores.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgRankingEntrenadores.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            // Estilo general de las cabeceras
            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6c9aa5"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 16.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Left));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgRankingEntrenadores.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgRankingEntrenadores.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Transparent)),
                    new Setter(Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(35, 40, 45))),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code Light")),
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
