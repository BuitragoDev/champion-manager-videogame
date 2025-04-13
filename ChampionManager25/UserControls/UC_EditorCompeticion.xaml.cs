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
using ChampionManager25.Vistas;
using System.Diagnostics;

namespace ChampionManager25.UserControls
{
    public partial class UC_EditorCompeticion : UserControl
    {
        #region "Variables"
        private string rutaGrande = Path.Combine(GestorPartidas.RutaRecursosUsuario, "logos_competiciones");
        private string ruta80x80 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "logos_competiciones", "80x80");

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
            Metodos.ReproducirSonidoClick();

            string nombreImagenGrande = "";
            string nombreImagen80x80 = "";

            try
            {
                if (!string.IsNullOrEmpty(imagenGrandeTemporal))
                {
                    string destinoGrande = Path.Combine(rutaGrande, $"{competicion.IdCompeticion}.png");
                    nombreImagenGrande = GuardarImagen(imagenGrandeTemporal, destinoGrande);
                }

                if (!string.IsNullOrEmpty(imagen80x80Temporal))
                {
                    string destino80 = Path.Combine(ruta80x80, $"{competicion.IdCompeticion}.png");
                    nombreImagen80x80 = GuardarImagen(imagen80x80Temporal, destino80);
                }

                // Actualizar el nombre y la ruta de las imagenes en la Base de Datos
                Competicion oCompeticion = new Competicion
                {
                    IdCompeticion = 1,
                    Nombre = txtNombreCompeticion.Text.Trim(),
                    RutaImagen = $"Recursos/img/logos_competiciones/{nombreImagenGrande}",
                    RutaImagen80 = $"Recursos/img/logos_competiciones/80x80/{nombreImagen80x80}"
                };
                _logicaCompeticion.EditarCompeticion(oCompeticion);

                string titulo = "INFORMACIÓN";
                string mensaje = "La competición ha sido actualizada correctamente";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
            }
            catch (Exception ex)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "Ha ocurrido un error al guardar los datos";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 200x200
        private void BtnCargarGrande_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrande.Source = CargarImagenSinBloqueo(path);
                imagenGrandeTemporal = path;
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 80x80
        private void BtnCargar80x80_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80.Source = CargarImagenSinBloqueo(path);
                imagen80x80Temporal = path;
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

        private string GuardarImagen(string origen, string destino)
        {
            try
            {
                string directorio = Path.GetDirectoryName(destino);
                string nombreArchivo = Path.GetFileNameWithoutExtension(destino);
                string extension = Path.GetExtension(destino);

                string destinoFinal = destino;
                int version = 1;

                // Si ya existe, buscar una versión libre
                while (File.Exists(destinoFinal))
                {
                    destinoFinal = Path.Combine(directorio, $"{nombreArchivo}_v{version}{extension}");
                    version++;
                }

                File.Copy(origen, destinoFinal);
                return Path.GetFileName(destinoFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la imagen: " + ex.Message);
                return null;
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
                        string titulo = "FORMATO INVÁLIDO";
                        string mensaje = "Solo se permiten imágenes en formato PNG";
                        frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaEmergente.ShowDialog();
                        return false;
                    }

                    if (imagen.Width != anchoEsperado || imagen.Height != altoEsperado)
                    {
                        string titulo = "FORMATO INVÁLIDO";
                        string mensaje = $"La imagen debe tener un tamaño exacto de {anchoEsperado}x{altoEsperado}px";
                        frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaEmergente.ShowDialog();
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "Error al validar la imagen";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
                return false;
            }
        }
        #endregion
    }
}
