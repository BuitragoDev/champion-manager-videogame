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

        // Llamada al método que crea un partido de Liga.
        public int crearPartido(int local, int visitante, string fecha, int competicion, int jornada, int idManager)
        {
            return _datos.crearPartido(local, visitante, fecha, competicion, jornada, idManager);
        }

        // Llamada al método para insertar un nuevo partido de Copa
        public int crearPartidoCopa(int local, int visitante, string fecha, int competicion, int ronda, int partidoVuelta, int idManager)
        {
            return _datos.crearPartidoCopa(local, visitante, fecha, competicion, ronda, partidoVuelta, idManager);
        }

        // Llamada al método para insertar un nuevo partido de Copa Europa 1
        public int CrearPartidoCopaEuropa(int local, int visitante, string fecha, int competicion, int jornada, int idMng)
        {
            return _datos.CrearPartidoCopaEuropa(local, visitante, fecha, competicion, jornada, idMng);
        }

        // Llamada al menerar calendario de Champions
        public void GenerarCalendarioChampions(List<Equipo> idEquipos, int idCompeticion, int idManager, DateTime fechaInicio)
        {
            _datos.GenerarCalendarioChampions(idEquipos, idCompeticion, idManager, fechaInicio);
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

        // Llamada al método que devuelve todos los partidos de mi equipo en Copa
        public List<Partido> MostrarMisPartidosCopaNacional(int equipo, int idManager)
        {
            return _datos.MostrarMisPartidosCopaNacional(equipo, idManager);
        }

        // Llamada al método que devuelve todos los partidos de mi equipo en Copa de Europa 1
        public List<Partido> MostrarMisPartidosCopaEuropa1(int equipo, int idManager)
        {
            return _datos.MostrarMisPartidosCopaEuropa1(equipo, idManager);
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

        // Llamada al metodo que carga los partidos de una Ronda de Copa Nacional
        public List<Partido> CargarRondaCopa(int ronda, int vuelta, int manager, int competicion)
        {
            return _datos.CargarRondaCopa(ronda, vuelta, manager, competicion);
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

        // Llamada al método que actualiza con el resultado de un partido de Copa
        public void ActualizarPartidoCopaNacional(Partido partido)
        {
            _datos.ActualizarPartidoCopaNacional(partido);
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

        // Llamada al método para obtener el nombre de una ronda de Copa
        public string ObtenerNombreRonda(int idRonda)
        {
            return _datos.ObtenerNombreRonda(idRonda);
        }

        // Llamada al método que devuelve una lista con los equipos clasificados de Copa
        public List<Equipo> ObtenerEquiposClasificados(int idRonda, int idCompeticion, int idManager)
        {
            return _datos.ObtenerEquiposClasificados(idRonda, idCompeticion, idManager);
        }

        // Llamada al método que devuelve la final de Copa Nacional
        public Partido ObtenerFinalCopa()
        {
            return _datos.ObtenerFinalCopa();
        }

        // Llamada al metodo que devuelve la ultima ronda de mi equipo en Copa nacional
        public string ObtenerUltimaRondaEquipo(int equipo)
        {
            return _datos.ObtenerUltimaRondaEquipo(equipo);
        }

        // Llamada al metodo que devuelve la ultima jornada de Liga jugada
        public int ObtenerUltimaJornadaJugada(int equipo)
        {
            return _datos.ObtenerUltimaJornadaJugada(equipo);
        }

        // Llamada al metodo que devuelve la ultima ronda de Copa jugada
        public int ObtenerUltimaRondaJugada(int equipo)
        {
            return _datos.ObtenerUltimaRondaJugada(equipo);
        }

        // Llamada al metodo que devuelve la ultima ronda de Copa jugada por mi equipo
        public int ObtenerUltimaRondaJugadaMiEquipo(int equipo)
        {
            return _datos.ObtenerUltimaRondaJugadaMiEquipo(equipo);
        }
    }
}
