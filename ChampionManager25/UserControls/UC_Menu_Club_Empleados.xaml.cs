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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Club_Empleados : UserControl
    {
        private Manager _manager;
        private int _equipo;

        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        List<Empleado>? listaEmpleadosDisponible = null;

        public UC_Menu_Club_Empleados(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnFirmar1.Visibility = Visibility.Hidden;
            btnFirmar2.Visibility = Visibility.Hidden;
            btnFirmar3.Visibility = Visibility.Hidden;
            btnFirmar4.Visibility = Visibility.Hidden;
            btnFirmar5.Visibility = Visibility.Hidden;

            CargarEmpleadosContratados();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (SEGUNDO ENTRENADOR)
        private void btnContratarSegundoEntrenador_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(1);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (DELEGADO)
        private void btnContratarDelegado_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(2);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (DIRECTOR TÉCNICO)
        private void btnContratarDirectorTecnico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(3);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (PREPARADOR FÍSICO)
        private void btnContratarPreparadorFisico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(4);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (PSICOLOGO)
        private void btnContratarPsicologo_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(5);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (FINANCIERO)
        private void btnContratarFinanciero_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(6);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (EQUIPO MÉDICO)
        private void btnContratarEquipoMedico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(7);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón CONTRATAR EMPLEADO (ENCARGADO PABELLÓN)
        private void btnContratarEncargadoPabellon_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            listaEmpleadosDisponible = _logicaEmpleado.MostrarListaEmpleadosDisponibles(8);
            CargarEmpleadosDisponibles(listaEmpleadosDisponible);
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón FIRMAR 1
        private void btnFirmar1_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (listaEmpleadosDisponible != null && listaEmpleadosDisponible.Count > 0)
            {
                _logicaEmpleado.FicharEmpleado(_equipo, _manager.IdManager, listaEmpleadosDisponible[0].IdEmpleado);
            }
            CargarEmpleadosContratados();
            LimpiarEmpleadosDisponibles();
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón FIRMAR 2
        private void btnFirmar2_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (listaEmpleadosDisponible != null && listaEmpleadosDisponible.Count > 0)
            {
                _logicaEmpleado.FicharEmpleado(_equipo, _manager.IdManager, listaEmpleadosDisponible[1].IdEmpleado);
            }
            CargarEmpleadosContratados();
            LimpiarEmpleadosDisponibles();
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón FIRMAR 3
        private void btnFirmar3_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (listaEmpleadosDisponible != null && listaEmpleadosDisponible.Count > 0)
            {
                _logicaEmpleado.FicharEmpleado(_equipo, _manager.IdManager, listaEmpleadosDisponible[2].IdEmpleado);
            }
            CargarEmpleadosContratados();
            LimpiarEmpleadosDisponibles();
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón FIRMAR 4
        private void btnFirmar4_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (listaEmpleadosDisponible != null && listaEmpleadosDisponible.Count > 0)
            {
                _logicaEmpleado.FicharEmpleado(_equipo, _manager.IdManager, listaEmpleadosDisponible[3].IdEmpleado);
            }
            CargarEmpleadosContratados();
            LimpiarEmpleadosDisponibles();
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón FIRMAR 5
        private void btnFirmar5_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (listaEmpleadosDisponible != null && listaEmpleadosDisponible.Count > 0)
            {
                _logicaEmpleado.FicharEmpleado(_equipo, _manager.IdManager, listaEmpleadosDisponible[4].IdEmpleado);
            }
            CargarEmpleadosContratados();
            LimpiarEmpleadosDisponibles();
            LimpiarFichaEmpleado();
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (SEGUNDO ENTRENADOR)
        private void btnDespedirSegundoEntrenador_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Segundo Entrenador?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Segundo Entrenador");

                _logicaEmpleado.DespedirEmpleado(1);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (DELEGADO)
        private void btnDespedirDelegado_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Delegado?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Delegado");

                _logicaEmpleado.DespedirEmpleado(2);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (DIRECTOR TECNICO)
        private void btnDespedirDirectorTecnico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Director Técnico?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Director Técnico");

                _logicaEmpleado.DespedirEmpleado(3);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (PREPARADOR FISICO)
        private void btnDespedirPreparadorFisico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Preparador Físico?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Preparador Físico");

                _logicaEmpleado.DespedirEmpleado(4);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (PSICOLOGO)
        private void btnDespedirPsicologo_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Psicólogo?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Psicólogo");

                _logicaEmpleado.DespedirEmpleado(5);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (FINANCIERO)
        private void btnDespedirFinanciero_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Financiero?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Financiero");

                _logicaEmpleado.DespedirEmpleado(6);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (EQUIPO MEDICO)
        private void btnDespedirEquipoMedico_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Equipo Médico?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Equipo Médico");

                _logicaEmpleado.DespedirEmpleado(7);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del botón DESPEDIR EMPLEADO (ENCARGADO PABELLON)
        private void btnDespedirEncargadoPabellon_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas despedir al Encargado del Campo?",
                                                      "Confirmación",
                                                      MessageBoxButton.YesNo,
                                                      MessageBoxImage.Warning);

            // Si el usuario selecciona "Sí", ejecutar la acción
            if (result == MessageBoxResult.Yes)
            {
                // Crear el Gasto de la indemnización en la tabla finanzas de la BD y restar presupuesto
                PagarIndemnizacion("Encargado Campo");

                _logicaEmpleado.DespedirEmpleado(8);
                CargarEmpleadosContratados();
                LimpiarEmpleadosDisponibles();
                LimpiarFichaEmpleado();
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del SEGUNDO ENTRENADOR
        private void txtNombreSegundoEntrenador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreSegundoEntrenador.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioSegundoEntrenador.Text;
            txtFichaInfo.Text = "Es el encargado de entrenar individualmente a tus jugadores, además de dar un bonus al entrenamiento ofensivo y defensivo del equipo. Un entrenador asistente de más nivel permite entrenar más jugadores de forma individual y aumenta la velocidad con la que subirán los parámetros de tus jugadores.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreSegundoEntrenador.Text);
            txtFichaEmpleado.Text = categoria.Puesto.ToUpper();
            MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del DELEGADO
        private void txtNombreDelegado_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreDelegado.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioDelegado.Text;
            txtFichaInfo.Text = "Es el encargado de analizar al equipo rival antes de un partido, comunicarte cualquier acontecimiento referente a tu equipo o a las competiciones en las que participes.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreDelegado.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del DIRECTOR TÉCNICO
        private void txtNombreDirectorTecnico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreDirectorTecnico.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioDirectorTecnico.Text;
            txtFichaInfo.Text = "Te permite realizar una búsqueda muy precisa de todos los jugadores que hay en el juego. También es necesario si quieres informes detallados de jugadores que puedan ser interesantes sus fichajes.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreDirectorTecnico.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del PREPARADOR FÍSICO
        private void txtNombrePreparadorFisico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombrePreparadorFisico.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioPreparadorFisico.Text;
            txtFichaInfo.Text = "Es el encargado de entrenar los atributos físicos de tus jugadores, además de dar un bonus al entrenamiento físico del equipo. Un preparador físico de más nivel aumenta la velocidad con la que subirán los parámetros físicos de tus jugadores.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombrePreparadorFisico.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del PSICÓLOGO
        private void txtNombrePsicologo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombrePsicologo.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioPsicologo.Text;
            txtFichaInfo.Text = "Ayuda a tus jguadores si estos están bajos de moral debido a situaciones como pueden ser el estado de ánimo debido a los malos resultados del equipo, la confianza con el entrenador debido a la falta de minutos de juego, o la química de vestuario.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombrePsicologo.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del FINANCIERO
        private void txtNombreFinanciero_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreFinanciero.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioFinanciero.Text;
            txtFichaInfo.Text = "Controla distintas áreas financieras del club como patrocinadores, ofertas de televisión o abonos de la temporada. El financiero siempre te conseguirá los mejores acuerdos dependiendo de su calidad y si lo deseas se encargará de estas áreas financieras del club.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreFinanciero.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del EQUIPO MÉDICO
        private void txtNombreEquipoMedico_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreEquipoMedico.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioEquipoMedico.Text;
            txtFichaInfo.Text = "El equipo médico está cualificado para tratar las lesiones de tus jugadores y hacer sesiones de fisioterapia entre partidos. Si quieres reducir el tiempo de lesión de un jugador o quieres recuperar mejor la condición física de tus jugadores entre partidos, deberás contratar un buen equipo médico.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreEquipoMedico.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        // ------------------------------------------------------ Evento CLICK del texto del NOMBRE del ENCARGADO DEL PABELLÓN
        private void txtNombreEncargadoPabellon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            txtFichaNombreEmpleado.Text = txtNombreEncargadoPabellon.Text;
            txtFichaSalarioEmpleado.Text = txtSalarioEncargadoPabellon.Text;
            txtFichaInfo.Text = "El encargado del campo te informará de los presupustos, reformas y semanas restantes de una obra que hayas encargado en el estadio. Dependiendo de su calidad te ayudará a recortar el tiempo de las obras del estadio.";
            Empleado categoria = _logicaEmpleado.MostrarCategoriaEmpleado(txtNombreEncargadoPabellon.Text);
            txtFichaEmpleado.Text = categoria?.Puesto?.ToUpper() ?? "Puesto no disponible";
            if (categoria != null)
            {
                MostrarEstrellas(categoria.Categoria, canvasFichaEmpleado);
            }
        }

        #region "Métodos"
        private void MostrarEstrellas(int reputacion, Canvas canvas)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvas.Children.Clear();

            // Cargar las imágenes de recursos
            ImageSource estrellaON = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOn.png"));
            ImageSource estrellaOFF = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/estrellaOff.png"));

            // Determinar el número de estrellas según la reputación
            int numeroEstrellas = 0;

            if (reputacion == 5)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion == 4)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion == 3)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion == 2)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion == 1)
            {
                numeroEstrellas = 1;
            }
            else
            {
                numeroEstrellas = 0;
            }

            // Calcular la posición inicial para centrar las estrellas
            double totalWidth = 5 * 24; // 5 estrellas * 24px de ancho
            double leftMargin = (canvas.ActualWidth - totalWidth) / 2;

            // Dibujar 5 estrellas (activas e inactivas)
            for (int i = 0; i < 5; i++)
            {
                // Crear la imagen de la estrella
                Image estrella = new Image
                {
                    Width = 24, // Ancho de cada estrella
                    Height = 24,
                    Source = i < numeroEstrellas ? estrellaON : estrellaOFF
                };

                // Colocar la estrella en el Canvas
                Canvas.SetLeft(estrella, leftMargin + i * 24); // Ajustar para centrar
                Canvas.SetTop(estrella, 3);

                // Agregar la estrella al Canvas
                canvas.Children.Add(estrella);
            }
        }

        private void CargarEmpleadosContratados()
        {
            // Inicializar todo a los valores predeterminados, esto es lo que se hace
            // cuando no se encuentra un empleado con el puesto correspondiente.

            // Para Segundo Entrenador
            txtNombreSegundoEntrenador.Text = "";
            txtSalarioSegundoEntrenador.Text = "";
            canvasSegundoEntrenador.Children.Clear();
            btnContratarSegundoEntrenador.Visibility = Visibility.Visible;
            btnDespedirSegundoEntrenador.Visibility = Visibility.Hidden;

            // Para Delegado
            txtNombreDelegado.Text = "";
            txtSalarioDelegado.Text = "";
            canvasDelegado.Children.Clear();
            btnContratarDelegado.Visibility = Visibility.Visible;
            btnDespedirDelegado.Visibility = Visibility.Hidden;

            // Para Director Técnico
            txtNombreDirectorTecnico.Text = "";
            txtSalarioDirectorTecnico.Text = "";
            canvasDirectorTecnico.Children.Clear();
            btnContratarDirectorTecnico.Visibility = Visibility.Visible;
            btnDespedirDirectorTecnico.Visibility = Visibility.Hidden;

            // Para Preparador Físico
            txtNombrePreparadorFisico.Text = "";
            txtSalarioPreparadorFisico.Text = "";
            canvasPreparadorFisico.Children.Clear();
            btnContratarPreparadorFisico.Visibility = Visibility.Visible;
            btnDespedirPreparadorFisico.Visibility = Visibility.Hidden;

            // Para Psicólogo
            txtNombrePsicologo.Text = "";
            txtSalarioPsicologo.Text = "";
            canvasPsicologo.Children.Clear();
            btnContratarPsicologo.Visibility = Visibility.Visible;
            btnDespedirPsicologo.Visibility = Visibility.Hidden;

            // Para Financiero
            txtNombreFinanciero.Text = "";
            txtSalarioFinanciero.Text = "";
            canvasFinanciero.Children.Clear();
            btnContratarFinanciero.Visibility = Visibility.Visible;
            btnDespedirFinanciero.Visibility = Visibility.Hidden;

            // Para Equipo Médico
            txtNombreEquipoMedico.Text = "";
            txtSalarioEquipoMedico.Text = "";
            canvasEquipoMedico.Children.Clear();
            btnContratarEquipoMedico.Visibility = Visibility.Visible;
            btnDespedirEquipoMedico.Visibility = Visibility.Hidden;

            // Para Encargado Pabellón
            txtNombreEncargadoPabellon.Text = "";
            txtSalarioEncargadoPabellon.Text = "";
            canvasEncargadoPabellon.Children.Clear();
            btnContratarEncargadoPabellon.Visibility = Visibility.Visible;
            btnDespedirEncargadoPabellon.Visibility = Visibility.Hidden;

            // Ahora recorrer la lista de empleados
            List<Empleado> listaEmpleadosContratados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);

            foreach (Empleado empleado in listaEmpleadosContratados)
            {
                if (empleado.Puesto.Equals("Segundo Entrenador"))
                {
                    txtNombreSegundoEntrenador.Text = empleado.Nombre;
                    txtSalarioSegundoEntrenador.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasSegundoEntrenador);
                    btnContratarSegundoEntrenador.Visibility = Visibility.Hidden;
                    btnDespedirSegundoEntrenador.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Delegado"))
                {
                    txtNombreDelegado.Text = empleado.Nombre;
                    txtSalarioDelegado.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasDelegado);
                    btnContratarDelegado.Visibility = Visibility.Hidden;
                    btnDespedirDelegado.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Director Técnico"))
                {
                    txtNombreDirectorTecnico.Text = empleado.Nombre;
                    txtSalarioDirectorTecnico.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasDirectorTecnico);
                    btnContratarDirectorTecnico.Visibility = Visibility.Hidden;
                    btnDespedirDirectorTecnico.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Preparador Físico"))
                {
                    txtNombrePreparadorFisico.Text = empleado.Nombre;
                    txtSalarioPreparadorFisico.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasPreparadorFisico);
                    btnContratarPreparadorFisico.Visibility = Visibility.Hidden;
                    btnDespedirPreparadorFisico.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Psicólogo"))
                {
                    txtNombrePsicologo.Text = empleado.Nombre;
                    txtSalarioPsicologo.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasPsicologo);
                    btnContratarPsicologo.Visibility = Visibility.Hidden;
                    btnDespedirPsicologo.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Financiero"))
                {
                    txtNombreFinanciero.Text = empleado.Nombre;
                    txtSalarioFinanciero.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasFinanciero);
                    btnContratarFinanciero.Visibility = Visibility.Hidden;
                    btnDespedirFinanciero.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Equipo Médico"))
                {
                    txtNombreEquipoMedico.Text = empleado.Nombre;
                    txtSalarioEquipoMedico.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasEquipoMedico);
                    btnContratarEquipoMedico.Visibility = Visibility.Hidden;
                    btnDespedirEquipoMedico.Visibility = Visibility.Visible;
                }
                else if (empleado.Puesto.Equals("Encargado Campo"))
                {
                    txtNombreEncargadoPabellon.Text = empleado.Nombre;
                    txtSalarioEncargadoPabellon.Text = empleado.Salario.ToString("N0") + " €";
                    MostrarEstrellas(empleado.Categoria, canvasEncargadoPabellon);
                    btnContratarEncargadoPabellon.Visibility = Visibility.Hidden;
                    btnDespedirEncargadoPabellon.Visibility = Visibility.Visible;
                }
            }
        }


        private void CargarEmpleadosDisponibles(List<Empleado> listaEmpleadosDisponible)
        {
            // Verificar que la lista tenga al menos 5 elementos
            if (listaEmpleadosDisponible.Count >= 5)
            {
                // Asignar los valores de los empleados a los TextBox
                txtNombreEmpleadoDisponible1.Text = listaEmpleadosDisponible[0].Nombre;
                txtNombreEmpleadoDisponible2.Text = listaEmpleadosDisponible[1].Nombre;
                txtNombreEmpleadoDisponible3.Text = listaEmpleadosDisponible[2].Nombre;
                txtNombreEmpleadoDisponible4.Text = listaEmpleadosDisponible[3].Nombre;
                txtNombreEmpleadoDisponible5.Text = listaEmpleadosDisponible[4].Nombre;

                txtPuestoEmpleadoDisponible1.Text = listaEmpleadosDisponible[0].Puesto;
                txtPuestoEmpleadoDisponible2.Text = listaEmpleadosDisponible[1].Puesto;
                txtPuestoEmpleadoDisponible3.Text = listaEmpleadosDisponible[2].Puesto;
                txtPuestoEmpleadoDisponible4.Text = listaEmpleadosDisponible[3].Puesto;
                txtPuestoEmpleadoDisponible5.Text = listaEmpleadosDisponible[4].Puesto;

                MostrarEstrellas(listaEmpleadosDisponible[0].Categoria, canvasEmpleadoDisponible1);
                MostrarEstrellas(listaEmpleadosDisponible[1].Categoria, canvasEmpleadoDisponible2);
                MostrarEstrellas(listaEmpleadosDisponible[2].Categoria, canvasEmpleadoDisponible3);
                MostrarEstrellas(listaEmpleadosDisponible[3].Categoria, canvasEmpleadoDisponible4);
                MostrarEstrellas(listaEmpleadosDisponible[4].Categoria, canvasEmpleadoDisponible5);

                txtSalarioEmpleadoDisponible1.Text = listaEmpleadosDisponible[0].Salario.ToString("N0") + " €";
                txtSalarioEmpleadoDisponible2.Text = listaEmpleadosDisponible[1].Salario.ToString("N0") + " €";
                txtSalarioEmpleadoDisponible3.Text = listaEmpleadosDisponible[2].Salario.ToString("N0") + " €";
                txtSalarioEmpleadoDisponible4.Text = listaEmpleadosDisponible[3].Salario.ToString("N0") + " €";
                txtSalarioEmpleadoDisponible5.Text = listaEmpleadosDisponible[4].Salario.ToString("N0") + " €";

                btnFirmar1.Visibility = Visibility.Visible;
                btnFirmar2.Visibility = Visibility.Visible;
                btnFirmar3.Visibility = Visibility.Visible;
                btnFirmar4.Visibility = Visibility.Visible;
                btnFirmar5.Visibility = Visibility.Visible;
            }
        }

        private void LimpiarEmpleadosDisponibles()
        {
            txtNombreEmpleadoDisponible1.Text = "-";
            txtNombreEmpleadoDisponible2.Text = "-";
            txtNombreEmpleadoDisponible3.Text = "-";
            txtNombreEmpleadoDisponible4.Text = "-";
            txtNombreEmpleadoDisponible5.Text = "-";

            txtPuestoEmpleadoDisponible1.Text = "-";
            txtPuestoEmpleadoDisponible2.Text = "-";
            txtPuestoEmpleadoDisponible3.Text = "-";
            txtPuestoEmpleadoDisponible4.Text = "-";
            txtPuestoEmpleadoDisponible5.Text = "-";

            canvasEmpleadoDisponible1.Children.Clear();
            canvasEmpleadoDisponible2.Children.Clear();
            canvasEmpleadoDisponible3.Children.Clear();
            canvasEmpleadoDisponible4.Children.Clear();
            canvasEmpleadoDisponible5.Children.Clear();

            txtSalarioEmpleadoDisponible1.Text = "-";
            txtSalarioEmpleadoDisponible2.Text = "-";
            txtSalarioEmpleadoDisponible3.Text = "-";
            txtSalarioEmpleadoDisponible4.Text = "-";
            txtSalarioEmpleadoDisponible5.Text = "-";

            btnFirmar1.Visibility = Visibility.Hidden;
            btnFirmar2.Visibility = Visibility.Hidden;
            btnFirmar3.Visibility = Visibility.Hidden;
            btnFirmar4.Visibility = Visibility.Hidden;
            btnFirmar5.Visibility = Visibility.Hidden;
        }

        private void LimpiarFichaEmpleado()
        {
            txtFichaNombreEmpleado.Text = "-";
            txtFichaSalarioEmpleado.Text = "-";
            txtFichaInfo.Text = "-";
            txtFichaEmpleado.Text = "FICHA EMPLEADO";
            canvasFichaEmpleado.Children.Clear();
        }

        private void PagarIndemnizacion(string puesto)
        {
            List<Empleado> listaEmpleadosContratados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);
            foreach (var empleado in listaEmpleadosContratados)
            {
                if (empleado.Puesto.Equals(puesto))
                {
                    // Crear Gasto en la tabla finanzas
                    int indemnizacion = empleado.Salario / 2;
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 14,
                        Tipo = 2,
                        Cantidad = indemnizacion,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, indemnizacion);
                }
            }
        }
        #endregion
    }
}
