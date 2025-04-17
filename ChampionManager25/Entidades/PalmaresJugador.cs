using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class PalmaresJugador
    {
        // Atributos
        public int IdPalmares { get; set; }
        public int IdJugador { get; set; }
        public int Titulos { get; set; }

        // Atributos extra
        public string NombreJugador { get; set; }

        // Constructor por defecto
        public PalmaresJugador()
        {
        }

        // Constructor con parámetros
        public PalmaresJugador(int idPalmares, int idJugador, int titulos)
        {
            IdPalmares = idPalmares;
            IdJugador = idJugador;
            Titulos = titulos;
        }

        // Constructor con parámetros sin ID
        public PalmaresJugador(int idJugador, int titulos)
        {
            IdJugador = idJugador;
            Titulos = titulos;
        }

        // Método ToString
        public override string ToString()
        {
            return $"IdPalmares: {IdPalmares}, IdJugador: {IdJugador}, Titulos: {Titulos}";
        }
    }
}
