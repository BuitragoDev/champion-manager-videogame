using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using NAudio.Wave;
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

namespace ChampionManager25.UserControls
{
    public partial class UC_Menu_Finanzas_Prestamos : UserControl
    {
        #region "Variables"
        private Manager _manager;
        private int _equipo;

        int reputacionFinanciero = 0;
        int pagoSemanal;
        int tasa = 0;
        #endregion

        // Instancias de la LOGICA
        PrestamoLogica _logicaPrestamo = new PrestamoLogica();
        EmpleadoLogica _logicaEmpleado = new EmpleadoLogica();
        FinanzaLogica _logicaFinanza = new FinanzaLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();
        MensajeLogica _logicaMensajes = new MensajeLogica();

        public UC_Menu_Finanzas_Prestamos(Manager manager, int equipo)
        {
            InitializeComponent();
            _manager = manager;
            _equipo = equipo;
            Metodos metodos = new Metodos();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CargarDatos();
        }

        // -------------------------------------------------------------------- Evento CLICK del botón DISMINUIR CAPITAL
        private void btnCapitalRestar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Usa CultureInfo para manejar números con formato regional
            var culture = CultureInfo.CurrentCulture;

            // Verifica y convierte el texto del TextBox a decimal
            if (decimal.TryParse(txtCapitalPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal numero))
            {
                int capital = (int)Math.Round(numero); // Convierte a entero

                // Limita el rango del número
                if (capital <= 1_000_000 || capital > 20_000_000)
                {
                    return;
                }

                // Resta 1,000,000 al número
                capital -= 1_000_000;

                // Asegura que el resultado esté dentro del rango permitido
                if (capital < 1_000_000)
                {
                    return;
                }

                // Actualiza el TextBox con el número formateado
                txtCapitalPrestamo.Text = capital.ToString("N0", culture);

                // Convierte el texto del segundo TextBox (semanas) a entero
                if (int.TryParse(txtSemanasPrestamo.Text, out int semanas))
                {
                    // Llama a calcularPagoSemanal con valores enteros
                    txtPagoSemana.Text = calcularPagoSemanal(capital, semanas).ToString("N0");
                }
            }
        }


        // -------------------------------------------------------------------- Evento CLICK del botón AUMENTAR CAPITAL
        private void btnCapitalSumar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Usa CultureInfo para manejar números con formato regional
            var culture = CultureInfo.CurrentCulture;

