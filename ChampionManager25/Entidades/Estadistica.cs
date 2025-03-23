﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Estadistica
    {
        // Atributos
        public int IdEstadistica { get; set; }
        public int IdJugador { get; set; }
        public int PartidosJugados { get; set; } = 0;
        public int Goles { get; set; } = 0;
        public int Asistencias { get; set; } = 0;
        public int TarjetasAmarillas { get; set; } = 0;
        public int TarjetasRojas { get; set; } = 0;
        public int MVP { get; set; } = 0;
        public int IdManager { get; set; }

        // Atributos extra
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NombreCompleto => $"{Nombre} {Apellido}";
        public string? Nacionalidad { get; set; }
        public int? Dorsal { get; set; }
        public int? RolId { get; set; }
        public int? IdEquipo { get; set; }


        // Constructor vacío
        public Estadistica() { }

        // Constructor con parámetros
        public Estadistica(int idEstadistica, int idJugador, int partidosJugados, int goles, int asistencias,
                           int tarjetasAmarillas, int tarjetasRojas, int mvp, int idManager)
        {
            IdEstadistica = idEstadistica;
            IdJugador = idJugador;
            PartidosJugados = partidosJugados;
            Goles = goles;
            Asistencias = asistencias;
            TarjetasAmarillas = tarjetasAmarillas;
            TarjetasRojas = tarjetasRojas;
            MVP = mvp;
            IdManager = idManager;
        }

        // Constructor con parámetros SIN ID
        public Estadistica(int idJugador, int partidosJugados, int goles, int asistencias,
                           int tarjetasAmarillas, int tarjetasRojas, int mvp, int idManager)
        {
            IdJugador = idJugador;
            PartidosJugados = partidosJugados;
            Goles = goles;
            Asistencias = asistencias;
            TarjetasAmarillas = tarjetasAmarillas;
            TarjetasRojas = tarjetasRojas;
            MVP = mvp;
            IdManager = idManager;
        }

        public override string ToString()
        {
            return $"ID: {IdEstadistica}, Jugador: {IdJugador}, Partidos: {PartidosJugados}, " +
                   $"Goles: {Goles}, Asistencias: {Asistencias}, Amarillas: {TarjetasAmarillas}, " +
                   $"Rojas: {TarjetasRojas}, MVP: {MVP}";
        }
    }
}
