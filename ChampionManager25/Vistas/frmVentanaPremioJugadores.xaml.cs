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
using System.Windows.Shapes;
using ChampionManager25.Logica;
using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using ChampionManager25.MisMetodos;
using System.Windows.Controls.Primitives;
using ChampionManager25.UserControls;

namespace ChampionManager25.Vistas
{
    public partial class frmVentanaPremioJugadores : Window
    {
        #region "Variables"
        private List<Jugador> _listaBalonOro;
        private List<Jugador> _listaBotaOro;
        private List<Jugador> _listaMejorOnce;
        private int _equipo;
        #endregion

        // Instancias de la LOGICA
        EquipoLogica _logicaEquipo = new EquipoLogica();
        CompeticionLogica _logicaCompeticion = new CompeticionLogica();
        PalmaresLogica _logicaPalmares = new PalmaresLogica();
        EstadisticasLogica _logicaEstadistica = new EstadisticasLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        TransferenciaLogica _logicaTransferencia = new TransferenciaLogica();

        public frmVentanaPremioJugadores(List<Jugador> listaBalonOro, List<Jugador> listaBotaOro, List<Jugador> listaMejorOnce, int equipo)
        {
            InitializeComponent();
            _listaBalonOro = listaBalonOro;
            _listaBotaOro = listaBotaOro;
            _listaMejorOnce = listaMejorOnce;
            _equipo = equipo;
        }

        private void premiosJugadores_Loaded(object sender, RoutedEventArgs e)
        {
            string ruta_logo = _logicaCompeticion.ObtenerCompeticion(1).RutaImagen80;
            imgLogoCompeticion.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + ruta_logo));

