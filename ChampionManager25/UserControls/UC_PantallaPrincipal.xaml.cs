using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_PantallaPrincipal : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private static MediaPlayer mediaPlayer = new MediaPlayer(); // Inicialización al declarar
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();

        NacionalidadToFlagConverter convertidorBandera = new NacionalidadToFlagConverter();

        public UC_PantallaPrincipal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;

            // Código que inicializa el sonido de fondo de la pantalla principal
            try
            {
                Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pantallaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar datos de la cabecera
            imgLogoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/80x80/" + _equipo + ".png"));

            txtEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            txtManager.Text = _manager.Nombre + " " + _manager.Apellido;
            MostrarEstrellas(_manager.Reputacion);

            DateTime hoy = Metodos.hoy;
            // Formatear la fecha en español
            CultureInfo culturaEspañol = new CultureInfo("es-ES");
            string dia = hoy.ToString("dd", culturaEspañol); // Día
            string mes = hoy.ToString("MMM", culturaEspañol).ToUpper(); // Mes abreviado en español y en mayúsculas
            string año = hoy.ToString("yyyy", culturaEspañol); // Año

            // Combinar el formato
            string fechaFormateada = $"{dia} {mes} {año}";

            // Mostrar la fecha en el TextBlock
            txtFechaActual.Text = fechaFormateada;

            // Obtener el día de la semana en español
            string diaSemana = hoy.ToString("dddd", culturaEspañol);

            // Capitalizar la primera letra (opcional, si el formato por defecto no es suficiente)
            diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1);

            // Mostrar el día de la semana en el TextBlock
            txtDiaSemana.Text = diaSemana;

            // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
            if (DockPanel_Central.Children.Count > 0)
            {
                DockPanel_Central.Children.Clear();
            }
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);
        }

        private void pantallaPrincipal_Unloaded(object sender, RoutedEventArgs e)
        {
            Metodos.DetenerMusica(); // Detener la música al descargar el control
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón AJUSTES
        private void imgAjustes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Reproducir el sonido de clic
            Metodos.ReproducirSonidoClick();

            // Confirmación antes de salir
            MessageBoxResult resultado = MessageBox.Show(
                "¿Estás seguro de que quieres salir del juego?",
                "Confirmar salida",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón HOME
        private void imgHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón EQUIPO
        private void imgClub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ENTRENADOR
        private void imgEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón CALENDARIO 
        private void imgCalendario_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ESTADIO
        private void imgPabellon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MÁNAGER
        private void imgManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MENSAJES 
        private void imgCorreo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }
        // ------------------------------------------------------------------------------------------------------------

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

            if (reputacion == 5)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion == 4)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion == 3)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion == 2)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion == 1)
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
                canvasEstrellas.Children.Add(estrella);
            }
        }

        // --------------------------------------------- MÉTODOS PARA LAS OPCIONES DE SUBMENÚ -----------------------------------------------
        // Método para cargar UC_Menu_Club_Informacion
        private void CargarClubInformacion()
        {
          
        }

        // Método para cargar UC_Menu_Club_Plantilla
        private void CargarClubPlantilla()
        {
           
        }

        // Método para cargar UC_Menu_Club_Empleados
        private void CargarClubEmpleados()
        {
            
        }

        // Método para cargar UC_Menu_Club_Palmares
        private void CargarClubPalmares()
        {
           
        }

        // Método para cargar UC_Menu_Club_Records
        private void CargarClubRecords()
        {
            
        }

        // Método para cargar UC_Menu_Manager_Palmares
        private void CargarManagerPalmares()
        {
     
        }

        // Método para cargar UC_Menu_Manager_Ficha
        private void CargarManagerFicha()
        {
           
        }

        // Método para cargar UC_Menu_Estadio_Informacion
        private void CargarEstadioInformacion()
        {
          
        }
        #endregion
    }
}
