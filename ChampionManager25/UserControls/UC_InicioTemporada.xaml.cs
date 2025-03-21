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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChampionManager25.UserControls
{
    public partial class UC_InicioTemporada : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;
        private List<int> _idsPartidos;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        PartidoLogica _logicaPartido = new PartidoLogica();

        public UC_InicioTemporada(Manager manager, int equipo, List<int> ids)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            _idsPartidos = ids;
        }

        private void inicioTemporada_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurarDataGridObjetivos(1);

            imgLogoEquipo.Source = new BitmapImage(new Uri("pack://application:,,,/Recursos/img/escudos_equipos/" + _equipo + ".png"));
            lblNombreEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Nombre.ToUpper();
            lblObjetivoEquipo.Text = _logicaEquipo.ListarDetallesEquipo(_equipo).Objetivo;
            lblNombreManager.Text = _manager.Nombre + " " + _manager.Apellido;
        }

        // ---------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON ATRÁS
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            _logicaPartido.eliminarPartidos(_idsPartidos);

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPretemporada(_manager, _equipo);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------ EVENTO CLICK DEL BOTON CONTINUAR 
        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Notificar a MainWindow para cargar el nuevo UserControl
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPresentacion(_manager, _equipo, _idsPartidos);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private void ConfigurarDataGridObjetivos(int competicion)
        {
            // Limpiar cualquier configuración previa
            dgObjetivos.Columns.Clear();

            // Crear las columnas de manera manual
            dgObjetivos.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Nombre"),
                Header = "EQUIPO",
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.PaddingProperty, new Thickness(10, 0, 0, 0)) // Margen izquierdo en el texto
                    }
                }
            });

            dgObjetivos.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Objetivo"),
                Header = "OBJETIVO",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.PaddingProperty, new Thickness(10, 0, 0, 0)) // Margen izquierdo en el texto
                    }
                }
            });

            dgObjetivos.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Entrenador"),
                Header = "ENTRENADOR",
                Width = new DataGridLength(250, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center),
                        new Setter(TextBlock.PaddingProperty, new Thickness(10, 0, 0, 0)) // Margen izquierdo en el texto
                    }
                }
            });

            // Limpiar cualquier fila previa en el DataGrid
            dgObjetivos.Items.Clear();

            // Obtener la lista de equipos
            List<Equipo> listadoEquipos = _logicaEquipo.ListarEquiposCompeticion(competicion);

            // Agregar las filas al DataGrid
            foreach (var equipo in listadoEquipos)
            {
                if (equipo.IdEquipo != _equipo)
                {
                    // Crear un objeto de fila manualmente, no necesitamos DataGridRow directamente
                    dgObjetivos.Items.Add(equipo);
                }
            }

            // Configurar propiedades generales del DataGrid
            dgObjetivos.RowHeight = 28;
            dgObjetivos.RowBackground = new SolidColorBrush(Colors.LightGray);  // Color de fondo de las filas
            dgObjetivos.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);  // Fondo alternado (si lo deseas)
            dgObjetivos.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgObjetivos.BorderThickness = new Thickness(0);
            dgObjetivos.CanUserAddRows = false;
            dgObjetivos.CanUserResizeColumns = false;
            dgObjetivos.CanUserResizeRows = false;
            dgObjetivos.SelectionMode = DataGridSelectionMode.Single;
            dgObjetivos.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgObjetivos.HeadersVisibility = DataGridHeadersVisibility.Column; // Mostrar cabeceras
            dgObjetivos.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            // Estilo de cabeceras
            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 16.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HeightProperty, 30.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Left));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(10, 0, 0, 0))); // Margen izquierdo en el encabezado
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgObjetivos.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgObjetivos.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Transparent)),
                    new Setter(Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(35, 40, 45))),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code Light")),
                    new Setter(Control.FontSizeProperty, 14.0),
                    new Setter(Control.PaddingProperty, new Thickness(10, 0, 0, 0)), // Margen izquierdo de 10px en las celdas
                    new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent),
                    new Setter(DataGridCell.BorderThicknessProperty, new Thickness(0))
                }
            };
        }
        #endregion
    }
}
