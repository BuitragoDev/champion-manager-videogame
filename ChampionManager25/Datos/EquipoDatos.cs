﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using ChampionManager25.Datos;
using ChampionManager25.Entidades;

namespace ChampionManager25.Datos
{
    public class EquipoDatos : Conexion
    {
        // ---------------------------------------------------------------------- Método para Mostrar los equipos de una competición
        public List<Equipo> ListarEquiposCompeticion(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT 
                    e.id_equipo,
                    e.nombre,
                    e.nombre_corto,
                    e.presidente,
                    e.ciudad,
                    e.estadio,
                    e.objetivo,
                    e.aforo,
                    e.reputacion,
                    e.rival,
                    e.competicion_europea,
                    t.nombre || ' ' || t.apellido AS entrenador,
                    t.reputacion AS reputacion_entrenador,
                    e.ruta_imagen, e.ruta_imagen120, e.ruta_imagen80, e.ruta_imagen64, e.ruta_imagen32, 
                    e.ruta_estadio_interior, e.ruta_estadio_exterior, 
                    e.ruta_kit_local, e.ruta_kit_visitante
                FROM 
                    equipos e
                LEFT JOIN 
                    entrenadores t
                ON 
                    e.id_equipo = t.id_equipo
                WHERE 
                    e.id_competicion = @idCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                    Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                    Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                    Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                    Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                    Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                    Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                    Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                    CompeticionEuropea = int.TryParse(dr["competicion_europea"]?.ToString(), out int competicionEuropea) ? competicionEuropea : 0,
                                    Entrenador = dr["entrenador"] as string ?? "Sin asignar",
                                    ReputacionEntrenador = dr["reputacion_entrenador"] != DBNull.Value ? Convert.ToInt32(dr["reputacion_entrenador"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }

        // ===================================================================== Método para Mostrar los datos de un EQUIPO por su ID
        public Equipo ListarDetallesEquipo(int id)
        {
            Equipo oEquipo = new Equipo();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT id_equipo, nombre, nombre_corto, presidente, presupuesto, ciudad, estadio, aforo, reputacion, objetivo, rival, id_competicion, competicion_europea,
                                            ruta_imagen, ruta_imagen120, ruta_imagen80, ruta_imagen64, ruta_imagen32, 
                                            ruta_estadio_interior, ruta_estadio_exterior, ruta_kit_local, ruta_kit_visitante
                                     FROM equipos WHERE id_equipo = @idEquipo";

                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idEquipo", id);

                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            oEquipo = new Equipo()
                            {
                                // Usamos el operador de coalescencia nula para evitar la asignación de null
                                IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                Presupuesto = int.TryParse(dr["presupuesto"]?.ToString(), out int presupuesto) ? presupuesto : 0,
                                Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                IdCompeticion = dr["id_competicion"] != DBNull.Value ? Convert.ToInt32(dr["id_competicion"]) : 0,
                                CompeticionEuropea = dr["competicion_europea"] != DBNull.Value ? Convert.ToInt32(dr["competicion_europea"]) : 0,
                                RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                            };
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
            return oEquipo;
        }

        // ===================================================================== Método para Mostrar el Listado de todos los equipos
        public List<Equipo> ListarEquipos(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta con JOIN para obtener datos de entrenadores
                    string query = @"SELECT id_equipo, nombre, nombre_corto, presidente, ciudad, estadio, objetivo,
                                            aforo, reputacion, rival, competicion_europea, ruta_imagen, ruta_imagen120, ruta_imagen80, ruta_imagen64, 
                                            ruta_imagen32, ruta_estadio_interior, ruta_estadio_exterior, ruta_kit_local, ruta_kit_visitante
                                     FROM 
                                        equipos
                                     WHERE 
                                        id_competicion = @IdCompeticion";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"] as string ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"] as string ?? string.Empty,
                                    Presidente = dr["presidente"] as string ?? string.Empty,
                                    Ciudad = dr["ciudad"] as string ?? string.Empty,
                                    Estadio = dr["estadio"] as string ?? string.Empty,
                                    Objetivo = dr["objetivo"] as string ?? string.Empty,
                                    Aforo = dr["aforo"] != DBNull.Value ? Convert.ToInt32(dr["aforo"]) : 0,
                                    Reputacion = dr["reputacion"] != DBNull.Value ? Convert.ToInt32(dr["reputacion"]) : 0,
                                    Rival = dr["rival"] != DBNull.Value ? Convert.ToInt32(dr["rival"]) : 0,
                                    CompeticionEuropea = dr["competicion_europea"] != DBNull.Value ? Convert.ToInt32(dr["competicion_europea"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }

                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }

        // ===================================================================== Método que devuelve la asistencia a un partido
        public int CalcularAsistencia(int idEquipoLocal)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Obtener aforo y reputación
                    string queryEquipo = "SELECT aforo, reputacion FROM equipos WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmdEquipo = new SQLiteCommand(queryEquipo, conn))
                    {
                        cmdEquipo.Parameters.AddWithValue("@idEquipo", idEquipoLocal);

                        using (SQLiteDataReader reader = cmdEquipo.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int aforo = Convert.ToInt32(reader["aforo"]);
                                int reputacion = Convert.ToInt32(reader["reputacion"]);

                                // Ajustar aforo por remodelaciones
                                string queryRemodelacion = "SELECT COUNT(*) FROM remodelaciones WHERE id_equipo = @idEquipo";
                                using (SQLiteCommand remodelacionCmd = new SQLiteCommand(queryRemodelacion, conn))
                                {
                                    remodelacionCmd.Parameters.AddWithValue("@idEquipo", idEquipoLocal);
                                    int remodelaciones = Convert.ToInt32(remodelacionCmd.ExecuteScalar());

                                    if (remodelaciones > 0)
                                        aforo = (int)(aforo * 0.90); // Reducción del 10%
                                }

                                // Precios por defecto
                                int precioGeneral = 80;
                                int precioTribuna = 100;
                                int precioVIP = 150;

                                // Obtener precios de taquilla
                                string queryTaquilla = @"SELECT precio_entrada_general, precio_entrada_tribuna, precio_entrada_vip 
                                                         FROM taquilla 
                                                         WHERE id_equipo = @idEquipo";

                                using (SQLiteCommand cmdTaquilla = new SQLiteCommand(queryTaquilla, conn))
                                {
                                    cmdTaquilla.Parameters.AddWithValue("@idEquipo", idEquipoLocal);

                                    using (SQLiteDataReader readerTaquilla = cmdTaquilla.ExecuteReader())
                                    {
                                        if (readerTaquilla.Read())
                                        {
                                            precioGeneral = Convert.ToInt32(readerTaquilla["precio_entrada_general"]);
                                            precioTribuna = Convert.ToInt32(readerTaquilla["precio_entrada_tribuna"]);
                                            precioVIP = Convert.ToInt32(readerTaquilla["precio_entrada_vip"]);
                                        }
                                        else
                                        {
                                            // Insertar precios por defecto si no existen
                                            string insertTaquilla = @"INSERT INTO taquilla (id_equipo, precio_entrada_general, precio_entrada_tribuna, precio_entrada_vip) 
                                                                      VALUES (@idEquipo, @precioGeneral, @precioTribuna, @precioVIP)";

                                            using (SQLiteCommand insertCmd = new SQLiteCommand(insertTaquilla, conn))
                                            {
                                                insertCmd.Parameters.AddWithValue("@idEquipo", idEquipoLocal);
                                                insertCmd.Parameters.AddWithValue("@precioGeneral", precioGeneral);
                                                insertCmd.Parameters.AddWithValue("@precioTribuna", precioTribuna);
                                                insertCmd.Parameters.AddWithValue("@precioVIP", precioVIP);
                                                insertCmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                // Calcular asistencia base
                                double asistenciaBase = aforo * (reputacion / 100.0);

                                // Penalización por precios altos (cada 10€ por encima de 80, se resta un 3%)
                                double penalizacion = Math.Max(0, ((precioGeneral - 80) / 10.0) * 0.03);
                                asistenciaBase *= (1 - penalizacion);

                                // Asegurar un mínimo del 20% de aforo
                                double asistenciaMinima = aforo * 0.20;
                                if (asistenciaBase < asistenciaMinima)
                                    asistenciaBase = asistenciaMinima;

                                // Variación aleatoria positiva hasta 10%
                                Random rand = new Random();
                                double variacion = rand.NextDouble() * 0.10;
                                double asistenciaFinal = asistenciaBase * (1 + variacion);

                                // Límite superior al aforo
                                asistenciaFinal = Math.Min(asistenciaFinal, aforo);

                                return (int)Math.Round(asistenciaFinal);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al calcular asistencia: {ex.Message}");
            }

            return 0;
        }


        /*public int CalcularAsistencia(int idEquipoLocal)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta
                    string query = "SELECT aforo, reputacion FROM equipos WHERE id_equipo = @idEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idEquipo", idEquipoLocal);

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int aforo = Convert.ToInt32(reader["aforo"]);
                                int reputacion = Convert.ToInt32(reader["reputacion"]);

                                // Calculamos la asistencia base
                                double asistenciaBase = aforo * (reputacion / 100.0);

                                // Añadimos una variación aleatoria, pero sin que sobrepase el aforo
                                Random rand = new Random();
                                double maxVariacion = 0.10; // Variación máxima permitida, hasta un 10% del aforo
                                double variacion = rand.NextDouble() * maxVariacion; // Variación positiva entre 0% y 10%

                                // La asistencia no puede ser mayor que el aforo
                                double asistenciaFinal = asistenciaBase * (1 + variacion);

                                // Aseguramos que no se exceda el aforo
                                if (asistenciaFinal > aforo)
                                {
                                    asistenciaFinal = aforo;
                                }

                                // Redondeamos y devolvemos el valor final
                                return (int)Math.Round(asistenciaFinal);
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return 0;
        }*/

        // ==================================================== Método para Mostrar el Listado de todos los equipos excepto el elegido
        public List<Equipo> ListarOtrosEquipos(int equipoElegido)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta con JOIN para obtener datos de entrenadores
                    string query = @"SELECT id_equipo, nombre, nombre_corto, presidente, ciudad, estadio, objetivo,
                                            aforo, reputacion, rival, competicion_europea, ruta_imagen, ruta_imagen120, ruta_imagen80, ruta_imagen64, 
                                            ruta_imagen32, ruta_estadio_interior, ruta_estadio_exterior, ruta_kit_local, ruta_kit_visitante
                                     FROM 
                                        equipos
                                     WHERE 
                                        id_equipo <> @EquipoElegido AND id_competicion BETWEEN 1 AND 2";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@EquipoElegido", equipoElegido);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"] as string ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"] as string ?? string.Empty,
                                    Presidente = dr["presidente"] as string ?? string.Empty,
                                    Ciudad = dr["ciudad"] as string ?? string.Empty,
                                    Estadio = dr["estadio"] as string ?? string.Empty,
                                    Objetivo = dr["objetivo"] as string ?? string.Empty,
                                    Aforo = dr["aforo"] != DBNull.Value ? Convert.ToInt32(dr["aforo"]) : 0,
                                    Reputacion = dr["reputacion"] != DBNull.Value ? Convert.ToInt32(dr["reputacion"]) : 0,
                                    Rival = dr["rival"] != DBNull.Value ? Convert.ToInt32(dr["rival"]) : 0,
                                    CompeticionEuropea = dr["competicion_europea"] != DBNull.Value ? Convert.ToInt32(dr["competicion_europea"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }

                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }

        // ==================================================== Método para Mostrar el Listado de todos los equipos
        public List<Equipo> ListarTodosLosEquipos()
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta con JOIN para obtener datos de entrenadores
                    string query = @"SELECT id_equipo, id_competicion, nombre, nombre_corto, presidente, ciudad, estadio, objetivo,
                                            aforo, reputacion, rival, competicion_europea, ruta_imagen, ruta_imagen120, ruta_imagen80, ruta_imagen64, 
                                            ruta_imagen32, ruta_estadio_interior, ruta_estadio_exterior, ruta_kit_local, ruta_kit_visitante
                                     FROM 
                                        equipos
                                     WHERE 
                                        id_competicion BETWEEN 1 AND 3";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    IdCompeticion = dr["id_competicion"] != DBNull.Value ? Convert.ToInt32(dr["id_competicion"]) : 0,
                                    Nombre = dr["nombre"] as string ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"] as string ?? string.Empty,
                                    Presidente = dr["presidente"] as string ?? string.Empty,
                                    Ciudad = dr["ciudad"] as string ?? string.Empty,
                                    Estadio = dr["estadio"] as string ?? string.Empty,
                                    Objetivo = dr["objetivo"] as string ?? string.Empty,
                                    Aforo = dr["aforo"] != DBNull.Value ? Convert.ToInt32(dr["aforo"]) : 0,
                                    Reputacion = dr["reputacion"] != DBNull.Value ? Convert.ToInt32(dr["reputacion"]) : 0,
                                    Rival = dr["rival"] != DBNull.Value ? Convert.ToInt32(dr["rival"]) : 0,
                                    CompeticionEuropea = dr["competicion_europea"] != DBNull.Value ? Convert.ToInt32(dr["competicion_europea"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }

                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }

        // ---------------------------------------------------------------- Método que actualiza los detalles de un equipo
        public void EditarEquipo(Equipo equipo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Campos obligatorios
                    string query = @"UPDATE equipos SET nombre = @Nombre, nombre_corto = @NombreCorto,
                                               presidente = @Presidente, ciudad = @Ciudad,
                                               estadio = @Estadio, aforo = @Aforo,
                                               reputacion = @Reputacion, objetivo = @Objetivo,
                                               rival = @Rival";

                    // Lista de parámetros dinámicos para campos de rutas
                    List<string> camposOpcionales = new List<string>();
                    if (equipo.RutaImagen != "Recursos/img/escudos_equipos/") camposOpcionales.Add("ruta_imagen = @RutaImagen");
                    if (equipo.RutaImagen120 != "Recursos/img/escudos_equipos/120x120/") camposOpcionales.Add("ruta_imagen120 = @RutaImagen120");
                    if (equipo.RutaImagen80 != "Recursos/img/escudos_equipos/80x80/") camposOpcionales.Add("ruta_imagen80 = @RutaImagen80");
                    if (equipo.RutaImagen64 != "Recursos/img/escudos_equipos/64x64/") camposOpcionales.Add("ruta_imagen64 = @RutaImagen64");
                    if (equipo.RutaImagen32 != "Recursos/img/escudos_equipos/32x32/") camposOpcionales.Add("ruta_imagen32 = @RutaImagen32");
                    if (equipo.RutaEstadioExterior != "Recursos/img/estadios/") camposOpcionales.Add("ruta_estadio_exterior = @RutaEstadioExterior");
                    if (equipo.RutaEstadioInterior != "Recursos/img/estadios/") camposOpcionales.Add("ruta_estadio_interior = @RutaEstadioInterior");
                    if (equipo.RutaKitLocal != "Recursos/img/kits/") camposOpcionales.Add("ruta_kit_local = @RutaKitLocal");
                    if (equipo.RutaKitVisitante != "Recursos/img/kits/") camposOpcionales.Add("ruta_kit_visitante = @RutaKitVisitante");

                    // Agrega los campos opcionales al query si existen
                    if (camposOpcionales.Count > 0)
                    {
                        query += ", " + string.Join(", ", camposOpcionales);
                    }

                    query += " WHERE id_equipo = @IdEquipo";
                 
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        // Parámetros
                        cmd.Parameters.AddWithValue("@Nombre", equipo.Nombre);
                        cmd.Parameters.AddWithValue("@NombreCorto", equipo.NombreCorto);
                        cmd.Parameters.AddWithValue("@Presidente", equipo.Presidente);
                        cmd.Parameters.AddWithValue("@Ciudad", equipo.Ciudad);
                        cmd.Parameters.AddWithValue("@Estadio", equipo.Estadio);
                        cmd.Parameters.AddWithValue("@Aforo", equipo.Aforo);
                        cmd.Parameters.AddWithValue("@Reputacion", equipo.Reputacion);
                        cmd.Parameters.AddWithValue("@Objetivo", equipo.Objetivo);
                        cmd.Parameters.AddWithValue("@Rival", equipo.Rival);

                        cmd.Parameters.AddWithValue("@RutaImagen", equipo.RutaImagen);
                        cmd.Parameters.AddWithValue("@RutaImagen120", equipo.RutaImagen120);
                        cmd.Parameters.AddWithValue("@RutaImagen80", equipo.RutaImagen80);
                        cmd.Parameters.AddWithValue("@RutaImagen64", equipo.RutaImagen64);
                        cmd.Parameters.AddWithValue("@RutaImagen32", equipo.RutaImagen32);

                        cmd.Parameters.AddWithValue("@RutaEstadioExterior", equipo.RutaEstadioExterior);
                        cmd.Parameters.AddWithValue("@RutaEstadioInterior", equipo.RutaEstadioInterior);

                        cmd.Parameters.AddWithValue("@RutaKitLocal", equipo.RutaKitLocal);
                        cmd.Parameters.AddWithValue("@RutaKitVisitante", equipo.RutaKitVisitante);

                        cmd.Parameters.AddWithValue("@IdEquipo", equipo.IdEquipo);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el equipo: " + ex.Message);
            }
        }

        // ---------------------------------------------------------------- Método que cambia un equipo de competicion
        public void AscenderDescenderEquipo(int equipo, int competicion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE equipos
                                     SET id_competicion = @IdCompeticion
                                     WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);
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

        // ---------------------------------------------------------------- Método que cambia un equipo de competicion europea
        public void AscenderDescenderEquipoEuropa(int equipo, int competicion)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE equipos
                                     SET competicion_europea = @IdCompeticion
                                     WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdCompeticion", competicion);
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

        // ---------------------------------------------------------------- Método que cambia un Objetivo de Temporada
        public void CambiarObjetivoTemporada(int equipo, string objetivo)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE equipos
                                     SET objetivo = @Objetivo
                                     WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Objetivo", objetivo);
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

        // ---------------------------------------------------------------- Método que resta una cantidad al Presupuesto
        public void RestarCantidadAPresupuesto(int equipo, int cantidad)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE equipos SET presupuesto = presupuesto - @Cantidad WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------- Método que suma una cantidad al Presupuesto
        public void SumarCantidadAPresupuesto(int equipo, int cantidad)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Consulta SQL para obtener las finanzas del equipo
                    string query = @"UPDATE equipos SET presupuesto = presupuesto + @Cantidad WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }
        }

        // ---------------------------------------------------------------- Método que actualiza el aforo de un estadio
        public void ActualizarAforo(int equipo, int aumento)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();

                    // Campos obligatorios
                    string query = @"UPDATE equipos SET aforo = aforo + @Cantidad WHERE id_equipo = @IdEquipo";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        // Agregar parámetro para evitar inyección SQL
                        cmd.Parameters.AddWithValue("@IdEquipo", equipo);
                        cmd.Parameters.AddWithValue("@Cantidad", aumento);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el equipo: " + ex.Message);
            }
        }

