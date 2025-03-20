using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Competicion
    {
        // Atributos
        public int IdCompeticion { get; set; }
        public string Nombre { get; set; }

        // Constructor por defecto
        public Competicion() { }

        // Constructor con parámetros
        public Competicion(int idCompeticion, string nombre)
        {
            IdCompeticion = idCompeticion;
            Nombre = nombre;
        }

        // Constructor con parámetros sin ID_COMPETICION
        public Competicion(string nombre)
        {
            Nombre = nombre;
        }

        // Método ToString
        public override string ToString()
        {
            return $"ID: {IdCompeticion}, Nombre: {Nombre}";
        }
    }
}
