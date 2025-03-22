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
    public partial class UC_Menu_Manager : UserControl
    {
        // Eventos para notificar al control principal
        public event Action MostrarPalmares;
        public event Action MostrarFicha;

        public UC_Menu_Manager()
        {
            InitializeComponent();
        }

        // Evento CLICK de la opción FICHA
        private void lblFicha_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblFicha.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblPalmares.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarFicha?.Invoke();
        }

        // Evento CLICK de la opción PALMARES
        private void lblPalmares_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblFicha.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmares.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            MostrarPalmares?.Invoke();
        }
    }
}
