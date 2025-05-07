using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
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

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Finanzas_DerechosTV : UserControl
    {
        private Manager _manager;
        private int _equipo;

        TelevisionLogica _logicaTelevision = new TelevisionLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_Menu_Finanzas_DerechosTV(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatosDerechosTV();
        }

        // ------------------------------------------------------------------------ Evento CLICK del botón OFERTAS PATROCINADORES
        private void btnOfertasTelevisiones_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            // Crear una nueva instancia de la ventana de detalles
            frmListaDerechosTelevision listaTelevisionesWindow = new frmListaDerechosTelevision(_equipo, _manager);  // Pasar el valor correcto del idEquipo

            // Suscribirse al evento Closed de la ventana secundaria
            listaTelevisionesWindow.Closed += ListaTelevisionesWindow_Closed;

            listaTelevisionesWindow.ShowDialog();
        }

        #region "Métodos"
        private void ListaTelevisionesWindow_Closed(object? sender, EventArgs e)
        {
            // Cuando la ventana emergente se cierra, recargamos los datos en el UserControl
            CargarDatosDerechosTV();
        }

        private void CargarDatosDerechosTV()
        {
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            imgLogo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));

            // Consultar si hay patrocinadores contratados
            Television cadenatv = _logicaTelevision.TelevisionesContratadas(_manager.IdManager, _equipo);


            if (cadenatv != null)
            {
                txtTelevision1.Text = cadenatv.Nombre.ToUpper();
                imgTelevision1.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/cadenaTV/" + cadenatv.IdTelevision + ".png"));
                txtCantidadTelevision1.Text = cadenatv.Cantidad.ToString("N0") + " $";
                if (cadenatv.DuracionContrato == 1)
                {
                    txtDuracionTelevision1.Text = cadenatv.DuracionContrato.ToString() + " año";
                }
                else
                {
                    txtDuracionTelevision1.Text = cadenatv.DuracionContrato.ToString() + " años";
                }
                btnOfertasTelevisiones.Visibility = Visibility.Hidden;
            }
            else
            {
                btnOfertasTelevisiones.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
