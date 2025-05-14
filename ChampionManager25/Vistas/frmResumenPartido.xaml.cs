using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmResumenPartido : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private Partido _partido;
        Equipo equipoLocal;
        Equipo equipoVisitante;

        public int copaFinalizada = 0;

        private static Random random = new Random(); //Random global
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        FechaDatos _datosFecha = new FechaDatos();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        TaquillaLogica _logicaTaquilla = new TaquillaLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();

        public frmResumenPartido(Manager manager, int equipo, Partido partido)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            this._partido = partido;

            // Código que inicializa el sonido de fondo 
            try
            {
                if (Metodos.SonidoActivado == true)
                {
                    Metodos.ReproducirMusica("backgroundMusic2.wav");
                } 
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resumenPartido_Loaded(object sender, RoutedEventArgs e)
        {
            int comp = _partido.IdCompeticion; // IdCompeticion del primer partido
            int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;

            if (comp == 4)
            {
                int ronda = _partido.Ronda ?? 0; // Ronda de Copa

                string nombreRonda = _logicaPartidos.ObtenerNombreRonda(ronda);
                string ruta_comp = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
                imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                lblTituloVentana.Text += $" ({nombreRonda})";
            }
            else if (comp >= 1 && comp <= 2)
            {
                int jornada = _partido.Jornada ?? 0; // Jornada de Liga
                string ruta_comp = _logicaCompeticion.ObtenerCompeticion(miCompeticion).RutaImagen80;
                imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                lblTituloVentana.Text += $" (Jornada {jornada})";
            }

            equipoLocal = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal);
            equipoVisitante = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoVisitante);
            // Cargar datos basicos del partido
            txtEquipoLocal.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Nombre.ToUpper();
            txtEquipoVisitante.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoVisitante).Nombre.ToUpper();
            imgEscudoEquipoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaImagen120));
            imgEscudoEquipoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoVisitante.RutaImagen120));
            txtEstadio.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Estadio;
            imgEstadio.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaEstadioInterior));
            txtAforo.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Aforo.ToString("N0") + " espectadores";

            List<Jugador> todosLosJugadores = _logicaJugador.ListadoJugadoresCompleto(_partido.IdEquipoLocal)
                                                      .Concat(_logicaJugador.ListadoJugadoresCompleto(_partido.IdEquipoVisitante))
                                                      .ToList();

            // Actualizar sanciones y lesiones
            ActualizarSancionesYLesiones(todosLosJugadores);

            // Actualizar atributos por entrenamiento
            List<Jugador> misJugadores = _logicaJugador.ListadoJugadoresCompleto(_equipo);
            ActualizarEntrenamiento(misJugadores);

            // Simular partido
            SimularPartido(_partido);

            // Comprobar si es la final de Copa
            if (_partido.IdCompeticion == 4)
            {
                int ronda = _partido.Ronda ?? 0; // Ronda de Copa
                if (ronda > 5)
                {
                    copaFinalizada = 1;
                }
            }

            btnAvanzar.IsEnabled = true;
        }

        private void resumenPartido_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Metodos.SonidoActivado == true)
            {
                Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
            }
        }

        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region "Metodos"
        private void SimularPartido(Partido partido)
        {
            List<Jugador> jugadoresLocal;
            List<Jugador> jugadoresVisitante;
            bool soyLocal = partido.IdEquipoLocal == _equipo;

            if (soyLocal)
            {
                jugadoresLocal = _logicaJugador.JugadoresMiEquipoJueganPartido(partido.IdEquipoLocal);
                jugadoresVisitante = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoVisitante);
            }
            else
            {
                jugadoresLocal = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoLocal);
                jugadoresVisitante = _logicaJugador.JugadoresMiEquipoJueganPartido(partido.IdEquipoVisitante);
            }

            // Solo tu equipo puede tener penalizaciones
            bool yoSinPortero = !_logicaJugador.TengoPortero(_equipo);
            bool yoSinDefensas = !_logicaJugador.TengoDefensas(_equipo);
            bool yoSinDelanteros = !_logicaJugador.TengoDelanteros(_equipo);

            // Penalizaciones propias
            bool penalizarAtaqueLocal = soyLocal && yoSinDelanteros;
            bool penalizarDefensaLocal = soyLocal && (yoSinPortero || yoSinDefensas);

            bool penalizarAtaqueVisitante = !soyLocal && yoSinDelanteros;
            bool penalizarDefensaVisitante = !soyLocal && (yoSinPortero || yoSinDefensas);

            // Simular goles
            int golesLocal = CalcularGoles(
                jugadoresLocal,
                jugadoresVisitante,
                penalizarAtaqueLocal,
                penalizarDefensaVisitante // penaliza al rival solo si TU defensa está mal
            );

            int golesVisitante = CalcularGoles(
                jugadoresVisitante,
                jugadoresLocal,
                penalizarAtaqueVisitante,
                penalizarDefensaLocal // penaliza al rival solo si TU defensa está mal
            );

            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;

            txtGolesLocal.Text = golesLocal.ToString();
            txtGolesVisitante.Text = golesVisitante.ToString();

            // Asignar goleadores y asistentes
            List<(Jugador, Jugador?)> golesYAsistencias = AsignarGolesYAsistencias(golesLocal, jugadoresLocal, random)
                .Concat(AsignarGolesYAsistencias(golesVisitante, jugadoresVisitante, random))
                .ToList();

            // Asignar tarjetas
            List<(Jugador, string)> tarjetas = AsignarTarjetas(jugadoresLocal, jugadoresVisitante, random);

            // Determinar MVP
            Jugador mvp = DeterminarMVP(golesYAsistencias, jugadoresLocal, jugadoresVisitante);
            txtDetallesMvp.Text = $"{_logicaJugador.MostrarDatosJugador(mvp.IdJugador).Rol.ToUpper()}";
            txtNombreMvp.Text = _logicaJugador.MostrarDatosJugador(mvp.IdJugador).NombreCompleto;
            imgMvp.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + mvp.RutaImagen));
            // Contar goles y asistencias del MVP
            int golesMVP = golesYAsistencias.Count(ga => ga.Item1.IdJugador == mvp.IdJugador);
            int asistenciasMVP = golesYAsistencias.Count(ga => ga.Item2 != null && ga.Item2.IdJugador == mvp.IdJugador);
            txtEstadisticasMvp.Text = $"{golesMVP} {(golesMVP == 1 ? "gol" : "goles")} / {asistenciasMVP} {(asistenciasMVP == 1 ? "asistencia" : "asistencias")}";

            // Calcular asistencia al estadio
            partido.Asistencia = _logicaEquipo.CalcularAsistencia(partido.IdEquipoLocal);
            txtAsistencia.Text = (partido.Asistencia?.ToString("N0") ?? "0") + " espectadores";

            // Calcular recaudacion
            Taquilla taquilla = _logicaTaquilla.RecuperarPreciosTaquilla(_equipo, _manager.IdManager);
            double? recaudacion = taquilla.PrecioEntradaGeneral * (partido.Asistencia * 0.50) + 
                                 taquilla.PrecioEntradaTribuna * (partido.Asistencia * 0.40) + 
                                 taquilla.PrecioEntradaVip * (partido.Asistencia * 0.10);

            // Redondeamos a un número entero
            int recaudacionEntera = recaudacion.HasValue
                        ? (int)Math.Round(recaudacion.Value)  // Redondeamos el valor y lo convertimos a int
                        : 0;  // Si es null, asignamos 0

            txtRecaudacion.Text = recaudacionEntera.ToString("N0") + " €"; // o " E" si prefieres la E

            // ---------------- SUMAR LA RECAUDACION DE LA TAQUILLA
            if (partido.IdEquipoLocal == _equipo)
            {
                // Crear el ingreso
                Finanza nuevoIngreso = new Finanza
                {
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Temporada = Metodos.temporadaActual.ToString(),
                    IdConcepto = 1,
                    Tipo = 1,
                    Cantidad = recaudacionEntera,
                    Fecha = Metodos.hoy.Date
                };
                _logicaFinanza.CrearIngreso(nuevoIngreso);

                // Sumar los ingresos al Presupuesto
                _logicaEquipo.SumarCantidadAPresupuesto(_equipo, recaudacionEntera);
            }

            // ---------------- CREAR INGRESO EXTRA TELEVISION POR PARTIDO DE COPA
            if (partido.IdEquipoLocal == _equipo && partido.IdCompeticion == 4)
            {
                // Crear el ingreso
                Finanza nuevoIngreso = new Finanza
                {
                    IdEquipo = _equipo,
                    IdManager = _manager.IdManager,
                    Temporada = Metodos.temporadaActual.ToString(),
                    IdConcepto = 3,
                    Tipo = 1,
                    Cantidad = 1500000,
                    Fecha = Metodos.hoy.Date
                };
                _logicaFinanza.CrearIngreso(nuevoIngreso);

                // Sumar los ingresos al Presupuesto
                _logicaEquipo.SumarCantidadAPresupuesto(_equipo, recaudacionEntera);
            }

            // ACTUALIZAR RESULTADO SI ES UN AMISTOSO
            if (partido.IdCompeticion == 10)
            {
                _logicaPartidos.ActualizarPartido(partido);
            }

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE LIGA
            if (partido.IdCompeticion >= 1 && partido.IdCompeticion <= 2)
            {
                _logicaPartidos.ActualizarPartido(partido);

                // Actualizar la clasificacion
                Clasificacion cla_local;
                Clasificacion cla_visitante;
                if (golesLocal == golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 0
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 0
                    };
                }
                else if (golesLocal > golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 1,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 1,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = -1
                    };
                }
                else
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 1,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = -1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 1,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 1
                    };
                }

                if (partido.IdCompeticion == 1)
                {
                    // Actualizar la Clasificacion de la Division de los equipos de mi equipo
                    _logicaClasificacion.ActualizarClasificacion(cla_local);
                    _logicaClasificacion.ActualizarClasificacion(cla_visitante);
                }
                else
                {
                    // Actualizar la Clasificacion de la Division de los equipos de mi equipo
                    _logicaClasificacion.ActualizarClasificacion2(cla_local);
                    _logicaClasificacion.ActualizarClasificacion2(cla_visitante);
                }

                // Actualizar estadísticas de cada jugador en la base de datos
                ActualizarEstadisticasPartido(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);

                // Comprobar si ha habido algun lesionado y actualizarlo en la BD
                List<int> lesionados = SimularLesiones(jugadoresLocal, jugadoresVisitante);
                foreach (int jugador in lesionados)
                {
                    List<(string Descripcion, int MinSemanas, int MaxSemanas)> lesiones = new List<(string, int, int)>
                    {
                        ("Contusión leve", 1, 1),
                        ("Distensión muscular leve", 2, 3),
                        ("Contractura lumbar", 2, 4),
                        ("Esguince de tobillo grado 1", 2, 4),
                        ("Tendinitis rotuliana", 3, 5),
                        ("Rotura fibrilar leve", 3, 5),
                        ("Esguince de rodilla grado 2", 5, 7),
                        ("Fractura de dedo del pie", 6, 8),
                        ("Rotura fibrilar moderada", 6, 9),
                        ("Desgarro isquiotibial", 7, 10),
                        ("Lesión del ligamento colateral medial", 8, 11),
                        ("Fractura de costilla", 9, 12),
                        ("Lesión meniscal", 10, 14),
                        ("Luxación de hombro", 11, 15),
                        ("Rotura parcial del ligamento cruzado anterior", 13, 17),
                        ("Fractura de metatarsiano", 14, 18),
                        ("Rotura de menisco con cirugía", 16, 20),
                        ("Rotura completa del ligamento cruzado anterior", 20, 28),
                        ("Fractura de tibia", 25, 32),
                        ("Doble rotura ligamentosa con cirugía", 30, 40)
                    };

                    int numeroAleatorio = random.Next(1, 41); // de 1 a 40
                    var lesion = lesiones.FirstOrDefault(l => numeroAleatorio >= l.MinSemanas && numeroAleatorio <= l.MaxSemanas);

                    // En caso de no encontrar ninguna (aunque no debería pasar con rangos bien cubiertos)
                    if (lesion.Descripcion == null)
                    {
                        lesion = ("Lesión desconocida", numeroAleatorio, numeroAleatorio);
                    }

                    _logicaJugador.PonerJugadorLesionado(jugador, numeroAleatorio, lesion.Descripcion);

                    // Si es un jugador de mi equipo...
                    if (_logicaJugador.EsDeMiEquipo(jugador, _equipo))
                    {
                        // Crear el mensaje
                        Mensaje mensajeInicio = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = _logicaJugador.MostrarDatosJugador(jugador).NombreCompleto,
                            Asunto = "Jugador Lesionado",
                            Contenido = "Desde el equipo médico del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te informamos de que " + _logicaJugador.MostrarDatosJugador(jugador).NombreCompleto + " se ha lesionado (" + lesion.Descripcion + "), y permanecerá de baja durante " + numeroAleatorio + " semanas.",
                            TipoMensaje = "Notificación",
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Leido = false,
                            Icono = jugador // 0 es icono de equipo
                        };

                        _logicaMensajes.crearMensaje(mensajeInicio);
                    }
                }

                // Actualizar la BD de jugadores sancionados
                foreach (var tarjeta in tarjetas)
                {
                    Jugador jugador = tarjeta.Item1;
                    string tipoTarjeta = tarjeta.Item2;

                    // Comprobar cuantas amarillas tiene y si es multiplo de 2 se le aplica 1 partido de sancion
                    Estadistica statJugador = _logicaEstadisticas.MostrarEstadisticasJugador(jugador.IdJugador, _manager.IdManager);

                    if (tipoTarjeta == "amarilla" || tipoTarjeta == "dobleamarilla")
                    {
                        if (statJugador.TarjetasAmarillas % 5 == 0)
                        {
                            _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, 1);

                            // Si es un jugador de mi equipo...
                            if (_logicaJugador.EsDeMiEquipo(jugador.IdJugador, _equipo))
                            {
                                // Crear el mensaje
                                Mensaje mensajeInicio = new Mensaje
                                {
                                    Fecha = Metodos.hoy,
                                    Remitente = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto,
                                    Asunto = "Jugador Sancionado",
                                    Contenido = "Como delegado del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te informo de que " + _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto + " ha sido sancionado con 1 partidos sin poder jugar por acumulación de tarjetas amarillas.",
                                    TipoMensaje = "Notificación",
                                    IdEquipo = _equipo,
                                    IdManager = _manager.IdManager,
                                    Leido = false,
                                    Icono = jugador.IdJugador // 0 es icono de equipo
                                };

                                _logicaMensajes.crearMensaje(mensajeInicio);
                            }
                        }
                    }
                    else if (tipoTarjeta == "roja")
                    {
                        _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, 2);

                        // Si es un jugador de mi equipo...
                        if (_logicaJugador.EsDeMiEquipo(jugador.IdJugador, _equipo))
                        {
                            // Crear el mensaje
                            Mensaje mensajeInicio = new Mensaje
                            {
                                Fecha = Metodos.hoy,
                                Remitente = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto,
                                Asunto = "Jugador Sancionado",
                                Contenido = "Como delegado del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te informo de que " + _logicaJugador.MostrarDatosJugador(jugador.IdJugador).NombreCompleto + " ha sido sancionado con 2 partidos sin poder jugar tras haber recibido una tarjeta roja.",
                                TipoMensaje = "Notificación",
                                IdEquipo = _equipo,
                                IdManager = _manager.IdManager,
                                Leido = false,
                                Icono = jugador.IdJugador // 0 es icono de equipo
                            };

                            _logicaMensajes.crearMensaje(mensajeInicio);
                        }
                    } 
                }

                // Actualizar las confianzas
                ActualizarConfianzaManager(partido, golesLocal, golesVisitante);

                // Actualizar atributos de los jugadores
                ActualizacionAtributos(jugadoresLocal, jugadoresVisitante, partido.IdEquipoLocal, 
                                       partido.IdEquipoVisitante, golesLocal, golesVisitante, golesYAsistencias, mvp);
            }

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE COPA
            if (partido.IdCompeticion == 4)
            {
                _logicaPartidos.ActualizarPartidoCopaNacional(partido);
            }

            // Mostrar los goleadores y amonestados en la ventana.
            ActualizarPanels(golesYAsistencias, tarjetas);
        }

        private int CalcularGoles(List<Jugador> jugadores, List<Jugador> jugadoresRival,
                          bool penalizarAtaque, bool rivalConDefensaDebil)
        {
            if (jugadores.Count == 0 || jugadoresRival.Count == 0)
                return 0;

            // Nivel ofensivo del equipo que ataca
            double ataque = jugadores.Average(j =>
                (j.Remate + j.Pase + j.Calidad + j.Tiro + j.Regate + j.Velocidad) / 6.0);

            // Penalización si NO tienes delanteros
            if (penalizarAtaque)
                ataque *= 0.5;

            // Nivel defensivo del rival
            double defensaRival = jugadoresRival.Average(j =>
                (j.Entradas + j.Resistencia + j.Agresividad + j.Velocidad) / 4.0);

            // Si la defensa del rival está mal (porque eres tú sin portero o sin defensas)
            if (rivalConDefensaDebil)
                defensaRival *= 0.5;

            // Diferencia entre ataque y defensa
            double diferencia = ataque - defensaRival;
            double factor = 0.5 + (diferencia / 10.0);
            factor = Math.Clamp(factor, 0.2, 1.2); // para evitar resultados extremos

            // Cálculo de goles esperados con un poco de aleatoriedad
            double golesEsperados = factor * 2.0;
            double variacion = (random.NextDouble() * 2.0) - 1.0;
            int goles = (int)Math.Round(golesEsperados + variacion);

            return Math.Clamp(goles, 0, 7);
        }

        private List<(Jugador, Jugador?)> AsignarGolesYAsistencias(int goles, List<Jugador> jugadores, Random random)
        {
            List<(Jugador, Jugador?)> lista = new List<(Jugador, Jugador?)>();

            // Filtrar jugadores que no sean porteros
            var jugadoresNoPorteros = jugadores.Where(j => j.RolId != 1).ToList();

            // Asignar pesos basados en atributos y posición
            var pesosGoleadores = jugadoresNoPorteros.Select(j =>
                                                                (jugador: j, peso: (j.Remate * 1.5 + j.Tiro * 1.5 + j.Regate * 1.5 + j.Calidad) *
                                                                (j.RolId == 10 ? 10 : (j.RolId >= 7 && j.RolId <= 9 ? 5 : 0.5))) // RolId 10 tiene un peso de 10, y 7-9 tienen un peso de 5
                                                             ).ToList();

            var pesosAsistentes = jugadoresNoPorteros.Select(j =>
                (jugador: j, peso: (j.Pase * 1.5 + j.Calidad) * (j.RolId >= 6 && j.RolId <= 10 ? 2 : 1))
            ).ToList();

            // Normalizar pesos
            double totalPesoGoleador = pesosGoleadores.Sum(p => p.peso);
            double totalPesoAsistente = pesosAsistentes.Sum(p => p.peso);

            for (int i = 0; i < goles; i++)
            {
                // Selección ponderada de goleador
                Jugador goleador = SeleccionarJugadorPonderado(pesosGoleadores, totalPesoGoleador, random);

                // 80% de probabilidad de asistencia
                Jugador? asistente = random.NextDouble() > 0.2 ?
                    SeleccionarJugadorPonderado(pesosAsistentes, totalPesoAsistente, random) : null;

                lista.Add((goleador, asistente));

                // -------------- CREAR GASTO POR GOL
                Jugador player = _logicaJugador.MostrarDatosJugador(goleador.IdJugador);

                // Bonus
                int bonusGoles = player.BonusGoles ?? 0;
                if (bonusGoles != 0)
                {
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 16,
                        Tipo = 2,
                        Cantidad = bonusGoles,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, bonusGoles);
                }
            }

            return lista;
        }

        // Método para seleccionar un jugador de forma ponderada
        private Jugador SeleccionarJugadorPonderado(List<(Jugador jugador, double peso)> listaPesos, double totalPeso, Random random)
        {
            double valorAleatorio = random.NextDouble() * totalPeso;
            double suma = 0;

            foreach (var (jugador, peso) in listaPesos)
            {
                suma += peso;
                if (valorAleatorio <= suma)
                    return jugador;
            }

            return listaPesos.Last().jugador; // En caso de error, devolver el último
        }
        private List<(Jugador, string)> AsignarTarjetas(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante, Random random)
        {
            List<(Jugador, string)> tarjetas = new List<(Jugador, string)>();

            // Calcular cuántas tarjetas habrá (máximo 6 por equipo)
            int totalTarjetasLocal = random.Next(0, 6);
            int totalTarjetasVisitante = random.Next(0, 6);

            // Asignar tarjetas a cada equipo
            AsignarTarjetasEquipo(jugadoresLocal, totalTarjetasLocal, tarjetas, random);
            AsignarTarjetasEquipo(jugadoresVisitante, totalTarjetasVisitante, tarjetas, random);

            return tarjetas;
        }
        private void AsignarTarjetasEquipo(List<Jugador> jugadores, int totalTarjetas, List<(Jugador, string)> tarjetas, Random random)
        {
            // Limitar el número de tarjetas al número de jugadores disponibles
            totalTarjetas = Math.Min(totalTarjetas, jugadores.Count);

            // Seleccionamos jugadores más agresivos para recibir tarjetas
            List<Jugador> jugadoresAgresivos = jugadores
                        .Where(j => j.Agresividad > 60)
                        .OrderByDescending(j => j.Agresividad)
                        .Take(totalTarjetas)
                        .ToList();

            Dictionary<int, int> tarjetasRecibidas = new Dictionary<int, int>(); // Control de tarjetas por jugador

            for (int i = 0; i < totalTarjetas; i++)
            {
                Jugador jugador = jugadoresAgresivos[i];

                // Ver cuántas tarjetas tiene ya este jugador
                if (!tarjetasRecibidas.ContainsKey(jugador.IdJugador))
                    tarjetasRecibidas[jugador.IdJugador] = 0;

                double probabilidad = random.NextDouble();

                if (probabilidad <= 0.50) // 50% de probabilidad de recibir una amarilla
                {
                    // Si el jugador ya tiene amarilla, solo 10% de probabilidad de recibir otra
                    if (tarjetasRecibidas[jugador.IdJugador] == 1 && random.NextDouble() > 0.20)
                        continue;

                    tarjetas.Add((jugador, "amarilla"));
                    tarjetasRecibidas[jugador.IdJugador]++;

                    // Si el jugador tiene 2 amarillas, se convierte en roja
                    if (tarjetasRecibidas[jugador.IdJugador] == 2)
                    {
                        tarjetas.Add((jugador, "dobleamarilla")); // Segunda amarilla y roja
                        tarjetasRecibidas[jugador.IdJugador] = 3; // Para evitar más tarjetas
                    }
                }
                else if (probabilidad > 0.95) // 5% de probabilidad de roja directa
                {
                    if (tarjetasRecibidas[jugador.IdJugador] == 0) // No dar roja si ya tiene amarilla
                    {
                        tarjetas.Add((jugador, "roja"));
                        tarjetasRecibidas[jugador.IdJugador] = 3; // Para evitar más tarjetas
                    }
                }
            }
        }


        private Jugador DeterminarMVP(List<(Jugador, Jugador?)> golesYAsistencias, List<Jugador> local, List<Jugador> visitante)
        {
            Dictionary<Jugador, int> puntuaciones = new Dictionary<Jugador, int>();

            foreach ((Jugador goleador, Jugador? asistente) in golesYAsistencias)
            {
                if (!puntuaciones.ContainsKey(goleador)) puntuaciones[goleador] = 0;
                puntuaciones[goleador] += 3;

                if (asistente != null)
                {
                    if (!puntuaciones.ContainsKey(asistente)) puntuaciones[asistente] = 0;
                    puntuaciones[asistente] += 2;
                }  
            }

            // Si hay puntuaciones, devolver el jugador con más puntos
            if (puntuaciones.Count > 0)
            {
                return puntuaciones.OrderByDescending(p => p.Value).First().Key;
            }
            else
            {
                // Si no hay goleadores ni asistentes, elegir un jugador aleatorio de los que jugaron
                List<Jugador> todosJugadores = local.Concat(visitante).ToList();
                if (todosJugadores.Count == 0) throw new Exception("No hay jugadores disponibles para elegir MVP.");

                Random random = new Random();
                return todosJugadores[random.Next(todosJugadores.Count)];
            }
        }

        private void ActualizarEstadisticasPartido(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante,
                                           List<(Jugador, Jugador?)> golesYAsistencias,
                                           List<(Jugador, string)> tarjetas,
                                           Jugador mvp)
        {
            Dictionary<int, Estadistica> estadisticas = new Dictionary<int, Estadistica>();

            // Asegurar que todos los jugadores del partido sumen 1 partido jugado
            foreach (var jugador in jugadoresLocal.Concat(jugadoresVisitante))
            {
                estadisticas[jugador.IdJugador] = new Estadistica
                {
                    IdJugador = jugador.IdJugador,
                    PartidosJugados = 1, // Todos los jugadores suman 1 partido
                    Goles = 0,
                    Asistencias = 0,
                    TarjetasAmarillas = 0,
                    TarjetasRojas = 0,
                    MVP = 0
                };

                // -------------- CREAR GASTO POR PARTIDO
                Jugador player = _logicaJugador.MostrarDatosJugador(jugador.IdJugador);

                // Bonus
                int bonusPartido = player.BonusPartido ?? 0;
                if (bonusPartido != 0)
                {
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 16,
                        Tipo = 2,
                        Cantidad = bonusPartido,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, bonusPartido);
                }
            }

            // Sumar goles y asistencias
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                estadisticas[goleador.IdJugador].Goles++;

                if (asistente != null)
                {
                    estadisticas[asistente.IdJugador].Asistencias++;
                }
            }

            // Sumar tarjetas
            foreach (var (jugador, tipoTarjeta) in tarjetas)
            {
                if (tipoTarjeta.Contains("roja")) estadisticas[jugador.IdJugador].TarjetasRojas++;
                if (tipoTarjeta.Contains("amarilla")) estadisticas[jugador.IdJugador].TarjetasAmarillas++;
            }

            // Sumar MVP
            estadisticas[mvp.IdJugador].MVP++;

            // Guardar estadísticas en la base de datos
            foreach (var estadistica in estadisticas.Values)
            {
                _logicaEstadisticas.ActualizarEstadisticas(estadistica);
            }
        }

        public List<int> SimularLesiones(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante)
        {
            List<int> lesionados = new List<int>();

            // Recorrer jugadores locales y visitantes
            foreach (var jugador in jugadoresLocal)
            {
                if (random.Next(0, 51) == 13) // Generar número entre 0 y 50, si es 13 -> lesión
                {
                    lesionados.Add(jugador.IdJugador);
                }
            }

            foreach (var jugador in jugadoresVisitante)
            {
                if (random.Next(0, 51) == 13)
                {
                    lesionados.Add(jugador.IdJugador);
                }
            }

            return lesionados; // Devuelve la lista con los ID de jugadores lesionados
        }

        private void ActualizarSancionesYLesiones(List<Jugador> jugadores)
        {
            foreach (var jugador in jugadores)
            {
                // Reducir el número de partidos sancionados si es mayor que 0
                if (jugador.Sancionado > 0)
                {
                    _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, jugador.Sancionado - 1);
                }
            }
        }

        private void ActualizarConfianzaManager(Partido partido, int golesLocal, int golesVisitante)
        {
            int miEquipo, rival, golesEquipo, golesRival;
            // Comprobamos si mi equipo es el Local o el Visitante
            if (partido.IdEquipoLocal == _equipo)
            {
                miEquipo = partido.IdEquipoLocal;
                rival = partido.IdEquipoVisitante;
                golesEquipo = golesLocal;
                golesRival = golesVisitante;
            }
            else
            {
                rival = partido.IdEquipoLocal;
                miEquipo = partido.IdEquipoVisitante;
                golesRival = golesLocal;
                golesEquipo = golesVisitante;
            }

            // Actualizamos las confianzas
            int rankingMiEquipo = ObtenerPuestoEquipo(miEquipo, 1, _manager.IdManager);
            int rankingRival = ObtenerPuestoEquipo(rival, 1, _manager.IdManager);
            int rivalidad = _logicaEquipo.ListarDetallesEquipo(miEquipo).Rival;
            int diferenciaGoles = Math.Abs(golesEquipo - golesRival);

            // Base de confianza (valores iniciales)
            int cambioDirectiva = 0;
            int cambioFans = 0;
            int cambioJugadores = 0;

            bool esRivalHistorico = rival == rivalidad;
            bool victoria = golesEquipo > golesRival;
            bool derrota = golesEquipo < golesRival;
            bool empate = golesEquipo == golesRival;
            bool goleada = diferenciaGoles >= 3;

            if (victoria)
            {
                // Directiva: evalúa rendimiento según fuerza del rival
                cambioDirectiva = rankingMiEquipo > rankingRival ? 4 : 2;
                // Fans: celebran victoria, más si es contra rival histórico
                cambioFans = 4;
                if (esRivalHistorico) cambioFans += 6;
                // Jugadores: sube moral por victoria
                cambioJugadores = 3;

                if (goleada)
                {
                    cambioDirectiva += 3;
                    cambioFans += 5;
                    cambioJugadores += 3;
                }

                if (esRivalHistorico)
                {
                    cambioDirectiva += 3;
                    cambioJugadores += 2;
                }

                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 1, 0, 0, 3);
                _logicaManager.ActualizarManagerTemporal(new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 1,
                    PartidosEmpatados = 0,
                    PartidosPerdidos = 0,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                });
            }
            else if (derrota)
            {
                cambioDirectiva = rankingMiEquipo < rankingRival ? -2 : -4;
                cambioFans = -4;
                cambioJugadores = -3;

                if (goleada)
                {
                    cambioFans -= 2;
                    cambioJugadores -= 1;
                }

                if (esRivalHistorico)
                {
                    cambioFans -= 6;
                    cambioDirectiva -= 2;
                    cambioJugadores -= 2;
                }

                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 0, 0, 1, 0);
                _logicaManager.ActualizarManagerTemporal(new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 0,
                    PartidosEmpatados = 0,
                    PartidosPerdidos = 1,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                });
            }
            else if (empate)
            {
                cambioDirectiva = rankingMiEquipo > rankingRival ? 1 : -1;
                cambioFans = rankingMiEquipo > rankingRival ? 2 : -2;
                cambioJugadores = rankingMiEquipo > rankingRival ? 1 : -2;

                if (esRivalHistorico)
                {
                    cambioFans += 1;
                    cambioJugadores += 1;
                }

                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 0, 1, 0, 1);
                _logicaManager.ActualizarManagerTemporal(new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 0,
                    PartidosEmpatados = 1,
                    PartidosPerdidos = 0,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                });
            }

            // Limitar el rango de impacto según tipo de confianza
            cambioDirectiva = Math.Clamp(cambioDirectiva, -5, 5);
            cambioFans = Math.Clamp(cambioFans, -8, 8);
            cambioJugadores = Math.Clamp(cambioJugadores, -6, 6);

            _logicaManager.ActualizarConfianza(_manager.IdManager, cambioDirectiva, cambioFans, cambioJugadores);
        }

        public int ObtenerPuestoEquipo(int equipoId, int competicion, int manager)
        {
            List<Clasificacion> clasificaciones = _logicaClasificacion.MostrarClasificacion(competicion, manager);

            // Buscar la posición del equipo en la lista
            var equipo = clasificaciones.FirstOrDefault(c => c.IdEquipo == equipoId);

            return equipo != null ? equipo.Posicion : -1; // Retorna -1 si no encuentra el equipo
        }

        public void ActualizacionAtributos(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante, int idEquipolocal, int idEquipoVisitante,
                                           int golesLocal, int golesVisitante, List<(Jugador, Jugador?)> golesYAsistencias, Jugador mvp)
        {
            if (golesLocal > golesVisitante)
            {
                // Subir la moral 1 punto del los jugadores que ganan y bajar 1 punto de los jugadores que pierden
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -1, 0);
                }
            } 
            else if (golesLocal < golesVisitante)
            {
                // Subir la moral 1 punto del los jugadores que ganan y bajar 1 punto de los jugadores que pierden
                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -1, 0);
                }
            } 
            else
            {
                // Subir la moral 1 punto del los jugadores que empatan
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }
            }

            // Subir la moral y el estado de forma de los jugadores que han marcado, asistido o han sido MVP
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                _logicaJugador.ActualizarMoralEstadoForma(goleador.IdJugador, 2, 2);
                if (asistente != null)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(asistente.IdJugador, 1, 1);
                }
            }
            _logicaJugador.ActualizarMoralEstadoForma(mvp.IdJugador, 3, 3);
        }

        public void ActualizarEntrenamiento(List<Jugador> misJugadores)
        {
            foreach (var jugador in misJugadores)
            {  
                switch (jugador.Entrenamiento)
                {
                    case 1: // Portero
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 1, 0, 0, 0, 0, 0);
                        break;

                    case 2: // Entradas
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 0, 1, 0);
                        _logicaJugador.ActualizarOtrosAtributosEntrenamiento(jugador.IdJugador, 0, 0, -1, 0);
                        break;

                    case 3: // Remate
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 1, 0, 0);
                        _logicaJugador.ActualizarOtrosAtributosEntrenamiento(jugador.IdJugador, 1, 0, 0, 0);
                        break;
                    case 4: // Pase
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 1, 0, 0, 0, 0);
                        _logicaJugador.ActualizarOtrosAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 1);
                        break;
                    case 5: // Regate
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 0, 1, 0, 0, 0);
                        _logicaJugador.ActualizarOtrosAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 1);
                        break;
                    case 6: // Tiro
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 0, 0, 1);
                        _logicaJugador.ActualizarOtrosAtributosEntrenamiento(jugador.IdJugador, 1, 0, 0, 0);
                        break;
                    default:
                        _logicaJugador.ActualizarAtributosEntrenamiento(jugador.IdJugador, 0, 0, 0, 0, 0, 0);
                        break;
                }
            }
        }

        private void ActualizarPanels(List<(Jugador, Jugador?)> golesYAsistencias, List<(Jugador, string)> tarjetas)
        {
            // Limpiar los DockPanel antes de agregar nuevos elementos
            panelGolesEquipoLocal.Children.Clear();
            panelGolesEquipoVisitante.Children.Clear();
            panelTarjetasEquipoLocal.Children.Clear();
            panelTarjetasEquipoVisitante.Children.Clear();

            // Agregar goles al StackPanel correspondiente
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                if (goleador.IdEquipo == _partido.IdEquipoLocal)
                    AgregarElementoAlPanel(panelGolesEquipoLocal, goleador.NombreCompleto, "gol"); 
                else
                    AgregarElementoAlPanel(panelGolesEquipoVisitante, goleador.NombreCompleto, "gol"); 
            }

            // Agregar tarjetas al StackPanel correspondiente
            Dictionary<int, int> tarjetasAmarillasPorJugador = new Dictionary<int, int>();

            // Agregar tarjetas al StackPanel correspondiente
            foreach (var (jugador, tarjeta) in tarjetas)
            {
                // Si el jugador no tiene registro de tarjetas amarillas, inicializarlo
                if (!tarjetasAmarillasPorJugador.ContainsKey(jugador.IdJugador))
                {
                    tarjetasAmarillasPorJugador[jugador.IdJugador] = 0;
                }

                // Determinar el tipo de tarjeta
                string tipoTarjeta = tarjeta;

                // Si el jugador recibe una tarjeta amarilla
                if (tarjeta == "amarilla")
                {
                    tarjetasAmarillasPorJugador[jugador.IdJugador]++;

                    // Si el jugador tiene 2 tarjetas amarillas, se convierte en doble amarilla
                    if (tarjetasAmarillasPorJugador[jugador.IdJugador] == 2)
                    {
                        tipoTarjeta = "dobleamarilla";  // Cambiar a "dobleamarilla" si tiene dos amarillas
                    }
                }
                // Si el jugador recibe una tarjeta roja directa
                else if (tarjeta == "roja")
                {
                    tipoTarjeta = "roja";  // Cambiar a "roja" si es una tarjeta roja directa
                }

                // Agregar la tarjeta correspondiente al panel correspondiente
                if (jugador.IdEquipo == _partido.IdEquipoLocal)
                {
                    AgregarElementoAlPanel(panelTarjetasEquipoLocal, jugador.NombreCompleto, tipoTarjeta); // Usar el tipo de tarjeta calculado
                }
                else
                {
                    AgregarElementoAlPanel(panelTarjetasEquipoVisitante, jugador.NombreCompleto, tipoTarjeta); // Usar el tipo de tarjeta calculado
                }
            }
        }

        private void AgregarElementoAlPanel(Panel panel, string nombreJugador, string tipoEvento)
        {
            // Crear un Grid con 2 columnas
            Grid grid = new Grid
            {
                Margin = new Thickness(0, 5, 0, 5) // Espacio arriba y abajo de cada fila
            };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60) }); // Columna para la imagen
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }); // Columna para el nombre

            // Crear el StackPanel para apilar las tarjetas
            StackPanel stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal, // Cambio a horizontal
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Asignar la fuente de la imagen según el tipo de evento
            switch (tipoEvento)
            {
                case "gol":
                    var golIcono = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/goleador.png")),
                        Width = 16,
                        Height = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    grid.Children.Add(golIcono);
                    Grid.SetColumn(golIcono, 0);
                    break;

                case "amarilla":
                    var amarillaIcono = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/amonestacionAmarilla.png")),
                        Width = 16,
                        Height = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    stackPanel.Children.Add(amarillaIcono); // Añadir la tarjeta amarilla al StackPanel
                    break;

                case "dobleamarilla":
                    var amarilla1 = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/amonestacionAmarilla.png")),
                        Width = 16,
                        Height = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    var amarilla2 = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/amonestacionAmarilla.png")),
                        Width = 16,
                        Height = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    stackPanel.Children.Add(amarilla1); // Añadir la primera tarjeta amarilla
                    stackPanel.Children.Add(amarilla2); // Añadir la segunda tarjeta amarilla
                    break;

                case "roja":
                    var rojaIcono = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/amonestacionRoja.png")),
                        Width = 16,
                        Height = 16,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    grid.Children.Add(rojaIcono);
                    Grid.SetColumn(rojaIcono, 0);
                    break;
            }

            // Crear el nombre del jugador
            TextBlock nombre = new TextBlock
            {
                Text = nombreJugador,
                FontSize = 16,
                FontFamily = new FontFamily("Cascadia Code SemiBold"),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            Grid.SetColumn(nombre, 1);

            // Agregar el StackPanel de tarjetas al Grid (si se trata de una tarjeta amarilla o doble amarilla)
            if (stackPanel.Children.Count > 0)
            {
                grid.Children.Add(stackPanel);
                Grid.SetColumn(stackPanel, 0);
            }

            // Agregar el nombre del jugador al Grid
            grid.Children.Add(nombre);

            // Agregar el Grid al Panel
            panel.Children.Add(grid);
        }
        #endregion
    }
}
