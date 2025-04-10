using System;
using System.IO;
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
using ChampionManager25.MisMetodos;
using ChampionManager25.Entidades;
using ChampionManager25.Logica;
using System.Configuration;
using ChampionManager25.Datos;
using System.Data.SQLite;
using Microsoft.Win32;

namespace ChampionManager25.UserControls
{
    public partial class UC_CrearManager : UserControl
    {
        #region "Variables"
        #endregion

        // Instancias de la LOGICA
        ManagerLogica logica = new ManagerLogica();

        public UC_CrearManager()
        {
            InitializeComponent();
            this.Loaded += CrearManager_Loaded;
        }

        private void CrearManager_Loaded(object sender, RoutedEventArgs e)
        {
            txtNombre.Focus();
            Keyboard.Focus(txtNombre);
            CargarNacionalidades();
        }

        // -------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON VOLVER
        private void imgBotonAtras_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.CargarPortada();
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------------------------------------------------------- EVENTO CLICK DEL BOTON AVANZAR
        private void btnAvanzar_Click(object sender, RoutedEventArgs e)
        {
            Metodos.ReproducirSonidoTransicion();

            // Validación y creación del manager...
            Manager nuevoManager = new Manager
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Nacionalidad = cbNacionalidad.SelectedItem?.ToString() ?? "Desconocida",
                FechaNacimiento = new DateTime(
                    int.Parse(txtAnio.Text),
                    int.Parse(txtMes.Text),
                    int.Parse(txtDia.Text))
            };

            int idManager = logica.CrearNuevoManager(nuevoManager);

            if (idManager > 0)
            {
                nuevoManager.IdManager = idManager;

                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.CargarSeleccionEquipo(nuevoManager);
            }
            else
            {
                MessageBox.Show("Error al crear el Manager");
            }
        }
        // --------------------------------------------------------------------------------------------------------------------------------

