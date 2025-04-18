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
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmResumenCopaNacional : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public frmResumenCopaNacional(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
        }

        private void resumenCopa_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        } 
    }
}
