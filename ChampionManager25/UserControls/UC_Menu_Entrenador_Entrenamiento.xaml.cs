using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Entrenador_Entrenamiento : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;

        int idJugadorSeleccionado;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_Menu_Entrenador_Entrenamiento(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void entrenamiento_Loaded(object sender, RoutedEventArgs e)
        {
            List<Jugador> listaEquipo = _logicaJugador.ListadoJugadoresCompleto(_equipo);
            CargarListajugadores(listaEquipo);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento PORTERO
        private void btnPortero_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 1);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento ENTRADAS 
        private void btnEntradas_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 2);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento REMATE
        private void btnRemate_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 3);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento PASE
        private void btnPase_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 4);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento REGATE
        private void btnRegate_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 5);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento TIRO
        private void btnTiro_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 6);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        // ------------------------------------------------------------------------- Evento CLICK del boton entrenamiento SIN ENTRENAMIENTO
        private void btnNoEntrenar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Actualizar Entrenamiento
            _logicaJugador.EntrenarJugador(idJugadorSeleccionado, 0);
            CargarEntrenamiento(idJugadorSeleccionado);
        }

        #region "Metodos"
        private void CargarListajugadores(List<Jugador> listaEquipo)
        {
            wrapJugadores.Children.Clear(); // Limpiamos el WrapPanel antes de llenarlo

            NacionalidadToFlagConverter converter = new NacionalidadToFlagConverter();
            int contador = 0;
            foreach (var jugador in listaEquipo)
            {
                // Declaramos el Border antes del if-else
                Border border;

                if (contador % 2 == 0)
                {
                    // Crear el Border
                    border = new Border
                    {
                        Height = 45,
                        Width = 510,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(2),
                        Background = Brushes.LightGray,
                        Cursor = Cursors.Hand,
                        Tag = new { jugador.IdJugador, jugador.NombreCompleto }
                    };
                } 
                else
                {
                    // Crear el Border
                    border = new Border
                    {
                        Height = 45,
                        Width = 510,
                        BorderBrush = Brushes.Black,
                        BorderThickness = new Thickness(1),
                        Margin = new Thickness(2),
                        Background = Brushes.WhiteSmoke,
                        Cursor = Cursors.Hand,
                        Tag = new { jugador.IdJugador, jugador.NombreCompleto }
                    };
                }

                // Agregar evento de clic
                border.MouseLeftButtonDown += Border_MouseLeftButtonDown;

                // Crear el Grid
                Grid grid = new Grid();
                grid.Height = 45;

                // Definir ColumnDefinitions
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });  // Nacionalidad
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(250) }); // NombreCompleto
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });  // RolId
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });  // Media

                // Crear y agregar elementos a las columnas

                // Nacionalidad (Imagen en lugar de texto)
                Image imgBandera = new Image
                {
                    Width = 40,
                    Height = 30,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Convertir la nacionalidad a una imagen usando el conversor
                var uri = (Uri)converter.Convert(jugador.Nacionalidad, typeof(Uri), null, System.Globalization.CultureInfo.CurrentCulture);
                imgBandera.Source = new BitmapImage(uri);


                Grid.SetColumn(imgBandera, 0);
                grid.Children.Add(imgBandera);

                // NombreCompleto
                TextBlock txtNombre = new TextBlock
                {
                    Text = jugador.NombreCompleto,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold
                };
                Grid.SetColumn(txtNombre, 1);
                grid.Children.Add(txtNombre);

                // RolId
                PosicionToStringConverter converterPosicion = new PosicionToStringConverter();

                TextBlock txtRol = new TextBlock
                {
                    Text = (string)converterPosicion.Convert(jugador.RolId, typeof(string), null, CultureInfo.CurrentCulture),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"), 
                    FontSize = 16, 
                    FontWeight = FontWeights.SemiBold 
                };

                Grid.SetColumn(txtRol, 2);
                grid.Children.Add(txtRol);

                // Media
                // Instancia del conversor de color para la media
                MediaToColorConverter converterMedia = new MediaToColorConverter();

                // Media
                TextBlock txtMedia = new TextBlock
                {
                    Text = jugador.Media.ToString(),
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = (Brush)converterMedia.Convert(jugador.Media, typeof(Brush), null, CultureInfo.CurrentCulture) // Aplicamos el conversor de color
                };

                Grid.SetColumn(txtMedia, 3);
                grid.Children.Add(txtMedia);

                // Agregar el Grid al Border
                border.Child = grid;

                // Agregar el Border al WrapPanel
                wrapJugadores.Children.Add(border);

                contador++;
            }
        }

        // Evento que maneja el clic en el Border
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            if (sender is Border border && border.Tag is { } tag)
            {
                // Acceder a las propiedades usando reflexión
                var idJugador = (int)tag.GetType().GetProperty("IdJugador").GetValue(tag);
                var nombreCompleto = (string)tag.GetType().GetProperty("NombreCompleto").GetValue(tag);

                Jugador jugador = _logicaJugador.MostrarDatosJugador(idJugador);
                imgFotoJugador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
                txtNombreJugador.Text = nombreCompleto;

                idJugadorSeleccionado = idJugador;
                CargarEntrenamiento(idJugador);
            }
        }

        private void CargarEntrenamiento(int idJugador) {
            // Comprobar el tipo de entrenamiento del jugador
            int entrenamiento = _logicaJugador.EntrenamientoJugador(idJugador);

            // Definir los botones en una lista para facilitar el acceso
            var botones = new Dictionary<int, Button>
            {
                { 1, btnPortero },
                { 2, btnEntradas },
                { 3, btnRemate },
                { 4, btnPase },
                { 5, btnRegate },
                { 6, btnTiro },
                { 0, btnNoEntrenar }
            };

            // Definir los colores
            SolidColorBrush colorActivo = new SolidColorBrush(Color.FromRgb(0x1D, 0x6A, 0x7D));
            SolidColorBrush colorInactivo = new SolidColorBrush(Color.FromArgb(0xFF, 0x23, 0x28, 0x2D));

            // Aplicar cambios
            foreach (var kvp in botones)
            {
                kvp.Value.Background = (kvp.Key == entrenamiento) ? colorActivo : colorInactivo;
                kvp.Value.IsEnabled = kvp.Key != entrenamiento;
            }

            //Mostrar la foto y el texto del entrenamiento
            switch (entrenamiento)
            {
                case 1:
                    txtTipoEntrenamiento.Text = "PORTERO";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/portero.png"));
                    break;
                case 2:
                    txtTipoEntrenamiento.Text = "ENTRADAS";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/entradas.png"));
                    break;
                case 3:
                    txtTipoEntrenamiento.Text = "REMATE";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/remate.png"));
                    break;
                case 4:
                    txtTipoEntrenamiento.Text = "PASE";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/pase.png"));
                    break;
                case 5:
                    txtTipoEntrenamiento.Text = "REGATE";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/regate.png"));
                    break;
                case 6:
                    txtTipoEntrenamiento.Text = "TIRO";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/tiro.png"));
                    break;
                default:
                    txtTipoEntrenamiento.Text = "";
                    imgEntrenamiento.Source = new BitmapImage(new Uri($"pack://application:,,,/Recursos/img/sinEntrenamiento.png"));
                    break;
            }
        }
        #endregion
    }
}
