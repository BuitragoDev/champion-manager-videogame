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
        public void RegistrarOferta(Transferencia oferta)
        {
            _datos.RegistrarOferta(oferta);
        }

        // Llamada al metodo que registra una transferencia por un jugador.
        public void RegistrarTransferencia(Transferencia transfer)
        {
            _datos.RegistrarTransferencia(transfer);
        }

        // Llamada al metodo que actualiza una oferta por un jugador.
        public void ActualizarOferta(Transferencia oferta)
        {
            _datos.ActualizarOferta(oferta);
        }

        // Llamada al metodo que comprueba si el jugador ya tiene una oferta aceptada por el equipo
        public bool ComprobarOfertaActiva(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarOfertaActiva(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al metodo que comprueba si el jugador ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaJugador(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarRespuestaJugador(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaEquipo(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarRespuestaEquipo(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al metodo que comprueba si el equipo ya ha aceptado o rechazado una oferta por el jugador
        public int ComprobarRespuestaEquipoCesion(int idJugador, int idEquipoComprador, int idEquipoVendedor)
        {
            return _datos.ComprobarRespuestaEquipoCesion(idJugador, idEquipoComprador, idEquipoVendedor);
        }

        // Llamada al método para Mostrar la Lista de Ofertas
        public List<Transferencia> ListarOfertas()
        {
            return _datos.ListarOfertas();
        }

        // Llamada al método para Mostrar la Lista de Traspasos y Cesiones
        public List<Transferencia> ListarTraspasos()
        {
            return _datos.ListarTraspasos();
        }

        // Llamada al método para Mostrar la Lista de Ofertas Sin Finalizar
        public List<Transferencia> ListarOfertasSinFinalizar()
        {
            return _datos.ListarOfertasSinFinalizar();
        }

        // Llamada al metodo que borra una oferta por un jugador
        public void BorrarOferta(int jugador)
        {
            _datos.BorrarOferta(jugador);
        }

        // Llamada al método para Mostrar los detalles de la oferta de un jugador
        public Transferencia MostrarDetallesOferta(int jugador)
        {
            return _datos.MostrarDetallesOferta(jugador);
        }

        // Llamada al metodo que resetea las tablas ofertas y transferencias
        public void ResetearTransferencias()
        {
            _datos.ResetearTransferencias();
        }
    }
}
