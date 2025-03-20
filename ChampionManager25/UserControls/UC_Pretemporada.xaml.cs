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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_Pretemporada : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<int> _idsPartidos;

        private int numeroPartido = 1;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        //PartidoLogica _logicaPartido = new PartidoLogica();
        Equipo miEquipo = null;

        // Las fechas a asignar
        private List<string> fechas = new List<string>
                {
                    "20/07/2024", "24/07/2024", "31/07/2024", "03/08/2024", "06/08/2024"
                };

        // Lista con los equipos seleccionados.
        List<int> rivales = new List<int> { 0, 0, 0, 0, 0 };

        // Lista con las posiciones vetadas para no jugar.
        List<int> noJugar = new List<int>();

        // Variables botones No Jugar
        private bool noJugar1 = false;
        private bool noJugar2 = false;
        private bool noJugar3 = false;
        private bool noJugar4 = false;
        private bool noJugar5 = false;

        // Lista con los ids de los partidos creados.
        private List<int> idsPartidos = new List<int>();

        public UC_Pretemporada(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;

            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
        }

        public UC_Pretemporada(Manager manager, int equipo, List<int> ids)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _idsPartidos = ids;

            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
        }
    }
}
