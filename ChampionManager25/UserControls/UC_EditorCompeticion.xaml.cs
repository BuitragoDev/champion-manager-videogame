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
        private Competicion competicion2;
        private Competicion competicion5;
        private Competicion competicion6;

        private string imagenGrandeTemporal = null;
        private string imagen80x80Temporal = null;
        private string imagenGrandeLiga2Temporal = null;
        private string imagen80x80Liga2Temporal = null;
        private string imagenGrandeEuropa1Temporal = null;
        private string imagen80x80Europa1Temporal = null;
        private string imagenGrandeEuropa2Temporal = null;
        private string imagen80x80Europa2Temporal = null;
        #endregion

        // Instancias de la Logica
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_EditorCompeticion()
        {
            InitializeComponent();
        }

        private void editarCompeticion_Loaded(object sender, RoutedEventArgs e)
        {
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

            competicion = _logicaCompeticion.ObtenerCompeticion(1);
            competicion2 = _logicaCompeticion.ObtenerCompeticion(2);
            competicion5 = _logicaCompeticion.ObtenerCompeticion(5);
            competicion6 = _logicaCompeticion.ObtenerCompeticion(6);

            txtNombreCompeticion.Text = competicion.Nombre;
            txtNombreCompeticion2.Text = competicion2.Nombre;
            txtNombreCompeticionEuropa1.Text = competicion5.Nombre;
            txtNombreCompeticionEuropa2.Text = competicion6.Nombre;

            string pathGrande = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion.RutaImagen}");
            string path80 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion.RutaImagen80}");
            string pathGrandeLiga2 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion2.RutaImagen}");
            string path80Liga2 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion2.RutaImagen80}");
            string pathGrandeEuropa1 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion5.RutaImagen}");
            string path80Europa1 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion5.RutaImagen80}");
            string pathGrandeEuropa2 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion6.RutaImagen}");
            string path80Europa2 = Path.Combine(GestorPartidas.RutaMisDocumentos, $"{competicion6.RutaImagen80}");

            if (File.Exists(pathGrande))
                imgGrande.Source = CargarImagenSinBloqueo(pathGrande);

            if (File.Exists(path80))
                img80x80.Source = CargarImagenSinBloqueo(path80);

            if (File.Exists(pathGrandeLiga2))
                imgGrandeLiga2.Source = CargarImagenSinBloqueo(pathGrandeLiga2);

            if (File.Exists(path80Liga2))
                img80x80Liga2.Source = CargarImagenSinBloqueo(path80Liga2);

            if (File.Exists(pathGrandeEuropa1))
                imgGrandeEuropa1.Source = CargarImagenSinBloqueo(pathGrandeEuropa1);

            if (File.Exists(path80Europa1))
                img80x80Europa1.Source = CargarImagenSinBloqueo(path80Europa1);

            if (File.Exists(pathGrandeEuropa2))
                imgGrandeEuropa2.Source = CargarImagenSinBloqueo(pathGrandeEuropa2);

            if (File.Exists(path80Europa2))
                img80x80Europa2.Source = CargarImagenSinBloqueo(path80Europa2);
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
            string nombreImagenGrandeLiga2 = "";
            string nombreImagen80x80Liga2 = "";
            string nombreImagenGrandeEuropa1 = "";
            string nombreImagen80x80Europa1 = "";
            string nombreImagenGrandeEuropa2 = "";
            string nombreImagen80x80Europa2 = "";

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

                if (!string.IsNullOrEmpty(imagenGrandeLiga2Temporal))
                {
                    string destinoGrandeLiga2 = Path.Combine(rutaGrande, $"{competicion2.IdCompeticion}.png");
                    nombreImagenGrandeLiga2 = GuardarImagen(imagenGrandeLiga2Temporal, destinoGrandeLiga2);
                }

                if (!string.IsNullOrEmpty(imagen80x80Liga2Temporal))
                {
                    string destino80Liga2 = Path.Combine(ruta80x80, $"{competicion2.IdCompeticion}.png");
                    nombreImagen80x80Liga2 = GuardarImagen(imagen80x80Liga2Temporal, destino80Liga2);
                }
                
                if (!string.IsNullOrEmpty(imagenGrandeEuropa1Temporal))
                {
                    string destinoGrandeEuropa1 = Path.Combine(rutaGrande, $"{competicion5.IdCompeticion}.png");
                    nombreImagenGrandeEuropa1 = GuardarImagen(imagenGrandeEuropa1Temporal, destinoGrandeEuropa1);
                }

                if (!string.IsNullOrEmpty(imagen80x80Europa1Temporal))
                {
                    string destino80Europa1 = Path.Combine(ruta80x80, $"{competicion5.IdCompeticion}.png");
                    nombreImagen80x80Europa1 = GuardarImagen(imagen80x80Europa1Temporal, destino80Europa1);
                }

                if (!string.IsNullOrEmpty(imagenGrandeEuropa2Temporal))
                {
                    string destinoGrandeEuropa2 = Path.Combine(rutaGrande, $"{competicion6.IdCompeticion}.png");
                    nombreImagenGrandeEuropa2 = GuardarImagen(imagenGrandeEuropa2Temporal, destinoGrandeEuropa2);
                }

                if (!string.IsNullOrEmpty(imagen80x80Europa2Temporal))
                {
                    string destino80Europa2 = Path.Combine(ruta80x80, $"{competicion6.IdCompeticion}.png");
                    nombreImagen80x80Europa2 = GuardarImagen(imagen80x80Europa2Temporal, destino80Europa2);
                }

                // Actualizar el nombre y la ruta de las imagenes en la Base de Datos
                Competicion oCompeticion = new Competicion
                {
                    IdCompeticion = 1,
                    Nombre = txtNombreCompeticion.Text.Trim(),
                    RutaImagen = $"Recursos/img/logos_competiciones/{nombreImagenGrande}",
                    RutaImagen80 = $"Recursos/img/logos_competiciones/80x80/{nombreImagen80x80}"
                };
                Competicion oCompeticion2 = new Competicion
                {
                    IdCompeticion = 2,
                    Nombre = txtNombreCompeticion2.Text.Trim(),
                    RutaImagen = $"Recursos/img/logos_competiciones/{nombreImagenGrandeLiga2}",
                    RutaImagen80 = $"Recursos/img/logos_competiciones/80x80/{nombreImagen80x80Liga2}"
                };
                Competicion oCompeticionEuropa1 = new Competicion
                {
                    IdCompeticion = 5,
                    Nombre = txtNombreCompeticionEuropa1.Text.Trim(),
                    RutaImagen = $"Recursos/img/logos_competiciones/{nombreImagenGrandeEuropa1}",
                    RutaImagen80 = $"Recursos/img/logos_competiciones/80x80/{nombreImagen80x80Europa1}"
                };
                Competicion oCompeticionEuropa2 = new Competicion
                {
                    IdCompeticion = 6,
                    Nombre = txtNombreCompeticionEuropa2.Text.Trim(),
                    RutaImagen = $"Recursos/img/logos_competiciones/{nombreImagenGrandeEuropa2}",
                    RutaImagen80 = $"Recursos/img/logos_competiciones/80x80/{nombreImagen80x80Europa2}"
                };
                _logicaCompeticion.EditarCompeticion(oCompeticion);
                _logicaCompeticion.EditarCompeticion(oCompeticion2);
                _logicaCompeticion.EditarCompeticion(oCompeticionEuropa1);
                _logicaCompeticion.EditarCompeticion(oCompeticionEuropa2);

                // Limpiar imagenes temporales
                imagenGrandeTemporal = null;
                imagen80x80Temporal = null;
                imagenGrandeLiga2Temporal = null;
                imagen80x80Liga2Temporal = null;
                imagenGrandeEuropa1Temporal = null;
                imagen80x80Europa1Temporal = null;
                imagenGrandeEuropa2Temporal = null;
                imagen80x80Europa2Temporal = null;

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

        // ---------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 200x200 LIGA 1
        private void imgGrande_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrande.Source = CargarImagenSinBloqueo(path);
                imagenGrandeTemporal = path;
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 80x80 LIGA 1
        private void img80x80_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80.Source = CargarImagenSinBloqueo(path);
                imagen80x80Temporal = path;
            }
        }

        // ---------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 200x200 LIGA 2
        private void imgGrandeLiga2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrandeLiga2.Source = CargarImagenSinBloqueo(path);
                imagenGrandeLiga2Temporal = path;
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 80x80 LIGA 2
        private void img80x80Liga2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80Liga2.Source = CargarImagenSinBloqueo(path);
                imagen80x80Liga2Temporal = path;
            }
        }

        // ---------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 200x200 EUROPA 1
        private void imgGrandeEuropa1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrandeEuropa1.Source = CargarImagenSinBloqueo(path);
                imagenGrandeEuropa1Temporal = path;
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 80x80 EUROPA 1
        private void img80x80Europa1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80Europa1.Source = CargarImagenSinBloqueo(path);
                imagen80x80Europa1Temporal = path;
            }
        }

        // ---------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 200x200 EUROPA 2
        private void imgGrandeEuropa2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgGrandeEuropa2.Source = CargarImagenSinBloqueo(path);
                imagenGrandeEuropa2Temporal = path;
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR IMAGEN 80x80 EUROPA 2
        private void img80x80Europa2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                img80x80Europa2.Source = CargarImagenSinBloqueo(path);
                imagen80x80Europa2Temporal = path;
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
