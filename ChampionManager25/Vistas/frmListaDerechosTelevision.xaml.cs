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
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmListaDerechosTelevision : Window
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        TelevisionLogica _logicaTelevision = new TelevisionLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();

        int cadenaTVSeleccionada = 0;
        int[]? cadenatvPrincipal;
        int[]? cantidadesPrincipal;
        int[]? mensualidadPrincipal;
        int[]? duracionesPrincipal;

        public frmListaDerechosTelevision(int equipo, Manager manager)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void VerDerechosTVWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int reputacionEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;

            List<Television> listaTelevisiones = _logicaTelevision.MostrarListaCadenasTV();

            // Generar lista de Patrocinadores Principales
            GenerarCadenasTVPrincipales(reputacionEquipo, listaTelevisiones, out cadenatvPrincipal, out cantidadesPrincipal, out mensualidadPrincipal, out duracionesPrincipal);

            // Cargar datos en la ventana
            imgTelevisionPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/cadenaTV/" + cadenatvPrincipal[0] + ".png"));
            imgTelevisionPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/cadenaTV/" + cadenatvPrincipal[1] + ".png"));
            imgTelevisionPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/cadenaTV/" + cadenatvPrincipal[2] + ".png"));

            txtCantidadTelevisionPrincipal1.Text = cantidadesPrincipal[0].ToString("N0") + " €";
            txtCantidadTelevisionPrincipal2.Text = cantidadesPrincipal[1].ToString("N0") + " €";
            txtCantidadTelevisionPrincipal3.Text = cantidadesPrincipal[2].ToString("N0") + " €";

            txtMensualidadTelevisionPrincipal1.Text = mensualidadPrincipal[0].ToString("N0") + " €";
            txtMensualidadTelevisionPrincipal2.Text = mensualidadPrincipal[1].ToString("N0") + " €";
            txtMensualidadTelevisionPrincipal3.Text = mensualidadPrincipal[2].ToString("N0") + " €";

            if (duracionesPrincipal[0] == 1)
            {
                txtDuracionTelevisionPrincipal1.Text = duracionesPrincipal[0].ToString() + " año";
            }
            else
            {
                txtDuracionTelevisionPrincipal1.Text = duracionesPrincipal[0].ToString() + " años";
            }

            if (duracionesPrincipal[1] == 1)
            {
                txtDuracionTelevisionPrincipal2.Text = duracionesPrincipal[1].ToString() + " año";
            }
            else
            {
                txtDuracionTelevisionPrincipal2.Text = duracionesPrincipal[1].ToString() + " años";
            }

            if (duracionesPrincipal[2] == 1)
            {
                txtDuracionTelevisionPrincipal3.Text = duracionesPrincipal[2].ToString() + " año";
            }
            else
            {
                txtDuracionTelevisionPrincipal3.Text = duracionesPrincipal[2].ToString() + " años";
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del botón CERRAR
        private void imgCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        }

        // --------------------------------------------------------------------- Evento CLICK de los botones de selección TELEVISIONES PRINCIPALES
        private void imgSeleccionTelevisionPrincipal1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionTelevisionPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            cadenaTVSeleccionada = 1;

            visibilidadBoton();
        }

        private void imgSeleccionTelevisionPrincipal2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionTelevisionPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            cadenaTVSeleccionada = 2;

            visibilidadBoton();
        }

        private void imgSeleccionTelevisionPrincipal3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionTelevisionPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionTelevisionPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            cadenaTVSeleccionada = 3;

            visibilidadBoton();
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del botón CONFIRMAR TELEVISIONES
        private void btnConfirmarTelevisiones_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            _logicaTelevision.AnadirUnaCadenaTV(cadenatvPrincipal[cadenaTVSeleccionada - 1], cantidadesPrincipal[cadenaTVSeleccionada - 1], mensualidadPrincipal[cadenaTVSeleccionada - 1], duracionesPrincipal[cadenaTVSeleccionada - 1], _equipo, _manager.IdManager);

            // Crear el mensaje con el numero de abonados de la temporada
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
            string nombreCadena = _logicaTelevision.NombreCadenaTV(cadenatvPrincipal[cadenaTVSeleccionada - 1]);

            Mensaje mensajeCadena = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Acuerdo de Derechos Televisivos Cerrado",
                Contenido = $"¡Grandes noticias para el club! Hemos cerrado un acuerdo con la cadena {nombreCadena} para la retransmisión de nuestros partidos. Este contrato nos reportará un pago inicial de {cantidadesPrincipal[cadenaTVSeleccionada - 1].ToString("N0", new CultureInfo("es-ES"))}€ y un pago mensual de {mensualidadPrincipal[cadenaTVSeleccionada - 1].ToString("N0", new CultureInfo("es-ES"))}€ por temporada durante {duracionesPrincipal[cadenaTVSeleccionada - 1]} años y aumentará notablemente nuestra visibilidad a nivel nacional e internacional.\r\n\r\nEs un paso importante que no solo mejorará nuestras finanzas, sino también la imagen del club. El creciente interés mediático es un reflejo directo del buen trabajo que estás realizando.",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajeCadena);

            // Crear ingreso del pago inicial de la television
            int pagoTelevision = cantidadesPrincipal[cadenaTVSeleccionada - 1];
            Finanza nuevoIngresoTelevision = new Finanza
            {
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Temporada = Metodos.temporadaActual.ToString(),
                IdConcepto = 7,
                Tipo = 1,
                Cantidad = pagoTelevision,
                Fecha = Metodos.hoy.Date
            };
            _logicaFinanza.CrearIngreso(nuevoIngresoTelevision);

            // Restar la indemnización al Presupuesto
            _logicaEquipo.SumarCantidadAPresupuesto(_equipo, pagoTelevision);

            this.Close();
        }

        #region "Métodos"
        public void GenerarCadenasTVPrincipales(int reputacionEquipo, List<Television> listaTelevisiones,
                          out int[] cadenatvPrincipal, out int[] cantidadesPrincipal, out int[] mensualidadPrincipal, out int[] duracionesPrincipal)
        {
            // Determinar el límite de reputación de los derechos de TV basado en la reputación del equipo
            int limiteReputacion;
            int cantidadMin = 20; // en millones
            int cantidadMax = 50;
            int mensualidadMin = 200000;
            int mensualidadMax = 1000000;

            if (reputacionEquipo >= 50 && reputacionEquipo <= 60)
            {
                limiteReputacion = 1;
                cantidadMin = 10;
                cantidadMax = 25;
                mensualidadMin = 200000;
                mensualidadMax = 500000;
            }
            else if (reputacionEquipo >= 61 && reputacionEquipo <= 70)
            {
                limiteReputacion = 2;
                cantidadMin = 15;
                cantidadMax = 30;
                mensualidadMin = 300000;
                mensualidadMax = 600000;
            }
            else if (reputacionEquipo >= 71 && reputacionEquipo <= 80)
            {
                limiteReputacion = 3;
                cantidadMin = 20;
                cantidadMax = 40;
                mensualidadMin = 400000;
                mensualidadMax = 800000;
            }
            else if (reputacionEquipo >= 81 && reputacionEquipo <= 90)
            {
                limiteReputacion = 4;
                cantidadMin = 25;
                cantidadMax = 45;
                mensualidadMin = 500000;
                mensualidadMax = 900000;
            }
            else if (reputacionEquipo >= 91 && reputacionEquipo <= 100)
            {
                limiteReputacion = 5;
                cantidadMin = 30;
                cantidadMax = 50;
                mensualidadMin = 600000;
                mensualidadMax = 1000000;
            }
            else
            {
                limiteReputacion = 0;
                cantidadMin = 5;
                cantidadMax = 15;
                mensualidadMin = 50000;
                mensualidadMax = 100000;
            }

            // Filtrar cadenasTV según el límite de reputación
            var cadenasTVFiltrados = listaTelevisiones
                .Where(p => p.Reputacion <= limiteReputacion)
                .ToList();

            // Barajar los cadenasTV para obtener una selección aleatoria
            Random random = new Random();
            var seleccionAleatoria = cadenasTVFiltrados
                .OrderBy(x => random.Next())
                .Take(3)
                .ToList();

            // Inicializar arrays
            cadenatvPrincipal = seleccionAleatoria.Select(p => p.IdTelevision).ToArray();
            cantidadesPrincipal = seleccionAleatoria
                .Select(p => random.Next(cantidadMin, cantidadMax + 1) * 500000)
                .ToArray();
            mensualidadPrincipal = seleccionAleatoria
                .Select(p => random.Next(mensualidadMin / 50000, mensualidadMax / 50000 + 1) * 50000)
                .ToArray();
            duracionesPrincipal = seleccionAleatoria
                .Select(p => random.Next(1, 4))
                .ToArray();
        }

        private void visibilidadBoton()
        {
            if (cadenaTVSeleccionada == 0)
            {
                btnConfirmarTelevisiones.Visibility = Visibility.Hidden;
            }
            else
            {
                btnConfirmarTelevisiones.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
