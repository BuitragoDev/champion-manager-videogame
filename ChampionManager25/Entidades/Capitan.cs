using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Capitan
    {
        // Atributos
        public int IdCapitan { get; set; }
        public int Capitan1 { get; set; }
        public int Capitan2 { get; set; }
        public int Capitan3 { get; set; }

        // Constructor por defecto
        public Capitan() { }

        // Constructor con parámetros
        public Capitan(int idCapitan, int capitan1, int capitan2, int capitan3)
        {
            IdCapitan = idCapitan;
            Capitan1 = capitan1;
            Capitan2 = capitan2;
            Capitan3 = capitan3;
        }

        // Constructor con parámetros sin ID
        public Capitan(int capitan1, int capitan2, int capitan3)
        {
            Capitan1 = capitan1;
            Capitan2 = capitan2;
            Capitan3 = capitan3;
        }

        // Método ToString
        public override string ToString()
        {
            return $"Capitan 1: {Capitan1}\n" +
                   $"Capitan 2: {Capitan2}\n" +
                   $"Capitan 3: {Capitan3}";
        }
    }
}
