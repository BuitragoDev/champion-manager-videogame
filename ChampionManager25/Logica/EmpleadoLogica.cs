using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class EmpleadoLogica
    {
        // Instanciar EquipoDatos dentro de la clase lógica
        private EmpleadoDatos _datos = new EmpleadoDatos();

        // Llamada al método que trae el listado de los empleados disponibles
        public List<Empleado> MostrarListaEmpleadosDisponibles(int puesto)
        {
            return _datos.MostrarListaEmpleadosDisponibles(puesto);
        }

        // Llamada al método que cambia el campo Contratado de 0 a 1.
        public void FicharEmpleado(int equipo, int idManager, int idEmpleado)
        {
            _datos.FicharEmpleado(equipo, idManager, idEmpleado);
        }

        // Llamada al método para Mostrar la lista de empleados contratados
        public List<Empleado> MostrarListaEmpleadosContratados(int equipo, int manager)
        {
            return _datos.MostrarListaEmpleadosContratados(equipo, manager);
        }

        // Llamada al método que despide a un empleado
        public void DespedirEmpleado(int puesto)
        {
            _datos.DespedirEmpleado(puesto);
        }

        // Llamada al método para Calcular Salario Total Empleados
        public int SalarioTotalEmpleados(int equipo, int manager)
        {
            return _datos.SalarioTotalEmpleados(equipo, manager);
        }

        // Llamada al método para Mostrar la Categoria de un empleado
        public Empleado MostrarCategoriaEmpleado(string nombre)
        {
            return _datos.MostrarCategoriaEmpleado(nombre);
        }

        // Llamada al método que devuelve TRUE si hay un empleado contratado
        public bool EmpleadoEncontrado(string tarea, int manager)
        {
            return _datos.EmpleadoEncontrado(tarea, manager);
        }

        // Llamada al método que devuelve un objeto empleado por su Puesto
        public Empleado? ObtenerEmpleadoPorPuesto(string tarea)
        {
            return _datos.ObtenerEmpleadoPorPuesto(tarea);
        }
    }
}
