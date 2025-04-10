using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace ChampionManager25.MisMetodos
{
    public class ImagePathConverterFoto : IValueConverter
    {
        public static List<Jugador> Jugadores { get; set; } // Lista estática para los equipos

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int idJugador) 
            {
                // Buscar el equipo con el IdJugador
                var jugador = Jugadores?.FirstOrDefault(e => e.IdJugador == idJugador);

                // Si encontramos el jugador, devolver la ruta de la imagen
                if (jugador != null)
                {
                    string rutaCompleta = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "ChampionsManager",
                        jugador.RutaImagen
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
