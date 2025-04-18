using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;

namespace ChampionManager25.UserControls
{
    public partial class UC_EditorClub : UserControl
    {
        #region "Variables"
        private int _equipo;
        Equipo oEquipo;
        Entrenador oEntrenador;

        private string rutaEscudoGrande = Path.Combine(GestorPartidas.RutaRecursosUsuario, "escudos_equipos");
        private string rutaEscudo120 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "escudos_equipos", "120x120");
        private string rutaEscudo80 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "escudos_equipos", "80x80");
        private string rutaEscudo64 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "escudos_equipos", "64x64");
        private string rutaEscudo32 = Path.Combine(GestorPartidas.RutaRecursosUsuario, "escudos_equipos", "32x32");
        private string rutaEntrenador = Path.Combine(GestorPartidas.RutaRecursosUsuario, "entrenadores");
        private string rutaEstadioInterior = Path.Combine(GestorPartidas.RutaRecursosUsuario, "estadios");
        private string rutaEstadioExterior = Path.Combine(GestorPartidas.RutaRecursosUsuario, "estadios");
        private string rutaKitLocal = Path.Combine(GestorPartidas.RutaRecursosUsuario, "kits");
        private string rutaKitVisitante = Path.Combine(GestorPartidas.RutaRecursosUsuario, "kits");

        private string imagenEscudoGrandeTemporal = null;
        private string imagenEscudo120Temporal = null;
        private string imagenEscudo80Temporal = null;
        private string imagenEscudo64Temporal = null;
        private string imagenEscudo32Temporal = null;
        private string imagenEntrenadorTemporal = null;
        private string imagenEstadioInteriorTemporal = null;
        private string imagenEstadioExteriorTemporal = null;
        private string imagenKitLocalTemporal = null;
        private string imagenKitVisitanteTemporal = null;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        EntrenadorLogica _logicaEntrenador = new EntrenadorLogica();

        public UC_EditorClub(int equipo)
        {
            InitializeComponent();
            _equipo = equipo;

            oEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            oEntrenador = _logicaEntrenador.MostrarEntrenador(_equipo);
        }

        private void editarClub_Loaded(object sender, RoutedEventArgs e)
        { 
            lblSeleccionEquipo.Text = oEquipo.Nombre.ToUpper();
            CargarDatos(oEquipo, oEntrenador);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorEquipos();
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESCUDO 200x200px
        private void imgEscudo200_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 200, 200))
            {
                imgEscudo200.Source = CargarImagenSinBloqueo(path);
                imagenEscudoGrandeTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESCUDO 120x120px
        private void imgEscudo120_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 120, 120))
            {
                imgEscudo120.Source = CargarImagenSinBloqueo(path);
                imagenEscudo120Temporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESCUDO 80x80px
        private void imgEscudo80_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 80, 80))
            {
                imgEscudo80.Source = CargarImagenSinBloqueo(path);
                imagenEscudo80Temporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESCUDO 64x64px
        private void imgEscudo64_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 64, 64))
            {
                imgEscudo64.Source = CargarImagenSinBloqueo(path);
                imagenEscudo64Temporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESCUDO 32x32px
        private void imgEscudo32_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 32, 32))
            {
                imgEscudo32.Source = CargarImagenSinBloqueo(path);
                imagenEscudo32Temporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton KIT LOCAL
        private void imgKitLocal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 160, 160))
            {
                imgKitLocal.Source = CargarImagenSinBloqueo(path);
                imagenKitLocalTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton KIT VISITANTE
        private void imgKitVisitante_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 160, 160))
            {
                imgKitVisitante.Source = CargarImagenSinBloqueo(path);
                imagenKitVisitanteTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton FOTO DE ENTRENADOR
        private void imgFotoEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 100, 100))
            {
                imgFotoEntrenador.Source = CargarImagenSinBloqueo(path);
                imagenEntrenadorTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESTADIO EXTERIOR
        private void imgEstadioExterior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 400, 160))
            {
                imgEstadioExterior.Source = CargarImagenSinBloqueo(path);
                imagenEstadioExteriorTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton ESTADIO INTERIOR
        private void imgEstadioInterior_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 700, 540))
            {
                imgEstadioInterior.Source = CargarImagenSinBloqueo(path);
                imagenEstadioInteriorTemporal = path;
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton REESTABLECER
        private void btnReestablecer_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            CargarDatos(oEquipo, oEntrenador);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton GUARDAR CAMBIOS
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string nombreImagenEntrenador = "";
            string nombreImagenEstadioExterior = "";
            string nombreImagenEstadioInterior = "";
            string nombreImagenEscudo200 = "";
            string nombreImagenEscudo120 = "";
            string nombreImagenEscudo80 = "";
            string nombreImagenEscudo64 = "";
            string nombreImagenEscudo32 = "";
            string nombreImagenKitLocal = "";
            string nombreImagenKitVisitante = "";

            try
            {
                // -------------------------------------------------------------------------------- Actualizar los Datos del Club en la Base de Datos
                Equipo oEquipoEditar = new Equipo
                {
                    IdEquipo = _equipo,
                    Nombre = txtNombreLargo.Text.Trim(),
                    NombreCorto = txtNombreCorto.Text.Trim(),
                    Presidente = txtPresidente.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim(),
                    Estadio = txtNombreEstadio.Text.Trim(),
                    Aforo = int.Parse(txtAforoEstadio.Text.Trim()),
                    Reputacion = (int)cbReputacion.SelectedItem,
                    Objetivo = (cbObjetivo.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Rival = (int)cbRival.SelectedValue,
                    RutaEstadioExterior = "",
                    RutaEstadioInterior = "",
                    RutaImagen = "",
                    RutaImagen120 = "",
                    RutaImagen80 = "",
                    RutaImagen64 = "",
                    RutaImagen32 = "",
                    RutaKitLocal = "",
                    RutaKitVisitante = ""
                };

                if (!string.IsNullOrEmpty(imagenEntrenadorTemporal))
                {
                    string destinoEntrenador = Path.Combine(rutaEntrenador, $"{_equipo}.png");
                    nombreImagenEntrenador = GuardarImagen(imagenEntrenadorTemporal, destinoEntrenador);
                }
                if (!string.IsNullOrEmpty(imagenEstadioExteriorTemporal))
                {
                    string destinoEstadioExterior = Path.Combine(rutaEstadioExterior, $"{_equipo}exterior.png");
                    nombreImagenEstadioExterior = GuardarImagen(imagenEstadioExteriorTemporal, destinoEstadioExterior);
                }
                if (!string.IsNullOrEmpty(imagenEstadioInteriorTemporal))
                {
                    string destinoEstadioInterior = Path.Combine(rutaEstadioInterior, $"{_equipo}interior.png");
                    nombreImagenEstadioInterior = GuardarImagen(imagenEstadioInteriorTemporal, destinoEstadioInterior);
                }
                if (!string.IsNullOrEmpty(imagenKitLocalTemporal))
                {
                    string destinoKitLocal = Path.Combine(rutaKitLocal, $"{_equipo}local.png");
                    nombreImagenKitLocal = GuardarImagen(imagenKitLocalTemporal, destinoKitLocal);
                }
                if (!string.IsNullOrEmpty(imagenKitVisitanteTemporal))
                {
                    string destinoKitVisitante = Path.Combine(rutaKitVisitante, $"{_equipo}visitante.png");
                    nombreImagenKitVisitante = GuardarImagen(imagenKitVisitanteTemporal, destinoKitVisitante);
                }
                if (!string.IsNullOrEmpty(imagenEscudoGrandeTemporal))
                {
                    string destinoEscudo200 = Path.Combine(rutaEscudoGrande, $"{_equipo}.png");
                    nombreImagenEscudo200 = GuardarImagen(imagenEscudoGrandeTemporal, destinoEscudo200);
                }
                if (!string.IsNullOrEmpty(imagenEscudo120Temporal))
                {
                    string destinoEscudo120 = Path.Combine(rutaEscudo120, $"{_equipo}.png");
                    nombreImagenEscudo120 = GuardarImagen(imagenEscudo120Temporal, destinoEscudo120);
                }

                if (!string.IsNullOrEmpty(imagenEscudo80Temporal))
                {
                    string destinoEscudo80 = Path.Combine(rutaEscudo80, $"{_equipo}.png");
                    nombreImagenEscudo80 = GuardarImagen(imagenEscudo80Temporal, destinoEscudo80);
                }
                if (!string.IsNullOrEmpty(imagenEscudo64Temporal))
                {
                    string destinoEscudo64 = Path.Combine(rutaEscudo64, $"{_equipo}.png");
                    nombreImagenEscudo64 = GuardarImagen(imagenEscudo64Temporal, destinoEscudo64);
                }
                if (!string.IsNullOrEmpty(imagenEscudo32Temporal))
                {
                    string destinoEscudo32 = Path.Combine(rutaEscudo32, $"{_equipo}.png");
                    nombreImagenEscudo32 = GuardarImagen(imagenEscudo32Temporal, destinoEscudo32);
                }

                // ------------------------------------------------------------------------ Actualizar las imagenes de los ESCUDOS en la Base de Datos
                oEquipoEditar.RutaImagen = $"Recursos/img/escudos_equipos/{nombreImagenEscudo200}";
                oEquipoEditar.RutaImagen120 = $"Recursos/img/escudos_equipos/120x120/{nombreImagenEscudo120}";
                oEquipoEditar.RutaImagen80 = $"Recursos/img/escudos_equipos/80x80/{nombreImagenEscudo80}";
                oEquipoEditar.RutaImagen64 = $"Recursos/img/escudos_equipos/64x64/{nombreImagenEscudo64}";
                oEquipoEditar.RutaImagen32 = $"Recursos/img/escudos_equipos/32x32/{nombreImagenEscudo32}";


                // ------------------------------------------------------------------- Actualizar las imagenes de las EQUIPACIONES en la Base de Datos
                oEquipoEditar.RutaKitLocal = $"Recursos/img/kits/{nombreImagenKitLocal}";
                oEquipoEditar.RutaKitVisitante = $"Recursos/img/kits/{nombreImagenKitVisitante}";

                // --------------------------------------------------------------------------- Actualizar las imagenes del Estadio en la Base de Datos
                oEquipoEditar.RutaEstadioExterior = $"Recursos/img/estadios/{nombreImagenEstadioExterior}";
                oEquipoEditar.RutaEstadioInterior = $"Recursos/img/estadios/{nombreImagenEstadioInterior}";

                // ------------------------------------------------- Actualizar el nombre y la ruta de las imagenes del ENTRENADOR en la Base de Datos
                Entrenador oEntrenador = new Entrenador
                {
                    IdEquipo = _equipo,
                    Nombre = txtNombreEntrenador.Text.Trim(),
                    Apellido = txtApellidoEntrenador.Text.Trim(),
                    Puntos = int.Parse(txtPuntosEntrenador.Text.Trim()),
                    Reputacion = (int)cbReputacionEntrenador.SelectedItem,
                    TacticaFavorita = (cbTacticaFavorita.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    RutaImagen = $"Recursos/img/entrenadores/{nombreImagenEntrenador}",
                };
                _logicaEntrenador.EditarEntrenador(oEntrenador);
                _logicaEquipo.EditarEquipo(oEquipoEditar);

                // Limpiar imagenes cargadas
                nombreImagenEntrenador = "";
                nombreImagenEstadioExterior = "";
                nombreImagenEstadioInterior = "";
                nombreImagenEscudo200 = "";
                nombreImagenEscudo120 = "";
                nombreImagenEscudo80 = "";
                nombreImagenEscudo64 = "";
                nombreImagenEscudo32 = "";
                nombreImagenKitLocal = "";
                nombreImagenKitVisitante = "";

                // -----------------------------------------------------------------------------------------------------------------------------------
                string titulo = "INFORMACIÓN";
                string mensaje = "El equipo ha sido actualizado correctamente";
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

        // ------------------------------------------------- EVENTOS DE LAS CAJAS DE TEXTO ------------------------------------------------
        // --------------- NOMBRE EQUIPO LARGO
        private void txtNombreLargo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- NOMBRE EQUIPO CORTO
        private void txtNombreCorto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        private void txtNombreCorto_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- NOMBRE PRESIDENTE
        private void txtPresidente_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- NOMBRE CIUDAD
        private void txtCiudad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- NOMBRE ENTRENADOR
        private void txtNombreEntrenador_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- APELLIDO ENTRENADOR
        private void txtApellidoEntrenador_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- PUNTOS ENTRENADOR
        private void txtPuntosEntrenador_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPuntosEntrenador_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- NOMBRE ESTADIO
        private void txtNombreEstadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- AFORO ESTADIO
        private void txtAforoEstadio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtAforoEstadio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        #region "Metodos"
        private void CargarDatos(Equipo equipo, Entrenador entrenador)
        {
            // Cargar Datos del Club
            txtNombreLargo.Text = equipo.Nombre;
            txtNombreCorto.Text = equipo.NombreCorto;
            txtPresidente.Text = equipo.Presidente;
            txtCiudad.Text = equipo.Ciudad;

            // Cargar ComboBox Reputacion Equipo
            for (int i = 1; i <= 100; i++)
            {
                cbReputacion.Items.Add(i);
            }
            cbReputacion.SelectedItem = equipo.Reputacion;

            // Cargar ComboBox Objetivo
            cbObjetivo.SelectedValue = equipo.Objetivo;

            // Cargar ComboBox Rival
            List<Equipo> listaEquipos = _logicaEquipo.ListarOtrosEquipos(equipo.IdEquipo);
            cbRival.ItemsSource = listaEquipos;
            cbRival.DisplayMemberPath = "Nombre";
            cbRival.SelectedValuePath = "IdEquipo";
            cbRival.SelectedValue = oEquipo.Rival;

            // Cargar Imagenes de los Escudos
            imgEscudo200.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen));
            imgEscudo120.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen120));
            imgEscudo80.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
            imgEscudo64.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen64));
            imgEscudo32.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen32));

            // Cargar imagenes de las Equipaciones
            imgKitLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaKitLocal));
            imgKitVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaKitVisitante));

            // Cargar Datos Entrenador
            imgFotoEntrenador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + entrenador.RutaImagen));
            txtNombreEntrenador.Text = entrenador.Nombre;
            txtApellidoEntrenador.Text = entrenador.Apellido;
            txtPuntosEntrenador.Text = entrenador.Puntos.ToString();

            // Cargar ComboBox Entrenador
            for (int i = 1; i <= 100; i++)
            {
                cbReputacionEntrenador.Items.Add(i);
            }
            cbReputacionEntrenador.SelectedItem = oEntrenador.Reputacion;
            cbTacticaFavorita.SelectedValue = entrenador.TacticaFavorita;

            // Cargar los datos del Estadio
            imgEstadioInterior.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaEstadioInterior));
            imgEstadioExterior.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaEstadioExterior));
            txtNombreEstadio.Text = equipo.Estadio;
            txtAforoEstadio.Text = equipo.Aforo.ToString();
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
        #endregion
    }
}
