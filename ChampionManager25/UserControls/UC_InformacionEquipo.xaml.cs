using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;

namespace ChampionManager25.UserControls
{
    public partial class UC_InformacionEquipo : UserControl
    {
        #region "Variables"
        Equipo equipo;
        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_InformacionEquipo(Equipo eqp)
        {
            InitializeComponent();
            this.equipo = eqp;
        }

        private void informacionEquipo_Loaded(object sender, RoutedEventArgs e)
        {
            // Imagen del Escudo
            string escudoPath = $"/Recursos/img/escudos_equipos/{equipo.IdEquipo}.png";
            BitmapImage bitmapEscudo = new BitmapImage(new Uri(escudoPath, UriKind.Relative));
            imgEscudo.Source = bitmapEscudo;

            // Imágenes de los Equipos Rivales
            string rival1Path = $"/Recursos/img/escudos_equipos/120x120/{equipo.Rival}.png";
            BitmapImage bitmapImage1 = new BitmapImage(new Uri(rival1Path, UriKind.Relative));
            imgRival.Source = bitmapImage1;

            // Detalles de la Estrella del equipo
            Jugador estrella = _logicaJugador.MejorJugador(equipo.IdEquipo);

            string caraPath = $"/Recursos/img/jugadores/{estrella.IdJugador}.png";
            BitmapImage bitmapImageJugador = new BitmapImage(new Uri(caraPath, UriKind.Relative));
            imgCaraJugador.Source = bitmapImageJugador;

            lblMejorJugador.Text = estrella.Nombre + " " + estrella.Apellido;

            // Calcular la media si el jugador no es null
            if (estrella != null)
            {
                // Calcular la media sumando las estadísticas y dividiéndolas entre 9
                double media = (estrella.Velocidad + estrella.Agresividad + estrella.Resistencia + estrella.Calidad +
                                estrella.EstadoForma + estrella.Moral) / 6.0;

                // Redondear la media a un número entero (sin decimales)
                int mediaRedondeada = (int) media;

                // Asignar el valor de la media redondeada al TextBlock
                lblAverage.Text = mediaRedondeada.ToString();
            }

            // Datos del Club (Estadio)
            lblEstadio.Text = equipo.Estadio;
            lblAforo.Text = equipo.Aforo.ToString("N0") + " asientos";
            string estadioPath = $"/Recursos/img/estadios/{equipo.IdEquipo}exterior.png";
            BitmapImage bitmapEstadio = new BitmapImage(new Uri(estadioPath, UriKind.Relative));
            imgEstadio.Source = bitmapEstadio;

            // Datos del Club (Club)
            lblNombreClub.Text = equipo.Nombre;
            lblPresidente.Text = equipo.Presidente;
            lblCiudad.Text = equipo.Ciudad;
            lblObjetivo.Text = equipo.Objetivo;
            // Cargar estrellas reputación
            int reputacion = equipo.Reputacion;
            MostrarEstrellas(reputacion);

            // Imágenes de las equipaciones
            string camiseta1Path = $"/Recursos/img/kits/{equipo.IdEquipo}local.png";
            string camiseta2Path = $"/Recursos/img/kits/{equipo.IdEquipo}visitante.png";
            BitmapImage bitmapCamiseta1 = new BitmapImage(new Uri(camiseta1Path, UriKind.Relative));
            BitmapImage bitmapCamiseta2 = new BitmapImage(new Uri(camiseta2Path, UriKind.Relative));
            imgCamisetaLocal.Source = bitmapCamiseta1;
            imgCamisetaVisitante.Source = bitmapCamiseta2;
        }

        #region "Métodos"
        private void MostrarEstrellas(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellas.Children.Clear();

            // Cargar las imágenes de recursos
            ImageSource estrellaON = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOn.png"));
            ImageSource estrellaOFF = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOff.png"));

            // Determinar el número de estrellas según la reputación
            int numeroEstrellas = 0;

            if (reputacion == 100)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion >= 90)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion >= 80)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion >= 60)
            {
                numeroEstrellas = 2;
            }
            else
            {
                numeroEstrellas = 1;
            }

            // Dibujar 5 estrellas (activas e inactivas)
            for (int i = 0; i < 5; i++)
            {
                // Crear la imagen de la estrella
                Image estrella = new Image
                {
                    Width = 32, // Ancho de cada estrella
                    Height = 32,
                    Source = i < numeroEstrellas ? estrellaON : estrellaOFF
                };

                // Colocar la estrella en el Canvas
                Canvas.SetLeft(estrella, i * 35); // Separación horizontal entre estrellas
                Canvas.SetTop(estrella, 0);

                // Agregar la estrella al Canvas
                canvasEstrellas.Children.Add(estrella);
            }
        }
        #endregion
    }
}
