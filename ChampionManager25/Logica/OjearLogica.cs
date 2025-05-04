using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class OjearLogica
    {
        // Instanciar OjearDatos dentro de la clase lógica
        private OjearDatos _datos = new OjearDatos();

        // Llamada al método que pone un jugador en la CARTERA
        public void PonerJugadorCartera(int jugador, Manager manager, int dias)
        {
            _datos.PonerJugadorCartera(jugador, manager, dias);
        }

        // Llamada al método que QUITA un jugador de la CARTERA
        public void QuitarJugadorCartera(int jugador, Manager manager)
        {
            _datos.QuitarJugadorCartera(jugador, manager);
        }

        // Llamada al método que dice si un jugador ya está siendo ojeado
        public bool ComprobarSiJugadorEnCartera(int jugador, Manager manager)
        {
            return _datos.ComprobarSiJugadorEnCartera(jugador, manager);
        }

        // Llamada al método que dice si un jugador ya ha sido ojeado
        public bool ComprobarJugadorOjeado(int jugador, Manager manager)
        {
            return _datos.ComprobarJugadorOjeado(jugador, manager);
        }

        // Llamada al método para mostrar la lista detallada de los jugadores ojeados
        public List<Jugador> ListadoJugadoresOjeados(int idManager)
        {
            return _datos.ListadoJugadoresOjeados(idManager);
        }
    }
}
