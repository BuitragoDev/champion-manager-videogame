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
        public int Presupuesto { get; set; }
        public string Ciudad { get; set; }
        public string Estadio { get; set; }
        public int Aforo { get; set; }
        public int Reputacion { get; set; }
        public string Objetivo { get; set; }
        public int Rival { get; set; }
        public int IdCompeticion { get; set; }
        public int CompeticionEuropea { get; set; }
        public string RutaImagen { get; set; }
        public string RutaImagen120 { get; set; }
        public string RutaImagen80 { get; set; }
        public string RutaImagen64 { get; set; }
        public string RutaImagen32 { get; set; }
        public string RutaEstadioInterior { get; set; }
        public string RutaEstadioExterior { get; set; }
        public string RutaKitLocal { get; set; }
        public string RutaKitVisitante { get; set; }

        // Nuevas propiedades para el entrenador.
        public string? Entrenador { get; set; } // Nombre completo del entrenador
        public int ReputacionEntrenador { get; set; } // Reputación del entrenador

        // Constructor vacío
        public Equipo() { }

        // Constructor con parámetros
        public Equipo(int idEquipo, string nombre, string nombreCorto, string presidente, int presupuesto, string ciudad,
                      string estadio, int aforo, int reputacion, string objetivo, int rival, int idCompeticion, string rutaImagen,
                      string rutaImagen120, string rutaImagen80, string rutaImagen64, string rutaImagen32, string rutaEstadioInterior,
                      string rutaEstadioExterior, string rutaKitLocal, string rutaKitVisitante, int competicionEuropea)
        {
            IdEquipo = idEquipo;
            Nombre = nombre;
            NombreCorto = nombreCorto;
            Presidente = presidente;
            Presupuesto = presupuesto;
            Ciudad = ciudad;
            Estadio = estadio;
            Aforo = aforo;
            Reputacion = reputacion;
            Objetivo = objetivo;
            Rival = rival;
            IdCompeticion = idCompeticion;
            RutaImagen = rutaImagen;
            RutaImagen120 = rutaImagen120;
            RutaImagen80 = rutaImagen80;
            RutaImagen64 = rutaImagen64;
            RutaImagen32 = rutaImagen32;
            RutaEstadioInterior = rutaEstadioInterior;
            RutaEstadioExterior = rutaEstadioExterior;
            RutaKitLocal = rutaKitLocal;
            RutaKitVisitante = rutaKitVisitante;
            CompeticionEuropea = competicionEuropea;
        }

        // Método ToString para representar el objeto en formato de texto
        public override string ToString()
        {
            return $"Equipo: {Nombre} ({NombreCorto}) - Ciudad: {Ciudad}, Estadio: {Estadio} (Aforo: {Aforo}) - " +
                   $"Presidente: {Presidente}, Presupuesto: {Presupuesto}, Reputación: {Reputacion}, " +
                   $"Objetivo: {Objetivo}, Rival1: {Rival.ToString() ?? "N/A"}, Competición: {IdCompeticion}";
        }
    }
}
