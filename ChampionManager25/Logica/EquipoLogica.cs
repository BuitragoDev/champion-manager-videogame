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

        // Llamada al método para Mostrar el Listado de todos los equipos excepto el elegido
        public List<Equipo> ListarOtrosEquipos(int equipoElegido)
        {
            return _datos.ListarOtrosEquipos(equipoElegido);
        }

        // Llamada al método para Mostrar el Listado de todos los equipos
        public List<Equipo> ListarTodosLosEquipos()
        {
            return _datos.ListarTodosLosEquipos();
        }

        // Llamada al método que actualiza los detalles de un equipo
        public void EditarEquipo(Equipo equipo)
        {
            _datos.EditarEquipo(equipo);
        }

        // Llamada al método que cambia un equipo de competicion
        public void AscenderDescenderEquipo(int equipo, int competicion)
        {
            _datos.AscenderDescenderEquipo(equipo, competicion);
        }

        // Llamada al método que cambia un Objetivo de Temporada
        public void CambiarObjetivoTemporada(int equipo, string objetivo)
        {
            _datos.CambiarObjetivoTemporada(equipo, objetivo);
        }

        // Llamada al método que resta una cantidad al Presupuesto
        public void RestarCantidadAPresupuesto(int equipo, int cantidad)
        {
            _datos.RestarCantidadAPresupuesto(equipo, cantidad);
        }

        // Llamada al método que suma una cantidad al Presupuesto
        public void SumarCantidadAPresupuesto(int equipo, int cantidad)
        {
            _datos.SumarCantidadAPresupuesto(equipo, cantidad);
        }

        // Llamada al método que actualiza el aforo de un estadio
        public void ActualizarAforo(int equipo, int aumento)
        {
            _datos.ActualizarAforo(equipo, aumento);
        }
    }
}
