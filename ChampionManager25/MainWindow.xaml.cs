using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using ChampionManager25.UserControls;
using ChampionManager25.MisMetodos;
using System.Windows.Threading;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;

namespace ChampionManager25;

public partial class MainWindow : Window
{
    // Propiedades para manejar el estado de la partida
    public string RutaPartidaActual { get; set; }
    public bool PartidaEnProgreso { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        MainContent.Content = new UC_PantallaCarga();

        // --------------- CÓDIGO QUE INICIALIZA EL SONIDO DE FONDO AL ARRANCAR LA APLICACIÓN
        try
        {
            Metodos.ReproducirMusica("backgroundMusic.wav");
        }
        catch (FileNotFoundException ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        // ----------------------------------------------------------------------------------

        // ------------------------------------------------------ CREACION DE UN TEMPORIZADOR
        DispatcherTimer timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(5) // Tiempo de espera: 5 segundos
        };

        // Asignar el evento Tick
        timer.Tick += (s, e) =>
        {
            // Detener el temporizador
            timer.Stop();

            // Cambiar al UserControl UC_Portada
            MainContent.Content = new UC_Portada();
        };

        // Iniciar el temporizador
        timer.Start();
        // ----------------------------------------------------------------------------------
    }

    // ------------------------------------------------ LIBERAR RECURSOS AL CERRAR LA VENTANA
    protected override void OnClosed(EventArgs e)
    {
        Metodos.DetenerMusica(); // Detener la música al cerrar la ventana
        base.OnClosed(e);
    }
    // --------------------------------------------------------------------------------------

    // ----------------------------------------- METODO PARA CARGAR EL USERCONTROL UC_Portada
    public void CargarPortada()
    {
        // Ruta donde se guardan las partidas
        string rutaPartidas = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ChampionsManager",
            "Partidas");

