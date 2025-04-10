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
        public string Ruta { get; set; }
        public int IdManager { get; set; }
        public string Nombre { get; set; }
        public string NombreManager { get; set; }
        public string Equipo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime UltimaModificacion { get; set; }

        // Método ToString que devuelve una cadena representativa
        public override string ToString()
        {
            return $"FechaCreacion: {FechaCreacion.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}
