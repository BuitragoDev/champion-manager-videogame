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
    public partial class UC_Menu_Finanzas : UserControl
    {
        // Eventos para notificar al control principal
        public event Action MostrarIngresos;
        public event Action MostrarGastos;
        public event Action MostrarPatrocinadores;
        public event Action MostrarTelevision;
        public event Action MostrarPrestamos;

        public UC_Menu_Finanzas()
        {
            InitializeComponent();
        }

        // Evento CLICK de la opción INGRESOS
        private void lblIngresos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblIngresos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblGastos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPatrocinadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblTelevision.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPrestamos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarIngresos?.Invoke();
        }

        // Evento CLICK de la opción GASTOS
        private void lblGastos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblIngresos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblGastos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblPatrocinadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblTelevision.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPrestamos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarGastos?.Invoke();
        }

        // Evento CLICK de la opción PATROCINADORES
        private void lblPatrocinadores_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblIngresos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblGastos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPatrocinadores.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblTelevision.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPrestamos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarPatrocinadores?.Invoke();
        }

        // Evento CLICK de la opción OFERTAS TV
        private void lblTelevision_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblIngresos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblGastos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPatrocinadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblTelevision.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblPrestamos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarTelevision?.Invoke();
        }

        // Evento CLICK de la opción PRESTAMOS BANCARIOS
        private void lblPrestamos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblIngresos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblGastos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPatrocinadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblTelevision.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPrestamos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            MostrarPrestamos?.Invoke();
        }
    }
}
