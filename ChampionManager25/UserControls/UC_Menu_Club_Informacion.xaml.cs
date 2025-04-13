using ChampionManager25.Datos;
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
        Jugador jugador;
        Estadistica estadistica;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

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
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            imgEscudo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));

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
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(miEquipo.IdCompeticion).RutaImagen;
            imgLigaNacional.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));

            // Datos Entrenador
            txtNombreEntrenador.Text = _logicaManager.MostrarManager(_manager.IdManager).Nombre.ToString() + " " +
                                       _logicaManager.MostrarManager(_manager.IdManager).Apellido.ToString();
            MostrarEstrellasEntrenador(_logicaManager.MostrarManager(_manager.IdManager).Reputacion);

            // Datos Estadio
            txtNombreEstadio.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Estadio;
            txtCapacidadEstadio.Text = "( " + _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo.ToString("N0") + " asientos )";

            // Equipaciones
            imgKitLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaKitLocal));
            imgKitVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaKitVisitante));

            // Rivalidades
            int? rival = _logicaEquipo.ListarDetallesEquipo(_equipo).Rival;
            if (rival.HasValue)
            {
                Equipo equipoRival = _logicaEquipo.ListarDetallesEquipo(_equipo);
                imgRival1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoRival.RutaImagen120));
            }

            // Mejores Jugadores
            // Goles
            estadistica = _logicaEstadistica.MostrarJugadorConMasGoles(miEquipo.IdEquipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoMaximoGoleador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtGolesNombre.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Apellido;
            txtGolesDemarcacion.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasGoles(_equipo).IdJugador).Rol;
            int goles = _logicaEstadistica.MostrarJugadorConMasGoles(_equipo).Goles;
            txtGolesValor.Text = goles.ToString();

            // Asistencias
            estadistica = _logicaEstadistica.MostrarJugadorConMasAsistencias(miEquipo.IdEquipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoMaximoAsistente.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtAsistenciasNombre.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Nombre + " " +
                                              _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Apellido;
            txtAsistenciasDemarcacion.Text = _logicaJugador.MostrarDatosJugador(_logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).IdJugador).Rol;
            int asistencias = _logicaEstadistica.MostrarJugadorConMasAsistencias(_equipo).Asistencias;
            txtAsistenciasValor.Text = asistencias.ToString();

            // MVP
            estadistica = _logicaEstadistica.MostrarJugadorConMasMvp(miEquipo.IdEquipo);
            jugador = _logicaJugador.MostrarDatosJugador(estadistica.IdJugador);
            imgFotoMaximoMVP.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
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
