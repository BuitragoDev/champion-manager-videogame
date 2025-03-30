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

        // Llamada al método para mostrar los titulares del equipo
        public void CrearAlineacion(string tactica, int equipo)
        {
            _datos.CrearAlineacion(tactica, equipo);
        }

        // Llamada al método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> MostrarAlineacion(int inicio, int final)
        {
            return _datos.MostrarAlineacion(inicio, final);
        }

        // Llamada al método que cambia las posiciones entre 2 jugadores
        public void IntercambioPosicion(int jugador1, int jugador2, int posicion1, int posicion2)
        {
            _datos.IntercambioPosicion(jugador1, jugador2, posicion1, posicion2);
        }

        // Llamada al método que crea los 3 capitanes de mas edad
        public void CrearCapitanes(int equipo)
        {
            _datos.CrearCapitanes(equipo);
        }

        // Llamada al método que crea una lista con los 3 capitanes
        public Capitan MostrarCapitanes()
        {
            return _datos.MostrarCapitanes();
        }

        // Llamada al método para mostrar el entrenamiento de un jugador
        public int EntrenamientoJugador(int jugador)
        {
            return _datos.EntrenamientoJugador(jugador);
        }

        // Llamada al método que selecciona un entrenamiento para un jugador
        public void EntrenarJugador(int jugador, int tipo)
        {
            _datos.EntrenarJugador(jugador, tipo);
        }
    }
}
