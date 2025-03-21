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
using System.Windows.Shapes;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.UserControls;

namespace ChampionManager25.Vistas
{
    public partial class frmVerDetallesEquipo : Window
    {
        #region "Variables"
        public int idEquipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        Equipo equipo = new Equipo();

        public frmVerDetallesEquipo(int idEquipo)
        {
            InitializeComponent();
            this.idEquipo = idEquipo;
        }

        private void VerDetallesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Obtenemos el objeto Equipo.
            equipo = _logicaEquipo.ListarDetallesEquipo(idEquipo);

            // Escudo del equipo en la Barra Superior
            string escudoPath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
            BitmapImage bitmapEscudo = new BitmapImage(new Uri(escudoPath, UriKind.Relative));
            imgEscudoEquipo.Source = bitmapEscudo;

            // Cargamos el contenido.
            lblTituloVentana.Text = equipo.Nombre;

            // Subrayamos la opción activa.
            lblInformacion.Foreground = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            lblInformacion.IsEnabled = false;

            // Cargar el UserControl de la Información de Equipo
            UC_InformacionEquipo userControl = new UC_InformacionEquipo(equipo)
            {
                HorizontalAlignment = HorizontalAlignment.Left, // Opcional: Ajusta según el diseño
                VerticalAlignment = VerticalAlignment.Top,      // Opcional: Ajusta según el diseño
                Width = 1440,  // Opcional: Ajusta si deseas tamaño fijo
                Height = 800   // Opcional: Ajusta si deseas tamaño fijo
            };
            panelContenido.Children.Clear();
            panelContenido.Children.Add(userControl);
        }

        // -------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON CERRAR
        private void imgBotonCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // --------------------------------------------------------------------------------------------- EVENTO CLICK DEL LABEL INFORMACION
        private void lblInformacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            lblInformacion.Foreground = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            lblPlantilla.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));
            lblPalmares.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));

            lblInformacion.IsEnabled = false;
            lblPlantilla.IsEnabled = true;
            lblPalmares.IsEnabled = true;

            // Cargar el UserControl de la Información de Equipo
            UC_InformacionEquipo userControl = new UC_InformacionEquipo(equipo)
            {
                HorizontalAlignment = HorizontalAlignment.Left, // Opcional: Ajusta según el diseño
                VerticalAlignment = VerticalAlignment.Top,      // Opcional: Ajusta según el diseño
                Width = 1440,  // Opcional: Ajusta si deseas tamaño fijo
                Height = 800   // Opcional: Ajusta si deseas tamaño fijo
            };
            panelContenido.Children.Clear();
            panelContenido.Children.Add(userControl);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------ EVENTO CLICK DEL LABEL PLANTILLA
        private void lblPlantilla_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            lblPlantilla.Foreground = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            lblInformacion.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));
            lblPalmares.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));

            lblPlantilla.IsEnabled = false;
            lblInformacion.IsEnabled = true;
            lblPalmares.IsEnabled = true;

            // Cargar el UserControl de la Plantilla de Equipo
            UC_InformacionPlantilla userControlPlantilla = new UC_InformacionPlantilla(equipo)
            {
                HorizontalAlignment = HorizontalAlignment.Left, // Opcional: Ajusta según el diseño
                VerticalAlignment = VerticalAlignment.Top,      // Opcional: Ajusta según el diseño
                Width = 1440,  // Opcional: Ajusta si deseas tamaño fijo
                Height = 800   // Opcional: Ajusta si deseas tamaño fijo
            };
            panelContenido.Children.Clear();
            panelContenido.Children.Add(userControlPlantilla);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------- EVENTO CLICK DEL LABEL PALMARES
        private void lblPalmares_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            lblPalmares.Foreground = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            lblPlantilla.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));
            lblInformacion.Foreground = new SolidColorBrush(Color.FromRgb(0x23, 0x28, 0x2D));

            lblPalmares.IsEnabled = false;
            lblPlantilla.IsEnabled = true;
            lblInformacion.IsEnabled = true;

            // Cargar el UserControl del Palmarés de Equipo
            UC_InformacionPalmares userControlPalmares = new UC_InformacionPalmares(equipo)
            {
                HorizontalAlignment = HorizontalAlignment.Left, // Opcional: Ajusta según el diseño
                VerticalAlignment = VerticalAlignment.Top,      // Opcional: Ajusta según el diseño
                Width = 1440,  // Opcional: Ajusta si deseas tamaño fijo
                Height = 800   // Opcional: Ajusta si deseas tamaño fijo
            };
            panelContenido.Children.Clear();
            panelContenido.Children.Add(userControlPalmares);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------
    }
}
