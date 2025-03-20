using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChampionManager25.MisMetodos
{
    public class Metodos
    {
        #region "Variables"
        private static MediaPlayer? mediaPlayer; // Variable estática para mantener una única instancia del MediaPlayer
        #endregion

        public Metodos()
        {
            
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
        #endregion
    }
}
