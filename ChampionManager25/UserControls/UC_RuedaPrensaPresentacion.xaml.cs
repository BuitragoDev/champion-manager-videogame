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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_RuedaPrensaPresentacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<int> _idsPartidos;
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

        private Cuestionario _cuestionario = new Cuestionario();

        public UC_RuedaPrensaPresentacion(Manager manager, int equipo, List<int> ids)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _idsPartidos = ids;
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
            imgLogo1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/64x64/" + _equipo + ".png"));
            imgLogo2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/64x64/" + _equipo + ".png"));
            imgLogo3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/64x64/" + _equipo + ".png"));

            lblTitulo1.Content = (_manager?.Nombre?.ToUpper() ?? "NOMBRE NO DISPONIBLE") + " " +
                                 (_manager?.Apellido?.ToUpper() ?? "APELLIDO NO DISPONIBLE");

            lblTitulo2.Content = "Nuevo entrenador del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            lblTitulo3.Content = "EN DIRECTO DESDE " +
                                 (_logicaEquipo.ListarDetallesEquipo(_equipo)?.Ciudad?.ToUpper() ?? "CIUDAD NO DISPONIBLE");


            imgLogo4.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/" + _equipo + ".png"));
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
        private void btnDeclinarRueda_Click(object sender, RoutedEventArgs e)
        {
            // Crear el calendario de las Ligas
            int temporadaActual = Metodos.temporadaActual;
            _datosPartido.GenerarCalendario(temporadaActual, _manager.IdManager, 1);

            // Generar las clasificaciones
            _logicaClasificacion.RellenarClasificacion(1, _manager.IdManager);

            // Generar registros de la tabla ESTADÍSTICAS_JUGADORES
            int numJugadores = _logicaJugador.NumeroJugadoresTotales();
            _logicaEstadistica.InsertarEstadisticasJugadores(numJugadores, _manager.IdManager);

            // Generar el primer registro del historial
            _logicaHistorial.CrearLineaHistorial(_manager.IdManager, _equipo, "2024/2025");

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

            _logicaMensajes.crearMensaje(mensajeInicio1);

            // Cargar la pantalla principal con los nuevos datos
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
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
        #endregion
    }
}
