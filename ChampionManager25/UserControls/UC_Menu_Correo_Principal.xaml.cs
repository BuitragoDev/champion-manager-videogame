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

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Correo_Principal : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private int idMensaje;
        private Border borderSeleccionado = null;
        #endregion

        // Instancias de la LOGICA
        MensajeLogica _logicaMensaje = new MensajeLogica();
        EquipoLogica _logicaEquipos = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();

        public UC_Menu_Correo_Principal(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefrescarPanelMensajes();
        }

        // ------------------------------------------------------------------------ Evento CLICK del Botón MARCAR COMO LEÍDO
        private void botonMarcarLeido_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            _logicaMensaje.MarcarComoLeido(idMensaje);
            RefrescarPanelMensajes();
        }

        #region "Métodos"
        private void RefrescarPanelMensajes()
        {
            // Obtener los mensajes
            List<Mensaje> listado = _logicaMensaje.MostrarMisMensajes(_manager.IdManager);

            // Limpiar el DockPanel antes de cargar nuevos mensajes
            panelListaDeCorreos.Children.Clear();

            foreach (var mensaje in listado)
            {
                Border mensajeBorder;
                if (mensaje.Leido == false)
                {
                    // Crear el Border que contiene cada mensaje
                    mensajeBorder = new Border
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        Background = Brushes.LightGray,
                        Margin = new Thickness(5),
                        Padding = new Thickness(5),
                        Width = 635,
                        Cursor = Cursors.Hand
                    };
                }
                else
                {
                    // Crear el Border que contiene cada mensaje
                    mensajeBorder = new Border
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(1),
                        Background = Brushes.WhiteSmoke,
                        Margin = new Thickness(5),
                        Padding = new Thickness(5),
                        Width = 635,
                        Cursor = Cursors.Hand
                    };
                }


                // Crear el Grid dentro del Border
                Grid mensajeGrid = new Grid
                {
                    Margin = new Thickness(5)
                };

                // Definir las filas y columnas del Grid (ahora con 3 filas)
                mensajeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });  // Fila para la fecha
                mensajeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });  // Fila para el remitente
                mensajeGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });  // Fila para el asunto
                mensajeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });  // Columna para la imagen
                mensajeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });  // Columna para el texto

                // Columna izquierda (Imagen del equipo con RowSpan = 3)
                Image imgEquipo = new Image
                {
                    Width = 80,
                    Height = 80,
                    Stretch = Stretch.UniformToFill,
                    Margin = new Thickness(5)
                };

                try
                {
                    if (mensaje.Icono == 0)
                    {
                        int idEquipo = mensaje.IdEquipo ?? 0;  
                        Equipo equipo = _logicaEquipos.ListarDetallesEquipo(idEquipo);
                        imgEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen80));
                    }
                    else if (mensaje.Icono > 0)
                    {
                        Jugador jugador = _logicaJugador.MostrarDatosJugador(mensaje.Icono);
                        imgEquipo.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
                    }
                }
                catch
                {
                    // Si no se encuentra la imagen, usar una predeterminada
                    imgEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/80x80/default.png"));
                }
                Grid.SetColumn(imgEquipo, 0);
                Grid.SetRow(imgEquipo, 0);
                Grid.SetRowSpan(imgEquipo, 3); // RowSpan de 3 para que ocupe toda la altura del Grid
                mensajeGrid.Children.Add(imgEquipo);

                // Fila superior derecha (Fecha)
                // Fila superior derecha (Fecha)
                TextBlock fechaText = new TextBlock
                {
                    Text = $"{mensaje.Fecha.ToString("dd/MM/yyyy")}",  // Formatear la fecha
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.Black,
                    FontSize = 14,
                    HorizontalAlignment = HorizontalAlignment.Left,  // Alineado a la izquierda de la columna
                    Margin = new Thickness(10, 0, 0, 0)
                };
                Grid.SetColumn(fechaText, 1);
                Grid.SetRow(fechaText, 0);
                mensajeGrid.Children.Add(fechaText);
                // Fila media derecha (Remitente)
                TextBlock remitenteText = new TextBlock
                {
                    Text = $"{mensaje.Remitente}",
                    FontFamily = new FontFamily("Cascadia Code SemiBold"),
                    FontWeight = FontWeights.Bold,
                    Foreground = Brushes.DarkRed,
                    FontSize = 20,
                    Margin = new Thickness(12, 0, 0, 0)
                };
                Grid.SetColumn(remitenteText, 1);
                Grid.SetRow(remitenteText, 1);
                mensajeGrid.Children.Add(remitenteText);

                // Fila inferior derecha (Asunto)
                TextBlock asuntoText = new TextBlock
                {
                    Text = $"{mensaje.Asunto}",
                    Foreground = Brushes.Black,
                    FontFamily = new FontFamily("Cascadia Code Light"),
                    FontSize = 14,
                    Width = 500,  // Ancho fijo de 500px
                    TextWrapping = TextWrapping.Wrap  // Permite que el texto se ajuste a múltiples líneas
                };
                Grid.SetColumn(asuntoText, 1);
                Grid.SetRow(asuntoText, 2);
                mensajeGrid.Children.Add(asuntoText);

                // Asignar el evento de clic al Border
                mensajeBorder.MouseLeftButtonDown += (s, eArgs) =>
                {
                    Metodos.ReproducirSonidoClick();

                    if (mensaje != null)
                    {
                        if (mensaje.Icono == 0)
                        {
                            int idEquipo = mensaje.IdEquipo ?? 0;
                            Equipo equipo = _logicaEquipos.ListarDetallesEquipo(idEquipo);
                            imgLogoMensaje.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + equipo.RutaImagen120));
                        }
                        else if (mensaje.Icono > 0)
                        {
                            Jugador jugador = _logicaJugador.MostrarDatosJugador(mensaje.Icono);
                            imgLogoMensaje.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
                        }

                        txtFechaMensaje.Text = mensaje.Fecha.ToString("dd/MM/yyyy");
                        txtRemitenteMensaje.Text = mensaje.Remitente;
                        txtAsuntoMensaje.Text = mensaje.Asunto;
                        txtContenidoMensaje.Text = mensaje.Contenido;
                        botonMarcarLeido.Visibility = Visibility.Visible;
                        idMensaje = mensaje.IdMensaje;

                        // Restaurar el grosor del Border previamente seleccionado
                        if (borderSeleccionado != null && borderSeleccionado != mensajeBorder)
                        {
                            borderSeleccionado.BorderThickness = new Thickness(1);
                        }

                        // Establecer el nuevo Border seleccionado con grosor más grande
                        mensajeBorder.BorderThickness = new Thickness(3);
                        borderSeleccionado = mensajeBorder;
                    }
                    else
                    {
                        limpiarCampos();
                    }
                };

                // Imagen de la papelera a la derecha de la fecha
                Image imgPapelera = new Image
                {
                    Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/icons/borrar_icon.png")), // Ruta de la imagen
                    Width = 32,
                    Height = 32,
                    Margin = new Thickness(5),
                    HorizontalAlignment = HorizontalAlignment.Right, // Alineada a la derecha de la columna
                    Cursor = Cursors.Hand // Cambiar el cursor al pasar el mouse
                };
                Grid.SetColumn(imgPapelera, 1);
                Grid.SetRow(imgPapelera, 0);

                // Evento para manejar el clic en la papelera
                imgPapelera.MouseLeftButtonDown += (s, e) =>
                {
                    Metodos.ReproducirSonidoClick();

                    // Detener la propagación del evento al Border
                    e.Handled = true;

                    // Verifica si el mensaje que se está eliminando es el seleccionado
                    if (idMensaje == mensaje.IdMensaje)
                    {
                        limpiarCampos(); // Limpia los campos si el mensaje eliminado estaba seleccionado
                    }

                    // Elimina el mensaje de la base de datos
                    _logicaMensaje.eliminarMensaje(mensaje.IdMensaje);

                    // Refresca el panel después de eliminar
                    RefrescarPanelMensajes();
                };

                // Añadir la imagen de la papelera al Grid
                mensajeGrid.Children.Add(imgPapelera);

                // Añadir el Grid al Border
                mensajeBorder.Child = mensajeGrid;

                // Añadir el Border al WrapPanel
                panelListaDeCorreos.Children.Add(mensajeBorder);
            }
        }

        private int ObtenerNumeroMensajes()
        {
            // Aquí deberías obtener el número actualizado de mensajes no leídos
            return _logicaMensaje.MensajesNoLeidos(_manager.IdManager);
        }

        private void limpiarCampos()
        {
            imgLogoMensaje.Source = null;
            txtFechaMensaje.Text = "";
            txtRemitenteMensaje.Text = "";
            txtAsuntoMensaje.Text = "";
            txtContenidoMensaje.Text = "";
            botonMarcarLeido.Visibility = Visibility.Hidden;
        }
        #endregion
    }
}
