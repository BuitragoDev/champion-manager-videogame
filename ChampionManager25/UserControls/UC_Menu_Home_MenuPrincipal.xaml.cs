using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class UC_Menu_Home_MenuPrincipal : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DateTime hoy;
        Equipo miEquipo;
        Equipo equipoLocal;
        Equipo equipoVisitante;
        Estadistica estadistica;
        Jugador jugador;
        List<Equipo> equipos;
        private int numeroEquipo;
        #endregion

        // Instancias de la LOGICA
        PartidoLogica _logicaPartidos = new PartidoLogica();
        EquipoLogica _logicaEquipos = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        FechaDatos _datosFecha = new FechaDatos();

        public UC_Menu_Home_MenuPrincipal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            miEquipo = _logicaEquipos.ListarDetallesEquipo(_equipo);
            equipos = _logicaEquipos.ListarEquipos(miEquipo.IdCompeticion);
            numeroEquipo = equipos.Count;

            Fecha fechaObjeto = _datosFecha.ObtenerFechaHoy();
            hoy = DateTime.Parse(fechaObjeto.Hoy);
        }

        private void homeMenuPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            // ÚLTIMO PARTIDO
            Partido ultimoPartido = _logicaPartidos.ObtenerUltimoPartido(_equipo, _manager.IdManager, hoy);
            
            if (ultimoPartido != null)
            {
                equipoLocal = _logicaEquipos.ListarDetallesEquipo(ultimoPartido.IdEquipoLocal);
                equipoVisitante = _logicaEquipos.ListarDetallesEquipo(ultimoPartido.IdEquipoVisitante);

                // Cargar componentes del próximo partido.
                if (ultimoPartido.IdEquipoLocal == _equipo)
                {  
                    imgLocalVisitanteUltimoPartido.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/casa_icon.png"));
                }
                else if (ultimoPartido.IdEquipoVisitante == _equipo)
                {     
                    imgLocalVisitanteUltimoPartido.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/avion_icon.png"));
                }

                txtUltimoPartido.Text += NombreCompeticion(ultimoPartido.IdCompeticion,
                                                           ultimoPartido.Jornada ?? 0);

                imgEscudoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaImagen80));
                imgEscudoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoVisitante.RutaImagen80));

                txtUltimoPartidoLocal.Text = _logicaEquipos.ListarDetallesEquipo(ultimoPartido?.IdEquipoLocal ?? 0)?.Nombre?.ToUpper() ?? "";
                txtUltimoPartidoVisitante.Text = _logicaEquipos.ListarDetallesEquipo(ultimoPartido?.IdEquipoVisitante ?? 0)?.Nombre?.ToUpper() ?? "";
                txtPuntosLocal.Text = ultimoPartido?.GolesLocal.ToString() ?? "0";
                txtPuntosVisitante.Text = ultimoPartido?.GolesVisitante.ToString() ?? "0";
            }

            // PRÓXIMO PARTIDO
            Partido proximoPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, hoy);

            if (proximoPartido != null)
            {
                equipoLocal = _logicaEquipos.ListarDetallesEquipo(proximoPartido.IdEquipoLocal);
                equipoVisitante = _logicaEquipos.ListarDetallesEquipo(proximoPartido.IdEquipoVisitante);

                // Cargar componentes del próximo partido.
                if (proximoPartido.IdEquipoLocal == _equipo)
                {
                    imgLocalVisitante.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/casa_icon.png"));
                }
                else if (proximoPartido.IdEquipoVisitante == _equipo)
                {
                    imgLocalVisitante.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/avion_icon.png"));
                }

                txtProximoPartido.Text += NombreCompeticion(proximoPartido.IdCompeticion,
                                                           proximoPartido.Jornada ?? 0);
                
                imgLogoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaImagen80));
                imgLogoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoVisitante.RutaImagen80));

                txtNombreEquipos.Text = (_logicaEquipos.ListarDetallesEquipo(proximoPartido?.IdEquipoLocal ?? 0)?.Nombre?.ToUpper() ?? "SIN EQUIPO") + " - " +
                                        (_logicaEquipos.ListarDetallesEquipo(proximoPartido?.IdEquipoVisitante ?? 0)?.Nombre?.ToUpper() ?? "SIN EQUIPO");




                txtCancha.Text = "Estadio: " + (_logicaEquipos.ListarDetallesEquipo(proximoPartido?.IdEquipoLocal ?? 0)?.Estadio ?? "Sin estadio");

                txtFechaProximoPartido.Text = proximoPartido != null && proximoPartido.FechaPartido != DateTime.MinValue
                                              ? proximoPartido.FechaPartido.ToString("dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"))
                                              : "Fecha no disponible";
            }

            // CARGAR DATAGRID CLASIFICACION
            ConfigurarDataGridClasificacion();
            
            // Asignamos la lista de equipos al convertidor
            ImagePathConverter.Equipos = equipos;

            CargarDatosClasificacion(_logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion);

            // ESTADÍSTICAS DEL EQUIPO
            imgEscudoEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen80));
            txtNombreCancha.Text = _logicaEquipos.ListarDetallesEquipo(_equipo).Estadio;
            txtCapacidadCancha.Text = _logicaEquipos.ListarDetallesEquipo(_equipo).Aforo.ToString("N0") + " espectadores";
            txtObjetivoContenido.Text = _logicaEquipos.ListarDetallesEquipo(_equipo).Objetivo;
            //Racha
            txtNombreLiga.Text = _logicaCompeticion.MostrarNombreCompeticion(_logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion);

            int miCompeticion = miEquipo.IdCompeticion;
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(miCompeticion).RutaImagen80;
            imgLogoLiga.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
            int racha = _logicaClasificacion.MostrarClasificacionPorEquipo(_equipo, _manager.IdManager, miEquipo.IdCompeticion).Racha;
            txtRachaNumero.Text = racha.ToString();
            //Objetivo
            List<Clasificacion> cl = new List<Clasificacion>();
            if (miCompeticion == 1)
            {
                cl = _logicaClasificacion.MostrarClasificacion(_logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion, _manager.IdManager);
            } 
            else
            {
                cl = _logicaClasificacion.MostrarClasificacion2(_logicaEquipos.ListarDetallesEquipo(_equipo).IdCompeticion, _manager.IdManager);
            }

            int posicion = 0;
            foreach (var equipo in cl)
            {
                posicion++; // Incrementa la posición en cada iteración
                if (equipo.IdEquipo == _equipo) // Verifica si el equipo actual coincide con el buscado
                {
                    break; // Detén el bucle cuando se encuentra el equipo
                }
            }

            string objetivo = _logicaEquipos.ListarDetallesEquipo(_equipo).Objetivo;
            if(miCompeticion == 1)
            {
                if (objetivo.Equals("Campeón"))
                {
                    if (posicion <= 4)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 16)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
            }
            else{
                if (objetivo.Equals("Ascenso"))
                {
                    if (posicion <= 4)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 16)
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/verificado_icon.png"));
                    }
                    else
                    {
                        imgObjetivoResultado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rechazado_icon.png"));
                    }
                }
            }

            // Media equipo
            double mediaEquipo = _logicaJugador.ObtenerMediaEquipo(_equipo);
            txtMediaNumero.Text = Math.Round(mediaEquipo).ToString();
            if (mediaEquipo > 85)
            {
                txtMediaNumero.Foreground = new SolidColorBrush(Colors.Green);
            }
            else if (mediaEquipo >= 75)
            {
                txtMediaNumero.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else if (mediaEquipo >= 65)
            {
                txtMediaNumero.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                txtMediaNumero.Foreground = new SolidColorBrush(Colors.Red);
            }

            // RELACIONES
            int cDirectiva = _logicaManager.MostrarManager(_manager.IdManager).CDirectiva;
            int cFans = _logicaManager.MostrarManager(_manager.IdManager).CFans;
            int cJugadores = _logicaManager.MostrarManager(_manager.IdManager).CJugadores;

            txtConfianzaDirectivaValor.Text = cDirectiva.ToString();
            txtConfianzaFansValor.Text = cFans.ToString();
            txtConfianzaJugadoresValor.Text = cJugadores.ToString();

            elipseConfianzaDirectiva.Stroke = DeterminarColorElipse(cDirectiva);
            elipseConfianzaFans.Stroke = DeterminarColorElipse(cFans);
            elipseConfianzaJugadores.Stroke = DeterminarColorElipse(cJugadores);

            // ESTADISTICAS JUGADORES.
            // Goles
            estadistica = _logicaEstadistica.MostrarJugadorConMasGoles(_equipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoGoles.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugadorGoles.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Apellido;
            txtGolesPJ.Text = "Partidos jugados: " + _logicaEstadistica.MostrarJugadorConMasGoles(_equipo).PartidosJugados.ToString();

            int goles = _logicaEstadistica.MostrarJugadorConMasGoles(_equipo).Goles;
            txtGolesValor.Text = goles.ToString();

            // Asistencias
            estadistica = _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoAsistencias.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugadorAsistencias.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Apellido;
            txtAsistenciasPJ.Text = "Partidos jugados: " + _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).PartidosJugados.ToString();

            int asistencias = _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).Asistencias;
            txtAsistenciasValor.Text = asistencias.ToString();

            // MVP
            estadistica = _logicaEstadistica.MostrarJugadorConMasMvp(_equipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoMvp.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugadorMvp.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador).Apellido;
            txtMvpPJ.Text = "Partidos jugados: " + _logicaEstadistica.MostrarJugadorConMasMvp(_equipo).PartidosJugados.ToString();

            int mvp = _logicaEstadistica.MostrarJugadorConMasMvp(_equipo).MVP;
            txtMvpValor.Text = mvp.ToString();

            // Tarjetas Amarillas
            estadistica = _logicaEstadistica.MostrarJugadorConMasTarjetasAmarillas(_equipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoTarjetasAmarillas.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugadorTarjetasAmarillas.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasTarjetasAmarillas(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasTarjetasAmarillas(_equipo).IdJugador).Apellido;
            txtTarjetasAmarillasPJ.Text = "Partidos jugados: " + _logicaEstadistica.MostrarJugadorConMasTarjetasAmarillas(_equipo).PartidosJugados.ToString();

            int tAmarillas = _logicaEstadistica.MostrarJugadorConMasTarjetasAmarillas(_equipo).TarjetasAmarillas;
            txtTarjetasAmarillasValor.Text = tAmarillas.ToString();

            // Tarjetas Rojas
            estadistica = _logicaEstadistica.MostrarJugadorConMasTarjetasRojas(_equipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoTarjetasRojas.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombreJugadorTarjetasRojas.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasTarjetasRojas(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasTarjetasRojas(_equipo).IdJugador).Apellido;
            txtTarjetasRojasPJ.Text = "Partidos jugados: " + _logicaEstadistica.MostrarJugadorConMasTarjetasRojas(_equipo).PartidosJugados.ToString();

            int tRojas = _logicaEstadistica.MostrarJugadorConMasTarjetasRojas(_equipo).TarjetasRojas;
            txtTarjetasRojasValor.Text = tRojas.ToString();
        }

        #region "Métodos"
        private string NombreCompeticion(int id, int? jornada)
        {
            string nombreCompeticion = _logicaCompeticion.MostrarNombreCompeticion(id);

            if (jornada != 0)
                return $" ({nombreCompeticion} - J{jornada})";
            else
                return $" ({nombreCompeticion})";
        }


        private void ConfigurarDataGridClasificacion()
        {
            dgClasificacion.SelectionChanged -= DgClasificacion_SelectionChanged;

            // Configuración del DataGrid
            dgClasificacion.SelectedItem = null;
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
                            Value = numeroEquipo - 3,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo - 2,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo - 1,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo,
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
                            Value = numeroEquipo - 3,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo - 2,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo - 1,
                            Setters = { new Setter(DataGridCell.BackgroundProperty, Brushes.DarkRed) }
                        },
                        new DataTrigger
                        {
                            Binding = new System.Windows.Data.Binding("Posicion"),
                            Value = numeroEquipo,
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

            // Columna Nombre Equipo
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

            // Columna PUNTOS
            dgClasificacion.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Puntos"),
                Header = "PTS",
                Width = new DataGridLength(90, DataGridLengthUnitType.Pixel),
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
            dgClasificacion.RowHeight = 33;
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
        }

        // Método que carga con datos el DataGrid Clasificacion.
        private void CargarDatosClasificacion(int competicion)
        {
            List<Clasificacion> clasificaciones;

            if (competicion == 1)
            {
                clasificaciones = _logicaClasificacion.MostrarClasificacion(competicion, _manager.IdManager);
            } 
            else
            {
                clasificaciones = _logicaClasificacion.MostrarClasificacion2(competicion, _manager.IdManager);
            }

            // Asignar los datos al DataGrid
            dgClasificacion.ItemsSource = clasificaciones;
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

        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos > 70)
                return new SolidColorBrush(Colors.Green);
            else if (puntos >= 50)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x76, 0x17));
            else
                return new SolidColorBrush(Colors.Red);
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
