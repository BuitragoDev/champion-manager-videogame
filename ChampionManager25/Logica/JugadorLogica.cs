using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace ChampionManager25.Logica
{
    public class JugadorLogica
    {
        // Instanciar JugadorDatos
        private JugadorDatos _datos = new JugadorDatos();

        // Llamada al método que trae el jugador con mejor valoración media
        public Jugador MejorJugador(int id)
        {
            return _datos.MejorJugador(id);
        }

        // Llamada al método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> ListadoJugadoresCompleto(int id)
        {
            return _datos.ListadoJugadoresCompleto(id);
        }

        // Llamada al método que devuelve la media de un equipo
        public double ObtenerMediaEquipo(int idEquipo)
        {
            return _datos.ObtenerMediaEquipo(idEquipo);
        }

        // Llamada al método para mostrar el numero total de jugadores del juego
        public int NumeroJugadoresTotales()
        {
            return _datos.NumeroJugadoresTotales();
        }

        // Llamada al método para mostrar los datos de un jugador
        public Jugador MostrarDatosJugador(int id)
        {
            return _datos.MostrarDatosJugador(id);
        }

        // Llamada al método que renueva el status de un jugador
        public void RenovarStatusJugador(int jugador, int rol)
        {
            _datos.RenovarStatusJugador(jugador, rol);
        }

        // Llamada al método que cambia el equipo del jugador
        public void CambiarDeEquipo(int jugador, int equipo)
        {
            _datos.CambiarDeEquipo(jugador, equipo);
        }
    }
}
