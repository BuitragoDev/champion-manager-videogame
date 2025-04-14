using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChampionManager25.MisMetodos
{
    public class SancionToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int sancion && sancion > 0)
            {
                return new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/prohibido_icon.png"));
            }
            return null; // No muestra nada si el valor es 0
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
