using ChampionManager25.Entidades;
using ChampionManager25.MisMetodos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace ChampionManager25.Datos
{
    public class JugadorDatos : Conexion
    {
        private Metodos metodos;

        public JugadorDatos()
        {
            metodos = new Metodos();  // Asegura que la temporada se inicialice
        }

        // ===================================================================== Método para mostrar el jugador con más Media
        public Jugador? MejorJugador(int id)
        {
            Jugador? oJugador = null; // Inicializamos la variable de tipo Jugador como null

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT id_jugador, nombre, apellido, rol_id, velocidad, resistencia, agresividad, calidad, estado_forma, moral, ruta_imagen
                                     FROM jugadores
                                     WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            double maxMedia = -1; // Variable para almacenar la máxima media
                            int idJugadorMaximo = 0; // Variable para almacenar el id del jugador con la mayor media

                            while (reader.Read())
                            {
                                // Leer los valores de la base de datos
                                int velocidad = reader.IsDBNull(reader.GetOrdinal("velocidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("velocidad"));
                                int resistencia = reader.IsDBNull(reader.GetOrdinal("resistencia")) ? 0 : reader.GetInt32(reader.GetOrdinal("resistencia"));
                                int agresividad = reader.IsDBNull(reader.GetOrdinal("agresividad")) ? 0 : reader.GetInt32(reader.GetOrdinal("agresividad"));
                                int calidad = reader.IsDBNull(reader.GetOrdinal("calidad")) ? 0 : reader.GetInt32(reader.GetOrdinal("calidad"));
                                int estado_forma = reader.IsDBNull(reader.GetOrdinal("estado_forma")) ? 0 : reader.GetInt32(reader.GetOrdinal("estado_forma"));
                                int moral = reader.IsDBNull(reader.GetOrdinal("moral")) ? 0 : reader.GetInt32(reader.GetOrdinal("moral"));

                                // Calcular la media
                                double media = (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0;

                                // Si la media actual es mayor que la máxima encontrada, actualizar el máximo y el jugador correspondiente
                                if (media > maxMedia)
                                {
                                    maxMedia = media;
                                    idJugadorMaximo = reader.GetInt32(reader.GetOrdinal("id_jugador"));
                                    string nombre = reader.GetString(reader.GetOrdinal("nombre"));
                                    string apellido = reader.GetString(reader.GetOrdinal("apellido"));
                                    string rutaImagen = reader.GetString(reader.GetOrdinal("ruta_imagen"));

                                    // Crear un nuevo objeto Jugador con los datos del jugador con mayor media
                                    oJugador = new Jugador
                                    {
                                        IdJugador = idJugadorMaximo,
                                        Nombre = nombre,
                                        Apellido = apellido,
                                        Velocidad = velocidad,
                                        Resistencia = resistencia,
                                        Agresividad = agresividad,
                                        Calidad = calidad,
                                        EstadoForma = estado_forma,
                                        Moral = moral,
                                        RutaImagen = rutaImagen
                                    };
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oJugador;
        }

        // ======================================================= Método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> ListadoJugadoresCompleto(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.*, c.duracion AS AniosContrato, c.salario_anual AS SalarioTemporada, c.clausula_rescision AS ClausulaRescision
                                    FROM jugadores j
                                    LEFT JOIN contratos c ON j.id_jugador = c.id_jugador
                                    WHERE j.id_equipo = @idEquipo
                                    ORDER BY j.rol_id ASC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    Entrenamiento = dr.GetInt32(dr.GetOrdinal("entrenamiento")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen")),
                                    ValorMercado = dr.GetInt32(dr.GetOrdinal("valor_mercado")),
                                    AniosContrato = dr.IsDBNull(dr.GetOrdinal("AniosContrato")) ? null : dr.GetInt32(dr.GetOrdinal("AniosContrato")),
                                    SalarioTemporada = dr.IsDBNull(dr.GetOrdinal("SalarioTemporada")) ? null : dr.GetInt32(dr.GetOrdinal("SalarioTemporada")),
                                    ClausulaRescision = dr.IsDBNull(dr.GetOrdinal("ClausulaRescision")) ? null : dr.GetInt32(dr.GetOrdinal("ClausulaRescision"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método para mostrar la Media del equipo
        public double ObtenerMediaEquipo(int idEquipo)
        {
            double media = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT velocidad, resistencia, agresividad, calidad, estado_forma, moral 
                                 FROM jugadores 
                                 WHERE id_equipo = @IdEquipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEquipo", idEquipo);

                    using (var reader = cmd.ExecuteReader())
                    {
                        var medias = new List<double>();

                        while (reader.Read())
                        {
                            int velocidad = reader.GetInt32(0);
                            int resistencia = reader.GetInt32(1);
                            int agresividad = reader.GetInt32(2);
                            int calidad = reader.GetInt32(3);
                            int estadoForma = reader.GetInt32(4);
                            int moral = reader.GetInt32(5);

                            double mediaJugador = (velocidad + resistencia + agresividad + calidad + estadoForma + moral) / 6.0;

                            medias.Add(mediaJugador);
                        }

                        if (medias.Any())
                        {
                            media = medias.Average();
                        }
                    }
                }
            }

            return media;
        }

        // ===================================================================== Método para mostrar el numero total de jugadores del juego
        public int NumeroJugadoresTotales()
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM jugadores";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // ======================================================= Método para mostrar los datos de un jugador
        public Jugador MostrarDatosJugador(int id)
        {
            Jugador jugador = null;

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT 
                                        j.id_jugador,
                                        j.nombre,
                                        j.apellido,
                                        j.peso,
                                        j.altura,
                                        j.nacionalidad,
                                        j.dorsal,
                                        j.fecha_nacimiento,
                                        j.rol_id,
                                        j.rol,
                                        j.velocidad,
                                        j.resistencia,
                                        j.agresividad,
                                        j.calidad,
                                        j.estado_forma,
                                        j.moral,
                                        j.potencial,
                                        j.portero,
                                        j.pase,
                                        j.regate,
                                        j.remate,
                                        j.entradas,
                                        j.tiro,
                                        j.lesion,
                                        j.tipo_lesion,
                                        j.lesion_tratada,
                                        j.valor_mercado,
                                        j.estado_animo, 
                                        j.situacion_mercado,
                                        j.id_equipo,
                                        j.status,
                                        j.proxima_negociacion,
                                        j.ruta_imagen,
                                        c.duracion AS AniosContrato,
                                        c.salario_anual AS SalarioTemporada,
                                        c.clausula_rescision AS ClausulaRescision,
                                        c.bono_por_partidos AS BonoPartidos,
                                        c.bono_por_goles AS BonoGoles,
                                        e.nombre AS NombreEquipo
                                    FROM jugadores j
                                    LEFT JOIN equipos e ON e.id_equipo = j.id_equipo
                                    LEFT JOIN contratos c ON j.id_jugador = c.id_jugador
                                    WHERE j.id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@IdJugador", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    ValorMercado = dr.GetInt32(dr.GetOrdinal("valor_mercado")),
                                    EstadoAnimo = dr.GetInt32(dr.GetOrdinal("estado_animo")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen")),
                                    NombreEquipo = dr.GetString(dr.GetOrdinal("NombreEquipo")),
                                    SituacionMercado = dr.IsDBNull(dr.GetOrdinal("situacion_mercado")) ? 0 : dr.GetInt32(dr.GetOrdinal("situacion_mercado")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    AniosContrato = dr.IsDBNull(dr.GetOrdinal("AniosContrato")) ? null : dr.GetInt32(dr.GetOrdinal("AniosContrato")),
                                    SalarioTemporada = dr.IsDBNull(dr.GetOrdinal("SalarioTemporada")) ? null : dr.GetInt32(dr.GetOrdinal("SalarioTemporada")),
                                    BonusPartido = dr.IsDBNull(dr.GetOrdinal("BonoPartidos")) ? null : dr.GetInt32(dr.GetOrdinal("BonoPartidos")),
                                    BonusGoles = dr.IsDBNull(dr.GetOrdinal("BonoGoles")) ? null : dr.GetInt32(dr.GetOrdinal("BonoGoles")),
                                    ClausulaRescision = dr.IsDBNull(dr.GetOrdinal("ClausulaRescision")) ? null : dr.GetInt32(dr.GetOrdinal("ClausulaRescision")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    ProximaNegociacion = dr.IsDBNull(dr.GetOrdinal("proxima_negociacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("proxima_negociacion"))
                                };
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return jugador;
        }

        // ======================================================= Método para mostrar todos los jugadores de la base de datos
        public List<Jugador> MostrarListaTotalJugadores()
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT * FROM jugadores";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    Entrenamiento = dr.GetInt32(dr.GetOrdinal("entrenamiento")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método que renueva el status de un jugador
        public void RenovarStatusJugador(int jugador, int rol)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET status = @Status WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Status", rol);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


        // ===================================================================== Método que cambia el equipo del jugador
        public void CambiarDeEquipo(int jugador, int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // 1. Obtener dorsales usados por el equipo destino
                    List<int> dorsalesOcupados = new List<int>();
                    string dorsalesQuery = @"SELECT dorsal FROM jugadores WHERE id_equipo = @IdEquipo AND id_equipo <> 0";

                    using (SQLiteCommand cmdDorsales = new SQLiteCommand(dorsalesQuery, conn))
                    {
                        cmdDorsales.Parameters.AddWithValue("@IdEquipo", equipo);
                        using (SQLiteDataReader reader = cmdDorsales.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dorsalesOcupados.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    // 2. Buscar el primer dorsal disponible del 1 al 50
                    int nuevoDorsal = Enumerable.Range(1, 50).FirstOrDefault(d => !dorsalesOcupados.Contains(d));

                    if (nuevoDorsal == 0)
                    {
                        MessageBox.Show("No hay dorsales disponibles entre 1 y 50 para este equipo.");
                        return;
                    }

                    // 3. Actualizar el equipo y el dorsal del jugador
                    string query = @"UPDATE jugadores 
                             SET id_equipo = @IdEquipo, dorsal = @NuevoDorsal 
                             WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@NuevoDorsal", nuevoDorsal);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }


        // ------------------------------------------------------------------------- Método para mostrar los titulares del equipo
        public void CrearAlineacion(int equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    string query = @"SELECT id_jugador, rol_id,
                                           (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 as media
                                     FROM jugadores 
                                     WHERE id_equipo = @equipo
                                     ORDER BY 
                                        CASE 
                                            WHEN rol_id = 1 THEN 1
                                            WHEN rol_id = 4 THEN 2
                                            WHEN rol_id = 2 THEN 3
                                            WHEN rol_id = 3 THEN 4
                                            WHEN rol_id BETWEEN 5 AND 7 THEN 5
                                            WHEN rol_id BETWEEN 8 AND 10 THEN 6
                                            ELSE 7 
                                        END,
                                        media DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@equipo", equipo);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            List<(int id_jugador, int rol_id, double media)> jugadores = new();

                            while (reader.Read())
                            {
                                jugadores.Add((
                                    reader.GetInt32(0),
                                    reader.GetInt32(1),
                                    reader.GetDouble(2)
                                ));
                            }

                            if (jugadores.Count == 0) return;

                            // Agrupar por rol
                            var porteros = jugadores.Where(j => j.rol_id == 1).ToList();
                            var centrales = jugadores.Where(j => j.rol_id == 4).ToList();
                            var lateralDer = jugadores.Where(j => j.rol_id == 2).ToList();
                            var lateralIzq = jugadores.Where(j => j.rol_id == 3).ToList();
                            var mediocampistas = jugadores.Where(j => j.rol_id >= 5 && j.rol_id <= 7).ToList();
                            var delanteros = jugadores.Where(j => j.rol_id >= 8 && j.rol_id <= 10).ToList();

                            Dictionary<int, int> posiciones = new();
                            int posicion = 1;

                            if (porteros.Count >= 1) posiciones[posicion++] = porteros[0].id_jugador;
                            if (centrales.Count >= 2) { posiciones[posicion++] = centrales[0].id_jugador; posiciones[posicion++] = centrales[1].id_jugador; }
                            if (lateralDer.Count >= 1) posiciones[posicion++] = lateralDer[0].id_jugador;
                            if (lateralIzq.Count >= 1) posiciones[posicion++] = lateralIzq[0].id_jugador;

                            for (int i = 0; i < Math.Min(4, mediocampistas.Count); i++)
                                posiciones[posicion++] = mediocampistas[i].id_jugador;

                            for (int i = 0; i < Math.Min(2, delanteros.Count); i++)
                                posiciones[posicion++] = delanteros[i].id_jugador;

                            // Si hay menos de 11 jugadores asignados, completar con los mejores disponibles restantes
                            var usados = new HashSet<int>(posiciones.Values);
                            var restantes = jugadores.Where(j => !usados.Contains(j.id_jugador)).ToList();

                            while (posiciones.Count < 11 && restantes.Count > 0)
                            {
                                posiciones[posicion++] = restantes[0].id_jugador;
                                usados.Add(restantes[0].id_jugador);
                                restantes.RemoveAt(0);
                            }

                            // Insertar titulares
                            foreach (var kvp in posiciones)
                            {
                                string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@id_jugador", kvp.Value);
                                    insertCmd.Parameters.AddWithValue("@posicion", kvp.Key);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }

                            // Insertar suplentes
                            int pos = 12;
                            foreach (var suplente in jugadores.Where(j => !posiciones.Values.Contains(j.id_jugador)))
                            {
                                string insertQuery = "INSERT INTO alineacion (id_jugador, posicion) VALUES (@id_jugador, @posicion)";
                                using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@id_jugador", suplente.id_jugador);
                                    insertCmd.Parameters.AddWithValue("@posicion", pos++);
                                    insertCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"Error al generar la alineación: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que agrega un jugador a la alineacion
        public void AgregarJugadorAlineacion(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"INSERT INTO alineacion (id_jugador, posicion)
                                    VALUES (@IdJugador,
                                        COALESCE((SELECT MAX(posicion) FROM alineacion), 0) + 1)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que resetea la tabla alineacion
        public void ResetearAlineacion()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM alineacion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que quita un jugador a la alineacion
        public void QuitarJugadorAlineacion(int idJugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Paso 1: Obtener la posición del jugador a eliminar
                    int posicionJugador = -1;
                    string queryPosicion = "SELECT posicion FROM alineacion WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand cmd = new SQLiteCommand(queryPosicion, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", idJugador);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            posicionJugador = Convert.ToInt32(result);
                        }
                        else
                        {
                            MessageBox.Show("Jugador no encontrado en la alineación.");
                            return;
                        }
                    }

                    // Paso 2: Eliminar el jugador
                    string queryEliminar = "DELETE FROM alineacion WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand cmd = new SQLiteCommand(queryEliminar, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdJugador", idJugador);
                        cmd.ExecuteNonQuery();
                    }

                    // Paso 3: Actualizar posiciones de los jugadores con posición mayor
                    string queryActualizar = "UPDATE alineacion SET posicion = posicion - 1 WHERE posicion > @Posicion";
                    using (SQLiteCommand cmd = new SQLiteCommand(queryActualizar, conn))
                    {
                        cmd.Parameters.AddWithValue("@Posicion", posicionJugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ======================================================= Método para mostrar la lista de Jugadores Detallada por equipo
        public List<Jugador> MostrarAlineacion(int inicio, int final)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.*, a.posicion
                                     FROM jugadores j
                                     JOIN alineacion a ON j.id_jugador = a.id_jugador 
                                     WHERE a.posicion >= @inicio AND a.posicion <= @final 
                                     ORDER BY a.posicion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@inicio", inicio);
                        cmd.Parameters.AddWithValue("@final", final);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    PosicionAlineacion = dr.GetInt32(dr.GetOrdinal("posicion")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ===================================================================== Método que cambia las posiciones entre 2 jugadores
        public void IntercambioPosicion(int jugador1, int jugador2, int posicion1, int posicion2)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    using (SQLiteTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SQLiteCommand cmd = conn.CreateCommand())
                            {
                                // Actualizar la posición del primer jugador
                                cmd.CommandText = @"UPDATE alineacion 
                                                    SET posicion = @PosicionDos
                                                    WHERE id_jugador = @IdJugadorUno";
                                cmd.Parameters.AddWithValue("@IdJugadorUno", jugador1);
                                cmd.Parameters.AddWithValue("@PosicionDos", posicion2);
                                cmd.ExecuteNonQuery();

                                // Limpiar parámetros para la segunda consulta
                                cmd.Parameters.Clear();

                                // Actualizar la posición del segundo jugador
                                cmd.CommandText = @"UPDATE alineacion 
                                                    SET posicion = @PosicionUno
                                                    WHERE id_jugador = @IdJugadorDos";
                                cmd.Parameters.AddWithValue("@IdJugadorDos", jugador2);
                                cmd.Parameters.AddWithValue("@PosicionUno", posicion1);
                                cmd.ExecuteNonQuery();
                            }

                            // Confirmar los cambios en la base de datos
                            transaction.Commit();
                        }
                        catch (SQLiteException ex)
                        {
                            // Revertir cambios si hay error
                            transaction.Rollback();
                            MessageBox.Show($"Error en la transacción: {ex.Message}");
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para mostrar el entrenamiento de un jugador
        public int EntrenamientoJugador(int jugador)
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT entrenamiento FROM jugadores WHERE id_jugador = @IdJugador";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // ===================================================================== Método que selecciona un entrenamiento para un jugador
        public void EntrenarJugador(int jugador, int tipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET entrenamiento = @Entrenamiento
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Entrenamiento", tipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ======================================================= Método para mostrar la lista de Jugadores de un equipo SIN PORTEROS
        public List<Jugador> PlantillaSinPorteros(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT *
                                    FROM jugadores
                                    WHERE id_equipo = @idEquipo AND rol_id <> 1 AND lesion = 0";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@idEquipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ======================================================= Método para crear los 16 jugadores que jugaran el partido
        public List<Jugador> JugadoresJueganPartido(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener un portero y los 15 mejores jugadores de campo sin estar lesionados ni sancionados
                    string query = @"SELECT * FROM (
                                        SELECT *, 
                                            (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                        FROM jugadores 
                                        WHERE id_equipo = @equipo AND lesion = 0 AND sancionado = 0 AND rol_id = 1
                                        ORDER BY media DESC 
                                        LIMIT 1
                                     )
                                     UNION ALL
                                     SELECT * FROM (
                                        SELECT *, 
                                            (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                        FROM jugadores 
                                        WHERE id_equipo = @equipo AND lesion = 0 AND sancionado = 0 AND rol_id BETWEEN 2 AND 10
                                        ORDER BY media DESC 
                                        LIMIT 16
                                     )";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@equipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            string jugadoresLocalInfo = "Jugadores del equipo local:\n";
            foreach (var jugador in lista)
            {
                jugadoresLocalInfo += $"ID: {jugador.IdJugador}, Nombre: {jugador.Nombre} {jugador.Apellido}, Rol: {jugador.Rol}\n";
                
            }
          
            return lista;
        }

        // ======================================================= Método para crear los 16 jugadores que jugaran el partido DE MI EQUIPO
        public List<Jugador> JugadoresMiEquipoJueganPartido(int id)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener un portero y los 15 mejores jugadores de campo sin estar lesionados ni sancionados
                    string query = @"SELECT j.*, 
                                            (j.velocidad + j.resistencia + j.agresividad + j.calidad + j.estado_forma + j.moral) / 6.0 AS media,
                                            a.posicion
                                     FROM alineacion a
                                     JOIN jugadores j ON a.id_jugador = j.id_jugador
                                     WHERE j.id_equipo = @equipo
                                        AND j.lesion = 0
                                        AND j.sancionado = 0
                                     ORDER BY a.posicion
                                     LIMIT 16";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@equipo", id);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    Sancionado = dr.GetInt32(dr.GetOrdinal("sancionado")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen"))
                                };

                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            string jugadoresLocalInfo = "Jugadores del equipo local:\n";
            foreach (var jugador in lista)
            {
                jugadoresLocalInfo += $"ID: {jugador.IdJugador}, Nombre: {jugador.Nombre} {jugador.Apellido}, Rol: {jugador.Rol}\n";

            }

            return lista;
        }

        // ===================================================================== Método que pone a un jugador como lesionado
        public void PonerJugadorLesionado(int jugador, int duracion, string tipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET lesion = @Duracion, tipo_lesion = @Descripcion
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Duracion", duracion);
                        cmd.Parameters.AddWithValue("@Descripcion", tipo);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que selecciona un entrenamiento para un jugador
        public void PonerJugadorSancionado(int jugador, int duracion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET sancionado = @Duracion
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Duracion", duracion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método para decir si un jugador pertenece a mi equipo
        public bool EsDeMiEquipo(int jugador, int equipo)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM jugadores WHERE id_jugador = @IdJugador AND id_equipo = @IdEquipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);
                    cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        // ===================================================================== Método que incrementa/decrementa la moral y el estado de forma
        public void ActualizarMoralEstadoForma(int jugador, int moral, int estadoForma)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET moral = MAX(0, MIN(99, moral + @Moral)),
                                         estado_forma = MAX(0, MIN(99, estado_forma + @EstadoForma))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Moral", moral);
                        cmd.Parameters.AddWithValue("@EstadoForma", estadoForma);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ===================================================================== Método que actualiza los atributos por entrenamiento
        public void ActualizarAtributosEntrenamiento(int jugador, int portero, int pase, int regate, int remate, int entradas, int tiro)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET portero = MAX(0, MIN(99, portero + @Portero)),
                                         pase = MAX(0, MIN(99, pase + @Pase)),
                                         regate = MAX(0, MIN(99, regate + @Regate)),
                                         remate = MAX(0, MIN(99, remate + @Remate)),
                                         entradas = MAX(0, MIN(99, entradas + @Entradas)),
                                         tiro = MAX(0, MIN(99, tiro + @Tiro))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Portero", portero);
                        cmd.Parameters.AddWithValue("@Pase", pase);
                        cmd.Parameters.AddWithValue("@Regate", regate);
                        cmd.Parameters.AddWithValue("@Remate", remate);
                        cmd.Parameters.AddWithValue("@Entradas", entradas);
                        cmd.Parameters.AddWithValue("@Tiro", tiro);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        public void ActualizarOtrosAtributosEntrenamiento(int jugador, int velocidad, int resistencia, int agresividad, int calidad)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET velocidad = MAX(0, MIN(99, velocidad + @Velocidad)),
                                         resistencia = MAX(0, MIN(99, resistencia + @Resistencia)),
                                         agresividad = MAX(0, MIN(99, agresividad + @Agresividad)),
                                         calidad = MAX(0, MIN(99, calidad + @Calidad))
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Velocidad", velocidad);
                        cmd.Parameters.AddWithValue("@Resistencia", resistencia);
                        cmd.Parameters.AddWithValue("@Agresividad", agresividad);
                        cmd.Parameters.AddWithValue("@Calidad", calidad);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ------------------------------------------------------------------------- Método que resetea la moral y el estado de forma
        public void ResetearMoralEstadoForma()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores
                                     SET moral = 50, estado_forma = 50";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ------------------------------------------------------------------------- Método que actualiza un jugador
        public void ActualizarJugador(Jugador ojugadorEditar)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Construir consulta base
                    string query = @"UPDATE jugadores 
                                     SET nombre = @nombre,
                                        apellido = @apellido,
                                        id_equipo = @id_equipo,
                                        dorsal = @dorsal,
                                        status = @status,
                                        rol = @rol,
                                        rol_id = @rol_id,
                                        velocidad = @velocidad,
                                        resistencia = @resistencia,
                                        agresividad = @agresividad,
                                        calidad = @calidad,
                                        estado_forma = @estado_forma,
                                        moral = @moral,
                                        potencial = @potencial,
                                        portero = @portero,
                                        pase = @pase,
                                        regate = @regate,
                                        remate = @remate,
                                        entradas = @entradas,
                                        tiro = @tiro,
                                        fecha_nacimiento = @fecha_nacimiento,
                                        peso = @peso,
                                        altura = @altura,
                                        nacionalidad = @nacionalidad";

                    // Si hay ruta de imagen, añadirla a la consulta
                    if (ojugadorEditar.RutaImagen != "Recursos/img/jugadores/")
                    {
                        query += ", ruta_imagen = @ruta_imagen";
                    }

                    // Cerrar la consulta con WHERE
                    query += " WHERE id_jugador = @id_jugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros obligatorios
                        cmd.Parameters.AddWithValue("@nombre", ojugadorEditar.Nombre);
                        cmd.Parameters.AddWithValue("@apellido", ojugadorEditar.Apellido);
                        cmd.Parameters.AddWithValue("@id_equipo", ojugadorEditar.IdEquipo);
                        cmd.Parameters.AddWithValue("@dorsal", ojugadorEditar.Dorsal);
                        cmd.Parameters.AddWithValue("@status", ojugadorEditar.Status);
                        cmd.Parameters.AddWithValue("@rol", ojugadorEditar.Rol);
                        cmd.Parameters.AddWithValue("@rol_id", ojugadorEditar.RolId);
                        cmd.Parameters.AddWithValue("@velocidad", ojugadorEditar.Velocidad);
                        cmd.Parameters.AddWithValue("@resistencia", ojugadorEditar.Resistencia);
                        cmd.Parameters.AddWithValue("@agresividad", ojugadorEditar.Agresividad);
                        cmd.Parameters.AddWithValue("@calidad", ojugadorEditar.Calidad);
                        cmd.Parameters.AddWithValue("@estado_forma", ojugadorEditar.EstadoForma);
                        cmd.Parameters.AddWithValue("@moral", ojugadorEditar.Moral);
                        cmd.Parameters.AddWithValue("@potencial", ojugadorEditar.Potencial);
                        cmd.Parameters.AddWithValue("@portero", ojugadorEditar.Portero);
                        cmd.Parameters.AddWithValue("@pase", ojugadorEditar.Pase);
                        cmd.Parameters.AddWithValue("@regate", ojugadorEditar.Regate);
                        cmd.Parameters.AddWithValue("@remate", ojugadorEditar.Remate);
                        cmd.Parameters.AddWithValue("@entradas", ojugadorEditar.Entradas);
                        cmd.Parameters.AddWithValue("@tiro", ojugadorEditar.Tiro);
                        cmd.Parameters.AddWithValue("@fecha_nacimiento", ojugadorEditar.FechaNacimiento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@peso", ojugadorEditar.Peso);
                        cmd.Parameters.AddWithValue("@altura", ojugadorEditar.Altura);
                        cmd.Parameters.AddWithValue("@nacionalidad", ojugadorEditar.Nacionalidad ?? "Desconocida");
                        cmd.Parameters.AddWithValue("@id_jugador", ojugadorEditar.IdJugador);

                        // Parámetro opcional
                        if (!string.IsNullOrEmpty(ojugadorEditar.RutaImagen))
                        {
                            cmd.Parameters.AddWithValue("@ruta_imagen", ojugadorEditar.RutaImagen);
                        }

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ------------------------------------------------------------------- Método que reduce una lesion un %
        public void TratarLesion(int idJugador, int porcentaje)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Paso 1: Obtener el valor actual de la lesión
                    int lesionActual = 0;
                    string selectQuery = "SELECT lesion FROM jugadores WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@IdJugador", idJugador);
                        object result = selectCmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            lesionActual = Convert.ToInt32(result);
                    }

                    // Paso 2: Calcular la nueva lesión
                    double factor = (100 - porcentaje) / 100.0;
                    int nuevaLesion = (int)Math.Round(lesionActual * factor);

                    // Paso 3: Actualizar el valor de lesión
                    string updateQuery = "UPDATE jugadores SET lesion = @NuevaLesion WHERE id_jugador = @IdJugador";
                    using (SQLiteCommand updateCmd = new SQLiteCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@NuevaLesion", nuevaLesion);
                        updateCmd.Parameters.AddWithValue("@IdJugador", idJugador);
                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que pone un jugador en el mercado
        public void PonerJugadorEnMercado(int jugador, int opcion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET situacion_mercado = @Opcion WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Opcion", opcion);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que quita un jugador del mercado
        public void QuitarJugadorDeMercado(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET situacion_mercado = 0 WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que despide un jugador del equipo
        public void DespedirJugador(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET id_equipo = 0 WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que elimina un contrato de un jugador
        public void EliminarContratoJugador(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM contratos WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que crea una nueva fecha de negociación
        public void NegociacionCancelada(int jugador, int dias)
        {
            DateTime fechaEnfado = Metodos.hoy.AddDays(dias);
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET proxima_negociacion = @FechaEnfado WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@FechaEnfado", fechaEnfado.ToString("yyyy-MM-dd"));

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que renueva el contrato de un jugador
        public void RenovarContratoJugador(int jugador, int salario, int clausula, int anios, int bonusP, int bonusG)
        {
            DateTime nuevaFecha = new DateTime(Metodos.hoy.Year + anios, 6, 30);

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE contratos SET salario_anual = @Salario, 
                                                          clausula_rescision = @Clausula, 
                                                          duracion = @Anios, 
                                                          bono_por_partidos = @BonusP, 
                                                          bono_por_goles = @BonusG, 
                                                          fecha_inicio = @FechaInicio,
                                                          fecha_fin = @FechaFin
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Salario", salario);
                        cmd.Parameters.AddWithValue("@Clausula", clausula);
                        cmd.Parameters.AddWithValue("@Anios", anios);
                        cmd.Parameters.AddWithValue("@FechaInicio", Metodos.hoy.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@FechaFin", nuevaFecha.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@BonusP", bonusP);
                        cmd.Parameters.AddWithValue("@BonusG", bonusG);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que cambia el contrato tras un fichaje
        public void CambiarContratoJugador(int jugador, int salario, int clausula, int anios, int bonusP, int bonusG, int equipo)
        {
            DateTime nuevaFecha = new DateTime(Metodos.hoy.Year + anios, 6, 30);

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE contratos SET salario_anual = @Salario, 
                                                          clausula_rescision = @Clausula, 
                                                          duracion = @Anios, 
                                                          bono_por_partidos = @BonusP, 
                                                          bono_por_goles = @BonusG, 
                                                          fecha_inicio = @FechaInicio,
                                                          fecha_fin = @FechaFin,
                                                          id_equipo = @IdEquipo
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Salario", salario);
                        cmd.Parameters.AddWithValue("@Clausula", clausula);
                        cmd.Parameters.AddWithValue("@Anios", anios);
                        cmd.Parameters.AddWithValue("@FechaInicio", Metodos.hoy.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@FechaFin", nuevaFecha.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@BonusP", bonusP);
                        cmd.Parameters.AddWithValue("@BonusG", bonusG);
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método para mostrar la lista de Jugadores en el Mercado
        public List<Jugador> ListadoJugadoresMercado(int equipo, int tipoStart, int tipoEnd, int mediaStart, int mediaEnd, int posicionStart, int posicionEnd)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.*, c.duracion AS AniosContrato, c.salario_anual AS SalarioTemporada, c.clausula_rescision AS ClausulaRescision
                                     FROM jugadores j
                                     LEFT JOIN contratos c ON j.id_jugador = c.id_jugador
                                     WHERE j.situacion_mercado BETWEEN @TipoStart AND @TipoEnd AND
                                           ROUND((j.velocidad + j.agresividad + j.resistencia + j.calidad + j.estado_forma + j.moral) / 6, 0) BETWEEN @MediaStart AND @MediaEnd AND
                                           j.rol_id BETWEEN @PosicionStart AND @PosicionEnd";


                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor del parámetro de la consulta
                        cmd.Parameters.AddWithValue("@Equipo", equipo);
                        cmd.Parameters.AddWithValue("@TipoStart", tipoStart);
                        cmd.Parameters.AddWithValue("@TipoEnd", tipoEnd);
                        cmd.Parameters.AddWithValue("@MediaStart", mediaStart);
                        cmd.Parameters.AddWithValue("@MediaEnd", mediaEnd);
                        cmd.Parameters.AddWithValue("@PosicionStart", posicionStart);
                        cmd.Parameters.AddWithValue("@PosicionEnd", posicionEnd);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    ValorMercado = dr.GetInt32(dr.GetOrdinal("valor_mercado")),
                                    EstadoAnimo = dr.GetInt32(dr.GetOrdinal("estado_animo")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen")),
                                    SituacionMercado = dr.IsDBNull(dr.GetOrdinal("situacion_mercado")) ? 0 : dr.GetInt32(dr.GetOrdinal("situacion_mercado")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    AniosContrato = dr.IsDBNull(dr.GetOrdinal("AniosContrato")) ? null : dr.GetInt32(dr.GetOrdinal("AniosContrato")),
                                    SalarioTemporada = dr.IsDBNull(dr.GetOrdinal("SalarioTemporada")) ? null : dr.GetInt32(dr.GetOrdinal("SalarioTemporada")),
                                    ClausulaRescision = dr.IsDBNull(dr.GetOrdinal("ClausulaRescision")) ? null : dr.GetInt32(dr.GetOrdinal("ClausulaRescision")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    ProximaNegociacion = dr.IsDBNull(dr.GetOrdinal("proxima_negociacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("proxima_negociacion"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ---------------------------------------------------------------------- Método para mostrar la lista de Jugadores Por Filtros
        public List<Jugador> ListadoJugadoresPorFiltro(int equipo, string nacionalidad, int competicion, int posicion, int edadMin, int edadMax, int mediaMin, int mediaMax, int calidadMin, int calidadMax,
                               int velocidadMin, int velocidadMax, int resistenciaMin, int resistenciaMax, int agresividadMin, int agresividadMax)
        {
            List<Jugador> lista = new List<Jugador>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT j.* 
                                     FROM jugadores j
                                     JOIN equipos e ON j.id_equipo = e.id_equipo
                                     WHERE (@equipo <> j.id_equipo
                                           AND (@nacionalidad = '' OR j.nacionalidad = @nacionalidad))
                                           AND (@competicion = 0 OR e.id_competicion = @competicion)
                                           AND (@posicion = 0 OR j.rol_id = @posicion)
                                           AND ((julianday(@hoy) - julianday(j.fecha_nacimiento)) / 365.25) BETWEEN @edadMin AND @edadMax
                                           AND (@mediaMax = 1 OR ((j.velocidad + j.resistencia + j.agresividad + j.calidad + j.estado_forma + j.moral) / 6.0) BETWEEN @mediaMin AND @mediaMax)
                                           AND (@calidadMin = 1 OR j.calidad >= @calidadMin)
                                           AND (@calidadMax = 1 OR j.calidad <= @calidadMax)
                                           AND (@velocidadMin = 1 OR j.velocidad >= @velocidadMin)
                                           AND (@velocidadMax = 1 OR j.velocidad <= @velocidadMax)
                                           AND (@resistenciaMin = 1 OR j.resistencia >= @resistenciaMin)
                                           AND (@resistenciaMax = 1 OR j.resistencia <= @resistenciaMax)
                                           AND (@agresividadMin = 1 OR j.agresividad >= @agresividadMin)
                                           AND (@agresividadMax = 1 OR j.agresividad <= @agresividadMax)
                                     ORDER BY j.calidad DESC";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Asignar el valor de los parámetros de la consulta
                        cmd.Parameters.AddWithValue("@nacionalidad", nacionalidad);
                        cmd.Parameters.AddWithValue("@competicion", competicion);
                        cmd.Parameters.AddWithValue("@posicion", posicion);
                        cmd.Parameters.AddWithValue("@edadMin", edadMin);
                        cmd.Parameters.AddWithValue("@edadMax", edadMax);
                        cmd.Parameters.AddWithValue("@mediaMin", mediaMin);
                        cmd.Parameters.AddWithValue("@mediaMax", mediaMax);
                        cmd.Parameters.AddWithValue("@calidadMin", calidadMin);
                        cmd.Parameters.AddWithValue("@calidadMax", calidadMax);
                        cmd.Parameters.AddWithValue("@velocidadMin", velocidadMin);
                        cmd.Parameters.AddWithValue("@velocidadMax", velocidadMax);
                        cmd.Parameters.AddWithValue("@resistenciaMin", resistenciaMin);
                        cmd.Parameters.AddWithValue("@resistenciaMax", resistenciaMax);
                        cmd.Parameters.AddWithValue("@agresividadMin", agresividadMin);
                        cmd.Parameters.AddWithValue("@agresividadMax", agresividadMax);
                        cmd.Parameters.AddWithValue("@hoy", Metodos.hoy.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@equipo", equipo);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Jugador y asignar los valores de la base de datos
                                Jugador jugador = new Jugador
                                {
                                    IdJugador = dr.GetInt32(dr.GetOrdinal("id_jugador")),
                                    Nombre = dr.GetString(dr.GetOrdinal("nombre")),
                                    Apellido = dr.GetString(dr.GetOrdinal("apellido")),
                                    Peso = dr.GetInt32(dr.GetOrdinal("peso")),
                                    Altura = dr.GetInt32(dr.GetOrdinal("altura")),
                                    Nacionalidad = dr.GetString(dr.GetOrdinal("nacionalidad")),
                                    Dorsal = dr.GetInt32(dr.GetOrdinal("dorsal")),
                                    FechaNacimiento = DateTime.Parse(dr.GetString(dr.GetOrdinal("fecha_nacimiento"))),
                                    RolId = dr.GetInt32(dr.GetOrdinal("rol_id")),
                                    Rol = dr.GetString(dr.GetOrdinal("rol")),
                                    Velocidad = dr.GetInt32(dr.GetOrdinal("velocidad")),
                                    Resistencia = dr.GetInt32(dr.GetOrdinal("resistencia")),
                                    Agresividad = dr.GetInt32(dr.GetOrdinal("agresividad")),
                                    Calidad = dr.GetInt32(dr.GetOrdinal("calidad")),
                                    EstadoForma = dr.GetInt32(dr.GetOrdinal("estado_forma")),
                                    Moral = dr.GetInt32(dr.GetOrdinal("moral")),
                                    Potencial = dr.GetInt32(dr.GetOrdinal("potencial")),
                                    Portero = dr.GetInt32(dr.GetOrdinal("portero")),
                                    Pase = dr.GetInt32(dr.GetOrdinal("pase")),
                                    Regate = dr.GetInt32(dr.GetOrdinal("regate")),
                                    Remate = dr.GetInt32(dr.GetOrdinal("remate")),
                                    Entradas = dr.GetInt32(dr.GetOrdinal("entradas")),
                                    Tiro = dr.GetInt32(dr.GetOrdinal("tiro")),
                                    Lesion = dr.GetInt32(dr.GetOrdinal("lesion")),
                                    TipoLesion = dr.IsDBNull(dr.GetOrdinal("tipo_lesion")) ? null : dr.GetString(dr.GetOrdinal("tipo_lesion")),
                                    LesionTratada = dr.GetInt32(dr.GetOrdinal("lesion_tratada")),
                                    ValorMercado = dr.GetInt32(dr.GetOrdinal("valor_mercado")),
                                    EstadoAnimo = dr.GetInt32(dr.GetOrdinal("estado_animo")),
                                    RutaImagen = dr.GetString(dr.GetOrdinal("ruta_imagen")),
                                    SituacionMercado = dr.IsDBNull(dr.GetOrdinal("situacion_mercado")) ? 0 : dr.GetInt32(dr.GetOrdinal("situacion_mercado")),
                                    IdEquipo = dr.GetInt32(dr.GetOrdinal("id_equipo")),
                                    AniosContrato = dr.IsDBNull(dr.GetOrdinal("AniosContrato")) ? null : dr.GetInt32(dr.GetOrdinal("AniosContrato")),
                                    SalarioTemporada = dr.IsDBNull(dr.GetOrdinal("SalarioTemporada")) ? null : dr.GetInt32(dr.GetOrdinal("SalarioTemporada")),
                                    ClausulaRescision = dr.IsDBNull(dr.GetOrdinal("ClausulaRescision")) ? null : dr.GetInt32(dr.GetOrdinal("ClausulaRescision")),
                                    Status = dr.GetInt32(dr.GetOrdinal("status")),
                                    ProximaNegociacion = dr.IsDBNull(dr.GetOrdinal("proxima_negociacion")) ? (DateTime?)null : dr.GetDateTime(dr.GetOrdinal("proxima_negociacion"))
                                };

                                // Agregar el jugador a la lista
                                lista.Add(jugador);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // ---------------------------------------------------------------------- Método que resta un año de contrato
        public void RestarAnioContrato(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE contratos 
                                     SET duracion = duracion - 1, 
                                         fecha_fin = DATE(fecha_fin, '-1 year') 
                                     WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------------- Método que borra un contrato
        public void BorrarContrato(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"DELETE FROM contratos WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ======================================================= Método para mostrar todos los contratos de jugadores
        public List<Contrato> MostrarListaTotalContratos()
        {
            List<Contrato> lista = new List<Contrato>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta para obtener todos los jugadores del equipo con el id_equipo proporcionado
                    string query = @"SELECT * FROM contratos";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                // Crear un objeto Contrato
                                Contrato contrato = new Contrato
                                {
                                    IdContrato = Convert.ToInt32(dr["id_contrato"]),
                                    IdJugador = Convert.ToInt32(dr["id_jugador"]),
                                    IdEquipo = Convert.ToInt32(dr["id_equipo"]),
                                    SalarioAnual = Convert.ToInt32(dr["salario_anual"]),
                                    Duracion = Convert.ToInt32(dr["duracion"]),
                                    FechaInicio = dr["fecha_inicio"].ToString(),
                                    FechaFin = dr["fecha_fin"].ToString(),
                                    ClausulaRescision = Convert.ToInt32(dr["clausula_rescision"]),
                                    BonoPorGoles = Convert.ToInt32(dr["bono_por_goles"]),
                                    BonoPorPartidos = Convert.ToInt32(dr["bono_por_partidos"])
                                };

                                // Agregar el contrato a la lista
                                lista.Add(contrato);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // En caso de error, mostrar el mensaje con la excepción
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return lista;
        }

        // -------------------------------------------------------- Método para devuelve el salario medio de los jugadores con la misma media
        public int SalarioMedioJugadores(int jugador)
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"WITH media_objetivo AS (
                                    SELECT 
                                        (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                    FROM jugadores
                                    WHERE id_jugador = @IdJugador
                                 )
                                 SELECT 
                                    CAST((CAST(AVG(c.salario_anual) AS FLOAT) + 500) / 1000 AS INTEGER) * 1000 AS salario_medio
                                 FROM jugadores j
                                 JOIN contratos c ON j.id_jugador = c.id_jugador
                                 JOIN media_objetivo mo ON 
                                    ABS(((j.velocidad + j.resistencia + j.agresividad + j.calidad + j.estado_forma + j.moral) / 6.0) - mo.media) < 0.01";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // -------------------------------------------------------- Método para devuelve la clausula media de los jugadores con la misma media
        public int ClausulaMediaJugadores(int jugador)
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"WITH media_objetivo AS (
                                    SELECT 
                                        (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0 AS media
                                    FROM jugadores
                                    WHERE id_jugador = @IdJugador
                                 )
                                 SELECT 
                                    CAST((CAST(AVG(c.clausula_rescision) AS FLOAT) + 500) / 1000 AS INTEGER) * 1000 AS clausula_media
                                 FROM jugadores j
                                 JOIN contratos c ON j.id_jugador = c.id_jugador
                                 JOIN media_objetivo mo ON 
                                    ABS(((j.velocidad + j.resistencia + j.agresividad + j.calidad + j.estado_forma + j.moral) / 6.0) - mo.media) < 0.01";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // --------------------------------------------------------------------- Método que borra la Proxima Fecha de Negociacion
        public void BorrarFechaNegociacion(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET proxima_negociacion = NULL WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------- Método que suma una semana lesionado
        public void SumarSemanaLesionado(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE estadisticas_jugadores SET semanas_lesionado = semanas_lesionado + 1 WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // -------------------------------------------------------- Método para devuelve las semanas lesionado de un jugador
        public int SemanasLesionado(int jugador)
        {
            int num = 0;

            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT semanas_lesionado FROM estadisticas_jugadores WHERE id_jugador = @IdJugador";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdJugador", jugador);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            num = reader.GetInt32(0);
                        }
                    }
                }
            }

            return num;
        }

        // --------------------------------------------------------------------- Método que pone al jugador en tratamiento por lesion
        public void ActivarTratamientoLesion(int jugador, int valor)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET lesion_tratada = @Valor WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.Parameters.AddWithValue("@Valor", valor);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------- Método que pone un jugador Transferible
        public void PonerTransferible(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET situacion_mercado = 1 WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------- Método que pone un jugador Cedible
        public void PonerCedible(int jugador)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE jugadores SET situacion_mercado = 2 WHERE id_jugador = @IdJugador";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdJugador", jugador);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------- Método que realiza los traspasos de la IA
        public void TraspasosIA(int equipo)
        {
            using (var connection = new SQLiteConnection(Conexion.Cadena))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                var cmdEquipos = new SQLiteCommand("SELECT id_equipo FROM equipos WHERE id_equipo <> @idManagerEquipo AND id_equipo <> 0", connection, transaction);
                cmdEquipos.Parameters.AddWithValue("@idManagerEquipo", equipo);
                var readerEquipos = cmdEquipos.ExecuteReader();
                var rnd = new Random();

                var equiposIA = new List<int>();
                while (readerEquipos.Read())
                    equiposIA.Add(readerEquipos.GetInt32(0));
                readerEquipos.Close();

                foreach (int idEquipoIA in equiposIA)
                {
                    if (rnd.Next(0, 100) >= 40) continue;

                    // Obtener reputación del equipo IA
                    int reputacionEquipo;
                    var cmdReputacion = new SQLiteCommand("SELECT reputacion FROM equipos WHERE id_equipo = @idEquipoIA", connection, transaction);
                    cmdReputacion.Parameters.AddWithValue("@idEquipoIA", idEquipoIA);
                    reputacionEquipo = Convert.ToInt32(cmdReputacion.ExecuteScalar());

                    // Determinar media mínima exigida según la reputación
                    int mediaMaxima = reputacionEquipo switch
                                    {
                                        >= 90 => 99,
                                        >= 80 => 82,
                                        >= 70 => 78,
                                        >= 60 => 70,
                                        >= 50 => 60,
                                        _ => 50
                                    };

                    var necesidades = new Dictionary<int, int>
                                    {
                                        { 1, 3 },  { 2, 3 }, { 3, 3 }, { 4, 5 },
                                        { 5, 4 },  { 6, 4 }, { 7, 4 }, { 8, 4 },
                                        { 9, 4 }, { 10, 4 }
                                    };

                    var cmdConteo = new SQLiteCommand(@"SELECT rol_id, COUNT(*) 
                                                        FROM jugadores 
                                                        WHERE id_equipo = @idEquipo 
                                                        GROUP BY rol_id", connection, transaction);
                    cmdConteo.Parameters.AddWithValue("@idEquipo", idEquipoIA);

                    var actuales = new Dictionary<int, int>();
                    using (var lector = cmdConteo.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            int rol = lector.GetInt32(0);
                            int cantidad = lector.GetInt32(1);
                            actuales[rol] = cantidad;
                        }
                    }

                    int rolNecesitado = -1;
                    int maxDeficit = 0;
                    foreach (var kvp in necesidades)
                    {
                        int rol = kvp.Key;
                        int minimo = kvp.Value;
                        int tiene = actuales.ContainsKey(rol) ? actuales[rol] : 0;
                        int deficit = minimo - tiene;

                        if (deficit > maxDeficit)
                        {
                            maxDeficit = deficit;
                            rolNecesitado = rol;
                        }
                    }

                    if (rolNecesitado == -1) continue;

                    // Buscar un jugador en el mercado que cumpla la media mínima
                    var cmdObjetivos = new SQLiteCommand(@"SELECT id_jugador, id_equipo, valor_mercado 
                                                           FROM jugadores 
                                                           WHERE (situacion_mercado = 1 OR id_equipo = 0)
                                                              AND rol_id = @rolNecesitado 
                                                              AND (id_equipo != @equipoIA AND id_equipo != @idMiEquipo)
                                                              AND (
                                                                  (velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0
                                                              ) >= @mediaMaxima
                                                           ORDER BY valor_mercado ASC 
                                                           LIMIT 1", connection, transaction);
                    cmdObjetivos.Parameters.AddWithValue("@rolNecesitado", rolNecesitado);
                    cmdObjetivos.Parameters.AddWithValue("@equipoIA", idEquipoIA);
                    cmdObjetivos.Parameters.AddWithValue("@idMiEquipo", equipo);
                    cmdObjetivos.Parameters.AddWithValue("@mediaMaxima", mediaMaxima);

                    var readerObjetivo = cmdObjetivos.ExecuteReader();

                    if (readerObjetivo.Read())
                    {
                        int idJugador = readerObjetivo.GetInt32(0);
                        int idEquipoOrigen = readerObjetivo.GetInt32(1);
                        int valor = readerObjetivo.GetInt32(2);
                        readerObjetivo.Close();

                        // Comprobar presupuesto
                        var cmdPresupuesto = new SQLiteCommand("SELECT presupuesto FROM equipos WHERE id_equipo = @idEquipo", connection, transaction);
                        cmdPresupuesto.Parameters.AddWithValue("@idEquipo", idEquipoIA);
                        int presupuestoIA = Convert.ToInt32(cmdPresupuesto.ExecuteScalar());

                        if (valor > presupuestoIA)
                            continue;

                        int salario = valor / 10;
                        int duracion = rnd.Next(2, 5);
                        int clausula = valor * 2;

                        string fechaHoy = Metodos.hoy.ToString("yyyy-MM-dd");
                        string fechaFin = Metodos.hoy.AddYears(duracion).ToString("yyyy-MM-dd");

                        var updateContratoAnterior = new SQLiteCommand(@"UPDATE contratos 
                                                                         SET fecha_fin = @fechaFinReal 
                                                                         WHERE id_jugador = @idJugador AND fecha_fin > @fechaFinReal", connection, transaction);
                        updateContratoAnterior.Parameters.AddWithValue("@fechaFinReal", fechaHoy);
                        updateContratoAnterior.Parameters.AddWithValue("@idJugador", idJugador);
                        updateContratoAnterior.ExecuteNonQuery();

                        var insertTransferencia = new SQLiteCommand(@"INSERT INTO transferencias 
                                                                      (id_jugador, id_equipo_origen, id_equipo_destino, tipo_fichaje, monto_oferta, fecha_oferta, fecha_traspaso, salario_anual, duracion, clausula_rescision, respuesta_equipo, respuesta_jugador)
                                                                      VALUES (@idJugador, @origen, @destino, 1, @monto, @fecha, @fecha, @salario, @duracion, @clausula, 1, 1)", connection, transaction);
                        insertTransferencia.Parameters.AddWithValue("@idJugador", idJugador);
                        insertTransferencia.Parameters.AddWithValue("@origen", idEquipoOrigen);
                        insertTransferencia.Parameters.AddWithValue("@destino", idEquipoIA);
                        insertTransferencia.Parameters.AddWithValue("@monto", valor);
                        insertTransferencia.Parameters.AddWithValue("@fecha", fechaHoy);
                        insertTransferencia.Parameters.AddWithValue("@salario", salario);
                        insertTransferencia.Parameters.AddWithValue("@duracion", duracion);
                        insertTransferencia.Parameters.AddWithValue("@clausula", clausula);
                        insertTransferencia.ExecuteNonQuery();

                        var updateJugador = new SQLiteCommand(@"UPDATE jugadores 
                                                                SET id_equipo = @nuevoEquipo, situacion_mercado = 0 
                                                                WHERE id_jugador = @idJugador", connection, transaction);
                        updateJugador.Parameters.AddWithValue("@nuevoEquipo", idEquipoIA);
                        updateJugador.Parameters.AddWithValue("@idJugador", idJugador);
                        updateJugador.ExecuteNonQuery();

                        var deleteContrato = new SQLiteCommand(@"DELETE FROM contratos WHERE id_jugador = @idJugador", connection, transaction);
                        deleteContrato.Parameters.AddWithValue("@idJugador", idJugador);
                        deleteContrato.ExecuteNonQuery();

                        var insertContrato = new SQLiteCommand(@"INSERT INTO contratos 
                                                                 (id_jugador, id_equipo, salario_anual, duracion, fecha_inicio, fecha_fin, clausula_rescision)
                                                                 VALUES (@idJugador, @idEquipo, @salario, @duracion, @fechaInicio, @fechaFin, @clausula)", connection, transaction);
                        insertContrato.Parameters.AddWithValue("@idJugador", idJugador);
                        insertContrato.Parameters.AddWithValue("@idEquipo", idEquipoIA);
                        insertContrato.Parameters.AddWithValue("@salario", salario);
                        insertContrato.Parameters.AddWithValue("@duracion", duracion);
                        insertContrato.Parameters.AddWithValue("@fechaInicio", fechaHoy);
                        insertContrato.Parameters.AddWithValue("@fechaFin", fechaFin);
                        insertContrato.Parameters.AddWithValue("@clausula", clausula);
                        insertContrato.ExecuteNonQuery();

                        var updatePresupuestoComprador = new SQLiteCommand("UPDATE equipos SET presupuesto = presupuesto - @valor WHERE id_equipo = @idEquipo", connection, transaction);
                        updatePresupuestoComprador.Parameters.AddWithValue("@valor", valor);
                        updatePresupuestoComprador.Parameters.AddWithValue("@idEquipo", idEquipoIA);
                        updatePresupuestoComprador.ExecuteNonQuery();

                        var updatePresupuestoVendedor = new SQLiteCommand("UPDATE equipos SET presupuesto = presupuesto + @valor WHERE id_equipo = @idEquipo", connection, transaction);
                        updatePresupuestoVendedor.Parameters.AddWithValue("@valor", valor);
                        updatePresupuestoVendedor.Parameters.AddWithValue("@idEquipo", idEquipoOrigen);
                        updatePresupuestoVendedor.ExecuteNonQuery();
                    }
                    else
                    {
                        readerObjetivo.Close();
                    }
                }

                transaction.Commit();
                connection.Close();
            }
        }

        // --------------------------------------------------------------------- Método que comprueba si hay equipos que quieran hacerme ofertas
        public List<OfertaIA> OfertasMiEquipo(int miEquipo)
        {
            var ofertas = new List<OfertaIA>();

            using (var connection = new SQLiteConnection(Conexion.Cadena))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                var cmdEquipos = new SQLiteCommand("SELECT id_equipo FROM equipos WHERE id_equipo <> @idManagerEquipo AND id_equipo <> 0", connection, transaction);
                cmdEquipos.Parameters.AddWithValue("@idManagerEquipo", miEquipo);
                var readerEquipos = cmdEquipos.ExecuteReader();
                var rnd = new Random();

                var equiposIA = new List<int>();
                while (readerEquipos.Read())
                    equiposIA.Add(readerEquipos.GetInt32(0));
                readerEquipos.Close();

                foreach (int idEquipoIA in equiposIA)
                {
                    if (rnd.Next(0, 100) >= 50) continue;

                    // Obtener reputación del equipo IA
                    var cmdReputacion = new SQLiteCommand("SELECT reputacion FROM equipos WHERE id_equipo = @idEquipoIA", connection, transaction);
                    cmdReputacion.Parameters.AddWithValue("@idEquipoIA", idEquipoIA);
                    int reputacionEquipo = Convert.ToInt32(cmdReputacion.ExecuteScalar());

                    int mediaMaxima = reputacionEquipo switch
                                    {
                                        >= 90 => 99,
                                        >= 80 => 82,
                                        >= 70 => 78,
                                        >= 60 => 70,
                                        >= 50 => 60,
                                        _ => 50
                                    };

                    var necesidades = new Dictionary<int, int>
                                    {
                                        { 1, 2 }, { 2, 2 }, { 3, 2 }, { 4, 4 },
                                        { 5, 2 }, { 6, 2 }, { 7, 2 }, { 8, 2 },
                                        { 9, 2 }, { 10, 2 }
                                    };

                    var cmdConteo = new SQLiteCommand(@"SELECT rol_id, COUNT(*) 
                                                        FROM jugadores 
                                                        WHERE id_equipo = @idEquipo 
                                                        GROUP BY rol_id", connection, transaction);
                    cmdConteo.Parameters.AddWithValue("@idEquipo", idEquipoIA);

                    var actuales = new Dictionary<int, int>();
                    using (var lector = cmdConteo.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            int rol = lector.GetInt32(0);
                            int cantidad = lector.GetInt32(1);
                            actuales[rol] = cantidad;
                        }
                    }

                    int rolNecesitado = -1;
                    int maxDeficit = 0;
                    foreach (var kvp in necesidades)
                    {
                        int rol = kvp.Key;
                        int minimo = kvp.Value;
                        int tiene = actuales.ContainsKey(rol) ? actuales[rol] : 0;
                        int deficit = minimo - tiene;

                        if (deficit > maxDeficit)
                        {
                            maxDeficit = deficit;
                            rolNecesitado = rol;
                        }
                    }

                    if (rolNecesitado == -1) continue;

                    // Buscar jugador disponible y con media suficiente
                    var cmdObjetivos = new SQLiteCommand(@"SELECT id_jugador 
                                                           FROM jugadores 
                                                           WHERE (situacion_mercado = 1 OR situacion_mercado = 2)
                                                              AND rol_id = @rolNecesitado 
                                                              AND id_equipo = @idMiEquipo
                                                              AND ((velocidad + resistencia + agresividad + calidad + estado_forma + moral) / 6.0) <= @mediaMaxima
                                                              AND NOT EXISTS (
                                                                  SELECT 1 
                                                                  FROM ofertasRecibidas 
                                                                  WHERE ofertasRecibidas.id_jugador_interesado = jugadores.id_jugador
                                                           )
                                                           ORDER BY valor_mercado ASC 
                                                           LIMIT 1", connection, transaction);

                    cmdObjetivos.Parameters.AddWithValue("@rolNecesitado", rolNecesitado);
                    cmdObjetivos.Parameters.AddWithValue("@idMiEquipo", miEquipo);
                    cmdObjetivos.Parameters.AddWithValue("@mediaMaxima", mediaMaxima);

                    var readerObjetivo = cmdObjetivos.ExecuteReader();

                    if (readerObjetivo.Read())
                    {
                        int idJugador = readerObjetivo.GetInt32(0);
                        ofertas.Add(new OfertaIA
                        {
                            IdEquipoInteresado = idEquipoIA,
                            IdJugadorObjetivo = idJugador
                        });
                    }
                    readerObjetivo.Close();
                }

                transaction.Commit();
                connection.Close();
            }

            return ofertas;
        }

        // ---------------------------------------------------------------- Método para decir si tengo portero en la alineacion titular
        public bool TengoPortero(int equipo)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM alineacion a
                                 JOIN jugadores j ON a.id_jugador = j.id_jugador
                                 WHERE a.posicion BETWEEN 1 AND 11
                                      AND j.rol_id = 1
                                      AND j.id_equipo = @equipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@equipo", equipo);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // ---------------------------------------------------------------- Método para decir si tengo 4 defensas en el 11 titular
        public bool TengoDefensas(int equipo)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM alineacion a
                                 JOIN jugadores j ON a.id_jugador = j.id_jugador
                                 WHERE a.posicion BETWEEN 1 AND 11
                                      AND j.rol_id BETWEEN 2 AND 4
                                      AND j.id_equipo = @equipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@equipo", equipo);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 3;
                }
            }
        }

        // ---------------------------------------------------------------- Método para decir cuantos si tengo 1 delantero en el 11 titular
        public bool TengoDelanteros(int equipo)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM alineacion a
                                 JOIN jugadores j ON a.id_jugador = j.id_jugador
                                 WHERE a.posicion BETWEEN 1 AND 11
                                      AND j.rol_id = 10
                                      AND j.id_equipo = @equipo";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@equipo", equipo);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }
    }
}
