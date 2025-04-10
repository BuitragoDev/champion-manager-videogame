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
    public partial class UC_EditorHome : UserControl
    {
        public UC_EditorHome()
        {
            InitializeComponent();
        }

        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR JUGADORES
        private void imgEditarJugadores_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
        }

        private void imgEditarJugadores_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarJugadores.Source = new BitmapImage(new Uri("/Recursos/img/editor_jugadores_hover.png", UriKind.Relative));
        }

        private void imgEditarJugadores_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarJugadores.Source = new BitmapImage(new Uri("/Recursos/img/editor_jugadores.png", UriKind.Relative));
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR EQUIPOS
        private void imgEditarEquipos_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
        }

        private void imgEditarEquipos_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarEquipos.Source = new BitmapImage(new Uri("/Recursos/img/editor_equipos_hover.png", UriKind.Relative));
        }

        private void imgEditarEquipos_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarEquipos.Source = new BitmapImage(new Uri("/Recursos/img/editor_equipos.png", UriKind.Relative));
        }

        // ------------------------------------------------------------------------------- Eventos del boton EDITAR COMPETICION
        private void imgEditarCompeticion_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarEditorCompeticion();
        }

        private void imgEditarCompeticion_MouseEnter(object sender, MouseEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            imgEditarCompeticion.Source = new BitmapImage(new Uri("/Recursos/img/editor_competicion_hover.png", UriKind.Relative));
        }

        private void imgEditarCompeticion_MouseLeave(object sender, MouseEventArgs e)
        {
            imgEditarCompeticion.Source = new BitmapImage(new Uri("/Recursos/img/editor_competicion.png", UriKind.Relative));
        }
    }
}
