using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;

namespace ChampionManager25.Logica
{
    public class EstadisticasLogica
    {
        // Instanciar EstadisticaDatos dentro de la clase lógica
        private EstadisticaDatos _datos = new EstadisticaDatos();

        // Llamada al método para Crear una fila de estadística para jugador de liga
        public void InsertarEstadisticasJugadores(int manager)
        {
            _datos.InsertarEstadisticasJugadores(manager);
        }

        // Llamada al método para Crear una fila de estadística para jugador de Europa
        public void InsertarEstadisticasJugadoresEuropa(int manager)
        {
            _datos.InsertarEstadisticasJugadoresEuropa(manager);
        }

        // Llamada al metodo que inserta una fila estadistica de un jugador
        public void InsertarLineaEstadisticaJugador(int numJugador, int manager)
        {
            _datos.InsertarLineaEstadisticaJugador(numJugador, manager);
        }

        // Llamada al método que devuelve el jugador con más Goles
        public Estadistica MostrarJugadorConMasGoles(int equipo)
        {
            return _datos.MostrarJugadorConMasGoles(equipo);
        }

        // Llamada al método que devuelve el jugador con más Asistencias
        public Estadistica MostrarJugadorConMasAsistencias(int equipo)
        {
            return _datos.MostrarJugadorConMasAsistencias(equipo);
        }

        // Llamada al método que devuelve el jugador con más MVP
        public Estadistica MostrarJugadorConMasMvp(int equipo)
        {
            return _datos.MostrarJugadorConMasMvp(equipo);
        }

        // Llamada al método que devuelve el jugador con más Tarjetas Amarillas
        public Estadistica MostrarJugadorConMasTarjetasAmarillas(int equipo)
        {
            return _datos.MostrarJugadorConMasTarjetasAmarillas(equipo);
        }

        // Llamada al método que devuelve el jugador con más Tarjetas Rojas
        public Estadistica MostrarJugadorConMasTarjetasRojas(int equipo)
        {
            return _datos.MostrarJugadorConMasTarjetasRojas(equipo);
        }

        // Llamada al método que devuelve la ESTADÍSTICA HISTÓRICA DE UN EQUIPO
        public EstadisticaHistorica MostrarEstadisticaHistoricaEquipo(int equipo, string categoria)
        {
            return _datos.MostrarEstadisticaHistoricaEquipo(equipo, categoria);
        }

        // Llamada al método que devuelve la ESTADÍSTICA HISTÓRICA TOTAL DE UN EQUIPO
        public EstadisticaHistorica MostrarEstadisticaHistoricaTotalEquipo(int equipo, string categoria)
        {
            return _datos.MostrarEstadisticaHistoricaTotalEquipo(equipo, categoria);
        }

        // Llamada al método para mostrar las estadísticas de un Equipo
        public List<Estadistica> MostrarEstadisticasEquipo(int id, int manager)
        {
            return _datos.MostrarEstadisticasEquipo(id, manager);
        }

        // Llamada al método para mostrar las estadísticas de toda la competicion
        public List<Estadistica> MostrarEstadisticasTotales(int manager, int filtro, int competicion)
        {
            return _datos.MostrarEstadisticasTotales(manager, filtro, competicion);
        }

        // Llamada al método para mostrar las estadísticas de un Jugador
        public Estadistica MostrarEstadisticasJugador(int id, int manager)
        {
            return _datos.MostrarEstadisticasJugador(id, manager);
        }

        // Llamada al método para actualizar las estadisticas de los jugadores
        public void ActualizarEstadisticas(Estadistica estadistica)
        {
            _datos.ActualizarEstadisticas(estadistica);
        }

        // Llamada al método para actualizar las estadisticas de los jugadores de Europa
        public void ActualizarEstadisticasEuropa(Estadistica estadistica)
        {
            _datos.ActualizarEstadisticasEuropa(estadistica);
        }

        // Llamada al método para resetea la estadistica de un jugador
        public void ResetearEstadisticaJugador(int jugador)
        {
            _datos.ResetearEstadisticaJugador(jugador);
        }

        // Llamada al metodo que resetea las estadisticas de los jugadores
        public void ResetearEstadisticas()
        {
            _datos.ResetearEstadisticas();
        }

        // Llamada al metodo que resetea las estadisticas de los jugadores
        public void ResetearEstadisticasEuropa()
        {
            _datos.ResetearEstadisticasEuropa();
        }

        // Llamada al metodo que muestra los 3 mejores jugadores de la temporada en Liga
        public List<Jugador> BalonDeOro()
        {
            return _datos.BalonDeOro();
        }

        // Llamada al metodo que muestra los 3 mejores jugadores de la temporada en Copa de Europa 1
        public List<Jugador> BalonDeOroEuropa1()
        {
            return _datos.BalonDeOroEuropa1();
        }

        // Llamada al metodo que muestra los 3 mejores jugadores de la temporada en Copa de Europa 2
        public List<Jugador> BalonDeOroEuropa2()
        {
            return _datos.BalonDeOroEuropa2();
        }

        // Llamada al metodo que muestra los 3 mejores jugadores de la temporada
        public List<Jugador> BotaDeOro()
        {
            return _datos.BotaDeOro();
        }

        // Llamada al metodo que muestra el mejor 11 de la temporada
        public List<Jugador> MejorOnceTemporada()
        {
            return _datos.MejorOnceTemporada();
        }
    }
}
