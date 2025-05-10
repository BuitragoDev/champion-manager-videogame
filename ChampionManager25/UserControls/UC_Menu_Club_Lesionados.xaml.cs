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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using ChampionManager25.Vistas;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Club_Lesionados : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        int reputacionMedico = 0;
        int reduccion = 0;
        Jugador jugadorSeleccionado = null;
        private Border borderSeleccionado = null;

        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        public UC_Menu_Club_Lesionados(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Empleado? medico = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Equipo Médico");

            if (medico != null)
            {
                txtNombreMedico.Text = medico.Nombre;
                reputacionMedico = medico.Categoria;
                MostrarEstrellas(reputacionMedico);

                string[] tratamiento = {
                    "Nuestro equipo médico puede reducir el tiempo de recuperación un 10%.",
                    "Nuestro equipo médico puede reducir el tiempo de recuperación un 20%.",
                    "Nuestro equipo médico puede reducir el tiempo de recuperación un 30%.",
                    "Nuestro equipo médico puede reducir el tiempo de recuperación un 40%.",
                    "Nuestro equipo médico puede reducir el tiempo de recuperación un 50%."
                };

                if (reputacionMedico >= 1 && reputacionMedico <= 5)
                {
                    txtNivelMedico.Text = tratamiento[reputacionMedico - 1];
                }

                int[] porcentaje = {
                        10, 20, 30, 40, 50
                    };

                if (medico.Categoria >= 1 && medico.Categoria <= 5)
                {
                    reduccion = porcentaje[medico.Categoria - 1];
                }
            } else
            {
                btnTratarLesion.IsEnabled = false;
                txtNombreMedico.Text = "No tienes ningún equipo médico contratado";
            }

            // Lista de jugadores
            List<Jugador> listaJugadores = _logicaJugador.ListadoJugadoresCompleto(_equipo);
            CargarListajugadores();
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton TRATAR LESION
        private void btnTratarLesion_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            int jugadorTratado = _logicaJugador.MostrarDatosJugador(jugadorSeleccionado.IdJugador).LesionTratada;
            if (jugadorSeleccionado != null && jugadorTratado != 1)
            {
                _logicaJugador.TratarLesion(jugadorSeleccionado.IdJugador, reduccion);

                string titulo = "INFORMACIÓN";
                string mensaje = $"Me complace informarte de que el tratamiento aplicado a {jugadorSeleccionado.NombreCompleto} ha dado excelentes resultados. Gracias al trabajo del equipo médico, hemos conseguido acelerar su recuperación, reduciendo la duración estimada de su lesión.";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                bool? resultado = ventanaEmergente.ShowDialog();

                _logicaJugador.ActivarTratamientoLesion(jugadorSeleccionado.IdJugador, 1);

                CargarListajugadores();

                jugadorTratado = _logicaJugador.MostrarDatosJugador(jugadorSeleccionado.IdJugador).LesionTratada;
                if (jugadorSeleccionado != null && jugadorTratado == 1)
                {
                    btnTratarLesion.IsEnabled = false;
                }
                else
                {
                    btnTratarLesion.IsEnabled = true;
                }
            }
        }

        #region "Metodos"
        private void CargarListajugadores()
        {
            List<Jugador> listaJugadores = _logicaJugador.ListadoJugadoresCompleto(_equipo);
            wrapJugadores.Children.Clear(); // Limpiamos el WrapPanel antes de llenarlo

            int contador = 0;
            foreach (var jugador in listaJugadores)
            {
                if (jugador.Lesion > 0)
                {
                    // Declaramos el Border antes del if-else
                    Border border;

                    var colorOriginal = (contador % 2 == 0) ? Brushes.LightGray : Brushes.WhiteSmoke;

                    border = new Border
                    {
                        Height = 70,
                        Width = 1825,
                        Margin = new Thickness(2),
                        Background = colorOriginal,
                        Cursor = Cursors.Hand,
                        Tag = new { jugador.IdJugador, jugador.NombreCompleto },
                        ToolTip = colorOriginal // Guardamos el color original en ToolTip
                    };

                    // Agregar evento de clic
                    border.MouseLeftButtonDown += Border_MouseLeftButtonDown;

                    // Crear el Grid
                    Grid grid = new Grid();
                    grid.Height = 70;

                    // Definir ColumnDefinitions
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });  // Foto
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(450) }); // NombreCompleto
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(700) });  // Lesion
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(300) });  // Semanas

                    // Crear y agregar elementos a las columnas
                    var converter = new IdjugadorToFotoConverter();
                    var uri = (Uri)converter.Convert(jugador.IdJugador, typeof(Uri), null, CultureInfo.InvariantCulture);

                    Image imgJugador = new Image
                    {
                        Width = 50,
                        Height = 50,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Source = new BitmapImage(uri)
                    };

                    Grid.SetColumn(imgJugador, 0);
                    grid.Children.Add(imgJugador);

                    // NombreCompleto
                    TextBlock txtNombre = new TextBlock
                    {
                        Text = jugador.NombreCompleto,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        FontSize = 16,
                        FontWeight = FontWeights.SemiBold
                    };
                    Grid.SetColumn(txtNombre, 1);
                    grid.Children.Add(txtNombre);

                    // Tipo de Lesion
                    TextBlock txtRol = new TextBlock
                    {
                        Text = jugador.TipoLesion,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        FontSize = 16,
                        FontWeight = FontWeights.SemiBold
                    };

                    Grid.SetColumn(txtRol, 2);
                    grid.Children.Add(txtRol);

                    // Semanas
                    TextBlock txtSemanas = new TextBlock
                    {
                        Text = jugador.Lesion.ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontFamily = new FontFamily("Cascadia Code SemiBold"),
                        FontSize = 24,
                        FontWeight = FontWeights.SemiBold
                    };
                    Grid.SetColumn(txtSemanas, 3);
                    grid.Children.Add(txtSemanas);

                    // Agregar el Grid al Border
                    border.Child = grid;

                    // Agregar el Border al WrapPanel
                    wrapJugadores.Children.Add(border);

                    contador++;
                }
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            if (sender is Border nuevoBorder && nuevoBorder.Tag is { } tag)
            {
                // Restaurar el color del anterior seleccionado, si existe
                if (borderSeleccionado != null && borderSeleccionado != nuevoBorder)
                {
                    // Restaurar el color original guardado en ToolTip
                    borderSeleccionado.Background = borderSeleccionado.ToolTip as Brush;
                }

                // Marcar el nuevo Border
                nuevoBorder.Background = (Brush)new BrushConverter().ConvertFromString("#b1d5c9");
                borderSeleccionado = nuevoBorder;

                // Obtener el jugador
                var idJugador = (int)tag.GetType().GetProperty("IdJugador").GetValue(tag);
                jugadorSeleccionado = _logicaJugador.MostrarDatosJugador(idJugador);

                int jugadorTratado = _logicaJugador.MostrarDatosJugador(jugadorSeleccionado.IdJugador).LesionTratada;
                if (jugadorSeleccionado != null && jugadorTratado == 1)
                {
                    btnTratarLesion.IsEnabled = false;
                }
                else
                {
                    btnTratarLesion.IsEnabled = true;
                }
            }
        }


        private void MostrarEstrellas(int reputacion)
        {
            // Limpiar el canvas antes de agregar nuevas estrellas
            canvasEstrellas.Children.Clear();

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
        #endregion
    }
}
