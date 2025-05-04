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

        int patrocinadorPrincipalSeleccionado = 0;
        int[]? patrocinadorPrincipal;
        int[]? cantidadesPrincipal;
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
            GenerarPatrocinadoresPrincipales(reputacionEquipo, listaPatrocinadores, out patrocinadorPrincipal, out cantidadesPrincipal, out duracionesPrincipal);

            // Cargar datos en la ventana
            imgPatrocinadorPrincipal1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[0] + ".png"));
            imgPatrocinadorPrincipal2.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[1] + ".png"));
            imgPatrocinadorPrincipal3.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinadorPrincipal[2] + ".png"));

            txtCantidadPatrocinadorPrincipal1.Text = cantidadesPrincipal[0].ToString("N0") + " $";
            txtCantidadPatrocinadorPrincipal2.Text = cantidadesPrincipal[1].ToString("N0") + " $";
            txtCantidadPatrocinadorPrincipal3.Text = cantidadesPrincipal[2].ToString("N0") + " $";

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
            _logicaPatrocinador.AnadirUnPatrocinador(patrocinadorPrincipal[patrocinadorPrincipalSeleccionado - 1], cantidadesPrincipal[patrocinadorPrincipalSeleccionado - 1], duracionesPrincipal[patrocinadorPrincipalSeleccionado - 1], _equipo, _manager.IdManager);

            // Crear el mensaje con el numero de abonados de la temporada
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;
            string nombrePatrocinador = _logicaPatrocinador.NombrePatrocinador(patrocinadorPrincipal[patrocinadorPrincipalSeleccionado - 1]);

            Mensaje mensajePatrocinador = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Nuevo Acuerdo de Patrocinio Cerrado",
                Contenido = $"Me complace anunciar que hemos alcanzado un acuerdo con un nuevo patrocinador: {nombrePatrocinador}. Este contrato nos aportará {cantidadesPrincipal[patrocinadorPrincipalSeleccionado - 1].ToString("N0", new CultureInfo("es-ES"))}€ por temporada durante {duracionesPrincipal[patrocinadorPrincipalSeleccionado - 1]} años y refleja el creciente interés comercial en nuestro proyecto deportivo.\n\nEste tipo de alianzas son fundamentales para mejorar nuestra situación financiera y nos permitirán tener mayor margen de maniobra en futuras operaciones.\n\nFelicidades por la imagen que estás proyectando del club. ¡Sigamos creciendo juntos!",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajePatrocinador);
            this.Close();
        }

        #region "Métodos"
        public void GenerarPatrocinadoresPrincipales(int reputacionEquipo, List<Patrocinador> listaPatrocinadores,
                          out int[] patrocinadorPrincipal, out int[] cantidadesPrincipal, out int[] duracionesPrincipal)
        {
            // Determinar el límite de reputación de patrocinadores basado en la reputación del equipo
            int limiteReputacion;
            if (reputacionEquipo >= 50 && reputacionEquipo <= 60)
            {
                limiteReputacion = 1;
            }
            else if (reputacionEquipo >= 61 && reputacionEquipo <= 70)
            {
                limiteReputacion = 2;
            }
            else if (reputacionEquipo >= 71 && reputacionEquipo <= 80)
            {
                limiteReputacion = 3;
            }
            else if (reputacionEquipo >= 81 && reputacionEquipo <= 90)
            {
                limiteReputacion = 4;
            }
            else if (reputacionEquipo >= 91 && reputacionEquipo <= 100)
            {
                limiteReputacion = 5;
            }
            else
            {
                limiteReputacion = 0; // No aplica selección si no está dentro de los rangos
            }

            // Filtrar patrocinadores según el límite de reputación
            var patrocinadoresFiltrados = listaPatrocinadores
            .Where(p => p.Reputacion <= limiteReputacion)
            .ToList();

            // Barajar los patrocinadores para obtener una selección aleatoria
            Random random = new Random();
            var seleccionAleatoria = patrocinadoresFiltrados
                .OrderBy(x => random.Next())
                .Take(3) // Tomar hasta 3 patrocinadores
                .ToList();

            // Inicializar los arrays
            patrocinadorPrincipal = seleccionAleatoria.Select(p => p.IdPatrocinador).ToArray();
            cantidadesPrincipal = seleccionAleatoria
                .Select(p => random.Next(20, 51) * 500000) // Generar valores aleatorios entre 20M y 50M, múltiplos de 500K
                .ToArray();
            duracionesPrincipal = seleccionAleatoria
                .Select(p => random.Next(1, 4)) // Generar valores aleatorios entre 1 y 3 años
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
