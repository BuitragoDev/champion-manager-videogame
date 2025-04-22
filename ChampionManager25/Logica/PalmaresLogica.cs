using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class PalmaresLogica
    {
        // Instanciar PalmaresDatos dentro de la clase lógica
        private PalmaresDatos _datos = new PalmaresDatos();

        // Llamada al método que trae el listado de los equipos de una competición
        public int numTitulosEquipoCompeticion(int equipo, int competicion)
        {
            return _datos.numTitulosEquipoCompeticion(equipo, competicion);
        }

        // Llamada al método para Mostrar el Palmarés del Manager
        public List<PalmaresManager> MostrarPalmaresManager(int equipo, int manager)
        {
            return _datos.MostrarPalmaresManager(equipo, manager);
        }

        // Llamada al método para Mostrar el Palmarés Completo
        public List<Palmares> MostrarPalmaresCompleto()
        {
            return _datos.MostrarPalmaresCompleto();
        }

        // Llamada al método para Mostrar el Palmarés Completo de Copa Nacional
        public List<Palmares> MostrarPalmaresCompletoCopa()
        {
            return _datos.MostrarPalmaresCompletoCopa();
        }

        // Llamada al método para Mostrar el Palmarés del Balon de Oro
        public List<PalmaresJugador> MostrarPalmaresBalonOroTotal()
        {
            return _datos.MostrarPalmaresBalonOroTotal();
        }

        // Llamada al método para Mostrar el Palmarés de la Bota de Oro
        public List<PalmaresJugador> MostrarPalmaresBotaOroTotal()
        {
            return _datos.MostrarPalmaresBotaOroTotal();
        }

        // Llamada al método para Mostrar el Historial de las Finales
        public List<HistorialFinales> MostrarHistorialFinales()
        {
            return _datos.MostrarHistorialFinales();
        }

        // Llamada al método para Mostrar el Historial de las Finales de Copa Nacional
        public List<HistorialFinales> MostrarHistorialFinalesCopa()
        {
            return _datos.MostrarHistorialFinalesCopa();
        }

        // Llamada al metodo que suma un titulo al manager si gana la Liga
        public void AnadirTituloManager(int competicion, int equipo, int manager, int temporada)
        {
            _datos.AnadirTituloManager(competicion, equipo, manager, temporada);
        }

        // Llamada al metodo que suma un titulo al campeon de Liga
        public void AnadirTituloCampeon(int equipo)
        {
            _datos.AnadirTituloCampeon(equipo);
        }

        // Llamada al metodo que agrega el campeon y subcampeon de una temporada
        public void AnadirCampeonFinalista(int temporada, int campeon, int finalista)
        {
            _datos.AnadirCampeonFinalista(temporada, campeon, finalista);
        }

        // Llamada al método para Mostrar el Historial del Balon de Oro
        public List<HistorialJugador> MostrarPalmaresBalonOro()
        {
            return _datos.MostrarPalmaresBalonOro();
        }

        // Llamada al método para Mostrar el Historial de la Bota de Oro
        public List<HistorialJugador> MostrarPalmaresBotaOro()
        {
            return _datos.MostrarPalmaresBotaOro();
        }

        // Llamada al metodo que suma un titulo al jugador para el Balon de Oro
        public void AnadirTituloBalonOro(int jugador)
        {
            _datos.AnadirTituloBalonOro(jugador);
        }

        // Llamada al metodo que agrega los 3 premiados al balon de Oro, Plata y Bronce
        public void AnadirPremiosMejorJugador(int temporada, int oro, int plata, int bronce)
        {
            _datos.AnadirPremiosMejorJugador(temporada, oro, plata, bronce);
        }

        // Llamada al metodo que suma un titulo al jugador para el Bota de Oro
        public void AnadirTituloBotaOro(int jugador)
        {
            _datos.AnadirTituloBotaOro(jugador);
        }

        // Llamada al metodo que agrega los 3 premiados al Bota de Oro, Plata y Bronce
        public void AnadirPremiosMaximoGoleador(int temporada, int oro, int plata, int bronce)
        {
            _datos.AnadirPremiosMaximoGoleador(temporada, oro, plata, bronce);
        }

        // Llamada al metodo que suma un titulo al campeon de Copa Nacional
        public void AnadirTituloCampeonCopa(int equipo)
        {
            _datos.AnadirTituloCampeonCopa(equipo);
        }

        // Llamada al metodo que agrega el campeon y subcampeon de Copa Nacional
        public void AnadirCampeonFinalistaCopa(int temporada, int campeon, int finalista)
        {
            _datos.AnadirCampeonFinalistaCopa(temporada, campeon, finalista);
        }
    }
}
