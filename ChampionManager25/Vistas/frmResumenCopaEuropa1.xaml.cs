using ChampionManager25.Datos;
using ChampionManager25.Entidades;
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
    public partial class frmResumenCopaEuropa1 : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        List<Jugador> balonOro;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();

        public frmResumenCopaEuropa1(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
        }

        private void resumenEuropa1_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
            imgLogoCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
            lblTituloVentana.Text = $"Resumen de la {_logicaCompeticion.ObtenerCompeticion(5).Nombre}";

            // Partido de la Final de Copa
            Partido final = _logicaPartido.ObtenerFinalCopaEuropa1();
            int equipoLocal = final.IdEquipoLocal;
            int equipoVisitante = final.IdEquipoVisitante;
            int golesLocal = final.GolesLocal ?? 0;
            int golesVisitante = final.GolesVisitante ?? 0;
            int equipoGanador = 0;
            int equipoPerdedor = 0;
            string desempate = "";

            if (golesLocal > golesVisitante)
            {
                equipoGanador = equipoLocal;
                equipoPerdedor = equipoVisitante;
            }
            else if (golesLocal < golesVisitante)
            {
                equipoGanador = equipoVisitante;
                equipoPerdedor = equipoLocal;
            }
            else
            {
                // Empate: elegir ganador aleatorio
                Random rnd = new Random();
                bool localGana = rnd.Next(0, 2) == 0;

                equipoGanador = localGana ? equipoLocal : equipoVisitante;
                equipoPerdedor = localGana ? equipoVisitante : equipoLocal;

                desempate = $"{_logicaEquipo.ListarDetallesEquipo(equipoGanador).Nombre} ganador en el tiempo extra";
            }

            // Mostrar detalles de la final
            Equipo oEquipoLocal = _logicaEquipo.ListarDetallesEquipo(equipoLocal);
            Equipo oEquipoVisitante = _logicaEquipo.ListarDetallesEquipo(equipoVisitante);
            imgEquipoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + oEquipoLocal.RutaImagen120));
            imgEquipoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + oEquipoVisitante.RutaImagen120));
            txtGolesLocal.Text = golesLocal.ToString();
            txtGolesVisitante.Text = golesVisitante.ToString();
            txtDesempate.Text = desempate;
            txtNombreEquipoLocal.Text = _logicaEquipo.ListarDetallesEquipo(equipoLocal).Nombre;
            txtNombreEquipoVisitante.Text = _logicaEquipo.ListarDetallesEquipo(equipoVisitante).Nombre;

            // Balon de Oro
            balonOro = _logicaEstadisticas.BalonDeOroEuropa1();

            imgFotoBalonOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + balonOro[0].RutaImagen));
            imgFotoBalonPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + balonOro[1].RutaImagen));
            imgFotoBalonBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + balonOro[2].RutaImagen));

            txtBalonOroNombre.Text = $"{balonOro[0].Nombre}";
            txtBalonOroApellido.Text = $"{balonOro[0].Apellido.ToUpper()}";
            txtBalonOroPuntos.Text = $"{balonOro[0].Valoracion} puntos";

            txtBalonPlataNombre.Text = $"{balonOro[1].Nombre}";
            txtBalonPlataApellido.Text = $"{balonOro[1].Apellido.ToUpper()}";
            txtBalonPlataPuntos.Text = $"{balonOro[1].Valoracion} puntos";

            txtBalonBronceNombre.Text = $"{balonOro[2].Nombre}";
            txtBalonBronceApellido.Text = $"{balonOro[2].Apellido.ToUpper()}";
            txtBalonBroncePuntos.Text = $"{balonOro[2].Valoracion} puntos";

            string rutaEquipoBota1 = _logicaEquipo.ListarDetallesEquipo(balonOro[0].IdEquipo).RutaImagen64;
            string rutaEquipoBota2 = _logicaEquipo.ListarDetallesEquipo(balonOro[1].IdEquipo).RutaImagen64;
            string rutaEquipoBota3 = _logicaEquipo.ListarDetallesEquipo(balonOro[2].IdEquipo).RutaImagen64;
            imgEquipoBalonOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota1));
            imgEquipoBalonPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota2));
            imgEquipoBalonBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota3));

            // Actualizar historial Copa Europa 1 en la Base de Datos
            _logicaPalmares.AnadirCampeonFinalistaCopaEuropa1(Metodos.temporadaActual, equipoGanador, equipoPerdedor);
            _logicaPalmares.AnadirTituloCampeonCopaEuropa1(equipoGanador);
        }

        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        } 
    }
}
