using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Numerics;
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
    public partial class frmSimulandoPartidos : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        List<Partido> listaPartidos;
        Equipo equipoLocal;
        Equipo equipoVisitante;

        private static Random random = new Random(); //Random global
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        FechaDatos _datosFecha = new FechaDatos();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();
        EntrenadorLogica _logicaEntrenador = new EntrenadorLogica();

        public frmSimulandoPartidos(Manager manager, int equipo, List<Partido> listaPartidos)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            this.listaPartidos = listaPartidos;
        }

        private async void simulacionPartidos_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                foreach (Partido partido in listaPartidos)
                {
                    List<Jugador> todosLosJugadores = _logicaJugador.ListadoJugadoresCompleto(partido.IdEquipoLocal)
                                                      .Concat(_logicaJugador.ListadoJugadoresCompleto(partido.IdEquipoVisitante))
                                                      .ToList();
                    
                    ActualizarSancionesYLesiones(todosLosJugadores);
                    SimularPartido(partido);
                }
            });

            imgBotonCerrar.IsEnabled = true;
            progressBar.Visibility = Visibility.Collapsed;
            areaPartidos.Visibility = Visibility.Visible;
            MostrarPartidos(_logicaPartidos.PartidosHoy(_equipo, _manager.IdManager));
        }

        // -------------------------------------------------------------------------------- Evento CLICK del boton CERRRAR
        private void imgBotonCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #region "Metodos"
        private void SimularPartido(Partido partido)
        {
            // Cargar jugadores de los equipos desde la BD sin porteros
            List<Jugador> jugadoresLocal = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoLocal);
            List<Jugador> jugadoresVisitante = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoVisitante);

            // Calcular goles con el equipo rival en cuenta
            int golesLocal = CalcularGoles(jugadoresLocal, jugadoresVisitante);
            int golesVisitante = CalcularGoles(jugadoresVisitante, jugadoresLocal);

            // Guardar resultado en el partido
            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;

            // Asignar goleadores y asistentes
            List<(Jugador, Jugador?)> golesYAsistencias = AsignarGolesYAsistencias(golesLocal, jugadoresLocal, random)
                .Concat(AsignarGolesYAsistencias(golesVisitante, jugadoresVisitante, random))
                .ToList();

            // Asignar tarjetas
            List<(Jugador, string)> tarjetas = AsignarTarjetas(jugadoresLocal, jugadoresVisitante, random);

            // Determinar MVP
            Jugador mvp = DeterminarMVP(golesYAsistencias, jugadoresLocal, jugadoresVisitante);

            // Calcular asistencia al estadio
            partido.Asistencia = _logicaEquipo.CalcularAsistencia(partido.IdEquipoLocal);

            // Mostrar guardar resultado del partido
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
            _logicaClasificacion.ActualizarClasificacion(cla_local);
            _logicaClasificacion.ActualizarClasificacion(cla_visitante);

            // Actualizar estadísticas de cada jugador en la base de datos
            ActualizarEstadisticasPartido(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);

            // Comprobar si ha habido algun lesionado y actualizarlo en la BD
            List<int> lesionados = SimularLesiones(jugadoresLocal, jugadoresVisitante);
            foreach (int jugador in lesionados)
            {
                int numeroAleatorio = random.Next(0, 9);
                _logicaJugador.PonerJugadorLesionado(jugador, numeroAleatorio);
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
                    }
                } 
                else if (tipoTarjeta == "roja")
                {
                    _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, 2);
                }  
            }

            // Actualizar los puntos de manager
            int entrenadorLocal = _logicaEntrenador.MostrarEntrenador(partido.IdEquipoLocal).IdEntrenador;
            int entrenadorVisitante = _logicaEntrenador.MostrarEntrenador(partido.IdEquipoVisitante).IdEntrenador;
            ActualizarPuntosManager(partido, golesLocal, golesVisitante, entrenadorLocal, entrenadorVisitante);

            // Actualizar atributos de los jugadores
            ActualizacionAtributos(jugadoresLocal, jugadoresVisitante, partido.IdEquipoLocal,
                                   partido.IdEquipoVisitante, golesLocal, golesVisitante, golesYAsistencias, mvp);
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
            // Seleccionamos jugadores más agresivos para recibir tarjetas
            List<Jugador> jugadoresAgresivos = jugadores.OrderByDescending(j => j.Agresividad).Take(totalTarjetas).ToList();
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

        private void ActualizarPuntosManager(Partido partido, int golesLocal, int golesVisitante, int entrenadorLocal, int entrenadorVisitante)
        {
            if (golesLocal > golesVisitante) // Victoria Local
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorLocal, 5);    
            }
            else if (golesLocal < golesVisitante) // Victoria Visitante
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorVisitante, 5);
            }
            else // Empate
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorLocal, 2);
                _logicaEntrenador.ActualizarResultadoManager(entrenadorVisitante, 2);
            }
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

        private void MostrarPartidos(List<Partido> partidos)
        {
            // Limpiar la grid antes de cargar los nuevos partidos
            gridPartidos.Children.Clear();

            for (int i = 0; i < partidos.Count; i++)
            {
                Partido partido = partidos[i];
                equipoLocal = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal);
                equipoVisitante = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante);

                // Determinar en qué fila y columna colocar el partido
                int row = i % 13; 
                int column = i < 13 ? 0 : 1; 

                // Determinar el color de fondo de la fila (alternar entre WhiteSmoke y LightGray)
                Brush backgroundColor = (row % 2 == 0) ? Brushes.Gainsboro : Brushes.LightGray;

                // Crear un contenedor para la fila con fondo
                Border rowBackground = new Border
                {
                    Background = backgroundColor,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(2)
                };

                Grid.SetRow(rowBackground, row);
                Grid.SetColumn(rowBackground, column);
                gridPartidos.Children.Add(rowBackground);

                // Crear un Grid para cada partido dentro de su celda
                Grid partidoGrid = new Grid();
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Escudo local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) }); // Nombre local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Goles local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Escudo visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) }); // Nombre visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Goles visitante

                // Escudo equipo local
                Image imgLocal = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaImagen32)),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(imgLocal, 0);
                partidoGrid.Children.Add(imgLocal);

                // Nombre equipo local
                TextBlock txtLocal = new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal).Nombre,
                    FontSize = 16,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Foreground = partido.IdEquipoLocal == _equipo ? Brushes.DarkRed : Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(txtLocal, 1);
                partidoGrid.Children.Add(txtLocal);

                // Goles equipo local
                TextBlock txtGolesLocal = new TextBlock
                {
                    Text = partido.Estado == "Pendiente" ? "-" : partido.GolesLocal.ToString(),
                    FontSize = 22,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(txtGolesLocal, 2);
                partidoGrid.Children.Add(txtGolesLocal);

                // Escudo equipo visitante
                Image imgVisitante = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoVisitante.RutaImagen32)),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(imgVisitante, 3);
                partidoGrid.Children.Add(imgVisitante);

                // Nombre equipo visitante
                TextBlock txtVisitante = new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante).Nombre,
                    FontSize = 16,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Foreground = partido.IdEquipoVisitante == _equipo ? Brushes.DarkRed : Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(5)
                };
                Grid.SetColumn(txtVisitante, 4);
                partidoGrid.Children.Add(txtVisitante);

                // Goles equipo visitante
                TextBlock txtGolesVisitante = new TextBlock
                {
                    Text = partido.Estado == "Pendiente" ? "-" : partido.GolesVisitante.ToString(),
                    FontSize = 22,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(txtGolesVisitante, 5);
                partidoGrid.Children.Add(txtGolesVisitante);

                // Agregar el partidoGrid dentro del fondo de la celda
                Grid.SetRow(partidoGrid, row);
                Grid.SetColumn(partidoGrid, column);
                gridPartidos.Children.Add(partidoGrid);
            }
        }
        #endregion
    }
}
