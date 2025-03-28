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
    public partial class UC_Menu_Entrenador : UserControl
    {
        // Eventos para notificar al control principal
        public event Action? MostrarAlineacion;
        public event Action? MostrarEntrenamiento;
        public event Action? MostrarRival;

        public UC_Menu_Entrenador()
        {
            InitializeComponent();
        }

        private void lblAlineacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblAlineacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblEntrenamiento.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblVerRival.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarAlineacion?.Invoke();
        }

        private void lblEntrenamiento_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblEntrenamiento.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblAlineacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblVerRival.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarEntrenamiento?.Invoke();
        }

        private void lblCerRival_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblVerRival.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblEntrenamiento.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblAlineacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarRival?.Invoke();
        }
    }
}
