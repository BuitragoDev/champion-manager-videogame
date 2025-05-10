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
    public partial class UC_Menu_Finanzas_Gastos : UserControl
    {
        private Manager _manager;
        private int _equipo;
        int anio = Metodos.temporadaActual;

        EquipoLogica _logicaEquipo = new EquipoLogica();
        JugadorLogica _logicaJugador = new JugadorLogica();
        FinanzaLogica _logicaFinanzas = new FinanzaLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();

        public UC_Menu_Finanzas_Gastos(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtAnio.Text = "Temporada " + anio + "/" + (anio + 1);

            if (anio == 2024)
            {
                btnAnioAnterior.Visibility = Visibility.Hidden;
            }
            if (anio == Metodos.temporadaActual)
            {
                btnAnioSiguiente.Visibility = Visibility.Hidden;
            }
            // Carga de Valores ------------------------------------------------------------------------------
            Equipo miEquipo = _logicaEquipo.ListarDetallesEquipo(_equipo);
            txtDineroTotalFranquicia.Text = miEquipo.Presupuesto.ToString("N0") + " €";

            txtGastosSalariosEmpleados.Text = _logicaEmpleado.SalarioTotalEmpleados(_equipo, _manager.IdManager).ToString("N0") + " €";

            List<Jugador> miPlantilla = _logicaJugador.ListadoJugadoresCompleto(_equipo);
            int gastoSueldosJugadores = 0;
            foreach (Jugador jugador in miPlantilla)
            {
                gastoSueldosJugadores += (int)(jugador.SalarioTemporada ?? 0);
            }
            txtGastosSalariosJugadores.Text = gastoSueldosJugadores.ToString("N0") + " €";
            // -----------------------------------------------------------------------------------------------
            CargarDatos(_manager.IdManager, Metodos.temporadaActual);
            AgregarBordes();
            // -----------------------------------------------------------------------------------------------
            CargarCantidades(_manager.IdManager, Metodos.temporadaActual);
        }

        // ---------------------------------------------------------------------------------- Evento CLICK del boton AÑO ANTERIOR
        private void btnAnioAnterior_Click(object sender, RoutedEventArgs e)
        {
            anio--;
            if (anio == 2024)
            {
                btnAnioAnterior.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAnioAnterior.Visibility = Visibility.Visible;
            }

            if (anio < Metodos.temporadaActual)
            {
                btnAnioSiguiente.Visibility = Visibility.Visible;
            }
            else
            {
                btnAnioSiguiente.Visibility = Visibility.Hidden;
            }
            txtAnio.Text = "Temporada " + anio + "/" + (anio + 1);
            CargarDatos(_manager.IdManager, anio);
            CargarCantidades(_manager.IdManager, anio);
        }

        // ---------------------------------------------------------------------------------- Evento CLICK del boton AÑO SIGUIENTE
        private void btnAnioSiguiente_Click(object sender, RoutedEventArgs e)
        {
            btnAnioAnterior.Visibility = Visibility.Visible;
            anio++;
            txtAnio.Text = "Temporada " + anio + "/" + (anio + 1);

            if (anio == Metodos.temporadaActual + 1)
            {
                btnAnioSiguiente.Visibility = Visibility.Hidden;
            }

            if (anio == Metodos.temporadaActual)
            {
                btnAnioSiguiente.Visibility = Visibility.Hidden;
            }

            CargarDatos(_manager.IdManager, anio);
            CargarCantidades(_manager.IdManager, anio);
        }

        #region "Métodos"
        private void CargarDatos(int manager, int temporada)
        {
            gridGrafico.Children.Clear();
            List<Finanza> finanzas = _logicaFinanzas.MostrarFinanzasEquipo(manager, temporada);

            double[] ingresos = new double[12];
            double[] gastos = new double[12];

            // Iterar sobre la lista de finanzas
            foreach (var finanza in finanzas)
            {
                // Obtener el mes de la fecha (1 = enero, ..., 12 = diciembre)
                int mesCalendario = finanza.Fecha.Month;

                // Mapear el mes al índice del array (julio = 0, agosto = 1, ..., junio = 11)
                int mesIndex = (mesCalendario - 7 + 12) % 12;

                if (finanza.Tipo == 1) // Ingreso
                {
                    ingresos[mesIndex] += finanza.Cantidad;
                }
                else if (finanza.Tipo == 2) // Gasto
                {
                    gastos[mesIndex] += finanza.Cantidad;
                }
            }

            // Altura máxima para la escala
            double maxAltura = 100; // Altura máxima en píxeles (corresponde a 10,000 unidades)

            for (int i = 0; i < 12; i++)
            {
                // Calcular balance mensual
                double balance = ingresos[i] - gastos[i];
                double altura = Math.Abs(balance / 30000000) * maxAltura; // Altura proporcional

                // Balance Mensual
                var balanceMensual = new TextBlock
                {
                    Text = balance.ToString("N0") + " €",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code Light"),
                    FontSize = 10
                };
                Grid.SetRow(balanceMensual, 0);
                Grid.SetColumn(balanceMensual, i);
                gridGrafico.Children.Add(balanceMensual);

                if (balance > 0)
                {
                    // Balance positivo: Rectángulo verde en la fila 0 (alineado abajo)
                    var rectIngreso = new Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Height = altura
                    };
                    Grid.SetRow(rectIngreso, 1);
                    Grid.SetColumn(rectIngreso, i);
                    gridGrafico.Children.Add(rectIngreso);
                }
                else if (balance < 0)
                {
                    // Balance negativo: Rectángulo rojo en la fila 1 (alineado arriba)
                    var rectGasto = new Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.Red),
                        VerticalAlignment = VerticalAlignment.Top,
                        Height = altura
                    };
                    Grid.SetRow(rectGasto, 2);
                    Grid.SetColumn(rectGasto, i);
                    gridGrafico.Children.Add(rectGasto);
                }

                // Texto del mes
                var textoMes = new TextBlock
                {
                    Text = ObtenerNombreMes(i),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontFamily = new FontFamily("Cascadia Code Light"),
                    FontSize = 12
                };
                Grid.SetRow(textoMes, 3);
                Grid.SetColumn(textoMes, i);
                gridGrafico.Children.Add(textoMes);
            }
        }

        private string ObtenerNombreMes(int indice)
        {
            string[] meses = { "JUL " + anio.ToString(),
                       "AGO " + anio.ToString(),
                       "SEP " + anio.ToString(),
                       "OCT " + anio.ToString(),
                       "NOV " + anio.ToString(),
                       "DIC " + anio.ToString(),
                       "ENE " + (anio + 1).ToString(),
                       "FEB " + (anio + 1).ToString(),
                       "MAR " + (anio + 1).ToString(),
                       "ABR " + (anio + 1).ToString(),
                       "MAY " + (anio + 1).ToString(),
                       "JUN " + (anio + 1).ToString() };
            return meses[indice];
        }

        private void CargarCantidades(int manager, int temporada)
        {
            List<Finanza> listaFinanzas = _logicaFinanzas.MostrarFinanzasEquipo(manager, temporada);
            int salarioJugador = 0;
            int cancelacionJugador = 0;
            int salarioEmpleado = 0;
            int cancelacionEmpleado = 0;
            int obras = 0;
            int bonus = 0;
            int prestamosGasto = 0;
            int total = 0;
            int traspaso = 0;

            foreach (Finanza finanza in listaFinanzas)
            {
                if (finanza.IdConcepto == 11)
                {
                    salarioJugador += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 12)
                {
                    cancelacionJugador += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 13)
                {
                    salarioEmpleado += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 14)
                {
                    cancelacionEmpleado += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 15)
                {
                    obras += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 16)
                {
                    bonus += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 17)
                {
                    prestamosGasto += (int)finanza.Cantidad;
                }
                if (finanza.IdConcepto == 18)
                {
                    traspaso += (int)finanza.Cantidad;
                }
                if (finanza.Tipo == 2) // Tipo 2 = Gastos
                {
                    total += (int)finanza.Cantidad;
                }
            }
            txtSalarioJugadores.Text = salarioJugador.ToString("N0") + " €";
            txtCancelacionSalarioJugador.Text = cancelacionJugador.ToString("N0") + " €";
            txtSalarioEmpleado.Text = salarioEmpleado.ToString("N0") + " €";
            txtCancelacionContratoEmpleado.Text = cancelacionEmpleado.ToString("N0") + " €";
            txtGradas.Text = obras.ToString("N0") + " €";
            txtBonusJugadores.Text = bonus.ToString("N0") + " €";
            txtPrestamos.Text = prestamosGasto.ToString("N0") + " €";
            txtTotal.Text = total.ToString("N0") + " €";
            txtFichajes.Text = traspaso.ToString("N0") + " €";
        }


        private void AgregarBordes()
        {
            for (int row = 0; row < gridGrafico.RowDefinitions.Count; row++)
            {
                for (int col = 0; col < gridGrafico.ColumnDefinitions.Count; col++)
                {
                    // Crear un Border
                    var border = new Border
                    {
                        BorderThickness = new Thickness(
                            left: col == 0 ? 1 : 0,   // Borde izquierdo solo para la primera columna
                            top: row == 0 ? 1 : 0,    // Borde superior solo para la primera fila
                            right: 1,                 // Siempre dibujar el borde derecho
                            bottom: 1                 // Siempre dibujar el borde inferior
                        ),
                        BorderBrush = new SolidColorBrush(Colors.LightGray)
                    };

                    // Ajustar su posición en el Grid
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);

                    // Añadir el borde al Grid
                    gridGrafico.Children.Add(border);
                }
            }
        }
        #endregion
    }
}
