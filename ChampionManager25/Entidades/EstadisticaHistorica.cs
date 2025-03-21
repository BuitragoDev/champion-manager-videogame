using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class EstadisticaHistorica
    {
        public int IdEstadistica { get; set; }
        public int IdEquipo { get; set; }
        public string NombreJugador { get; set; }
        public string Temporada { get; set; }
        public string Categoria { get; set; }  // Ej: "Goles", "Asistencias", "Tarjetas Amarillas", "Tarjetas Rojas"
        public int Valor { get; set; }  // Número de goles, asistencias, tarjetas, etc.

        // Constructor vacío
        public EstadisticaHistorica() { }

        // Constructor con parámetros
        public EstadisticaHistorica(int idEstadistica, int idEquipo, string nombreJugador, string temporada, string categoria, int valor)
        {
            IdEstadistica = idEstadistica;
            IdEquipo = idEquipo;
            NombreJugador = nombreJugador;
            Temporada = temporada;
            Categoria = categoria;
            Valor = valor;
        }

        // Constructor con parámetros SIN ID
        public EstadisticaHistorica(int idEquipo, string nombreJugador, string temporada, string categoria, int valor)
        {
            IdEquipo = idEquipo;
            NombreJugador = nombreJugador;
            Temporada = temporada;
            Categoria = categoria;
            Valor = valor;
        }

        // Sobrescribimos ToString() para una mejor visualización
        public override string ToString()
        {
            return $"{NombreJugador} - {Categoria}: {Valor} ({Temporada})";
        }
    }
}
