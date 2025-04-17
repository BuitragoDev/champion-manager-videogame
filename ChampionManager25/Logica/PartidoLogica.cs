using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;

namespace ChampionManager25.Logica
{
    public class PartidoLogica
    {
        PartidoDatos _datos = new PartidoDatos();

        // Llamada al método que crea un partido.
        public int crearPartido(int local, int visitante, string fecha, int competicion, int jornada, int idManager)
        {
            return _datos.crearPartido(local, visitante, fecha, competicion, jornada, idManager);
        }

        // Llamada al método que elimina un partido.
        public void eliminarPartidos(List<int> idsPartidos)
        {
            _datos.eliminarPartidos(idsPartidos);
        }

        // Llamada al método que muestra el próximo partido
        public Partido ObtenerProximoPartido(int equipo, int idManager, DateTime hoy)
        {
            return _datos.ObtenerProximoPartido(equipo, idManager, hoy);
        }


        // Llamada al método que muestra el último partido
        public Partido ObtenerUltimoPartido(int equipo, int idManager, DateTime hoy)
        {
            return _datos.ObtenerUltimoPartido(equipo, idManager, hoy);
        }

        // Llamada al método que obtiene los partidos de mi equipo.
        public List<Partido> MostrarMisPartidos(int equipo, int idManager)
        {
            return _datos.MostrarMisPartidos(equipo, idManager);
        }
        // Llamada al método que devuelve un partido en base a una fecha
        public Partido MostrarDetallesPartido(int equipo, int idManager, DateTime fecha)
        {
            return _datos.MostrarDetallesPartido(equipo, idManager, fecha);
        }

        // Llamada al método que devuelve mi próximo partido en casa
        public Partido MostrarProximoPartidoLocal(int equipo, int idManager, DateTime fecha)
        {
            return _datos.MostrarProximoPartidoLocal(equipo, idManager, fecha);
        }

        // Llamada al metodo que carga los partidos de una jornada
        public List<Partido> CargarJornada(int jornada, int manager, int competicion)
        {
            return _datos.CargarJornada(jornada, manager, competicion);
        }

        // Llamada al método que devuelve los ultimos 5 partidos de un equipo
        public List<Partido> UltimosCincoPartidos(int equipo, int idManager)
        {
            return _datos.UltimosCincoPartidos(equipo, idManager);
        }

        // Llamada al método que devuelve los partidos de hoy que NO juega mi equipo
        public List<Partido> PartidosHoy(int miEquipo, int idManager)
        {
            return _datos.PartidosHoy(miEquipo, idManager);
        }

        // Llamada al método que actualiza con el resultado de un partido
        public void ActualizarPartido(Partido partido)
        {
            _datos.ActualizarPartido(partido);
        }

        // Llamada al método para obtener la fecha del ultimo partido
        public string ultimoPartidoCalendario()
        {
            return _datos.ultimoPartidoCalendario();
        }

        // Llamada al método para crear el calendario
        public void GenerarCalendario(int temporadaActual, int idManager, int idCompeticion)
        {
            _datos.GenerarCalendario(temporadaActual, idManager, idCompeticion);
        }

        // Llamada al método que resetea la tabla partidos
        public void ResetearPartidos()
        {
            _datos.ResetearPartidos();
        }
    }
}
