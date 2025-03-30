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
            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Home menuPrincipal = new UC_Menu_Home();
            DockPanel_Submenu.Children.Add(menuPrincipal);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón EQUIPO
        private void imgClub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Club menuClub = new UC_Menu_Club();
            DockPanel_Submenu.Children.Add(menuClub);

            // Suscribirse a los eventos
            menuClub.MostrarInformacion += CargarClubInformacion;
            menuClub.MostrarPlantilla += CargarClubPlantilla;

            // Cambiar el color del texto
            menuClub.lblInformacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ENTRENADOR
        private void imgEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Entrenador menuEntrenador = new UC_Menu_Entrenador();
            DockPanel_Submenu.Children.Add(menuEntrenador);

            menuEntrenador.MostrarAlineacion += CargarEntrenadorAlineacion;
            menuEntrenador.MostrarEntrenamiento += CargarEntrenadorEntrenamiento;
            menuEntrenador.MostrarRival += CargarEntrenadorRival;

            // Cambiar el color del texto
            menuEntrenador.lblAlineacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón COMPETICION
        private void imgCompeticiones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Competicion menuCompeticion = new UC_Menu_Competicion();
            DockPanel_Submenu.Children.Add(menuCompeticion);

            // Suscribirse a los eventos
            menuCompeticion.MostrarClasificacion += CargarCompeticionClasificacion;
            menuCompeticion.MostrarResultados += CargarCompeticionResultados;
            menuCompeticion.MostrarEstadisticas += CargarCompeticionEstadisticas;
            menuCompeticion.MostrarPalmares += CargarCompeticionPalmares;

            // Cambiar el color del texto
            menuCompeticion.lblClasificacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion clasificacionPrincipal = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clasificacionPrincipal);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón CALENDARIO 
        private void imgCalendario_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Calendario menuCalendario = new UC_Menu_Calendario();
            DockPanel_Submenu.Children.Add(menuCalendario);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Calendario_Principal calendarioPrincipal = new UC_Menu_Calendario_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(calendarioPrincipal);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ESTADIO
        private void imgPabellon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Estadio menuEstadio = new UC_Menu_Estadio();
            DockPanel_Submenu.Children.Add(menuEstadio);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MÁNAGER
        private void imgManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Manager menuManager = new UC_Menu_Manager();
            DockPanel_Submenu.Children.Add(menuManager);

            // Suscribirse a los eventos MostrarPalmares y MostrarFicha
            menuManager.MostrarPalmares += CargarManagerPalmares;
            menuManager.MostrarFicha += CargarManagerFicha;

            // Cambiar el color del texto "Ficha" a naranja
            menuManager.lblFicha.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MENSAJES 
        private void imgCorreo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Correo menuCorreo = new UC_Menu_Correo();
            DockPanel_Submenu.Children.Add(menuCorreo);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Correo_Principal correoPrincipal = new UC_Menu_Correo_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(correoPrincipal);
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
            // Cargar UC_Menu_Club_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }

        // Método para cargar UC_Menu_Club_Plantilla
        private void CargarClubPlantilla()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Plantilla clubPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
            DockPanel_Central.Children.Add(clubPlantilla);
        }

        // Método para cargar UC_Menu_Entrenador_Alineacion
        private void CargarEntrenadorAlineacion()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }

        // Método para cargar UC_Menu_Entrenador_Entrenamiento
        private void CargarEntrenadorEntrenamiento()
        {
            // Cargar UC_Menu_Entrenador_Tactica
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Entrenamiento entrenadorEntrenamiento = new UC_Menu_Entrenador_Entrenamiento(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorEntrenamiento);
        }

        // Método para cargar UC_Menu_Entrenador_Rival
        private void CargarEntrenadorRival()
        {
            // Cargar UC_Menu_Entrenador_Rival
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Rival entrenadorRival = new UC_Menu_Entrenador_Rival(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorRival);
        }

        // Método para cargar UC_Menu_Competicion_Clasificacion
        private void CargarCompeticionClasificacion()
        {
            // Cargar UC_Menu_Competicion_Clasificacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion competicionClasificacion = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionClasificacion);
        }

        // Método para cargar UC_Menu_Competicion_Resultados
        private void CargarCompeticionResultados()
        {
            // Cargar UC_Menu_Competicion_Resultados
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Resultados competicionResultados = new UC_Menu_Competicion_Resultados(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionResultados);
        }

        // Método para cargar UC_Menu_Competicion_Estadisticas
        private void CargarCompeticionEstadisticas()
        {
            // Cargar UC_Menu_Competicion_Estadisticas
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Estadisticas competicionEstadisticas = new UC_Menu_Competicion_Estadisticas(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionEstadisticas);
        }

        // Método para cargar UC_Menu_Competicion_Palmares
        private void CargarCompeticionPalmares()
        {
            // Cargar UC_Menu_Competicion_Palmares
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Palmares competicionPalmares = new UC_Menu_Competicion_Palmares(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionPalmares);
        }

        // Método para cargar UC_Menu_Manager_Ficha
        private void CargarManagerFicha()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }

        // Método para cargar UC_Menu_Manager_Palmares
        private void CargarManagerPalmares()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Palmares managerPalmares = new UC_Menu_Manager_Palmares(_manager, _equipo);
            DockPanel_Central.Children.Add(managerPalmares);
        }

        // Método para cargar UC_Menu_Estadio_Informacion
        private void CargarEstadioInformacion()
        {
            // Cargar UC_Menu_Estadio_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }
        #endregion
    }
}
