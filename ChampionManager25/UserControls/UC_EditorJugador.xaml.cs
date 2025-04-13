using ChampionManager25.Logica;
using ChampionManager25.MisMetodos;
using ChampionManager25.Entidades;
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
using ChampionManager25.Datos;
using ChampionManager25.Vistas;
using System.Drawing;
using System.IO;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;

namespace ChampionManager25.UserControls
{
    public partial class UC_EditorJugador : UserControl
    {
        #region "Variables"
        private int _jugador;
        private int _equipo;
        Jugador oJugador;

        private string imagenJugadorTemporal = null;
        private string rutaFotoJugador = Path.Combine(GestorPartidas.RutaRecursosUsuario, "jugadores");
        #endregion

        // Instancias de la LOGICA
        JugadorLogica _logicaJugador = new JugadorLogica();
        EquipoLogica _logicaEquipo = new EquipoLogica();

        public UC_EditorJugador(int jugador, int eq)
        {
            InitializeComponent();
            _jugador = jugador;
            _equipo = eq;
            oJugador = _logicaJugador.MostrarDatosJugador(_jugador);
        }

        private void editorJugador_Loaded(object sender, RoutedEventArgs e)
        {
            // Cargar Detalles del Jugador
            CargarDetallesJugador(oJugador);

            // Cargar Nacionalidades
            CargarNacionalidades();
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
            mainWindow.CargarEditorJugadores(_equipo);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton REESTABLECER
        private void btnReestablecer_Click(object sender, RoutedEventArgs e)
        {
            CargarDetallesJugador(oJugador);
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton GUARDAR CAMBIOS
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string nombreFotoJugador = "";

            try
            {
                Jugador ojugadorEditar = new Jugador
                {
                    IdJugador = _jugador,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtNombre.Text.Trim(),
                    IdEquipo = (int)cbEquipo.SelectedValue,
                    Dorsal = (int)cbDorsal.SelectedItem,
                    Rol = (cbDemarcacion.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    RolId = cbDemarcacion.SelectedIndex + 1,
                    Velocidad = int.Parse(txtVelocidad.Text.Trim()),
                    Resistencia = int.Parse(txtResistencia.Text.Trim()),
                    Agresividad = int.Parse(txtAgresividad.Text.Trim()),
                    Calidad = int.Parse(txtCalidad.Text.Trim()),
                    EstadoForma = int.Parse(txtEstadoForma.Text.Trim()),
                    Moral = int.Parse(txtMoral.Text.Trim()),
                    Potencial = int.Parse(txtPotencial.Text.Trim()),
                    Portero = int.Parse(txtPortero.Text.Trim()),
                    Pase = int.Parse(txtPase.Text.Trim()),
                    Regate = int.Parse(txtRegate.Text.Trim()),
                    Remate = int.Parse(txtRemate.Text.Trim()),
                    Entradas = int.Parse(txtEntradas.Text.Trim()),
                    Tiro = int.Parse(txtTiro.Text.Trim()),
                    Nacionalidad = cbNacionalidad.SelectedItem?.ToString() ?? "Desconocida",
                    FechaNacimiento = new DateTime(int.Parse(txtAnio.Text.Trim()),
                                                   int.Parse(txtMes.Text.Trim()),
                                                   int.Parse(txtDia.Text.Trim())),
                    Peso = int.Parse(txtPeso.Text.Trim()),
                    Altura = int.Parse(txtAltura.Text.Trim()),
                    Status = cbStatus.SelectedIndex + 1
                };

                if (!string.IsNullOrEmpty(imagenJugadorTemporal))
                {
                    string destinoFoto = Path.Combine(rutaFotoJugador, $"{_jugador}.png");
                    nombreFotoJugador = GuardarImagen(imagenJugadorTemporal, destinoFoto);
                }

                ojugadorEditar.RutaImagen = $"Recursos/img/jugadores/{nombreFotoJugador}";

                // Actualizar el jugador en la Base de Datos
                _logicaJugador.ActualizarJugador(ojugadorEditar);

                // -----------------------------------------------------------------------------------------------------------------------------------
                string titulo = "INFORMACIÓN";
                string mensaje = "El jugador ha sido actualizado correctamente";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
            }
            catch (Exception ex)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "Ha ocurrido un error al guardar los datos";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
            }
        }

        // -------------------------------------------------------------------------- Evento CLICK del boton FOTO JUGADOR
        private void imgFotoJugador_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoClick();

            string path = AbrirSelectorImagen();
            if (path != null && ValidarImagen(path, 100, 100))
            {
                imgFotoJugador.Source = CargarImagenSinBloqueo(path);
                imagenJugadorTemporal = path;  
            }
        }

