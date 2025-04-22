using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
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
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_EditorEquipos : UserControl
    {
        #region "Variables"
        int equipoSeleccionado = 0; // Variable que recoge el id del equipo seleccionado.
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_EditorEquipos()
        {
            InitializeComponent();
        }

        private void editorEquipos_Loaded(object sender, RoutedEventArgs e)
        {
            Conexion.EstablecerConexionPartida("./championsManagerDB.db");
            string ruta_competicion_principal = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen;
            string ruta_competicion_dos = _logicaCompeticion.ObtenerCompeticion(2).RutaImagen;
            string ruta_competicion_reserva = _logicaCompeticion.ObtenerCompeticion(3).RutaImagen;
            imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_principal));
            imgCompeticion2.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_dos));
            imgCompeticion3.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_competicion_reserva));

            CargarEscudos(1);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorHome();
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION PRINCIPAL
        private void imgCompeticion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            btnEditarEquipo.IsEnabled = false;

            imgCompeticion.IsEnabled = false;
            imgCompeticion2.IsEnabled = true;
            imgCompeticion3.IsEnabled = true;

            CargarEscudos(1);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION 2
        private void imgCompeticionReserva_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            btnEditarEquipo.IsEnabled = false;

            imgCompeticion.IsEnabled = true;
            imgCompeticion2.IsEnabled = false;
            imgCompeticion3.IsEnabled = true;

            CargarEscudos(2);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton COMPETICION RESERVA
        private void imgCompeticion3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            btnEditarEquipo.IsEnabled = false;

            imgCompeticion.IsEnabled = true;
            imgCompeticion2.IsEnabled = true;
            imgCompeticion3.IsEnabled = false;

            CargarEscudos(3);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton EDITAR EQUIPO
        private void btnEditarEquipo_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorClub(equipoSeleccionado);
        }

        #region "Métodos"
        private void CargarEscudos(int competicion)
        {
            // Limpiar el WrapPanel por si ya tiene escudos
            wrapPanelEquipos.Children.Clear();

            // Recogemos la lista de equipo.
            List<Equipo> oEquipos = new List<Equipo>();
            oEquipos = _logicaEquipo.ListarEquiposCompeticion(competicion);

            // Variable para rastrear el Border seleccionado
            Border? contenedorSeleccionado = null;

            // Recorrer los datos para crear las imágenes
            foreach (var equipo in oEquipos)
            {
                // Ruta de la imagen usando el id_imagen
                string imagePath = $"{GestorPartidas.RutaMisDocumentos}/{equipo.RutaImagen120}";

                // Crear la imagen
                Image img = new Image
                {
                    Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute)),
                    Width = 120,
                    Height = 120,
                    Cursor = Cursors.Hand // Cursor de mano
                };

                // Crear el contenedor Border
                Border contenedor = new Border
                {
                    Child = img,
                    Width = 130, // Ajustado al ItemWidth del WrapPanel
                    Height = 130, // Ajustado al ItemHeight del WrapPanel
                    Margin = new Thickness(0),
                    BorderThickness = new Thickness(0),
                    BorderBrush = Brushes.Transparent,
                    Tag = equipo.IdEquipo // Guardar el IdEquipo en Tag
                };

                // Asignar un evento de clic al Border
                contenedor.MouseLeftButtonDown += (s, e) =>
                {
                    Metodos.ReproducirSonidoClick();

                    // Eliminar el borde verde del contenedor previamente seleccionado, si existe
                    if (contenedorSeleccionado is not null)
                    {
                        contenedorSeleccionado.BorderThickness = new Thickness(0);
                        contenedorSeleccionado.BorderBrush = Brushes.Transparent;
                        contenedorSeleccionado.Background = Brushes.Transparent;
                    }

                    // Establecer borde verde en el contenedor actual
                    contenedorSeleccionado = contenedor;
                    contenedor.BorderThickness = new Thickness(2);
                    contenedor.BorderBrush = new BrushConverter().ConvertFromString("#9b8b5a") as Brush;
                    contenedor.Background = new BrushConverter().ConvertFromString("#CCCCCC") as Brush;

                    // Mostrar el nombre del equipo en el botón btnVerDetalles
                    btnEditarEquipo.Content = $"EDITAR AL {equipo.Nombre.ToUpper()}";
                    equipoSeleccionado = equipo.IdEquipo;

                    btnEditarEquipo.IsEnabled = true;
                };

                // Agregar el contenedor al WrapPanel
                wrapPanelEquipos.Children.Add(contenedor);
            }
        }
        #endregion

    }
}
