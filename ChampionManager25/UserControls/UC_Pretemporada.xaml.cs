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
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;

namespace ChampionManager25.UserControls
{
    public partial class UC_Pretemporada : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<int> _idsPartidos;

        private int numeroPartido = 1;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();
        Equipo miEquipo = null;

        // Las fechas a asignar
        private List<string> fechas = new List<string>
        {
            new DateTime(Metodos.temporadaActual, 7, 29).ToString("dd/MM/yyyy"),
            new DateTime(Metodos.temporadaActual, 7, 31).ToString("dd/MM/yyyy"),
            new DateTime(Metodos.temporadaActual, 8, 2).ToString("dd/MM/yyyy"),
            new DateTime(Metodos.temporadaActual, 8, 4).ToString("dd/MM/yyyy"),
            new DateTime(Metodos.temporadaActual, 8, 6).ToString("dd/MM/yyyy")
        };

        // Lista con los equipos seleccionados.
        List<int> rivales = new List<int> { 0, 0, 0, 0, 0 };

        // Lista con las posiciones vetadas para no jugar.
        List<int> noJugar = new List<int>();

        // Variables botones No Jugar
        private bool noJugar1 = false;
        private bool noJugar2 = false;
        private bool noJugar3 = false;
        private bool noJugar4 = false;
        private bool noJugar5 = false;

        // Lista con los ids de los partidos creados.
        private List<int> idsPartidos = new List<int>();

        public UC_Pretemporada(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;

            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
        }

        public UC_Pretemporada(Manager manager, int equipo, List<int> ids)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _idsPartidos = ids;

            miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
        }

        private void pretemporada_Loaded(object sender, RoutedEventArgs e)
        {
            lblNombreMiEquipo.Text = "Rivales de pretemporada del " + miEquipo.Nombre.ToUpper();
            string rutaImagenMiLogo = $"/Recursos/img/escudos_equipos/64x64/{miEquipo.IdEquipo}.png";
            imgLogoMiEquipo.Source = new BitmapImage(new Uri(rutaImagenMiLogo, UriKind.Relative));

            CargarEscudos(1, 1);
        }

        // -------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Eliminar los partidos creados.
            _logicaPartido.eliminarPartidos(idsPartidos);

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarSeleccionEquipo(_manager);
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON AVANZAR
        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos la lista de ids.
            idsPartidos.Clear();

            // Competición siempre será 10 para amistosos
            int competicion = 10;

            // Recorrer las primeras 4 fechas, donde mi equipo es el visitante
            for (int i = 0; i < 4; i++)
            {
                if (rivales[i] != 0)
                {
                    string fechaFormateada = DateTime.Parse(fechas[i]).ToString("yyyy-MM-dd"); // Convertir a string sin hora

                    int idPartido = _logicaPartido.crearPartido(
                        rivales[i],
                        _equipo,
                        fechaFormateada, // Pasamos un string con la fecha en formato "yyyy-MM-dd"
                        competicion,
                        0,
                        _manager.IdManager
                    );

                    if (idPartido != -1)
                    {
                        idsPartidos.Add(idPartido); // Guardar el id del partido
                    }
                }
            }


            // El quinto partido será con mi equipo como local y el rival como visitante
            if (rivales[4] != 0)
            {
                string fechaFormateada = DateTime.Parse(fechas[4]).ToString("yyyy-MM-dd"); // Convertir a string sin hora

                int idPartido5 = _logicaPartido.crearPartido(
                    _equipo,
                    rivales[4],
                    fechaFormateada, // Pasamos un string con la fecha en formato "yyyy-MM-dd"
                    competicion,
                    0,
                    _manager.IdManager
                );

                if (idPartido5 != -1)
                {
                    idsPartidos.Add(idPartido5); // Guardar el id del partido
                }
            }

            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarInicioTemporada(_manager, _equipo, idsPartidos);
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // ----------------------------------------------------------------------------------------- Evento CLICK de la imagen de CHAMPIONS
        private void imgChampions_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();
            CargarEscudos(1, 1);
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------- Evento CLICK de los botones NO JUGAR 
        private void btnNoJugar1_Click(object sender, RoutedEventArgs e)
        {
            btnNoJugar1.Visibility = Visibility.Hidden;
            btnBorrar1.Visibility = Visibility.Visible;
            fila2.Visibility = Visibility.Visible;
            numeroPartido = 2;
            noJugar.Add(0);
            noJugar1 = true;
        }

        private void btnNoJugar2_Click(object sender, RoutedEventArgs e)
        {
            btnNoJugar2.Visibility = Visibility.Hidden;
            btnBorrar2.Visibility = Visibility.Visible;
            fila3.Visibility = Visibility.Visible;
            numeroPartido = 3;
            noJugar.Add(1);
            noJugar2 = true;
        }

        private void btnNoJugar3_Click(object sender, RoutedEventArgs e)
        {
            btnNoJugar3.Visibility = Visibility.Hidden;
            btnBorrar3.Visibility = Visibility.Visible;
            fila4.Visibility = Visibility.Visible;
            numeroPartido = 4;
            noJugar.Add(2);
            noJugar3 = true;
        }

        private void btnNoJugar4_Click(object sender, RoutedEventArgs e)
        {
            btnNoJugar4.Visibility = Visibility.Hidden;
            btnBorrar4.Visibility = Visibility.Visible;
            fila5.Visibility = Visibility.Visible;
            numeroPartido = 5;
            noJugar.Add(3);
            noJugar4 = true;
        }

        private void btnNoJugar5_Click(object sender, RoutedEventArgs e)
        {
            btnNoJugar5.Visibility = Visibility.Hidden;
            btnBorrar5.Visibility = Visibility.Visible;
            numeroPartido = 6;
            noJugar.Add(4);
            noJugar5 = true;
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ---------------------------------------------------------------------------------------------- Evento CLICK de los botones BORRAR 
        private void btnBorrar1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Rellenar los campos de la fila 1 con los datos del equipo
            txtTipo1.Text = "";

            //Logo
            imgLogo1.Source = null;

            txtRival1.Text = "";
            txtCancha1.Text = "";
            txtFecha1.Text = "";

            // Ocultar el botón
            btnNoJugar1.Visibility = Visibility.Visible;
            btnBorrar1.Visibility = Visibility.Hidden;

            // Borrar el rival de la lista
            habilitarImagen(0);

            // Variable de control del Boton no jugar.
            if (noJugar1 == true)
            {
                // Verificar si la lista contiene el valor 1
                if (noJugar.Contains(0))
                {
                    // Eliminar el valor 1 de la lista
                    noJugar.Remove(0);
                }
            }

            // Posicionamos el primer hueco vacío
            hayHuecosVacios();
        }

        private void btnBorrar2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Rellenar los campos de la fila 2 con los datos del equipo
            txtTipo2.Text = "";

            //Logo
            imgLogo2.Source = null;

            txtRival2.Text = "";
            txtCancha2.Text = "";
            txtFecha2.Text = "";

            // Ocultar el botón
            btnNoJugar2.Visibility = Visibility.Visible;
            btnBorrar2.Visibility = Visibility.Hidden;

            // Borrar el rival de la lista
            habilitarImagen(1);

            // Variable de control del Boton no jugar.
            if (noJugar2 == true)
            {
                // Verificar si la lista contiene el valor 1
                if (noJugar.Contains(1))
                {
                    // Eliminar el valor 1 de la lista
                    noJugar.Remove(1);
                }
            }

            // Posicionamos el primer hueco vacío
            hayHuecosVacios();
        }

        private void btnBorrar3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Rellenar los campos de la fila 3 con los datos del equipo
            txtTipo3.Text = "";

            //Logo
            imgLogo3.Source = null;

            txtRival3.Text = "";
            txtCancha3.Text = "";
            txtFecha3.Text = "";

            // Ocultar el botón
            btnNoJugar3.Visibility = Visibility.Visible;
            btnBorrar3.Visibility = Visibility.Hidden;

            // Borrar el rival de la lista
            habilitarImagen(2);

            // Variable de control del Boton no jugar.
            if (noJugar3 == true)
            {
                // Verificar si la lista contiene el valor 1
                if (noJugar.Contains(2))
                {
                    // Eliminar el valor 1 de la lista
                    noJugar.Remove(2);
                }
            }

            // Posicionamos el primer hueco vacío
            hayHuecosVacios();
        }

        private void btnBorrar4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Rellenar los campos de la fila 4 con los datos del equipo
            txtTipo4.Text = "";

            //Logo
            imgLogo4.Source = null;

            txtRival4.Text = "";
            txtCancha4.Text = "";
            txtFecha4.Text = "";

            // Ocultar el botón
            btnNoJugar4.Visibility = Visibility.Visible;
            btnBorrar4.Visibility = Visibility.Hidden;

            // Borrar el rival de la lista
            habilitarImagen(3);

            // Variable de control del Boton no jugar.
            if (noJugar4 == true)
            {
                // Verificar si la lista contiene el valor 1
                if (noJugar.Contains(3))
                {
                    // Eliminar el valor 1 de la lista
                    noJugar.Remove(3);
                }
            }

            // Posicionamos el primer hueco vacío
            hayHuecosVacios();
        }

        private void btnBorrar5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Rellenar los campos de la fila 5 con los datos del equipo
            txtTipo5.Text = "";

            //Logo
            imgLogo5.Source = null;

            txtRival5.Text = "";
            txtCancha5.Text = "";
            txtFecha5.Text = "";

            // Ocultar el botón
            btnNoJugar5.Visibility = Visibility.Visible;
            btnBorrar5.Visibility = Visibility.Hidden;

            // Borrar el rival de la lista
            habilitarImagen(4);

            // Variable de control del Boton no jugar.
            if (noJugar5 == true)
            {
                // Verificar si la lista contiene el valor 1
                if (noJugar.Contains(4))
                {
                    // Eliminar el valor 1 de la lista
                    noJugar.Remove(4);
                }
            }

            // Posicionamos el primer hueco vacío
            hayHuecosVacios();
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private void CargarEscudos(int minimo, int maximo)
        {
            // Obtener la lista de equipos
            List<Equipo> oEquipos = _logicaEquipo.ListarEquiposCompeticion(1);

            // Limpiar el WrapPanel antes de agregar elementos (opcional)
            wrapPanelEquipos.Children.Clear();

            // Ruta base de las imágenes
            string rutaBase = "/Recursos/img/escudos_equipos/64x64/";

            foreach (var equipo in oEquipos)
            {
                if (equipo.IdEquipo != _equipo)
                {
                    // Determinar si el equipo está en la lista de rivales
                    bool estaEnRivales = rivales.Contains(equipo.IdEquipo);

                    // Crear una instancia de Image
                    Image imgEquipo = new Image
                    {
                        Width = 64,
                        Height = 64,
                        Margin = new Thickness(5),
                        Stretch = Stretch.Uniform,
                        Cursor = Cursors.Hand, // Opcional: para indicar interactividad
                        Tag = equipo, // Guardar el objeto equipo en Tag
                        IsEnabled = !rivales.Contains(equipo.IdEquipo), // Deshabilitar si el equipo está en la lista rivales
                        Opacity = estaEnRivales ? 0.5 : 1.0 // Reducir la opacidad si está en rivales
                    };

                    // Establecer el Source de la imagen
                    string rutaImagen = $"{rutaBase}{equipo.IdEquipo}.png";
                    imgEquipo.Source = new BitmapImage(new Uri(rutaImagen, UriKind.Relative));

                    // Asignar el ToolTip con el nombre del equipo
                    imgEquipo.ToolTip = equipo.Nombre; // Aquí asignas el nombre del equipo al ToolTip

                    // Asignar el evento para manejar clics
                    imgEquipo.MouseLeftButtonDown += ImgEquipo_MouseLeftButtonDown;

                    // Agregar la imagen al WrapPanel
                    wrapPanelEquipos.Children.Add(imgEquipo);
                }
            }
        }

        // Evento clic para cada imagen
        private void ImgEquipo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Obtener el objeto equipo desde el Tag de la imagen
            Image imagenSeleccionada = sender as Image;
            Equipo equipo = imagenSeleccionada?.Tag as Equipo;

            if (equipo != null && numeroPartido <= 5)
            {
                // Deshabilitar la imagen (hacerla no interactiva)
                imagenSeleccionada.Cursor = Cursors.Arrow;  // Cambiar el cursor para que no sea un puntero de mano
                imagenSeleccionada.IsEnabled = false;  // Deshabilitar la imagen para que no pueda volver a ser clickeada
                imagenSeleccionada.Opacity = 0.5;  // Opcional: disminuir la opacidad para que se vea inactiva

                hayHuecosVacios();

                // Agregar el valor de equipo.IdEquipo
                rivales[numeroPartido - 1] = equipo.IdEquipo;

                if (numeroPartido == 1)
                {
                    // Rellenar los campos de la fila 3 con los datos del equipo
                    txtTipo1.Text = "Amistoso";

                    //Logo
                    string imagePath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
                    imgLogo1.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    txtRival1.Text = equipo.Nombre; // Nombre del equipo rival
                    txtCancha1.Text = equipo.Estadio; // Información de la cancha
                    txtFecha1.Text = fechas[numeroPartido - 1].ToString(); // Fecha (puedes cambiar por la que necesites)

                    // Ocultar el botón
                    btnNoJugar1.Visibility = Visibility.Hidden;
                    btnBorrar1.Visibility = Visibility.Visible;
                    fila2.Visibility = Visibility.Visible;
                    numeroPartido = 2;
                }
                else if (numeroPartido == 2)
                {
                    // Rellenar los campos de la fila 3 con los datos del equipo
                    txtTipo2.Text = "Amistoso";

                    //Logo
                    string imagePath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
                    imgLogo2.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    txtRival2.Text = equipo.Nombre; // Nombre del equipo rival
                    txtCancha2.Text = equipo.Estadio; // Información de la cancha
                    txtFecha2.Text = fechas[numeroPartido - 1].ToString(); // Fecha (puedes cambiar por la que necesites)

                    // Ocultar el botón
                    btnNoJugar2.Visibility = Visibility.Hidden;
                    btnBorrar2.Visibility = Visibility.Visible;
                    fila3.Visibility = Visibility.Visible;
                    numeroPartido = 3;
                }
                else if (numeroPartido == 3)
                {
                    // Rellenar los campos de la fila 3 con los datos del equipo
                    txtTipo3.Text = "Amistoso";

                    //Logo
                    string imagePath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
                    imgLogo3.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    txtRival3.Text = equipo.Nombre; // Nombre del equipo rival
                    txtCancha3.Text = equipo.Estadio; // Información de la cancha
                    txtFecha3.Text = fechas[numeroPartido - 1].ToString(); // Fecha (puedes cambiar por la que necesites)

                    // Ocultar el botón
                    btnNoJugar3.Visibility = Visibility.Hidden;
                    btnBorrar3.Visibility = Visibility.Visible;
                    fila4.Visibility = Visibility.Visible;
                    numeroPartido = 4;
                }
                else if (numeroPartido == 4)
                {
                    // Rellenar los campos de la fila 3 con los datos del equipo
                    txtTipo4.Text = "Amistoso";

                    //Logo
                    string imagePath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
                    imgLogo4.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    txtRival4.Text = equipo.Nombre; // Nombre del equipo rival
                    txtCancha4.Text = equipo.Estadio; // Información de la cancha
                    txtFecha4.Text = fechas[numeroPartido - 1].ToString(); // Fecha (puedes cambiar por la que necesites)

                    // Ocultar el botón
                    btnNoJugar4.Visibility = Visibility.Hidden;
                    btnBorrar4.Visibility = Visibility.Visible;
                    fila5.Visibility = Visibility.Visible;
                    numeroPartido = 5;
                }
                else if (numeroPartido == 5)
                {
                    // Rellenar los campos de la fila 3 con los datos del equipo
                    txtTipo5.Text = "Presentación";

                    //Logo
                    string imagePath = $"/Recursos/img/escudos_equipos/64x64/{equipo.IdEquipo}.png";
                    imgLogo5.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));

                    txtRival5.Text = equipo.Nombre; // Nombre del equipo rival
                    txtCancha5.Text = miEquipo.Estadio; // Información de la cancha
                    txtFecha5.Text = fechas[numeroPartido - 1].ToString(); // Fecha (puedes cambiar por la que necesites)

                    // Ocultar el botón
                    btnNoJugar5.Visibility = Visibility.Hidden;
                    btnBorrar5.Visibility = Visibility.Visible;
                    numeroPartido = 6;
                }
            }
        }

        private void hayHuecosVacios()
        {
            // Posicionamos el primer hueco vacío que no esté vetado
            for (int i = 0; i < rivales.Count; i++)
            {
                if (rivales[i] == 0 && !noJugar.Contains(i))
                {
                    numeroPartido = i + 1; // La posición en base 1
                    break;
                }
            }
        }

        private void habilitarImagen(int pos)
        {
            // Verificar si hay algún equipo en la lista
            if (rivales.Count > 0 && rivales[pos] != 0)
            {
                // Obtener el id del equipo a eliminar
                int idEquipoBorrar = rivales[pos];

                // Actualizar la lista, poniendo a 0 el valor en la posición correspondiente
                rivales[pos] = 0;

                // Crear el nombre del archivo de la imagen que queremos habilitar
                string imagenNombre = $"{idEquipoBorrar}.png";

                // Buscar la imagen en el WrapPanel usando el nombre del archivo
                foreach (UIElement elemento in wrapPanelEquipos.Children)
                {
                    if (elemento is Image imgEquipo)
                    {
                        // Verificar si el Source de la imagen es un BitmapImage
                        if (imgEquipo.Source is BitmapImage bitmapImage)
                        {
                            // Obtener la ruta completa del archivo de la imagen, en base a su URI
                            Uri uriImagen = bitmapImage.UriSource;
                            string rutaImagen = uriImagen.ToString();  // Obtener la URI como una cadena

                            // Verificar si la ruta de la imagen termina con el nombre esperado
                            if (rutaImagen.EndsWith(imagenNombre))
                            {
                                // Habilitar la imagen y permitir que se vuelva a hacer clic
                                imgEquipo.IsEnabled = true;
                                imgEquipo.Opacity = 1;  // Restaurar opacidad si la habías cambiado antes
                                imgEquipo.Cursor = Cursors.Hand;  // Restaurar el cursor de mano para indicar interactividad
                                break;  // Salir del ciclo una vez que encontramos la imagen correspondiente
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}
