﻿using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Numerics;
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
    public partial class frmSimulandoPartidos : Window
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        List<Partido> listaPartidos;
        Equipo equipoLocal;
        Equipo equipoVisitante;

        public int copaFinalizada = 0;
        public int copaEuropa1Finalizada = 0;
        public int copaEuropa2Finalizada = 0;
        int miCompeticionEuropea;

        private static Random random = new Random(); //Random global
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        FechaDatos _datosFecha = new FechaDatos();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();
        EntrenadorLogica _logicaEntrenador = new EntrenadorLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();

        public frmSimulandoPartidos(Manager manager, int equipo, List<Partido> listaPartidos)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            this.listaPartidos = listaPartidos;

            // Código que inicializa el sonido de fondo 
            try
            {
                if (Metodos.SonidoActivado == true)
                {
                    Metodos.ReproducirMusica("backgroundMusic2.wav");
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void simulacionPartidos_Loaded(object sender, RoutedEventArgs e)
        {
            DayOfWeek diaSemanaIngles = Metodos.hoy.DayOfWeek;
            string diaSemana = "";

            switch (diaSemanaIngles)
            {
                case DayOfWeek.Monday:
                    diaSemana = "Lunes";
                    break;
                case DayOfWeek.Tuesday:
                    diaSemana = "Martes";
                    break;
                case DayOfWeek.Wednesday:
                    diaSemana = "Miércoles";
                    break;
                case DayOfWeek.Thursday:
                    diaSemana = "Jueves";
                    break;
                case DayOfWeek.Friday:
                    diaSemana = "Viernes";
                    break;
                case DayOfWeek.Saturday:
                    diaSemana = "Sábado";
                    break;
                case DayOfWeek.Sunday:
                    diaSemana = "Domingo";
                    break;
            }

            txtSimulando.Text = $"Simulando partidos de la {_logicaCompeticion.MostrarNombreCompeticion(listaPartidos[0].IdCompeticion)} ({diaSemana}, {Metodos.hoy.ToString("dd-MM-yyyy")})...";
            int comp = listaPartidos[0].IdCompeticion; // IdCompeticion del primer partido
            int partidoVuelta = listaPartidos[0].PartidoVuelta ?? 0; // Comprobar si es un partido de vuelta
            miCompeticionEuropea = _logicaEquipo.ListarDetallesEquipo(_equipo).CompeticionEuropea;

            if (comp == 4)
            {
                int ronda = listaPartidos[0].Ronda ?? 0; // Ronda de Copa

                string nombreRonda = _logicaPartidos.ObtenerNombreRonda(ronda);
                string ruta_logo = _logicaCompeticion.ObtenerCompeticion(4).RutaImagen80;
                imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
                lblTituloVentana.Text += $" ({nombreRonda})";
            }
            if (comp == 5)
            {
                if (listaPartidos[0].Ronda == 0)
                {
                    int jornada = listaPartidos[0].Jornada ?? 0; // Jornada de Copa de Europa 1
                    string ruta_comp = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
                    imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                    lblTituloVentana.Text += $" (Jornada {jornada})";
                }
                else
                {
                    int ronda = listaPartidos[0].Ronda ?? 0; // Ronda de Copa Europa 1

                    string nombreRonda = _logicaPartidos.ObtenerNombreRonda(ronda);
                    string ruta_comp = _logicaCompeticion.ObtenerCompeticion(5).RutaImagen80;
                    imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                    lblTituloVentana.Text += $" ({nombreRonda})";
                }

            }
            if (comp == 6)
            {
                if (listaPartidos[0].Ronda == 0)
                {
                    int jornada = listaPartidos[0].Jornada ?? 0; // Jornada de Copa de Europa 2
                    string ruta_comp = _logicaCompeticion.ObtenerCompeticion(6).RutaImagen80;
                    imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                    lblTituloVentana.Text += $" (Jornada {jornada})";
                }
                else
                {
                    int ronda = listaPartidos[0].Ronda ?? 0; // Ronda de Copa Europa 2

                    string nombreRonda = _logicaPartidos.ObtenerNombreRonda(ronda);
                    string ruta_comp = _logicaCompeticion.ObtenerCompeticion(6).RutaImagen80;
                    imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_comp));
                    lblTituloVentana.Text += $" ({nombreRonda})";
                }

            }
            else if (comp >= 1 && comp <= 2) 
            { 
                int jornada = listaPartidos[0].Jornada ?? 0; // Jornada de Liga
                int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
                string ruta_logo = _logicaCompeticion.ObtenerCompeticion(miCompeticion).RutaImagen80;
                imgCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));
                lblTituloVentana.Text += $" (Jornada {jornada})";
            }

            progressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                foreach (Partido partido in listaPartidos)
                {
                    List<Jugador> todosLosJugadores = _logicaJugador.ListadoJugadoresCompleto(partido.IdEquipoLocal)
                                                      .Concat(_logicaJugador.ListadoJugadoresCompleto(partido.IdEquipoVisitante))
                                                      .ToList();
                    
                    ActualizarSancionesYLesiones(todosLosJugadores);
                    SimularPartido(partido);
                }
            });

            imgBotonCerrar.IsEnabled = true;
            progressBar.Visibility = Visibility.Collapsed;
            areaPartidos.Visibility = Visibility.Visible;
            MostrarPartidos(_logicaPartidos.PartidosHoy(_equipo, _manager.IdManager));

            if (comp == 4)
            {
                int ronda = listaPartidos[0].Ronda ?? 0; // Ronda de Copa
                if (partidoVuelta == 1)
                {
                    List<Equipo> clasificados = _logicaPartidos.ObtenerEquiposClasificados(ronda, 4, _manager.IdManager);

                    // Generar los partidos de la siguiente ronda
                    GeneralCalendarioCopa(clasificados, ronda);

                    // Comprobar si mi equipo ha pasado de ronda
                    bool clasificado = false;
                    foreach (var equipo in clasificados)
                    {
                        if (equipo.IdEquipo == _equipo)
                        {
                            clasificado = true;
                        }
                    }
                    if (clasificado == true)
                    {
                        // Mostrar ventana emergente
                        string titulo = "INFORMACIÓN";
                        string mensaje = "Felicidades por el trabajo realizado en la eliminatoria de Copa.\n\nEl equipo ha logrado el pase a la siguiente ronda, cumpliendo con lo esperado y manteniendo vivas nuestras aspiraciones en esta competición.";
                        frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaPasoDeRonda.ShowDialog();

                        // Actualizar confianza
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 10, 15, 10);
                    } 
                    else
                    {
                        int miUltimoRonda = _logicaPartidos.ObtenerUltimaRondaJugadaMiEquipo(_equipo);
                        if (miUltimoRonda >= ronda)
                        {
                            int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;
                            string mensaje = "";

                            if (reputacion > 89)
                            {
                                mensaje = "El equipo ha quedado eliminado de la Copa, un resultado que está por debajo de las expectativas marcadas para esta competición.";
                            }
                            else if (reputacion > 74)
                            {
                                mensaje = "Tras una eliminatoria muy igualada, el equipo ha quedado eliminado de la Copa.";
                            }
                            else
                            {
                                mensaje = "Pese a la eliminación, queremos reconocer el esfuerzo del equipo en esta edición de la Copa. La imagen ofrecida ha sido competitiva, y se ha luchado hasta el último minuto.";
                            }

                            // Mostrar ventana emergente
                            string titulo = "INFORMACIÓN";
                            frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            ventanaPasoDeRonda.ShowDialog();

                            // Actualizar confianza
                            _logicaManager.ActualizarConfianza(_manager.IdManager, -10, -15, -5);
                        } 
                    }
                }
                if (ronda > 5)
                {
                    copaFinalizada = 1;
                }
            }

            if (comp == 5)
            {
                int ronda = listaPartidos[0].Ronda ?? 0; 
                int jornada = listaPartidos[0].Jornada ?? 0; 

                if (jornada == 8)
                {
                    List<Clasificacion> clasificacionEuropa1 = _logicaClasificacion.MostrarClasificacionCopaEuropa1(5, _manager.IdManager);
                    List<int> clasificadosEuropa1 = new List<int>();

                    // Se crea la lista con los idEquipo de los 16 primeros
                    foreach (var equipo in clasificacionEuropa1.Take(16))
                    {
                        clasificadosEuropa1.Add(equipo.IdEquipo);
                    }

                    // Generar los partidos de la siguiente ronda de Copa Europa 1
                    GenerarCalendarioEuropa1(clasificadosEuropa1, jornada, ronda);

                    if (miCompeticionEuropea == 5)
                    {
                        // Comprobar si mi equipo ha pasado de ronda
                        bool clasificado = false;
                        foreach (var equipo in clasificadosEuropa1)
                        {
                            if (equipo == _equipo)
                            {
                                clasificado = true;
                            }
                        }
                        if (clasificado == true)
                        {
                            // Mostrar ventana emergente
                            string titulo = "INFORMACIÓN";
                            string mensaje = $"Felicidades por el trabajo realizado en la {_logicaCompeticion.MostrarNombreCompeticion(5)}.\n\nEl equipo ha logrado el pase a la siguiente ronda, cumpliendo con lo esperado y manteniendo vivas nuestras aspiraciones en esta competición.";
                            frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            ventanaPasoDeRonda.ShowDialog();

                            // Actualizar confianza
                            _logicaManager.ActualizarConfianza(_manager.IdManager, 10, 15, 10);
                        }
                        else
                        {
                            int miUltimoRonda = _logicaPartidos.ObtenerUltimaRondaJugadaMiEquipo(_equipo);
                            if (miUltimoRonda >= ronda)
                            {
                                int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;
                                string mensaje = "";

                                if (reputacion > 89)
                                {
                                    mensaje = $"El equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(5)}, un resultado que está por debajo de las expectativas marcadas para esta competición.";
                                }
                                else if (reputacion > 74)
                                {
                                    mensaje = $"Tras una participación irregular, el equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(5)}.";
                                }
                                else
                                {
                                    mensaje = $"Pese a la eliminación, queremos reconocer el esfuerzo del equipo en esta edición de la {_logicaCompeticion.MostrarNombreCompeticion(5)}. La imagen ofrecida ha sido competitiva, y se ha luchado hasta el último minuto.";
                                }

                                // Mostrar ventana emergente
                                string titulo = "INFORMACIÓN";
                                frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                ventanaPasoDeRonda.ShowDialog();

                                // Actualizar confianza
                                _logicaManager.ActualizarConfianza(_manager.IdManager, -10, -15, -5);
                            }
                        }
                    }
                } 
                else if (jornada > 8)
                {
                    if (partidoVuelta == 1)
                    {
                        List<int> clasificadosEuropa1 = _logicaPartidos.ObtenerEquiposClasificadosEuropa1(ronda, 5, _manager.IdManager);

                        // Generar los partidos de la siguiente ronda
                        GenerarCalendarioEuropa1(clasificadosEuropa1, jornada, ronda);

                        if (miCompeticionEuropea == 5)
                        {
                            // Comprobar si mi equipo ha pasado de ronda
                            bool clasificado = false;
                            foreach (var equipo in clasificadosEuropa1)
                            {
                                if (equipo == _equipo)
                                {
                                    clasificado = true;
                                }
                            }
                            if (clasificado == true)
                            {
                                // Mostrar ventana emergente
                                string titulo = "INFORMACIÓN";
                                string mensaje = $"Felicidades por el trabajo realizado en la {_logicaCompeticion.MostrarNombreCompeticion(5)}.\n\nEl equipo ha logrado el pase a la siguiente ronda, cumpliendo con lo esperado y manteniendo vivas nuestras aspiraciones en esta competición.";
                                frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                ventanaPasoDeRonda.ShowDialog();

                                // Actualizar confianza
                                _logicaManager.ActualizarConfianza(_manager.IdManager, 10, 15, 10);
                            }
                            else
                            {
                                int miUltimoRonda = _logicaPartidos.ObtenerUltimaRondaJugadaMiEquipo(_equipo);
                                if (miUltimoRonda >= ronda)
                                {
                                    int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;
                                    string mensaje = "";

                                    if (reputacion > 89)
                                    {
                                        mensaje = $"El equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(5)}, un resultado que está por debajo de las expectativas marcadas para esta competición.";
                                    }
                                    else if (reputacion > 74)
                                    {
                                        mensaje = $"Tras una participación irregular, el equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(5)}.";
                                    }
                                    else
                                    {
                                        mensaje = $"Pese a la eliminación, queremos reconocer el esfuerzo del equipo en esta edición de la {_logicaCompeticion.MostrarNombreCompeticion(5)}. La imagen ofrecida ha sido competitiva, y se ha luchado hasta el último minuto.";
                                    }

                                    // Mostrar ventana emergente
                                    string titulo = "INFORMACIÓN";
                                    frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                    ventanaPasoDeRonda.ShowDialog();

                                    // Actualizar confianza
                                    _logicaManager.ActualizarConfianza(_manager.IdManager, -10, -15, -5);
                                }
                            }
                        }                 
                    }
                }
                if (ronda > 5)
                {
                    copaEuropa1Finalizada = 1;
                }
            }

            if (comp == 6)
            {
                int ronda2 = listaPartidos[0].Ronda ?? 0;
                int jornada2 = listaPartidos[0].Jornada ?? 0;

                if (jornada2 == 8)
                {
                    List<Clasificacion> clasificacionEuropa2 = _logicaClasificacion.MostrarClasificacionCopaEuropa2(6, _manager.IdManager);
                    List<int> clasificadosEuropa2 = new List<int>();

                    // Se crea la lista con los idEquipo de los 16 primeros
                    foreach (var equipo in clasificacionEuropa2.Take(16))
                    {
                        clasificadosEuropa2.Add(equipo.IdEquipo);
                    }

                    // Generar los partidos de la siguiente ronda de Copa Europa 1
                    GenerarCalendarioEuropa2(clasificadosEuropa2, jornada2, ronda2);

                    if (miCompeticionEuropea == 6)
                    {
                        // Comprobar si mi equipo ha pasado de ronda
                        bool clasificado = false;
                        foreach (var equipo in clasificadosEuropa2)
                        {
                            if (equipo == _equipo)
                            {
                                clasificado = true;
                            }
                        }
                        if (clasificado == true)
                        {
                            // Mostrar ventana emergente
                            string titulo = "INFORMACIÓN";
                            string mensaje = $"Felicidades por el trabajo realizado en la {_logicaCompeticion.MostrarNombreCompeticion(6)}.\n\nEl equipo ha logrado el pase a la siguiente ronda, cumpliendo con lo esperado y manteniendo vivas nuestras aspiraciones en esta competición.";
                            frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                            ventanaPasoDeRonda.ShowDialog();

                            // Actualizar confianza
                            _logicaManager.ActualizarConfianza(_manager.IdManager, 10, 15, 10);
                        }
                        else
                        {
                            int miUltimoRonda = _logicaPartidos.ObtenerUltimaRondaJugadaMiEquipo(_equipo);
                            if (miUltimoRonda >= ronda2)
                            {
                                int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;
                                string mensaje = "";

                                if (reputacion > 89)
                                {
                                    mensaje = $"El equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(6)}, un resultado que está por debajo de las expectativas marcadas para esta competición.";
                                }
                                else if (reputacion > 74)
                                {
                                    mensaje = $"Tras una participación irregular, el equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(6)}.";
                                }
                                else
                                {
                                    mensaje = $"Pese a la eliminación, queremos reconocer el esfuerzo del equipo en esta edición de la {_logicaCompeticion.MostrarNombreCompeticion(6)}. La imagen ofrecida ha sido competitiva, y se ha luchado hasta el último minuto.";
                                }

                                // Mostrar ventana emergente
                                string titulo = "INFORMACIÓN";
                                frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                ventanaPasoDeRonda.ShowDialog();

                                // Actualizar confianza
                                _logicaManager.ActualizarConfianza(_manager.IdManager, -10, -15, -5);
                            }
                        }
                    }      
                }
                else if (jornada2 > 8)
                {
                    if (partidoVuelta == 1)
                    {
                        List<int> clasificadosEuropa2 = _logicaPartidos.ObtenerEquiposClasificadosEuropa2(ronda2, 6, _manager.IdManager);

                        // Generar los partidos de la siguiente ronda
                        GenerarCalendarioEuropa2(clasificadosEuropa2, jornada2, ronda2);

                        if (miCompeticionEuropea == 6)
                        {
                            // Comprobar si mi equipo ha pasado de ronda
                            bool clasificado = false;
                            foreach (var equipo in clasificadosEuropa2)
                            {
                                if (equipo == _equipo)
                                {
                                    clasificado = true;
                                }
                            }
                            if (clasificado == true)
                            {
                                // Mostrar ventana emergente
                                string titulo = "INFORMACIÓN";
                                string mensaje = $"Felicidades por el trabajo realizado en la {_logicaCompeticion.MostrarNombreCompeticion(6)}.\n\nEl equipo ha logrado el pase a la siguiente ronda, cumpliendo con lo esperado y manteniendo vivas nuestras aspiraciones en esta competición.";
                                frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                ventanaPasoDeRonda.ShowDialog();

                                // Actualizar confianza
                                _logicaManager.ActualizarConfianza(_manager.IdManager, 10, 15, 10);
                            }
                            else
                            {
                                int miUltimoRonda = _logicaPartidos.ObtenerUltimaRondaJugadaMiEquipo(_equipo);
                                if (miUltimoRonda >= ronda2)
                                {
                                    int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;
                                    string mensaje = "";

                                    if (reputacion > 89)
                                    {
                                        mensaje = $"El equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(6)}, un resultado que está por debajo de las expectativas marcadas para esta competición.";
                                    }
                                    else if (reputacion > 74)
                                    {
                                        mensaje = $"Tras una participación irregular, el equipo ha quedado eliminado de la {_logicaCompeticion.MostrarNombreCompeticion(6)}.";
                                    }
                                    else
                                    {
                                        mensaje = $"Pese a la eliminación, queremos reconocer el esfuerzo del equipo en esta edición de la {_logicaCompeticion.MostrarNombreCompeticion(6)}. La imagen ofrecida ha sido competitiva, y se ha luchado hasta el último minuto.";
                                    }

                                    // Mostrar ventana emergente
                                    string titulo = "INFORMACIÓN";
                                    frmVentanaEmergenteDosBotones ventanaPasoDeRonda = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                                    ventanaPasoDeRonda.ShowDialog();

                                    // Actualizar confianza
                                    _logicaManager.ActualizarConfianza(_manager.IdManager, -10, -15, -5);
                                }
                            }
                        }                   
                    }
                }
                if (ronda2 > 5)
                {
                    copaEuropa2Finalizada = 1;
                }
            }
        }

        private void simulacionPartidos_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Metodos.SonidoActivado == true)
            {
                Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
            }
        }

        // -------------------------------------------------------------------------------- Evento CLICK del boton CERRRAR
        private void imgBotonCerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        #region "Metodos"
        private void SimularPartido(Partido partido)
        {
            int competicionLocal = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal).IdCompeticion;

            // Cargar jugadores de los equipos desde la BD sin porteros
            List<Jugador> jugadoresLocal = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoLocal);
            List<Jugador> jugadoresVisitante = _logicaJugador.JugadoresJueganPartido(partido.IdEquipoVisitante);

            // Calcular goles con el equipo rival en cuenta
            int golesLocal = CalcularGoles(jugadoresLocal, jugadoresVisitante);
            int golesVisitante = CalcularGoles(jugadoresVisitante, jugadoresLocal);

            // Guardar resultado en el partido
            partido.GolesLocal = golesLocal;
            partido.GolesVisitante = golesVisitante;

            // Asignar goleadores y asistentes
            List<(Jugador, Jugador?)> golesYAsistencias = AsignarGolesYAsistencias(golesLocal, jugadoresLocal, random)
                .Concat(AsignarGolesYAsistencias(golesVisitante, jugadoresVisitante, random))
                .ToList();

            // Asignar tarjetas
            List<(Jugador, string)> tarjetas = AsignarTarjetas(jugadoresLocal, jugadoresVisitante, random);

            // Determinar MVP
            Jugador mvp = DeterminarMVP(golesYAsistencias, jugadoresLocal, jugadoresVisitante);

            // Calcular asistencia al estadio
            partido.Asistencia = _logicaEquipo.CalcularAsistencia(partido.IdEquipoLocal);

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE LIGA
            if (partido.IdCompeticion >= 1 && partido.IdCompeticion <= 2)
            {
                // Mostrar guardar resultado del partido
                _logicaPartidos.ActualizarPartido(partido);

                // Actualizar la clasificacion
                Clasificacion cla_local;
                Clasificacion cla_visitante;
                if (golesLocal == golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 0
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 0
                    };
                }
                else if (golesLocal > golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 1,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 1,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = -1
                    };
                }
                else
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 1,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = -1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 1,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 1
                    };
                }

                if (competicionLocal == 1)
                {
                    // Actualizar la Clasificacion de la Division de los equipos de mi equipo
                    _logicaClasificacion.ActualizarClasificacion(cla_local);
                    _logicaClasificacion.ActualizarClasificacion(cla_visitante);
                }
                else
                {
                    // Actualizar la Clasificacion de la Division de los equipos de mi equipo
                    _logicaClasificacion.ActualizarClasificacion2(cla_local);
                    _logicaClasificacion.ActualizarClasificacion2(cla_visitante);
                }

                // Actualizar estadísticas de cada jugador en la base de datos
                ActualizarEstadisticasPartido(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);

                // Actualizar la BD de jugadores sancionados
                foreach (var tarjeta in tarjetas)
                {
                    Jugador jugador = tarjeta.Item1;
                    string tipoTarjeta = tarjeta.Item2;

                    // Comprobar cuantas amarillas tiene y si es multiplo de 5 se le aplica 1 partido de sancion
                    Estadistica statJugador = _logicaEstadisticas.MostrarEstadisticasJugador(jugador.IdJugador, _manager.IdManager);

                    if (tipoTarjeta == "amarilla" || tipoTarjeta == "dobleamarilla")
                    {
                        if (statJugador.TarjetasAmarillas % 5 == 0)
                        {
                            _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, 1);
                        }
                    }
                    else if (tipoTarjeta == "roja")
                    {
                        _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, 2);
                    }
                }
            }

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE COPA
            if (partido.IdCompeticion == 4)
            {
                _logicaPartidos.ActualizarPartidoCopaNacional(partido);
            }

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE COPA DE EUROPA 1
            if (partido.IdCompeticion == 5)
            {
                _logicaPartidos.ActualizarPartidoCopaEuropa1(partido);

                // Actualizar la clasificacion
                Clasificacion cla_local;
                Clasificacion cla_visitante;
                if (golesLocal == golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 0
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 0
                    };
                }
                else if (golesLocal > golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 1,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 1,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = -1
                    };
                }
                else
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 1,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = -1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 1,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 1
                    };
                }

                if (partido.Jornada <= 8)
                {
                    // Actualizar la Clasificacion de la Copa de Europa 1
                    _logicaClasificacion.ActualizarClasificacionEuropa1(cla_local);
                    _logicaClasificacion.ActualizarClasificacionEuropa1(cla_visitante);
                }         

                // Actualizar estadísticas de cada jugador en la base de datos
                ActualizarEstadisticasPartidoEuropa(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);
            }

            // ACTUALIZAR DATOS SI ES UN PARTIDO DE COPA DE EUROPA 2
            if (partido.IdCompeticion == 6)
            {
                _logicaPartidos.ActualizarPartidoCopaEuropa2(partido);

                // Actualizar la clasificacion
                Clasificacion cla_local;
                Clasificacion cla_visitante;
                if (golesLocal == golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 0
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 1,
                        Perdidos = 0,
                        Puntos = 1,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 0
                    };
                }
                else if (golesLocal > golesVisitante)
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 1,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = 1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 1,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = -1
                    };
                }
                else
                {
                    cla_local = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoLocal,
                        Jugados = 1,
                        Ganados = 0,
                        Empatados = 0,
                        Perdidos = 1,
                        Puntos = 0,
                        LocalVictorias = 0,
                        LocalDerrotas = 1,
                        VisitanteVictorias = 0,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesLocal,
                        GolesContra = golesVisitante,
                        Racha = -1
                    };
                    cla_visitante = new Clasificacion
                    {
                        IdEquipo = partido.IdEquipoVisitante,
                        Jugados = 1,
                        Ganados = 1,
                        Empatados = 0,
                        Perdidos = 0,
                        Puntos = 3,
                        LocalVictorias = 0,
                        LocalDerrotas = 0,
                        VisitanteVictorias = 1,
                        VisitanteDerrotas = 0,
                        GolesFavor = golesVisitante,
                        GolesContra = golesLocal,
                        Racha = 1
                    };
                }

                if (partido.Jornada <= 8)
                {
                    // Actualizar la Clasificacion de la Copa de Europa 1
                    _logicaClasificacion.ActualizarClasificacionEuropa2(cla_local);
                    _logicaClasificacion.ActualizarClasificacionEuropa2(cla_visitante);
                }         

                // Actualizar estadísticas de cada jugador en la base de datos
                ActualizarEstadisticasPartidoEuropa(jugadoresLocal, jugadoresVisitante, golesYAsistencias, tarjetas, mvp);
            }

            // Comprobar si ha habido algun lesionado y actualizarlo en la BD
            List<int> lesionados = SimularLesiones(jugadoresLocal, jugadoresVisitante);
            foreach (int jugador in lesionados)
            {
                List<(string Descripcion, int MinSemanas, int MaxSemanas)> lesiones = new List<(string, int, int)>
                    {
                        ("Contusión leve", 1, 1),
                        ("Distensión muscular leve", 2, 3),
                        ("Contractura lumbar", 2, 4),
                        ("Esguince de tobillo grado 1", 2, 4),
                        ("Tendinitis rotuliana", 3, 5),
                        ("Rotura fibrilar leve", 3, 5),
                        ("Esguince de rodilla grado 2", 5, 7),
                        ("Fractura de dedo del pie", 6, 8),
                        ("Rotura fibrilar moderada", 6, 9),
                        ("Desgarro isquiotibial", 7, 10),
                        ("Lesión del ligamento colateral medial", 8, 11),
                        ("Fractura de costilla", 9, 12),
                        ("Lesión meniscal", 10, 14),
                        ("Luxación de hombro", 11, 15),
                        ("Rotura parcial del ligamento cruzado anterior", 13, 17),
                        ("Fractura de metatarsiano", 14, 18),
                        ("Rotura de menisco con cirugía", 16, 20),
                        ("Rotura completa del ligamento cruzado anterior", 20, 28),
                        ("Fractura de tibia", 25, 32),
                        ("Doble rotura ligamentosa con cirugía", 30, 40)
                    };

                int numeroAleatorio = random.Next(1, 41); // de 1 a 40
                var lesion = lesiones.FirstOrDefault(l => numeroAleatorio >= l.MinSemanas && numeroAleatorio <= l.MaxSemanas);

                // En caso de no encontrar ninguna (aunque no debería pasar con rangos bien cubiertos)
                if (lesion.Descripcion == null)
                {
                    lesion = ("Lesión desconocida", numeroAleatorio, numeroAleatorio);
                }

                _logicaJugador.PonerJugadorLesionado(jugador, numeroAleatorio, lesion.Descripcion);
            }

            // Actualizar los puntos de manager
            int entrenadorLocal = _logicaEntrenador.MostrarEntrenador(partido.IdEquipoLocal).IdEntrenador;
            int entrenadorVisitante = _logicaEntrenador.MostrarEntrenador(partido.IdEquipoVisitante).IdEntrenador;
            ActualizarPuntosManager(partido, golesLocal, golesVisitante, entrenadorLocal, entrenadorVisitante);

            // Actualizar atributos de los jugadores
            ActualizacionAtributos(jugadoresLocal, jugadoresVisitante, partido.IdEquipoLocal,
                                   partido.IdEquipoVisitante, golesLocal, golesVisitante, golesYAsistencias, mvp);
        }

        private int CalcularGoles(List<Jugador> jugadores, List<Jugador> jugadoresRival)
        {
            if (jugadores.Count == 0 || jugadoresRival.Count == 0) return 0; // Evitar errores

            // Calcular ataque del equipo y defensa del rival
            double ataque = jugadores.Average(j => (j.Remate + j.Pase + j.Calidad + j.Tiro + j.Regate + j.Velocidad) / 6.0);
            double defensa = jugadoresRival.Average(j => (j.Entradas + j.Resistencia + j.Agresividad + j.Velocidad) / 4.0);

            // Diferencia ajustada con más impacto
            double diferencia = (ataque - defensa) / 5.0; // Reducimos la escala del impacto
            double factor = 0.5 + (diferencia / 2.0); // Hacemos que el factor oscile más entre 0.3 y 0.7

            factor = Math.Clamp(factor, 0.25, 0.75); // Limitamos el factor ofensivo a valores más realistas

            // Goles esperados con una mayor base de variabilidad
            double golesEsperados = factor * (2.0 + random.NextDouble() * 2.5); // Entre 2 y 4.5 goles posibles

            // Variación aleatoria equilibrada
            double variacion = (random.NextDouble() * 1.8) - 0.9; // De -0.9 a +0.9 para más variedad
            int goles = (int)Math.Round(golesEsperados + variacion);

            // Asegurar valores dentro de un rango realista
            return Math.Clamp(goles, 0, 7);
        }
        private List<(Jugador, Jugador?)> AsignarGolesYAsistencias(int goles, List<Jugador> jugadores, Random random)
        {
            List<(Jugador, Jugador?)> lista = new List<(Jugador, Jugador?)>();

            // Filtrar jugadores que no sean porteros
            var jugadoresNoPorteros = jugadores.Where(j => j.RolId != 1).ToList();

            // Asignar pesos basados en atributos y posición
            var pesosGoleadores = jugadoresNoPorteros.Select(j =>
                                                                (jugador: j, peso: (j.Remate * 1.5 + j.Tiro * 1.5 + j.Regate * 1.5 + j.Calidad) *
                                                                (j.RolId == 10 ? 10 : (j.RolId >= 7 && j.RolId <= 9 ? 5 : 0.5))) // RolId 10 tiene un peso de 10, y 7-9 tienen un peso de 5
                                                            ).ToList();

            var pesosAsistentes = jugadoresNoPorteros.Select(j =>
                (jugador: j, peso: (j.Pase * 1.5 + j.Calidad) * (j.RolId >= 6 && j.RolId <= 10 ? 2 : 1))
            ).ToList();

            // Normalizar pesos
            double totalPesoGoleador = pesosGoleadores.Sum(p => p.peso);
            double totalPesoAsistente = pesosAsistentes.Sum(p => p.peso);

            for (int i = 0; i < goles; i++)
            {
                // Selección ponderada de goleador
                Jugador goleador = SeleccionarJugadorPonderado(pesosGoleadores, totalPesoGoleador, random);

                // 80% de probabilidad de asistencia
                Jugador? asistente = random.NextDouble() > 0.2 ?
                    SeleccionarJugadorPonderado(pesosAsistentes, totalPesoAsistente, random) : null;

                lista.Add((goleador, asistente));
            }

            return lista;
        }

        // Método para seleccionar un jugador de forma ponderada
        private Jugador SeleccionarJugadorPonderado(List<(Jugador jugador, double peso)> listaPesos, double totalPeso, Random random)
        {
            double valorAleatorio = random.NextDouble() * totalPeso;
            double suma = 0;

            foreach (var (jugador, peso) in listaPesos)
            {
                suma += peso;
                if (valorAleatorio <= suma)
                    return jugador;
            }

            return listaPesos.Last().jugador; // En caso de error, devolver el último
        }
        private List<(Jugador, string)> AsignarTarjetas(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante, Random random)
        {
            List<(Jugador, string)> tarjetas = new List<(Jugador, string)>();

            // Calcular cuántas tarjetas habrá (máximo 6 por equipo)
            int totalTarjetasLocal = random.Next(0, 6);
            int totalTarjetasVisitante = random.Next(0, 6);

            // Asignar tarjetas a cada equipo
            AsignarTarjetasEquipo(jugadoresLocal, totalTarjetasLocal, tarjetas, random);
            AsignarTarjetasEquipo(jugadoresVisitante, totalTarjetasVisitante, tarjetas, random);

            return tarjetas;
        }
        private void AsignarTarjetasEquipo(List<Jugador> jugadores, int totalTarjetas, List<(Jugador, string)> tarjetas, Random random)
        {
            // Seleccionamos jugadores más agresivos para recibir tarjetas
            List<Jugador> jugadoresAgresivos = jugadores.OrderByDescending(j => j.Agresividad).Take(totalTarjetas).ToList();
            Dictionary<int, int> tarjetasRecibidas = new Dictionary<int, int>(); // Control de tarjetas por jugador

            for (int i = 0; i < totalTarjetas; i++)
            {
                Jugador jugador = jugadoresAgresivos[i];

                // Ver cuántas tarjetas tiene ya este jugador
                if (!tarjetasRecibidas.ContainsKey(jugador.IdJugador))
                    tarjetasRecibidas[jugador.IdJugador] = 0;

                double probabilidad = random.NextDouble();

                if (probabilidad <= 0.50) // 50% de probabilidad de recibir una amarilla
                {
                    // Si el jugador ya tiene amarilla, solo 10% de probabilidad de recibir otra
                    if (tarjetasRecibidas[jugador.IdJugador] == 1 && random.NextDouble() > 0.20)
                        continue;

                    tarjetas.Add((jugador, "amarilla"));
                    tarjetasRecibidas[jugador.IdJugador]++;

                    // Si el jugador tiene 2 amarillas, se convierte en roja
                    if (tarjetasRecibidas[jugador.IdJugador] == 2)
                    {
                        tarjetas.Add((jugador, "dobleamarilla")); // Segunda amarilla y roja
                        tarjetasRecibidas[jugador.IdJugador] = 3; // Para evitar más tarjetas
                    }
                }
                else if (probabilidad > 0.95) // 5% de probabilidad de roja directa
                {
                    if (tarjetasRecibidas[jugador.IdJugador] == 0) // No dar roja si ya tiene amarilla
                    {
                        tarjetas.Add((jugador, "roja"));
                        tarjetasRecibidas[jugador.IdJugador] = 3; // Para evitar más tarjetas
                    }
                }
            }
        }

        private Jugador DeterminarMVP(List<(Jugador, Jugador?)> golesYAsistencias, List<Jugador> local, List<Jugador> visitante)
        {
            Dictionary<Jugador, int> puntuaciones = new Dictionary<Jugador, int>();

            foreach ((Jugador goleador, Jugador? asistente) in golesYAsistencias)
            {
                if (!puntuaciones.ContainsKey(goleador)) puntuaciones[goleador] = 0;
                puntuaciones[goleador] += 3;

                if (asistente != null)
                {
                    if (!puntuaciones.ContainsKey(asistente)) puntuaciones[asistente] = 0;
                    puntuaciones[asistente] += 2;
                }
            }

            // Si hay puntuaciones, devolver el jugador con más puntos
            if (puntuaciones.Count > 0)
            {
                return puntuaciones.OrderByDescending(p => p.Value).First().Key;
            }
            else
            {
                // Si no hay goleadores ni asistentes, elegir un jugador aleatorio de los que jugaron
                List<Jugador> todosJugadores = local.Concat(visitante).ToList();
                if (todosJugadores.Count == 0) throw new Exception("No hay jugadores disponibles para elegir MVP.");

                Random random = new Random();
                return todosJugadores[random.Next(todosJugadores.Count)];
            }
        }

        private void ActualizarEstadisticasPartido(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante,
                                           List<(Jugador, Jugador?)> golesYAsistencias,
                                           List<(Jugador, string)> tarjetas,
                                           Jugador mvp)
        {
            Dictionary<int, Estadistica> estadisticas = new Dictionary<int, Estadistica>();

            // Asegurar que todos los jugadores del partido sumen 1 partido jugado
            foreach (var jugador in jugadoresLocal.Concat(jugadoresVisitante))
            {
                estadisticas[jugador.IdJugador] = new Estadistica
                {
                    IdJugador = jugador.IdJugador,
                    PartidosJugados = 1, // Todos los jugadores suman 1 partido
                    Goles = 0,
                    Asistencias = 0,
                    TarjetasAmarillas = 0,
                    TarjetasRojas = 0,
                    MVP = 0
                };
            }

            // Sumar goles y asistencias
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                estadisticas[goleador.IdJugador].Goles++;

                if (asistente != null)
                {
                    estadisticas[asistente.IdJugador].Asistencias++;
                }
            }

            // Sumar tarjetas
            foreach (var (jugador, tipoTarjeta) in tarjetas)
            {
                if (tipoTarjeta.Contains("roja")) estadisticas[jugador.IdJugador].TarjetasRojas++;
                if (tipoTarjeta.Contains("amarilla")) estadisticas[jugador.IdJugador].TarjetasAmarillas++;
            }

            // Sumar MVP
            estadisticas[mvp.IdJugador].MVP++;

            // Guardar estadísticas en la base de datos
            foreach (var estadistica in estadisticas.Values)
            {
                _logicaEstadisticas.ActualizarEstadisticas(estadistica);
            }
        }

        private void ActualizarEstadisticasPartidoEuropa(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante,
                                           List<(Jugador, Jugador?)> golesYAsistencias,
                                           List<(Jugador, string)> tarjetas,
                                           Jugador mvp)
        {
            Dictionary<int, Estadistica> estadisticas = new Dictionary<int, Estadistica>();

            // Asegurar que todos los jugadores del partido sumen 1 partido jugado
            foreach (var jugador in jugadoresLocal.Concat(jugadoresVisitante))
            {
                estadisticas[jugador.IdJugador] = new Estadistica
                {
                    IdJugador = jugador.IdJugador,
                    PartidosJugados = 1, // Todos los jugadores suman 1 partido
                    Goles = 0,
                    Asistencias = 0,
                    TarjetasAmarillas = 0,
                    TarjetasRojas = 0,
                    MVP = 0
                };

                // -------------- CREAR GASTO POR PARTIDO
                Jugador player = _logicaJugador.MostrarDatosJugador(jugador.IdJugador);

                // Bonus
                int bonusPartido = player.BonusPartido ?? 0;
                if (bonusPartido != 0)
                {
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 16,
                        Tipo = 2,
                        Cantidad = bonusPartido,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, bonusPartido);
                }
            }

            // Sumar goles y asistencias
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                estadisticas[goleador.IdJugador].Goles++;

                if (asistente != null)
                {
                    estadisticas[asistente.IdJugador].Asistencias++;
                }
            }

            // Sumar tarjetas
            foreach (var (jugador, tipoTarjeta) in tarjetas)
            {
                if (tipoTarjeta.Contains("roja")) estadisticas[jugador.IdJugador].TarjetasRojas++;
                if (tipoTarjeta.Contains("amarilla")) estadisticas[jugador.IdJugador].TarjetasAmarillas++;
            }

            // Sumar MVP
            estadisticas[mvp.IdJugador].MVP++;

            // Guardar estadísticas en la base de datos
            foreach (var estadistica in estadisticas.Values)
            {
                _logicaEstadisticas.ActualizarEstadisticasEuropa(estadistica);
            }
        }

        public List<int> SimularLesiones(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante)
        {
            List<int> lesionados = new List<int>();

            // Recorrer jugadores locales y visitantes
            foreach (var jugador in jugadoresLocal)
            {
                if (random.Next(0, 81) == 13) // Generar número entre 0 y 80, si es 13 -> lesión
                {
                    lesionados.Add(jugador.IdJugador);
                }
            }

            foreach (var jugador in jugadoresVisitante)
            {
                if (random.Next(0, 81) == 13)
                {
                    lesionados.Add(jugador.IdJugador);
                }
            }

            return lesionados; // Devuelve la lista con los ID de jugadores lesionados
        }

        private void ActualizarSancionesYLesiones(List<Jugador> jugadores)
        {
            foreach (var jugador in jugadores)
            {
                // Reducir el número de partidos sancionados si es mayor que 0
                if (jugador.Sancionado > 0)
                {
                    _logicaJugador.PonerJugadorSancionado(jugador.IdJugador, jugador.Sancionado - 1);
                }
            }
        }

        private void ActualizarPuntosManager(Partido partido, int golesLocal, int golesVisitante, int entrenadorLocal, int entrenadorVisitante)
        {
            if (golesLocal > golesVisitante) // Victoria Local
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorLocal, 5);    
            }
            else if (golesLocal < golesVisitante) // Victoria Visitante
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorVisitante, 5);
            }
            else // Empate
            {
                _logicaEntrenador.ActualizarResultadoManager(entrenadorLocal, 2);
                _logicaEntrenador.ActualizarResultadoManager(entrenadorVisitante, 2);
            }
        }

        public void ActualizacionAtributos(List<Jugador> jugadoresLocal, List<Jugador> jugadoresVisitante, int idEquipolocal, int idEquipoVisitante,
                                           int golesLocal, int golesVisitante, List<(Jugador, Jugador?)> golesYAsistencias, Jugador mvp)
        {
            if (golesLocal > golesVisitante)
            {
                // Subir la moral 1 punto del los jugadores que ganan y bajar 1 punto de los jugadores que pierden
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -1, 0);
                }
            }
            else if (golesLocal < golesVisitante)
            {
                // Subir la moral 1 punto del los jugadores que ganan y bajar 1 punto de los jugadores que pierden
                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, -1, 0);
                }
            }
            else
            {
                // Subir la moral 1 punto del los jugadores que empatan
                foreach (var jugador in jugadoresLocal)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }

                foreach (var jugador in jugadoresVisitante)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                }
            }

            // Subir la moral y el estado de forma de los jugadores que han marcado, asistido o han sido MVP
            foreach (var (goleador, asistente) in golesYAsistencias)
            {
                _logicaJugador.ActualizarMoralEstadoForma(goleador.IdJugador, 2, 2);
                if (asistente != null)
                {
                    _logicaJugador.ActualizarMoralEstadoForma(asistente.IdJugador, 1, 1);
                }
            }
            _logicaJugador.ActualizarMoralEstadoForma(mvp.IdJugador, 3, 3);
        }

        private void MostrarPartidos(List<Partido> partidos)
        {
            // Limpiar la grid antes de cargar los nuevos partidos
            gridPartidos.Children.Clear();
            gridPartidos.RowDefinitions.Clear();
            gridPartidos.ColumnDefinitions.Clear();

            // Crear 12 columnas (6 por partido, 2 partidos por fila)
            for (int i = 0; i < 12; i++)
            {
                gridPartidos.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Filtrar los partidos visibles
            List<Partido> partidosVisibles = new List<Partido>();
            int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;

            foreach (var partido in partidos)
            {
                if (partido.IdCompeticion == miCompeticion || partido.IdCompeticion == 4 || partido.IdCompeticion == 5 || partido.IdCompeticion == 6)
                    partidosVisibles.Add(partido);
            }

            // Calcular cuántas filas necesitamos (dos partidos por fila)
            int filasNecesarias = (int)Math.Ceiling(partidosVisibles.Count / 2.0);

            for (int i = 0; i < filasNecesarias; i++)
            {
                gridPartidos.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < partidosVisibles.Count; i++)
            {
                Partido partido = partidosVisibles[i];
                Equipo equipoLocal = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal);
                Equipo equipoVisitante = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante);

                Brush fondoGolesLocal;
                Brush fondoGolesVisitante;

                if (partido.Estado != "Pendiente")
                {
                    if (partido.GolesLocal > partido.GolesVisitante)
                        fondoGolesLocal = Brushes.DarkGreen; // Ganó el local
                    else if (partido.GolesLocal < partido.GolesVisitante)
                        fondoGolesLocal = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23282D")); // Perdió el local
                    else
                        fondoGolesLocal = Brushes.Gray; // Empate
                }
                else
                {
                    fondoGolesLocal = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23282D")); // Partido no jugado
                }

                if (partido.Estado != "Pendiente")
                {
                    if (partido.GolesVisitante > partido.GolesLocal)
                        fondoGolesVisitante = Brushes.DarkGreen; // Ganó el visitante
                    else if (partido.GolesVisitante < partido.GolesLocal)
                        fondoGolesVisitante = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23282D")); // Perdió el visitante
                    else
                        fondoGolesVisitante = Brushes.Gray; // Empate
                }
                else
                {
                    fondoGolesVisitante = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF23282D")); // Partido no jugado
                }

                int row = i / 2;
                int columnOffset = (i % 2 == 0) ? 0 : 6;

                // Fondo alterno por fila
                Brush backgroundColor = (row % 2 == 0) ? Brushes.Gainsboro : Brushes.LightGray;
                Border rowBackground = new Border
                {
                    Background = backgroundColor,
                    BorderThickness = new Thickness(0),
                    Margin = new Thickness(2),
                    Height = 50
                };
                Grid.SetRow(rowBackground, row);
                Grid.SetColumnSpan(rowBackground, 6);
                Grid.SetColumn(rowBackground, columnOffset);
                gridPartidos.Children.Add(rowBackground);

                // Crear Grid interno para el partido
                Grid partidoGrid = new Grid();
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });  // Escudo local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) }); // Nombre local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });  // Goles local
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(70) });  // Goles visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) }); // Nombre visitante
                partidoGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) }); // Escudo visitante

                // Escudo local
                partidoGrid.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoLocal.RutaImagen32)),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
                Grid.SetColumn(partidoGrid.Children[^1], 0);

                // Nombre local
                partidoGrid.Children.Add(new TextBlock
                {
                    Text = equipoLocal.Nombre,
                    FontSize = 16,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Foreground = equipoLocal.IdEquipo == _equipo ? Brushes.DarkRed : Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Margin = new Thickness(5)
                });
                Grid.SetColumn(partidoGrid.Children[^1], 1);

                // Goles local con fondo negro
                var borderGolesLocal = new Border
                {
                    Background = fondoGolesLocal,
                    Margin = new Thickness(3),
                    Child = new TextBlock
                    {
                        Text = partido.Estado == "Pendiente" ? "-" : partido.GolesLocal.ToString(),
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Foreground = Brushes.White,
                        TextAlignment = TextAlignment.Center,
                        Padding = new Thickness(2, 2, 2, 2)
                    }
                };
                Grid.SetColumn(borderGolesLocal, 2);
                partidoGrid.Children.Add(borderGolesLocal);

                // Goles visitante con fondo negro
                var borderGolesVisitante = new Border
                {
                    Background = fondoGolesVisitante,
                    Margin = new Thickness(3),
                    Child = new TextBlock
                    {
                        Text = partido.Estado == "Pendiente" ? "-" : partido.GolesVisitante.ToString(),
                        FontSize = 24,
                        FontWeight = FontWeights.Bold,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Foreground = Brushes.White,
                        TextAlignment = TextAlignment.Center,
                        Padding = new Thickness(2, 2, 2, 2)
                    }
                };
                Grid.SetColumn(borderGolesVisitante, 3);
                partidoGrid.Children.Add(borderGolesVisitante);


                // Nombre visitante
                partidoGrid.Children.Add(new TextBlock
                {
                    Text = equipoVisitante.Nombre,
                    FontSize = 16,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    Foreground = equipoVisitante.IdEquipo == _equipo ? Brushes.DarkRed : Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(4)
                });
                Grid.SetColumn(partidoGrid.Children[^1], 4);

                // Escudo visitante
                partidoGrid.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipoVisitante.RutaImagen32)),
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                });
                Grid.SetColumn(partidoGrid.Children[^1], 5);

                Grid.SetRow(partidoGrid, row);
                Grid.SetColumn(partidoGrid, columnOffset);
                Grid.SetColumnSpan(partidoGrid, 6);
                gridPartidos.Children.Add(partidoGrid);
            }
        }

        public void GeneralCalendarioCopa(List<Equipo> equiposCopa, int ronda)
        {
            // Mezclar aleatoriamente
            Random rnd = new Random();
            var equiposMezclados = equiposCopa.OrderBy(e => rnd.Next()).ToList();

            // Fechas de ida y vuelta
            DateTime fechaIda = ObtenerFechaIda();
            DateTime fechaVuelta = fechaIda.AddDays(14);

            // Crear los emparejamientos
            for (int i = 0; i < equiposMezclados.Count; i += 2)
            {
                int idLocal = equiposMezclados[i].IdEquipo;
                int idVisitante = equiposMezclados[i + 1].IdEquipo;

                if (ronda < 5)
                {
                    // Partido de ida
                    _logicaPartidos.crearPartidoCopa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 4, ronda + 1, 0, _manager.IdManager);

                    // Partido de vuelta (se invierte local/visitante)
                    _logicaPartidos.crearPartidoCopa(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), 4, ronda + 1, 1, _manager.IdManager);
                }
                else
                {
                    // Partido unico
                    _logicaPartidos.crearPartidoCopa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 4, ronda + 1, 0, _manager.IdManager);
                }   
            }
        }

        public void GenerarCalendarioEuropa1(List<int> equiposEuropa1, int jornada, int ronda)
        {
            // Mezclar aleatoriamente
            Random rnd = new Random();
            var equiposMezclados = equiposEuropa1.OrderBy(e => rnd.Next()).ToList();

            // Fechas de ida y vuelta
            DateTime fechaIda = ObtenerFechaIdaEuropa();
            DateTime fechaVuelta = fechaIda.AddDays(14);

            // Crear los emparejamientos
            for (int i = 0; i < equiposMezclados.Count; i += 2)
            {
                int idLocal = equiposMezclados[i];
                int idVisitante = equiposMezclados[i + 1];

                if (ronda < 5)
                {
                    // Partido de ida
                    _logicaPartidos.CrearPartidoCopaEuropa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 5, jornada + 1, ronda != 0 ? ronda + 1 : 3, 0, _manager.IdManager);

                    // Partido de vuelta (se invierte local/visitante)
                    _logicaPartidos.CrearPartidoCopaEuropa(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), 5, jornada + 1, ronda != 0 ? ronda + 1 : 3, 1, _manager.IdManager);
                }
                else
                {
                    // Partido unico
                    _logicaPartidos.CrearPartidoCopaEuropa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 5, jornada + 1, ronda != 0 ? ronda + 1 : 3, 0, _manager.IdManager);
                }
            }
        }

        public void GenerarCalendarioEuropa2(List<int> equiposEuropa1, int jornada, int ronda)
        {
            // Mezclar aleatoriamente
            Random rnd = new Random();
            var equiposMezclados = equiposEuropa1.OrderBy(e => rnd.Next()).ToList();

            // Fechas de ida y vuelta
            DateTime fechaIda = ObtenerFechaIdaEuropa();
            DateTime fechaVuelta = fechaIda.AddDays(14);

            // Crear los emparejamientos
            for (int i = 0; i < equiposMezclados.Count; i += 2)
            {
                int idLocal = equiposMezclados[i];
                int idVisitante = equiposMezclados[i + 1];

                if (ronda < 5)
                {
                    // Partido de ida
                    _logicaPartidos.CrearPartidoCopaEuropa2(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 6, jornada + 1, ronda != 0 ? ronda + 1 : 3, 0, _manager.IdManager);

                    // Partido de vuelta (se invierte local/visitante)
                    _logicaPartidos.CrearPartidoCopaEuropa2(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), 6, jornada + 1, ronda != 0 ? ronda + 1 : 3, 1, _manager.IdManager);
                }
                else
                {
                    // Partido unico
                    _logicaPartidos.CrearPartidoCopaEuropa2(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), 6, jornada + 1, ronda != 0 ? ronda + 1 : 3, 0, _manager.IdManager);
                }
            }
        }

        public DateTime ObtenerFechaIda()
        {
            Fecha fechaObj = _datosFecha.ObtenerFechaHoy();

            if (fechaObj == null || string.IsNullOrWhiteSpace(fechaObj.Hoy))
                throw new Exception("No se pudo obtener una fecha válida.");

            if (!DateTime.TryParse(fechaObj.Hoy, out DateTime hoy))
                throw new Exception("Formato de fecha inválido en la base de datos.");

            return hoy.AddDays(21);
        }

        public DateTime ObtenerFechaIdaEuropa()
        {
            Fecha fechaObj = _datosFecha.ObtenerFechaHoy();

            if (fechaObj == null || string.IsNullOrWhiteSpace(fechaObj.Hoy))
                throw new Exception("No se pudo obtener una fecha válida.");

            if (!DateTime.TryParse(fechaObj.Hoy, out DateTime hoy))
                throw new Exception("Formato de fecha inválido en la base de datos.");

            return hoy.AddDays(14);
        }
        #endregion
    }
}
