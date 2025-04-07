using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
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
using static System.Resources.ResXFileRef;

namespace ChampionManager25.UserControls
{
    public partial class UC_CargarPartida : UserControl
    {
        PartidaGuardada partidaSeleccionada;
        private Border borderSeleccionado = null; // Variable para almacenar el Border seleccionado

        ManagerLogica _logicaManager = new ManagerLogica();
        private static EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_CargarPartida()
        {
            InitializeComponent();
        }

        private void cargarPartida_Loaded(object sender, RoutedEventArgs e)
        {
            CargarPartidas();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Restaurar la conexión a la BD base
            string bdBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "championsManagerDB.db");
            SQLiteDatabaseManager.UpdateConnectionString(bdBase);

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }

        // -------------------------------------------------------------------------------------- Evento CLICK del boton CARGAR PARTIDA
        private void btnCargarPartida_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Actualizar la cadena de conexión con la partida seleccionada
            SQLiteDatabaseManager.UpdateConnectionString(partidaSeleccionada.RutaCompleta);

            // Cargar el Manager y equipo de la partida seleccionada
            Manager manager = _logicaManager.MostrarManager(1);
            int equipo = partidaSeleccionada.IdEquipo;

            // Cargar la pantalla principal del juego
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPantallaPrincipal(manager, equipo);
        }

        #region "Metodos"
        public void CargarPartidas()
        {
            var partidas = ObtenerPartidasGuardadas();

            // Limpiar el WrapPanel antes de añadir nuevos elementos
            wrapPartidasCargadas.Children.Clear();

            // Crear los elementos dinámicamente
            foreach (var partida in partidas)
            {
                // Crear un Border que contendrá el Grid
                Border border = new Border
                {
                    Background = Brushes.LightGray,
                    BorderBrush = new SolidColorBrush(Colors.DarkGray),
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(5),
                    Cursor = Cursors.Hand,
                    Width = 1020,
                    Height = 100
                };

                // Agregar los eventos MouseEnter y MouseLeave al Border
                border.MouseEnter += (s, e) =>
                {
                    // Si el border ya está seleccionado, no cambiar el color
                    if (border != borderSeleccionado)
                    {
                        border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a0be99"));
                    }
                };
                border.MouseLeave += (s, e) =>
                {
                    // Si el border no está seleccionado, restaurar el color de fondo
                    if (border != borderSeleccionado)
                    {
                        border.Background = new SolidColorBrush(Colors.LightGray);
                    }
                };

                // Agregar el evento de clic al Border
                border.MouseLeftButtonDown += (s, e) => SeleccionarPartida(partida, border);

                // Crear un Grid dentro del Border
                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) }); // Columna para el escudo
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Star) }); // Columna para el nombre de la partida
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220, GridUnitType.Star) }); // Columna para la fecha
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });

                // Crear la imagen para el escudo del equipo
                Image imgEscudo = new Image
                {
                    Width = 80,
                    Height = 80,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                string escudoPath = $"/Recursos/img/escudos_equipos/80x80/{partida.IdEquipo}.png";
                imgEscudo.Source = new BitmapImage(new Uri(escudoPath, UriKind.RelativeOrAbsolute));

                Grid.SetColumn(imgEscudo, 0);
                grid.Children.Add(imgEscudo);

                // Crear el TextBlock para el nombre de la partida (nombre del equipo)
                TextBlock nombrePartidaTextBlock = new TextBlock
                {
                    Text = partida.NombreVisible,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 18,
                    Foreground = Brushes.Black // Color inicial del texto
                };
                Grid.SetColumn(nombrePartidaTextBlock, 1);  // Colocar en la primera columna

                // Crear el TextBlock para la fecha
                TextBlock fechaTextBlock = new TextBlock
                {
                    Text = partida.FechaCreacion.ToString("dd/MM/yyyy"),  // Formato de fecha
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontSize = 18,
                    Foreground = Brushes.Black // Color inicial del texto
                };
                Grid.SetColumn(fechaTextBlock, 2);  // Colocar en la segunda columna

                // Crear la imagen para el boton borrar
                Image imgPapelera = new Image
                {
                    Width = 32,
                    Height = 32,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                string papeleraPath = $"/Recursos/img/icons/papelera_icon.png";
                imgPapelera.Source = new BitmapImage(new Uri(papeleraPath, UriKind.RelativeOrAbsolute));

                // Evento de clic para eliminar la partida
                imgPapelera.MouseLeftButtonDown += (s, e) => BorrarPartida(partida);

                Grid.SetColumn(imgPapelera, 3);  // Colocar en la cuarta columna
                grid.Children.Add(imgPapelera);

                // Añadir los TextBlocks al Grid
                grid.Children.Add(nombrePartidaTextBlock);
                grid.Children.Add(fechaTextBlock);

                // Añadir el Grid al Border
                border.Child = grid;

                // Añadir el Border al WrapPanel
                wrapPartidasCargadas.Children.Add(border);
            }
        }

        public static List<PartidaGuardada> ObtenerPartidasGuardadas()
        {
            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PartidasGuardadas");
            var lista = new List<PartidaGuardada>();

            if (!Directory.Exists(carpeta)) return lista;

            var archivos = Directory.GetFiles(carpeta, "manager*.db");

            foreach (var archivo in archivos)
            {
                var info = new FileInfo(archivo);

                try
                {
                    // Intentamos obtener el equipo de la base de datos
                    Manager equipo = ObtenerNombreEquipoDesdeBD(archivo);

                    // Usamos conexión temporal para evitar afectar la global
                    string nombreEquipo = SQLiteDatabaseManager.EjecutarConConexionTemporal(
                        archivo,
                        () => _logicaEquipo.ListarDetallesEquipo(equipo.IdEquipo ?? 0).Nombre
                    );

                    // Solo si no hay error, se agrega la partida a la lista
                    lista.Add(new PartidaGuardada
                    {
                        NombreArchivo = Path.GetFileNameWithoutExtension(archivo),
                        RutaCompleta = archivo,
                        FechaCreacion = info.CreationTime,
                        NombreVisible = $"Partida con el {nombreEquipo}\n\n{equipo.Nombre.ToUpper()} {equipo.Apellido.ToUpper()}",
                        IdEquipo = equipo.IdEquipo ?? 0
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al cargar partida de {archivo}: {ex.Message}");
                }
            }

            return lista;
        }
        private static Manager ObtenerNombreEquipoDesdeBD(string rutaBD)
        {
            try
            {
                using (var conn = new SQLiteConnection($"Data Source={rutaBD};Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT m.nombre, m.apellido, m.nacionalidad, e.id_equipo FROM managers m JOIN equipos e ON m.id_equipo = e.id_equipo LIMIT 1";

                    using (var cmd = new SQLiteCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Manager
                            {
                                Nombre = reader.GetString(0),   
                                Apellido = reader.GetString(1), 
                                Nacionalidad = reader.GetString(2),
                                IdEquipo = reader.GetInt32(3),  
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al realizar la consulta");
            }
            return null; // Valor por defecto en caso de no encontrar el equipo
        }

        private void SeleccionarPartida(PartidaGuardada partida, Border border) 
        {
            Metodos.ReproducirSonidoClick();
            partidaSeleccionada = partida;

            // Si ya hay un Border seleccionado, restaurar su color
            if (borderSeleccionado != null)
            {
                borderSeleccionado.Background = Brushes.LightGray;
            }

            // Establecer el nuevo Border como seleccionado y cambiar su color
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a0be99"));

            // Guardar el nuevo Border como seleccionado
            borderSeleccionado = border;
        }

        private void BorrarPartida(PartidaGuardada partida)
        {
            // Eliminar el archivo de la carpeta PartidasGuardadas
            if (File.Exists(partida.RutaCompleta))
            {
                File.Delete(partida.RutaCompleta);
            }

            // Actualizar la UI para reflejar los cambios
            CargarPartidas();
        }
        #endregion
    }
}
