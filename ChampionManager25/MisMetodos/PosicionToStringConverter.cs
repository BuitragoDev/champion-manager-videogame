using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChampionManager25.MisMetodos
{
    public class PosicionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int posicion)
            {
                return posicion switch
                {
                    1 => "POR",
                    2 => "LD",
                    3 => "LI",
                    4 => "DFC",
                    5 => "MC",
                    6 => "MCD",
                    7 => "MCO",
                    8 => "ED",
                    9 => "EI",
                    10 => "DC",
                    _ => string.Empty // Si el valor no es válido, se devuelve una cadena vacía
                };
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value; // En este caso, no necesitamos convertir de nuevo, así que devolvemos el valor tal cual
        }
    }
}
