using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Fecha
    {
        // Propiedades que corresponden a los campos de la tabla SQL
        public int IdFecha { get; set; }
        public string? Hoy { get; set; }
        public int Anio { get; set; }

        // Constructor por defecto
        public Fecha()
        {
        }

        // Constructor con parámetros
        public Fecha(int idFecha, string hoy, int anio)
        {
            IdFecha = idFecha;
            Hoy = hoy;
            Anio = anio;
        }

        // Constructor para crear un objeto sin incluir el id_Fecha (útil para inserts)
        public Fecha(string hoy, int anio)
        {
            Hoy = hoy;
            Anio = anio;
        }

        // Sobrescribir el método ToString
        public override string ToString()
        {
            return $"IdFecha: {IdFecha}, Hoy: {Hoy}, Año: {Anio}";
        }
    }
}
