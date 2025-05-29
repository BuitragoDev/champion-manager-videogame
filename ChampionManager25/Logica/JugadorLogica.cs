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
        public void CrearAlineacion(int equipo)
        {
            _datos.CrearAlineacion(equipo);
        }

        // Llamada al método que agrega un jugador a la alineacion
        public void AgregarJugadorAlineacion(int jugador)
        {
            _datos.AgregarJugadorAlineacion(jugador);
        }

        // Llamada al método que quita un jugador a la alineacion
        public void QuitarJugadorAlineacion(int idJugador)
        {
            _datos.QuitarJugadorAlineacion(idJugador);
        }

        // Llamada al método que resetea la tabla alineacion
        public void ResetearAlineacion()
        {
            _datos.ResetearAlineacion();
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

        // Llamada al método que cambia el contrato tras un fichaje
        public void CambiarContratoJugador(int jugador, int salario, int clausula, int anios, int bonusP, int bonusG, int equipo)
        {
            _datos.CambiarContratoJugador(jugador, salario, clausula, anios, bonusP, bonusG, equipo);
        }

        // Llamada al método para mostrar la lista de Jugadores en el Mercado
        public List<Jugador> ListadoJugadoresMercado(int equipo, int tipoStart, int tipoEnd, int mediaStart, int mediaEnd, int posicionStart, int posicionEnd)
        {
            return _datos.ListadoJugadoresMercado(equipo, tipoStart, tipoEnd, mediaStart, mediaEnd, posicionStart, posicionEnd);
        }

        // Llamada al método para mostrar la lista de Jugadores Por Filtros
        public List<Jugador> ListadoJugadoresPorFiltro(int equipo, string nacionalidad, int competicion, string posicion, int edadMin, int edadMax, int mediaMin, int mediaMax, int calidadMin, int calidadMax,
                               int velocidadMin, int velocidadMax, int resistenciaMin, int resistenciaMax, int agresividadMin, int agresividadMax)
        {
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

           

            return _datos.ListadoJugadoresPorFiltro(equipo, nacionalidad, competicion, pos, edadMin, edadMax, mediaMin, mediaMax, calidadMin, calidadMax,
                               velocidadMin, velocidadMax, resistenciaMin, resistenciaMax, agresividadMin, agresividadMax);
        }

        // Llamada al método que resta un año de contrato
        public void RestarAnioContrato(int jugador)
        {
            _datos.RestarAnioContrato(jugador);
        }

        // Llamada al método que borra un contrato
        public void BorrarContrato(int jugador)
        {
            _datos.BorrarContrato(jugador);
        }

        // Llamada al método para mostrar todos los contratos de jugadores
        public List<Contrato> MostrarListaTotalContratos()
        {
            return _datos.MostrarListaTotalContratos();
        }

        // Llamada al método para devuelve el salario medio de los jugadores con la misma media
        public int SalarioMedioJugadores(int jugador)
        {
            return _datos.SalarioMedioJugadores(jugador);
        }

        // Llamada al método para devuelve la clausula media de los jugadores con la misma media
        public int ClausulaMediaJugadores(int jugador)
        {
            return _datos.ClausulaMediaJugadores(jugador);
        }

        // Llamada al método que borra la Proxima Fecha de Negociacion
        public void BorrarFechaNegociacion(int jugador)
        {
            _datos.BorrarFechaNegociacion(jugador);
        }

        // Llamada al método que suma una semana lesionado
        public void SumarSemanaLesionado(int jugador)
        {
            _datos.SumarSemanaLesionado(jugador);
        }

        // Llamada al método que muestra las semanas lesionado
        public int SemanasLesionado(int jugador)
        {
            return _datos.SemanasLesionado(jugador);
        }

        // Llamada al método que pone al jugador en tratamiento por lesion
        public void ActivarTratamientoLesion(int jugador, int valor)
        {
            _datos.ActivarTratamientoLesion(jugador, valor);
        }

        // Llamada al método que quita al jugador el tipo de lesion
        public void QuitarTipoLesion(int jugador)
        {
            _datos.QuitarTipoLesion(jugador);
        }

        // Llamada al método que pone un jugador Transferible
        public void PonerTransferible(int jugador)
        {
            _datos.PonerTransferible(jugador);
        }

        // Llamada al método que pone un jugador Cedible
        public void PonerCedible(int jugador)
        {
            _datos.PonerCedible(jugador);
        }

        // Llamada al método que realiza los traspasos de la IA
        public void TraspasosIA(int equipo)
        {
            _datos.TraspasosIA(equipo);
        }

        // Llamada al método que comprueba si hay equipos que quieran hacerme ofertas
        public List<OfertaIA> OfertasMiEquipo(int miEquipo)
        {
            return _datos.OfertasMiEquipo(miEquipo);
        }

        // Llamada al método para decir si tengo portero en la alineacion titular
        public bool TengoPortero(int equipo)
        {
            return _datos.TengoPortero(equipo);
        }

        // Llamada al método para decir tengo 4 defensas en el 11 titular
        public bool TengoDefensas(int equipo)
        {
            return _datos.TengoDefensas(equipo);
        }

        // Llamada al método para decir tengo 1 delantero en el 11 titular
        public bool TengoDelanteros(int equipo)
        {
            return _datos.TengoDelanteros(equipo);
        }
    }
}
