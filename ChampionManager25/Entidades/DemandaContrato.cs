﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class DemandaContrato
    {
        public int SalarioDeseado { get; set; }
        public int ClausulaDeseada { get; set; }
        public int DuracionContrato { get; set; } // En años
        public int RolDeseado { get; set; } // 1. Clave, 2. Importante, 3. Rotación, 4. Ocasional
        public int BonoPartidos { get; set; }
        public int BonoGoles { get; set; }
    }
}
