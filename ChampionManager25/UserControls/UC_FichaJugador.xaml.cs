using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class UC_FichaJugador : UserControl
    {
        #region "Variables"
        private int _jugador;
        private int _equipo;
        private int _opcion; // Variable que se usa para saber si el jugador es de mi equipo o es de otro.
        private Manager _manager;
        private UC_PantallaPrincipal _pantallaPrincipal;
        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensaje = new MensajeLogica();
        ManagerLogica _logicaManager = new ManagerLogica();

        public UC_FichaJugador(int jugador, int equipo, Manager manager, int opcion, UC_PantallaPrincipal pantallaPrincipal)
        {
            InitializeComponent();
            _jugador = jugador;
            _equipo = equipo;
            _manager = manager;
            _opcion = opcion;
            _pantallaPrincipal = pantallaPrincipal;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);

            CargarFichaJugador();
        }

        // --------------------------------------------------------------- Evento CLICK del BOTÓN VOLVER
        private void btnVolverPlantilla_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Obtener el DockPanel padre
            DockPanel? parentDockPanel = this.Parent as DockPanel;

            if (parentDockPanel != null)
            {
                // Limpiar el contenido actual
                parentDockPanel.Children.Clear();

                UC_Menu_Club_Plantilla menuPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
                parentDockPanel.Children.Add(menuPlantilla);
            }
        }
        // -----------------------------------------------------------------------------------------------

        #region "Métodos"
        private SolidColorBrush DeterminarColorElipse(int puntos)
        {
            if (puntos is int mediaValue)
            {
                if (mediaValue > 90)
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2BC513")); // Verde claro
                if (mediaValue >= 80)
                    return Brushes.DarkGreen; // Verde oscuro
                if (mediaValue >= 65)
                    return Brushes.Orange; // Naranja
                return Brushes.Red; // Rojo
            }
            return Brushes.Black; // Por defecto
        }

        private void CargarFichaJugador()
        {
            Jugador jugador = _logicaJugador.MostrarDatosJugador(_jugador);

            // Cargar Datos del Jugador
            txtDorsal.Text = jugador.Dorsal.ToString();
            txtNombreJugador.Text = jugador.NombreCompleto;
            imgFotoJugador.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/jugadores/" + jugador.IdJugador + ".png"));
            imgEscudoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/120x120/" + jugador.IdEquipo + ".png"));
            lblAverage.Text = jugador.Media.ToString();
            elipseMedia.Stroke = DeterminarColorElipse(jugador.Media);

            txtNombreEquipo.Text = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Nombre;
            txtPosicion.Text = jugador.Rol;
            txtAltura.Text = jugador.AlturaEnMetros.ToString("0.00 'm'");
            txtPeso.Text = jugador.Peso.ToString() + " kg";
            txtEdad.Text = jugador.Edad.ToString() + " años (" + jugador.FechaNacimiento.ToString("dd/MM/yyyy") + ")";
            txtNacionalidad.Text = jugador.Nacionalidad;

            // Posición en el campo.
            int posicion = jugador.RolId;
            imgCoordenadas.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/demarcaciones/" + posicion + ".png"));

            // Estado Actual
            txtCondicionFisica.Text = jugador.EstadoForma.ToString();

            int moral = jugador.Moral;
            if (moral >= 70)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/arriba_icon.png"));
            }
            else if (moral >= 35)
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/derecha_icon.png"));
            }
            else
            {
                imgMoral.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/abajo_icon.png"));
            }

            int lesionado = jugador.Lesion;
            if (lesionado > 0)
            {
                imgLesionado.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/lesion.png"));
            }
            else
            {
                imgLesionado.Source = null;
            }

            int status = jugador.Status;
            if (status == 1)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol1_icon.png"));
            }
            else if (status == 2)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol2_icon.png"));
            }
            else if (status == 3)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol3_icon.png"));
            }
            else if (status == 4)
            {
                imgStatus.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/rol4_icon.png"));
            }

            // Atributos
            txtVelocidad.Text = jugador.Velocidad.ToString();
            txtResistencia.Text = jugador.Resistencia.ToString();
            txtAgresividad.Text = jugador.Agresividad.ToString();
            txtCalidad.Text = jugador.Calidad.ToString();
            txtPotencial.Text = jugador.Potencial.ToString();

            txtPortero.Text = jugador.Portero.ToString();
            txtPase.Text = jugador.Pase.ToString();
            txtRegate.Text = jugador.Regate.ToString();
            txtRemate.Text = jugador.Remate.ToString();
            txtEntradas.Text = jugador.Entradas.ToString();
            txtTiro.Text = jugador.Tiro.ToString();
        }
        #endregion
    }
}
