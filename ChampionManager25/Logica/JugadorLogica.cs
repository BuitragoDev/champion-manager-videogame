using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        // Llamada al método para mostrar todos los jugadores de la base de datos
        public List<Jugador> MostrarListaTotalJugadores()
        {
            return _datos.MostrarListaTotalJugadores();
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

        // Llamada al método para mostrar la lista de Jugadores de un equipo SIN PORTEROS
        public List<Jugador> PlantillaSinPorteros(int id)
        {
            return _datos.PlantillaSinPorteros(id);
        }

        // Llamada al método para crear los 16 jugadores que jugaran el partido
        public List<Jugador> JugadoresJueganPartido(int id)
        {
            return _datos.JugadoresJueganPartido(id);
        }

        // Llamada al método para crear los 16 jugadores que jugaran el partido DE MI EQUIPO
        public List<Jugador> JugadoresMiEquipoJueganPartido(int id)
        {
            return _datos.JugadoresMiEquipoJueganPartido(id);
        }

        // Llamada al método que pone a un jugador como lesionado
        public void PonerJugadorLesionado(int jugador, int duracion, string tipo)
        {
            _datos.PonerJugadorLesionado(jugador, duracion, tipo);
        }

        // Llamada al método que pone a un jugador como sancionado
        public void PonerJugadorSancionado(int jugador, int duracion)
        {
            _datos.PonerJugadorSancionado(jugador, duracion);
        }

        // Llamada al método para decir si un jugador pertenece a mi equipo
        public bool EsDeMiEquipo(int jugador, int equipo)
        {
            return _datos.EsDeMiEquipo(jugador, equipo);
        }

        // Llamada al método que incrementa/decrementa la moral y el estado de forma
        public void ActualizarMoralEstadoForma(int jugador, int moral, int estadoForma)
        {
            _datos.ActualizarMoralEstadoForma(jugador, moral, estadoForma);
        }

        // Llamada al método que actualiza los atributos por entrenamiento
        public void ActualizarAtributosEntrenamiento(int jugador, int portero, int pase, int regate, int remate, int entradas, int tiro)
        {
            _datos.ActualizarAtributosEntrenamiento(jugador, portero, pase, regate, remate, entradas, tiro);
        }

        public void ActualizarOtrosAtributosEntrenamiento(int jugador, int velocidad, int resistencia, int agresividad, int calidad)
        {
            _datos.ActualizarOtrosAtributosEntrenamiento(jugador, velocidad, resistencia, agresividad, calidad);
        }

        // Llamada al método que resetea la moral y el estado de forma
        public void ResetearMoralEstadoForma()
        {
            _datos.ResetearMoralEstadoForma();
        }

        // Llamada al método que actualiza un jugador
        public void ActualizarJugador(Jugador jugador)
        {
            _datos.ActualizarJugador(jugador);
        }

        // Llamada al método que reduce una lesion un %
        public void TratarLesion(int idJugador, int porcentaje)
        {
            _datos.TratarLesion(idJugador, porcentaje);
        }

        // Llamada al método que pone un jugador en el mercado
        public void PonerJugadorEnMercado(int jugador, int opcion)
        {
            _datos.PonerJugadorEnMercado(jugador, opcion);
        }

        // Llamada al método que quita un jugador del mercado
        public void QuitarJugadorDeMercado(int jugador)
        {
            _datos.QuitarJugadorDeMercado(jugador);
        }

        // Llamada al método que despide un jugador del equipo
        public void DespedirJugador(int jugador)
        {
            _datos.DespedirJugador(jugador);
        }

        // Llamada al método que elimina un contrato de un jugador
        public void EliminarContratoJugador(int jugador)
        {
            _datos.EliminarContratoJugador(jugador);
        }

        // Llamada al método que crea una nueva fecha de negociación
        public void NegociacionCancelada(int jugador, int dias)
        {
            _datos.NegociacionCancelada(jugador, dias);
        }

        // Llamada al método que renueva el contrato de un jugador
        public void RenovarContratoJugador(int jugador, int salario, int clausula, int anios, int bonusP, int bonusG)
        {
            _datos.RenovarContratoJugador(jugador, salario, clausula, anios, bonusP, bonusG);
        }

        // Llamada al método para mostrar la lista de Jugadores en el Mercado
        public List<Jugador> ListadoJugadoresMercado(int equipo, int tipoStart, int tipoEnd, int mediaStart, int mediaEnd, int posicionStart, int posicionEnd)
        {
            return _datos.ListadoJugadoresMercado(equipo, tipoStart, tipoEnd, mediaStart, mediaEnd, posicionStart, posicionEnd);
        }

        // Llamada al método para mostrar la lista de Jugadores Por Filtros
        public List<Jugador> ListadoJugadoresPorFiltro(int equipo, string nacionalidad, string competicion, string posicion, string edad, string media, string calidad, string velocidad, string resistencia, string agresividad)
        {
            // Competiciones
            int comp = 0;
            if (competicion == "LaLiga EA Sports")
            {
                comp = 1;
            }
            else if (competicion == "LaLiga Hypermotion")
            {
                comp = 2;
            }
            else if (competicion == "Primera Federación")
            {
                comp = 3;
            }

            // Posiciones
            var posiciones = new Dictionary<string, int>
            {
                { "Portero", 1 },
                { "Lateral Derecho", 2 },
                { "Lateral Izquierdo", 3 },
                { "Central", 4 },
                { "Mediocentro Defensivo", 5 },
                { "Mediocentro", 6 },
                { "Mediocentro Ofensivo", 7 },
                { "Extremo Derecho", 8 },
                { "Extremo Izquierdo", 9 },
                { "Delantero Centro", 10 },
            };
            int pos = posiciones.GetValueOrDefault(posicion, 0);

            // Edad
            var rangosEdad = new Dictionary<string, (int min, int max)>
            {
                { ">40", (40, 100) },
                { "30-40", (30, 40) },
                { "25-35", (25, 35) },
                { "18-30", (18, 30) },
                { "18-25", (18, 25) },
                { "<18", (0, 18) }
            };

            (int edadMin, int edadMax) = rangosEdad.GetValueOrDefault(edad, (0, 100));

            // Media
            var rangosMedia = new Dictionary<string, (int min, int max)>
            {
                { "0-100", (0, 100) },
                { "0-90", (0, 90) },
                { "80-100", (80, 100) },
                { "0-80", (0, 80) },
                { "0-70", (0, 70) },
                { "0-60", (0, 60) },
                { "0-50", (0, 50) }
            };

            (int mediaMin, int mediaMax) = rangosMedia.GetValueOrDefault(media, (0, 100));

            // Calidad
            var rangosCalidad = new Dictionary<string, int>
            {
                { "0-100", 100 },
                { "0-90", 90 },
                { "0-80", 80 },
                { "0-70", 70 },
                { "0-60", 60 },
                { "0-50", 50 }
            };

            int calidadMax = rangosCalidad.GetValueOrDefault(calidad, (100));

            // Velocidad
            var rangosVelocidad = new Dictionary<string, int>
            {
                { "0-100", 100 },
                { "0-90", 90 },
                { "0-80", 80 },
                { "0-70", 70 },
                { "0-60", 60 },
                { "0-50", 50 }
            };

            int velocidadMax = rangosVelocidad.GetValueOrDefault(velocidad, (100));

            // Resistencia
            var rangosResistencia = new Dictionary<string, int>
            {
                { "0-100", 100 },
                { "0-90", 90 },
                { "0-80", 80 },
                { "0-70", 70 },
                { "0-60", 60 },
                { "0-50", 50 }
            };

            int resistenciaMax = rangosResistencia.GetValueOrDefault(resistencia, (100));

            // Agresividad
            var rangosAgresividad = new Dictionary<string, int>
            {
                { "0-100", 100 },
                { "0-90", 90 },
                { "0-80", 80 },
                { "0-70", 70 },
                { "0-60", 60 },
                { "0-50", 50 }
            };

            int agresividadMax = rangosAgresividad.GetValueOrDefault(agresividad, (100));

            return _datos.ListadoJugadoresPorFiltro(equipo, nacionalidad, comp, pos, edadMin, edadMax, mediaMin, mediaMax, calidadMax, velocidadMax, resistenciaMax, agresividadMax);
        }
    }
}
