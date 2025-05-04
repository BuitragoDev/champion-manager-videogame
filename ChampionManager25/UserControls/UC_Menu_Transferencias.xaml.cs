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
    public partial class UC_Menu_Transferencias : UserControl
    {
        // Eventos para notificar al control principal
        public event Action? MostrarMercado;
        public event Action? MostrarBuscarPorEquipo;
        public event Action? MostrarBuscarPorFiltro;
        public event Action? MostrarCartera;
        public event Action? MostrarEstadoOfertas;
        public event Action? MostrarListaTraspasos;

        public UC_Menu_Transferencias()
        {
            InitializeComponent();
        }
        private void lblMercado_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblMercado.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblCartera.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadoOfertas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblListaTraspasos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarMercado?.Invoke();
        }

        private void lblBuscarPorEquipo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblMercado.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblCartera.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadoOfertas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblListaTraspasos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarBuscarPorEquipo?.Invoke();
        }

        private void lblBuscarPorFiltro_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblMercado.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblCartera.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadoOfertas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblListaTraspasos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarBuscarPorFiltro?.Invoke();
        }

        private void lblCartera_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblCartera.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblMercado.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadoOfertas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblListaTraspasos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarCartera?.Invoke();
        }

        private void lblEstadoOfertas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblEstadoOfertas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblCartera.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblMercado.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblListaTraspasos.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarEstadoOfertas?.Invoke();
        }

        private void lblListaTraspasos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            lblListaTraspasos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblBuscarPorEquipo.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblBuscarPorFiltro.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblCartera.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadoOfertas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblMercado.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarListaTraspasos?.Invoke();
        }
    }
}