        // ---------------------------------------------------------------------- Método para Mostrar los equipos que juega Europa 1
        public List<Equipo> EquiposJueganEuropa1(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT e.*, en.nombre AS nombre_entrenador, en.reputacion AS reputacion_entrenador
                                     FROM equipos e
                                     LEFT JOIN entrenadores en ON e.id_equipo = en.id_equipo
                                     WHERE e.id_competicion = @idCompeticion AND e.competicion_europea = 5";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                    Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                    Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                    Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                    Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                    Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                    Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                    Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                    CompeticionEuropea = int.TryParse(dr["competicion_europea"]?.ToString(), out int competicionEuropea) ? competicionEuropea : 0,
                                    Entrenador = dr["nombre_entrenador"] as string ?? "Sin asignar",
                                    ReputacionEntrenador = dr["reputacion_entrenador"] != DBNull.Value ? Convert.ToInt32(dr["reputacion_entrenador"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }

        // ---------------------------------------------------------------------- Método para Mostrar los equipos que juega Europa 2
        public List<Equipo> EquiposJueganEuropa2(int competicion)
        {
            List<Equipo> oEquipo = new List<Equipo>();

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(Conexion.Cadena))
                {
                    conn.Open();
                    string query = @"SELECT e.*, en.nombre AS nombre_entrenador, en.reputacion AS reputacion_entrenador
                                     FROM equipos e
                                     LEFT JOIN entrenadores en ON e.id_equipo = en.id_equipo
                                     WHERE e.id_competicion = @idCompeticion AND e.competicion_europea = 6";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idCompeticion", competicion);

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                oEquipo.Add(new Equipo()
                                {
                                    // Usamos el operador de coalescencia nula para evitar la asignación de null
                                    IdEquipo = dr["id_equipo"] != DBNull.Value ? Convert.ToInt32(dr["id_equipo"]) : 0,
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    NombreCorto = dr["nombre_corto"]?.ToString() ?? string.Empty,
                                    Presidente = dr["presidente"]?.ToString() ?? string.Empty,
                                    Ciudad = dr["ciudad"]?.ToString() ?? string.Empty,
                                    Estadio = dr["estadio"]?.ToString() ?? string.Empty,
                                    Aforo = int.TryParse(dr["aforo"]?.ToString(), out int capacidad) ? capacidad : 0,
                                    Reputacion = int.TryParse(dr["reputacion"]?.ToString(), out int reputacion) ? reputacion : 0,
                                    Objetivo = dr["objetivo"]?.ToString() ?? string.Empty,
                                    Rival = int.TryParse(dr["rival"]?.ToString(), out int rival) ? rival : 0,
                                    CompeticionEuropea = int.TryParse(dr["competicion_europea"]?.ToString(), out int competicionEuropea) ? competicionEuropea : 0,
                                    Entrenador = dr["nombre_entrenador"] as string ?? "Sin asignar",
                                    ReputacionEntrenador = dr["reputacion_entrenador"] != DBNull.Value ? Convert.ToInt32(dr["reputacion_entrenador"]) : 0,
                                    RutaImagen = dr["ruta_imagen"]?.ToString() ?? string.Empty,
                                    RutaImagen120 = dr["ruta_imagen120"]?.ToString() ?? string.Empty,
                                    RutaImagen80 = dr["ruta_imagen80"]?.ToString() ?? string.Empty,
                                    RutaImagen64 = dr["ruta_imagen64"]?.ToString() ?? string.Empty,
                                    RutaImagen32 = dr["ruta_imagen32"]?.ToString() ?? string.Empty,
                                    RutaEstadioInterior = dr["ruta_estadio_interior"]?.ToString() ?? string.Empty,
                                    RutaEstadioExterior = dr["ruta_estadio_exterior"]?.ToString() ?? string.Empty,
                                    RutaKitLocal = dr["ruta_kit_local"]?.ToString() ?? string.Empty,
                                    RutaKitVisitante = dr["ruta_kit_visitante"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return oEquipo;
        }
    }
}
