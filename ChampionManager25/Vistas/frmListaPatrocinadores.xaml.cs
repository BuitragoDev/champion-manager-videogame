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
    public partial class frmListaPatrocinadores : Window
    {
        private Manager _manager;
        private int _equipo;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        PatrocinadorLogica _logicaPatrocinador = new PatrocinadorLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();

        int patrocinadorPrincipalSeleccionado = 0;
        int[]? patrocinadorPrincipal;
        int[]? cantidadesPrincipal;
        int[]? mensualidadPrincipal;
        int[]? duracionesPrincipal;

        public frmListaPatrocinadores(int equipo, Manager manager)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void VerPatrocinadoresWindow_Loaded(object sender, RoutedEventArgs e)
        {
            int reputacionEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;

            List<Patrocinador> listaPatrocinadores = _logicaPatrocinador.MostrarListaPatrocinadores();

            // Generar lista de Patrocinadores Principales
            GenerarPatrocinadoresPrincipales(reputacionEquipo, listaPatrocinadores, out patrocinadorPrincipal, out cantidadesPrincipal, out mensualidadPrincipal, out duracionesPrincipal);

            // Cargar datos en la ventana
            imgPatrocinadorPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[0] + ".png"));
            imgPatrocinadorPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[1] + ".png"));
            imgPatrocinadorPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[2] + ".png"));

            txtCantidadPatrocinadorPrincipal1.Text = cantidadesPrincipal[0].ToString("N0") + " €";
            txtCantidadPatrocinadorPrincipal2.Text = cantidadesPrincipal[1].ToString("N0") + " €";
            txtCantidadPatrocinadorPrincipal3.Text = cantidadesPrincipal[2].ToString("N0") + " €";

            txtMensualidadPatrocinadorPrincipal1.Text = mensualidadPrincipal[0].ToString("N0") + " €";
            txtMensualidadPatrocinadorPrincipal2.Text = mensualidadPrincipal[1].ToString("N0") + " €";
            txtMensualidadPatrocinadorPrincipal3.Text = mensualidadPrincipal[2].ToString("N0") + " €";

            if (duracionesPrincipal[0] == 1)
            {
                txtDuracionPatrocinadorPrincipal1.Text = duracionesPrincipal[0].ToString() + " año";
            }
            else
            {
                txtDuracionPatrocinadorPrincipal1.Text = duracionesPrincipal[0].ToString() + " años";
            }

            if (duracionesPrincipal[1] == 1)
            {
                txtDuracionPatrocinadorPrincipal2.Text = duracionesPrincipal[1].ToString() + " año";
            }
            else
            {
                txtDuracionPatrocinadorPrincipal2.Text = duracionesPrincipal[1].ToString() + " años";
            }

            if (duracionesPrincipal[2] == 1)
            {
                txtDuracionPatrocinadorPrincipal3.Text = duracionesPrincipal[2].ToString() + " año";
            }
            else
            {
                txtDuracionPatrocinadorPrincipal3.Text = duracionesPrincipal[2].ToString() + " años";
            }
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del botón CERRAR
        private void imgCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        }

        // --------------------------------------------------------------------- Evento CLICK de los botones de selección PATROCINADORES PRINCIPALES
        private void imgSeleccionPatrocinadorPrincipal1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionPatrocinadorPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            patrocinadorPrincipalSeleccionado = 1;

            visibilidadBoton();
        }

        private void imgSeleccionPatrocinadorPrincipal2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionPatrocinadorPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            patrocinadorPrincipalSeleccionado = 2;

            visibilidadBoton();
        }

        private void imgSeleccionPatrocinadorPrincipal3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgSeleccionPatrocinadorPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/seleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            imgSeleccionPatrocinadorPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/noSeleccionado_icon.png"));
            patrocinadorPrincipalSeleccionado = 3;

            visibilidadBoton();
        }

        // ------------------------------------------------------------------------------------- Evento CLICK del botón CONFIRMAR PATROCINADORES
        private void btnConfirmarPatrocinadores_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            _logicaPatrocinador.AnadirUnPatrocinador(patrocinadorPrincipal[patrocinadorPrincipalSeleccionado - 1], cantidadesPrincipal[patrocinadorPrincipalSeleccionado - 1], mensualidadPrincipal[patrocinadorPrincipalSeleccionado - 1], duracionesPrincipal[patrocinadorPrincipalSeleccionado - 1], _equipo, _manager.IdManager);

            // Crear el mensaje con el numero de abonados de la temporada
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
            string nombrePatrocinador = _logicaPatrocinador.NombrePatrocinador(patrocinadorPrincipal[patrocinadorPrincipalSeleccionado - 1]);

            Mensaje mensajePatrocinador = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Nuevo Acuerdo de Patrocinio Cerrado",
                Contenido = $"Me complace anunciar que hemos alcanzado un acuerdo con un nuevo patrocinador: {nombrePatrocinador}. Este contrato nos aportará un pago inicial de {cantidadesPrincipal[patrocinadorPrincipalSeleccionado - 1].ToString("N0", new CultureInfo("es-ES"))}€ y un pago mensual de {mensualidadPrincipal[patrocinadorPrincipalSeleccionado - 1].ToString("N0", new CultureInfo("es-ES"))}€ por temporada durante {duracionesPrincipal[patrocinadorPrincipalSeleccionado - 1]} años y refleja el creciente interés comercial en nuestro proyecto deportivo.\n\nEste tipo de alianzas son fundamentales para mejorar nuestra situación financiera y nos permitirán tener mayor margen de maniobra en futuras operaciones.\n\nFelicidades por la imagen que estás proyectando del club. ¡Sigamos creciendo juntos!",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajePatrocinador);

            // Crear ingreso del pago inicial del patrocinador
            int pagoPatrocinador = cantidadesPrincipal[patrocinadorPrincipalSeleccionado - 1];
            Finanza nuevoIngresoPatrocinio = new Finanza
            {
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Temporada = Metodos.temporadaActual.ToString(),
                IdConcepto = 6,
                Tipo = 1,
                Cantidad = pagoPatrocinador,
                Fecha = Metodos.hoy.Date
            };
            _logicaFinanza.CrearIngreso(nuevoIngresoPatrocinio);

            // Restar la indemnización al Presupuesto
            _logicaEquipo.SumarCantidadAPresupuesto(_equipo, pagoPatrocinador);

            this.Close();
        }

        #region "Métodos"
        public void GenerarPatrocinadoresPrincipales(int reputacionEquipo, List<Patrocinador> listaPatrocinadores,
                  out int[] patrocinadorPrincipal, out int[] cantidadesPrincipal, out int[] mensualidadPrincipal, out int[] duracionesPrincipal)
        {
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

            // Filtrar patrocinadores según el límite de reputación
            var patrocinadoresFiltrados = listaPatrocinadores
                .Where(p => p.Reputacion <= limiteReputacion)
                .ToList();

            // Barajar los patrocinadores para obtener una selección aleatoria
            Random random = new Random();
            var seleccionAleatoria = patrocinadoresFiltrados
                .OrderBy(x => random.Next())
                .Take(3)
                .ToList();

            // Inicializar arrays
            patrocinadorPrincipal = seleccionAleatoria.Select(p => p.IdPatrocinador).ToArray();
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
            if (patrocinadorPrincipalSeleccionado == 0)
            {
                btnConfirmarPatrocinadores.Visibility = Visibility.Hidden;
            }
            else
            {
                btnConfirmarPatrocinadores.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
