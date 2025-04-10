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
        public string RutaImagen { get; set; }
        public string RutaImagen80 { get; set; }

        // Constructor por defecto
        public Competicion() { }

        // Constructor con parámetros
        public Competicion(int idCompeticion, string nombre, string rutaImagen, string rutaImagen80)
        {
            IdCompeticion = idCompeticion;
            Nombre = nombre;
            RutaImagen = rutaImagen;
            RutaImagen80 = rutaImagen80;
        }

        // Constructor con parámetros sin ID_COMPETICION
        public Competicion(string nombre, string rutaImagen, string rutaImagen80)
        {
            Nombre = nombre;
            RutaImagen = rutaImagen;
            RutaImagen80 = rutaImagen80;
        }

        // Método ToString
        public override string ToString()
        {
            return $"ID: {IdCompeticion}, Nombre: {Nombre}, RutaImagen: {RutaImagen}";
        }
    }
}
