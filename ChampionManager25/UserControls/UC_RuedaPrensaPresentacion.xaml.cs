using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ChampionManager25.UserControls
{
    public partial class UC_RuedaPrensaPresentacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<int> _idsPartidos;
        private readonly string _rutaPartida;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        PartidoDatos _datosPartido = new PartidoDatos();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        HistorialLogica _logicaHistorial = new HistorialLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();
        TaquillaLogica _logicaTaquilla = new TaquillaLogica();

        private Cuestionario _cuestionario = new Cuestionario();

        public UC_RuedaPrensaPresentacion(Manager manager, int equipo, List<int> ids, string rutaPartida)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _idsPartidos = ids;
            _rutaPartida = rutaPartida;
        }

        private void ruedaPrensa_Loaded(object sender, RoutedEventArgs e)
        {
            lblSlider.Content = "DIRECTO: PRESENTACIÓN DE " +
                                (_manager?.Nombre?.ToUpper() ?? "NOMBRE NO DISPONIBLE") + " " +
                                (_manager?.Apellido?.ToUpper() ?? "APELLIDO NO DISPONIBLE") + " COMO ENTRENADOR DEL " +
                                (_logicaEquipo.ListarDetallesEquipo(_equipo)?.Nombre?.ToUpper() ?? "EQUIPO NO DISPONIBLE");

            // Crear la animación para mover el Label
            var animation = new DoubleAnimation
            {
                From = 225, // Comienza fuera de la pantalla (derecha)
                To = 0,     // Termina fuera de la pantalla (izquierda)
                Duration = new Duration(TimeSpan.FromSeconds(20)), // Duración de la animación
                RepeatBehavior = RepeatBehavior.Forever, // Repetir la animación infinitamente
                AutoReverse = true // No invertir la animación
            };

            // Aplicar la animación a la propiedad Canvas.Left del Label
            Storyboard.SetTarget(animation, lblSlider);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(Canvas.Left)"));

            // Crear y ejecutar el storyboard
            var storyboard = new Storyboard();
            storyboard.Children.Add(animation);
            storyboard.Begin();
            // -------------------------- Rellenado de componentes
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            imgLogo1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen64));
            imgLogo2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen64));
            imgLogo3.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen64));

            lblTitulo1.Content = (_manager?.Nombre?.ToUpper() ?? "NOMBRE NO DISPONIBLE") + " " +
                                 (_manager?.Apellido?.ToUpper() ?? "APELLIDO NO DISPONIBLE");

            lblTitulo2.Content = "Nuevo entrenador del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            lblTitulo3.Content = "EN DIRECTO DESDE " +
                                 (_logicaEquipo.ListarDetallesEquipo(_equipo)?.Ciudad?.ToUpper() ?? "CIUDAD NO DISPONIBLE");


            imgLogo4.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));
            lblPresidente.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
            lblEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;

            lblPregunta.Text = "Bienvenido a la presentación de nuestro nuevo entrenador " +
                                (_manager?.Nombre?.ToUpper() ?? "NOMBRE NO DISPONIBLE") + " " +
                                (_manager?.Apellido?.ToUpper() ?? "APELLIDO NO DISPONIBLE") +
                                ". El equipo tiene como objetivo " +
                                (_logicaEquipo.ListarDetallesEquipo(_equipo)?.Objetivo?.ToUpper() ?? "OBJETIVO NO DISPONIBLE") +
                                ", esperemos que el entrenador y el equipo puedan lograrlo. Ahora empezará la rueda de prensa con nuestro entrenador.";
        }

        // --------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON ATRÁS 
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarInicioTemporada(_manager, _equipo, _idsPartidos);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON EMPEZAR RUEDA DE PRENSA 
        private void btnEmpezarRueda_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            btnDeclinarRueda.IsEnabled = false;
            btnEmpezarRueda.IsEnabled = false;
            imgBotonAtras.IsEnabled = false;

            // Mostrar el TextBlock de la pregunta y los RadioButton
            lblEntrevistador.Visibility = Visibility.Visible;
            lblPregunta.Visibility = Visibility.Visible;
            respuestasPanel.Visibility = Visibility.Visible;

            // Seleccionar 5 preguntas aleatorias
            _cuestionario.SeleccionarPreguntas();

            // Mostrar la primera pregunta y respuestas
            MostrarPregunta();
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON DECLINAR RUEDA DE PRENSA
        private async void btnDeclinarRueda_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            
            if (_manager.PrimeraTemporada == 0)
            {
                // Cambiar primera_temporada a 1
                _logicaManager.ModificarPrimeraTemporada(1);

                btnDeclinarRueda.IsEnabled = false;
                btnEmpezarRueda.IsEnabled = false;
                imgBotonAtras.IsEnabled = false;
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

                    // Generar el calendario de Champions
                    List<Equipo> equiposChampions = _logicaEquipo.EquiposJueganEuropa1(1)
                                                    .Concat(_logicaEquipo.ListarEquiposCompeticion(5))
                                                    .OrderBy(e => Guid.NewGuid())
                                                    .ToList();
                    _logicaPartido.GenerarCalendarioChampions(equiposChampions, 5, _manager.IdManager, ObtenerSegundoMiercolesOctubre(Metodos.temporadaActual));

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
                mainWindow.RutaPartidaActual = rutaFinal;
            }

            Metodos.ReproducirSonidoTransicion();
            mainWindow.CargarPantallaPrincipal(_manager, _equipo);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------- EVENTO CLICK DE LOS RADIOBUTTON -------------------------------------------------
        private void rbRespuesta1_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Responder(0);  // Responder con la primera respuesta
        }

        private void rbRespuesta2_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Responder(1);  // Responder con la segunda respuesta
        }

        private void rbRespuesta3_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Responder(2);  // Responder con la tercera respuesta
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private void MostrarPregunta()
        {
            // Si hay más preguntas, mostrar la siguiente
            if (_cuestionario.HayMasPreguntas())
            {
                Pregunta preguntaActual = _cuestionario.ObtenerPreguntaActual() ?? new Pregunta();  // O algún valor predeterminado
                lblEntrevistador.Text = preguntaActual.Entrevistador; // Mostrar el entrevistador en el TextBlock
                lblPregunta.Text = preguntaActual.Texto;  // Mostrar la pregunta en el TextBlock

                // Mostrar las respuestas
                rbRespuesta1.Content = preguntaActual.Respuestas[0].Texto;
                rbRespuesta2.Content = preguntaActual.Respuestas[1].Texto;
                rbRespuesta3.Content = preguntaActual.Respuestas[2].Texto;
            }
            else
            {
                // Si ya no hay más preguntas, ir al resumen:
                _cuestionario.ObtenerResultadosFinales(_manager.IdManager);
                lblEntrevistador.Visibility = Visibility.Hidden;
                lblPregunta.Visibility = Visibility.Hidden;
                rbRespuesta1.Visibility = Visibility.Hidden;
                rbRespuesta2.Visibility = Visibility.Hidden;
                rbRespuesta3.Visibility = Visibility.Hidden;

                // Cargar el UserControl UC_ResumenPresentacion
                Metodos.ReproducirSonidoTransicion();

                // Notificar a MainWindow para cargar el nuevo UserControl
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.CargarResumenPresentacion(_manager, _equipo, _cuestionario._puntosDirectiva, _cuestionario._puntosFans, _cuestionario._puntosJugadores);
            }
        }
        private void Responder(int indiceRespuesta)
        {
            // Obtener la respuesta seleccionada
            Pregunta preguntaActual = _cuestionario.ObtenerPreguntaActual();
            Respuesta respuestaSeleccionada = preguntaActual.Respuestas[indiceRespuesta];

            // Sumar puntos por la respuesta seleccionada
            _cuestionario.SumarPuntos(respuestaSeleccionada);

            // Avanzar a la siguiente pregunta
            _cuestionario.AvanzarPregunta();

            // Desmarcar todos los RadioButton
            DesmarcarRadioButtons();

            // Mostrar la siguiente pregunta
            MostrarPregunta();
        }

        private void DesmarcarRadioButtons()
        {
            // Desmarcar todos los RadioButtons
            rbRespuesta1.IsChecked = false;
            rbRespuesta2.IsChecked = false;
            rbRespuesta3.IsChecked = false;
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
                _logicaPartido.crearPartidoCopa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 0, idManager);

                // Partido de vuelta (se invierte local/visitante)
                _logicaPartido.crearPartidoCopa(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 1, idManager);
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

        public static DateTime ObtenerSegundoMiercolesOctubre(int anio)
        {
            DateTime fecha = new DateTime(anio, 10, 1);
            int miercolesEncontrados = 0;

            while (fecha.Month == 10)
            {
                if (fecha.DayOfWeek == DayOfWeek.Wednesday)
                {
                    miercolesEncontrados++;
                    if (miercolesEncontrados == 2)
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
