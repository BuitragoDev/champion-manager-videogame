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
    public partial class UC_Menu_Estadio_Entradas : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        Equipo miEquipo;
        #endregion

        // Instancias de la LOGICA
        PartidoLogica _logicaPartido = new PartidoLogica();
        TaquillaLogica _logicaTaquilla = new TaquillaLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_Menu_Estadio_Entradas(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
        }

        private void UCEntradas_Loaded(object sender, RoutedEventArgs e)
        {
            Partido proximoPartido = _logicaPartido.MostrarProximoPartidoLocal(_equipo, _manager.IdManager, Metodos.hoy);
            Equipo local = _logicaEquipo.ListarDetallesEquipo(proximoPartido.IdEquipoLocal);
            Equipo visitante = _logicaEquipo.ListarDetallesEquipo(proximoPartido.IdEquipoVisitante);

            imgLogo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen));
            string ruta_comp = _logicaCompeticion.ObtenerCompeticion(proximoPartido.IdCompeticion).RutaImagen;
            imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
            txtFecha.Text = proximoPartido.FechaPartido.ToString("d 'de' MMMM 'de' yyyy", new System.Globalization.CultureInfo("es-ES"));
            imgLogoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + local.RutaImagen80));
            imgLogoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + visitante.RutaImagen80));

            Taquilla miTaquilla = _logicaTaquilla.RecuperarPreciosTaquilla(_equipo, _manager.IdManager);
            txtPrecioEntradaGeneral.Text = miTaquilla.PrecioEntradaGeneral.ToString();
            txtPrecioEntradaTribuna.Text = miTaquilla.PrecioEntradaTribuna.ToString();
            txtPrecioEntradaVip.Text = miTaquilla.PrecioEntradaVip.ToString();

            if (miTaquilla.PrecioAbonoGeneral == 0)
            {
                txtPrecioAbonoGeneral.Text = "250";
            }
            else
            {
                txtPrecioAbonoGeneral.Text = miTaquilla.PrecioAbonoGeneral.ToString();
            }

            if (miTaquilla.PrecioAbonoTribuna == 0)
            {
                txtPrecioAbonoTribuna.Text = "500";
            }
            else
            {
                txtPrecioAbonoTribuna.Text = miTaquilla.PrecioAbonoTribuna.ToString();
            }

            if (miTaquilla.PrecioAbonoVip == 0)
            {
                txtPrecioAbonoVip.Text = "1000";
            }
            else
            {
                txtPrecioAbonoVip.Text = miTaquilla.PrecioAbonoVip.ToString();
            }

            //Comprobar si se ha establecido el precio de los abonos.
            bool verificadoAbono = _logicaTaquilla.ComprobarAbono(_equipo, _manager.IdManager);
            if (verificadoAbono)
            {
                btnEstablecerPrecioAbono.Visibility = Visibility.Hidden;
            }
            else
            {
                btnEstablecerPrecioAbono.Visibility = Visibility.Visible;
            }
        }

        // ---------------------------------------------------------------------------------------------- Eventos de las CAJAS DE TEXTO
        private void txtPrecioEntradaVip_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPrecioEntradaTribuna_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPrecioEntradaGeneral_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPrecioAbonoVip_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPrecioAbonoTribuna_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPrecioAbonoGeneral_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Permitir solo números (del 0 al 9), no espacios ni otros caracteres
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton ESTABLECER PRECIO ENTRADAS
        private void btnEstablecerPrecioEntrada_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            _logicaTaquilla.EstablecerPrecioEntradas(_equipo, _manager.IdManager, int.Parse(txtPrecioEntradaGeneral.Text.Trim()), int.Parse(txtPrecioEntradaTribuna.Text.Trim()),
                                                   int.Parse(txtPrecioEntradaVip.Text.Trim()));
            btnEstablecerPrecioEntrada.Visibility = Visibility.Hidden;
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton ESTABLECER PRECIO ABONO
        private void btnEstablecerPrecioAbono_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            _logicaTaquilla.EstablecerPrecioAbonos(_equipo, _manager.IdManager, int.Parse(txtPrecioAbonoGeneral.Text.Trim()),
                                                   int.Parse(txtPrecioAbonoTribuna.Text.Trim()), int.Parse(txtPrecioAbonoVip.Text.Trim()));
            btnEstablecerPrecioAbono.Visibility = Visibility.Hidden;
        }
    }
}
