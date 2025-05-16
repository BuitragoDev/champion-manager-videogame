using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Manager
    {
        // Atributos
        public int IdManager { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Nacionalidad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int? IdEquipo { get; set; } // Puede ser null
        public int CDirectiva { get; set; }
        public int CFans { get; set; }
        public int CJugadores { get; set; }
        public int Reputacion { get; set; }
        public int PartidosJugados { get; set; }
        public int PartidosGanados { get; set; }
        public int PartidosEmpatados { get; set; }
        public int PartidosPerdidos { get; set; }
        public int Puntos { get; set; }
        public string Tactica { get; set; }
        public int Despedido { get; set; }
        public int PrimeraTemporada { get; set; }

        public string RutaImagen { get; set; }

        // Constructor por defecto
        public Manager() { }

        // Constructor con parámetros
        public Manager(int idManager, string nombre, string apellido, string nacionalidad, DateTime fechaNacimiento, int? idEquipo,
            int cDirectiva, int cFans, int cJugadores, int reputacion, int partidosJugados, int partidosGanados, int partidosEmpatados,
            int partidosPerdidos, int puntos, string tactica, int despedido, string rutaImagen, int primeraTemporada)
        {
            IdManager = idManager;
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
            FechaNacimiento = fechaNacimiento;
            IdEquipo = idEquipo;
            CDirectiva = cDirectiva;
            CFans = cFans;
            CJugadores = cJugadores;
            Reputacion = reputacion;
            PartidosJugados = partidosJugados;
            PartidosGanados = partidosGanados;
            PartidosEmpatados = partidosEmpatados;
            PartidosPerdidos = partidosPerdidos;
            Puntos = puntos;
            Tactica = tactica;
            Despedido = despedido;
            RutaImagen = rutaImagen;
            PrimeraTemporada = primeraTemporada;
        }

        // Constructor con parámetros sin ID_MANAGER
        public Manager(string nombre, string apellido, string nacionalidad, DateTime fechaNacimiento, int? idEquipo, int cDirectiva,
            int cFans, int cJugadores, int reputacion, int partidosJugados, int partidosGanados, int partidosEmpatados,
            int partidosPerdidos, int puntos, string tactica, int despedido, string rutaImagen, int primeraTemporada)
        {
            Nombre = nombre;
            Apellido = apellido;
            Nacionalidad = nacionalidad;
            FechaNacimiento = fechaNacimiento;
            IdEquipo = idEquipo;
            CDirectiva = cDirectiva;
            CFans = cFans;
            CJugadores = cJugadores;
            Reputacion = reputacion;
            PartidosJugados = partidosJugados;
            PartidosGanados = partidosGanados;
            PartidosEmpatados = partidosEmpatados;
            PartidosPerdidos = partidosPerdidos;
            Puntos = puntos;
            Tactica = tactica;
            Despedido = despedido;
            RutaImagen = rutaImagen;
            PrimeraTemporada = primeraTemporada;
        }

        // Método ToString
        public override string ToString()
        {
            return $"Manager: {Nombre} {Apellido}\n" +
                   $"Nacionalidad: {Nacionalidad}\n" +
                   $"Fecha de Nacimiento: {FechaNacimiento.ToShortDateString()}\n" +
                   $"Equipo Actual: {(IdEquipo.HasValue ? IdEquipo.ToString() : "Sin equipo")}\n" +
                   $"Confianza Directiva: {CDirectiva}\n" +
                   $"Confianza Fans: {CFans}\n" +
                   $"Confianza Jugadores: {CJugadores}\n" +
                   $"Reputación: {Reputacion}\n" +
                   $"Partidos Jugados: {PartidosJugados}\n" +
                   $"Partidos Ganados: {PartidosGanados}\n" +
                   $"Partidos Empatados: {PartidosEmpatados}\n" +
                   $"Partidos Perdidos: {PartidosPerdidos}\n" +
                   $"Tactica: {Tactica}\n" +
                   $"Puntos: {Puntos}\n" +
                   $"Despedido: {Despedido}\n" +
                   $"PrimeraTemporada: {PrimeraTemporada}\n" +
                   $"Ruta: {RutaImagen}";
        }
    }
}
