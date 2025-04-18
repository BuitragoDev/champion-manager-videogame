using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_PantallaPrincipal : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private static MediaPlayer mediaPlayer = new MediaPlayer(); // Inicialización al declarar
        Equipo miEquipo;
        List<Jugador> balonOro;
        List<Jugador> mejorOnce;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        FechaDatos _datosFecha = new FechaDatos();
        HistorialLogica _logicaHistorial = new HistorialLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        ManagerLogica _logicaManager = new ManagerLogica();

        NacionalidadToFlagConverter convertidorBandera = new NacionalidadToFlagConverter();

        public UC_PantallaPrincipal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);

            // Código que inicializa el sonido de fondo de la pantalla principal
            try
            {
                Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pantallaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar datos de la cabecera
            imgLogoEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen80));

            txtEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            txtManager.Text = _manager.Nombre + " " + _manager.Apellido;
            int repu = _logicaManager.MostrarManager(_manager.IdManager).Reputacion;
            MostrarEstrellas(repu);

            CargarFecha();

            Fecha fechaObjeto = _datosFecha.ObtenerFechaHoy();
            DateTime hoy = DateTime.Parse(fechaObjeto.Hoy);

            // Comprobar si es DIA DE PARTIDO y cambiar el boton
            Partido proximoPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, hoy);
            if (proximoPartido != null && proximoPartido.FechaPartido == hoy)
            {
                btnAvanzar.Content = "PARTIDO";
            } 

            // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
            if (DockPanel_Central.Children.Count > 0)
            {
                DockPanel_Central.Children.Clear();
            }
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);
        }

        private void pantallaPrincipal_Unloaded(object sender, RoutedEventArgs e)
        {
            //Metodos.DetenerMusica(); // Detener la música al descargar el control
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón AVANZAR
        private async void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            // Reproducir el sonido
            Metodos.ReproducirSonidoTransicion();

            // Comprobar si mi equipo tiene partido.
            Partido miPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, Metodos.hoy);

            if (miPartido != null && miPartido.FechaPartido == Metodos.hoy)
            {
                // Comprobamos si hay jugadores lesionados o sancionados en la alineacion titular
                List<Jugador> alineacion = _logicaJugador.MostrarAlineacion(1, 11);
                int cont = 0;
                foreach (var jugador in alineacion)
                {
                    if (jugador.Lesion > 0 || jugador.Sancionado > 0)
                    {
                        cont++;
                    }
                }

                if (cont > 0)
                {
                    // Mostrar ventana avisando de que la alineacion es incorrecta
                    string titulo = "INFORMACIÓN";
                    string mensaje = "Por favor revisa la alineación, has incluido jugadores que están lesionados o sancionados y no pueden jugar el partido.";
                    frmVentanaEmergenteDosBotones ventanaAlineacionIncorrecta = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                    ventanaAlineacionIncorrecta.ShowDialog();
                } 
                else
                {
                    // Cargar Pantalla de Simulacion de MI PARTIDO
                    frmResumenPartido ventanaResumenPartido = new frmResumenPartido(_manager, _equipo, miPartido);
                    ventanaResumenPartido.ShowDialog();

                    // Comprobamos si hay otros partidos hoy
                    List<Partido> listaPartidos = _logicaPartidos.PartidosHoy(_equipo, _manager.IdManager);
                    if (listaPartidos != null && listaPartidos.Count > 0)
                    {
                        // Cargar Ventana de Simulacion de partidos
                        frmSimulandoPartidos ventanaSimulacion = new frmSimulandoPartidos(_manager, _equipo, listaPartidos);
                        ventanaSimulacion.ShowDialog();

                        // Avanzamos un dia en el calendario
                        _datosFecha.AvanzarUnDia();

                        // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                        await Task.Delay(50);

                        // Recargamos la fecha
                        CargarFecha();

                        // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                        if (DockPanel_Central.Children.Count > 0)
                        {
                            DockPanel_Central.Children.Clear();
                        }
                        UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                        DockPanel_Central.Children.Add(homeMenuPrincipal);

                        if (ventanaSimulacion.copaFinalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa
                            frmResumenCopaNacional ventanaResumenCopaNacional = new frmResumenCopaNacional(_manager, _equipo);
                            ventanaResumenCopaNacional.ShowDialog();
                        }
                    }
                    else
                    {
                        // Avanzamos un dia en el calendario
                        _datosFecha.AvanzarUnDia();

                        // Recargamos la fecha
                        CargarFecha();

                        // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                        if (DockPanel_Central.Children.Count > 0)
                        {
                            DockPanel_Central.Children.Clear();
                        }
                        UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                        DockPanel_Central.Children.Add(homeMenuPrincipal);
                    }
                }  
            }
            else
            {
                // Comprobar si hay partidos este dia y simularlos
                List<Partido> listaPartidos = _logicaPartidos.PartidosHoy(_equipo, _manager.IdManager);
                
                if (listaPartidos != null && listaPartidos.Count > 0)
                {
                    // Cargar Ventana de Simulacion de partidos
                    frmSimulandoPartidos ventanaSimulacion = new frmSimulandoPartidos(_manager, _equipo, listaPartidos);
                    ventanaSimulacion.ShowDialog();

                    // Avanzamos un dia en el calendario
                    _datosFecha.AvanzarUnDia();

                    // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                    await Task.Delay(50);

                    // Recargamos la fecha
                    CargarFecha();

                    // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                    if (DockPanel_Central.Children.Count > 0)
                    {
                        DockPanel_Central.Children.Clear();
                    }
                    UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                    DockPanel_Central.Children.Add(homeMenuPrincipal);
                }
                else
                {
                    // Avanzamos un dia en el calendario
                    _datosFecha.AvanzarUnDia();

                    // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                    await Task.Delay(50);

                    // Recargamos la fecha
                    CargarFecha();

                    // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                    if (DockPanel_Central.Children.Count > 0)
                    {
                        DockPanel_Central.Children.Clear();
                    }
                    UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                    DockPanel_Central.Children.Add(homeMenuPrincipal);
                }
            }

            // Comprobamos si la fecha de hoy es mayor que el ultimo partido del calendario
            if (DateTime.TryParse(_logicaPartidos.ultimoPartidoCalendario(), out DateTime ultimoPartido))
            {
                DateTime hoy = Metodos.hoy;

                if (hoy > ultimoPartido)
                {
                    List<Clasificacion> clasificacion = _logicaClasificacion.MostrarClasificacion(1, _manager.IdManager);
                    int miEquipoId = _equipo;
                    int posicion = clasificacion.FirstOrDefault(c => c.IdEquipo == miEquipoId)?.Posicion ?? -1;

                    // Cargar Pantalla de Final de Temporada
                    frmResumenTemporada ventanaResumenTemporada = new frmResumenTemporada(_manager, _equipo, clasificacion, posicion);
                    ventanaResumenTemporada.ShowDialog();
                    
                    // Comprobar si se ha conseguido el Objetivo de Temporada
                    string objetivo = _logicaEquipo.ListarDetallesEquipo(_equipo).Objetivo;
                    int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
                    CalcularObjetivoTemporada(objetivo, posicion, miCompeticion);

                    Manager yo = _logicaManager.MostrarManager(_manager.IdManager);
                    if (yo.CDirectiva == 0 || yo.CFans == 0 || yo.CJugadores == 0)
                    {
                        // Despedir al manager en la Base de Datos
                        _logicaManager.DespedirManager(_manager.IdManager);

                        // Mostrar ventana de despido
                        string titulo = "INFORMACIÓN";
                        string mensaje = "El club ha decidido prescindir de los servicios de su entrenador.\n\nLos recientes resultados no han estado a la altura de las expectativas, y la directiva considera necesario un cambio de rumbo para reconducir el club.\n\nAgradecemos su dedicación y profesionalidad durante su etapa al frente del equipo, y le deseamos suerte en sus futuros proyectos.";
                        frmVentanaDespido ventanaDespido = new frmVentanaDespido(titulo, mensaje);
                        ventanaDespido.ShowDialog();

                        Metodos.ReproducirSonidoTransicion();

                        var mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.CargarPortada();
                    }
                    else if(posicion >= 33)
                    {
                        // Despedir al manager en la Base de Datos
                        _logicaManager.DespedirManager(_manager.IdManager);

                        // Mostrar ventana de despido
                        string titulo = "INFORMACIÓN";
                        string mensaje = "El club ha decidido prescindir de los servicios de su entrenador.\n\nEl descenso de categoría ha sido la gota que ha colmado el vaso, y la directiva considera necesario un cambio de rumbo para reconducir el club.\n\nAgradecemos su dedicación y profesionalidad durante su etapa al frente del equipo, y le deseamos suerte en sus futuros proyectos.";
                        frmVentanaDespido ventanaDespido = new frmVentanaDespido(titulo, mensaje);
                        ventanaDespido.ShowDialog();

                        Metodos.ReproducirSonidoTransicion();

                        var mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.CargarPortada();
                    }
                    else
                    {
                        btnAvanzar.Visibility = Visibility.Collapsed;
                        progressBar.Visibility = Visibility.Visible;

                        await Task.Run(() =>
                        {
                            // Actualizar la tabla historial_manager
                            _logicaHistorial.CopiarPartidosHistorialManager(Metodos.temporadaActual);
                            _logicaHistorial.CopiarConfianzasManager(Metodos.temporadaActual);
                            _logicaHistorial.CopiarPosicionLigaManager(Metodos.temporadaActual, posicion);

                            // Actualizar las tablas palmares, palmares_manager e historial_finales
                            // PALMARES
                            foreach (var equipo in clasificacion)
                            {
                                if (equipo.Posicion == 1)
                                {
                                    _logicaPalmares.AnadirTituloCampeon(equipo.IdEquipo);
                                }
                            }

                            // PALMARES_MANAGER
                            if (posicion == 1) // Si he ganado la liga...
                            {
                                _logicaPalmares.AnadirTituloManager(1, _equipo, _manager.IdManager, Metodos.temporadaActual);
                            }

                            // REPUTACION MANAGER
                            if (posicion == 1)
                            {
                                _logicaManager.ActualizarReputacion(_manager.IdManager, 25);
                            }
                            else if (posicion <= 4)
                            {
                                _logicaManager.ActualizarReputacion(_manager.IdManager, 10);
                            }
                            else if (posicion >= 16)
                            {
                                _logicaManager.ActualizarReputacion(_manager.IdManager, -10);
                            }
                            else
                            {
                                _logicaManager.ActualizarReputacion(_manager.IdManager, 5);
                            }

                            // HISTORIAL FINALES
                            int campeon = 0;
                            int finalista = 0;
                            foreach (var equipo in clasificacion)
                            {
                                if (equipo.Posicion == 1)
                                {
                                    campeon = equipo.IdEquipo;
                                }
                                if (equipo.Posicion == 2)
                                {
                                    finalista = equipo.IdEquipo;
                                }
                            }
                            _logicaPalmares.AnadirCampeonFinalista(Metodos.temporadaActual, campeon, finalista);

                            // Balon de Oro
                            balonOro = _logicaEstadisticas.BalonDeOro();

                            // Mejor 11 de la Temporada
                            mejorOnce = _logicaEstadisticas.MejorOnceTemporada();

                            // Crear clasificaciones para descensos y ascensos
                            List<Clasificacion> clasificacion1Final = _logicaClasificacion.MostrarClasificacion(1, _manager.IdManager);
                            List<Clasificacion> clasificacion2Final = _logicaClasificacion.MostrarClasificacion2(2, _manager.IdManager);

                            // Resetear tablas clasificacion, estadisticas_jugadores, historial_manager_temp
                            _logicaClasificacion.ResetearClasificacion(1);
                            _logicaClasificacion.ResetearClasificacion(2);
                            _logicaEstadisticas.ResetearEstadisticas();
                            _logicaHistorial.ResetearHistorialTemporal();
                            _logicaPartidos.ResetearPartidos();

                            // Resetear Moral y Estado de Forma a 50
                            _logicaJugador.ResetearMoralEstadoForma();

                            // Crear Nueva temporada
                            _datosFecha.AvanzarUnAnio();
                            Metodos.temporadaActual = _datosFecha.ObtenerFechaHoy().Anio;
                            _datosFecha.AvanzarFecha(Metodos.temporadaActual);
                            Metodos.hoy = DateTime.Parse(_datosFecha.ObtenerFechaHoy().Hoy);

                            // Descender
                            int totalEquipos = clasificacion1Final.Count();
                            foreach (var equipo in clasificacion1Final)
                            {
                                if (equipo.Posicion > (totalEquipos - 4) && equipo.Posicion <= totalEquipos)
                                {
                                    _logicaEquipo.AscenderDescenderEquipo(equipo.IdEquipo, 2);
                                }
                            }

                            // Ascender
                            foreach (var equipo in clasificacion2Final)
                            {
                                if (equipo.Posicion >= 1 && equipo.Posicion <= 4)
                                {
                                    _logicaEquipo.AscenderDescenderEquipo(equipo.IdEquipo, 1);
                                }
                            }

                            // Crear el calendario de las Ligas
                            int temporadaActual = Metodos.temporadaActual;
                            _logicaPartidos.GenerarCalendario(temporadaActual, _manager.IdManager, 1);
                            _logicaPartidos.GenerarCalendario(temporadaActual, _manager.IdManager, 2);

                            // Generar las clasificaciones
                            _logicaClasificacion.RellenarClasificacion(1, _manager.IdManager);
                            _logicaClasificacion.RellenarClasificacion2(2, _manager.IdManager);

                            // Generar el primer registro del historial
                            string temporadaFormateada = $"{temporadaActual}/{temporadaActual + 1}";
                            _logicaHistorial.CrearLineaHistorial(_manager.IdManager, _equipo, temporadaFormateada);

                            // Crear los mensaje de inicio de partida
                            Mensaje mensajeNuevaTemporada = new Mensaje
                            {
                                Fecha = new DateTime(temporadaActual, 7, 15),
                                Remitente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente,
                                Asunto = "Nueva Temporada",
                                Contenido = "Desde la Directiva del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te damos la bienvenida a una nueva temporada.\n\nLa junta directiva y los empleados te irán informando a través de correos electrónicos de las cosas que sucedan en el club.",
                                TipoMensaje = "Notificación",
                                IdEquipo = _equipo,
                                IdManager = _manager.IdManager,
                                Leido = false,
                                Icono = 0 // 0 es icono de equipo
                            };

                            _logicaMensajes.crearMensaje(mensajeNuevaTemporada);
                        });

                        // Cargar Pantalla de Premios de Jugadores
                        frmVentanaPremioJugadores ventanaPremiosJugadores = new frmVentanaPremioJugadores(balonOro, mejorOnce);
                        ventanaPremiosJugadores.ShowDialog();

                        CargarFecha();

                        // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                        if (DockPanel_Central.Children.Count > 0)
                        {
                            DockPanel_Central.Children.Clear();
                        }
                        UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                        DockPanel_Central.Children.Add(homeMenuPrincipal);

                        progressBar.Visibility = Visibility.Collapsed;
                        btnAvanzar.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón AJUSTES
        private void imgAjustes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Reproducir el sonido de clic
            Metodos.ReproducirSonidoClick();

            frmMenuOpciones ventanaEmergente = new frmMenuOpciones();
            ventanaEmergente.ShowDialog();
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón HOME
        private void imgHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Home menuPrincipal = new UC_Menu_Home();
            DockPanel_Submenu.Children.Add(menuPrincipal);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón EQUIPO
        private void imgClub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Club menuClub = new UC_Menu_Club();
            DockPanel_Submenu.Children.Add(menuClub);

            // Suscribirse a los eventos
            menuClub.MostrarInformacion += CargarClubInformacion;
            menuClub.MostrarPlantilla += CargarClubPlantilla;

            // Cambiar el color del texto
            menuClub.lblInformacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ENTRENADOR
        private void imgEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Entrenador menuEntrenador = new UC_Menu_Entrenador();
            DockPanel_Submenu.Children.Add(menuEntrenador);

            menuEntrenador.MostrarAlineacion += CargarEntrenadorAlineacion;
            menuEntrenador.MostrarEntrenamiento += CargarEntrenadorEntrenamiento;
            menuEntrenador.MostrarRival += CargarEntrenadorRival;

            // Cambiar el color del texto
            menuEntrenador.lblAlineacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón COMPETICION
        private void imgCompeticiones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Competicion menuCompeticion = new UC_Menu_Competicion();
            DockPanel_Submenu.Children.Add(menuCompeticion);

            // Suscribirse a los eventos
            menuCompeticion.MostrarClasificacion += CargarCompeticionClasificacion;
            menuCompeticion.MostrarResultados += CargarCompeticionResultados;
            menuCompeticion.MostrarEstadisticas += CargarCompeticionEstadisticas;
            menuCompeticion.MostrarPalmares += CargarCompeticionPalmares;
            menuCompeticion.MostrarPalmaresJugadores += CargarCompeticionPalmaresJugadores;

            // Cambiar el color del texto
            menuCompeticion.lblClasificacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion clasificacionPrincipal = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clasificacionPrincipal);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón CALENDARIO 
        private void imgCalendario_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Calendario menuCalendario = new UC_Menu_Calendario();
            DockPanel_Submenu.Children.Add(menuCalendario);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Calendario_Principal calendarioPrincipal = new UC_Menu_Calendario_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(calendarioPrincipal);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ESTADIO
        private void imgPabellon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Estadio menuEstadio = new UC_Menu_Estadio();
            DockPanel_Submenu.Children.Add(menuEstadio);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MÁNAGER
        private void imgManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Manager menuManager = new UC_Menu_Manager();
            DockPanel_Submenu.Children.Add(menuManager);

            // Suscribirse a los eventos MostrarPalmares y MostrarFicha
            menuManager.MostrarPalmares += CargarManagerPalmares;
            menuManager.MostrarFicha += CargarManagerFicha;

            // Cambiar el color del texto "Ficha" a naranja
            menuManager.lblFicha.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MENSAJES 
        private void imgCorreo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Correo menuCorreo = new UC_Menu_Correo();
            DockPanel_Submenu.Children.Add(menuCorreo);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Correo_Principal correoPrincipal = new UC_Menu_Correo_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(correoPrincipal);
        }
        // ------------------------------------------------------------------------------------------------------------

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

            if (reputacion == 100)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion >= 90)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion >= 70)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion >= 50)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion >= 25)
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

        // --------------------------------------------- MÉTODOS PARA LAS OPCIONES DE SUBMENÚ -----------------------------------------------
        // Método para cargar UC_Menu_Club_Informacion
        private void CargarClubInformacion()
        {
            // Cargar UC_Menu_Club_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }

        // Método para cargar UC_Menu_Club_Plantilla
        private void CargarClubPlantilla()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Plantilla clubPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
            DockPanel_Central.Children.Add(clubPlantilla);
        }

        // Método para cargar UC_Menu_Entrenador_Alineacion
        private void CargarEntrenadorAlineacion()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }

        // Método para cargar UC_Menu_Entrenador_Entrenamiento
        private void CargarEntrenadorEntrenamiento()
        {
            // Cargar UC_Menu_Entrenador_Tactica
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Entrenamiento entrenadorEntrenamiento = new UC_Menu_Entrenador_Entrenamiento(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorEntrenamiento);
        }

        // Método para cargar UC_Menu_Entrenador_Rival
        private void CargarEntrenadorRival()
        {
            // Cargar UC_Menu_Entrenador_Rival
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Rival entrenadorRival = new UC_Menu_Entrenador_Rival(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorRival);
        }

        // Método para cargar UC_Menu_Competicion_Clasificacion
        private void CargarCompeticionClasificacion()
        {
            // Cargar UC_Menu_Competicion_Clasificacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion competicionClasificacion = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionClasificacion);
        }

        // Método para cargar UC_Menu_Competicion_Resultados
        private void CargarCompeticionResultados()
        {
            // Cargar UC_Menu_Competicion_Resultados
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Resultados competicionResultados = new UC_Menu_Competicion_Resultados(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionResultados);
        }

        // Método para cargar UC_Menu_Competicion_Estadisticas
        private void CargarCompeticionEstadisticas()
        {
            // Cargar UC_Menu_Competicion_Estadisticas
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Estadisticas competicionEstadisticas = new UC_Menu_Competicion_Estadisticas(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionEstadisticas);
        }

        // Método para cargar UC_Menu_Competicion_Palmares
        private void CargarCompeticionPalmares()
        {
            // Cargar UC_Menu_Competicion_Palmares
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Palmares competicionPalmares = new UC_Menu_Competicion_Palmares(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionPalmares);
        }

        // Método para cargar UC_Menu_Competicion_Palmares_Jugadores
        private void CargarCompeticionPalmaresJugadores()
        {
            // Cargar UC_Menu_Competicion_Palmares_Jugadores
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Palmares_Jugadores competicionPalmaresJugadores = new UC_Menu_Competicion_Palmares_Jugadores(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionPalmaresJugadores);
        }

        // Método para cargar UC_Menu_Manager_Ficha
        private void CargarManagerFicha()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }

        // Método para cargar UC_Menu_Manager_Palmares
        private void CargarManagerPalmares()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Palmares managerPalmares = new UC_Menu_Manager_Palmares(_manager, _equipo);
            DockPanel_Central.Children.Add(managerPalmares);
        }

        // Método para cargar UC_Menu_Estadio_Informacion
        private void CargarEstadioInformacion()
        {
            // Cargar UC_Menu_Estadio_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }

        private void CargarFecha()
        {
            Fecha fechaObjeto = _datosFecha.ObtenerFechaHoy();
            DateTime hoy = DateTime.Parse(fechaObjeto.Hoy);
            // Formatear la fecha en español
            CultureInfo culturaEspañol = new CultureInfo("es-ES");
            string dia = hoy.ToString("dd", culturaEspañol); // Día
            string mes = hoy.ToString("MMM", culturaEspañol).ToUpper(); // Mes abreviado en español y en mayúsculas
            string año = hoy.ToString("yyyy", culturaEspañol); // Año

            // Combinar el formato
            string fechaFormateada = $"{dia} {mes} {año}";

            // Mostrar la fecha en el TextBlock
            txtFechaActual.Text = fechaFormateada;

            // Obtener el día de la semana en español
            string diaSemana = hoy.ToString("dddd", culturaEspañol);

            // Capitalizar la primera letra (opcional, si el formato por defecto no es suficiente)
            diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1);

            // Mostrar el día de la semana en el TextBlock
            txtDiaSemana.Text = diaSemana;

            // Comprobar si mi equipo juega hoy y cambiar el nombre al boton AVANZAR a PARTIDO
            Partido proximopartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, Metodos.hoy);
            if (proximopartido != null && proximopartido.FechaPartido == hoy)
            {
                btnAvanzar.Content = "PARTIDO";
            } else
            {
                btnAvanzar.Content = "AVANZAR";
            }
        }

        private void CalcularObjetivoTemporada(string objetivo, int posicion, int competicion)
        {
            if (competicion == 1)
            {
                if (objetivo.Equals("Campeón"))
                {
                    if (posicion <= 4)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 40, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 15)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 25, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
            }
            else
            {
                if (objetivo.Equals("Ascenso"))
                {
                    if (posicion <= 4)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 40, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 15)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 25, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -50, 0, 0);
                    }
                }
            }
        }
        #endregion
    }
}
