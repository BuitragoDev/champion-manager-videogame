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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Estadio_Ampliaciones : System.Windows.Controls.UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        Equipo miEquipo;
        private int tipoRemodelacion = 0;
        int aforo;
        #endregion

        // Instancias de la LOGICA
        RemodelacionLogica _logicaRemodelacion = new RemodelacionLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        public UC_Menu_Estadio_Ampliaciones(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            aforo = _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo;
        }

        private void UCAmpliaciones_Loaded(object sender, RoutedEventArgs e)
        {
            
            imgLogo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));
            txtNombreEstadio.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Estadio + " (" + _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo.ToString("N0") + " asientos)";

            Remodelacion obraActiva = _logicaRemodelacion.ComprobarRemodelacion(_equipo, _manager.IdManager);

            if (obraActiva != null)
            {
                imgSeleccionObra1.Visibility = Visibility.Hidden;
                imgSeleccionObra2.Visibility = Visibility.Hidden;
                imgSeleccionObra3.Visibility = Visibility.Hidden;
                btnEmpezarObras.Visibility = Visibility.Hidden;

                borderBarraTitulo.Background = new SolidColorBrush(Colors.DarkRed);
                TimeSpan semanasRestantes = obraActiva.FechaFinal - Metodos.hoy;
                int semanas = (int)Math.Ceiling(semanasRestantes.TotalDays / 7);
                txtTitulo.Text = "LAS OBRAS FINALIZARÁN EN " + semanas.ToString() + " SEMANAS";

                if (obraActiva.TipoRemodelacion == 1)
                {
                    imgExcavadora1.Visibility = Visibility.Visible;
                }
                else if (obraActiva.TipoRemodelacion == 2)
                {
                    imgExcavadora2.Visibility = Visibility.Visible;
                }
                else if (obraActiva.TipoRemodelacion == 3)
                {
                    imgExcavadora3.Visibility = Visibility.Visible;
                }

                CargarImagenConstruccion(aforo);
            }
            else
            {
                imgSeleccionObra1.Visibility = Visibility.Visible;
                imgSeleccionObra2.Visibility = Visibility.Visible;
                imgSeleccionObra3.Visibility = Visibility.Visible;

                imgExcavadora1.Visibility = Visibility.Hidden;
                imgExcavadora2.Visibility = Visibility.Hidden;
                imgExcavadora3.Visibility = Visibility.Hidden;

                if (aforo >= 75000)
                {
                    imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/large.png"));
                }
                else if (aforo >= 25000)
                {
                    imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/medium.png"));
                }
                else
                {
                    imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/small.png"));
                }
            }
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón SELECCION OBRA 1
        private void imgSeleccionObra1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgSeleccionObra1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionObra2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionObra3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            btnEmpezarObras.Visibility = Visibility.Visible;
            tipoRemodelacion = 1;
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón SELECCION OBRA 2
        private void imgSeleccionObra2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgSeleccionObra2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionObra1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionObra3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            btnEmpezarObras.Visibility = Visibility.Visible;
            tipoRemodelacion = 2;
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón SELECCION OBRA 3
        private void imgSeleccionObra3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            imgSeleccionObra3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionObra2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionObra1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            btnEmpezarObras.Visibility = Visibility.Visible;
            tipoRemodelacion = 3;
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón EMPEZAR OBRAS 
        private void btnEmpezarObras_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            int aumento = 0;
            int semanasObras = 0;
            if (tipoRemodelacion != 0)
            {
                DateTime fechaFinal;
                if (tipoRemodelacion == 1)
                {
                    aumento = 500;
                    semanasObras = 20;
                    fechaFinal = Metodos.hoy.AddDays(20 * 7); // Fecha actual más 20 semanas
                    _logicaRemodelacion.CrearNuevaRemodelacion(_equipo, _manager.IdManager, fechaFinal, tipoRemodelacion);
                }
                else if (tipoRemodelacion == 2)
                {
                    aumento = 1000;
                    semanasObras = 35;
                    fechaFinal = Metodos.hoy.AddDays(35 * 7); // Fecha actual más 35 semanas
                    _logicaRemodelacion.CrearNuevaRemodelacion(_equipo, _manager.IdManager, fechaFinal, tipoRemodelacion);
                }
                else if (tipoRemodelacion == 3)
                {
                    aumento = 1500;
                    semanasObras = 50;
                    fechaFinal = Metodos.hoy.AddDays(50 * 7); // Fecha actual más 50 semanas
                    _logicaRemodelacion.CrearNuevaRemodelacion(_equipo, _manager.IdManager, fechaFinal, tipoRemodelacion);
                }
            }

            imgSeleccionObra1.Visibility = Visibility.Hidden;
            imgSeleccionObra2.Visibility = Visibility.Hidden;
            imgSeleccionObra3.Visibility = Visibility.Hidden;
            btnEmpezarObras.Visibility = Visibility.Hidden;

            if (tipoRemodelacion == 1)
            {
                imgExcavadora1.Visibility = Visibility.Visible;
            }
            else if (tipoRemodelacion == 2)
            {
                imgExcavadora2.Visibility = Visibility.Visible;
            }
            else if (tipoRemodelacion == 3)
            {
                imgExcavadora3.Visibility = Visibility.Visible;
            }

            Remodelacion obraActiva = _logicaRemodelacion.ComprobarRemodelacion(_equipo, _manager.IdManager);

            if (obraActiva != null)
            {
                borderBarraTitulo.Background = new SolidColorBrush(Colors.DarkRed);
                TimeSpan semanasRestantes = obraActiva.FechaFinal - Metodos.hoy;
                int semanas = (int)Math.Ceiling(semanasRestantes.TotalDays / 7);
                txtTitulo.Text = "LAS OBRAS FINALIZARÁN EN " + semanas.ToString() + " SEMANAS";

                CargarImagenConstruccion(aforo);
            }

            // Creamos el mensaje
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

            Mensaje mensajePrestamo = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Inicio de las Obras de Remodelación del Estadio",
                Contenido = $"Hoy es un día importante para nuestro club. Me complace informarte que han comenzado oficialmente las obras de remodelación de las gradas del estadio. Esta inversión mejorará la comodidad de nuestros aficionados y aumentará la capacidad en {aumento.ToString("N0", new CultureInfo("es-ES"))} asientos, lo que se traducirá en mayores ingresos a futuro.\n\nAunque durante las próximas {semanasObras} semanas podremos experimentar ciertas limitaciones en la asistencia, estamos convencidos de que este paso es fundamental para crecer como institución.",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajePrestamo);
        }

        private void CargarImagenConstruccion(int aforo)
        {
            if (aforo >= 75000)
            {
                imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/large-construction.png"));
            }
            else if (aforo >= 25000)
            {
                imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/medium-construction.png"));
            }
            else
            {
                imgStadium.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/estadios/small-construction.png"));
            }
        }
    }
}
