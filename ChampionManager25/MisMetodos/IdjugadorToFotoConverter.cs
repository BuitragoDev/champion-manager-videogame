using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChampionManager25.MisMetodos
{
    public class IdjugadorToFotoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int idJugador) // Suponiendo que IdEquipo es un número entero
            {
                return new Uri($"/Recursos/img/jugadores/{idJugador}.png", UriKind.Relative);
            }
            return new Uri("/Recursos/img/jugadores/default.png", UriKind.Relative); // Imagen por defecto si no hay IdEquipo
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // No es necesario en este caso
        }
    }
}
