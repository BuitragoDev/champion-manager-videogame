using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class HistorialLogica
    {
        // Instanciar HistorialDatos dentro de la clase lógica
        private HistorialDatos _datos = new HistorialDatos();

        // Llamada al método para Mostrar el Historial del Mánager
        public List<Historial> MostrarHistorialManager(int manager)
        {
            return _datos.MostrarHistorialManager(manager);
        }

        // Llamada al método para insertar un nuevo Historial de Mánager vacío
        public void CrearLineaHistorial(int manager, int equipo, string temporada)
        {
            _datos.CrearLineaHistorial(manager, equipo, temporada);
        }

        // Llamada al método para copiar datos del historial temporal
        public void CopiarPartidosHistorialManager(int temporada)
        {
            _datos.CopiarPartidosHistorialManager(temporada);
        }

        // Llamada al método para copiar los datos de las confianzas
        public void CopiarConfianzasManager(int temporada)
        {
            _datos.CopiarConfianzasManager(temporada);
        }

        // Llamada al método para actualizar la posicion final en la Liga
        public void CopiarPosicionLigaManager(int temporada, int posicion)
        {
            _datos.CopiarPosicionLigaManager(temporada, posicion);
        }

        // Llamada al método para resetear el historial temporal
        public void ResetearHistorialTemporal()
        {
            _datos.ResetearHistorialTemporal();
        }
    }
}
