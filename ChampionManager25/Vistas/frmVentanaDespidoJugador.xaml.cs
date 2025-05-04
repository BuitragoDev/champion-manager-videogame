using ChampionManager25.Logica;
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
    public partial class frmVentanaDespidoJugador : Window
    {
        private string _titulo;
        private string _mensaje;
        private int _idJugador;
        private int _opcion; // 1 = Despedir jugador, 2 = Renovar Jugador,

        JugadorLogica _logicaJugador = new JugadorLogica();

        public frmVentanaDespidoJugador(string titulo, string mensaje, int idJugador, int opcion)
        {
            InitializeComponent();
            _titulo = titulo;
            _mensaje = mensaje;
            _idJugador = idJugador;
            _opcion = opcion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtTituloVentana.Text = _titulo;
            txtMensaje.Text = _mensaje;
        }

        // --------------------------------------------------------------- Evento CLICK del BOTÓN ACEPTAR
        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (_opcion == 1) // Opcion 1: Despedir Jugador
            {
                // Eliminar el id_equipo de la tabla jugadores.
                _logicaJugador.DespedirJugador(_idJugador);

                // Eliminar el contrato del jugador.
                _logicaJugador.EliminarContratoJugador(_idJugador);

                Metodos.ReproducirSonidoTransicion();
                this.DialogResult = true;
                this.Close();
            }
        }
        // -----------------------------------------------------------------------------------------------

        // --------------------------------------------------------------- Evento CLICK del BOTÓN CANCELAR
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.DialogResult = false;
            this.Close();
        }
        // -----------------------------------------------------------------------------------------------
    }
}
