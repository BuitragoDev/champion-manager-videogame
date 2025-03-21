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

        // Llamada al método para Crear una fila de estadística para jugador del juego
        public void InsertarEstadisticasJugadores(int numJugadores, int manager)
        {
            _datos.InsertarEstadisticasJugadores(numJugadores, manager);
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

        // Llamada al método que devuelve el jugador con más Goles + Asistencias
        public Estadistica MostrarJugadorConMasGolesAsistencias(int equipo)
        {
            return _datos.MostrarJugadorConMasGolesAsistencias(equipo);
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

        // Llamada al método que devuelve el jugador con más Hattrick
        public Estadistica MostrarJugadorConMasHattrick(int equipo)
        {
            return _datos.MostrarJugadorConMasHattrick(equipo);
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
    }
}
