using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChampionManager25.MisMetodos
{
    public class IdEquipoToEscudoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int idEquipo) // Suponiendo que IdEquipo es un número entero
            {
                return new Uri($"/Recursos/img/escudos_equipos/32x32/{idEquipo}.png", UriKind.Relative);
            }
            return new Uri("/Recursos/img/escudos_equipos/default.png", UriKind.Relative); // Imagen por defecto si no hay IdEquipo
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // No es necesario en este caso
        }
    }
}
