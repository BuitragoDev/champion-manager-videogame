using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class PrestamoLogica
    {
        private PrestamoDatos _datos = new PrestamoDatos();

        // Llamada al método para Mostrar los Préstamos de un equipo
        public List<Prestamo> MostrarPrestamos(int manager, int equipo)
        {
            return _datos.MostrarPrestamos(manager, equipo);
        }

        // Llamada al método para crear un contrato con una Television
        public void AnadirPrestamo(Prestamo prestamo)
        {
            _datos.AnadirPrestamo(prestamo);
        }

        // Llamada al método para Mostrar el orden de los Préstamos
        public List<int> OrdenPrestamos(int manager, int equipo)
        {
            return _datos.OrdenPrestamos(manager, equipo);
        }

        // Llamada al método para eliminar un Préstamo
        public void EliminarPrestamo(int manager, int equipo, int orden)
        {
            _datos.EliminarPrestamo(manager, equipo, orden);
        }

        // Llamada al método para restar una semana a un Préstamo
        public void RestarSemana(int orden)
        {
            _datos.RestarSemana(orden);
        }
    }
}
