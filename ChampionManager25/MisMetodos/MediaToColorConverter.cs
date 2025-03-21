using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ChampionManager25.MisMetodos
{
    public class MediaToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int mediaValue)
            {
                if (mediaValue > 90)
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
                if (mediaValue >= 80)
                    return Brushes.DarkGreen; // Verde oscuro
                if (mediaValue >= 65)
                    return Brushes.Orange; // Naranja
                return Brushes.Red; // Rojo
            }
            return Brushes.Transparent; // Por defecto
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value; // No es necesario implementar esta parte para el DataGrid
        }
    }
}
