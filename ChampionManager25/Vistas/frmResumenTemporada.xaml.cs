using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.UserControls;
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
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmResumenTemporada : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<Clasificacion> _clasificacion;
        private int _posicion;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public frmResumenTemporada(Manager manager, int equipo, List<Clasificacion> clasificacion, int posicion)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _clasificacion = clasificacion;
            _posicion = posicion;
        }

        private void resumenTemporada_Loaded(object sender, RoutedEventArgs e)
        {
            int campeon = 0;
            int subcampeon = 0;
            foreach (var equipo in _clasificacion)
            {
                if (equipo.Posicion == 1)
                {
                    campeon = equipo.IdEquipo;
                }
                if (equipo.Posicion == 2)
                {
                    subcampeon = equipo.IdEquipo;
                }
            }

            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));

            Equipo equipoCampeon = _logicaEquipo.ListarDetallesEquipo(campeon);
            Equipo equipoSubcampeon = _logicaEquipo.ListarDetallesEquipo(subcampeon);
            imgCampeon.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoCampeon.RutaImagen120));
            imgSubcampeon.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoSubcampeon.RutaImagen120));

            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            imgMiEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen120));
            txtNombreMiEquipo.Text += " " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre.ToUpper();
            txtPosicion.Text = _posicion + "º";

            Manager manager = _logicaManager.MostrarManager(_manager.IdManager);
            txtCDirectiva.Text = manager.CDirectiva.ToString();
            txtCFans.Text = manager.CFans.ToString();
            txtCJugadores.Text = manager.CJugadores.ToString();
            elipseDirectiva.Stroke = DeterminarColorElipse(manager.CDirectiva);
            elipseFans.Stroke = DeterminarColorElipse(manager.CFans);
            elipseJugadores.Stroke = DeterminarColorElipse(manager.CJugadores);

            int competicion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
            List<Clasificacion> clasificacion;
            if (competicion == 1)
            {
                clasificacion = _logicaClasificacion.MostrarClasificacion(1, _manager.IdManager);
            } 
            else
            {
                clasificacion = _logicaClasificacion.MostrarClasificacion2(2, _manager.IdManager);
            }
                
            int posicion = 0;
            for (int i = 0; i < clasificacion.Count; i++)
            {
                if (clasificacion[i].IdEquipo == _equipo)
                {
                    posicion = i + 1; // +1 si quieres que la posición empiece en 1
                    break; 
                }
            }

            // Mostrar Puntos de Reputacion
            if (posicion == 1)
            {
                txtPuntosReputacion.Text = "+ 25";
            }
            else if (posicion <= 4)
            {
                txtPuntosReputacion.Text = "+ 10";
            }
            else if (posicion >= 16)
            {
                txtPuntosReputacion.Text = "- 10";
            }
            else
            {
                txtPuntosReputacion.Text = "+ 5";
            }
        }

        // ------------------------------------------------------------------------------------ Evento CLICK del boton IR A PREMIOS JUGADORES
        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region "Metodos"
        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos > 70)
                return new SolidColorBrush(Colors.Green);
            else if (puntos >= 50)
                return new SolidColorBrush(Color.FromArgb(0xFF, 0xC6, 0x76, 0x17));
            else
                return new SolidColorBrush(Colors.Red);
        }
        #endregion
    }
}
