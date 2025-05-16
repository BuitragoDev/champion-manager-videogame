using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;

namespace ChampionManager25.Logica
{
    public class ManagerLogica
    {
        // Instanciar ManagerDatos dentro de la clase lógica
        private ManagerDatos _datos = new ManagerDatos();

        // Llamada al método que crea un nuevo Manager
        public int CrearNuevoManager(Manager manager)
        {
            int idManager = _datos.CrearNuevoManager(manager);
            if (idManager <= 0)
            {
                MessageBox.Show("Error al obtener el ID del Manager.");
            }
            return idManager;
        }

        // Llamada al método que elimina un mánager
        public void eliminarManager(int idManager)
        {
            _datos.eliminarManager(idManager);
        }

        // Llamada al método que añade un equipo a un Mánager ya creado
        public void AnadirEquipoSeleccionado(int idManager, int idEquipo)
        {
            _datos.AnadirEquipoSeleccionado(idManager, idEquipo);
        }

        // Llamada al método que actualiza los puntos de Directiva, Fans y Jugadores.
        public void ActualizarConfianza(int idManager, int directiva, int fans, int jugadores)
        {
            _datos.ActualizarConfianza(idManager, directiva, fans, jugadores);
        }

        // Llamada al método que actualiza la reputacion del Manager
        public void ActualizarReputacion(int idManager, int valor)
        {
            _datos.ActualizarReputacion(idManager, valor);
        }

        // Llamada al método que muestra un Mánager.
        public Manager MostrarManager(int idManager)
        {
            return _datos.MostrarManager(idManager);
        }

        // Llamada al método que cambia la tactica del manager
        public void CambiarTactica(int idManager, string tactica)
        {
            _datos.CambiarTactica(idManager, tactica);
        }

        // Llamada al método que actualiza los partidos de un manager
        public void ActualizarResultadoManager(int idManager, int jugados, int ganados, int empatados, int perdidos, int puntos)
        {
            _datos.ActualizarResultadoManager(idManager, jugados, ganados, empatados, perdidos, puntos);
        }

        // Llamada al método que actualiza la tabla historial_manager_temp
        public void ActualizarManagerTemporal(Historial historial)
        {
            _datos.ActualizarManagerTemporal(historial);
        }

        // Llamada al método que despide un manager
        public void DespedirManager(int idManager)
        {
            _datos.DespedirManager(idManager);
        }

        // Método que cambia el estado de la Primera Temporada.
        public void ModificarPrimeraTemporada(int valor)
        {
            _datos.ModificarPrimeraTemporada(valor);
        }
    }
}
