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
using System.Windows.Shapes;
using ChampionManager25.UserControls;
using ChampionManager25.MisMetodos;
using System.Windows.Threading;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;

namespace ChampionManager25;

public partial class MainWindow : Window
{
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

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        // Crear copia de la BD
        try
        {
            // Genera un número aleatorio de 10 cifras
            Random random = new Random();
            long numeroAleatorio = (long)(random.NextDouble() * 9_999_999_999L) + 1_000_000_000L;
            string numeroComoCadena = numeroAleatorio.ToString();

            // Obtener el ID del manager
            long managerId = numeroAleatorio;

            // Crear la base de datos del manager
            SQLiteDatabaseManager.CreateManagerDatabase(numeroComoCadena);

            // Continuar con la carga de la aplicación...
        }
        catch (Exception ex)
        {
            MessageBox.Show($"No se pudo iniciar la aplicación: {ex.Message}",
                          "Error Crítico",
                          MessageBoxButton.OK,
                          MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
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
        MainContent.Content = new UC_Portada();
    }
    // --------------------------------------------------------------------------------------

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_CrearManager
    public void CargarCrearManager()
    {
        MainContent.Content = new UC_CrearManager();
    }
    // --------------------------------------------------------------------------------------\

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_CargarPartida
    public void CargarPartida()
    {
        MainContent.Content = new UC_CargarPartida();
    }
    // --------------------------------------------------------------------------------------

    // --------------------------------- METODO PARA CARGAR EL USERCONTROL UC_SeleccionEquipo
    public void CargarSeleccionEquipo(Manager manager)
    {
        MainContent.Content = new UC_SeleccionEquipo(manager);
    }
    // --------------------------------------------------------------------------------------

    // ------------------------------------ METODO PARA CARGAR EL USERCONTROL UC_Pretemporada
    public void CargarPretemporada(Manager manager, int equipo)
    {
        MainContent.Content = new UC_Pretemporada(manager, equipo);
    }
    // --------------------------------------------------------------------------------------

    // --------------------------------- METODO PARA CARGAR EL USERCONTROL UC_InicioTemporada
    public void CargarInicioTemporada(Manager manager, int equipo, List<int> ids)
    {
        MainContent.Content = new UC_InicioTemporada(manager, equipo, ids);
    }
    // --------------------------------------------------------------------------------------

    // ------------------------- METODO PARA CARGAR EL USERCONTROL UC_RuedaPrensaPresentacion 
    public void CargarPresentacion(Manager manager, int equipo, List<int> ids)
    {
        MainContent.Content = new UC_RuedaPrensaPresentacion(manager, equipo, ids);
    }
    // ---------------------------------------------------------------------------------------

    // ----------------------------------- METODO PARA CARGAR EL USERCONTROL UC_ResumenPresentacion
    public void CargarResumenPresentacion(Manager manager, int equipo, int directiva, int fans, int jugadores)
    {
        // Cambiar el contenido de MainContent a UC_RuedaPrensaPresentacion
        MainContent.Content = new UC_ResumenPresentacion(manager, equipo, directiva, fans, jugadores);
    }
    // --------------------------------------------------------------------------------------------------------------------------

    // -------------------------------- METODO PARA CARGAR EL USERCONTROL UC_PantallaPrincipal
    public void CargarPantallaPrincipal(Manager manager, int equipo)
    {
        MainContent.Content = null;
        MainContent.Content = new UC_PantallaPrincipal(manager, equipo);
    }
    // ---------------------------------------------------------------------------------------
}