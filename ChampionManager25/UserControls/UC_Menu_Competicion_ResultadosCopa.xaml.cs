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

        private int jornadaActual = 1;
        private const int jornadaMin = 1;
        private const int jornadaMax = 35;
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
            List<Partido> partidosJornada = _logicaPartido.CargarJornada(jornadaActual, _manager.IdManager, miCompeticion);
            _panelCentral = panelCentral;
            //MostrarPartidos(partidosJornada);
        }

        private void resultadosCopa_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_liga = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoLiga.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_liga));
            lblTitulo.Text += " (" + _logicaCompeticion.MostrarNombreCompeticion(4).ToUpper() + ")";
            string ruta_copa = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
            imgLogoCopa.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_copa));
        }

        private void imgLogoLiga_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UC_Menu_Competicion_Resultados ucLiga = new UC_Menu_Competicion_Resultados(_manager, _equipo, _panelCentral);
            _panelCentral.Children.Clear();
            _panelCentral.Children.Add(ucLiga);
        }
    }
}
