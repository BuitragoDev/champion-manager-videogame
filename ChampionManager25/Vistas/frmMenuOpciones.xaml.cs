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
    public partial class frmMenuOpciones : Window
    {
        public frmMenuOpciones()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();

            this.Close();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            Application.Current.Shutdown();
        }

        private void imgCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
