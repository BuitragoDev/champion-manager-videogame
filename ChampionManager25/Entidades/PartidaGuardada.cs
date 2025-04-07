using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class PartidaGuardada
    {
        public string NombreArchivo { get; set; }
        public string RutaCompleta { get; set; }
        public DateTime FechaCreacion { get; set; }


        // Opcionales si los extraes desde la base de datos
        public string NombreManager { get; set; }
        public string NombreVisible { get; set; } // <-- Esto es lo que se muestra en el DataGrid
        public string Archivo => Path.GetFileName(RutaCompleta);
        public string Equipo { get; set; } // <-- Nombre del equipo que se mostrará
        public int IdEquipo { get; set; }

        // Método ToString que devuelve una cadena representativa
        public override string ToString()
        {
            return $"NombreArchivo: {NombreArchivo}, " +
                   $"RutaCompleta: {RutaCompleta}, " +
                   $"FechaCreacion: {FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")}, " +
                   $"NombreManager: {NombreManager}, " +
                   $"NombreVisible: {NombreVisible}, " +
                   $"Archivo: {Archivo}, " +
                   $"Equipo: {Equipo}, " +
                   $"IdEquipo: {IdEquipo}";
        }
    }
}
