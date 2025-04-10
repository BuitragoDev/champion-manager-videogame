using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Entidades;
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
using Microsoft.Win32;
using System.IO;
using ChampionManager25.Datos;
using System.Drawing;

namespace ChampionManager25.UserControls
{
    public partial class UC_EditorCompeticion : UserControl
    {
        #region "Variables"
        private string rutaGrande = Path.Combine(GestorPartidas.RutaRecursosUsuario, "img", "logos_competiciones");
        private string ruta80x80 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "img", "logos_competiciones", "80x80");

        private Competicion competicion;

        private string imagenGrandeTemporal = null;
        private string imagen80x80Temporal = null;
        #endregion

        // Instancias de la Logica
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_EditorCompeticion()
        {
            InitializeComponent();
        }

        private void editarCompeticion_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.EstablecerConexionPartida("./championsManagerDB.db");
            competicion = _logicaCompeticion.ObtenerCompeticion(1);

            txtNombreCompeticion.Text = competicion.Nombre;

            string pathGrande = Path.Combine(rutaGrande, $"{competicion.IdCompeticion}.png");
            string path80 = Path.Combine(ruta80x80, $"{competicion.IdCompeticion}.png");

            if (File.Exists(pathGrande))
                imgGrande.Source = CargarImagenSinBloqueo(pathGrande);

            if (File.Exists(path80))
                img80x80.Source = CargarImagenSinBloqueo(path80);
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton VOLVER    
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorHome();
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton GUARDAR
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Actualizar el nombre en la Base de Datos
            string nombre = txtNombreCompeticion.Text.Trim();
            _logicaCompeticion.CambiarNombreCompeticion(competicion.IdCompeticion, nombre);

            // Guardar imágenes solo si fueron seleccionadas nuevas
            if (!string.IsNullOrEmpty(imagenGrandeTemporal))
            {
                string destinoGrande = Path.Combine(rutaGrande, $"{competicion.IdCompeticion}.png");
                GuardarImagen(imagenGrandeTemporal, destinoGrande);
            }

            if (!string.IsNullOrEmpty(imagen80x80Temporal))
            {
                string destino80 = Path.Combine(ruta80x80, $"{competicion.IdCompeticion}.png");
                GuardarImagen(imagen80x80Temporal, destino80);
            }
        }

        #region "Metodos"
        public void CargarCompeticion(int idCompeticion)
        {
            string pathGrande = Path.Combine(rutaGrande, $"{competicion.IdCompeticion}.png");
            string path80x80 = Path.Combine(ruta80x80, $"{competicion.IdCompeticion}.png");

            if (File.Exists(pathGrande))
                imgGrande.Source = CargarImagenSinBloqueo(pathGrande);

            if (File.Exists(path80x80))
                img80x80.Source = CargarImagenSinBloqueo(path80x80);
        }

        private void BtnCargarGrande_Click(object sender, RoutedEventArgs e)
        {
            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrande.Source = CargarImagenSinBloqueo(path);
                imagenGrandeTemporal = path;
            }
        }

        private void BtnCargar80x80_Click(object sender, RoutedEventArgs e)
        {
            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80.Source = CargarImagenSinBloqueo(path);
                imagen80x80Temporal = path;
            }
        }

        private string AbrirSelectorImagen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private void GuardarImagen(string origen, string destino)
        {
            try
            {
                File.Copy(origen, destino, true); // true = sobreescribir
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la imagen: " + ex.Message);
            }
        }

        private BitmapImage CargarImagenSinBloqueo(string ruta)
        {
            using (FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                BitmapImage imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.CacheOption = BitmapCacheOption.OnLoad; // Cargar toda la imagen en memoria
                imagen.StreamSource = stream;
                imagen.EndInit();
                imagen.Freeze(); // Opcional: permite usarla en múltiples hilos
                return imagen;
            }
        }

        private bool ValidarImagen(string path, int anchoEsperado, int altoEsperado)
        {
            try
            {
                using (var imagen = new Bitmap(path))
                {
                    if (Path.GetExtension(path).ToLower() != ".png")
                    {
                        MessageBox.Show("Solo se permiten imágenes en formato PNG.", "Formato inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    if (imagen.Width != anchoEsperado || imagen.Height != altoEsperado)
                    {
                        MessageBox.Show($"La imagen debe tener un tamaño exacto de {anchoEsperado}x{altoEsperado}px.", "Tamaño inválido", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar la imagen: " + ex.Message);
                return false;
            }
        }
        #endregion
    }
}