            // Balon de Oro
            imgFotoBalonOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBalonOro[0].RutaImagen));
            imgFotoBalonPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBalonOro[1].RutaImagen));
            imgFotoBalonBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBalonOro[2].RutaImagen));

            string rutaEquipo1 = _logicaEquipo.ListarDetallesEquipo(_listaBalonOro[0].IdEquipo).RutaImagen64;
            string rutaEquipo2 = _logicaEquipo.ListarDetallesEquipo(_listaBalonOro[1].IdEquipo).RutaImagen64;
            string rutaEquipo3 = _logicaEquipo.ListarDetallesEquipo(_listaBalonOro[2].IdEquipo).RutaImagen64;
            imgEquipoOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipo1));
            imgEquipoPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipo2));
            imgEquipoBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipo3));

            txtNombreBalonOro.Text = $"{_listaBalonOro[0].Nombre} {_listaBalonOro[0].Apellido} - {_listaBalonOro[0].Valoracion} puntos";
            txtNombreBalonPlata.Text = $"{_listaBalonOro[1].Nombre} {_listaBalonOro[1].Apellido} - {_listaBalonOro[1].Valoracion} puntos";
            txtNombreBalonBronce.Text = $"{_listaBalonOro[2].Nombre} {_listaBalonOro[2].Apellido} - {_listaBalonOro[2].Valoracion} puntos";

            // Bota de Oro
            imgFotoBotaOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBotaOro[0].RutaImagen));
            imgFotoBotaPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBotaOro[1].RutaImagen));
            imgFotoBotaBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + _listaBotaOro[2].RutaImagen));

            string rutaEquipoBota1 = _logicaEquipo.ListarDetallesEquipo(_listaBotaOro[0].IdEquipo).RutaImagen64;
            string rutaEquipoBota2 = _logicaEquipo.ListarDetallesEquipo(_listaBotaOro[1].IdEquipo).RutaImagen64;
            string rutaEquipoBota3 = _logicaEquipo.ListarDetallesEquipo(_listaBotaOro[2].IdEquipo).RutaImagen64;
            imgEquipoBotaOro.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota1));
            imgEquipoBotaPlata.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota2));
            imgEquipoBotaBronce.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + rutaEquipoBota3));

            txtNombreBotaOro.Text = $"{_listaBotaOro[0].Nombre} {_listaBotaOro[0].Apellido} - {_listaBotaOro[0].Valoracion} goles";
            txtNombreBotaPlata.Text = $"{_listaBotaOro[1].Nombre} {_listaBotaOro[1].Apellido} - {_listaBotaOro[1].Valoracion} goles";
            txtNombreBotaBronce.Text = $"{_listaBotaOro[2].Nombre} {_listaBotaOro[2].Apellido} - {_listaBotaOro[2].Valoracion} goles";

            ConfigurarDataGrid();

            // Actualizar el palmares del premio del Balon de Oro
            _logicaPalmares.AnadirTituloBalonOro(_listaBalonOro[0].IdJugador);
            _logicaPalmares.AnadirPremiosMejorJugador(Metodos.temporadaActual, _listaBalonOro[0].IdJugador, _listaBalonOro[1].IdJugador, _listaBalonOro[2].IdJugador);

            // Actualizar el palmares del premio de la Bota de Oro
            _logicaPalmares.AnadirTituloBotaOro(_listaBotaOro[0].IdJugador);
            _logicaPalmares.AnadirPremiosMaximoGoleador(Metodos.temporadaActual, _listaBotaOro[0].IdJugador, _listaBotaOro[1].IdJugador, _listaBotaOro[2].IdJugador);
        }

        // ----------------------------------------------------------------------------- Evento CLICK del boton TERMINAR TEMPORADA
        private async void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            progressBar.Visibility = Visibility.Visible;

            await Task.Run(() =>
            {
                // Devolver a los jugadores cedidos a sus equipos
                List<Transferencia> traspasos = _logicaTransferencia.ListarTraspasos();
                if (traspasos != null)
                {
                    foreach (var traspaso in traspasos)
                    {
                        if (traspaso.TipoFichaje == 2)
                        {
                            _logicaJugador.CambiarDeEquipo(traspaso.IdJugador, traspaso.IdEquipoOrigen);
                        }
                    }
                }

                // Restar un año de contrato a todos los jugadores y dejar sin equipo si es CERO
                List<Contrato> totalContratosAntes = _logicaJugador.MostrarListaTotalContratos();
                foreach (var contrato in totalContratosAntes)
                {
                    _logicaJugador.RestarAnioContrato(contrato.IdJugador);
                }
                List<Contrato> totalContratosDespues = _logicaJugador.MostrarListaTotalContratos();
                foreach (var contrato in totalContratosDespues)
                {
                    if (contrato.Duracion == 0)
                    {
                        if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Status == 1)
                        {
                            if (contrato.IdEquipo != _equipo)
                            {
                                // Ampliar el contrato dependiendo de la edad
                                if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Edad > 30)
                                {
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, contrato.SalarioAnual, contrato.ClausulaRescision, 1, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                                else
                                {
                                    int nuevoSalario = (int)Math.Round(contrato.SalarioAnual * 0.2 / 100000.0) * 100000;
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, nuevoSalario, contrato.ClausulaRescision, 5, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                            } 
                            else
                            {
                                _logicaJugador.BorrarContrato(contrato.IdJugador);
                                _logicaJugador.CambiarDeEquipo(contrato.IdJugador, 0);
                            }
                        }
                        else if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Status == 2)
                        {
                            if (contrato.IdEquipo != _equipo)
                            {
                                // Ampliar el contrato dependiendo de la edad
                                if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Edad > 30)
                                {
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, contrato.SalarioAnual, contrato.ClausulaRescision, 1, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                                else
                                {
                                    int nuevoSalario = (int)Math.Round(contrato.SalarioAnual * 0.2 / 100000.0) * 100000;
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, nuevoSalario, contrato.ClausulaRescision, 4, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                            }
                            else
                            {
                                _logicaJugador.BorrarContrato(contrato.IdJugador);
                                _logicaJugador.CambiarDeEquipo(contrato.IdJugador, 0);
                            }
                        }
                        else if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Status == 3)
                        {
                            if (contrato.IdEquipo != _equipo)
                            {
                                // Ampliar el contrato dependiendo de la edad
                                if (_logicaJugador.MostrarDatosJugador(contrato.IdJugador).Edad > 30)
                                {
                                    int salarioBase = contrato.SalarioAnual;
                                    int descuento = (int)Math.Round(salarioBase * 0.2 / 100000.0) * 100000;
                                    int nuevoSalario = salarioBase - descuento;
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, nuevoSalario, contrato.ClausulaRescision, 1, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                                else
                                {
                                    int nuevoSalario = (int)Math.Round(contrato.SalarioAnual * 0.05 / 100000.0) * 100000;
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, nuevoSalario, contrato.ClausulaRescision, 3, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                            }
                            else
                            {
                                _logicaJugador.BorrarContrato(contrato.IdJugador);
                                _logicaJugador.CambiarDeEquipo(contrato.IdJugador, 0);
                            }
                        }
                        else
                        {
                            if (contrato.IdEquipo != _equipo)
                            {
                                List<Jugador> jugadoresEquipo = _logicaJugador.ListadoJugadoresCompleto(contrato.IdEquipo);
                                int totalJugadores = jugadoresEquipo.Count();
                                if (totalJugadores >= 20)
                                {
                                    _logicaJugador.BorrarContrato(contrato.IdJugador);
                                    _logicaJugador.CambiarDeEquipo(contrato.IdJugador, 0);
                                }
                                else
                                {
                                    int salarioBase = contrato.SalarioAnual;
                                    int descuento = (int)Math.Round(salarioBase * 0.25 / 100000.0) * 100000;
                                    int nuevoSalario = salarioBase - descuento;
                                    _logicaJugador.RenovarContratoJugador(contrato.IdJugador, nuevoSalario, contrato.ClausulaRescision, 1, contrato.BonoPorPartidos, contrato.BonoPorGoles);
                                }
                            }
                            else
                            {
                                _logicaJugador.BorrarContrato(contrato.IdJugador);
                                _logicaJugador.CambiarDeEquipo(contrato.IdJugador, 0);
                            }
                        }
                    }
                }
            });
            progressBar.Visibility = Visibility.Collapsed;
            this.Close();
        }

        #region "Metodos"
        private void ConfigurarDataGrid()
        {
            dgMejorOnce.AlternationCount = 2;  // Asegurarse de que haya alternancia entre filas
            dgMejorOnce.AutoGenerateColumns = false; // Deshabilitar generación automática de columnas
            dgMejorOnce.Columns.Clear(); // Limpiar cualquier columna previa

            dgMejorOnce.ItemsSource = _listaMejorOnce;

            // Crear la columna de tipo DataGridTemplateColumn para la FOTO
            dgMejorOnce.Columns.Add(new DataGridTemplateColumn
            {
                Header = "",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                CellTemplate = CrearPlantillaFoto()  // Pasamos los equipos
            });

            // Crear columna para el nombre
            dgMejorOnce.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreCompleto"),
                Header = "NOMBRE",
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
                HeaderStyle = new Style(typeof(DataGridColumnHeader))
                {
                    Setters =
                    {
                        new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Left), // Alineación a la izquierda
                        new Setter(BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))), // Fondo
                        new Setter(ForegroundProperty, Brushes.Black), // Color de texto
                        new Setter(FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")) // Fuente
                    }
                },
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Columna Equipo
            dgMejorOnce.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("NombreEquipo"),
                Header = "EQUIPO",
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Columna Demarcacion
            dgMejorOnce.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("Rol"),
                Header = "ROL",
                Width = new DataGridLength(300, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Columna Valoracion
            dgMejorOnce.Columns.Add(new DataGridTextColumn
            {
                Binding = new System.Windows.Data.Binding("ValoracionTexto"),
                Header = "PUNTOS",
                Width = new DataGridLength(100, DataGridLengthUnitType.Pixel),
                ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Left),
                        new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center)
                    }
                }
            });

            // Configurar altura de filas y estilos generales
            dgMejorOnce.RowHeight = 35;
            dgMejorOnce.RowBackground = new SolidColorBrush(Colors.LightGray);
            dgMejorOnce.AlternatingRowBackground = new SolidColorBrush(Colors.WhiteSmoke);
            dgMejorOnce.BorderBrush = new SolidColorBrush(Colors.Transparent);
            dgMejorOnce.BorderThickness = new Thickness(0);
            dgMejorOnce.CanUserAddRows = false;
            dgMejorOnce.CanUserResizeColumns = false;
            dgMejorOnce.CanUserResizeRows = false;
            dgMejorOnce.SelectionMode = DataGridSelectionMode.Single;
            dgMejorOnce.SelectionUnit = DataGridSelectionUnit.FullRow;
            dgMejorOnce.HeadersVisibility = DataGridHeadersVisibility.None; // NO Mostrar cabeceras
            dgMejorOnce.HorizontalGridLinesBrush = new SolidColorBrush(Colors.Transparent);

            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            columnHeaderStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9b8b5a"))));
            columnHeaderStyle.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")));
            columnHeaderStyle.Setters.Add(new Setter(Control.FontSizeProperty, 16.0));
            columnHeaderStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(5)));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Brushes.Transparent));
            columnHeaderStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));

            dgMejorOnce.ColumnHeaderStyle = columnHeaderStyle;

            // Estilo de celdas
            dgMejorOnce.CellStyle = new Style(typeof(DataGridCell))
            {
                Setters =
                {
                    new Setter(Control.BackgroundProperty, new SolidColorBrush(Colors.Transparent)),
                    new Setter(Control.ForegroundProperty, new SolidColorBrush(Color.FromRgb(35, 40, 45))),
                    new Setter(Control.FontFamilyProperty, new FontFamily("Cascadia Code SemiBold")),
                    new Setter(Control.FontSizeProperty, 16.0),
                    new Setter(Control.PaddingProperty, new Thickness(5)),
                    new Setter(DataGridCell.BorderBrushProperty, Brushes.Transparent),
                    new Setter(DataGridCell.BorderThicknessProperty, new Thickness(0))
                }
            };
        }

        private DataTemplate CrearPlantillaFoto()
        {
            // Crear la fábrica de elementos
            FrameworkElementFactory imageFactory = new FrameworkElementFactory(typeof(Image));
            imageFactory.SetValue(Image.WidthProperty, 32.0);
            imageFactory.SetValue(Image.HeightProperty, 32.0);

            // Usamos el convertidor sin parámetros
            imageFactory.SetBinding(Image.SourceProperty, new System.Windows.Data.Binding("IdJugador")
            {
                Converter = new IdjugadorToFotoConverter()
            });

            // Crear y devolver la plantilla
            DataTemplate template = new DataTemplate();
            template.VisualTree = imageFactory;
            return template;
        }

        private void dg_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            // Colorear las primeras 5 filas de amarillo
            if (e.Row.GetIndex() == 0)
            {
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#bcd5ba"));
            }
            else if (e.Row.GetIndex() >= 1 && e.Row.GetIndex() <= 3)
            {
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f5d4d4"));
            }
            else if (e.Row.GetIndex() >= 4 && e.Row.GetIndex() <= 6)
            {
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f2eec5"));
            }
            else if (e.Row.GetIndex() >= 7 && e.Row.GetIndex() <= 9)
            {
                e.Row.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c5e4f2"));
            }
        }
        #endregion
    }
}
