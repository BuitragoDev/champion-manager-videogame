using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class ClasificacionLogica
    {
        // Instanciar ManagerDatos dentro de la clase lógica
        private ClasificacionDatos _datos = new ClasificacionDatos();

        // Llamada al método para Rellenar la Clasificación
        public void RellenarClasificacion(int competicion, int manager)
        {
            _datos.RellenarClasificacion(competicion, manager);
        }

        // Llamada al método que muestra una lista de equipo para la clasificación
        public List<Clasificacion> MostrarClasificacion(int competicion, int manager)
        {
            return _datos.MostrarClasificacion(competicion, manager);
        }

        // Llamada al método que crea un objeto Clasificacion de un equipo
        public Clasificacion MostrarClasificacionPorEquipo(int equipo, int manager)
        {
            return _datos.MostrarClasificacionPorEquipo(equipo, manager);
        }

        // Llamada al método para Mostrar el equipo con MAS GOLES A FAVOR
        public Clasificacion MostrarMejorAtaque(int manager)
        {
            return _datos.MostrarMejorAtaque(manager);
        }

        // Llamada al método para Mostrar el equipo con MENOS GOLES EN CONTRA
        public Clasificacion MostrarMejorDefensa(int manager)
        {
            return _datos.MostrarMejorDefensa(manager);
        }

        // Llamada al método para Mostrar el equipo con MEJOR RACHA
        public Clasificacion MostrarMejorRacha(int manager)
        {
            return _datos.MostrarMejorRacha(manager);
        }

        // Llamada al método para Mostrar el equipo con MAS VICTORIAS COMO LOCAL
        public Clasificacion MostrarMejorEquipoLocal(int manager)
        {
            return _datos.MostrarMejorEquipoLocal(manager);
        }

        // Llamada al método para Mostrar el equipo con MAS VICTORIAS COMO VISITANTE
        public Clasificacion MostrarMejorEquipoVisitante(int manager)
        {
            return _datos.MostrarMejorEquipoVisitante(manager);
        }

        // Llamada al método para actualizar la Clasificacion
        public void ActualizarClasificacion(Clasificacion clasificacion)
        {
            _datos.ActualizarClasificacion(clasificacion);
        }
    }
}
