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
    public partial class frmVerDetallesEquipo : Window
    {
        #region "Variables"
        public int idEquipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        Equipo equipo = new Equipo();

        public frmVerDetallesEquipo(int idEquipo)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
        }
    }
}
