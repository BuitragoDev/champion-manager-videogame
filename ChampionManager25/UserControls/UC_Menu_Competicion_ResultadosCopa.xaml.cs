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
    public partial class UC_Menu_Competicion_ResultadosCopa : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DockPanel _panelCentral;
        Equipo equipo;

        private int rondaActual = 1;
        private int vueltaActual = 0;
        private const int rondaMin = 1;
        private const int rondaMax = 6;
        private const int vueltaMin = 0;
        private const int vueltaMax = 1;
        int miCompeticion;
        #endregion

        // Instancias de la LOGICA
        private PartidoLogica _logicaPartido = new PartidoLogica();
        private EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Competicion_ResultadosCopa(Manager manager, int equipo, DockPanel panelCentral)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();

            miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
            rondaActual = Math.Max(1, _logicaPartido.ObtenerUltimaRondaJugada(_equipo));
            List<Partido> partidosRonda = _logicaPartido.CargarRondaCopa(rondaActual, vueltaActual, _manager.IdManager, 4);
            _panelCentral = panelCentral;
            MostrarPartidos(partidosRonda);
        }

        private void resultadosCopa_Loaded(object sender, RoutedEventArgs e)
        {
            if (vueltaActual == 0)
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Ida)";
            }
            else
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Vuelta)";
            }

            lblTitulo.Text += " (" + _logicaCompeticion.MostrarNombreCompeticion(4).ToUpper() + ")";
            string ruta_liga = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoLiga.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga));
            string ruta_europa1 = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
            imgLogoCopaEuropa1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_europa1));
            string ruta_europa2 = _logicaCompeticion.ObtenerCompeticion(6).RutaImagen80;
            imgLogoCopaEuropa2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_europa2));
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton LIGA
        private void imgLogoLiga_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_Resultados ucLiga = new UC_Menu_Competicion_Resultados(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucLiga);
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton COPA EUROPA 1
        private void imgLogoCopaEuropa1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_ResultadosEuropa1 ucEuropa1 = new UC_Menu_Competicion_ResultadosEuropa1(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucEuropa1);
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton COPA EUROPA 2
        private void imgLogoCopaEuropa2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_ResultadosEuropa2 ucEuropa2 = new UC_Menu_Competicion_ResultadosEuropa2(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucEuropa2);
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton JORNADA ANTERIOR
        private void btnJornadaAnterior_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Primero intenta retroceder la vuelta
            if (vueltaActual > vueltaMin)
            {
                vueltaActual--;
            }
            else
            {
                // Si ya estabas en la vuelta mínima, retrocede la ronda y pon la vuelta al máximo
                if (rondaActual > rondaMin)
                {
                    rondaActual--;
                    vueltaActual = vueltaMax;
                }
                else
                {
                    return;
                }
            }

            // Cargar los partidos correspondientes a la nueva ronda/vuelta
            List<Partido> partidosRonda = _logicaPartido.CargarRondaCopa(rondaActual, vueltaActual, _manager.IdManager, 4);
            if (vueltaActual == 0)
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Ida)";
            }
            else
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Vuelta)";
            }
            MostrarPartidos(partidosRonda);
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton JORNADA SIGUIENTE
        private void btnJornadaSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // 🚫 Si ya estás en la Final (ronda 6) y en la ida (0), no avanzar más
            if (rondaActual == 6 && vueltaActual == 0)
                return;

            // Primero intenta avanzar la vuelta
            if (vueltaActual < vueltaMax)
            {
                vueltaActual++;
            }
            else
            {
                // Si ya estaba en la vuelta máxima, resetea la vuelta y avanza la ronda
                if (rondaActual < rondaMax)
                {
                    rondaActual++;
                    vueltaActual = vueltaMin;
                }
                else
                {
                    return;
                }
            }

            // Cargar los partidos correspondientes a la nueva ronda/vuelta
            List<Partido> partidosRonda = _logicaPartido.CargarRondaCopa(rondaActual, vueltaActual, _manager.IdManager, 4);
            if (vueltaActual == 0)
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Ida)";
            }
            else
            {
                txtJornadaActual.Text = $"{_logicaPartido.ObtenerNombreRonda(rondaActual)} (Vuelta)";
            }
            MostrarPartidos(partidosRonda);
        }

        #region "Metodos"
        private void MostrarPartidos(List<Partido> partidos)
        {
            // Limpiar la grid antes de cargar los nuevos partidos
            gridPartidos.Children.Clear();

            for (int i = 0; i < partidos.Count; i++)
            {
                Partido partido = partidos[i];
                int row = 0;
                int column = 0;

                // Determinar en qué fila y columna colocar el partido
                if (rondaActual == 1)
                {
                    row = i % 16;
                    column = i < 16 ? 0 : 1;
                }
                else if (rondaActual == 2)
                {
                    row = i % 8;
                    column = i < 8 ? 0 : 1;
                }
                else if (rondaActual == 3)
                {
                    row = i % 4;
                    column = i < 4 ? 0 : 1;
                }
                else if (rondaActual == 4)
                {
                    row = i % 2;
                    column = i < 2 ? 0 : 1;
                }
                else if (rondaActual == 5)
                {
                    row = i % 1;
                    column = i < 1 ? 0 : 1;
                }
                else if (rondaActual == 6)
                {
                    row = 0;
                    column = 0;
                }

                // Determinar el color de fondo de la fila (alternar entre WhiteSmoke y LightGray)
                Brush backgroundColor = (row % 2 == 0) ? Brushes.Gainsboro : Brushes.LightGray;

                // Crear un contenedor para la fila con fondo
                Border rowBackground = new Border
                {
                    Background = backgroundColor,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(2),
                    Height = 60
                };

                Grid.SetRow(rowBackground, row);
                Grid.SetColumn(rowBackground, column);
                if (rondaActual == 6) Grid.SetColumnSpan(rowBackground, 2); // aplicar span en final
                gridPartidos.Children.Add(rowBackground);

                // Crear un Grid para cada partido dentro de su celda
                Grid partidoGrid = new Grid();
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Escudo local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(245) }); // Nombre local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Goles local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Escudo visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(245) }); // Nombre visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Goles visitante

                // Escudo equipo local
                equipo = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal);
                Image imgLocal = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen32)),
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
                equipo = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante);
                Image imgVisitante = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen32)),
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
                if (rondaActual == 5) Grid.SetColumnSpan(partidoGrid, 2); // aplicar span en final
                gridPartidos.Children.Add(partidoGrid);
            }
        }
        #endregion
    }
}
