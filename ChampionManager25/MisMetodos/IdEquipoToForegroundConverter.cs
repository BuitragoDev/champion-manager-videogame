using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Drawing;

namespace ChampionManager25.MisMetodos
{
    public class IdEquipoToForegroundConverter : IValueConverter
    {
        private readonly int _equipo;

        public IdEquipoToForegroundConverter(int equipo)
        {
            _equipo = equipo;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Verifica si el IdEquipo coincide con _equipo
            if (value is int idEquipo && idEquipo == _equipo)
            {
                return Brushes.DarkRed; // Color rojo para coincidencias
            }
            return Brushes.Black; // Color negro para otros casos
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdEquipoToFontWeightConverter : IValueConverter
    {
        private readonly int _equipo;

        public IdEquipoToFontWeightConverter(int equipo)
        {
            _equipo = equipo;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int idEquipo && idEquipo == _equipo)
            {
                return FontWeights.Bold; // Negrita si coincide
            }
            return FontWeights.Normal; // Normal en otros casos
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IdEquipoToFontFamilyConverter : IValueConverter
    {
        private readonly int _equipo;

        public IdEquipoToFontFamilyConverter(int equipo)
        {
            _equipo = equipo;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int idEquipo && idEquipo == _equipo)
            {
                return new FontFamily("Cascadia Code SemiBold"); // Fuente específica si coincide
            }
            return new FontFamily("Cascadia Code Light"); // Fuente predeterminada
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
