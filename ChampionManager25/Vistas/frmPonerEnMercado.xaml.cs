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
    public partial class frmPonerEnMercado : Window
    {
        private string _titulo;
        private string _mensaje;
        public frmPonerEnMercado(string titulo, string mensaje)
        {
            InitializeComponent();
            _titulo = titulo;
            _mensaje = mensaje;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTituloVentana.Text = _titulo;
            txtMensaje.Text = _mensaje;
        }

        private void btnMercadoTransferibles_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            this.DialogResult = true;
            this.Close();
        }

        private void btnMercadoCesiones_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            this.DialogResult = false;
            this.Close();
        }
    }
}