        // ------------------------------------------------- EVENTOS DE LAS CAJAS DE TEXTO ------------------------------------------------
        // --------------- NOMBRE
        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        private void txtNombre_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivarBotonAvanzar();
        }

        // --------------- APELLIDO
        private void txtApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[a-zA-Z\sáéíóúÁÉÍÓÚüÜñÑ]+$");
        }

        private void txtApellido_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActivarBotonAvanzar();
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
                if (anio < 1900) anio = 1900;
                if (anio > DateTime.Now.Year) anio = DateTime.Now.Year;

                int maxDias = DateTime.DaysInMonth(anio, mes);

                if (dia < 1) dia = 1;
                if (dia > maxDias) dia = maxDias;

                txtDia.Text = dia.ToString();
                txtDia.SelectionStart = txtDia.Text.Length;

                // Validar día
                ValidarDia();
            }

            ActivarBotonAvanzar();
        }

        // --------------- MES
        private void txtMes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtMes_PreviewKeyDown(object sender, KeyEventArgs e)
        {
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

            ActivarBotonAvanzar();
        }

        // --------------- AÑO
        private void txtAnio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !System.Text.RegularExpressions.Regex.IsMatch(e.Text, @"^[0-9]$");
        }

        private void txtAnio_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtAnio_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtAnio.Text.Length == 4 && !string.IsNullOrWhiteSpace(txtAnio.Text) && int.TryParse(txtAnio.Text, out int anio))
            {
                int anioLimite = DateTime.Now.Year - 18;
                if (anio < 1900) anio = 1900;
                if (anio > anioLimite) anio = anioLimite;

                txtAnio.Text = anio.ToString();
                txtAnio.SelectionStart = txtAnio.Text.Length;

                // Validar día
                ValidarDia();
            }

            ActivarBotonAvanzar();
        }
        // ---------------------------------------------------------------------------------------------------------------------------------

        #region "Métodos"
        private void ActivarBotonAvanzar()
        {
            int dia, mes, anio;

            // Validar día, mes y año
            bool esDiaValido = int.TryParse(txtDia.Text.Trim(), out dia);
            bool esMesValido = int.TryParse(txtMes.Text.Trim(), out mes);
            bool esAnioValido = int.TryParse(txtAnio.Text.Trim(), out anio);
            bool esFechaValida = esDiaValido && esMesValido && esAnioValido &&
                                 anio >= 1950 && anio <= 2024 && EsFechaValida(dia, mes, anio);

            // Validar que los campos de nombre y apellido no estén vacíos
            bool esNombreValido = !string.IsNullOrWhiteSpace(txtNombre.Text);
            bool esApellidoValido = !string.IsNullOrWhiteSpace(txtApellido.Text);

            // El botón se habilita solo si todos los criterios son válidos
            bool valida = esFechaValida && esNombreValido && esApellidoValido;

            // Habilitar o deshabilitar el botón
            btnAvanzar.IsEnabled = valida;
        }

        private void ValidarDia()
        {
            // Validar el día en función del mes y año
            if (int.TryParse(txtDia.Text, out int dia) && int.TryParse(txtMes.Text, out int mes) && int.TryParse(txtAnio.Text, out int anio))
            {
                // Asegurarse de que el mes esté en el rango correcto (1-12)
                if (mes < 1 || mes > 12) mes = 1;

                // Asegurarse de que el año esté en el rango correcto (1900 - año actual)
                if (anio < 1900) anio = 1900;
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
                "", "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudita",
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
                "Micronesia", "Mónaco", "Mongolia", "Montenegro", "Mozambique", "Namibia", "Nauru", "Nepal",
                "Nicaragua", "Níger", "Nigeria", "Noruega", "Nueva Zelanda", "Omán", "Países Bajos", "Pakistán",
                "Palaos", "Panamá", "Papúa Nueva Guinea", "Paraguay", "Perú", "Polonia", "Portugal",
                "Reino Unido", "República Checa", "República del Congo", "República Dominicana", "Ruanda",
                "Rumanía", "Rusia", "Samoa", "San Cristóbal y Nieves", "San Marino", "Santo Tomé y Príncipe",
                "Senegal", "Serbia", "Seychelles", "Sierra Leona", "Singapur", "Siria", "Somalia", "Sri Lanka",
                "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia", "Suiza", "Surinam", "Sáhara Occidental",
                "Tailandia", "Tayikistán", "Tanzania", "Togo", "Tonga", "Trinidad y Tobago", "Túnez",
                "Turkmenistán", "Turquía", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu",
                "Vaticano", "Venezuela", "Vietnam", "Yemen", "Yibuti", "Zambia", "Zimbabue"
            };
        }

        public void CrearBaseDeDatosManager(string idManager)
        {
            // Ruta al directorio de salida (Debug o Release)
            string rutaProyecto = Directory.GetCurrentDirectory();  // Esto apunta al directorio de salida (Debug/Release)

            // Ruta de la base de datos original que está en el proyecto
            string rutaBaseDatosOriginal = Path.Combine(Directory.GetParent(rutaProyecto).FullName, "championsManagerDB.db");

            // Ruta donde se guardará la copia en el directorio de salida
            string rutaNuevaBaseDatos = Path.Combine(rutaProyecto, $"manager{idManager}DB.db");

            // Imprimir la ruta para depuración
            Console.WriteLine($"Buscando base de datos original en: {rutaBaseDatosOriginal}");

            // Verifica si la base de datos nueva ya existe, si no, crea una copia
            if (!File.Exists(rutaNuevaBaseDatos))
            {
                try
                {
                    // Asegúrate de que el archivo original existe antes de copiar
                    if (File.Exists(rutaBaseDatosOriginal))
                    {
                        File.Copy(rutaBaseDatosOriginal, rutaNuevaBaseDatos);
                        Console.WriteLine($"Base de datos copiada a {rutaNuevaBaseDatos}");
                    }
                    else
                    {
                        Console.WriteLine("El archivo de base de datos original no se encuentra.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al copiar la base de datos: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("La base de datos con el nombre especificado ya existe.");
            }
        }
        #endregion 
    }
}
