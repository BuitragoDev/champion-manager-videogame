using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
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
            imgCampeon.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/{campeon}.png"));
            imgSubcampeon.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/{subcampeon}.png"));

            imgMiEquipo.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/escudos_equipos/120x120/{_equipo}.png"));
            txtNombreMiEquipo.Text += " " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre.ToUpper();
            txtPosicion.Text = _posicion + "º";

            Manager manager = _logicaManager.MostrarManager(_manager.IdManager);
            txtCDirectiva.Text = manager.CDirectiva.ToString();
            txtCFans.Text = manager.CFans.ToString();
            txtCJugadores.Text = manager.CJugadores.ToString();
            elipseDirectiva.Stroke = DeterminarColorElipse(manager.CDirectiva);
            elipseFans.Stroke = DeterminarColorElipse(manager.CFans);
            elipseJugadores.Stroke = DeterminarColorElipse(manager.CJugadores);
        }

        // ------------------------------------------------------------------------------------ Evento CLICK del boton TERMINAR TEMPORADA
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
