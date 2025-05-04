using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Logica
{
    public class TelevisionLogica
    {
        private TelevisionDatos _datos = new TelevisionDatos();

        // Llamada al método para Mostrar la lista de Televisiones
        public List<Television> MostrarListaCadenasTV()
        {
            return _datos.MostrarListaCadenasTV();
        }

        // Llamada al método para crear un contrato con una Television
        public void AnadirUnaCadenaTV(int cadenatv, int cantidad, int duracion, int equipo, int manager)
        {
            _datos.AnadirUnaCadenaTV(cadenatv, cantidad, duracion, equipo, manager);
        }

        // Llamada al método para Mostrar las televisiones contratadas
        public Television TelevisionesContratadas(int manager, int equipo)
        {
            return _datos.TelevisionesContratadas(manager, equipo);
        }

        // Llamada al método que devuelve el nombre de una Cadena de TV
        public string NombreCadenaTV(int cadena)
        {
            return _datos.NombreCadenaTV(cadena);
        }

        // Llamada al método para restar 1 año a la Television contratada
        public void RestarAnioCadenaTV(int cadenatv)
        {
            _datos.RestarAnioCadenaTV(cadenatv);
        }

        // Llamada al método para elimina una Cadena de TV contratada
        public void CancelarCadenaTV(int cadenatv)
        {
            _datos.CancelarCadenaTV(cadenatv);
        }
    }
}
