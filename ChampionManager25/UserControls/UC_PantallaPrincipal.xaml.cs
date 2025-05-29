using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Vistas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
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
    public partial class UC_PantallaPrincipal : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private static MediaPlayer mediaPlayer = new MediaPlayer(); // Inicialización al declarar
        Equipo miEquipo;
        List<Jugador> balonOro;
        List<Jugador> botaOro;
        List<Jugador> mejorOnce;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        FechaDatos _datosFecha = new FechaDatos();
        HistorialLogica _logicaHistorial = new HistorialLogica();
        ClasificacionLogica _logicaClasificacion = new ClasificacionLogica();
        EstadisticasLogica _logicaEstadisticas = new EstadisticasLogica();
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        ManagerLogica _logicaManager = new ManagerLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        PrestamoLogica _logicaPrestamo = new PrestamoLogica();
        RemodelacionLogica _logicaRemodelacion = new RemodelacionLogica();
        TaquillaLogica _logicaTaquilla = new TaquillaLogica();
        PatrocinadorLogica _logicaPatrocinador = new PatrocinadorLogica();
        TelevisionLogica _logicaTelevision = new TelevisionLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();

        NacionalidadToFlagConverter convertidorBandera = new NacionalidadToFlagConverter();

        public UC_PantallaPrincipal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);

            // Código que inicializa el sonido de fondo de la pantalla principal
            try
            {
                Metodos.ReproducirMusica("backgroundTrainingSounds.wav");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void pantallaPrincipal_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar datos de la cabecera
            imgLogoEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + miEquipo.RutaImagen80));

            txtEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre;
            txtPresupuesto.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Presupuesto.ToString("N0") + " €";
            txtManager.Text = _manager.Nombre + " " + _manager.Apellido;
            int repu = _logicaManager.MostrarManager(_manager.IdManager).Reputacion;
            MostrarEstrellas(repu);

            // Comprobar mensajes nuevos
            int mensajesNoLeidos = _logicaMensajes.MensajesNoLeidos(_manager.IdManager);
            if (mensajesNoLeidos > 0)
            {
                imgCorreo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/correoNuevo_icon.png"));
            }
            else
            {
                imgCorreo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/correo_icon.png"));
            }

            CargarFecha();

            Fecha fechaObjeto = _datosFecha.ObtenerFechaHoy();
            DateTime hoy = DateTime.Parse(fechaObjeto.Hoy);

            // Comprobar si es DIA DE PARTIDO y cambiar el boton
            Partido proximoPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, hoy);
            if (proximoPartido != null && proximoPartido.FechaPartido == hoy)
            {
                btnAvanzar.Content = "PARTIDO";
            } 

            // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
            if (DockPanel_Central.Children.Count > 0)
            {
                DockPanel_Central.Children.Clear();
            }
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);
        }

        private void pantallaPrincipal_Unloaded(object sender, RoutedEventArgs e)
        {
            //Metodos.DetenerMusica(); // Detener la música al descargar el control
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón AVANZAR
        private async void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            // Reproducir el sonido
            Metodos.ReproducirSonidoTransicion();
            DateTime fechaHoy = Metodos.hoy;

            // --------------------- REALIZAR TRASPASOS DENTRO DE LAS VENTANAS DE TRASPASOS
            DateTime inicioVerano = new DateTime(fechaHoy.Year, 7, 1);
            DateTime finVerano = new DateTime(fechaHoy.Year, 8, 30);
            DateTime inicioInvierno = new DateTime(fechaHoy.Year, 1, 1);
            DateTime finInvierno = new DateTime(fechaHoy.Year, 1, 31);

            // Comprobar si hoy está en un rango válido
            bool enRangoEnero = fechaHoy >= inicioInvierno && fechaHoy <= finInvierno;
            bool enRangoVerano = fechaHoy >= inicioVerano && fechaHoy <= finVerano;

            if (enRangoEnero || enRangoVerano)
            {
                // Realizar traspasos IA
                _logicaJugador.TraspasosIA(_equipo);
            }

            // --------------------------- SI ES LUNES -------------------------
            btnAvanzar.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                if (fechaHoy.DayOfWeek == DayOfWeek.Monday)
                {
                    // --------------------- COMPROBAR MI PRESUPUESTO
                    int presupuesto = _logicaEquipo.ListarDetallesEquipo(_equipo).Presupuesto;
                    if (presupuesto < 0)
                    {
                        Metodos.avisosBancarrota += 1;
                    }

                    if (Metodos.avisosBancarrota > 0 && Metodos.avisosBancarrota < 4)
                    {
                        // Mostrar ventana de aviso de bancarrota
                        string titulo = "INFORMACIÓN";
                        string unidad = Metodos.avisosBancarrota == 1 ? "semana" : "semanas";
                        string mensaje = $"Tu equipo lleva {Metodos.avisosBancarrota} {unidad} en bancarrota.\n\nLa situación económica del club es crítica. No hay fondos suficientes para cubrir los gastos básicos, y la deuda sigue aumentando.\n\nSi no logras revertir esta situación pronto, la directiva podría tomar medidas drásticas, incluida tu destitución como manager.";
                        frmVentanaDespido ventanaBancarrota = new frmVentanaDespido(titulo, mensaje);
                        ventanaBancarrota.ShowDialog();
                    }

                    // --------------------- COMPROBAR JUGADORES QUE PUEDEN SER AGREGADOS AL MERCADO DE TRANSFERIBLES Y ACTUALIZAR LESIONADOS
                    List<Jugador> listaJugadores = _logicaJugador.MostrarListaTotalJugadores();
                    int partidosEquipo = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, Metodos.hoy).Jornada - 1 ?? 0;
                    int partidosJugador = 0; 
                    int semanasLesionado = 0;
                    List<int> jugadoresTransferibles = new List<int>();
             
                    foreach (var jugador in listaJugadores)
                    {
                        // LESIONADOS
                        // Reducir el número de partidos lesionado si es mayor que 0
                        if (jugador.Lesion > 0)
                        {
                            _logicaJugador.PonerJugadorLesionado(jugador.IdJugador, jugador.Lesion - 1, jugador.TipoLesion);

                            // Sumar semana lesionado a la tabla estadisticas_jugadores
                            _logicaJugador.SumarSemanaLesionado(jugador.IdJugador);
                        }
                        else
                        {
                            if (jugador.LesionTratada != 0)
                            {
                                _logicaJugador.ActivarTratamientoLesion(jugador.IdJugador, 0);
                            }

                            if (jugador.TipoLesion != "")
                            {
                                _logicaJugador.QuitarTipoLesion(jugador.IdJugador);
                            }
                        }

                        // COMPROBAR TRANSFERIBLES
                        // Comprobar si esta en la lista de transferidos
                        List<Transferencia> listaTraspasos = _logicaTransferencia.ListarTraspasos();
                        bool traspasado = false;
                        foreach (var traspaso in listaTraspasos)
                        {
                            if (traspaso.IdJugador == jugador.IdJugador)
                            {
                                traspasado = true;
                                break;
                            }
                        }

                        // Si el jugador no pertenece a mi equipo, no es ya transferible y no esta en la lista de traspasos realizados...
                        if (jugador.IdEquipo != _equipo && jugador.SituacionMercado == 0 && traspasado == false)
                        {
                            semanasLesionado = _logicaJugador.SemanasLesionado(jugador.IdJugador);
                            partidosJugador = _logicaEstadisticas.MostrarEstadisticasJugador(jugador.IdJugador, _manager.IdManager).PartidosJugados;

                            // 📌 Sistema de Puntuación
                            int indiceTransferibilidad = 0;

                            // ✔ Factor 1: Edad y Proyeccion
                            if (jugador.Edad > 30 && partidosEquipo > 10 && partidosJugador <= (partidosEquipo * 0.25) && semanasLesionado <= (partidosEquipo * 0.25))
                            {
                                indiceTransferibilidad += 2;
                            }

                            // ✔ Factor 2: Rol vs Partidos Jugados
                            if (jugador.Status <= 2 && partidosEquipo > 10 && partidosJugador <= (partidosEquipo * 0.75) && semanasLesionado <= (partidosEquipo * 0.25))
                            {
                                indiceTransferibilidad += 2;
                            } 
                            else if(jugador.Status > 2 && partidosEquipo > 10 && partidosJugador <= (partidosEquipo * 0.25) && semanasLesionado <= (partidosEquipo * 0.25))
                            {
                                indiceTransferibilidad += 2;
                            }

                            // ✔ Factor 3: Valor Mercado vs Suplencia
                            int presupuestoEquipo = _logicaEquipo.ListarDetallesEquipo(jugador.IdEquipo).Presupuesto;
                            if (jugador.SalarioTemporada > (presupuestoEquipo * 0.015) && partidosEquipo > 10 && partidosJugador < (partidosEquipo * 0.25) && semanasLesionado < (partidosEquipo * 0.25))
                            {
                                indiceTransferibilidad += 2;
                            }

                            // ✔ Factor 4: Rendimiento y Moral
                            if (jugador.Moral < 30 && partidosEquipo > 10 && partidosJugador <= (partidosEquipo * 0.50))
                            {
                                indiceTransferibilidad += 1;
                            }

                            // ✔ Factor 5: Final de Contrato
                            if (jugador.AniosContrato == 1 && partidosEquipo > 10 && partidosJugador <= (partidosEquipo * 0.50))
                            {
                                indiceTransferibilidad += 1;
                            }

                            // ❌ Factor 6: Equipos europeos
                            int idEquipo = _logicaJugador.MostrarDatosJugador(jugador.IdJugador).IdEquipo;
                            int idComp = _logicaEquipo.ListarDetallesEquipo(idEquipo).IdCompeticion;
                            if (idComp ==5 || idComp ==6)
                            {
                                if (jugador.Status == 4)
                                {
                                    if (jugador.Moral < 60)
                                    {
                                        indiceTransferibilidad += 4;
                                    }
                                    else
                                    {
                                        indiceTransferibilidad += 2;
                                    }
                                }
                                else
                                {
                                    indiceTransferibilidad -= 4;
                                }       
                            }

                            // 📌 RESULTADOS DE TRANSFERIBILIDAD
                            if (indiceTransferibilidad >= 4)
                            {
                                jugadoresTransferibles.Add(jugador.IdJugador);
                            }
                        }
                    }

                    // Ponemos a los jugadores en la lista de transferibles.
                    foreach (var id in jugadoresTransferibles)
                    {
                        Random rnd = new Random();
                        int numero = rnd.Next(1, 3);
                        if (numero == 1)
                        {
                            _logicaJugador.PonerTransferible(id);
                        } else
                        {
                            _logicaJugador.PonerCedible(id);
                        }
                    
                    }
                }
            });
            btnAvanzar.Visibility = Visibility.Visible;
            progressBar.Visibility = Visibility.Collapsed;

            // --------------------- COMPROBAR AVISOS DE BANCARROTA
            if (Metodos.avisosBancarrota > 3)
            {
                // Despedir al manager en la Base de Datos
                _logicaManager.DespedirManager(_manager.IdManager);

                // Mostrar ventana de despido
                string titulo = "INFORMACIÓN";
                string mensaje = "El club ha decidido prescindir de tus servicios.\n\nTras varias semanas en una grave situación económica y sin haber logrado revertir la bancarrota, la directiva ha tomado la difícil decisión de destituirte como manager del equipo.\n\nA pesar de los esfuerzos realizados, la falta de liquidez, el impago de salarios y la presión institucional han provocado un desenlace inevitable.";
                frmVentanaDespido ventanaDespido = new frmVentanaDespido(titulo, mensaje);
                ventanaDespido.ShowDialog();

                Metodos.ReproducirSonidoTransicion();

                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.CargarPortada();
            } 
            else
            {
                // --------------------- REALIZAR TRASPASOS Y CESIONES SI LOS HAY
                List<Transferencia> traspasos = _logicaTransferencia.ListarTraspasos();
                foreach (var traspaso in traspasos)
                {
                    if (DateTime.Parse(traspaso.FechaTraspaso) == fechaHoy.AddDays(1))
                    {
                        // Cambiar jugador de equipo
                        _logicaJugador.CambiarDeEquipo(traspaso.IdJugador, traspaso.IdEquipoDestino);

                        if (traspaso.TipoFichaje == 1)
                        {
                            // Cambiar su contrato
                            _logicaJugador.CambiarContratoJugador(traspaso.IdJugador, traspaso.SalarioAnual, traspaso.ClausulaRescision, traspaso.Duracion, traspaso.BonoPorPartidos, traspaso.BonoPorGoles, traspaso.IdEquipoDestino);
                        }

                        // Comprobar si es un jugador que ficha mi equipo
                        if (traspaso.IdEquipoDestino == _equipo)
                        {
                            // Añadimos al jugador a la tabla alineaciones
                            _logicaJugador.AgregarJugadorAlineacion(traspaso.IdJugador);


                            // Añadimos al jugador a la lista de estadisticas
                            if (_logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoOrigen).IdCompeticion > 2)
                            {
                                _logicaEstadisticas.InsertarLineaEstadisticaJugador(traspaso.IdJugador, _manager.IdManager);
                            }
                        }

                        // Comprobar si es un jugador que vende mi equipo
                        if (traspaso.IdEquipoOrigen == _equipo)
                        {
                            // Quitar jugador de la alineacion
                            _logicaJugador.QuitarJugadorAlineacion(traspaso.IdJugador);
                        }

                        // Poner estadisticas del jugador a 0 si viene de otra division
                        int divisionOrigen = _logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoOrigen).IdCompeticion;
                        int divisionDestino = _logicaEquipo.ListarDetallesEquipo(traspaso.IdEquipoDestino).IdCompeticion;
                        if (divisionOrigen != divisionDestino)
                        {
                            _logicaEstadisticas.ResetearEstadisticaJugador(traspaso.IdJugador);
                        }

                        // Quitar jugador del mercado
                        _logicaJugador.QuitarJugadorDeMercado(traspaso.IdJugador);
                    }
                }

                // --------------------- COMPROBAR OFERTAS POR MIS JUGADORES
                if (fechaHoy.DayOfWeek == DayOfWeek.Monday)
                {
                    List<OfertaIA> ofertasParaMi = _logicaJugador.OfertasMiEquipo(_equipo);
                    if (ofertasParaMi != null)
                    {
                        foreach (var oferta in ofertasParaMi)
                        {
                            int presupuestoEquipoInteresado = _logicaEquipo.ListarDetallesEquipo(oferta.IdEquipoInteresado).Presupuesto;
                            int valorMercadoJugadorInteresado = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).ValorMercado;
                            int clausulaJugadorInteresado = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).ClausulaRescision ?? 0;
                            int estadoForma = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).EstadoForma;
                            int moral = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).Moral;
                            int potencial = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).Potencial;
                            int edad = _logicaJugador.MostrarDatosJugador(oferta.IdJugadorObjetivo).Edad;

                            int tipo;
                            int monto;

                            if (presupuestoEquipoInteresado * 0.5 >= clausulaJugadorInteresado && estadoForma >= 80 
                                && moral >= 80 && potencial >= 85 && edad <= 30)
                            {
                                // Pagan la cláusula si el jugador está en buen estado y tienen dinero
                                tipo = 1;
                                monto = clausulaJugadorInteresado;
                            }
                            else if (presupuestoEquipoInteresado * 0.7 >= valorMercadoJugadorInteresado)
                            {
                                // Hacen una oferta de traspaso negociable
                                tipo = 1;
                                // Generar un valor aleatorio dentro del rango
                                int montoBase = AleatorioEntre((int)(valorMercadoJugadorInteresado * 0.7), (int)(valorMercadoJugadorInteresado * 1.1));
                                // Redondear al múltiplo más cercano de 50,000
                                monto = ((montoBase + 25000) / 50000) * 50000;
                            }
                            else
                            {
                                // Intentan cesión si no tienen dinero
                                tipo = 2;
                                monto = 0;
                            }

                            _logicaTransferencia.AgregarOfertaRecibida(oferta.IdJugadorObjetivo, oferta.IdEquipoInteresado, tipo, monto, 0);
                        }
                    }
                }

                // --------------------- SI ES LUNES Y HAY CONTRATADO UN PSICÓLOGO AUMENTAR LA MORAL DE TODOS LOS JUGADORES 1 PUNTO
                if (fechaHoy.DayOfWeek == DayOfWeek.Monday)
                {
                    Empleado? psicologo = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Psicólogo");
                    if (psicologo != null)
                    {
                        List<Jugador> plantilla = _logicaJugador.ListadoJugadoresCompleto(_equipo);
                        foreach (var jugador in plantilla)
                        {
                            _logicaJugador.ActualizarMoralEstadoForma(jugador.IdJugador, 1, 0);
                        }
                    }
                }

                // --------------------- COMPROBAR SI HOY ACABA ALGUNA REMODELACIÓN Y QUITARLA DE LA BD E INCREMENTAR EL AFORO
                Remodelacion obraActivaDB = _logicaRemodelacion.ComprobarRemodelacion(_equipo, _manager.IdManager);
                if (obraActivaDB != null)
                {
                    if (obraActivaDB.FechaFinal == fechaHoy)
                    {
                        _logicaRemodelacion.EliminarRemodelacion(obraActivaDB.IdRemodelacion);

                        if (obraActivaDB.TipoRemodelacion == 1)
                        {
                            _logicaEquipo.ActualizarAforo(_equipo, 500);
                        }
                        else if (obraActivaDB.TipoRemodelacion == 2)
                        {
                            _logicaEquipo.ActualizarAforo(_equipo, 1000);
                        }
                        else if (obraActivaDB.TipoRemodelacion == 3)
                        {
                            _logicaEquipo.ActualizarAforo(_equipo, 1500);
                        }
                    }
                }

                // --------------------- COMPROBAR SI HOY ACABA ALGÚN PRÉSTAMO BANCARIO Y QUITARLO DE LA BD
                List<Prestamo> prestamosActuales = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);
                foreach (var prestamo in prestamosActuales)
                {
                    if (prestamo.SemanasRestantes == 0)
                    {
                        _logicaPrestamo.EliminarPrestamo(_manager.IdManager, _equipo, prestamo.Orden);
                    }
                }

                // --------------------- MOSTRAR LOS ABONADOS DE LA TEMPORADA E INGRESAR EL DINERO SI ES EL SEGUNDO LUNES DE AGOSTO
                DateTime fechaAbonados = ObtenerSegundoLunesDeAgosto(Metodos.temporadaActual);
                if (fechaHoy == fechaAbonados)
                {
                    // Crear numero de abonados
                    Random random = new Random();
                    int aforo = _logicaEquipo.ListarDetallesEquipo(_equipo).Aforo;
                    string objetivo = _logicaEquipo.ListarDetallesEquipo(_equipo).Objetivo;
                    int reputacion = _logicaEquipo.ListarDetallesEquipo(_equipo).Reputacion;

                    int maxSocios = (int)(aforo * 0.90); // límite máximo realista, por ejemplo, 95% del aforo total

                    // Factor Objetivo
                    double factorObjetivo = 1;
                    if (objetivo.Equals("Campeón"))
                    {
                        factorObjetivo = 0.70 + random.NextDouble() * (0.80 - 0.70); ;
                    }
                    else if (objetivo.Equals("Ascenso"))
                    {
                        factorObjetivo = 0.70 + random.NextDouble() * (0.75 - 0.70); ;
                    }
                    else if (objetivo.Equals("Zona Tranquila"))
                    {
                        factorObjetivo = 0.50 + random.NextDouble() * (0.75 - 0.50); ;
                    }
                    else if (objetivo.Equals("Descenso"))
                    {
                        factorObjetivo = 0.30 + random.NextDouble() * (0.75 - 0.30); ;
                    }

                    // Factor precio abonos segun reputacion
                    Taquilla? precios = _logicaTaquilla.RecuperarPreciosTaquilla(_equipo, _manager.IdManager);
                    double factorReputacion = 1.0;

                    // Definir los umbrales según reputación
                    int umbralGeneral, umbralTribuna, umbralVip;

                    if (reputacion >= 95)
                    {
                        umbralGeneral = 900;
                        umbralTribuna = 2000;
                        umbralVip = 3000;
                    }
                    else if (reputacion >= 80)
                    {
                        umbralGeneral = 700;
                        umbralTribuna = 1500;
                        umbralVip = 2000;
                    }
                    else if (reputacion >= 70)
                    {
                        umbralGeneral = 500;
                        umbralTribuna = 1000;
                        umbralVip = 1500;
                    }
                    else
                    {
                        umbralGeneral = 200;
                        umbralTribuna = 400;
                        umbralVip = 500;
                    }

                    // Evaluar el factor según precios
                    factorReputacion = precios.PrecioAbonoGeneral >= umbralGeneral ? 0.8 : 1.1;
                    factorReputacion = precios.PrecioAbonoTribuna >= umbralTribuna ? 0.8 : factorReputacion;
                    factorReputacion = precios.PrecioAbonoVip >= umbralVip ? 0.9 : factorReputacion;

                    int sociosTemporada = (int)(maxSocios * factorObjetivo * factorReputacion);

                    // Crear el mensaje con el numero de abonados de la temporada
                    Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
                    string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

                    Mensaje mensajeAbonados = new Mensaje
                    {
                        Fecha = Metodos.hoy,
                        Remitente = financiero != null ? financiero.Nombre : presidente,
                        Asunto = "Campaña de abonados terminada",
                        Contenido = $"Ha terminado la campaña de abonos. Se han abonado un total de {sociosTemporada.ToString("N0", new CultureInfo("es-ES"))} socios.\n\n¡No está nada mal, las expectativas del equipo parece que han convencido a los socios!",
                        TipoMensaje = "Notificación",
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Leido = false,
                        Icono = 0 // 0 es icono de equipo
                    };

                    _logicaMensajes.crearMensaje(mensajeAbonados);

                    // Ingresar el dinero de los abonos
                    double abonoGeneral = sociosTemporada * 0.60;
                    double abonoTribuna = sociosTemporada * 0.30;
                    double abonoVip = sociosTemporada * 0.10;

                    int? precioAbonoGeneral = precios.PrecioAbonoGeneral ?? 0;
                    int? precioAbonoTribuna = precios.PrecioAbonoTribuna ?? 0;
                    int? precioAbonoVip = precios.PrecioAbonoVip ?? 0;

                    int ingresoTotal = (int)((abonoGeneral * precioAbonoGeneral) + (abonoTribuna * precioAbonoTribuna) + (abonoVip * precioAbonoVip));

                    // Crear el ingreso
                    Finanza nuevoIngreso = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 5,
                        Tipo = 1,
                        Cantidad = ingresoTotal,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearIngreso(nuevoIngreso);

                    // Sumar los ingresos al Presupuesto
                    _logicaEquipo.SumarCantidadAPresupuesto(_equipo, ingresoTotal);

                    // Deshabilitar el boton de abonados
                    Metodos.BotonAbonos = false;
                }

                // --------------------- REALIZAR COBROS DE LOS SALARIOS SI ES 1 DE MES
                if (fechaHoy.Day == 1)
                {
                    // SALARIO EMPLEADOS
                    List<Empleado> listaEmpleadosContratados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);
                    foreach (var empleado in listaEmpleadosContratados)
                    {
                        int mensualidadE = (int)empleado.Salario / 12;
                        Finanza nuevoGasto = new Finanza
                        {
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Temporada = Metodos.temporadaActual.ToString(),
                            IdConcepto = 13,
                            Tipo = 2,
                            Cantidad = mensualidadE,
                            Fecha = Metodos.hoy.Date
                        };
                        _logicaFinanza.CrearGasto(nuevoGasto);

                        // Restar la indemnización al Presupuesto
                        _logicaEquipo.RestarCantidadAPresupuesto(_equipo, mensualidadE);
                    }

                    // JUGADORES
                    List<Jugador> listaJugadoresContratados = _logicaJugador.ListadoJugadoresCompleto(_equipo);
                    foreach (var jugador in listaJugadoresContratados)
                    {
                        // Salario
                        int mensualidadJ = (int)jugador.SalarioTemporada / 12;
                        Finanza nuevoGasto = new Finanza
                        {
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Temporada = Metodos.temporadaActual.ToString(),
                            IdConcepto = 11,
                            Tipo = 2,
                            Cantidad = mensualidadJ,
                            Fecha = Metodos.hoy.Date
                        };
                        _logicaFinanza.CrearGasto(nuevoGasto);

                        // Restar la indemnización al Presupuesto
                        _logicaEquipo.RestarCantidadAPresupuesto(_equipo, mensualidadJ);
                    }
                }

                // --------------------- REALIZAR COBROS DE PRÉSTAMOS Y REMODELACIONES (SI LOS HUBIERA) SI ES LUNES
                if (fechaHoy.DayOfWeek == DayOfWeek.Monday)
                {
                    // PRESTAMOS
                    List<Prestamo> prestamosActivos = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);
                    foreach (var prestamo in prestamosActivos)
                    {
                        int pagoPrestamo = prestamo.PagoSemanal;
                        Finanza nuevoGasto = new Finanza
                        {
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Temporada = Metodos.temporadaActual.ToString(),
                            IdConcepto = 17,
                            Tipo = 2,
                            Cantidad = pagoPrestamo,
                            Fecha = Metodos.hoy.Date
                        };
                        _logicaFinanza.CrearGasto(nuevoGasto);

                        // Restar la cantidad semanal al Presupuesto
                        _logicaEquipo.RestarCantidadAPresupuesto(_equipo, pagoPrestamo);

                        // Restar una semana al prestamo
                        _logicaPrestamo.RestarSemana(prestamo.Orden);
                    }

                    // REMODELACIONES
                    Remodelacion obraActiva = _logicaRemodelacion.ComprobarRemodelacion(_equipo, _manager.IdManager);
                    Finanza nuevoGastoRemodelacion = null;
                    int pagoRemodelacion = 0;

                    if (obraActiva != null)
                    {
                        if (obraActiva.TipoRemodelacion == 1)
                        {
                            pagoRemodelacion = 250000;
                            nuevoGastoRemodelacion = new Finanza
                            {
                                IdEquipo = _equipo,
                                IdManager = _manager.IdManager,
                                Temporada = Metodos.temporadaActual.ToString(),
                                IdConcepto = 15,
                                Tipo = 2,
                                Cantidad = pagoRemodelacion,
                                Fecha = Metodos.hoy.Date
                            };
                        }
                        else if (obraActiva.TipoRemodelacion == 2)
                        {
                            pagoRemodelacion = 425000;
                            nuevoGastoRemodelacion = new Finanza
                            {
                                IdEquipo = _equipo,
                                IdManager = _manager.IdManager,
                                Temporada = Metodos.temporadaActual.ToString(),
                                IdConcepto = 15,
                                Tipo = 2,
                                Cantidad = pagoRemodelacion,
                                Fecha = Metodos.hoy.Date
                            };
                        }
                        else if (obraActiva.TipoRemodelacion == 3)
                        {
                            pagoRemodelacion = 600000;
                            nuevoGastoRemodelacion = new Finanza
                            {
                                IdEquipo = _equipo,
                                IdManager = _manager.IdManager,
                                Temporada = Metodos.temporadaActual.ToString(),
                                IdConcepto = 15,
                                Tipo = 2,
                                Cantidad = pagoRemodelacion,
                                Fecha = Metodos.hoy.Date
                            };
                        }
                        _logicaFinanza.CrearGasto(nuevoGastoRemodelacion);

                        // Restar la indemnización al Presupuesto
                        _logicaEquipo.RestarCantidadAPresupuesto(_equipo, pagoRemodelacion);
                    }
                }

                // --------------------- REALIZAR LOS INGRESOS DE PATROCINADOR Y TELEVISIÓN SI ES EL DÍA 1 DEL MES
                if (fechaHoy.Day == 1)
                {
                    // PATROCINADOR
                    Patrocinador? patrocinador = _logicaPatrocinador.PatrocinadoresContratados(_manager.IdManager, _equipo);

                    if (patrocinador != null)
                    {
                        int mensualidadPatrocinador = (int)patrocinador.Mensualidad;

                        Finanza nuevoIngresoPatrocinio = new Finanza
                        {
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Temporada = Metodos.temporadaActual.ToString(),
                            IdConcepto = 6,
                            Tipo = 1,
                            Cantidad = mensualidadPatrocinador,
                            Fecha = Metodos.hoy.Date
                        };
                        _logicaFinanza.CrearIngreso(nuevoIngresoPatrocinio);

                        // Restar la indemnización al Presupuesto
                        _logicaEquipo.SumarCantidadAPresupuesto(_equipo, mensualidadPatrocinador);
                    }

                    // CADENA TV
                    Television? television = _logicaTelevision.TelevisionesContratadas(_manager.IdManager, _equipo);

                    if (television != null)
                    {
                        int mensualidadTelevision = (int)television.Mensualidad;

                        Finanza nuevoIngresoTelevision = new Finanza
                        {
                            IdEquipo = _equipo,
                            IdManager = _manager.IdManager,
                            Temporada = Metodos.temporadaActual.ToString(),
                            IdConcepto = 3,
                            Tipo = 1,
                            Cantidad = mensualidadTelevision,
                            Fecha = Metodos.hoy.Date
                        };
                        _logicaFinanza.CrearIngreso(nuevoIngresoTelevision);

                        // Restar la indemnización al Presupuesto
                        _logicaEquipo.SumarCantidadAPresupuesto(_equipo, mensualidadTelevision);
                    }
                }

                // --------------------- COMPROBAR SI MI EQUIPO TIENE PARTIDO 
                Partido miPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, Metodos.hoy);

                if (miPartido != null && miPartido.FechaPartido == Metodos.hoy)
                {
                    int cont = 0;
                    // Comprobamos si hay jugadores lesionados o sancionados en la alineacion titular en partidos de Liga
                    List<Jugador> alineacion = _logicaJugador.MostrarAlineacion(1, 11);

                    if (miPartido.IdCompeticion >= 1 && miPartido.IdCompeticion >= 2)
                    {
                        foreach (var jugador in alineacion)
                        {
                            if (jugador.Lesion > 0 || jugador.Sancionado > 0)
                            {
                                cont++;
                            }
                        }
                    }
                    else
                    {
                        foreach (var jugador in alineacion)
                        {
                            if (jugador.Lesion > 0)
                            {
                                cont++;
                            }
                        }
                    }

                    if (cont > 0)
                    {
                        // Mostrar ventana avisando de que la alineacion es incorrecta
                        string titulo = "INFORMACIÓN";
                        string mensaje = "Por favor revisa la alineación, has incluido jugadores que están lesionados o sancionados y no pueden jugar el partido.";
                        frmVentanaEmergenteDosBotones ventanaAlineacionIncorrecta = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaAlineacionIncorrecta.ShowDialog();
                    }
                    else
                    {
                        // Cargar Pantalla de Simulacion de MI PARTIDO
                        frmResumenPartido ventanaResumenPartido = new frmResumenPartido(_manager, _equipo, miPartido);
                        ventanaResumenPartido.ShowDialog();

                        // Comprobar si la Copa Nacional ha terminado
                        if (ventanaResumenPartido.copaFinalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa
                            frmResumenCopaNacional ventanaResumenCopaNacional = new frmResumenCopaNacional(_manager, _equipo);
                            ventanaResumenCopaNacional.ShowDialog();
                        }

                        // Comprobar si la Copa de Europa 1 ha terminado
                        if (ventanaResumenPartido.copaEuropa1Finalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa Europa 1
                            frmResumenCopaEuropa1 ventanaResumenCopaEuropa1 = new frmResumenCopaEuropa1(_manager, _equipo);
                            ventanaResumenCopaEuropa1.ShowDialog();
                        }

                        // Comprobar si la Copa de Europa 2 ha terminado
                        if (ventanaResumenPartido.copaEuropa2Finalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa Europa 2
                            frmResumenCopaEuropa2 ventanaResumenCopaEuropa2 = new frmResumenCopaEuropa2(_manager, _equipo);
                            ventanaResumenCopaEuropa2.ShowDialog();
                        }

                        // Comprobamos si hay otros partidos hoy
                        List<Partido> listaPartidos = _logicaPartidos.PartidosHoy(_equipo, _manager.IdManager);
                        if (listaPartidos != null && listaPartidos.Count > 0)
                        {
                            // Cargar Ventana de Simulacion de partidos
                            frmSimulandoPartidos ventanaSimulacion = new frmSimulandoPartidos(_manager, _equipo, listaPartidos);
                            ventanaSimulacion.ShowDialog();

                            // Avanzamos un dia en el calendario
                            _datosFecha.AvanzarUnDia();

                            // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                            await Task.Delay(50);

                            // Recargamos la fecha
                            CargarFecha();

                            // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                            if (DockPanel_Central.Children.Count > 0)
                            {
                                DockPanel_Central.Children.Clear();
                            }
                            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                            DockPanel_Central.Children.Add(homeMenuPrincipal);

                            if (ventanaSimulacion.copaFinalizada == 1)
                            {
                                // Cargar Pantalla de Final de Copa
                                frmResumenCopaNacional ventanaResumenCopaNacional = new frmResumenCopaNacional(_manager, _equipo);
                                ventanaResumenCopaNacional.ShowDialog();
                            }
                        }
                        else
                        {
                            // Avanzamos un dia en el calendario
                            _datosFecha.AvanzarUnDia();

                            // Recargamos la fecha
                            CargarFecha();

                            // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                            if (DockPanel_Central.Children.Count > 0)
                            {
                                DockPanel_Central.Children.Clear();
                            }
                            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                            DockPanel_Central.Children.Add(homeMenuPrincipal);
                        }
                    }
                }
                else
                {
                    // --------------------- COMPROBAR SI HAY PARTIDOS ESTE DÍA Y SIMULARLOS
                    List<Partido> listaPartidos = _logicaPartidos.PartidosHoy(_equipo, _manager.IdManager);

                    if (listaPartidos != null && listaPartidos.Count > 0)
                    {
                        // Cargar Ventana de Simulacion de partidos
                        frmSimulandoPartidos ventanaSimulacion = new frmSimulandoPartidos(_manager, _equipo, listaPartidos);
                        ventanaSimulacion.ShowDialog();

                        // Avanzamos un dia en el calendario
                        _datosFecha.AvanzarUnDia();

                        // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                        await Task.Delay(50);

                        // Recargamos la fecha
                        CargarFecha();

                        // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                        if (DockPanel_Central.Children.Count > 0)
                        {
                            DockPanel_Central.Children.Clear();
                        }
                        UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                        DockPanel_Central.Children.Add(homeMenuPrincipal);

                        if (ventanaSimulacion.copaFinalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa
                            frmResumenCopaNacional ventanaResumenCopaNacional = new frmResumenCopaNacional(_manager, _equipo);
                            ventanaResumenCopaNacional.ShowDialog();
                        }

                        // Comprobar si la Copa de Europa 1 ha terminado
                        if (ventanaSimulacion.copaEuropa1Finalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa Europa 1
                            frmResumenCopaEuropa1 ventanaResumenCopaEuropa1 = new frmResumenCopaEuropa1(_manager, _equipo);
                            ventanaResumenCopaEuropa1.ShowDialog();
                        }

                        // Comprobar si la Copa de Europa 2 ha terminado
                        if (ventanaSimulacion.copaEuropa2Finalizada == 1)
                        {
                            // Cargar Pantalla de Final de Copa Europa 2
                            frmResumenCopaEuropa2 ventanaResumenCopaEuropa2 = new frmResumenCopaEuropa2(_manager, _equipo);
                            ventanaResumenCopaEuropa2.ShowDialog();
                        }
                    }
                    else
                    {
                        // Avanzamos un dia en el calendario
                        _datosFecha.AvanzarUnDia();

                        // Esperamos un momento para asegurarnos de que la base de datos ya procesó el cambio
                        await Task.Delay(50);

                        // Recargamos la fecha
                        CargarFecha();

                        // CARGAR EL CONTENIDO DEL PANEL PRINCIPAL
                        if (DockPanel_Central.Children.Count > 0)
                        {
                            DockPanel_Central.Children.Clear();
                        }
                        UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
                        DockPanel_Central.Children.Add(homeMenuPrincipal);
                    }
                }

                // --------------------- COMPROBAMOS SI LA FECHA DE HOY ES MAYOR QUE EL ÚLTIMO PARTIDO DEL CALENDARIO
                if (DateTime.TryParse(_logicaPartidos.ultimoPartidoCalendario(), out DateTime ultimoPartido))
                {
                    DateTime hoy = Metodos.hoy;

                    if (hoy > ultimoPartido)
                    {
                        int miCompeticion = _logicaEquipo.ListarDetallesEquipo(_equipo).IdCompeticion;
                        List<Clasificacion> clasificacion = _logicaClasificacion.MostrarClasificacion(miCompeticion, _manager.IdManager);
                        int miEquipoId = _equipo;
                        int posicion = clasificacion.FirstOrDefault(c => c.IdEquipo == miEquipoId)?.Posicion ?? 0;

                        // Cargar Pantalla de Final de Temporada
                        frmResumenTemporada ventanaResumenTemporada = new frmResumenTemporada(_manager, _equipo, clasificacion, posicion);
                        ventanaResumenTemporada.ShowDialog();

                        // Comprobar si se ha conseguido el Objetivo de Temporada
                        string objetivo = _logicaEquipo.ListarDetallesEquipo(_equipo).Objetivo;
                        CalcularObjetivoTemporada(objetivo, posicion, miCompeticion);

                        Manager yo = _logicaManager.MostrarManager(_manager.IdManager);
                        if (yo.CDirectiva == 0 || yo.CFans == 0 || yo.CJugadores == 0)
                        {
                            // Despedir al manager en la Base de Datos
                            _logicaManager.DespedirManager(_manager.IdManager);

                            // Mostrar ventana de despido
                            string titulo = "INFORMACIÓN";
                            string mensaje = "El club ha decidido prescindir de los servicios de su entrenador.\n\nLos recientes resultados no han estado a la altura de las expectativas, y la directiva considera necesario un cambio de rumbo para reconducir el club.\n\nAgradecemos su dedicación y profesionalidad durante su etapa al frente del equipo, y le deseamos suerte en sus futuros proyectos.";
                            frmVentanaDespido ventanaDespido = new frmVentanaDespido(titulo, mensaje);
                            ventanaDespido.ShowDialog();

                            Metodos.ReproducirSonidoTransicion();

                            var mainWindow = (MainWindow)Application.Current.MainWindow;
                            mainWindow.CargarPortada();
                        }
                        else
                        {
                            btnAvanzar.Visibility = Visibility.Collapsed;
                            progressBar.Visibility = Visibility.Visible;

                            await Task.Run(() =>
                            {
                                // Actualizar la tabla historial_manager
                                _logicaHistorial.CopiarPartidosHistorialManager(Metodos.temporadaActual);
                                _logicaHistorial.CopiarConfianzasManager(Metodos.temporadaActual);
                                _logicaHistorial.CopiarPosicionLigaManager(Metodos.temporadaActual, posicion);

                                // Actualizar las tablas palmares, palmares_manager e historial_finales
                                // PALMARES
                                foreach (var equipo in clasificacion)
                                {
                                    if (equipo.Posicion == 1)
                                    {
                                        _logicaPalmares.AnadirTituloCampeon(equipo.IdEquipo);
                                    }
                                }

                                // PALMARES_MANAGER
                                if (posicion == 1) // Si he ganado la liga...
                                {
                                    _logicaPalmares.AnadirTituloManager(1, _equipo, _manager.IdManager, Metodos.temporadaActual);
                                }

                                // REPUTACION MANAGER
                                if (posicion == 1)
                                {
                                    _logicaManager.ActualizarReputacion(_manager.IdManager, 25);
                                }
                                else if (posicion <= 4)
                                {
                                    _logicaManager.ActualizarReputacion(_manager.IdManager, 10);
                                }
                                else if (posicion >= 16)
                                {
                                    _logicaManager.ActualizarReputacion(_manager.IdManager, -10);
                                }
                                else
                                {
                                    _logicaManager.ActualizarReputacion(_manager.IdManager, 5);
                                }

                                // HISTORIAL FINALES
                                int campeon = 0;
                                int finalista = 0;
                                foreach (var equipo in clasificacion)
                                {
                                    if (equipo.Posicion == 1)
                                    {
                                        campeon = equipo.IdEquipo;
                                    }
                                    if (equipo.Posicion == 2)
                                    {
                                        finalista = equipo.IdEquipo;
                                    }
                                }
                                _logicaPalmares.AnadirCampeonFinalista(Metodos.temporadaActual, campeon, finalista);

                                // Balon de Oro
                                balonOro = _logicaEstadisticas.BalonDeOro();

                                // Bota de Oro
                                botaOro = _logicaEstadisticas.BotaDeOro();

                                // Mejor 11 de la Temporada
                                mejorOnce = _logicaEstadisticas.MejorOnceTemporada();

                                // Crear clasificaciones para descensos y ascensos
                                List<Clasificacion> clasificacion1Final = _logicaClasificacion.MostrarClasificacion(1, _manager.IdManager);
                                List<Clasificacion> clasificacion2Final = _logicaClasificacion.MostrarClasificacion2(2, _manager.IdManager);
                                List<Clasificacion> clasificacionEuropa1Final = _logicaClasificacion.MostrarClasificacionCopaEuropa1(5, _manager.IdManager);
                                List<Clasificacion> clasificacionEuropa2Final = _logicaClasificacion.MostrarClasificacionCopaEuropa2(6, _manager.IdManager);
                                List<Equipo> listaReservas = _logicaEquipo.ListarEquiposCompeticion(3);

                                // Resetear Moral y Estado de Forma a 50
                                _logicaJugador.ResetearMoralEstadoForma();

                                // Crear Nueva temporada
                                _datosFecha.AvanzarUnAnio();
                                Metodos.temporadaActual = _datosFecha.ObtenerFechaHoy().Anio;
                                _datosFecha.AvanzarFecha(Metodos.temporadaActual);
                                Metodos.hoy = DateTime.Parse(_datosFecha.ObtenerFechaHoy().Hoy);

                                // Descender a Division 2
                                int totalEquipos = clasificacion1Final.Count();
                                foreach (var equipo in clasificacion1Final)
                                {
                                    if (equipo.Posicion <= 2)
                                    {
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Campeón");
                                    }
                                    if (equipo.Posicion >= 5 && equipo.Posicion <= 15)
                                    {
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Zona Tranquila");
                                    }
                                    if (equipo.Posicion > (totalEquipos - 4) && equipo.Posicion <= totalEquipos)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipo(equipo.IdEquipo, 2);
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Ascenso");

                                        if (equipo.IdEquipo == _equipo) // Mi equipo desciende a Division 2
                                        {
                                            // Crear el mensaje de descenso de equipo
                                            Empleado? delegado = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Delegado");
                                            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

                                            Mensaje mensajeDescenso = new Mensaje
                                            {
                                                Fecha = Metodos.hoy,
                                                Remitente = delegado != null ? delegado.Nombre : presidente,
                                                Asunto = "Descenso confirmado",
                                                Contenido = $"El club ha certificado su descenso a {_logicaCompeticion.MostrarNombreCompeticion(2)} tras una temporada decepcionante.\nSi bien reconocemos tu trabajo y esfuerzo, los resultados no han acompañado. Esperamos una reunión contigo en los próximos días para analizar la situación y valorar tu continuidad al frente del proyecto.",
                                                TipoMensaje = "Notificación",
                                                IdEquipo = _equipo,
                                                IdManager = _manager.IdManager,
                                                Leido = false,
                                                Icono = 0 // 0 es icono de equipo
                                            };

                                            _logicaMensajes.crearMensaje(mensajeDescenso);
                                        }
                                    }
                                }

                                // Ascender a Division 1
                                foreach (var equipo in clasificacion2Final)
                                {
                                    if (equipo.Posicion >= 1 && equipo.Posicion <= 4)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipo(equipo.IdEquipo, 1);
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Descenso");

                                        if (equipo.IdEquipo == _equipo) // Mi equipo asciende a Division 1
                                        {
                                            // Crear el mensaje de ascenso de equipo
                                            Empleado? delegado = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Delegado");
                                            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

                                            Mensaje mensajeAscenso = new Mensaje
                                            {
                                                Fecha = Metodos.hoy,
                                                Remitente = delegado != null ? delegado.Nombre : presidente,
                                                Asunto = "Ascenso confirmado",
                                                Contenido = $"¡Felicidades! El club ha logrado el ascenso y tú has sido una pieza clave en este éxito.\nLa ciudad celebra, la afición sueña y la directiva ya piensa en lo que viene.\nNos gustaría contar contigo para liderar al equipo en esta nueva aventura en {_logicaCompeticion.MostrarNombreCompeticion(1)}.",
                                                TipoMensaje = "Notificación",
                                                IdEquipo = _equipo,
                                                IdManager = _manager.IdManager,
                                                Leido = false,
                                                Icono = 0 // 0 es icono de equipo
                                            };

                                            _logicaMensajes.crearMensaje(mensajeAscenso);
                                        }
                                    }
                                    if (equipo.Posicion >= 7 && equipo.Posicion <= 15)
                                    {
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Zona Tranquila");
                                    }
                                    if (equipo.Posicion > 4 && equipo.Posicion < 7)
                                    {
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Ascenso");
                                    }
                                }

                                // Descender a Reservas
                                int totalEquipos2 = clasificacion2Final.Count();
                                foreach (var equipo in clasificacion2Final)
                                {
                                    if (equipo.Posicion > (totalEquipos - 4) && equipo.Posicion <= totalEquipos2)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipo(equipo.IdEquipo, 3);
                                        _logicaEquipo.CambiarObjetivoTemporada(equipo.IdEquipo, "Ascenso");

                                        if (equipo.IdEquipo == _equipo) // Mi equipo desciende a Reservas
                                        {
                                            // Despedir al manager en la Base de Datos
                                            _logicaManager.DespedirManager(_manager.IdManager);

                                            // Mostrar ventana de despido
                                            string titulo = "INFORMACIÓN";
                                            string mensaje = $"El club ha decidido prescindir de los servicios de su entrenador.\n\nLos recientes resultados no han estado a la altura de las expectativas ya que el equipo ha consagrado el descenso en {_logicaCompeticion.MostrarNombreCompeticion(2)} abandonando el fútbol profesional, y la directiva considera necesario un cambio de rumbo para reconducir el club.\n\nAgradecemos su dedicación y profesionalidad durante su etapa al frente del equipo, y le deseamos suerte en sus futuros proyectos.";
                                            frmVentanaDespido ventanaDespido = new frmVentanaDespido(titulo, mensaje);
                                            ventanaDespido.ShowDialog();

                                            Metodos.ReproducirSonidoTransicion();

                                            var mainWindow = (MainWindow)Application.Current.MainWindow;
                                            mainWindow.CargarPortada();
                                        }
                                    }
                                }

                                // Ascender a Division 2
                                Random random = new Random();

                                List<Equipo> equiposSeleccionados = listaReservas // Seleccionar 4 equipos aleatorios sin repetir
                                    .OrderBy(e => random.Next())
                                    .Take(4)
                                    .ToList();

                                List<int> idsSeleccionados = equiposSeleccionados // Obtener solo los IdEquipo
                                    .Select(e => e.IdEquipo)
                                    .ToList();

                                foreach (var equipo in idsSeleccionados)
                                {
                                    _logicaEquipo.AscenderDescenderEquipo(equipo, 2);
                                    _logicaEquipo.CambiarObjetivoTemporada(equipo, "Descenso");
                                }

                                // Descender a Copa de Europa 2
                                int totalEquiposEuropa1 = clasificacionEuropa1Final.Count();
                                foreach (var equipo in clasificacionEuropa1Final)
                                {
                                    if (equipo.Posicion > (totalEquipos - 4) && equipo.Posicion <= totalEquipos)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipoEuropa(equipo.IdEquipo, 6);                                   
                                    }
                                }

                                // Ascender a Copa de Europa 1
                                int totalEquiposEuropa2 = clasificacionEuropa2Final.Count();
                                foreach (var equipo in clasificacionEuropa2Final)
                                {
                                    if (equipo.Posicion >= 1 && equipo.Posicion <= 4)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipoEuropa(equipo.IdEquipo, 5);
                                    }
                                }

                                // Actualizar equipos que juegan Europa en la Liga local
                                int totalEquiposLiga1 = clasificacion1Final.Count();
                                foreach (var equipo in clasificacion1Final)
                                {
                                    if (equipo.Posicion >= 1 && equipo.Posicion <= 4)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipoEuropa(equipo.IdEquipo, 5);
                                    }
                                    else if (equipo.Posicion >= 5 && equipo.Posicion <= 6)
                                    {
                                        _logicaEquipo.AscenderDescenderEquipoEuropa(equipo.IdEquipo, 6);
                                    }
                                    else
                                    {
                                        _logicaEquipo.AscenderDescenderEquipoEuropa(equipo.IdEquipo, 0);
                                    }
                                }

                                // Resetear tablas clasificacion, estadisticas_jugadores, historial_manager_temp, partidos y ofertas
                                _logicaClasificacion.ResetearClasificacion(1);
                                _logicaClasificacion.ResetearClasificacion(2);
                                _logicaClasificacion.ResetearClasificacion(5);
                                _logicaClasificacion.ResetearClasificacion(6);
                                _logicaEstadisticas.ResetearEstadisticas();
                                _logicaEstadisticas.ResetearEstadisticasEuropa();
                                _logicaHistorial.ResetearHistorialTemporal();
                                _logicaPartidos.ResetearPartidos();

                                // Crear el calendario de las Ligas
                                int temporadaActual = Metodos.temporadaActual;
                                _logicaPartidos.GenerarCalendario(temporadaActual, _manager.IdManager, 1);
                                _logicaPartidos.GenerarCalendario(temporadaActual, _manager.IdManager, 2);

                                // Generar el calendario de Copa nacional
                                List<Equipo> listaEquipos = _logicaEquipo.ListarTodosLosEquipos();
                                GeneralCalendarioCopa(listaEquipos);

                                // Generar el calendario de Champions
                                List<Equipo> equiposChampions = _logicaEquipo.EquiposJueganEuropa1(1)
                                                                .Concat(_logicaEquipo.ListarEquiposCompeticion(5))
                                                                .OrderBy(e => Guid.NewGuid())
                                                                .ToList();
                                _logicaPartidos.GenerarCalendarioChampions(equiposChampions, 5, _manager.IdManager, ObtenerPrimerMartesOctubre(Metodos.temporadaActual));

                                // Generar el calendario de Europa League
                                List<Equipo> equiposUefa = _logicaEquipo.EquiposJueganEuropa2(1)
                                                                .Concat(_logicaEquipo.ListarEquiposCompeticion(6))
                                                                .OrderBy(e => Guid.NewGuid())
                                                                .ToList();
                                _logicaPartidos.GenerarCalendarioChampions2(equiposUefa, 6, _manager.IdManager, ObtenerPrimerJuevesOctubre(Metodos.temporadaActual));

                                // Generar las clasificaciones
                                _logicaClasificacion.RellenarClasificacion(1, _manager.IdManager);
                                _logicaClasificacion.RellenarClasificacion2(2, _manager.IdManager);
                                _logicaClasificacion.RellenarClasificacionEuropa1(_manager.IdManager, equiposChampions);
                                _logicaClasificacion.RellenarClasificacionEuropa2(_manager.IdManager, equiposUefa);

                                // Generar el primer registro del historial
                                string temporadaFormateada = $"{temporadaActual}/{temporadaActual + 1}";
                                _logicaHistorial.CrearLineaHistorial(_manager.IdManager, _equipo, temporadaFormateada);

                                // Vaciar la tabla taquilla
                                _logicaTaquilla.EstablecerPrecioAbonos(_equipo, _manager.IdManager, 0, 0, 0);

                                // Restar un año al contrato del patrocinador y de la televisión.
                                Patrocinador? patrocinador = _logicaPatrocinador.PatrocinadoresContratados(_manager.IdManager, _equipo);
                                Television? television = _logicaTelevision.TelevisionesContratadas(_manager.IdManager, _equipo);

                                if (patrocinador != null)
                                {
                                    if (patrocinador.DuracionContrato > 1)
                                    {
                                        _logicaPatrocinador.RestarAnioPatrocinador(patrocinador.IdPatrocinador);
                                    }
                                    else
                                    {
                                        _logicaPatrocinador.CancelarPatrocinador(patrocinador.IdPatrocinador);
                                    }
                                }

                                if (television != null)
                                {
                                    if (television.DuracionContrato > 1)
                                    {
                                        _logicaTelevision.RestarAnioCadenaTV(television.IdTelevision);
                                    }
                                    else
                                    {
                                        _logicaTelevision.CancelarCadenaTV(television.IdTelevision);
                                    }
                                }

                                // Crear los mensaje de inicio de partida
                                Mensaje mensajeNuevaTemporada = new Mensaje
                                {
                                    Fecha = new DateTime(temporadaActual, 7, 15),
                                    Remitente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente,
                                    Asunto = "Nueva Temporada",
                                    Contenido = "Desde la Directiva del " + _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre + " te damos la bienvenida a una nueva temporada.\n\nLa junta directiva y los empleados te irán informando a través de correos electrónicos de las cosas que sucedan en el club.",
                                    TipoMensaje = "Notificación",
                                    IdEquipo = _equipo,
                                    IdManager = _manager.IdManager,
                                    Leido = false,
                                    Icono = 0 // 0 es icono de equipo
                                };

                                _logicaMensajes.crearMensaje(mensajeNuevaTemporada);

                                // Activar el boton de abonados
                                Metodos.BotonAbonos = true;
                            });

                            // Cargar Pantalla de Premios de Jugadores
                            frmVentanaPremioJugadores ventanaPremiosJugadores = new frmVentanaPremioJugadores(balonOro, botaOro, mejorOnce, _equipo);
                            ventanaPremiosJugadores.ShowDialog();

                            // Resetear y Generar la alineacion del equipo
                            _logicaJugador.ResetearAlineacion();
                            _logicaJugador.CrearAlineacion(_equipo);

                            CargarFecha();

                            progressBar.Visibility = Visibility.Collapsed;
                            btnAvanzar.Visibility = Visibility.Visible;

                            Metodos.ReproducirSonidoTransicion();

                            // Notificar a MainWindow para cargar el nuevo UserControl
                            var mainWindow = (MainWindow)Application.Current.MainWindow;
                            mainWindow.CargarPretemporada(_manager, _equipo);
                        }
                    }
                }
            }
            txtPresupuesto.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Presupuesto.ToString("N0") + " €";
            DockPanel_Submenu.Children.Clear();

            // Comprobar mensajes nuevos
            int mensajesNoLeidos = _logicaMensajes.MensajesNoLeidos(_manager.IdManager);
            if (mensajesNoLeidos > 0)
            {
                imgCorreo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/correoNuevo_icon.png"));
            }
            else
            {
                imgCorreo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/correo_icon.png"));
            }
        }

        // ----------------------------------------------------------------------------- Evento CLICK del botón AJUSTES
        private void imgAjustes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Reproducir el sonido de clic
            Metodos.ReproducirSonidoClick();

            frmMenuOpciones ventanaEmergente = new frmMenuOpciones();
            ventanaEmergente.ShowDialog();
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón HOME
        private void imgHome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Home menuPrincipal = new UC_Menu_Home();
            DockPanel_Submenu.Children.Add(menuPrincipal);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Home_MenuPrincipal homeMenuPrincipal = new UC_Menu_Home_MenuPrincipal(_manager, _equipo);
            DockPanel_Central.Children.Add(homeMenuPrincipal);

        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón EQUIPO
        private void imgClub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Club menuClub = new UC_Menu_Club();
            DockPanel_Submenu.Children.Add(menuClub);

            // Suscribirse a los eventos
            menuClub.MostrarInformacion += CargarClubInformacion;
            menuClub.MostrarPlantilla += CargarClubPlantilla;
            menuClub.MostrarEmpleados += CargarClubEmpleados;
            menuClub.MostrarLesionados += CargarClubLesionados;

            // Cambiar el color del texto
            menuClub.lblInformacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón ENTRENADOR
        private void imgEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Entrenador menuEntrenador = new UC_Menu_Entrenador();
            DockPanel_Submenu.Children.Add(menuEntrenador);

            menuEntrenador.MostrarAlineacion += CargarEntrenadorAlineacion;
            menuEntrenador.MostrarEntrenamiento += CargarEntrenadorEntrenamiento;
            menuEntrenador.MostrarRival += CargarEntrenadorRival;

            // Cambiar el color del texto
            menuEntrenador.lblAlineacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón COMPETICION
        private void imgCompeticiones_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Competicion menuCompeticion = new UC_Menu_Competicion();
            DockPanel_Submenu.Children.Add(menuCompeticion);

            // Suscribirse a los eventos
            menuCompeticion.MostrarClasificacion += CargarCompeticionClasificacion;
            menuCompeticion.MostrarResultados += CargarCompeticionResultados;
            menuCompeticion.MostrarEstadisticas += CargarCompeticionEstadisticas;
            menuCompeticion.MostrarPalmares += CargarCompeticionPalmares;
            menuCompeticion.MostrarPalmaresJugadores += CargarCompeticionPalmaresJugadores;

            // Cambiar el color del texto
            menuCompeticion.lblClasificacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion clasificacionPrincipal = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clasificacionPrincipal);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón CALENDARIO 
        private void imgCalendario_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Calendario menuCalendario = new UC_Menu_Calendario();
            DockPanel_Submenu.Children.Add(menuCalendario);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Calendario_Principal calendarioPrincipal = new UC_Menu_Calendario_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(calendarioPrincipal);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón TRANSFERENCIAS 
        private void imgTransferencias_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Reproducir sonido
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Transferencias menuTransferencias = new UC_Menu_Transferencias();

            // Suscribirse a los eventos
            menuTransferencias.MostrarMercado += CargarTransferenciasMercado;
            menuTransferencias.MostrarBuscarPorEquipo += CargarTransferenciasBuscarPorEquipo;
            menuTransferencias.MostrarBuscarPorFiltro += CargarTransferenciasBuscarPorFiltro;
            menuTransferencias.MostrarCartera += CargarTransferenciasCartera;
            menuTransferencias.MostrarEstadoOfertas += CargarTransferenciasEstadoOfertas;
            menuTransferencias.MostrarOfertasRecibidas += CargarTransferenciasOfertasRecibidas;
            menuTransferencias.MostrarListaTraspasos += CargarTransferenciasListaTraspasos;

            // Cambiar el color del texto "Ingresos" a naranja
            menuTransferencias.lblMercado.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Añadir el menú a la vista
            DockPanel_Submenu.Children.Add(menuTransferencias);

            // Cargar Panel Principal (en este caso es el de "Mercado")
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_Mercado transferenciasMercado = new UC_Menu_Transferencias_Mercado(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasMercado);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------- Evento CLICK del botón FINANZAS 
        private void imgFinanzas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Reproducir sonido
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Finanzas menuFinanzas = new UC_Menu_Finanzas();

            // Suscribirse a los eventos
            menuFinanzas.MostrarIngresos += CargarFinanzasIngresos;
            menuFinanzas.MostrarGastos += CargarFinanzasGastos;
            menuFinanzas.MostrarPatrocinadores += CargarFinanzasPatrocinadores;
            menuFinanzas.MostrarTelevision += CargarFinanzasDerechosTV;
            menuFinanzas.MostrarPrestamos += CargarFinanzasPrestamos;

            // Cambiar el color del texto "Ingresos" a naranja
            menuFinanzas.lblIngresos.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Añadir el menú a la vista
            DockPanel_Submenu.Children.Add(menuFinanzas);

            // Cargar Panel Principal (en este caso es el de "Ingresos")
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_Ingresos finanzasIngresos = new UC_Menu_Finanzas_Ingresos(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasIngresos);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------- Evento CLICK del botón ESTADIO
        private void imgPabellon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Estadio menuEstadio = new UC_Menu_Estadio();
            DockPanel_Submenu.Children.Add(menuEstadio);

            // Suscribirse a los eventos
            menuEstadio.MostrarInformacion += CargarEstadioInformacion;
            menuEstadio.MostrarEntradas += CargarEstadioEntradas;
            menuEstadio.MostrarAmpliaciones += CargarEstadioAmpliaciones;

            // Cambiar el color del texto
            menuEstadio.lblInformacion.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }
        // ---------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MÁNAGER
        private void imgManager_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Manager menuManager = new UC_Menu_Manager();
            DockPanel_Submenu.Children.Add(menuManager);

            // Suscribirse a los eventos MostrarPalmares y MostrarFicha
            menuManager.MostrarPalmares += CargarManagerPalmares;
            menuManager.MostrarFicha += CargarManagerFicha;

            // Cambiar el color del texto "Ficha" a naranja
            menuManager.lblFicha.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x1D, 0x6A, 0x7D));

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }
        // ------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------- Evento CLICK del botón MENSAJES 
        private void imgCorreo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Cargar Submenú
            DockPanel_Submenu.Children.Clear();
            UC_Menu_Correo menuCorreo = new UC_Menu_Correo();
            DockPanel_Submenu.Children.Add(menuCorreo);

            // Cargar Panel Principal
            DockPanel_Central.Children.Clear();
            UC_Menu_Correo_Principal correoPrincipal = new UC_Menu_Correo_Principal(_manager, _equipo);
            DockPanel_Central.Children.Add(correoPrincipal);
        }
        // ------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private void MostrarEstrellas(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellas.Children.Clear();

            // Cargar las imágenes de recursos
            ImageSource estrellaON = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOn.png"));
            ImageSource estrellaOFF = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOff.png"));

            // Determinar el número de estrellas según la reputación
            int numeroEstrellas = 0;

            if (reputacion == 100)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion >= 90)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion >= 70)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion >= 50)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion >= 25)
            {
                numeroEstrellas = 1;
            }
            else
            {
                numeroEstrellas = 0;
            }

            // Dibujar 5 estrellas (activas e inactivas)
            for (int i = 0; i < 5; i++)
            {
                // Crear la imagen de la estrella
                Image estrella = new Image
                {
                    Width = 32, // Ancho de cada estrella
                    Height = 32,
                    Source = i < numeroEstrellas ? estrellaON : estrellaOFF
                };

                // Colocar la estrella en el Canvas
                Canvas.SetLeft(estrella, i * 35); // Separación horizontal entre estrellas
                Canvas.SetTop(estrella, 0);

                // Agregar la estrella al Canvas
                canvasEstrellas.Children.Add(estrella);
            }
        }

        // --------------------------------------------- MÉTODOS PARA LAS OPCIONES DE SUBMENÚ -----------------------------------------------
        // Método para cargar UC_Menu_Club_Informacion
        private void CargarClubInformacion()
        {
            // Cargar UC_Menu_Club_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Informacion clubInformacion = new UC_Menu_Club_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(clubInformacion);
        }

        // Método para cargar UC_Menu_Club_Plantilla
        private void CargarClubPlantilla()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Plantilla clubPlantilla = new UC_Menu_Club_Plantilla(_manager, _equipo);
            DockPanel_Central.Children.Add(clubPlantilla);
        }

        // Método para cargar UC_Menu_Club_Empleados
        private void CargarClubEmpleados()
        {
            // Cargar UC_Menu_Club_Empleados
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Empleados clubEmpleados = new UC_Menu_Club_Empleados(_manager, _equipo);
            DockPanel_Central.Children.Add(clubEmpleados);
        }

        // Método para cargar UC_Menu_Club_Lesionados
        private void CargarClubLesionados()
        {
            // Cargar UC_Menu_Club_Lesionados
            DockPanel_Central.Children.Clear();
            UC_Menu_Club_Lesionados clubEmpleados = new UC_Menu_Club_Lesionados(_manager, _equipo);
            DockPanel_Central.Children.Add(clubEmpleados);
        }

        // Método para cargar UC_Menu_Entrenador_Alineacion
        private void CargarEntrenadorAlineacion()
        {
            // Cargar UC_Menu_Club_Plantilla
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Alineacion entrenadorAlineacion = new UC_Menu_Entrenador_Alineacion(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorAlineacion);
        }

        // Método para cargar UC_Menu_Entrenador_Entrenamiento
        private void CargarEntrenadorEntrenamiento()
        {
            // Cargar UC_Menu_Entrenador_Tactica
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Entrenamiento entrenadorEntrenamiento = new UC_Menu_Entrenador_Entrenamiento(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorEntrenamiento);
        }

        // Método para cargar UC_Menu_Entrenador_Rival
        private void CargarEntrenadorRival()
        {
            // Cargar UC_Menu_Entrenador_Rival
            DockPanel_Central.Children.Clear();
            UC_Menu_Entrenador_Rival entrenadorRival = new UC_Menu_Entrenador_Rival(_manager, _equipo);
            DockPanel_Central.Children.Add(entrenadorRival);
        }

        // Método para cargar UC_Menu_Competicion_Clasificacion
        private void CargarCompeticionClasificacion()
        {
            // Cargar UC_Menu_Competicion_Clasificacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Clasificacion competicionClasificacion = new UC_Menu_Competicion_Clasificacion(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionClasificacion);
        }

        // Método para cargar UC_Menu_Competicion_Resultados
        private void CargarCompeticionResultados()
        {
            // Cargar UC_Menu_Competicion_Resultados
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Resultados competicionResultados = new UC_Menu_Competicion_Resultados(_manager, _equipo, DockPanel_Central);
            DockPanel_Central.Children.Add(competicionResultados);
        }

        // Método para cargar UC_Menu_Competicion_Estadisticas
        private void CargarCompeticionEstadisticas()
        {
            // Cargar UC_Menu_Competicion_Estadisticas
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Estadisticas competicionEstadisticas = new UC_Menu_Competicion_Estadisticas(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionEstadisticas);
        }

        // Método para cargar UC_Menu_Competicion_Palmares
        private void CargarCompeticionPalmares()
        {
            // Cargar UC_Menu_Competicion_Palmares
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Palmares competicionPalmares = new UC_Menu_Competicion_Palmares(_manager, _equipo, DockPanel_Central);
            DockPanel_Central.Children.Add(competicionPalmares);
        }

        // Método para cargar UC_Menu_Competicion_Palmares_Jugadores
        private void CargarCompeticionPalmaresJugadores()
        {
            // Cargar UC_Menu_Competicion_Palmares_Jugadores
            DockPanel_Central.Children.Clear();
            UC_Menu_Competicion_Palmares_Jugadores competicionPalmaresJugadores = new UC_Menu_Competicion_Palmares_Jugadores(_manager, _equipo);
            DockPanel_Central.Children.Add(competicionPalmaresJugadores);
        }

        // Método para cargar UC_Menu_Manager_Ficha
        private void CargarManagerFicha()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Ficha managerFicha = new UC_Menu_Manager_Ficha(_manager, _equipo);
            DockPanel_Central.Children.Add(managerFicha);
        }

        // Método para cargar UC_Menu_Manager_Palmares
        private void CargarManagerPalmares()
        {
            DockPanel_Central.Children.Clear();
            UC_Menu_Manager_Palmares managerPalmares = new UC_Menu_Manager_Palmares(_manager, _equipo);
            DockPanel_Central.Children.Add(managerPalmares);
        }

        // Método para cargar UC_Menu_Estadio_Informacion
        private void CargarEstadioInformacion()
        {
            // Cargar UC_Menu_Estadio_Informacion
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Informacion pabellonInformacion = new UC_Menu_Estadio_Informacion(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonInformacion);
        }

        // Método para cargar UC_Menu_Estadio_Entradas
        private void CargarEstadioEntradas()
        {
            // Cargar UC_Menu_Estadio_Entradas
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Entradas pabellonEntradas = new UC_Menu_Estadio_Entradas(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonEntradas);
        }

        // Método para cargar UC_Menu_Estadio_Ampliaciones
        private void CargarEstadioAmpliaciones()
        {
            // Cargar UC_Menu_Estadio_Ampliaciones
            DockPanel_Central.Children.Clear();
            UC_Menu_Estadio_Ampliaciones pabellonAmpliaciones = new UC_Menu_Estadio_Ampliaciones(_manager, _equipo);
            DockPanel_Central.Children.Add(pabellonAmpliaciones);
        }

        // Método para cargar UC_Menu_Finanzas_Ingresos
        private void CargarFinanzasIngresos()
        {
            // Cargar UC_Menu_Finanzas_Ingresos
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_Ingresos finanzasIngresos = new UC_Menu_Finanzas_Ingresos(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasIngresos);
        }

        // Método para cargar UC_Menu_Finanzas_Gastos
        private void CargarFinanzasGastos()
        {
            // Cargar UC_Menu_Finanzas_Gastos
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_Gastos finanzasGastos = new UC_Menu_Finanzas_Gastos(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasGastos);
        }

        // Método para cargar UC_Menu_Finanzas_Patrocinadores
        private void CargarFinanzasPatrocinadores()
        {
            // Cargar UC_Menu_Finanzas_Patrocinadores
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_Patrocinadores finanzasPatrocinadores = new UC_Menu_Finanzas_Patrocinadores(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasPatrocinadores);
        }

        // Método para cargar UC_Menu_Finanzas_DerechosTV
        private void CargarFinanzasDerechosTV()
        {
            // Cargar UC_Menu_Finanzas_DerechosTV
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_DerechosTV finanzasTelevisiones = new UC_Menu_Finanzas_DerechosTV(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasTelevisiones);
        }

        // Método para cargar UC_Menu_Finanzas_Prestamos
        private void CargarFinanzasPrestamos()
        {
            // Cargar UC_Menu_Finanzas_DerechosTV
            DockPanel_Central.Children.Clear();
            UC_Menu_Finanzas_Prestamos finanzasPrestamos = new UC_Menu_Finanzas_Prestamos(_manager, _equipo);
            DockPanel_Central.Children.Add(finanzasPrestamos);
        }

        // Método para cargar UC_Menu_Transferencias_Mercado
        private void CargarTransferenciasMercado()
        {
            // Cargar UC_Menu_Transferencias_Mercado 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_Mercado transferenciasMercado = new UC_Menu_Transferencias_Mercado(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasMercado);
        }

        // Método para cargar UC_Menu_Transferencias_BuscarPorEquipo
        private void CargarTransferenciasBuscarPorEquipo()
        {
            // Cargar UC_Menu_Transferencias_BuscarPorEquipo 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_BuscarPorEquipo transferenciasBuscarPorEquipo = new UC_Menu_Transferencias_BuscarPorEquipo(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasBuscarPorEquipo);
        }

        // Método para cargar UC_Menu_Transferencias_BuscarPorFiltro
        private void CargarTransferenciasBuscarPorFiltro()
        {
            // Cargar UC_Menu_Transferencias_BuscarPorFiltro 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_BuscarPorFiltro transferenciasBuscarPorFiltro = new UC_Menu_Transferencias_BuscarPorFiltro(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasBuscarPorFiltro);
        }

        // Método para cargar UC_Menu_Transferencias_Cartera
        private void CargarTransferenciasCartera()
        {
            // Cargar UC_Menu_Transferencias_Cartera 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_Cartera transferenciasCartera = new UC_Menu_Transferencias_Cartera(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasCartera);
        }

        // Método para cargar UC_Menu_Transferencias_EstadoOfertas
        private void CargarTransferenciasEstadoOfertas()
        {
            // Cargar UC_Menu_Transferencias_EstadoOfertas 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_EstadoOfertas transferenciasEstadoOfertas = new UC_Menu_Transferencias_EstadoOfertas(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasEstadoOfertas);
        }

        // Método para cargar UC_Menu_Transferencias_OfertasRecibidas
        private void CargarTransferenciasOfertasRecibidas()
        {
            // Cargar UC_Menu_Transferencias_OfertasRecibidas 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_OfertasRecibidas transferenciasOfertasRecibidas = new UC_Menu_Transferencias_OfertasRecibidas(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasOfertasRecibidas);
        }

        // Método para cargar UC_Menu_Transferencias_ListaTraspasos
        private void CargarTransferenciasListaTraspasos()
        {
            // Cargar UC_Menu_Transferencias_ListaTraspasos 
            DockPanel_Central.Children.Clear();
            UC_Menu_Transferencias_ListaTraspasos transferenciasListaTraspasos = new UC_Menu_Transferencias_ListaTraspasos(_manager, _equipo);
            DockPanel_Central.Children.Add(transferenciasListaTraspasos);
        }

        private void CargarFecha()
        {
            Fecha fechaObjeto = _datosFecha.ObtenerFechaHoy();
            DateTime hoy = DateTime.Parse(fechaObjeto.Hoy);
            // Formatear la fecha en español
            CultureInfo culturaEspañol = new CultureInfo("es-ES");
            string dia = hoy.ToString("dd", culturaEspañol); // Día
            string mes = hoy.ToString("MMM", culturaEspañol).ToUpper(); // Mes abreviado en español y en mayúsculas
            string año = hoy.ToString("yyyy", culturaEspañol); // Año

            // Combinar el formato
            string fechaFormateada = $"{dia} {mes} {año}";

            // Mostrar la fecha en el TextBlock
            txtFechaActual.Text = fechaFormateada;

            // Obtener el día de la semana en español
            string diaSemana = hoy.ToString("dddd", culturaEspañol);

            // Capitalizar la primera letra (opcional, si el formato por defecto no es suficiente)
            diaSemana = char.ToUpper(diaSemana[0]) + diaSemana.Substring(1);

            // Mostrar el día de la semana en el TextBlock
            txtDiaSemana.Text = diaSemana;

            // Comprobar si mi equipo juega hoy y cambiar el nombre al boton AVANZAR a PARTIDO
            Partido proximopartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, Metodos.hoy);
            if (proximopartido != null && proximopartido.FechaPartido == hoy)
            {
                btnAvanzar.Content = "PARTIDO";
                // Obtén la imagen (imgBoton) del template del botón
                Image imgBoton = btnAvanzar.Template.FindName("imgBoton", btnAvanzar) as Image;
                if (imgBoton != null)
                {
                    // Cambiar la fuente de la imagen
                    imgBoton.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/matchday_icon.png"));
                }
            } 
            else
            {
                btnAvanzar.Content = "AVANZAR";
                // Obtén la imagen (imgBoton) del template del botón
                Image imgBoton = btnAvanzar.Template.FindName("imgBoton", btnAvanzar) as Image;
                if (imgBoton != null)
                {
                    // Cambiar la fuente de la imagen
                    imgBoton.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/flechaDerechaBlanca64px_icon.png"));
                }
            }
        }

        private void CalcularObjetivoTemporada(string objetivo, int posicion, int competicion)
        {
            if (competicion == 1)
            {
                if (objetivo.Equals("Campeón"))
                {
                    if (posicion <= 4)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -30, 0, 0);
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -30, 0, 0);
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 15)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 25, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -30, 0, 0);
                    }
                }
            }
            else
            {
                if (objetivo.Equals("Ascenso"))
                {
                    if (posicion <= 4)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -30, 0, 0);
                    }
                }
                else if (objetivo.Equals("Zona Tranquila"))
                {
                    if (posicion <= 14)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 30, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -30, 0, 0);
                    }
                }
                else if (objetivo.Equals("Descenso"))
                {
                    if (posicion <= 15)
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, 25, 0, 0);
                    }
                    else
                    {
                        _logicaManager.ActualizarConfianza(_manager.IdManager, -100, 0, 0);
                    }
                }
            }
        }

        private void GeneralCalendarioCopa(List<Equipo> listaEquipos)
        {
            GenerarTreintaidosavosCopa(listaEquipos, Metodos.temporadaActual, _manager.IdManager, 4);
        }

        public void GenerarTreintaidosavosCopa(List<Equipo> equiposCopa, int temporada, int idManager, int idCompeticionCopa)
        {
            if (equiposCopa.Count != 64)
                throw new ArgumentException("Deben ser exactamente 64 equipos para los dieciseisavos de final.");

            // Mezclar aleatoriamente
            Random rnd = new Random();
            var equiposMezclados = equiposCopa.OrderBy(e => rnd.Next()).ToList();

            // Fechas de ida y vuelta
            DateTime fechaIda = ObtenerPrimerMiercolesSeptiembre(temporada);
            DateTime fechaVuelta = fechaIda.AddDays(14);

            // Crear los emparejamientos
            for (int i = 0; i < equiposMezclados.Count; i += 2)
            {
                int idLocal = equiposMezclados[i].IdEquipo;
                int idVisitante = equiposMezclados[i + 1].IdEquipo;

                // Partido de ida
                _logicaPartidos.crearPartidoCopa(idLocal, idVisitante, fechaIda.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 0, idManager);

                // Partido de vuelta (se invierte local/visitante)
                _logicaPartidos.crearPartidoCopa(idVisitante, idLocal, fechaVuelta.ToString("yyyy-MM-dd"), idCompeticionCopa, 1, 1, idManager);
            }
        }

        public static DateTime ObtenerSegundoLunesDeAgosto(int anio)
        {
            DateTime fecha = new DateTime(anio, 8, 1);
            int lunesEncontrados = 0;

            while (fecha.Month == 8)
            {
                if (fecha.DayOfWeek == DayOfWeek.Monday)
                {
                    lunesEncontrados++;
                    if (lunesEncontrados == 2)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        public static DateTime ObtenerPrimerMiercolesSeptiembre(int anio)
        {
            DateTime fecha = new DateTime(anio, 9, 1);
            int miercolesEncontrados = 0;

            while (fecha.Month == 9)
            {
                if (fecha.DayOfWeek == DayOfWeek.Wednesday)
                {
                    miercolesEncontrados++;
                    if (miercolesEncontrados == 1)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        public static DateTime ObtenerPrimerMartesOctubre(int anio)
        {
            DateTime fecha = new DateTime(anio, 10, 1);
            int miercolesEncontrados = 0;

            while (fecha.Month == 10)
            {
                if (fecha.DayOfWeek == DayOfWeek.Tuesday)
                {
                    miercolesEncontrados++;
                    if (miercolesEncontrados == 1)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        public static DateTime ObtenerPrimerJuevesOctubre(int anio)
        {
            DateTime fecha = new DateTime(anio, 10, 1);
            int miercolesEncontrados = 0;

            while (fecha.Month == 10)
            {
                if (fecha.DayOfWeek == DayOfWeek.Thursday)
                {
                    miercolesEncontrados++;
                    if (miercolesEncontrados == 1)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        public static DateTime ObtenerTercerMartesFebrero(int anio)
        {
            DateTime fecha = new DateTime((anio + 1), 2, 1);
            int martesEncontrados = 0;

            while (fecha.Month == 2)
            {
                if (fecha.DayOfWeek == DayOfWeek.Tuesday)
                {
                    martesEncontrados++;
                    if (martesEncontrados == 3)
                    {
                        return fecha;
                    }
                }

                fecha = fecha.AddDays(1);
            }

            throw new Exception("No se encontró el tercer sábado de agosto.");
        }

        public void ActualizarPresupuesto()
        {
            txtPresupuesto.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Presupuesto.ToString("N0") + " €";
        }

        public void RecargarFichaJugador(int idJugador, int idEquipo, Manager manager, int opcion)
        {
            UC_FichaJugador nuevaFicha = new UC_FichaJugador(idJugador, idEquipo, manager, opcion, this);
            DockPanel_Central.Children.Clear(); 
            DockPanel_Central.Children.Add(nuevaFicha);
        }

        public int AleatorioEntre(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max + 1); // Incluye el máximo
        }
        #endregion
    }
}
