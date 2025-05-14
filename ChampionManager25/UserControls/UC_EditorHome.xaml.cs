using ChampionManager25.Datos;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
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
    public partial class UC_EditorHome : UserControl
    {
        public static string RutaMisDocumentos = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ChampionsManager");

        public UC_EditorHome()
        {
            InitializeComponent();
        }

        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR JUGADORES
        private void imgEditarJugadores_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorJugadores(1);
        }

        private void imgEditarJugadores_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarJugadores.Source = new BitmapImage(new Uri("/Recursos/img/editor_jugadores_hover.png", UriKind.Relative));
        }

        private void imgEditarJugadores_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarJugadores.Source = new BitmapImage(new Uri("/Recursos/img/editor_jugadores.png", UriKind.Relative));
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR EQUIPOS
        private void imgEditarEquipos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorEquipos();
        }

        private void imgEditarEquipos_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarEquipos.Source = new BitmapImage(new Uri("/Recursos/img/editor_equipos_hover.png", UriKind.Relative));
        }

        private void imgEditarEquipos_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarEquipos.Source = new BitmapImage(new Uri("/Recursos/img/editor_equipos.png", UriKind.Relative));
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR COMPETICION
        private void imgEditarCompeticion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorCompeticion();
        }

        private void imgEditarCompeticion_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarCompeticion.Source = new BitmapImage(new Uri("/Recursos/img/editor_competicion_hover.png", UriKind.Relative));
        }

        private void imgEditarCompeticion_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarCompeticion.Source = new BitmapImage(new Uri("/Recursos/img/editor_competicion.png", UriKind.Relative));
        }

        // ------------------------------------------------------------------------------- Eventos del boton IMPORTAR BASE DE DATOS
        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "SQLite Database (*.db)|*.db",
                Title = "Selecciona una base de datos SQLite personalizada"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string origen = openFileDialog.FileName;
                    string destino = System.IO.Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        "ChampionsManager", "database", "basePersonalizada.db"
                    );

                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destino)); // por si no existe
                    File.Copy(origen, destino, true);

                    string titulo = "INFORMACIÓN";
                    string mensaje = "Base de datos personalizada importada correctamente.";
                    frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                    ventanaEmergente.ShowDialog();

                    // Ruta de la base personalizada
                    string rutaBasePersonalizada = System.IO.Path.Combine(RutaMisDocumentos, "database", "basePersonalizada.db");

                    // Establecer la conexión
                    Conexion.EstablecerConexionPartida(rutaBasePersonalizada);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al importar la base de datos: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // ------------------------------------------------------------------------------- Eventos del boton REESTABLECER BASE DE DATOS ORIGINAL
        private void btnReestablecer_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            Conexion.CerrarTodasLasConexiones();

            string rutaBasePersonalizada = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ChampionsManager", "database", "basePersonalizada.db");

            if (File.Exists(rutaBasePersonalizada))
            {
                try
                {
                    File.Delete(rutaBasePersonalizada);

                    string titulo = "INFORMACIÓN";
                    string mensaje = "Base de datos personalizada eliminada correctamente. Se usará la base de datos original.";
                    frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                    ventanaEmergente.ShowDialog();

                    Conexion.EstablecerConexionPartida("./championsManagerDB.db");
                    btnReestablecer.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"No se pudo eliminar la base personalizada: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay ninguna base de datos personalizada para eliminar.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            string rutaBasePersonalizada = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "ChampionsManager", "database", "basePersonalizada.db");

            if (File.Exists(rutaBasePersonalizada))
            {
                btnReestablecer.Visibility = Visibility.Visible;
            }
            else
            {
                btnReestablecer.Visibility = Visibility.Collapsed;
            }
        }
    }
}
