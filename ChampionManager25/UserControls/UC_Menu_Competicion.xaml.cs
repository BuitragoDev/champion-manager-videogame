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
    public partial class UC_Menu_Competicion : UserControl
    {
        // Eventos para notificar al control principal
        public event Action? MostrarClasificacion;
        public event Action? MostrarResultados;
        public event Action? MostrarEstadisticas;
        public event Action? MostrarPalmares;
        public event Action? MostrarPalmaresJugadores;

        public UC_Menu_Competicion()
        {
            InitializeComponent();
        }

        private void lblClasificacion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblClasificacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblResultados.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadisticas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmares.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmaresJugadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarClasificacion?.Invoke();
        }

        private void lblResultados_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblResultados.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblClasificacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadisticas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmares.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmaresJugadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarResultados?.Invoke();
        }

        private void lblEstadisticas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblEstadisticas.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblResultados.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblClasificacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmares.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmaresJugadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarEstadisticas?.Invoke();
        }

        private void lblPalmares_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblPalmares.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblResultados.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadisticas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblClasificacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmaresJugadores.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            MostrarPalmares?.Invoke();
        }

        private void lblPalmaresJugadores_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            lblPalmaresJugadores.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));
            lblResultados.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblEstadisticas.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblClasificacion.Foreground = new SolidColorBrush(Colors.WhiteSmoke);
            lblPalmares.Foreground = new SolidColorBrush(Colors.WhiteSmoke);

            MostrarPalmaresJugadores?.Invoke();
        }
    }
}