            // Verifica y convierte el texto del TextBox a decimal
            if (decimal.TryParse(txtCapitalPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal numero))
            {
                int capital = (int)Math.Round(numero); // Convierte a entero

                // Limita el rango del número
                if (capital < 1_000_000 || capital >= 20_000_000)
                {
                    return;
                }

                // Resta 1,000,000 al número
                capital += 1_000_000;

                // Asegura que el resultado esté dentro del rango permitido
                if (capital < 1_000_000)
                {
                    return;
                }

                // Actualiza el TextBox con el número formateado
                txtCapitalPrestamo.Text = capital.ToString("N0", culture);

                // Convierte el texto del segundo TextBox (semanas) a entero
                if (int.TryParse(txtSemanasPrestamo.Text, out int semanas))
                {
                    // Llama a calcularPagoSemanal con valores enteros
                    txtPagoSemana.Text = calcularPagoSemanal(capital, semanas).ToString("N0");
                }
            }
        }

        // -------------------------------------------------------------------- Evento CLICK del botón DISMINUIR SEMANAS
        private void btnSemanasRestar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            // Usa CultureInfo para manejar números con formato regional
            var culture = CultureInfo.CurrentCulture;

            // Verifica y convierte el texto del TextBox de semanas
            if (decimal.TryParse(txtSemanasPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal semanasNumero))
            {
                // Limita el rango del número
                if (semanasNumero <= 5 || semanasNumero > 100)
                {
                    return;
                }

                // Resta 5 al número
                semanasNumero -= 5;

                // Actualiza el TextBox con el número formateado
                txtSemanasPrestamo.Text = semanasNumero.ToString("N0", culture);

                // Verifica y convierte el texto del TextBox de capital
                if (decimal.TryParse(txtCapitalPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal capitalNumero))
                {
                    // Llama al método calcularPagoSemanal con ambos valores como enteros
                    int capital = (int)Math.Round(capitalNumero);
                    int semanas = (int)Math.Round(semanasNumero);

                    txtPagoSemana.Text = calcularPagoSemanal(capital, semanas).ToString("N0");
                }
            }
        }


        // -------------------------------------------------------------------- Evento CLICK del botón AUMENTAR SEMANAS
        private void btnSemanasSumar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            // Usa CultureInfo para manejar números con formato regional
            var culture = CultureInfo.CurrentCulture;

            // Verifica y convierte el texto del TextBox de semanas
            if (decimal.TryParse(txtSemanasPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal semanasNumero))
            {
                // Limita el rango del número
                if (semanasNumero < 5 || semanasNumero >= 100)
                {
                    return;
                }

                // Suma 5 al número
                semanasNumero += 5;

                // Actualiza el TextBox con el número formateado
                txtSemanasPrestamo.Text = semanasNumero.ToString("N0", culture);

                // Verifica y convierte el texto del TextBox de capital
                if (decimal.TryParse(txtCapitalPrestamo.Text, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, culture, out decimal capitalNumero))
                {
                    // Llama al método calcularPagoSemanal con ambos valores como enteros
                    int capital = (int)Math.Round(capitalNumero);
                    int semanas = (int)Math.Round(semanasNumero);

                    txtPagoSemana.Text = calcularPagoSemanal(capital, semanas).ToString("N0");
                }
            }
        }

        // -------------------------------------------------------------------- Evento CLICK del botón SOLICITAR PRÉSTAMO
        private void btnSolicitarPrestamo_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            int capital = int.Parse(txtCapitalPrestamo.Text.Trim().Replace(".", ""));
            int semanas = int.Parse(txtSemanasPrestamo.Text.Trim().Replace(".", ""));

            // Comprobar primer hueco vacío.
            int huecoVacio = 0;  // Valor predeterminado por si no se encuentra ningún hueco

            List<int> ordenVacio = _logicaPrestamo.OrdenPrestamos(_manager.IdManager, _equipo);

            // Si no hay registros previos (lista vacía), asignar el orden 1
            if (ordenVacio.Count == 0)
            {
                huecoVacio = 1;  // El primer préstamo tendrá el orden 1
            }
            else
            {
                // Comprobamos los posibles valores de orden (1, 2 y 3)
                for (int i = 1; i <= 3; i++)
                {
                    // Si el valor no se encuentra en la lista de ordenes existentes
                    if (!ordenVacio.Contains(i))
                    {

                        huecoVacio = i;  // Asignamos el primer hueco vacío encontrado
                        break;  // Salimos del bucle porque ya encontramos el primer hueco
                    }
                }
            }

            // Càlculo del pago total con intereses
            double intereses = (capital * tasa) / 100;
            int pagoTotal = (int)Math.Round(capital + intereses);

            Prestamo nuevoPrestamo = new Prestamo
            {
                Orden = huecoVacio,
                Fecha = Metodos.hoy.ToString("dd/MM/yyyy"),
                Capital = pagoTotal,
                CapitalRestante = pagoTotal,
                Semanas = semanas,
                SemanasRestantes = semanas,
                TasaInteres = tasa,
                PagoSemanal = calcularPagoSemanal(capital, semanas),
                IdManager = _manager.IdManager,
                IdEquipo = _equipo,
            };

            _logicaPrestamo.AnadirPrestamo(nuevoPrestamo);

            // Creamos el ingreso
            Finanza nuevoIngreso = new Finanza
            {
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Temporada = Metodos.temporadaActual.ToString(),
                IdConcepto = 10,
                Tipo = 1,
                Cantidad = capital,
                Fecha = Metodos.hoy.Date
            };
            _logicaFinanza.CrearIngreso(nuevoIngreso);

            // Agregamos la cantidad al presupuesto
            _logicaEquipo.SumarCantidadAPresupuesto(_equipo, capital);

            // Creamos el mensaje
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

            Mensaje mensajePrestamo = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Préstamo Bancario",
                Contenido = $"Me complace informarte de que hemos conseguido la aprobación del préstamo solicitado por el club. El banco ha autorizado una inyección económica de {capital.ToString("N0", new CultureInfo("es-ES"))}€, que ya ha sido ingresada en nuestras cuentas.\nEl préstamo deberá ser pagado en {semanas} semanas y tiene un interés del {tasa} %.\n\nEsta operación nos permitirá afrontar con mayor solidez los retos financieros de la temporada. Confío en que sabrás administrar estos fondos con responsabilidad para reforzar el equipo y cumplir los objetivos marcados.",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajePrestamo);

            CargarDatos();
        }

        // -------------------------------------------------------------------- Evento CLICK del botón CANCELAR PRÉSTAMO 1
        private void btnCancelarPrestamo1_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnCancelarPrestamo1.Visibility = Visibility.Hidden;

            txtPrestamo1Fecha.Text = "-";
            txtPrestamo1Capital.Text = "-";
            txtPrestamo1CapitalRestante.Text = "-";
            txtPrestamo1Semanas.Text = "-";
            txtPrestamo1SemanasRestantes.Text = "-";
            txtPrestamo1TasaInteres.Text = "-";
            txtPrestamo1PagoSemanal.Text = "-";

            // Eliminamos el prestamo de la pantalla y hacemos el pago del importe que falta por abonar
            List<Prestamo> prestamosActivos = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);
            foreach (var prestamo in prestamosActivos)
            {
                if (prestamo.Orden == 1)
                {
                    int liquidacion = prestamo.CapitalRestante;
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 17,
                        Tipo = 2,
                        Cantidad = liquidacion,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, liquidacion);

                    // Crear mensaje
                    MensajeCancelacionPrestamo(liquidacion);
                }
            }
            _logicaPrestamo.EliminarPrestamo(_manager.IdManager, _equipo, 1);

            CargarDatos();
        }

        // -------------------------------------------------------------------- Evento CLICK del botón CANCELAR PRÉSTAMO 2
        private void btnCancelarPrestamo2_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnCancelarPrestamo2.Visibility = Visibility.Hidden;

            txtPrestamo2Fecha.Text = "-";
            txtPrestamo2Capital.Text = "-";
            txtPrestamo2CapitalRestante.Text = "-";
            txtPrestamo2Semanas.Text = "-";
            txtPrestamo2SemanasRestantes.Text = "-";
            txtPrestamo2TasaInteres.Text = "-";
            txtPrestamo2PagoSemanal.Text = "-";

            // Eliminamos el prestamo de la pantalla y hacemos el pago del importe que falta por abonar
            List<Prestamo> prestamosActivos = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);
            foreach (var prestamo in prestamosActivos)
            {
                if (prestamo.Orden == 2)
                {
                    int liquidacion = prestamo.CapitalRestante;
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 17,
                        Tipo = 2,
                        Cantidad = liquidacion,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, liquidacion);

                    // Crear mensaje
                    MensajeCancelacionPrestamo(liquidacion);
                }
            }
            _logicaPrestamo.EliminarPrestamo(_manager.IdManager, _equipo, 2);

            CargarDatos();
        }

        // -------------------------------------------------------------------- Evento CLICK del botón CANCELAR PRÉSTAMO 3
        private void btnCancelarPrestamo3_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();
            btnCancelarPrestamo3.Visibility = Visibility.Hidden;

            txtPrestamo3Fecha.Text = "-";
            txtPrestamo3Capital.Text = "-";
            txtPrestamo3CapitalRestante.Text = "-";
            txtPrestamo3Semanas.Text = "-";
            txtPrestamo3SemanasRestantes.Text = "-";
            txtPrestamo3TasaInteres.Text = "-";
            txtPrestamo3PagoSemanal.Text = "-";

            // Eliminamos el prestamo de la pantalla y hacemos el pago del importe que falta por abonar
            List<Prestamo> prestamosActivos = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);
            foreach (var prestamo in prestamosActivos)
            {
                if (prestamo.Orden == 3)
                {
                    int liquidacion = prestamo.CapitalRestante;
                    Finanza nuevoGasto = new Finanza
                    {
                        IdEquipo = _equipo,
                        IdManager = _manager.IdManager,
                        Temporada = Metodos.temporadaActual.ToString(),
                        IdConcepto = 17,
                        Tipo = 2,
                        Cantidad = liquidacion,
                        Fecha = Metodos.hoy.Date
                    };
                    _logicaFinanza.CrearGasto(nuevoGasto);

                    // Restar la indemnización al Presupuesto
                    _logicaEquipo.RestarCantidadAPresupuesto(_equipo, liquidacion);

                    // Crear mensaje
                    MensajeCancelacionPrestamo(liquidacion);
                }
            }
            _logicaPrestamo.EliminarPrestamo(_manager.IdManager, _equipo, 3);

            CargarDatos();
        }

        #region "Métodos"
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

        // Método que calcula el pago semanal dependiendo del capital y las semanas.
        private int calcularPagoSemanal(int capital, int semanas)
        {
            List<Empleado> empleados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);
            foreach (Empleado empleado in empleados)
            {
                if (empleado.Puesto.Equals("Financiero"))
                {
                    reputacionFinanciero = empleado.Categoria;
                }
            }

            int[] interes = {
                9,
                7,
                5,
                3,
                1
            };

            if (reputacionFinanciero >= 1 && reputacionFinanciero <= 5)
            {
                tasa = interes[reputacionFinanciero - 1];
            }

            // Càlculo del pago semanal con intereses
            double intereses = (capital * tasa) / 100;
            double pagoTotal = capital + intereses;
            pagoSemanal = (int)Math.Round((double)pagoTotal / semanas);

            return pagoSemanal;
        }

        private void CargarDatos()
        {
            // Cargar datos de pantalla
            btnSolicitarPrestamo.Visibility = Visibility.Hidden;
            txtNombreFinanciero.Text = "Necesitas contratar un Financiero.";

            List<Empleado> empleados = _logicaEmpleado.MostrarListaEmpleadosContratados(_equipo, _manager.IdManager);

            foreach (Empleado empleado in empleados)
            {
                if (empleado.Puesto.Equals("Financiero"))
                {
                    txtNombreFinanciero.Text = empleado.Nombre;
                    reputacionFinanciero = empleado.Categoria;
                    MostrarEstrellas(reputacionFinanciero);
                    btnSolicitarPrestamo.Visibility = Visibility.Visible;

                    int[] valorTasa = {
                        9, 7, 5, 3, 1
                    };

                    if (empleado.Categoria >= 1 && empleado.Categoria <= 5)
                    {
                        tasa = valorTasa[empleado.Categoria - 1];
                    }
                }
            }

            string[] intereses = {
                "Nuestro gestor financiero puede pedir préstamos a un 9% de interés.",
                "Nuestro gestor financiero puede pedir préstamos a un 7% de interés.",
                "Nuestro gestor financiero puede pedir préstamos a un 5% de interés.",
                "Nuestro gestor financiero puede pedir préstamos a un 3% de interés.",
                "Nuestro gestor financiero puede pedir préstamos a un 1% de interés."
            };

            if (reputacionFinanciero >= 1 && reputacionFinanciero <= 5)
            {
                txtInteresFinanciero.Text = intereses[reputacionFinanciero - 1];
            }

            string[] tasaInteres = {
                "9 %",
                "7 %",
                "5 %",
                "3 %",
                "1 %"
            };

            if (reputacionFinanciero >= 1 && reputacionFinanciero <= 5)
            {
                txtTasaInteres.Text = tasaInteres[reputacionFinanciero - 1];
            }

            int capital = int.Parse(txtCapitalPrestamo.Text.Trim().Replace(".", ""));
            int semanas = int.Parse(txtSemanasPrestamo.Text.Trim().Replace(".", ""));
            txtPagoSemana.Text = calcularPagoSemanal(capital, semanas).ToString("N0");

            // Comprobar si hay prestamos pedidos.
            List<Prestamo> prestamos = _logicaPrestamo.MostrarPrestamos(_manager.IdManager, _equipo);

            if (prestamos != null)
            {
                if (prestamos.Count == 3)
                {
                    btnSolicitarPrestamo.Visibility = Visibility.Hidden;
                }

                int contador = 1;
                foreach (Prestamo prestamo in prestamos)
                {
                    if (prestamo.Orden == 1)
                    {
                        txtPrestamo1Fecha.Text = prestamo.Fecha;
                        txtPrestamo1Capital.Text = prestamo.Capital.ToString("N0") + " €";
                        txtPrestamo1CapitalRestante.Text = prestamo.CapitalRestante.ToString("N0") + " €";
                        txtPrestamo1Semanas.Text = prestamo.Semanas.ToString();
                        txtPrestamo1SemanasRestantes.Text = prestamo.SemanasRestantes.ToString();
                        txtPrestamo1TasaInteres.Text = prestamo.TasaInteres.ToString() + " %";
                        txtPrestamo1PagoSemanal.Text = prestamo.PagoSemanal.ToString("N0") + " €";
                        btnCancelarPrestamo1.Visibility = Visibility.Visible;
                    }

                    if (prestamo.Orden == 2)
                    {
                        txtPrestamo2Fecha.Text = prestamo.Fecha;
                        txtPrestamo2Capital.Text = prestamo.Capital.ToString("N0") + " €";
                        txtPrestamo2CapitalRestante.Text = prestamo.CapitalRestante.ToString("N0") + " €";
                        txtPrestamo2Semanas.Text = prestamo.Semanas.ToString();
                        txtPrestamo2SemanasRestantes.Text = prestamo.SemanasRestantes.ToString();
                        txtPrestamo2TasaInteres.Text = prestamo.TasaInteres.ToString() + " %";
                        txtPrestamo2PagoSemanal.Text = prestamo.PagoSemanal.ToString("N0") + " €";
                        btnCancelarPrestamo2.Visibility = Visibility.Visible;
                    }

                    if (prestamo.Orden == 3)
                    {
                        txtPrestamo3Fecha.Text = prestamo.Fecha;
                        txtPrestamo3Capital.Text = prestamo.Capital.ToString("N0") + " €";
                        txtPrestamo3CapitalRestante.Text = prestamo.CapitalRestante.ToString("N0") + " €";
                        txtPrestamo3Semanas.Text = prestamo.Semanas.ToString();
                        txtPrestamo3SemanasRestantes.Text = prestamo.SemanasRestantes.ToString();
                        txtPrestamo3TasaInteres.Text = prestamo.TasaInteres.ToString() + " %";
                        txtPrestamo3PagoSemanal.Text = prestamo.PagoSemanal.ToString("N0") + " €";
                        btnCancelarPrestamo3.Visibility = Visibility.Visible;
                    }

                    contador++;
                }
            }
        }

        private void MensajeCancelacionPrestamo(int cantidad){
            // Creamos el mensaje
            Empleado? financiero = _logicaEmpleado.ObtenerEmpleadoPorPuesto("Financiero");
            string presidente = _logicaEquipo.ListarDetallesEquipo(_equipo).Presidente;

            Mensaje mensajePrestamo = new Mensaje
            {
                Fecha = Metodos.hoy,
                Remitente = financiero != null ? financiero.Nombre : presidente,
                Asunto = "Préstamo Bancario",
                Contenido = $"Me alegra comunicarte que hemos saldado por completo el préstamo pendiente de {cantidad.ToString("N0", new CultureInfo("es-ES"))}€ con la entidad bancaria. Gracias a tu gestión económica, hemos podido cancelar anticipadamente la deuda, liberando al club de cargas financieras y fortaleciendo nuestra estabilidad a medio y largo plazo.\n\nEste es un paso importante hacia un futuro más sostenible. Buen trabajo. Continúa así.",
                TipoMensaje = "Notificación",
                IdEquipo = _equipo,
                IdManager = _manager.IdManager,
                Leido = false,
                Icono = 0 // 0 es icono de equipo
            };

            _logicaMensajes.crearMensaje(mensajePrestamo);
        }
        #endregion
    }
}
