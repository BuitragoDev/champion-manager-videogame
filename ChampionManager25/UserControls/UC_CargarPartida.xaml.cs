using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
using static System.Resources.ResXFileRef;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChampionManager25.UserControls
{
    public partial class UC_CargarPartida : UserControl
    {
        PartidaGuardada partidaSeleccionada;
        private Border borderSeleccionado = null; // Variable para almacenar el Border seleccionado

        ManagerLogica _logicaManager = new ManagerLogica();
        private static EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_CargarPartida()
        {
            InitializeComponent();
        }

        private void cargarPartida_Loaded(object sender, RoutedEventArgs e)
        {
            CargarPartidas();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR PARTIDA
        private void btnCargarPartida_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

        }

        #region "Metodos"
        public void CargarPartidas()
        {
            
        }
        #endregion
    }
}
