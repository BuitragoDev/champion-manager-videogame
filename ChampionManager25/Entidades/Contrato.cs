using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Contrato
    {
        public int IdContrato { get; set; }
        public int IdJugador { get; set; }
        public int IdEquipo { get; set; }
        public int SalarioAnual { get; set; }
        public int Duracion { get; set; } // En años
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int ClausulaRescision { get; set; }
        public int BonoPorGoles { get; set; }
        public int BonoPorPartidos { get; set; }

        // Constructor vacío
        public Contrato() { }

        // Constructor completo
        public Contrato(int idContrato, int idJugador, int idEquipo, int salarioAnual, int duracion,
                        string fechaInicio, string fechaFin, int clausulaRescision,
                        int bonoPorGoles, int bonoPorPartidos)
        {
            IdContrato = idContrato;
            IdJugador = idJugador;
            IdEquipo = idEquipo;
            SalarioAnual = salarioAnual;
            Duracion = duracion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            ClausulaRescision = clausulaRescision;
            BonoPorGoles = bonoPorGoles;
            BonoPorPartidos = bonoPorPartidos;
        }

        public override string ToString()
        {
            return $"Contrato #{IdContrato} | Jugador: {IdJugador} | Equipo: {IdEquipo}\n" +
                   $"- Salario Anual: {SalarioAnual} €\n" +
                   $"- Duración: {Duracion} años\n" +
                   $"- Desde: {FechaInicio} hasta: {FechaFin}\n" +
                   $"- Cláusula: {ClausulaRescision} €\n" +
                   $"- Bono por Goles: {BonoPorGoles} €, Bono por Partidos: {BonoPorPartidos} €";
        }
    }
}
