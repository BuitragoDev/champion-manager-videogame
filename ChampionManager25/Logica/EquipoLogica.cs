using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;

namespace ChampionManager25.Logica
{
    public class EquipoLogica
    {
        // Instanciar EquipoDatos dentro de la clase lógica
        private EquipoDatos _datos = new EquipoDatos();

        // Llamada al método que trae el listado de los equipos de una competición
        public List<Equipo> ListarEquiposCompeticion(int competicion)
        {
            return _datos.ListarEquiposCompeticion(competicion);
        }

        // Llamada al método que trae el listado de los equipos
        public Equipo ListarDetallesEquipo(int id)
        {
            return _datos.ListarDetallesEquipo(id);
        }

        // Llamada al método para Mostrar el Listado de todos los equipos
        public List<Equipo> ListarEquipos(int competicion)
        {
            return _datos.ListarEquipos(competicion);
        }

        // Llamada al método que devuelve la asistencia a un partido
        public int CalcularAsistencia(int idEquipoLocal)
        {
            return _datos.CalcularAsistencia(idEquipoLocal);
        }
    }
}