        // ------------------------------------------------- EVENTOS DE LAS CAJAS DE TEXTO ------------------------------------------------
        // --------------- NOMBRE
        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- APELLIDO
        private void txtApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        // --------------- DIA
        private void txtDia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtDia_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtDia_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDia.Text) && int.TryParse(txtDia.Text, out int dia))
            {
                int mes = !string.IsNullOrWhiteSpace(txtMes.Text) && int.TryParse(txtMes.Text, out int parsedMes) ? parsedMes : 1;
                int anio = !string.IsNullOrWhiteSpace(txtAnio.Text) && int.TryParse(txtAnio.Text, out int parsedAnio) ? parsedAnio : DateTime.Now.Year;

                if (mes < 1 || mes > 12) mes = 1;
                if (anio < 1980) anio = 1980;
                if (anio > DateTime.Now.Year) anio = DateTime.Now.Year;

                int maxDias = DateTime.DaysInMonth(anio, mes);

                if (dia < 1) dia = 1;
                if (dia > maxDias) dia = maxDias;

                txtDia.Text = dia.ToString();
                txtDia.SelectionStart = txtDia.Text.Length;

                // Validar día
                ValidarDia();
            }
        }

        // --------------- MES
        private void txtMes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtMes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtMes_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMes.Text) && int.TryParse(txtMes.Text, out int mes))
            {
                if (mes < 1) mes = 1;
                if (mes > 12) mes = 12;

                txtMes.Text = mes.ToString();
                txtMes.SelectionStart = txtMes.Text.Length;

                // Validar día
                ValidarDia();
            }
        }

        // --------------- ANIO
        private void txtAnio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtAnio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtAnio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnio.Text.Length == 4 && !string.IsNullOrWhiteSpace(txtAnio.Text) && int.TryParse(txtAnio.Text, out int anio))
            {
                int anioLimite = DateTime.Now.Year - 15;
                if (anio < 1980) anio = 1980;
                if (anio > anioLimite) anio = anioLimite;

                txtAnio.Text = anio.ToString();
                txtAnio.SelectionStart = txtAnio.Text.Length;

                // Validar día
                ValidarDia();
            }
        }

        // --------------- ALTURA
        private void txtAltura_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtAltura_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- PESO
        private void txtPeso_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPeso_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- VELOCIDAD
        private void txtVelocidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtVelocidad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- RESISTENCIA
        private void txtResistencia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtResistencia_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- AGRESIVIDAD
        private void txtAgresividad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtAgresividad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- CALIDAD
        private void txtCalidad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtCalidad_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- ESTADO DE FORMA
        private void txtEstadoForma_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtEstadoForma_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- MORAL
        private void txtMoral_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtMoral_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- POTENCIAL
        private void txtPotencial_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPotencial_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- PORTERO
        private void txtPortero_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPortero_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- PASE
        private void txtPase_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtPase_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- REGATE
        private void txtRegate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtRegate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- REMATE
        private void txtRemate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtRemate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- ENTRADAS
        private void txtEntradas_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtEntradas_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        // --------------- TIRO
        private void txtTiro_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtTiro_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bloquear la tecla de espacio
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        #region "Metodos"
        private void CargarDetallesJugador(Jugador jugador)
        {
            // Detalles del Jugador
            imgFotoJugador.Source = new BitmapImage(new Uri(GestorPartidas.RutaMisDocumentos + "/" + jugador.RutaImagen));
            txtNombre.Text = jugador.Nombre;
            txtApellido.Text = jugador.Apellido;
            DateTime fechaNacimiento = jugador.FechaNacimiento;
            txtDia.Text = fechaNacimiento.Day.ToString();
            txtMes.Text = fechaNacimiento.Month.ToString();
            txtAnio.Text = fechaNacimiento.Year.ToString();
            txtAltura.Text = jugador.Altura.ToString();
            txtPeso.Text = jugador.Peso.ToString();

            // Cargar ComboBox Nacionalidad
            cbNacionalidad.SelectedValue = jugador.Nacionalidad;

            // Cargar ComboBox Equipo
            List<Equipo> listaEquipos = _logicaEquipo.ListarTodosLosEquipos();
            cbEquipo.ItemsSource = listaEquipos;
            cbEquipo.DisplayMemberPath = "Nombre";
            cbEquipo.SelectedValuePath = "IdEquipo";
            cbEquipo.SelectedValue = jugador.IdEquipo;

            // Cargar ComboBox Demarcacion
            cbDemarcacion.SelectedValue = jugador.Rol;

            // Cargar ComboBox Rol
            cbStatus.SelectedIndex = jugador.Status - 1;

            // Cargar ComboBox Dorsal
            for (int i = 1; i <= 50; i++)
            {
                cbDorsal.Items.Add(i);
            }
            cbDorsal.SelectedItem = jugador.Dorsal;

            // Cargar Atributos
            txtVelocidad.Text = jugador.Velocidad.ToString();
            txtResistencia.Text = jugador.Resistencia.ToString();
            txtAgresividad.Text = jugador.Agresividad.ToString();
            txtCalidad.Text = jugador.Calidad.ToString();
            txtEstadoForma.Text = jugador.EstadoForma.ToString();
            txtMoral.Text = jugador.Moral.ToString();
            txtPotencial.Text = jugador.Potencial.ToString();

            txtPortero.Text = jugador.Portero.ToString();
            txtEntradas.Text = jugador.Entradas.ToString();
            txtRegate.Text = jugador.Regate.ToString();
            txtPase.Text = jugador.Pase.ToString();
            txtRemate.Text = jugador.Remate.ToString();
            txtTiro.Text = jugador.Tiro.ToString();
        }

        private void ValidarDia()
        {
            // Validar el día en función del mes y año
            if (int.TryParse(txtDia.Text, out int dia) && int.TryParse(txtMes.Text, out int mes) && int.TryParse(txtAnio.Text, out int anio))
            {
                // Asegurarse de que el mes esté en el rango correcto (1-12)
                if (mes < 1 || mes > 12) mes = 1;

                // Asegurarse de que el año esté en el rango correcto (1900 - año actual)
                if (anio < 1980) anio = 1980;
                if (anio > DateTime.Now.Year) anio = DateTime.Now.Year;

                // Obtener el número máximo de días del mes
                int maxDias = DateTime.DaysInMonth(anio, mes);

                // Validar que el día no exceda el máximo permitido para el mes
                if (dia < 1) dia = 1;
                if (dia > maxDias) dia = maxDias;

                // Actualizar el valor del día
                txtDia.Text = dia.ToString();
                txtDia.SelectionStart = txtDia.Text.Length;
            }
        }

        private bool EsFechaValida(int dia, int mes, int anio)
        {
            try
            {
                DateTime fecha = new DateTime(anio, mes, dia);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void CargarNacionalidades()
        {
            // Obtener todas las nacionalidades del método ObtenerCodigoBanderas
            var nacionalidades = ObtenerTodasLasNacionalidades();

            // Asignar al ComboBox
            cbNacionalidad.ItemsSource = nacionalidades;
        }

        private List<string> ObtenerTodasLasNacionalidades()
        {
            // Extraer todas las nacionalidades recorriendo las claves del switch
            return new List<string>
            {
                "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita",
                "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Bangladesh",
                "Barbados", "Baréin", "Bélgica", "Belice", "Benín", "Bielorrusia", "Birmania", "Bolivia",
                "Bosnia y Herzegovina", "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi",
                "Bután", "Cabo Verde", "Camboya", "Camerún", "Canadá", "Chad", "Chile", "China", "Chipre",
                "Colombia", "Comoras", "Corea del Norte",
                "Corea del Sur", "Costa Rica", "Costa de Marfil", "Croacia", "Cuba", "Curazao", "Dinamarca",
                "Dominica", "Ecuador", "Egipto", "El Salvador", "Emiratos Árabes Unidos", "Eritrea", "Escocia",
                "Eslovaquia", "Eslovenia", "España", "Estados Unidos", "Estonia", "Eswatini", "Etiopía", "Fiji",
                "Filipinas", "Finlandia", "Francia", "Gabón", "Gales", "Gambia", "Georgia", "Ghana", "Granada", "Grecia",
                "Guatemala", "Guinea", "Guinea-Bisáu", "Guyana", "Haití", "Honduras", "Hungría", "India", "Indonesia",
                "Inglaterra", "Irak", "Irán", "Irlanda", "Islandia", "Islas Feroe", "Islas Marshall", "Islas Salomón",
                "Israel", "Italia", "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati",
                "Kosovo", "Kuwait", "Laos", "Lesoto", "Letonia", "Líbano", "Liberia", "Libia", "Liechtenstein",
                "Lituania", "Luxemburgo", "Macedonia del Norte", "Madagascar", "Malasia", "Malawi", "Maldivas",
                "Mali", "Malta", "Moldavia", "Marruecos", "Martinica", "Mauricio", "Mauritania", "México",
                "Micronesia", "Mónaco", "Mongolia", "Montenegro", "Mozambique", "Namibia", "Nepal",
                "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Países Bajos", "Pakistán",
                "Palaos", "Panamá", "Papúa Nueva Guinea", "Paraguay", "Perú", "Polonia", "Portugal",
                "Reino Unido", "República Checa", "República del Congo", "República Dominicana", "Ruanda",
                "Rumanía", "Rusia", "Samoa", "San Marino", "Santo Tomé y Príncipe",
                "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sri Lanka",
                "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Surinam", "Sáhara Occidental",
                "Tailandia", "Tayikistán", "Tanzania", "Togo", "Tonga", "Trinidad y Tobago", "Túnez",
                "Turkmenistán", "Turquía", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu",
                "Vaticano", "Venezuela", "Vietnam", "Yemen", "Yibuti", "Zambia", "Zimbabue"
            };
        }

        private string AbrirSelectorImagen()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }
            return null;
        }

        private BitmapImage CargarImagenSinBloqueo(string ruta)
        {
            using (FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                BitmapImage imagen = new BitmapImage();
                imagen.BeginInit();
                imagen.CacheOption = BitmapCacheOption.OnLoad; // Cargar toda la imagen en memoria
                imagen.StreamSource = stream;
                imagen.EndInit();
                imagen.Freeze(); // Opcional: permite usarla en múltiples hilos
                return imagen;
            }
        }

        private bool ValidarImagen(string path, int anchoEsperado, int altoEsperado)
        {
            try
            {
                using (var imagen = new Bitmap(path))
                {
                    if (Path.GetExtension(path).ToLower() != ".png")
                    {
                        string titulo = "FORMATO INVÁLIDO";
                        string mensaje = "Solo se permiten imágenes en formato PNG";
                        frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaEmergente.ShowDialog();
                        return false;
                    }

                    if (imagen.Width != anchoEsperado || imagen.Height != altoEsperado)
                    {
                        string titulo = "FORMATO INVÁLIDO";
                        string mensaje = $"La imagen debe tener un tamaño exacto de {anchoEsperado}x{altoEsperado}px";
                        frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                        ventanaEmergente.ShowDialog();
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                string titulo = "INFORMACIÓN";
                string mensaje = "Error al validar la imagen";
                frmVentanaEmergenteDosBotones ventanaEmergente = new frmVentanaEmergenteDosBotones(titulo, mensaje, 2);
                ventanaEmergente.ShowDialog();
                return false;
            }
        }

        private string GuardarImagen(string origen, string destino)
        {
            try
            {
                string directorio = Path.GetDirectoryName(destino);
                string nombreArchivo = Path.GetFileNameWithoutExtension(destino);
                string extension = Path.GetExtension(destino);

                string destinoFinal = destino;
                int version = 1;

                // Si ya existe, buscar una versión libre
                while (File.Exists(destinoFinal))
                {
                    destinoFinal = Path.Combine(directorio, $"{nombreArchivo}_v{version}{extension}");
                    version++;
                }

                File.Copy(origen, destinoFinal);
                return Path.GetFileName(destinoFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la imagen: " + ex.Message);
                return null;
            }
        }
        #endregion
    }
}
