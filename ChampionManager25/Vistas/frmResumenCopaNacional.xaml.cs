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
    public partial class frmResumenCopaNacional : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        PalmaresLogica _logicaPalmares = new PalmaresLogica();

        public frmResumenCopaNacional(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
        }

        private void resumenCopa_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
            imgLogoCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));

            // Partido de la Final de Copa
            Partido final = _logicaPartido.ObtenerFinalCopa();
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
            else if(golesLocal < golesVisitante)
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

            // Mostrar detalles del campeon
            Equipo equipoCampeon = _logicaEquipo.ListarDetallesEquipo(equipoGanador);
            imgCampeon.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoCampeon.RutaImagen120));
            txtNombreCampeon.Text = equipoCampeon.Nombre.ToUpper();

            // Mostrar detalles de la final
            Equipo oEquipoLocal = _logicaEquipo.ListarDetallesEquipo(equipoLocal);
            Equipo oEquipoVisitante = _logicaEquipo.ListarDetallesEquipo(equipoVisitante);
            imgEquipoLocal.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + oEquipoLocal.RutaImagen120));
            imgEquipoVisitante.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + oEquipoVisitante.RutaImagen120));
            txtGolesLocal.Text = golesLocal.ToString();
            txtGolesVisitante.Text = golesVisitante.ToString();
            txtDesempate.Text = desempate;

            // Mostrar detalles de mi equipo
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            string ultimaRonda = _logicaPartido.ObtenerUltimaRondaEquipo(_equipo);
            imgMiEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen80));
            txtResultadoEquipo.Text = ultimaRonda.ToString();

            // Actualizar historial Copa Nacional en la Base de Datos
            _logicaPalmares.AnadirCampeonFinalistaCopa(Metodos.temporadaActual, equipoGanador, equipoPerdedor);
            _logicaPalmares.AnadirTituloCampeonCopa(equipoGanador);
        }

        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            this.Close();
        } 
    }
}
