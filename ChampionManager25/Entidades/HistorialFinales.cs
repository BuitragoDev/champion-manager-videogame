using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class HistorialFinales
    {
        public int IdHistorial { get; set; }
        public string Temporada { get; set; }
        public int IdEquipoCampeon { get; set; }
        public int IdEquipoFinalista { get; set; }

        // Atributos extra
        public string NombreEquipoCampeon { get; set; }
        public string NombreEquipoFinalista { get; set; }

        // Constructor vacío
        public HistorialFinales() { }

        // Constructor con parámetros
        public HistorialFinales(int idHistorial, string temporada, int idEquipoCampeon, int idEquipoFinalista)
        {
            IdHistorial = idHistorial;
            Temporada = temporada;
            IdEquipoCampeon = idEquipoCampeon;
            IdEquipoFinalista = idEquipoFinalista;
        }

        // Método ToString para representación en texto
        public override string ToString()
        {
            return $"ID: {IdHistorial}, Temporada: {Temporada}, Campeón: {IdEquipoCampeon}, Finalista: {IdEquipoFinalista}";
        }
    }
}
