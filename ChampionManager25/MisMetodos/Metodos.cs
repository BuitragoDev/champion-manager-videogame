using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;

namespace ChampionManager25.MisMetodos
{
    public class Metodos
    {
        // Instancia de FechaDatos
        private FechaDatos datos = new FechaDatos();

        #region "Variables GLOBALES"
        public static int temporadaActual;
        public static DateTime hoy;
        public static bool SonidoActivado { get; set; } = true;
        public static int avisosBancarrota = 0;

        #endregion

        #region "Variables"
        private static MediaPlayer? mediaPlayer; // Variable estática para mantener una única instancia del MediaPlayer
        #endregion

        public Metodos()
        {
            InicializarTemporadaActual();
        }

        #region "Métodos"
        // ---------------------------------------------------------------- MÉTODO QUE REPRODUCE LA MUSICA DE FONDO
        public static void ReproducirMusica(string nombreArchivo)
        {
            if (mediaPlayer == null)
            {
                mediaPlayer = new MediaPlayer(); // Inicializar solo una vez
            }

            // Construir la ruta completa al archivo
            string musicPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos", "audio", nombreArchivo);

            if (File.Exists(musicPath))
            {
                mediaPlayer.Open(new Uri(musicPath)); // Abrir el archivo de audio
                mediaPlayer.MediaEnded += (sender, e) =>
                {
                    mediaPlayer.Position = TimeSpan.Zero; // Reiniciar desde el principio
                    mediaPlayer.Play();
                };
                mediaPlayer.Play(); // Iniciar la reproducción
            }
            else
            {
                throw new FileNotFoundException($"No se encontró el archivo de audio: {musicPath}");
            }
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------- MÉTODO PARA DETENER LA REPRODUCCIÓN
        public static void DetenerMusica()
        {
            if (mediaPlayer != null)
            {
                mediaPlayer.Stop();
                mediaPlayer.Close();
                mediaPlayer = null;
            }
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------- MÉTODO QUE REPRODUCE EL SONIDO AL HACER CLICK
        public static void ReproducirSonidoClick()
        {
            try
            {
                // Ruta al archivo de sonido dentro de los recursos
                string soundFilePath = "Recursos/audio/click.wav";

                // Cargar el archivo de sonido y reproducirlo
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play();
                }
            }
            catch (System.Exception ex)
            {
                // Manejo de excepciones en caso de que no se pueda reproducir el sonido
                System.Diagnostics.Debug.WriteLine("Error al reproducir el sonido: " + ex.Message);
            }
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------- MÉTODO QUE REPRODUCE LOS SONIDOS DE LAS TRANSICIONES.
        public static void ReproducirSonidoTransicion()
        {
            try
            {
                // Ruta al archivo de sonido dentro de los recursos
                string soundFilePath = "Recursos/audio/transicion.wav";

                // Cargar el archivo de sonido y reproducirlo
                using (SoundPlayer player = new SoundPlayer(soundFilePath))
                {
                    player.Play();
                }
            }
            catch (System.Exception ex)
            {
                // Manejo de excepciones en caso de que no se pueda reproducir el sonido
                System.Diagnostics.Debug.WriteLine("Error al reproducir el sonido: " + ex.Message);
            }
        }
        // --------------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------------------- MÉTODO PARA INICIALIZAR TEMPORADA ACTUAL
        public void InicializarTemporadaActual()
        {
            Fecha fechaHoy = datos.ObtenerFechaHoy();
            if (fechaHoy != null)
            {
                temporadaActual = fechaHoy.Anio; // Asignar el valor del año
                hoy = DateTime.Parse(fechaHoy.Hoy);
            }
            else
            {
                temporadaActual = 2024;
                hoy = DateTime.Parse("2024-07-15");
            }
        }
        // --------------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------- METODO QUE DEVUELVE LA DEMARCACION EN FORMATO CORTO.
        public static string ConvertirDemarcacion(int rol)
        {
            switch (rol)
            {
                case 1:
                    return "POR";  // Portero
                case 2:
                    return "LD";   // Lateral Derecho
                case 3:
                    return "LI";   // Lateral Izquierdo
                case 4:
                    return "DFC";  // Defensa Central
                case 5:
                    return "MC";   // Mediocentro
                case 6:
                    return "MCD";  // Mediocentro Defensivo
                case 7:
                    return "MCO";  // Mediocentro Ofensivo
                case 8:
                    return "ED";   // Extremo Derecho
                case 9:
                    return "EI";   // Extremo Izquierdo
                case 10:
                    return "DC";   // Delantero Centro
                default:
                    return "";  // En caso de que no se reconozca el rol
            }
        }
        // --------------------------------------------------------------------------------------------------------------
        #endregion
    }
}
