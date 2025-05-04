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
    public partial class UC_Menu_Estadio : UserControl
    {
        // Eventos para notificar al control principal
        public event Action MostrarInformacion;
        public event Action MostrarEntradas;
        public event Action MostrarAmpliaciones;

        public UC_Menu_Estadio()
        {
            InitializeComponent();
        }

        // Evento CLICK de la opción INFORMACIÓN
        private void lblInformacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblInformacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblEntradas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblAmpliaciones.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarInformacion?.Invoke();
        }

        // Evento CLICK de la opción ENTRADAS
        private void lblEntradas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblInformacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEntradas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblAmpliaciones.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarEntradas?.Invoke();
        }

        // Evento CLICK de la opción AMPLIACIONES
        private void lblAmpliaciones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblInformacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEntradas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblAmpliaciones.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            MostrarAmpliaciones?.Invoke();
        }
    }
}
