using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Pregunta
    {
        public string? Entrevistador { get; set; }
        public string? Texto { get; set; }
        public List<Respuesta>? Respuestas { get; set; }
    }
}
