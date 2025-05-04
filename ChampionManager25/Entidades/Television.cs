using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Television
    {
        // Atributos
        public int IdTelevision { get; set; }
        public string? Nombre { get; set; }
        public int Reputacion { get; set; }
        public int Cantidad { get; set; }
        public int DuracionContrato { get; set; }

        // Constructores
        public Television()
        {
        }

        public Television(int idTelevision, string nombre, int reputacion, int cantidad, int duracionContrato)
        {
            IdTelevision = idTelevision;
            Nombre = nombre;
            Reputacion = reputacion;
            Cantidad = cantidad;
            DuracionContrato = duracionContrato;
        }

        public Television(string nombre, int cantidad, int duracionContrato)
        {
            Nombre = nombre;
            Cantidad = cantidad;
            DuracionContrato = duracionContrato;
        }

        // ToString
        public override string ToString()
        {
            return $"Patrocinador: {Nombre}, Cantidad: {Cantidad}, Duración: {DuracionContrato} meses";
        }
    }
}
