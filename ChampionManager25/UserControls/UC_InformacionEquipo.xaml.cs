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
using ChampionManager25.Datos;
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
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_InformacionEquipo(Equipo eqp)
        {
            InitializeComponent();
            this.equipo = eqp;
        }

        private void informacionEquipo_Loaded(object sender, RoutedEventArgs e)
        {
            // Imagen del Escudo
            imgEscudo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen));

            // Imágenes de los Equipos Rivales
            string rutaEscudoRival = _logicaEquipo.ListarDetallesEquipo(equipo.Rival).RutaImagen120; // Obtener ruta escudo equipo rival
            imgRival.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEscudoRival));

            // Detalles de la Estrella del equipo
            Jugador estrella = _logicaJugador.MejorJugador(equipo.IdEquipo);
            imgCaraJugador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + estrella.RutaImagen));
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
            imgEstadio.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaEstadioExterior));

            // Datos del Club (Club)
            lblNombreClub.Text = equipo.Nombre;
            lblPresidente.Text = equipo.Presidente;
            lblCiudad.Text = equipo.Ciudad;
            lblObjetivo.Text = equipo.Objetivo;
            // Cargar estrellas reputación
            int reputacion = equipo.Reputacion;
            MostrarEstrellas(reputacion);

            // Imágenes de las equipaciones
            imgCamisetaLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaKitLocal));
            imgCamisetaVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaKitVisitante));
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
