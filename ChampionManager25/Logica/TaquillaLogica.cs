using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class TaquillaLogica
    {
        TaquillaDatos _datos = new TaquillaDatos();

        // Llamada al método que crea un registro para la taquilla del equipo
        public void GenerarTaquilla(int equipo, int idManager)
        {
            _datos.GenerarTaquilla(equipo, idManager);
        }

        // Llamada al método que comprueba si se ha establecido precio de abonos
        public bool ComprobarAbono(int equipo, int idManager)
        {
            return _datos.ComprobarAbono(equipo, idManager);
        }

        // Llamada al método que crea el precio de las entradas
        public void EstablecerPrecioEntradas(int equipo, int idManager, int precioGeneralEntrada, int precioTribunaEntrada, int precioVipEntrada)
        {
            _datos.EstablecerPrecioEntradas(equipo, idManager, precioGeneralEntrada, precioTribunaEntrada, precioVipEntrada);
        }

        // Llamada al método que crea el precio de los abonos
        public void EstablecerPrecioAbonos(int equipo, int idManager, int precioGeneral, int precioTribuna, int precioVip)
        {
            _datos.EstablecerPrecioAbonos(equipo, idManager, precioGeneral, precioTribuna, precioVip);
        }

        // Llamada al método que devuelve los precios de la taquilla.
        public Taquilla RecuperarPreciosTaquilla(int equipo, int idManager)
        {
            return _datos.RecuperarPreciosTaquilla(equipo, idManager);
        }
    }
}