        // Eliminar todas las partidas temporales que empiecen por "Partida_Temporal"
        try
        {
            if (Directory.Exists(rutaPartidas))
            {
                var archivosTemporales = Directory.GetFiles(rutaPartidas, "Partida_Temporal*.db");

                foreach (var archivo in archivosTemporales)
                {
                    try
                    {
                        Conexion.CerrarTodasLasConexiones();
                        File.Delete(archivo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"No se pudo eliminar {archivo}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al eliminar partidas temporales: {ex.Message}");
        }

        // Reset de estado por si se venía de una partida en progreso
        PartidaEnProgreso = false;
        RutaPartidaActual = null;

        // Cargar pantalla de portada
        MainContent.Content = new UC_Portada();
    }

    // --------------------------------------------------------------------------------------

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_CrearManager
    public void CargarCrearManager()
    {
        if (!PartidaEnProgreso)
        {
            // Crear nueva partida temporal
            RutaPartidaActual = GestorPartidas.CrearNuevaPartida();
            PartidaEnProgreso = true;

            // Establecer conexión
            Conexion.EstablecerConexionPartida(RutaPartidaActual);
        }

        // Cargar el UserControl (sin crear nueva partida si ya existe)
        MainContent.Content = new UC_CrearManager();
    }
    // --------------------------------------------------------------------------------------\

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_CargarPartida
    public void CargarPartida()
    {
        PartidaEnProgreso = false;
        MainContent.Content = new UC_CargarPartida();
    }
    // --------------------------------------------------------------------------------------

    // --------------------------------- METODO PARA CARGAR EL USERCONTROL UC_SeleccionEquipo
    public void CargarSeleccionEquipo(Manager manager)
    {
        if (!PartidaEnProgreso || string.IsNullOrEmpty(RutaPartidaActual))
        {
            MessageBox.Show("Error: No hay partida en progreso");
            CargarPortada();
            return;
        }

        Conexion.EstablecerConexionPartida(RutaPartidaActual);
        MainContent.Content = new UC_SeleccionEquipo(manager, RutaPartidaActual);
    }
    // --------------------------------------------------------------------------------------

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_Pretemporada
    public void CargarPretemporada(Manager manager, int equipo)
    {
        if (!PartidaEnProgreso || string.IsNullOrEmpty(RutaPartidaActual))
        {
            MessageBox.Show("Error: No hay partida en progreso");
            CargarPortada();
            return;
        }

        Conexion.EstablecerConexionPartida(RutaPartidaActual);
        MainContent.Content = new UC_Pretemporada(manager, equipo, RutaPartidaActual);
    }
    // --------------------------------------------------------------------------------------

    // --------------------------------- METODO PARA CARGAR EL USERCONTROL UC_InicioTemporada
    public void CargarInicioTemporada(Manager manager, int equipo, List<int> ids)
    {
        if (!PartidaEnProgreso || string.IsNullOrEmpty(RutaPartidaActual))
        {
            MessageBox.Show("Error: No hay partida en progreso");
            CargarPortada();
            return;
        }

        Conexion.EstablecerConexionPartida(RutaPartidaActual);
        MainContent.Content = new UC_InicioTemporada(manager, equipo, ids, RutaPartidaActual);
    }
    // --------------------------------------------------------------------------------------

    // ------------------------- METODO PARA CARGAR EL USERCONTROL UC_RuedaPrensaPresentacion 
    public void CargarPresentacion(Manager manager, int equipo, List<int> ids)
    {
        if (!PartidaEnProgreso || string.IsNullOrEmpty(RutaPartidaActual))
        {
            MessageBox.Show("Error: No hay partida en progreso");
            CargarPortada();
            return;
        }

        Conexion.EstablecerConexionPartida(RutaPartidaActual);
        MainContent.Content = new UC_RuedaPrensaPresentacion(manager, equipo, ids, RutaPartidaActual);
    }
    // ---------------------------------------------------------------------------------------

    // ----------------------------------- METODO PARA CARGAR EL USERCONTROL UC_ResumenPresentacion
    public void CargarResumenPresentacion(Manager manager, int equipo, int directiva, int fans, int jugadores)
    {
        if (!PartidaEnProgreso || string.IsNullOrEmpty(RutaPartidaActual))
        {
            MessageBox.Show("Error: No hay partida en progreso");
            CargarPortada();
            return;
        }

        Conexion.EstablecerConexionPartida(RutaPartidaActual);
        MainContent.Content = new UC_ResumenPresentacion(manager, equipo, directiva, fans, jugadores, RutaPartidaActual);
    }
    // --------------------------------------------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_PantallaPrincipal
    public void CargarPantallaPrincipal(Manager manager, int equipo)
    {
        if (!PartidaEnProgreso)
        {
            CargarPortada();
            return;
        }

        // Establecer conexión con la nueva ruta confirmada
        Conexion.EstablecerConexionPartida(RutaPartidaActual);

        // Cargar el UC_PantallaPrincipal con los datos de manager y equipo
        MainContent.Content = null;
        MainContent.Content = new UC_PantallaPrincipal(manager, equipo);
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorHome
    public void CargarEditorHome()
    {
        MainContent.Content = new UC_EditorHome();
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorCompeticion
    public void CargarEditorCompeticion()
    {
        MainContent.Content = new UC_EditorCompeticion();
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorEquipos
    public void CargarEditorEquipos()
    {
        MainContent.Content = new UC_EditorEquipos();
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorClub
    public void CargarEditorClub(int equipoSeleccionado)
    {
        MainContent.Content = new UC_EditorClub(equipoSeleccionado);
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorJugadores
    public void CargarEditorJugadores(int equipo)
    {
        MainContent.Content = new UC_EditorJugadores(equipo);
    }
    // ---------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_EditorJugador
    public void CargarEditorJugador(int idJugador, int idEquipo)
    {
        MainContent.Content = new UC_EditorJugador(idJugador, idEquipo);
    }
    // ---------------------------------------------------------------------------------------
}