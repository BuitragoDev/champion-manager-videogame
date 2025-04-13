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
            Conexion.EstablecerConexionPartida("./championsManagerDB.db");
        }

        // ---------------------------------------------------------------------------- EVENTOS DEL BOTON JUGAR CAMPEONATO
        private void imgJugarCampeonato_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarCrearManager();
        }

        private void imgJugarCampeonato_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgJugarCampeonato.Source = new BitmapImage(new Uri("/Recursos/img/jugarCampeonatoHover.png", UriKind.Relative));
        }

        private void imgJugarCampeonato_MouseLeave(object sender, MouseEventArgs e)
        {
            imgJugarCampeonato.Source = new BitmapImage(new Uri("/Recursos/img/jugarCampeonato.png", UriKind.Relative));
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
            imgCargarPartida.Source = new BitmapImage(new Uri("/Recursos/img/cargarPartidaHover.png", UriKind.Relative));
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
            imgEditorJuego.Source = new BitmapImage(new Uri("/Recursos/img/editorJuegoHover.png", UriKind.Relative));
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
            imgSalirJuego.Source = new BitmapImage(new Uri("/Recursos/img/salirJuegoHover.png", UriKind.Relative));
        }

        private void imgSalirJuego_MouseLeave(object sender, MouseEventArgs e)
        {
            imgSalirJuego.Source = new BitmapImage(new Uri("/Recursos/img/salirJuego.png", UriKind.Relative));
        }
        // --------------------------------------------------------------------------------------------------------------
    }
}
