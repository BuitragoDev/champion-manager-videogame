using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class MensajeLogica
    {
        MensajeDatos _datos = new MensajeDatos();

        // Llamada al método que crea un partido
        public List<Mensaje> MostrarMisMensajes(int idManager)
        {
            return _datos.MostrarMisMensajes(idManager);
        }

        // Llamada al método que marca un mensaje como leído
        public void MarcarComoLeido(int idMensaje)
        {
            _datos.MarcarComoLeido(idMensaje);
        }

        // Llamada al método que elimina un mensaje
        public void eliminarMensaje(int idMensaje)
        {
            _datos.eliminarMensaje(idMensaje);
        }

        // Llamada al método que crea un mensaje
        public void crearMensaje(Mensaje msg)
        {
            _datos.crearMensaje(msg);
        }

        // Llamada al método que devuelve el número de mensajes no leídos.
        public int MensajesNoLeidos(int idManager)
        {
            return _datos.MensajesNoLeidos(idManager);
        }
    }
}
