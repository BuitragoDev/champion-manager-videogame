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
    public partial class UC_Menu_Estadio_Informacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_Menu_Estadio_Informacion(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            imgLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/" + _equipo + ".png"));
            imgPabellon.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/" + _equipo + "interior.png"));
            txtNombrePabellon.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Estadio;
            txtCapacidad.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo.ToString("N0") + " asientos";
        }
    }
}
