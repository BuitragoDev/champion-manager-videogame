using ChampionManager25.Datos;
using ChampionManager25.MisMetodos;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Design;
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

namespace ChampionManager25.UserControls
{
    public partial class UC_Portada : UserControl
    {
        private static string rutaBaseDeDatosCreada = string.Empty;

        public UC_Portada()
        {
            InitializeComponent();
            // Copiar recursos en Mis Documentos/ChampionsManager
            GestorPartidas.CopiarRecursosSiNoExiste();
            // Ruta de la base personalizada
            string rutaBasePersonalizada = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ChampionsManager", "database", "basePersonalizada.db");

            // Ruta base original
            string rutaBaseOriginal = "championsManagerDB.db";

            // Si hay personalizada, usarla; si no, usar la original
            string rutaElegida = File.Exists(rutaBasePersonalizada) ? rutaBasePersonalizada : rutaBaseOriginal;

            // Establecer la conexión
            Conexion.EstablecerConexionPartida(rutaElegida);
        }

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON PARTIDA MANAGER
        private void imgPartidaManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarCrearManager();
        }

        private void imgPartidaManager_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgPartidaManager.Source = new BitmapImage(new Uri("/Recursos/img/partidaManagerON.png", UriKind.Relative));
        }

        private void imgPartidaManager_MouseLeave(object sender, MouseEventArgs e)
        {
            imgPartidaManager.Source = new BitmapImage(new Uri("/Recursos/img/partidaManager.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON PARTIDA PROMANAGER
        private void imgPartidaPromanager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
    
        }

        private void imgPartidaPromanager_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgPartidaPromanager.Source = new BitmapImage(new Uri("/Recursos/img/partidaPromanagerON.png", UriKind.Relative));
        }

        private void imgPartidaPromanager_MouseLeave(object sender, MouseEventArgs e)
        {
            imgPartidaPromanager.Source = new BitmapImage(new Uri("/Recursos/img/partidaPromanager.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON CARGAR PARTIDA
        private void imgCargarPartida_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPartida();
        }

        private void imgCargarPartida_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgCargarPartida.Source = new BitmapImage(new Uri("/Recursos/img/cargarPartidaON.png", UriKind.Relative));
        }

        private void imgCargarPartida_MouseLeave(object sender, MouseEventArgs e)
        {
            imgCargarPartida.Source = new BitmapImage(new Uri("/Recursos/img/cargarPartida.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON EDITOR DEL JUEGO
        private void imgEditorJuego_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorHome();
        }

        private void imgEditorJuego_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditorJuego.Source = new BitmapImage(new Uri("/Recursos/img/editorJuegoON.png", UriKind.Relative));
        }

        private void imgEditorJuego_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditorJuego.Source = new BitmapImage(new Uri("/Recursos/img/editorJuego.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON SALIR DEL JUEGO
        private void imgSalirJuego_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            Application.Current.Shutdown();
        }

        private void imgSalirJuego_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSalirJuego.Source = new BitmapImage(new Uri("/Recursos/img/salirJuegoON.png", UriKind.Relative));
        }

        private void imgSalirJuego_MouseLeave(object sender, MouseEventArgs e)
        {
            imgSalirJuego.Source = new BitmapImage(new Uri("/Recursos/img/salirJuego.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON REPORTAR BUG
        private void imgReportarBug_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
         
        }

        private void imgReportarBug_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgReportarBug.Source = new BitmapImage(new Uri("/Recursos/img/reportarErroresON.png", UriKind.Relative));
        }

        private void imgReportarBug_MouseLeave(object sender, MouseEventArgs e)
        {
            imgReportarBug.Source = new BitmapImage(new Uri("/Recursos/img/reportarErrores.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------
    }
}
