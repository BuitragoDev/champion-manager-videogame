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
using System.Windows.Shapes;

namespace ChampionManager25.Vistas
{
    public partial class frmVentanaEmergenteAceptarOferta : Window
    {
        #region "Variables"
        private string _titulo;
        private string _mensaje;
        #endregion

        public frmVentanaEmergenteAceptarOferta(string titulo, string mensaje)
        {
            InitializeComponent();
            _titulo = titulo;
            _mensaje = mensaje;
        }

        private void VentanaEmergente_Loaded(object sender, RoutedEventArgs e)
        {
            txtTituloVentana.Text = _titulo;
            txtMensaje.Text = _mensaje;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.DialogResult = false;
            this.Close();
        }
    }
}
