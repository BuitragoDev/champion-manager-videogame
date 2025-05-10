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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_FichaJugador : UserControl
    {
        #region "Variables"
        private int _jugador;
        private int _equipo;
        private int _opcion; // Variable que se usa para saber si el jugador es de mi equipo o es de otro.
        private Manager _manager;
        private UC_PantallaPrincipal _pantallaPrincipal;
        int opcionBotones = 1; // Variable para cambiar los botones
        int _ojeado;
        int operaciones = 0;
        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensaje = new MensajeLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        OjearLogica _logicaOjear = new OjearLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        public UC_FichaJugador(int jugador, int equipo, Manager manager, int opcion, UC_PantallaPrincipal pantallaPrincipal)
        {
            InitializeComponent();
            _jugador = jugador;
            _equipo = equipo;
            _manager = manager;
            _opcion = opcion;
            _pantallaPrincipal = pantallaPrincipal;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            bool ojeado = _logicaOjear.ComprobarJugadorOjeado(jugador.IdJugador, _manager);

            if (jugador.IdEquipo == _equipo)
            {
                CargarFichaJugador();
                opcionBotones = 1;  
            }
            else
            {
                if (ojeado)
                {
                    CargarFichaJugador();
                }
                else
                {
                    CargarFichaJugadorSinOjear();
                }

                opcionBotones = 2;
            }

            if (opcionBotones == 1)
            {
                btnVolverPlantilla.Visibility = Visibility.Visible;
                btnDespedir.Visibility = Visibility.Visible;
                btnRenovar.Visibility = Visibility.Visible;

                if (jugador.SituacionMercado != 0)
                {
                    btnPonerEnMercado.Visibility = Visibility.Collapsed;
                    btnQuitarDelMercado.Visibility = Visibility.Visible;
                } else
                {
                    btnPonerEnMercado.Visibility = Visibility.Visible;
                    btnQuitarDelMercado.Visibility = Visibility.Collapsed;
                }

                btnContratar.Visibility = Visibility.Collapsed;
                btnOjearJugador.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnVolverPlantilla.Visibility = Visibility.Visible;
                btnDespedir.Visibility = Visibility.Collapsed;
                btnRenovar.Visibility = Visibility.Collapsed;
                btnPonerEnMercado.Visibility = Visibility.Collapsed;
                btnQuitarDelMercado.Visibility = Visibility.Collapsed;
                btnContratar.Visibility = Visibility.Visible;
                btnOjearJugador.Visibility = Visibility.Visible;
            }

            List<Transferencia> ofertas = _logicaTransferencia.ListarTraspasos();
            DateTime fechaHoy = Metodos.hoy;
            DateTime fechaManiana = fechaHoy.AddDays(1);

            foreach (var oferta in ofertas)
            {
                if (!string.IsNullOrWhiteSpace(oferta.FechaTraspaso) &&
                    DateTime.TryParse(oferta.FechaTraspaso, out DateTime fechaTraspaso) &&
                    oferta.IdJugador == _jugador &&
                    fechaTraspaso == fechaManiana)
                {
                    btnContratar.IsEnabled = false;
                    btnOjearJugador.IsEnabled = false;
                }
            }
        }

        // --------------------------------------------------------------- Evento CLICK del BOTÓN VOLVER
        private void btnVolverPlantilla_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Obtener el DockPanel padre
            DockPanel? parentDockPanel = this.Parent as DockPanel;

            if (parentDockPanel != null)
            {
                // Limpiar el contenido actual
                parentDockPanel.Children.Clear();

                if (_opcion == 4)
                {
                    UC_Menu_Transferencias_BuscarPorEquipo menuTransferencias = new UC_Menu_Transferencias_BuscarPorEquipo(_manager, _equipo);
                    parentDockPanel.Children.Add(menuTransferencias);
                }
                else if (_opcion == 5)
                {
                    UC_Menu_Transferencias_BuscarPorFiltro menuTransferencias = new UC_Menu_Transferencias_BuscarPorFiltro(_manager, _equipo);
                    parentDockPanel.Children.Add(menuTransferencias);
                }
                else if (_opcion == 6)
                {
                    UC_Menu_Transferencias_Cartera menuCartera = new UC_Menu_Transferencias_Cartera(_manager, _equipo);
                    parentDockPanel.Children.Add(menuCartera);
                }
                else if (_opcion == 7)
                {
                    UC_Menu_Transferencias_EstadoOfertas menuEstadoOfertas = new UC_Menu_Transferencias_EstadoOfertas(_manager, _equipo);
                    parentDockPanel.Children.Add(menuEstadoOfertas);
                }
                else
                {
                    if (opcionBotones == 1)
                    {
                        UC_Menu_Club_Plantilla menuPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
                        parentDockPanel.Children.Add(menuPlantilla);
                    }
                    else
                    {
                        UC_Menu_Transferencias_Mercado menuTransferencias = new UC_Menu_Transferencias_Mercado(_manager, _equipo);
                        parentDockPanel.Children.Add(menuTransferencias);
                    }
                }
            }
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN RENOVAR
        private void btnRenovar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);

            int? aniosContrato = jugador.AniosContrato;
            DateTime? proximaNegociacion = jugador.ProximaNegociacion;
            string mensajeProximaNegociacion = proximaNegociacion.HasValue
                ? proximaNegociacion.Value.ToString("dd/MM/yyyy")
                : "(No hay una fecha disponible)";

            if (proximaNegociacion == null)
            {
                if (jugador.NombreCompleto != null)
                {
                    if (aniosContrato <= 3)
                    {
                        string titulo = "INFORMACIÓN";
                        string mensaje = "¿Estás seguro de renovar a " + jugador.NombreCompleto.ToUpper() + "?";

                        // Crear y mostrar la ventana emergente (2 = Renovar jugador)
                        frmVentanaRenovarJugador mensajeRenovar = new frmVentanaRenovarJugador(titulo, mensaje, jugador.IdJugador, 2);

                        bool? resultado = mensajeRenovar.ShowDialog(); // Capturar el resultado del diálogo

                        if (resultado == true) // Solo ejecutar si se pulsó Aceptar
                        {
                            // Crear una nueva instancia de la vista frmNegociacionesJugador (1 = renovacion)
                            frmNegociacionesJugador negociaciones = new frmNegociacionesJugador(jugador, _manager, _equipo, 1, _pantallaPrincipal);

                            // Suscribirse al evento Closed para ejecutar una acción cuando se cierre la ventana
                            negociaciones.Closed += (s, e) => RecargarDatos();

                            negociaciones.Show();
                        }
                    }
                    else
                    {
                        string titulo = "INFORMACIÓN";
                        string mensaje = "A " + jugador.NombreCompleto.ToUpper() + " aún le quedan " + aniosContrato + " años de contrato y no puede ser renovado.";

                        // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                        frmVentanaEmergenteDosBotones mensajeRenovacion = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                        mensajeRenovacion.ShowDialog();
                    }
                }
            }
            else
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "En estos momentos " + (jugador.NombreCompleto ?? "el jugador") + " no quiere reunirse contigo. A partir del próximo " + (proximaNegociacion?.ToString("dd/MM/yyyy") ?? "No disponible") + " puedes volver a intentarlo.";
                frmVentanaEmergenteDosBotones mensajeRenovacion = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                mensajeRenovacion.ShowDialog();
            }
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN DESPEDIR
        private void btnDespedir_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            int indemizacion = (jugador.SalarioTemporada ?? 0) * (jugador.AniosContrato ?? 0);
            if (jugador.NombreCompleto != null)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "¿Estás seguro de despedir a " + jugador.NombreCompleto.ToUpper() +
                                 "? Al despedirle antes de terminar su contrato deberás indemnizarle con " +
                                 ((decimal)(indemizacion)).ToString("N0", new CultureInfo("es-ES")) + " €";

                // Crear y mostrar la ventana emergente (1 = Despedir jugador)
                frmVentanaDespidoJugador mensajeDespedir = new frmVentanaDespidoJugador(titulo, mensaje, jugador.IdJugador, 1);

                bool? resultado = mensajeDespedir.ShowDialog(); // Capturar el resultado del diálogo

                if (resultado == true) // Solo ejecutar si se pulsó Aceptar
                {
                    // Restar indemnización del Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, indemizacion);

                    // Crear la entrada de Gasto en la tabla "finanzas" de la BD
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 12,
                        Tipo = 2,
                        Cantidad = indemizacion,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Crear el mensaje con el Despido de Jugador
                    string nombreJugador = jugador.Nombre + " " + jugador.Apellido;
                    string nombreManager = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " +
                                           _logicaManager.MostrarManager(_manager.IdManager).Apellido;
                    Mensaje mensajeDespido = new Mensaje
                    {
                        Fecha = Metodos.hoy,
                        Remitente = nombreJugador != null ? nombreJugador : "Desconocido",
                        Asunto = "Despido de Jugador",
                        Contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nViendo mi salida del equipo solo puedo decirte que no " +
                                    "son maneras de hacer las cosas. Aunque tuviera pocos minutos, mi trabajo en cada entreno no ha bajado " +
                                    "nada y he intentado ganarme mi lugar como los otros compañeros. No me merecía esta salida y creo que " +
                                    "lo sabes y comprendes mi enfado.\n\nEspero que le vaya bien al equipo, porque el club, la afición y " +
                                    "los demás compañeros se lo merecen. De ti ya me guardo la opinión...",
                        TipoMensaje = "Respuesta del Jugador",
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Leido = false,
                        Icono = jugador.IdJugador // Distinto de 0 es icono de jugador
                    };
                    _logicaMensaje.crearMensaje(mensajeDespido);

                    // Llamar al método ActualizarPresupuesto() de UC_PantallaPrincipal
                    _pantallaPrincipal.ActualizarPresupuesto();

                    // Obtener el DockPanel padre
                    DockPanel? parentDockPanel = this.Parent as DockPanel;

                    if (parentDockPanel != null)
                    {
                        // Limpiar el contenido actual
                        parentDockPanel.Children.Clear();

                        // Crear el nuevo UserControl y agregarlo al DockPanel
                        UC_Menu_Club_Plantilla menuPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
                        parentDockPanel.Children.Add(menuPlantilla);
                    }
                }
            }
        }

        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN PONER EN EL MERCADO
        private void btnPonerEnMercado_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            string titulo = "PONER EN MERCADO";
            string mensaje = "¿Dónde quieres poner a " + jugador.NombreCompleto.ToUpper() + "?";

            // Crear y mostrar la ventana emergente (1 = Despedir jugador)
            frmPonerEnMercado ponerEnMercado = new frmPonerEnMercado(titulo, mensaje);

            bool? resultado = ponerEnMercado.ShowDialog(); // Capturar el resultado del diálogo

            if (resultado == true) // Ejecutar si se pulsó Mercado de Transferibles
            {
                _logicaJugador.PonerJugadorEnMercado(jugador.IdJugador, 1);

                // Crear el mensaje con la respuesta del jugador
                string nombreJugador = jugador.Nombre + " " + jugador.Apellido;
                string nombreManager = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " +
                                       _logicaManager.MostrarManager(_manager.IdManager).Apellido;
                int moral = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).Moral;
                int animo = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).EstadoAnimo;
                string contenido = "";
                if (moral < 50 && animo < 40)
                {
                    contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nEstoy muy emocionado por esta oportunidad de estar en " +
                                "el mercado de transferibles. Siempre es gratificante saber que mi talento y esfuerzo están siendo " +
                                "reconocidos. Siento que he alcanzado un gran nivel esta temporada, y poder aspirar a nuevos horizontes " +
                                "me llena de ilusión. Estoy convencido de que puedo aportar mucho a cualquier equipo que apueste por " +
                                "mí, y estoy ansioso por demostrar mis habilidades en el campo.\n\nNo es una despedida fácil, pero estoy " +
                                "preparado para darlo todo en cualquier proyecto nuevo que se presente. Sea donde sea, seguiré " +
                                "trabajando con la misma dedicación y pasión por este deporte.";
                }
                else if (moral >= 50 && animo >= 40)
                {
                    contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nNo puedo creer que me hayan puesto en el mercado de " +
                                "transferibles sin tener una conversación previa conmigo. Me he esforzado al máximo en cada " +
                                "entrenamiento y partido, sacrificando muchas cosas por el bien del equipo. Sentía que había " +
                                "construido una relación sólida con el club y la afición, así que esto me ha pillado completamente " +
                                "por sorpresa.\n\nMe duele pensar que mi compromiso no ha sido valorado lo suficiente. Entiendo que el " +
                                "fútbol es un negocio, pero esperaba más respeto después de todo lo que he dado por esta camiseta. " +
                                "Voy a necesitar tiempo para procesar esto y pensar qué es lo mejor para mi futuro como jugador.";
                }

                Mensaje mensajeJugadorEnMercado = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : "Desconocido",
                    Asunto = "Jugador en el Mercado de Transferibles",
                    Contenido = contenido,
                    TipoMensaje = "Respuesta del Jugador",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = jugador.IdJugador // Distinto de 0 es icono de jugador
                };
                _logicaMensaje.crearMensaje(mensajeJugadorEnMercado);

                CargarFichaJugador();
            }
            else // Ejecutar si se pulsó Mercado de Cesiones
            {
                _logicaJugador.PonerJugadorEnMercado(jugador.IdJugador, 2);

                // Crear el mensaje con la respuesta del jugador
                string nombreJugador = jugador.Nombre + " " + jugador.Apellido;
                string nombreManager = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " +
                                       _logicaManager.MostrarManager(_manager.IdManager).Apellido;
                int moral = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).Moral;
                int animo = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).EstadoAnimo;
                string contenido = "";
                if (moral < 50 && animo < 40)
                {
                    contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nEstoy muy emocionado por esta oportunidad de estar en " +
                                "el mercado de transferibles. Siempre es gratificante saber que mi talento y esfuerzo están siendo " +
                                "reconocidos. Siento que he alcanzado un gran nivel esta temporada, y poder aspirar a nuevos horizontes " +
                                "me llena de ilusión. Estoy convencido de que puedo aportar mucho a cualquier equipo que apueste por " +
                                "mí, y estoy ansioso por demostrar mis habilidades en el campo.\n\nNo es una despedida fácil, pero estoy " +
                                "preparado para darlo todo en cualquier proyecto nuevo que se presente. Sea donde sea, seguiré " +
                                "trabajando con la misma dedicación y pasión por este deporte.";
                }
                else if (moral >= 50 && animo >= 40)
                {
                    contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nNo puedo creer que me hayan puesto en el mercado de " +
                                "transferibles sin tener una conversación previa conmigo. Me he esforzado al máximo en cada " +
                                "entrenamiento y partido, sacrificando muchas cosas por el bien del equipo. Sentía que había " +
                                "construido una relación sólida con el club y la afición, así que esto me ha pillado completamente " +
                                "por sorpresa.\n\nMe duele pensar que mi compromiso no ha sido valorado lo suficiente. Entiendo que el " +
                                "fútbol es un negocio, pero esperaba más respeto después de todo lo que he dado por esta camiseta. " +
                                "Voy a necesitar tiempo para procesar esto y pensar qué es lo mejor para mi futuro como jugador.";
                }

                Mensaje mensajeJugadorEnMercado = new Mensaje
                {
                    Fecha = Metodos.hoy,
                    Remitente = nombreJugador != null ? nombreJugador : "Desconocido",
                    Asunto = "Jugador en el Mercado de Cesiones",
                    Contenido = contenido,
                    TipoMensaje = "Respuesta del Jugador",
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Leido = false,
                    Icono = jugador.IdJugador // Distinto de 0 es icono de jugador
                };
                _logicaMensaje.crearMensaje(mensajeJugadorEnMercado);

                CargarFichaJugador();
            }

            btnPonerEnMercado.Visibility = Visibility.Collapsed;
            btnQuitarDelMercado.Visibility = Visibility.Visible;
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN QUITAR DEL MERCADO
        private void btnQuitarDelMercado_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            _logicaJugador.QuitarJugadorDeMercado(jugador.IdJugador);

            // Crear el mensaje con la respuesta del jugador
            string nombreJugador = jugador.Nombre + " " + jugador.Apellido;
            string nombreManager = _logicaManager.MostrarManager(_manager.IdManager).Nombre + " " +
                                   _logicaManager.MostrarManager(_manager.IdManager).Apellido;
            int moral = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).Moral;
            int animo = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).EstadoAnimo;
            string contenido = "";
            if (moral < 50 && animo < 40)
            {
                contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nNo puedo negar que estoy decepcionado por esta decisión. " +
                            "Sentía que salir del club era lo mejor para mi futuro, ya que estaba buscando nuevos desafíos y una " +
                            "oportunidad para crecer en otro entorno. Haber sido retirado del mercado de transferibles sin cumplir " +
                            "ese objetivo me ha dejado con una mezcla de frustración y desilusión.\n\nAunque siempre respetaré la camiseta " +
                            "que llevo, es difícil seguir motivado cuando sientes que no se han respetado tus deseos. Ahora tendré que " +
                            "reflexionar y decidir cómo manejar esta situación en adelante, pero mi mentalidad seguirá siendo " +
                            "profesional mientras esté aquí.";
            }
            else if (moral >= 50 && animo >= 40)
            {
                contenido = "Estimado/a " + nombreManager.ToUpper() + "\n\nMe siento aliviado y muy feliz de saber que ya no estoy en " +
                            "el mercado de transferibles. Siempre he querido seguir formando parte de este club, y la idea de salir " +
                            "nunca fue algo que me entusiasmara. Este equipo significa mucho para mí, y ahora que la situación está " +
                            "clara, puedo concentrarme al 100% en mi rendimiento.\n\nQuiero demostrarle al cuerpo técnico, a mis compañeros " +
                            "y a los aficionados que pertenezco aquí y que estoy dispuesto a darlo todo en el campo. Esta es una nueva " +
                            "oportunidad para seguir construyendo mi carrera en este club, y no la voy a desaprovechar.";
            }

            Mensaje mensajeJugadorEnMercado = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = nombreJugador != null ? nombreJugador : "Desconocido",
                Asunto = "Jugador deja de estar en el Mercado",
                Contenido = contenido,
                TipoMensaje = "Respuesta del Jugador",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = jugador.IdJugador // Distinto de 0 es icono de jugador
            };
            _logicaMensaje.crearMensaje(mensajeJugadorEnMercado);

            CargarFichaJugador();

            btnPonerEnMercado.Visibility = Visibility.Visible;
            btnQuitarDelMercado.Visibility = Visibility.Collapsed;
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN OJEAR JUGADOR
        private void btnOjearJugador_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Comprobar si se tiene contratado un Director Técnico.
            string mensaje = "";
            string titulo = "";
            bool encontrado = _logicaEmpleado.EmpleadoEncontrado("Director Técnico", _manager.IdManager);

            List<Jugador> listaJugadoresOjeados = _logicaOjear.ListadoJugadoresOjeados(_manager.IdManager);
            int numJugadoresOjeados = listaJugadoresOjeados.Count();

            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);

            if (encontrado)
            {

                if (numJugadoresOjeados >= 8)
                {
                    titulo = "¡ALERTA!";
                    mensaje = "Ya tienes el número máximo de jugadores en Cartera. Elimina alguno antes de ojear un nuevo jugador.";

                    // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                    frmVentanaEmergenteDosBotones mensajeOjeador = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                    mensajeOjeador.ShowDialog();
                }
                else
                {
                    titulo = "¡ALERTA!";
                    if (jugador.NombreCompleto != null)
                    {
                        // Comprobar si el jugador ya está siendo ojeado.
                        bool jugadorYaOjeado = _logicaOjear.ComprobarSiJugadorEnCartera(jugador.IdJugador, _manager);

                        if (jugadorYaOjeado)
                        {
                            mensaje = jugador.NombreCompleto.ToUpper() + " ya está siendo ojeado. Puedes ver los detalles entrando en tu cartera.";

                            // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                            frmVentanaEmergenteDosBotones mensajeOjeador = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                            mensajeOjeador.ShowDialog();
                        }
                        else
                        {
                            int categoria = 0;
                            int diasInforme = 0;
                            // Categoría del Director Técnico
                            List<Empleado> empleados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);
                            foreach (var empleado in empleados)
                            {
                                if (empleado.Puesto.Equals("Director Técnico"))
                                {
                                    categoria = empleado.Categoria;
                                }
                            }

                            if (categoria == 1)
                            {
                                diasInforme = 30;
                            }
                            else if (categoria == 2)
                            {
                                diasInforme = 25;
                            }
                            else if (categoria == 3)
                            {
                                diasInforme = 20;
                            }
                            else if (categoria == 4)
                            {
                                diasInforme = 15;
                            }
                            else if (categoria == 5)
                            {
                                diasInforme = 10;
                            }

                            mensaje = jugador.NombreCompleto.ToUpper() + " ha sido añadido a tu cartera. El día " + Metodos.hoy.AddDays(diasInforme).ToString("dd/MM/yyyy") + " tendrás disponible su ficha completa.";

                            // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                            frmVentanaEmergenteDosBotones mensajeOjeador = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                            mensajeOjeador.ShowDialog();

                            // Añadir el jugador a la cartera en la base de datos.
                            _logicaOjear.PonerJugadorCartera(jugador.IdJugador, _manager, diasInforme);
                        }
                    }
                }
            }
            else
            {
                titulo = "¡ALERTA!";
                mensaje = "No tienes ningún Director Técnico contratado. Para poder añadir este jugador a tu cartera debes contratar uno.";

                // Crear una nueva instancia de la vista frmVentanaEmergenteMensaje
                frmVentanaEmergenteDosBotones mensajeOjeador = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);

                mensajeOjeador.ShowDialog();
            }
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN CONTRATAR
        private void btnContratar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            DateTime? proximaNegociacion = jugador.ProximaNegociacion;

            // Comprobamos si tenemos contratado un Director Tecnico
            Empleado? director = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Director Técnico");

            if (director != null)
            {
                int[] numOperaciones = {
                        1, 2, 3, 4, 5
                    };

                if (director.Categoria >= 1 && director.Categoria <= 5)
                {
                    operaciones = numOperaciones[director.Categoria - 1];
                }

                List<Transferencia> ofertas = _logicaTransferencia.ListarOfertasSinFinalizar();
                int ofertasActivas = ofertas.Count();
                int jugadorConOferta = _logicaTransferencia.ComprobarRespuestaJugador(jugador.IdJugador, _equipo, jugador.IdEquipo);

                if (jugadorConOferta == 0)
                {
                    ofertasActivas = 0;
                }

                if (ofertasActivas >= operaciones)
                {
                    string titulo = "INFORMACIÓN";
                    string mensaje = $"Ya estás negociando con el número máximo de jugadores permitido por tu Director Técnico.";
                    frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                    mensajeEmergente.ShowDialog();
                } 
                else
                {
                    if (jugador.ProximaNegociacion > Metodos.hoy)
                    {
                        string titulo = "INFORMACIÓN";
                        string mensaje = "En estos momentos el " + (jugador.NombreEquipo ?? "el equipo") + " no quiere reunirse contigo. A partir del próximo " + (proximaNegociacion?.ToString("dd/MM/yyyy") ?? "No disponible") + " puedes volver a intentarlo.";
                        frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        mensajeEmergente.ShowDialog();
                    }
                    else
                    {
                        // No dejar realizar ofertas a jugadores que estan cedidos
                        int yaCedido = _logicaTransferencia.ComprobarRespuestaEquipoCesion(jugador.IdJugador, _equipo, jugador.IdEquipo);
                        if (yaCedido > 0)
                        {
                            string titulo = "INFORMACIÓN";
                            string mensaje = $"{jugador.NombreCompleto} ya ha aceptado una oferta de cesión y no puede recibir más ofertas hasta su finalización.";
                            frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            mensajeEmergente.ShowDialog();
                        }
                        else
                        {
                            // Comprobar si el jugador ya tiene una oferta activa.
                            int respuestaEquipoTraspaso = _logicaTransferencia.ComprobarRespuestaEquipo(jugador.IdJugador, _equipo, jugador.IdEquipo);
                            int respuestaEquipoCesion = _logicaTransferencia.ComprobarRespuestaEquipoCesion(jugador.IdJugador, _equipo, jugador.IdEquipo);

                            // Si es un jugador sin equipo
                            if (jugador.IdEquipo == 0)
                            {
                                respuestaEquipoTraspaso = 1;
                            }

                            if (respuestaEquipoTraspaso < 1 && respuestaEquipoCesion < 1)
                            {
                                // Crear una nueva instancia de la vista frmNegociacionesJugador
                                frmTraspasoJugador traspaso = new frmTraspasoJugador(jugador, _manager, _equipo, _pantallaPrincipal);
                                traspaso.Show();
                            }
                            else
                            {
                                // Comprobar si el equipo ya ha aceptado o rechazado la oferta.
                                int respuestaEquipo = _logicaTransferencia.ComprobarRespuestaEquipo(jugador.IdJugador, _equipo, jugador.IdEquipo);

                                if (respuestaEquipo == 0)
                                {
                                    // Crear una nueva instancia de la vista frmNegociacionesJugador
                                    frmTraspasoJugador traspaso = new frmTraspasoJugador(jugador, _manager, _equipo, _pantallaPrincipal);
                                    traspaso.Show();
                                }
                                else
                                {
                                    // Crear una nueva instancia de la vista frmNegociacionesJugador (2 = negociacion fichaje)
                                    frmNegociacionesJugador negociaciones = new frmNegociacionesJugador(jugador, _manager, _equipo, 2, _pantallaPrincipal);

                                    // Suscribirse al evento Closed para ejecutar una acción cuando se cierre la ventana
                                    negociaciones.Closed += (s, e) =>
                                    {
                                        _pantallaPrincipal.RecargarFichaJugador(_jugador, _equipo, _manager, _opcion);
                                    };
                                    negociaciones.Show();
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                string titulo = "INFORMACIÓN";
                string mensaje = $"No tienes ningún Director Técnico contratado. Para poder realizar ofertas por jugadores necesitas contratar uno.";
                frmVentanaEmergenteDosBotones mensajeEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                mensajeEmergente.ShowDialog();
            } 
        }
        // -----------------------------------------------------------------------------------------------

        #region "Métodos"
        private void RecargarDatos()
        {
            // Lógica para actualizar los datos en el UserControl
            CargarFichaJugador();
        }

        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos is int mediaValue)
            {
                if (mediaValue > 90)
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
                if (mediaValue >= 80)
                    return Brushes.DarkGreen; // Verde oscuro
                if (mediaValue >= 65)
                    return Brushes.Orange; // Naranja
                return Brushes.Red; // Rojo
            }
            return Brushes.Black; // Por defecto
        }

        private void CargarFichaJugador()
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);
            int situacion = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).SituacionMercado;
            bool esDeMiequipo = _logicaJugador.EsDeMiEquipo(jugador.IdJugador, _equipo);

            if (situacion == 0 && esDeMiequipo == true)
            {
                btnPonerEnMercado.Visibility = Visibility.Visible;
                btnQuitarDelMercado.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnPonerEnMercado.Visibility = Visibility.Collapsed;
                btnQuitarDelMercado.Visibility = Visibility.Visible;
            }

            // Cargar Datos del Jugador
            txtDorsal.Text = jugador.Dorsal.ToString();
            txtNombreJugador.Text = jugador.NombreCompleto;
            imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + jugador.IdJugador + ".png"));
            imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + jugador.IdEquipo + ".png"));
            lblAverage.Text = jugador.Media.ToString();
            elipseMedia.Stroke = DeterminarColorElipse(jugador.Media);

            txtNombreEquipo.Text = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre;
            txtPosicion.Text = jugador.Rol;
            txtAltura.Text = jugador.AlturaEnMetros.ToString("0.00 'm'");
            txtPeso.Text = jugador.Peso.ToString() + " kg";
            txtEdad.Text = jugador.Edad.ToString() + " años (" + jugador.FechaNacimiento.ToString("dd/MM/yyyy") + ")";
            txtNacionalidad.Text = jugador.Nacionalidad;
            txtValorMercado.Text = jugador.ValorMercado.ToString("N0") + " €";

            List<Transferencia> traspasos = _logicaTransferencia.ListarOfertas();
            foreach (var traspaso in traspasos)
            {
                if (traspaso.IdJugador == jugador.IdJugador && traspaso.TipoFichaje == 2)
                {
                    txtFuturoTraspaso.Text = $"📌 CEDIDO POR EL {_logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoOrigen).Nombre.ToUpper()}";
                    btnRenovar.IsEnabled = false;
                    btnDespedir.IsEnabled = false;
                    btnPonerEnMercado.IsEnabled = false;
                }
            }

            // Posición en el campo.
            int posicion = jugador.RolId;
            imgCoordenadas.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/demarcaciones/" + posicion + ".png"));

            // Estado Actual
            txtCondicionFisica.Text = jugador.EstadoForma.ToString();

            int moral = jugador.Moral;
            if (moral >= 70)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/arriba_icon.png"));
            }
            else if (moral >= 35)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/derecha_icon.png"));
            }
            else
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/abajo_icon.png"));
            }

            int estadoAnimo = jugador.EstadoAnimo;
            if (estadoAnimo >= 70)
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/arriba_icon.png"));
            }
            else if (estadoAnimo >= 35)
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/derecha_icon.png"));
            }
            else
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/abajo_icon.png"));
            }

            int situacionMercado = jugador.SituacionMercado;
            if (situacionMercado > 0)
            {
                imgSituacionMercado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/transferible_icon.png"));
            }
            else
            {
                imgSituacionMercado.Source = null;
            }

            int lesionado = jugador.Lesion;
            if (lesionado > 0)
            {
                imgLesionado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/lesion.png"));
            }
            else
            {
                imgLesionado.Source = null;
            }

            int status = jugador.Status;
            if (status == 1)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol1_icon.png"));
            }
            else if (status == 2)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol2_icon.png"));
            }
            else if (status == 3)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol3_icon.png"));
            }
            else if (status == 4)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol4_icon.png"));
            }

            // Contrato
            txtSalario.Text = jugador.SalarioTemporada > 0
                ? ((decimal)jugador.SalarioTemporada).ToString("N0") + " €"
                : "-";
            txtAniosContrato.Text = jugador.AniosContrato > 0
            ? jugador.AniosContrato.ToString() + " años"
            : "-";
            txtClausulaRescision.Text = jugador.ClausulaRescision > 0
            ? ((decimal)jugador.ClausulaRescision).ToString("N0") + " €"
            : "-";
            txtBonusPartido.Text = jugador.BonusPartido > 0
            ? ((decimal)jugador.BonusPartido).ToString("N0") + " €"
            : "-";
            txtBonusGoles.Text = jugador.BonusGoles > 0
            ? ((decimal)jugador.BonusGoles).ToString("N0") + " €"
            : "-";

            // Atributos
            txtVelocidad.Text = jugador.Velocidad.ToString();
            txtResistencia.Text = jugador.Resistencia.ToString();
            txtAgresividad.Text = jugador.Agresividad.ToString();
            txtCalidad.Text = jugador.Calidad.ToString();
            txtPotencial.Text = jugador.Potencial.ToString();

            txtPortero.Text = jugador.Portero.ToString();
            txtPase.Text = jugador.Pase.ToString();
            txtRegate.Text = jugador.Regate.ToString();
            txtRemate.Text = jugador.Remate.ToString();
            txtEntradas.Text = jugador.Entradas.ToString();
            txtTiro.Text = jugador.Tiro.ToString();
        }

        private void CargarFichaJugadorSinOjear()
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);

            // Cargar Datos del Jugador
            txtDorsal.Text = jugador.Dorsal.ToString();
            txtNombreJugador.Text = jugador.NombreCompleto;
            imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + jugador.IdJugador + ".png"));
            imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + jugador.IdEquipo + ".png"));
            lblAverage.Text = jugador.Media.ToString();
            elipseMedia.Stroke = DeterminarColorElipse(jugador.Media);

            txtNombreEquipo.Text = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre;
            txtPosicion.Text = jugador.Rol;
            txtAltura.Text = jugador.AlturaEnMetros.ToString("0.00 'm'");
            txtPeso.Text = jugador.Peso.ToString() + " kg";
            txtEdad.Text = jugador.Edad.ToString() + " años (" + jugador.FechaNacimiento.ToString("dd/MM/yyyy") + ")";
            txtNacionalidad.Text = jugador.Nacionalidad;
            txtValorMercado.Text = "";

            // Posición en el campo.
            int posicion = jugador.RolId;
            imgCoordenadas.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/demarcaciones/" + posicion + ".png"));

            // Estado Actual
            txtCondicionFisica.Text = jugador.EstadoForma.ToString();

            int moral = jugador.Moral;
            if (moral >= 70)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/arriba_icon.png"));
            }
            else if (moral >= 35)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/derecha_icon.png"));
            }
            else
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/abajo_icon.png"));
            }

            int estadoAnimo = jugador.EstadoAnimo;
            if (estadoAnimo >= 70)
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/arriba_icon.png"));
            }
            else if (estadoAnimo >= 35)
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/derecha_icon.png"));
            }
            else
            {
                imgEstadoAnimo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/abajo_icon.png"));
            }

            int situacionMercado = jugador.SituacionMercado;
            if (situacionMercado > 0)
            {
                imgSituacionMercado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/transferible_icon.png"));
            }
            else
            {
                imgSituacionMercado.Source = null;
            }

            int lesionado = jugador.Lesion;
            if (lesionado > 0)
            {
                imgLesionado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/lesion.png"));
            }
            else
            {
                imgLesionado.Source = null;
            }

            int status = jugador.Status;
            if (status == 1)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol1_icon.png"));
            }
            else if (status == 2)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol2_icon.png"));
            }
            else if (status == 3)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol3_icon.png"));
            }
            else if (status == 4)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol4_icon.png"));
            }

            // Atributos
            txtVelocidad.Text = jugador.Velocidad.ToString();
            txtResistencia.Text = jugador.Resistencia.ToString();
            txtAgresividad.Text = jugador.Agresividad.ToString();
            txtCalidad.Text = jugador.Calidad.ToString();
            txtPotencial.Text = jugador.Potencial.ToString();

            txtPortero.Text = jugador.Portero.ToString();
            txtPase.Text = jugador.Pase.ToString();
            txtRegate.Text = jugador.Regate.ToString();
            txtRemate.Text = jugador.Remate.ToString();
            txtEntradas.Text = jugador.Entradas.ToString();
            txtTiro.Text = jugador.Tiro.ToString();
        }

        

        #endregion
    }
}
