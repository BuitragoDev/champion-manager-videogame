using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class RemodelacionLogica
    {
        RemodelacionDatos _datos = new RemodelacionDatos();

        // Llamada al método que comprueba si hay una remodelación en marcha
        public Remodelacion ComprobarRemodelacion(int equipo, int idManager)
        {
            return _datos.ComprobarRemodelacion(equipo, idManager);
        }

        // Llamada al método que crea una nueva remodelación.
        public void CrearNuevaRemodelacion(int equipo, int idManager, DateTime fecha, int tipo)
        {
            _datos.CrearNuevaRemodelacion(equipo, idManager, fecha, tipo);
        }

        // Llamada al método que elimina una remodelación.
        public void EliminarRemodelacion(int id)
        {
            _datos.EliminarRemodelacion(id);
        }
    }
}
