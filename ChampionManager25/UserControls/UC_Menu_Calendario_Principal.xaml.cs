using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class UC_Menu_Calendario_Principal : UserControl, INotifyPropertyChanged
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DateTime hoy = Metodos.hoy;
        private DateTime fechaHoy = Metodos.hoy;
        #endregion

        // Instancias de la LOGICA
        private PartidoLogica _logicaPartido = new PartidoLogica();
        private EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_Menu_Calendario_Principal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _partidos = new List<Partido>();
            DataContext = this; // Establecer el DataContext
        }

        public DateTime MesActual
        {
            get => hoy;
            set
            {
                if (hoy != value)
                {
                    hoy = value;
                    OnPropertyChanged(nameof(MesActual));
                    OnPropertyChanged(nameof(MesActualFormato));
                }
            }
        }

        public string MesActualFormato => MesActual.ToString("MMMM yyyy", System.Globalization.CultureInfo.CurrentCulture).ToUpper();

        private List<Partido> _partidos;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarPartidosYActualizarCalendario();
            txtFechaSeleccionada.Text = hoy.ToShortDateString();
        }

        private void CargarPartidosYActualizarCalendario()
        {
            // Obtiene los partidos del equipo
            _partidos = _logicaPartido.MostrarMisPartidos(_equipo, _manager.IdManager);

            // Genera el calendario
            GenerarCalendario();
        }

        private void GenerarCalendario()
        {
            panelCuadriculaCalendario.Children.Clear();

            var diasEnMes = DateTime.DaysInMonth(MesActual.Year, MesActual.Month);
            int primerDia = ((int)new DateTime(MesActual.Year, MesActual.Month, 1).DayOfWeek - 1 + 7) % 7;

            // Crear el grid para el calendario
            var grid = new UniformGrid
            {
                Rows = 7,  // 1 fila para los días de la semana + 6 filas para los días del mes
                Columns = 7 // 7 columnas para cada día de la semana
            };

            // Agregar los días de la semana (LUNES, MARTES, etc.) en la primera fila
            var diasSemana = new string[] { "LUNES", "MARTES", "MIÉRCOLES", "JUEVES", "VIERNES", "SÁBADO", "DOMINGO" };

            for (int i = 0; i < 7; i++)
            {
                var diaSemanaTextBlock = new TextBlock
                {
                    Text = diasSemana[i],
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontWeight = FontWeights.Bold,
                    FontSize = 18
                };
                grid.Children.Add(diaSemanaTextBlock);
            }

            // Llenar los días del mes
            for (int i = 0; i < (int)primerDia; i++)
            {
                grid.Children.Add(new Border()); // Espacios vacíos antes del primer día
            }

            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                var fecha = new DateTime(MesActual.Year, MesActual.Month, dia);
                var partidosEnFecha = _partidos.Where(p => p.FechaPartido.Date == fecha).ToList();

                var diaPanel = CrearPanelDia(fecha, partidosEnFecha);
                grid.Children.Add(diaPanel);
            }

            panelCuadriculaCalendario.Children.Add(grid);
        }
        private Border CrearPanelDia(DateTime fecha, List<Partido> partidos)
        {
            // Crear el border que contendrá el grid con 2 filas
            var borderDia = new Border
            {
                BorderBrush = Brushes.Gray,      // Borde gris
                BorderThickness = new Thickness(1),  // Grosor de borde de 1px
                Background = Brushes.White,      // Fondo blanco para el recuadro
                Margin = new Thickness(2),      // Margen del Border
                Cursor = Cursors.Hand
            };

            // Comprobar si la fecha es el día actual
            if (fecha.Date == fechaHoy)
            {
                borderDia.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d8edf2"));
            }

            // Agregar evento Click al Border
            borderDia.MouseLeftButtonUp += (s, e) =>
            {
                Metodos.ReproducirSonidoClick();
                txtFechaSeleccionada.Text = fecha.ToShortDateString();
                cargarDetallesPartido(fecha);
            };

            // Crear un Grid con 1 columna y 2 filas
            var gridDia = new Grid();
            gridDia.RowDefinitions.Add(new RowDefinition());  // Fila 0 (día del mes)
            gridDia.RowDefinitions.Add(new RowDefinition { Height = new GridLength(90) });  // Fila 1 (escudo)

            // Fila 0 (día del mes) - Centrado en la parte superior
            var diaDelMes = new TextBlock
            {
                Text = fecha.Day.ToString(),
                HorizontalAlignment = HorizontalAlignment.Right,        // Centrar
                VerticalAlignment = VerticalAlignment.Center,           // Centrar verticalmente
                Margin = new Thickness(0, 0, 10, 0),                    // Margen superior y derecho de 10px
                FontSize = 16,                                          // Tamaño de fuente para el día
                FontFamily = new FontFamily("Cascadia Code SemiBold"),
                Foreground = Brushes.WhiteSmoke                         // Color del texto en negro
            };

            // Crear un Border con fondo naranja para la primera fila
            var borderFila0 = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1d6a7d")), // Nuevo color
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };

            // Agregar el TextBlock (día del mes) dentro del Border de la fila 0
            borderFila0.Child = diaDelMes;

            // Fila 1 (escudo del equipo rival) - Centrado
            var escudoRival = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,   // Centrar imagen
                VerticalAlignment = VerticalAlignment.Center,       // Centrar imagen
                Margin = new Thickness(0, 0, 0, 0),
                Height = 80,
                Width = 80
            };

            // Buscar el partido para obtener el escudo del rival
            var partido = partidos.FirstOrDefault(p => p.IdEquipoLocal == _equipo || p.IdEquipoVisitante == _equipo);

            if (partido != null)
            {
                // Obtener el id del equipo rival
                int idEquipoRival = (_equipo == partido.IdEquipoLocal) ? partido.IdEquipoVisitante : partido.IdEquipoLocal;

                // Mostrar el escudo del equipo rival
                escudoRival.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/80x80/{idEquipoRival}.png"));
            }

            // Agregar el Border con el día del mes (naranja) a la fila 0
            Grid.SetRow(borderFila0, 0);
            gridDia.Children.Add(borderFila0);

            // Agregar la imagen del escudo en la fila 1
            Grid.SetRow(escudoRival, 1);
            gridDia.Children.Add(escudoRival);

            // Establecer el grid como hijo del border
            borderDia.Child = gridDia;

            return borderDia;
        }

        private void btnMesSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MesActual = MesActual.AddMonths(1);
            CargarPartidosYActualizarCalendario();
        }

        private void btnMesAnterior_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MesActual = MesActual.AddMonths(-1);
            CargarPartidosYActualizarCalendario();
        }

        private void cargarDetallesPartido(DateTime fecha)
        {
            Partido partido = _logicaPartido.MostrarDetallesPartido(_equipo, _manager.IdManager, fecha);


            if (partido != null)
            {
                // Logo Competicion
                if (partido.IdCompeticion == 1)
                {
                    imgLogoCompeticion.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/logos_competiciones/1.png"));
                }
                else if (partido.IdCompeticion == 10)
                {
                    imgLogoCompeticion.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/logos_competiciones/10.png"));
                }

                // Local/Visitante
                if (partido.IdEquipoLocal == _equipo)
                {
                    imgCancha.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/casa_icon.png"));
                }
                else if (partido.IdEquipoVisitante == _equipo)
                {
                    imgCancha.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/avion_icon.png"));
                }

                // Cancha
                txtCancha.Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal).Estadio;

                // Logo y Nombre de Equipos
                imgLogoLocal.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/80x80/{partido.IdEquipoLocal}.png"));
                txtEquipoLocal.Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal).Nombre;
                imgLogoVisitante.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/80x80/{partido.IdEquipoVisitante}.png"));
                txtEquipoVisitante.Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante).Nombre;
            }
            else
            {
                imgLogoCompeticion.Source = null;
                imgCancha.Source = null;
                imgLogoLocal.Source = null;
                imgLogoVisitante.Source = null;
                txtCancha.Text = "";
                txtEquipoLocal.Text = "";
                txtEquipoVisitante.Text = "";
            }
        }
    }
}
