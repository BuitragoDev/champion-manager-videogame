using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Transferencia
    {
        public int IdFichaje { get; set; }
        public int IdJugador { get; set; }
        public int IdEquipoOrigen { get; set; }
        public int IdEquipoDestino { get; set; }
        public int TipoFichaje { get; set; }
        public int MontoOferta { get; set; }
        public string FechaOferta { get; set; }
        public int SalarioAnual { get; set; }
        public string FinContrato { get; set; }
        public int ClausulaRescision { get; set; }
        public int BonoPorGoles { get; set; }
        public int BonoPorPartidos { get; set; }
        public int? RespuestaEquipo { get; set; }
        public int? RespuestaJugador { get; set; }
        public int ValorMercado { get; set; }
        public int SituacionMercado { get; set; }
        public int Moral { get; set; }
        public int EstadoAnimo { get; set; }
        public int Status { get; set; }
        public int PresupuestoComprador { get; set; }
        public int PresupuestoVendedor { get; set; }
        public int Rival1 { get; set; }
        public int Rival2 { get; set; }
        public int Duracion { get; set; }

        // Constructor vacío
        public Transferencia() { }

        // Constructor con parámetros
        public Transferencia(int idFichaje, int idJugador, int idEquipoOrigen, int idEquipoDestino, int tipoFichaje,
                              int montoOferta, string fechaOferta, int salarioAnual, string finContrato, int clausulaRescision,
                              int bonoPorGoles, int bonoPorPartidos, int? respuestaEquipo, int? respuestaJugador,
                              int valorMercado, int situacionMercado, int moral, int estadoAnimo, int status, int presupuestoComprador,
                              int presupuestoVendedor, int rival1, int rival2, int duracion)
        {
            IdFichaje = idFichaje;
            IdJugador = idJugador;
            IdEquipoOrigen = idEquipoOrigen;
            IdEquipoDestino = idEquipoDestino;
            TipoFichaje = tipoFichaje;
            MontoOferta = montoOferta;
            FechaOferta = fechaOferta;
            SalarioAnual = salarioAnual;
            FinContrato = finContrato;
            ClausulaRescision = clausulaRescision;
            BonoPorGoles = bonoPorGoles;
            BonoPorPartidos = bonoPorPartidos;
            RespuestaEquipo = respuestaEquipo;
            RespuestaJugador = respuestaJugador;
            ValorMercado = valorMercado;
            SituacionMercado = situacionMercado;
            Moral = moral;
            EstadoAnimo = estadoAnimo;
            Status = status;
            PresupuestoComprador = presupuestoComprador;
            PresupuestoVendedor = presupuestoVendedor;
            Rival1 = rival1;
            Rival2 = rival2;
            Duracion = duracion;
        }

        // Constructor con parámetros sin ID
        public Transferencia(int idJugador, int idEquipoOrigen, int idEquipoDestino, int tipoFichaje,
                              int montoOferta, string fechaOferta, int salarioAnual, string finContrato, int clausulaRescision,
                              int bonoPorGoles, int bonoPorPartidos, int? respuestaEquipo, int? respuestaJugador,
                              int valorMercado, int situacionMercado, int moral, int estadoAnimo, int status, int presupuestoComprador,
                              int presupuestoVendedor, int rival1, int rival2, int duracion)
        {
            IdJugador = idJugador;
            IdEquipoOrigen = idEquipoOrigen;
            IdEquipoDestino = idEquipoDestino;
            TipoFichaje = tipoFichaje;
            MontoOferta = montoOferta;
            FechaOferta = fechaOferta;
            SalarioAnual = salarioAnual;
            FinContrato = finContrato;
            ClausulaRescision = clausulaRescision;
            BonoPorGoles = bonoPorGoles;
            BonoPorPartidos = bonoPorPartidos;
            RespuestaEquipo = respuestaEquipo;
            RespuestaJugador = respuestaJugador;
            ValorMercado = valorMercado;
            SituacionMercado = situacionMercado;
            Moral = moral;
            EstadoAnimo = estadoAnimo;
            Status = status;
            PresupuestoComprador = presupuestoComprador;
            PresupuestoVendedor = presupuestoVendedor;
            Rival1 = rival1;
            Rival2 = rival2;
            Duracion = duracion;
        }

        public override string ToString()
        {
            return $"IdFichaje: {IdFichaje}, IdJugador: {IdJugador}, IdEquipoOrigen: {IdEquipoOrigen}, IdEquipoDestino: {IdEquipoDestino}, " +
                   $"TipoFichaje: {TipoFichaje}, MontoOferta: {MontoOferta}, FechaOferta: {FechaOferta}, SalarioAnual: {SalarioAnual}, " +
                   $"FinContrato: {FinContrato}, ClausulaRescision: {ClausulaRescision}, BonoPorGoles: {BonoPorGoles}, BonoPorPartidos: {BonoPorPartidos}, " +
                   $"RespuestaEquipo: {RespuestaEquipo?.ToString() ?? "N/A"}, RespuestaJugador: {RespuestaJugador?.ToString() ?? "N/A"}, " +
                   $"ValorMercado: {ValorMercado}, SituacionMercado: {SituacionMercado}, Moral: {Moral}, EstadoAnimo: {EstadoAnimo}, " +
                   $"Status: {Status}, PresupuestoComprador: {PresupuestoComprador}, PresupuestoVendedor: {PresupuestoVendedor}, " +
                   $"Rival1: {Rival1}, Rival2: {Rival2}, Duracion: {Duracion}";
        }
    }
}
