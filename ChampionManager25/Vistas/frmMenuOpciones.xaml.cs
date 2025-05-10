using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmMenuOpciones : Window
    {
        public frmMenuOpciones()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizarEstadoSonido();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
       
            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Application.Current.Shutdown();
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            this.Close();
        }

        private void btnSonido_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            var boton = sender as Button;

            // Asegurarse de que 'boton' no sea nulo
            if (boton == null)
                return;

            // Acceder a la imagen dentro del template
            var template = boton.Template;
            var imagen = template.FindName("imgBoton", boton) as Image;

            if (Metodos.SonidoActivado)
            {
                Metodos.DetenerMusica();
                boton.Content = "ACTIVAR SONIDO";
                if (imagen != null)
                    imagen.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/sonidoOn_icon.png"));
            }
            else
            {
                try
                {
                    Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
                    boton.Content = "QUITAR EL SONIDO";
                    if (imagen != null)
                        imagen.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/sonidoOff_icon.png"));
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            Metodos.SonidoActivado = !Metodos.SonidoActivado;
        }

        private void ActualizarEstadoSonido()
        {
            var boton = btnSonido;  // Accedemos directamente al botón btnSonido

            if (boton != null)
            {
                // Acceder a la imagen dentro del template
                var template = boton.Template;
                var imagen = template.FindName("imgBoton", boton) as Image;

                if (Metodos.SonidoActivado)
                {
                    boton.Content = "QUITAR EL SONIDO";
                    if (imagen != null)
                        imagen.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/sonidoOff_icon.png"));
                }
                else
                {
                    boton.Content = "ACTIVAR SONIDO";
                    if (imagen != null)
                        imagen.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/sonidoOn_icon.png"));
                }
            }
        }
    }
}
