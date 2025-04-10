using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace ChampionManager25.MisMetodos
{
    public class ImagePathConverterPalmares : IValueConverter
    {
        public static List<Equipo> Equipos { get; set; } // Lista estática para los equipos

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int idEquipo) // Aseguramos que el valor sea el IdEquipo
            {
                // Buscar el equipo con el IdEquipo
                var equipo = Equipos?.FirstOrDefault(e => e.IdEquipo == idEquipo);

                // Si encontramos el equipo, devolver la ruta de la imagen
                if (equipo != null)
                {
                    string rutaCompleta = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "ChampionsManager",
                        equipo.RutaImagen32
                    );

                    // Verificar si la imagen existe y devolver la ruta
                    if (File.Exists(rutaCompleta))
                    {
                        return new BitmapImage(new Uri(rutaCompleta, UriKind.Absolute));
                    }
                }
            }

            // Si no encontramos la imagen o el equipo, devolver null
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
