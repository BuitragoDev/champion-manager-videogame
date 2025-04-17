using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ChampionManager25.MisMetodos
{
    public class PosicionColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == null || values[1] == null)
                return Brushes.Transparent;

            int posicion = System.Convert.ToInt32(values[0]);
            int miCompeticion = System.Convert.ToInt32(values[1]);
            int numeroEquipos = System.Convert.ToInt32(parameter); // Lo puedes pasar si lo necesitas

            if (miCompeticion == 1)
            {
                // Las 4 últimas rojas
                if (posicion >= numeroEquipos - 3 && posicion <= numeroEquipos)
                    return Brushes.DarkRed;
            }
            else if (miCompeticion == 2)
            {
                if (posicion <= 4)
                    return Brushes.DarkGreen;
                if (posicion >= numeroEquipos - 3 && posicion <= numeroEquipos)
                    return Brushes.DarkRed;
            }

            return Brushes.Transparent;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }

}
