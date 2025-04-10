using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Datos
{
    public class Conexion
    {
        private static string _cadenaPartidaActual;
        private static SQLiteConnection _conexionActual;

        public static void EstablecerConexionPartida(string rutaPartida)
        {
            _cadenaPartidaActual = $"Data Source={rutaPartida};Version=3;";

            // Abre la conexión si no está abierta
            if (_conexionActual == null || _conexionActual.State != System.Data.ConnectionState.Open)
            {
                _conexionActual = new SQLiteConnection(_cadenaPartidaActual);
                _conexionActual.Open();
            }
        }

        public static string Cadena
        {
            get
            {
                if (string.IsNullOrEmpty(_cadenaPartidaActual))
                    throw new InvalidOperationException("No se ha establecido la conexión a una partida");

                return _cadenaPartidaActual;
            }
        }

        public static void CerrarTodasLasConexiones()
        {
            if (_conexionActual != null)
            {
                if (_conexionActual.State != System.Data.ConnectionState.Closed)
                {
                    _conexionActual.Close();
                }
                _conexionActual.Dispose();
                _conexionActual = null;
            }
        }
    }

}
