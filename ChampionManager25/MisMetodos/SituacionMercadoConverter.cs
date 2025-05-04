using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChampionManager25.MisMetodos
{
    public class SituacionMercadoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int situacion)
            {
                return situacion == 1 ? "Transferible" : situacion == 2 ? "Cedible" : "Desconocido";
            }
            return "Desconocido";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string situacion)
            {
                return situacion == "Transferible" ? 1 :
                       situacion == "Cedible" ? 2 : 0;
            }
            return 0;
        }
    }
}
