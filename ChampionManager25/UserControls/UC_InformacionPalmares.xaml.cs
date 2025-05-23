﻿using System;
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
using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;

namespace ChampionManager25.UserControls
{
    public partial class UC_InformacionPalmares : UserControl
    {
        #region "Variables"
        Equipo equipo;
        #endregion

        // Instancias de la LOGICA
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();

        public UC_InformacionPalmares(Equipo eqp)
        {
            InitializeComponent();
            this.equipo = eqp;
        }

        private void informacionPalmares_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen;
            imgLiga1.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
            lblChampionsLeague.Text = _logicaPalmares.numTitulosEquipoCompeticion(equipo.IdEquipo, 1).ToString();
        }
    }
}
