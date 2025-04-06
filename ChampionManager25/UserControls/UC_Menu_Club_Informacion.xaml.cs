using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
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

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Club_Informacion : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();

        public UC_Menu_Club_Informacion(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Imagen del Escudo
            string escudoPath = $"/Recursos/img/escudos_equipos/{_equipo}.png";
            BitmapImage bitmapEscudo = new BitmapImage(new Uri(escudoPath, UriKind.Relative));
            imgEscudo.Source = bitmapEscudo;

            // Nombre del equipo y reputación
            txtNombreEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            MostrarEstrellas(_logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion);

            // Media del Equipo
            double mediaEquipo = _logicaJugador.ObtenerMediaEquipo(_equipo);
            elipseMediaEquipo.Stroke = DeterminarColorElipse((int)Math.Round(mediaEquipo));

            lblAverage.Text = Math.Round(mediaEquipo).ToString();

            if (mediaEquipo > 90)
            {
                lblAverage.Foreground = new SolidColorBrush(Colors.LightGreen);
            }
            else if (Math.Round(mediaEquipo) >= 80)
            {
                lblAverage.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else if (Math.Round(mediaEquipo) >= 65)
            {
                lblAverage.Foreground = new SolidColorBrush(Colors.Orange);
            }
            else
            {
                lblAverage.Foreground = new SolidColorBrush(Colors.Red);
            }

            // Liga Nacional
            int miLiga = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
            string nacionalPath = $"/Recursos/img/logos_competiciones/80x80/{miLiga}.png";
            BitmapImage bitmapNacional = new BitmapImage(new Uri(nacionalPath, UriKind.Relative));
            imgLigaNacional.Source = bitmapNacional;

            // Liga Internacional

            // Datos Entrenador
            txtNombreEntrenador.Text = _logicaManager.MostrarManager(_manager.IdManager).Nombre.ToString() + " " +
                                       _logicaManager.MostrarManager(_manager.IdManager).Apellido.ToString();
            MostrarEstrellasEntrenador(_logicaManager.MostrarManager(_manager.IdManager).Reputacion);

            // Datos Estadio
            txtNombreEstadio.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Estadio;
            txtCapacidadEstadio.Text = "( " + _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo.ToString("N0") + " asientos )";

            // Equipaciones
            string kitLocalPath = $"/Recursos/img/kits/{_equipo}local.png";
            BitmapImage bitmapKitLocal = new BitmapImage(new Uri(kitLocalPath, UriKind.Relative));
            imgKitLocal.Source = bitmapKitLocal;

            string kitVisitantePath = $"/Recursos/img/kits/{_equipo}visitante.png";
            BitmapImage bitmapKitVisitante = new BitmapImage(new Uri(kitVisitantePath, UriKind.Relative));
            imgKitVisitante.Source = bitmapKitVisitante;

            // Rivalidades
            int? rival = _logicaEquipo.ListarDetallesEquipo(_equipo).Rival;
            if (rival.HasValue)
            {
                string rival1Path = $"/Recursos/img/escudos_equipos/120x120/{rival}.png";
                BitmapImage bitmapRival1 = new BitmapImage(new Uri(rival1Path, UriKind.Relative));
                imgRival1.Source = bitmapRival1;
            }

            // Mejores Jugadores
            // Goles
            imgFotoMaximoGoleador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" +
                                                               _logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador + ".png"));
            txtGolesNombre.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Apellido;
            txtGolesDemarcacion.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Rol;
            int goles = _logicaEstadistica.MostrarJugadorConMasGoles(_equipo).Goles;
            txtGolesValor.Text = goles.ToString();

            // Asistencias
            imgFotoMaximoAsistente.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" +
                                                               _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador + ".png"));
            txtAsistenciasNombre.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Apellido;
            txtAsistenciasDemarcacion.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Rol;
            int asistencias = _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).Asistencias;
            txtAsistenciasValor.Text = asistencias.ToString();

            // MVP
            imgFotoMaximoMVP.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" +
                                                               _logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador + ".png"));
            txtMVPNombre.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador).Apellido;
            txtMVPDemarcacion.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasMvp(_equipo).IdJugador).Rol;
            int mvp = _logicaEstadistica.MostrarJugadorConMasMvp(_equipo).MVP;
            txtMVPValor.Text = mvp.ToString();
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

        private void MostrarEstrellasEntrenador(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellasEntrenador.Children.Clear();

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
            else if (reputacion >= 70)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion >= 50)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion >= 25)
            {
                numeroEstrellas = 1;
            }
            else
            {
                numeroEstrellas = 0;
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
                canvasEstrellasEntrenador.Children.Add(estrella);
            }
        }

        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos > 90)
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
            if (puntos >= 80)
                return Brushes.DarkGreen; // Verde oscuro
            if (puntos >= 65)
                return Brushes.Orange; // Naranja
            return new SolidColorBrush(Colors.Red);
        }
        #endregion
    }
}
