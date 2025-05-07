using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChampionManager25.MisMetodos
{
    public class IdEquipoToEscudoConverter : IValueConverter
    {
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int idEquipo) // Suponiendo que IdEquipo es un número entero
            {
                Equipo oEquipo = _logicaEquipo.ListarDetallesEquipo(idEquipo);
                string imagePath = $"{GestorPartidas.RutaMisDocumentos}/{oEquipo.RutaImagen120}";
                return new Uri(imagePath, UriKind.Absolute);
            }
            return new Uri("/Recursos/img/escudos_equipos/default.png", UriKind.Relative); // Imagen por defecto si no hay IdEquipo
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // No es necesario en este caso
        }
    }
}
