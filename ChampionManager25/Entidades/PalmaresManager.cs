using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class PalmaresManager
    {
        // Atributos
        public int IdPalmares { get; set; }
        public int IdEquipo { get; set; }
        public int IdManager { get; set; }
        public int IdCompeticion { get; set; }
        public string? Temporada { get; set; }

        // Constructor por defecto
        public PalmaresManager()
        {
        }

        // Constructor con parámetros
        public PalmaresManager(int idPalmares, int idEquipo, int idCompeticion, int manager, string temporada)
        {
            IdPalmares = idPalmares;
            IdEquipo = idEquipo;
            IdCompeticion = idCompeticion;
            IdManager = manager;
            Temporada = temporada;
        }

        // Constructor con parámetros sin ID
        public PalmaresManager(int idEquipo, int idCompeticion, int manager, string temporada)
        {
            IdEquipo = idEquipo;
            IdCompeticion = idCompeticion;
            IdManager = manager;
            Temporada = temporada;
        }

        // Método ToString
        public override string ToString()
        {
            return $"IdPalmares: {IdPalmares}, IdEquipo: {IdEquipo}, IdCompeticion: {IdCompeticion}, IdManager: {IdManager}, Temporada: {Temporada}";
        }
    }
}
