using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class FinanzaLogica
    {
        private FinanzaDatos _datos = new FinanzaDatos();

        // Llamada al método que trae el listado de los equipos
        public List<Finanza> MostrarFinanzasEquipo(int manager, int temporada)
        {
            return _datos.MostrarFinanzasEquipo(manager, temporada);
        }

        // Llamada al método que crea un GASTO
        public void CrearGasto(Finanza finanza)
        {
            _datos.CrearGasto(finanza);
        }

        // Llamada al método que crea un INGRESO
        public void CrearIngreso(Finanza finanza)
        {
            _datos.CrearIngreso(finanza);
        }
    }
}
