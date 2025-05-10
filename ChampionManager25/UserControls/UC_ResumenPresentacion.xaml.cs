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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_ResumenPresentacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private int _puntosDirectiva;
        private int _puntosFans;
        private int _puntosJugadores;
        private readonly string _rutaPartida;
        #endregion

        // Instancias de la LOGICA
        PartidoDatos _datosPartido = new PartidoDatos();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        HistorialLogica _logicaHistorial = new HistorialLogica();
        TaquillaLogica _logicaTaquilla = new TaquillaLogica();

        public UC_ResumenPresentacion(Manager manager, int equipo, int directiva, int fans, int jugadores, string rutaPartida)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _puntosDirectiva = directiva;
            _puntosFans = fans;
            _puntosJugadores = jugadores;
            _rutaPartida = rutaPartida;
        }

        private void resumenRuedaPrensa_Loaded(object sender, RoutedEventArgs e)
        {
            elipseDirectiva.Stroke = DeterminarColorElipse(_puntosDirectiva);
            elipseFans.Stroke = DeterminarColorElipse(_puntosFans);
            elipseJugadores.Stroke = DeterminarColorElipse(_puntosJugadores);

            lblPuntosDirectiva.Text = _puntosDirectiva.ToString();
            lblPuntosDirectiva.Foreground = DeterminarColorTexto(_puntosDirectiva);

            lblPuntosFans.Text = _puntosFans.ToString();
            lblPuntosFans.Foreground = DeterminarColorTexto(_puntosFans);

            lblPuntosJugadores.Text = _puntosJugadores.ToString();
            lblPuntosJugadores.Foreground = DeterminarColorTexto(_puntosJugadores);

            lblFrase1.Text = frasesResumenDirectiva(_puntosDirectiva);
            lblFrase2.Text = frasesResumenFans(_puntosFans);
            lblFrase3.Text = frasesResumenJugadores(_puntosJugadores);
        }

        // ---------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON AVANZAR
        private async void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnAvanzar.IsEnabled = false;
                progressBar.Visibility = Visibility.Visible;

                await Task.Run(() =>
                {
                    // Crear el calendario de las Ligas
                    int temporadaActual = Metodos.temporadaActual;
                    _datosPartido.GenerarCalendario(temporadaActual, _manager.IdManager, 1);
                    _datosPartido.GenerarCalendario(temporadaActual, _manager.IdManager, 2);

                    // Generar el calendario de Copa nacional
                    List<Equipo> listaEquipos = _logicaEquipo.ListarTodosLosEquipos();
                    GeneralCalendarioCopa(listaEquipos);

                    // Generar las clasificaciones
                    _logicaClasificacion.RellenarClasificacion(1, _manager.IdManager);
                    _logicaClasificacion.RellenarClasificacion2(2, _manager.IdManager);

                    // Generar registros de la tabla ESTADÍSTICAS_JUGADORES
                    int numJugadores = _logicaJugador.NumeroJugadoresTotales();
                    _logicaEstadistica.InsertarEstadisticasJugadores(numJugadores, _manager.IdManager);

                    // Asignar equipo a la tabla "taquilla"
                    _logicaTaquilla.GenerarTaquilla(_equipo, _manager.IdManager);

                    // Generar el primer registro del historial
                    _logicaHistorial.CrearLineaHistorial(_manager.IdManager, _equipo, "2024/2025");

                    // Generar la alineacion del equipo
                    _logicaJugador.CrearAlineacion(_equipo);

                    // Crear los mensaje de inicio de partida
                    Mensaje mensajeInicio1 = new Mensaje
                    {
                        Fecha = new DateTime(2024, 7, 15),
                        Remitente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente,
                        Asunto = "Bienvenido al Club",
                        Contenido = "Desde la Directiva del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te damos la bienvenida. Tenemos muchas esperanzas puestas en tí, y estamos seguros de que contigo empieza una nueva etapa en nuestro club, y que nos esperan grandes éxitos.\n\nLa junta directiva y los empleados te irán informando a través de correos electrónicos de las cosas que sucedan en el club.",
                        TipoMensaje = "Notificación",
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Leido = false,
                        Icono = 0 // 0 es icono de equipo
                    };

                    // Crear el mensaje aconsejando poner precio a los abonos de temporada
                    string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

                    Mensaje mensajeAbonados = new Mensaje
                    {
                        Fecha = Metodos.hoy,
                        Remitente = presidente,
                        Asunto = "Campaña de abonados",
                        Contenido = $"¡Se ha iniciado la campaña de abonados al club!\n\nPuedes establecer los precios de los abonos de temporada en la sección de ESTADIO.\n\nRecuerda que los abonos solamente pueden realizarse antes del inicio de la competición de Liga.",
                        TipoMensaje = "Notificación",
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Leido = false,
                        Icono = 0 // 0 es icono de equipo
                    };

                    _logicaMensajes.crearMensaje(mensajeAbonados);

                    _logicaMensajes.crearMensaje(mensajeInicio1);
                });

                progressBar.Visibility = Visibility.Collapsed;

                // Aquí ya puedes confirmar la partida, porque ya no se va a usar más la base de datos temporal
                string rutaFinal = GestorPartidas.ConfirmarPartida(_rutaPartida);
                Conexion.EstablecerConexionPartida(rutaFinal);

                // Actualizar ruta en MainWindow por si quieres guardarla allí
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.RutaPartidaActual = rutaFinal;

                Metodos.ReproducirSonidoTransicion();
                mainWindow.CargarPantallaPrincipal(_manager, _equipo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Se ha producido un error:\n\n{ex.Message}\n\nDetalles:\n{ex.StackTrace}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        // --------------------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos > 0)
                return new SolidColorBrush(Colors.Green);
            else if (puntos == 0)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x76, 0x17));
            else
                return new SolidColorBrush(Colors.Red);
        }

        private SolidColorBrush DeterminarColorTexto(int puntos)
        {
            if (puntos > 0)
                return new SolidColorBrush(Colors.Green);
            else if (puntos == 0)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x76, 0x17));
            else
                return new SolidColorBrush(Colors.Red);
        }

        private string frasesResumenDirectiva(int puntos)
        {
            if (puntos > 31)
                return "La Directiva está realmente impresionada con tus respuestas en esta rueda de prensa. Creen que has transmitido un mensaje sólido y alineado con la visión del club.";
            else if (puntos > 0)
                return "La Directiva ha recibido de buena manera tus declaraciones en esta rueda de prensa. Consideran que has manejado bien la situación y mostrado liderazgo.";
            else if (puntos == 0)
                return "La Directiva no tiene una opinión marcada sobre tus respuestas en esta rueda de prensa. Creen que has sido correcto, pero sin destacar especialmente.";
            else
                return "La Directiva ha quedado algo decepcionada con tus respuestas en esta rueda de prensa. Piensan que podrías haber sido más claro o transmitir un mensaje más positivo.";
        }

        private string frasesResumenFans(int puntos)
        {
            if (puntos > 31)
                return "La afición está entusiasmada con tus respuestas en esta rueda de prensa. Consideran que has hablado con pasión y has representado bien el sentir del equipo.";
            else if (puntos > 0)
                return "Los fans han valorado positivamente tus declaraciones. Creen que has respondido con seguridad y transmitido confianza en el futuro del equipo.";
            else if (puntos == 0)
                return "Los seguidores no están ni muy contentos ni muy molestos con tus respuestas. Han notado un discurso neutral, sin demasiada emoción.";
            else
                return "Los aficionados no han recibido bien tus palabras en esta rueda de prensa. Creen que podrías haber sido más convincente o mostrar más empatía con el equipo.";
        }

        private string frasesResumenJugadores(int puntos)
        {
            if (puntos > 31)
                return "Los jugadores están muy motivados tras escuchar tus palabras en esta rueda de prensa. Sienten que confías en ellos y eso los impulsa a dar lo mejor en el campo.";
            else if (puntos > 0)
                return "El equipo ha tomado bien tus declaraciones. Creen que has sabido mantener la calma y respaldar al grupo en este momento.";
            else if (puntos == 0)
                return "Los jugadores han recibido tus palabras de manera neutral. No han sentido un gran impacto, pero tampoco han percibido nada negativo.";
            else
                return "Las declaraciones en la rueda de prensa han generado algo de malestar en la plantilla. Algunos jugadores creen que podrías haber mostrado más respaldo o confianza en el equipo.";
        }

        private void GeneralCalendarioCopa(List<Equipo> listaEquipos)
        {
            GenerarTreintaidosavosCopa(listaEquipos, Metodos.temporadaActual, _manager.IdManager, 4);
        }

        public void GenerarTreintaidosavosCopa(List<Equipo> equiposCopa, int temporada, int idManager, int idCompeticionCopa)
        {
            if (equiposCopa.Count != 64)
                throw new ArgumentException("Deben ser exactamente 64 equipos para los dieciseisavos de final.");

            // Mezclar aleatoriamente
            Random rnd = new Random();
            var equiposMezclados = equiposCopa.OrderBy(e => rnd.Next()).ToList();

            // Fechas de ida y vuelta
            DateTime fechaIda = ObtenerPrimerMiercolesSeptiembre(temporada);
            DateTime fechaVuelta = fechaIda.AddDays(14);

            // Crear los emparejamientos
            for (int i = 0; i < equiposMezclados.Count; i += 2)
            {
                int idLocal = equiposMezclados[i].IdEquipo;
                int idVisitante = equiposMezclados[i + 1].IdEquipo;

                // Partido de ida
                _datosPartido.crearPartidoCopa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 0, idManager);

                // Partido de vuelta (se invierte local/visitante)
                _datosPartido.crearPartidoCopa(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 1, idManager);
            }
        }

        public static DateTime ObtenerPrimerMiercolesSeptiembre(int anio)
        {
            DateTime fecha = new DateTime(anio, 9, 1);
            int miercolesEncontrados = 0;

            while (fecha.Month == 9)
            {
                if (fecha.DayOfWeek == DayOfWeek.Wednesday)
                {
                    miercolesEncontrados++;
                    if (miercolesEncontrados == 1)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }
        #endregion
    }
}
