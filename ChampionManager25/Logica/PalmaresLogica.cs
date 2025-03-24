using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class PalmaresLogica
    {
        // Instanciar PalmaresDatos dentro de la clase lógica
        private PalmaresDatos _datos = new PalmaresDatos();

        // Llamada al método que trae el listado de los equipos de una competición
        public int numTitulosEquipoCompeticion(int equipo, int competicion)
        {
            return _datos.numTitulosEquipoCompeticion(equipo, competicion);
        }

        // Llamada al método para Mostrar el Palmarés del Manager
        public List<PalmaresManager> MostrarPalmaresManager(int equipo, int manager)
        {
            return _datos.MostrarPalmaresManager(equipo, manager);
        }

        // Llamada al método para Mostrar el Palmarés del Manager
        public List<Palmares> MostrarPalmaresCompleto()
        {
            return _datos.MostrarPalmaresCompleto();
        }

        // Llamada al método para Mostrar el Historial de las Finales
        public List<HistorialFinales> MostrarHistorialFinales()
        {
            return _datos.MostrarHistorialFinales();
        }
    }
}
