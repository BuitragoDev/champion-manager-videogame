using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ChampionManager25.MisMetodos
{
    class DuracionToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int duracion)
            {
                if (duracion == 1)
                    return Brushes.DarkRed;
                return Brushes.Black;
            }
            return Brushes.Black; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // No es necesario implementar esta parte para el DataGrid
        }
    }
}
