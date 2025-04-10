using System;
using System.IO;
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
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using ChampionManager25.Datos;

namespace ChampionManager25.UserControls
{
    public partial class UC_SeleccionEquipo : UserControl
    {

        #region "Variables"
        private readonly Manager _manager;
        private readonly string _rutaPartida;

        int equipoSeleccionado = 0; // Variable que recoge el id del equipo seleccionado.
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_SeleccionEquipo(Manager manager, string rutaPartida)
        {
            InitializeComponent();
            _manager = manager;
            _rutaPartida = rutaPartida;
        }

        private void seleccionEquipo_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen;
            imgLiga1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
            CargarEscudos(1);
        }

        // -------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _logicaManager.eliminarManager(_manager.IdManager);
            Metodos.ReproducirSonidoTransicion();
            
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarCrearManager();
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON AVANZAR
        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Añadir el equipo en la base de datos.
            _logicaManager.AnadirEquipoSeleccionado(_manager.IdManager, equipoSeleccionado);

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPretemporada(_manager, equipoSeleccionado);
        }

        // ----------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON CHAMPIONS
        private void imgLiga1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            CargarEscudos(1);
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON VER DETALLES
        private void btnVerDetalles_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
   
            frmVerDetallesEquipo detallesEquipoWindow = new frmVerDetallesEquipo(equipoSeleccionado); 
            detallesEquipoWindow.ShowDialog();
        }
        // --------------------------------------------------------------------------------------------------------------------------------

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
                    btnVerDetalles.Content = $"Ver Detalles del {equipo.Nombre}";
                    equipoSeleccionado = equipo.IdEquipo;

                    btnAvanzar.IsEnabled = true;
                    btnVerDetalles.IsEnabled = true;
                };

                // Agregar el contenedor al WrapPanel
                wrapPanelEquipos.Children.Add(contenedor);
            }
        }
        #endregion
    }
}
