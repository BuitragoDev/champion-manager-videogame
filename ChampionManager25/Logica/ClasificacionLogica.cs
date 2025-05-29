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

        // Llamada al método para Rellenar la Clasificación de Primera Division
        public void RellenarClasificacion(int competicion, int manager)
        {
            _datos.RellenarClasificacion(competicion, manager);
        }

        // Llamada al método para Rellenar la Clasificación de Segunda Division
        public void RellenarClasificacion2(int competicion, int manager)
        {
            _datos.RellenarClasificacion2(competicion, manager);
        }

        // Llamada al método para Rellenar la Clasificación de Copa de Europa 1
        public void RellenarClasificacionEuropa1(int manager, List<Equipo> equiposEuropa1)
        {
            _datos.RellenarClasificacionEuropa1(manager, equiposEuropa1);
        }

        // Llamada al método para Rellenar la Clasificación de Copa de Europa 2
        public void RellenarClasificacionEuropa2(int manager, List<Equipo> equiposEuropa2)
        {
            _datos.RellenarClasificacionEuropa2(manager, equiposEuropa2);
        }

        // Llamada al método que muestra una lista de equipo para la clasificación de Liga 1
        public List<Clasificacion> MostrarClasificacion(int competicion, int manager)
        {
            return _datos.MostrarClasificacion(competicion, manager);
        }

        // Llamada al método que muestra una lista de equipo para la clasificación de Liga 2
        public List<Clasificacion> MostrarClasificacion2(int competicion, int manager)
        {
            return _datos.MostrarClasificacion2(competicion, manager);
        }

        // Llamada al método para Mostrar la Clasificacion Europa 1
        public List<Clasificacion> MostrarClasificacionCopaEuropa1(int competicion, int manager)
        {
            return _datos.MostrarClasificacionCopaEuropa1(competicion, manager);
        }

        // Llamada al método para Mostrar la Clasificacion Europa 2
        public List<Clasificacion> MostrarClasificacionCopaEuropa2(int competicion, int manager)
        {
            return _datos.MostrarClasificacionCopaEuropa2(competicion, manager);
        }

        // Llamada al método que crea un objeto Clasificacion de un equipo
        public Clasificacion MostrarClasificacionPorEquipo(int equipo, int manager, int competicion)
        {
            return _datos.MostrarClasificacionPorEquipo(equipo, manager, competicion);
        }

        // Llamada al método para Mostrar el equipo con MAS GOLES A FAVOR
        public Clasificacion MostrarMejorAtaque(int manager, int competicion)
        {
            return _datos.MostrarMejorAtaque(manager, competicion);
        }

        // Llamada al método para Mostrar el equipo con MENOS GOLES EN CONTRA
        public Clasificacion MostrarMejorDefensa(int manager, int competicion)
        {
            return _datos.MostrarMejorDefensa(manager, competicion);
        }

        // Llamada al método para Mostrar el equipo con MEJOR RACHA
        public Clasificacion MostrarMejorRacha(int manager, int competicion)
        {
            return _datos.MostrarMejorRacha(manager, competicion);
        }

        // Llamada al método para Mostrar el equipo con MAS VICTORIAS COMO LOCAL
        public Clasificacion MostrarMejorEquipoLocal(int manager, int competicion)
        {
            return _datos.MostrarMejorEquipoLocal(manager, competicion);
        }

        // Llamada al método para Mostrar el equipo con MAS VICTORIAS COMO VISITANTE
        public Clasificacion MostrarMejorEquipoVisitante(int manager, int competicion)
        {
            return _datos.MostrarMejorEquipoVisitante(manager, competicion);
        }

        // Llamada al método para actualizar la Clasificacion
        public void ActualizarClasificacion(Clasificacion clasificacion)
        {
            _datos.ActualizarClasificacion(clasificacion);
        }

        // Llamada al método para actualizar la Clasificacion de la 2 division
        public void ActualizarClasificacion2(Clasificacion clasificacion)
        {
            _datos.ActualizarClasificacion2(clasificacion);
        }

        // Llamada al metodo para actualizar la clasificacion de la Copa de Europa 1
        public void ActualizarClasificacionEuropa1(Clasificacion clasificacion)
        {
            _datos.ActualizarClasificacionEuropa1(clasificacion);
        }

        // Llamada al metodo para actualizar la clasificacion de la Copa de Europa 2
        public void ActualizarClasificacionEuropa2(Clasificacion clasificacion)
        {
            _datos.ActualizarClasificacionEuropa2(clasificacion);
        }

        // Llamada al método para RESETEAR la Clasificación
        public void ResetearClasificacion(int competicion)
        {
            _datos.ResetearClasificacion(competicion);
        }
    }
}
