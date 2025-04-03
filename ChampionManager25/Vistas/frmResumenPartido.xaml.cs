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
            // Cargar datos basicos del partido
            txtEquipoLocal.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Nombre;
            txtEquipoVisitante.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoVisitante).Nombre;
            imgEscudoEquipoLocal.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + _partido.IdEquipoLocal + ".png"));
            imgEscudoEquipoVisitante.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + _partido.IdEquipoVisitante + ".png"));
            txtEstadio.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Estadio;
            imgEstadio.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/" + _partido.IdEquipoLocal + "interior.png"));
            txtAforo.Text = _logicaEquipo.ListarDetallesEquipo(_partido.IdEquipoLocal).Aforo.ToString("N0") + " espectadores";

            SimularPartido(_partido);
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
            // Cargar jugadores de los equipos desde la BD sin porteros
            List<Jugador> jugadoresLocal = _logicaJugador.PlantillaSinPorteros(partido.IdEquipoLocal);
            List<Jugador> jugadoresVisitante = _logicaJugador.PlantillaSinPorteros(partido.IdEquipoVisitante);

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
            imgMvp.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + mvp.IdJugador + ".png"));

            // Calcular asistencia al estadio
            partido.Asistencia = _logicaEquipo.CalcularAsistencia(partido.IdEquipoLocal);
            txtAsistencia.Text = (partido.Asistencia?.ToString("N0") ?? "0") + " espectadores";

            // Mostrar guardar resultado del partido
            _logicaPartidos.ActualizarPartido(partido);

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE LIGA
            if (partido.IdCompeticion == 1)
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
                _logicaClasificacion.ActualizarClasificacion(cla_local);
                _logicaClasificacion.ActualizarClasificacion(cla_visitante);

                // Actualizar estadísticas de cada jugador en la base de datos
                ActualizarEstadisticasPartido(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);
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
            return Math.Clamp(goles, 0, 6);
        }
        private List<(Jugador, Jugador?)> AsignarGolesYAsistencias(int goles, List<Jugador> jugadores, Random random)
        {
            List<(Jugador, Jugador?)> lista = new List<(Jugador, Jugador?)>();

            // Asignar pesos basados en atributos y posición
            var pesosGoleadores = jugadores.Select(j =>
                (jugador: j, peso: (j.Remate * 1.5 + j.Tiro * 1.5 + j.Calidad) * (j.RolId >= 7 && j.RolId <= 10 ? 5 : 0.5)) // Aumentado a x5
            ).ToList();

            var pesosAsistentes = jugadores.Select(j =>
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

                if (probabilidad <= 0.75) // 75% de probabilidad de recibir una amarilla
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
