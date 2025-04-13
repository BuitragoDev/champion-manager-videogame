using ChampionManager25.Datos;
using ChampionManager25.Entidades;
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

        // Llamada al método para Mostrar los datos de la competicion
        public Competicion ObtenerCompeticion(int idCompeticion)
        {
            return _datos.ObtenerCompeticion(idCompeticion);
        }

        // Llamada al método que cambia el nombre de la competicion
        public void EditarCompeticion(Competicion competicion)
        {
            _datos.EditarCompeticion(competicion);
        }

        // Llamada al método que muestra las 2 competiciones
        public List<Competicion> MostrarCompeticiones()
        {
            return _datos.MostrarCompeticiones();
        }
    }
}
