using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class CompeticionLogica
    {
        // Instanciar CompeticionDatos dentro de la clase lógica
        private CompeticionDatos _datos = new CompeticionDatos();

        // Llamada al método para Mostrar el Nombre de la Competición
        public string MostrarNombreCompeticion(int competicion)
        {
            return _datos.MostrarNombreCompeticion(competicion);
        }
    }
}
