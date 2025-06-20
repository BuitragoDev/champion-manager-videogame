﻿using System;
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
using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Competicion_ResultadosEuropa2 : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DockPanel _panelCentral;
        Equipo equipo;

        private int jornadaActual;
        private const int jornadaMin = 1;
        private const int jornadaMax = 12;
        int miCompeticion;
        #endregion

        // Instancias de la LOGICA
        private PartidoLogica _logicaPartido = new PartidoLogica();
        private EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_Menu_Competicion_ResultadosEuropa2(Manager manager, int equipo, DockPanel panelCentral)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _panelCentral = panelCentral;
            Metodos metodos = new Metodos();

            miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
            jornadaActual = Math.Max(1, _logicaPartido.ObtenerUltimaJornadaEuropa2Jugada(_equipo));
            List<Partido> partidosJornada = _logicaPartido.CargarJornadaCopaEuropa2(jornadaActual, _manager.IdManager, 6);
            MostrarPartidos(partidosJornada);
        }

        private void resultadosEuropa2_Loaded(object sender, RoutedEventArgs e)
        {
            txtJornadaActual.Text = $"Jornada {jornadaActual}";

            lblTitulo.Text += " (" + _logicaCompeticion.MostrarNombreCompeticion(6).ToUpper() + ")";
            string ruta_liga = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoLiga.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga));
            string ruta_copa = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
            imgLogoCopa.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_copa));
            string ruta_europa1 = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
            imgLogoCopaEuropa1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_europa1));
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton LIGA
        private void imgLogoLiga_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_Resultados ucLiga = new UC_Menu_Competicion_Resultados(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucLiga);
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton COPA
        private void imgLogoCopa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_ResultadosCopa ucCopa = new UC_Menu_Competicion_ResultadosCopa(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucCopa);
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton COPA EUROPA 1
        private void imgLogoCopaEuropa1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_ResultadosEuropa1 ucEuropa1 = new UC_Menu_Competicion_ResultadosEuropa1(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucEuropa1);
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton JORNADA ANTERIOR
        private void btnJornadaAnterior_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (jornadaActual > jornadaMin)
            {
                jornadaActual--;
                List<Partido> partidosJornada = _logicaPartido.CargarJornadaCopaEuropa2(jornadaActual, _manager.IdManager, 6);
                MostrarPartidos(partidosJornada);
                txtJornadaActual.Text = NombreJornada(jornadaActual);
            }
        }

        // ---------------------------------------------------------------------------- Evento CLICK del boton JORNADA SIGUIENTE
        private void btnJornadaSiguiente_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (jornadaActual < jornadaMax)
            {
                jornadaActual++;
                List<Partido> partidosJornada = _logicaPartido.CargarJornadaCopaEuropa2(jornadaActual, _manager.IdManager, 6);
                MostrarPartidos(partidosJornada);
                txtJornadaActual.Text = NombreJornada(jornadaActual);
            }
        }

        #region "Metodos"
        private void MostrarPartidos(List<Partido> partidos)
        {
            // Limpiar la grid antes de cargar los nuevos partidos
            gridPartidos.Children.Clear();

            for (int i = 0; i < partidos.Count; i++)
            {
                Partido partido = partidos[i];

                // Determinar en qué fila y columna colocar el partido
                int row = i % 9; // Las filas se repiten cada 9 partidos
                int column = i < 9 ? 0 : 1; // Los primeros 9 van en la columna 0, los siguientes en la columna 1

                // Determinar el color de fondo de la fila (alternar entre WhiteSmoke y LightGray)
                Brush backgroundColor = (row % 2 == 0) ? Brushes.Gainsboro : Brushes.LightGray;

                // Crear un contenedor para la fila con fondo
                Border rowBackground = new Border
                {
                    Background = backgroundColor,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(2),
                    Height = 50
                };

                Grid.SetRow(rowBackground, row);
                Grid.SetColumn(rowBackground, column);
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
                gridPartidos.Children.Add(partidoGrid);
            }
        }

        private string NombreJornada(int jornada)
        {
            if (jornada == 9)
            {
                return "Octavos de Final";
            }
            else if (jornada == 10)
            {
                return "Cuartos de Final";
            }
            else if (jornada == 11)
            {
                return "Semifinales";
            }
            else if (jornada == 12)
            {
                return "Final";
            }
            else
            {
                return $"Jornada {jornadaActual}";
            }
        }
        #endregion
    }
}
