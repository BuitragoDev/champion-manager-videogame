﻿using ChampionManager25.Datos;
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

        public frmResumenPartido(Manager manager, int equipo, Partido partido)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            this._partido = partido;

            // Código que inicializa el sonido de fondo 
            try
            {
                Metodos.ReproducirMusica("backgroundMusic2.wav");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resumenPartido_Loaded(object sender, RoutedEventArgs e)
        {
            int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(miCompeticion).RutaImagen80;
            imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
            equipoLocal = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal);
            equipoVisitante = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoVisitante);
            // Cargar datos basicos del partido
            txtEquipoLocal.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Nombre;
            txtEquipoVisitante.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoVisitante).Nombre;
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

            imgBotonCerrar.IsEnabled = true;
        }

        private void resumenPartido_Unloaded(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
        }

        private void imgBotonCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #region "Metodos"
        private void SimularPartido(Partido partido)
        {
            List<Jugador> jugadoresLocal = null;
            List<Jugador> jugadoresVisitante = null;

            // Cargar jugadores de los equipos desde la BD
            // Comprobar si soy el local o el visitante
            if (partido.IdEquipoLocal == _equipo)
            {
                jugadoresLocal = _logicaJugador.JugadoresMiEquipoJueganPartido(partido.IdEquipoLocal);
                jugadoresVisitante = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoVisitante);
            }
            else
            {
                jugadoresLocal = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoLocal);
                jugadoresVisitante = _logicaJugador.JugadoresMiEquipoJueganPartido(partido.IdEquipoVisitante);
            }

            // Calcular goles con el equipo rival en cuenta
            int golesLocal = CalcularGoles(jugadoresLocal, jugadoresVisitante);
            int golesVisitante = CalcularGoles(jugadoresVisitante, jugadoresLocal);

            // Guardar resultado en el partido
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
            txtNombreMvp.Text = _logicaJugador.MostrarDatosJugador(mvp.IdJugador).NombreCompleto;
            imgMvp.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + mvp.RutaImagen));

            // Calcular asistencia al estadio
            partido.Asistencia = _logicaEquipo.CalcularAsistencia(partido.IdEquipoLocal);
            txtAsistencia.Text = (partido.Asistencia?.ToString("N0") ?? "0") + " espectadores";

            // Mostrar guardar resultado del partido
            _logicaPartidos.ActualizarPartido(partido);

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE LIGA
            if (partido.IdCompeticion >= 1 && partido.IdCompeticion <= 2)
            {
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
                    int numeroAleatorio = random.Next(0, 9);
                    _logicaJugador.PonerJugadorLesionado(jugador, numeroAleatorio);

                    // Si es un jugador de mi equipo...
                    if (_logicaJugador.EsDeMiEquipo(jugador, _equipo))
                    {
                        // Crear el mensaje
                        Mensaje mensajeInicio = new Mensaje
                        {
                            Fecha = Metodos.hoy,
                            Remitente = _logicaJugador.MostrarDatosJugador(jugador).NombreCompleto,
                            Asunto = "Jugador Lesionado",
                            Contenido = "Desde el equipo médico del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te informamos de que " + _logicaJugador.MostrarDatosJugador(jugador).NombreCompleto + " se ha lesionado, y permanecerá de baja durante " + numeroAleatorio + " partidos.",
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
                        if (statJugador.TarjetasAmarillas % 2 == 0)
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

            // Mostrar los goleadores y amonestados en la ventana.
            ActualizarPanels(golesYAsistencias, tarjetas);
        }

        private int CalcularGoles(List<Jugador> jugadores, List<Jugador> jugadoresRival)
        {
            if (jugadores.Count == 0 || jugadoresRival.Count == 0) return 0; // Evitar errores

            // Calcular ataque del equipo y defensa del rival
            double ataque = jugadores.Average(j => (j.Remate + j.Pase + j.Calidad + j.Tiro + j.Regate + j.EstadoForma + j.Moral) / 7.0);
            double defensa = jugadoresRival.Average(j => (j.Entradas + j.Resistencia + j.Agresividad + j.EstadoForma + j.Velocidad + j.Moral) / 6.0);

            // Diferencia ajustada con más impacto
            double diferencia = (ataque - defensa) / 5.0; // Reducimos la escala del impacto
            double factor = 0.5 + (diferencia / 2.0); // Hacemos que el factor oscile más entre 0.3 y 0.7

            factor = Math.Clamp(factor, 0.25, 0.75); // Limitamos el factor ofensivo a valores más realistas

            // Goles esperados con una mayor base de variabilidad
            double golesEsperados = factor * (2.0 + random.NextDouble() * 2.5); // Entre 2 y 4.5 goles posibles

            // Variación aleatoria equilibrada
            double variacion = (random.NextDouble() * 1.8) - 0.9; // De -0.9 a +0.9 para más variedad
            int goles = (int)Math.Round(golesEsperados + variacion);

            // Asegurar valores dentro de un rango realista
            return Math.Clamp(goles, 0, 7);
        }
        private List<(Jugador, Jugador?)> AsignarGolesYAsistencias(int goles, List<Jugador> jugadores, Random random)
        {
            List<(Jugador, Jugador?)> lista = new List<(Jugador, Jugador?)>();

            // Filtrar jugadores que no sean porteros
            var jugadoresNoPorteros = jugadores.Where(j => j.RolId != 1).ToList();

            // Asignar pesos basados en atributos y posición
            var pesosGoleadores = jugadoresNoPorteros.Select(j =>
                (jugador: j, peso: (j.Remate * 1.5 + j.Tiro * 1.5 + j.Calidad) * (j.RolId >= 7 && j.RolId <= 10 ? 5 : 0.5)) // Aumentado a x5
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

                // 50% de probabilidad de asistencia
                Jugador? asistente = random.NextDouble() > 0.5 ?
                    SeleccionarJugadorPonderado(pesosAsistentes, totalPesoAsistente, random) : null;

                lista.Add((goleador, asistente));
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

                // Reducir el número de partidos lesionado si es mayor que 0
                if (jugador.Lesion > 0)
                {
                    _logicaJugador.PonerJugadorLesionado(jugador.IdJugador, jugador.Lesion - 1);
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
            // Determinar qué equipo es más fuerte basado en la clasificación actual
            int rankingMiEquipo = ObtenerPuestoEquipo(miEquipo, 1, _manager.IdManager);
            int rankingRival = ObtenerPuestoEquipo(rival, 1, _manager.IdManager);

            // Base de confianza (valores iniciales)
            int cambioDirectiva = 0;
            int cambioFans = 0;
            int cambioJugadores = 0;

            // Diferencia de goles
            int diferenciaGoles = Math.Abs(golesEquipo - golesRival);

            // Lógica de confianza según el resultado del partido
            if (golesEquipo > golesRival) // Victoria
            {
                if (rankingMiEquipo > rankingRival) // Ganamos contra un equipo más fuerte
                {
                    cambioDirectiva = 4;
                    cambioFans = 5;
                    cambioJugadores = 4;
                }
                else // Ganamos contra un equipo más débil
                {
                    cambioDirectiva = 3;
                    cambioFans = 3;
                    cambioJugadores = 3;
                }
                // Actualizar tabla managers
                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 1, 0, 0, 5);

                // Actualizar la tabla historial_manager_temp
                Historial historial = new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 1,
                    PartidosEmpatados = 0,
                    PartidosPerdidos = 0,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                };
                _logicaManager.ActualizarManagerTemporal(historial);
            }
            else if (golesEquipo < golesRival) // Derrota
            {
                if (rankingMiEquipo < rankingRival) // Perdemos contra un equipo más fuerte
                {
                    cambioDirectiva = -2;
                    cambioFans = -3;
                    cambioJugadores = -2;
                }
                else // Perdemos contra un equipo más débil
                {
                    cambioDirectiva = -4;
                    cambioFans = -5;
                    cambioJugadores = -4;
                }
                // Actualizar tabla managers
                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 0, 0, 1, 0);

                // Actualizar la tabla historial_manager_temp
                Historial historial = new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 0,
                    PartidosEmpatados = 0,
                    PartidosPerdidos = 1,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                };
                _logicaManager.ActualizarManagerTemporal(historial);
            }
            else // Empate
            {
                if (rankingMiEquipo > rankingRival) // Empate contra equipo fuerte
                {
                    cambioDirectiva = 1;
                    cambioFans = 2;
                    cambioJugadores = 1;
                }
                else // Empate contra equipo más débil
                {
                    cambioDirectiva = -2;
                    cambioFans = -3;
                    cambioJugadores = -2;
                }
                // Actualizar tabla managers
                _logicaManager.ActualizarResultadoManager(_manager.IdManager, 1, 0, 1, 0, 2);

                // Actualizar la tabla historial_manager_temp
                Historial historial = new Historial
                {
                    PartidosJugados = 1,
                    PartidosGanados = 0,
                    PartidosEmpatados = 1,
                    PartidosPerdidos = 0,
                    GolesMarcados = golesEquipo,
                    GolesRecibidos = golesRival
                };
                _logicaManager.ActualizarManagerTemporal(historial);
            }

            // Ajuste por goleada
            if (diferenciaGoles >= 3)
            {
                if (golesEquipo > golesRival) // Goleada a favor
                {
                    cambioDirectiva += 1;
                    cambioFans += 2;
                    cambioJugadores += 1;
                }
                else // Goleada en contra
                {
                    cambioDirectiva -= 1;
                    cambioFans -= 2;
                    cambioJugadores -= 1;
                }
            }

            // Limitar los cambios de confianza a un máximo de 5 por partido
            cambioDirectiva = Math.Clamp(cambioDirectiva, -5, 5);
            cambioFans = Math.Clamp(cambioFans, -5, 5);
            cambioJugadores = Math.Clamp(cambioJugadores, -5, 5);

            // Actualizar la confianza en la base de datos
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
                // Subir la moral y el estado de forma 5 puntos de los jugadores del equipo que gana y bajarla 5 puntos del equipo que pierde
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 5, 5);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -5, -5);
                }
            } 
            else if (golesLocal < golesVisitante)
            {
                // Subir la moral y el estado de forma 5 puntos de los jugadores del equipo que gana y bajarla 5 puntos del equipo que pierde
                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 5, 5);
                }

                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -5, -5);
                }
            } 
            else
            {
                // Subir la moral y el estado de forma 2 puntos de los jugadores de los equipos cuando empatan
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 2, 2);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 2, 2);
                }
            }

            // Subir la moral y el estado de forma de los jugadores que han marcado, asistido o han sido MVP
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                _logicaJugador.ActualizarMoralEstadoForma(goleador.IdJugador, 3, 3);
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
