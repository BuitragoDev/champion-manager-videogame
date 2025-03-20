using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Equipo
    {
        // Propiedades
        public int IdEquipo { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string Presidente { get; set; }
        public string Ciudad { get; set; }
        public string Estadio { get; set; }
        public int Aforo { get; set; }
        public int Reputacion { get; set; }
        public string Objetivo { get; set; }
        public int Rival { get; set; }
        public int IdCompeticion { get; set; }

        // Nuevas propiedades para el entrenador.
        public string? Entrenador { get; set; } // Nombre completo del entrenador
        public int ReputacionEntrenador { get; set; } // Reputación del entrenador

        // Constructor vacío
        public Equipo() { }

        // Constructor con parámetros
        public Equipo(int idEquipo, string nombre, string nombreCorto, string presidente, string ciudad,
                      string estadio, int aforo, int reputacion, string objetivo, int rival, int idCompeticion)
        {
            IdEquipo = idEquipo;
            Nombre = nombre;
            NombreCorto = nombreCorto;
            Presidente = presidente;
            Ciudad = ciudad;
            Estadio = estadio;
            Aforo = aforo;
            Reputacion = reputacion;
            Objetivo = objetivo;
            Rival = rival;
            IdCompeticion = idCompeticion;
        }

        // Método ToString para representar el objeto en formato de texto
        public override string ToString()
        {
            return $"Equipo: {Nombre} ({NombreCorto}) - Ciudad: {Ciudad}, Estadio: {Estadio} (Aforo: {Aforo}) - " +
                   $"Presidente: {Presidente}, Reputación: {Reputacion}, " +
                   $"Objetivo: {Objetivo}, Rival1: {Rival.ToString() ?? "N/A"}, Competición: {IdCompeticion}";
        }
    }
}
