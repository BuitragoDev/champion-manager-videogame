using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using ChampionManager25.Logica;
using ChampionManager25.Vistas;
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
using ChampionManager25.Datos;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Finanzas_Patrocinadores : UserControl
    {
        private Manager _manager;
        private int _equipo;

        PatrocinadorLogica _logicaPatrocinador = new PatrocinadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_Menu_Finanzas_Patrocinadores(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosPatrocinador();
        }

        // ------------------------------------------------------------------------ Evento CLICK del botón OFERTAS PATROCINADORES
        private void btnOfertasPatrocinadores_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            // Crear una nueva instancia de la ventana de detalles
            frmListaPatrocinadores listaPatrocinadoresWindow = new frmListaPatrocinadores(_equipo, _manager);

            // Suscribirse al evento Closed de la ventana secundaria
            listaPatrocinadoresWindow.Closed += ListaPatrocinadoresWindow_Closed;

            listaPatrocinadoresWindow.ShowDialog();
        }

        #region "Métodos"
        private void ListaPatrocinadoresWindow_Closed(object? sender, EventArgs e)
        {
            // Cuando la ventana emergente se cierra, recargamos los datos en el UserControl
            CargarDatosPatrocinador();
        }

        private void CargarDatosPatrocinador()
        {
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            imgLogo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));

            // Consultar si hay patrocinadores contratados
            Patrocinador patrocinador = _logicaPatrocinador.PatrocinadoresContratados(_manager.IdManager, _equipo);
            if (patrocinador != null)
            {
                txtPatrocinador1.Text = patrocinador.Nombre.ToUpper();
                imgPatrocinador1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/patrocinadores/" + patrocinador.IdPatrocinador + ".png"));
                txtCantidadPatrocinador1.Text = patrocinador.Cantidad.ToString("N0") + " €";
                txtMensualidadPatrocinador1.Text = patrocinador.Mensualidad.ToString("N0") + " €";
                if (patrocinador.DuracionContrato == 1)
                {
                    txtDuracionPatrocinador1.Text = patrocinador.DuracionContrato.ToString() + " año";
                }
                else
                {
                    txtDuracionPatrocinador1.Text = patrocinador.DuracionContrato.ToString() + " años";
                }
                btnOfertasPatrocinadores.Visibility = Visibility.Hidden;
            }
            else
            {
                btnOfertasPatrocinadores.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
