using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Entrenador_Rival : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private DateTime hoy = Metodos.hoy;
        Equipo equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        PartidoLogica _logicaPartidos = new PartidoLogica();
        EntrenadorLogica _logicaEntrenador = new EntrenadorLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();

        public UC_Menu_Entrenador_Rival(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void rival_Loaded(object sender, RoutedEventArgs e)
        {
            // --------------------------------------------------------------------------------------------------- JUGADORES PLANTILLA
            // Obtener el próximo partido
            Partido proximoPartido = _logicaPartidos.ObtenerProximoPartido(_equipo, _manager.IdManager, hoy);

            txtFecha.Text += " (" + proximoPartido.FechaPartido.ToString("dd-MM-yyyy") + ")";

            // Determinar el ID del equipo rival
            int rival = (proximoPartido.IdEquipoLocal == _equipo) ? proximoPartido.IdEquipoVisitante : proximoPartido.IdEquipoLocal;

            txtPlantilla.Text = _logicaEquipo.ListarDetallesEquipo(rival).Nombre.ToUpper();

            // Obtener la lista de jugadores del equipo rival
            List<Jugador> listaEquipo = _logicaJugador.ListadoJugadoresCompleto(rival);
            CargarListajugadores(listaEquipo);

            // --------------------------------------------------------------------------------------------------- ENTRENADOR
            Entrenador coach = _logicaEntrenador.MostrarEntrenador(rival);
            imgFotoEntrenador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + coach.RutaImagen));
            txtNombreEntrenador.Text = coach.NombreCompleto;
            MostrarEstrellas(coach.Reputacion);
            txtTactica.Text = coach.TacticaFavorita;

            // --------------------------------------------------------------------------------------------------- ULTIMOS PARTIDOS
            // Referencia al Grid dentro del Border
            Grid gridPartidos = bordeUltimosPartidos.Child as Grid;
            if (gridPartidos == null) return;

            // Limpiar filas anteriores si existen
            gridPartidos.Children.Clear();

            // Obtener los últimos 5 partidos
            List<Partido> listaPartidos = _logicaPartidos.UltimosCincoPartidos(rival, _manager.IdManager);

            int fila = 0;
            foreach (var partido in listaPartidos)
            {
                equipo = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal);
                // Escudo Local
                Image escudoLocal = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen32)),
                    Width = 32,
                    Height = 32
                };
                Grid.SetColumn(escudoLocal, 0);
                Grid.SetRow(escudoLocal, fila);
                gridPartidos.Children.Add(escudoLocal);

                // Nombre Equipo Local
                TextBlock nombreLocal = new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoLocal).Nombre, // Método para obtener el nombre
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold
                };
                Grid.SetColumn(nombreLocal, 1);
                Grid.SetRow(nombreLocal, fila);
                gridPartidos.Children.Add(nombreLocal);

                // Goles Local
                TextBlock golesLocal = new TextBlock
                {
                    Text = partido.GolesLocal.ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 32
                };
                Grid.SetColumn(golesLocal, 2);
                Grid.SetRow(golesLocal, fila);
                gridPartidos.Children.Add(golesLocal);

                // Goles Local
                TextBlock separador = new TextBlock
                {
                    Text = "-",
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 32
                };
                Grid.SetColumn(separador, 3);
                Grid.SetRow(separador, fila);
                gridPartidos.Children.Add(separador);

                // Goles Visitante
                TextBlock golesVisitante = new TextBlock
                {
                    Text = partido.GolesVisitante.ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 32
                };
                Grid.SetColumn(golesVisitante, 4);
                Grid.SetRow(golesVisitante, fila);
                gridPartidos.Children.Add(golesVisitante);

                // Nombre Equipo Visitante
                TextBlock nombreVisitante = new TextBlock
                {
                    Text = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante).Nombre,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold
                };
                Grid.SetColumn(nombreVisitante, 5);
                Grid.SetRow(nombreVisitante, fila);
                gridPartidos.Children.Add(nombreVisitante);

                // Escudo Visitante
                equipo = _logicaEquipo.ListarDetallesEquipo(partido.IdEquipoVisitante);
                Image escudoVisitante = new Image
                {
                    Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen32)),
                    Width = 32,
                    Height = 32
                };
                Grid.SetColumn(escudoVisitante, 6);
                Grid.SetRow(escudoVisitante, fila);
                gridPartidos.Children.Add(escudoVisitante);

                fila++;
            }

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

                // Estadisticas del Jugador
                Estadistica stats = _logicaEstadistica.MostrarEstadisticasJugador(idJugador, _manager.IdManager);
                txtGoles.Text = stats.Goles.ToString();
                txtAsistencias.Text = stats.Asistencias.ToString();
                txtMVP.Text = stats.MVP.ToString();
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

            if (reputacion == 100)
            {
                numeroEstrellas = 5;
            }
            else if (reputacion > 90)
            {
                numeroEstrellas = 4;
            }
            else if (reputacion > 80)
            {
                numeroEstrellas = 3;
            }
            else if (reputacion > 70)
            {
                numeroEstrellas = 2;
            }
            else if (reputacion > 60)
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
