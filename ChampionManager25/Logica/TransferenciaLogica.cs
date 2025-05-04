using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;

namespace ChampionManager25.Logica
{
    public class TransferenciaLogica
    {
        // Instanciar ManagerDatos dentro de la clase lógica
        private TransferenciaDatos _datos = new TransferenciaDatos();

        // Llamada al metodo que evalua una oferta por un jugador.
        public Transferencia EvaluarOfertaEquipo(int idJugador, int idEquipoComprador, int montoOferta, int tipoFichaje)
        {
            return _datos.EvaluarOfertaEquipo(idJugador, idEquipoComprador, montoOferta, tipoFichaje);
        }

        // Llamada al metodo que registra una oferta por un jugador.
        public void RegistrarOferta(Transferencia oferta, int respuestaEquipo)
        {
            _datos.RegistrarOferta(oferta, respuestaEquipo);
        }

        // Llamada al metodo que actualiza una oferta por un jugador.
        public void ActualizarOferta(Transferencia oferta, int respuestaEquipo)
        {
            _datos.ActualizarOferta(oferta, respuestaEquipo);
        }

        // Llamada al metodo que comprueba si el jugador ya tiene una oferta aceptada por el equipo
        public bool ComprobarOfertaActiva(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarOfertaActiva(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaEquipo(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarRespuestaEquipo(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al método que actualiza la fecha de traspaso
        public void ActualizarFechaTraspaso(int idJugador, int idEquipoDestino, int idEquipoOrigen, int tipoFichaje)
        {
            _datos.ActualizarFechaTraspaso(idJugador, idEquipoDestino, idEquipoOrigen, tipoFichaje);
        }
    }
}
