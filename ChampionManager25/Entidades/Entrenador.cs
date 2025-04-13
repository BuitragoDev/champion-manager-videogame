﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Entrenador
    {
        // Atributos
        public int IdEntrenador { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public int Reputacion { get; set; }
        public int Puntos { get; set; }
        public int? IdEquipo { get; set; }
        public string TacticaFavorita { get; set; }
        public string RutaImagen { get; set; }

        // Atributos extras
        public int Posicion { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NombreEquipo { get; set; }

        // Constructor sin parámetros
        public Entrenador() { }

        // Constructor con todos los parámetros
        public Entrenador(int idEntrenador, string nombre, string apellido, int reputacion, int puntos, string tacticaFavorita, int? idEquipo, string rutaImagen)
        {
            IdEntrenador = idEntrenador;
            Nombre = nombre;
            Apellido = apellido;
            Reputacion = reputacion;
            Puntos = puntos;
            TacticaFavorita = tacticaFavorita;
            IdEquipo = idEquipo;
            RutaImagen = rutaImagen;
        }

        // Constructor con todos los parámetros sin el ID_JUGADOR
        public Entrenador(string nombre, string apellido, int reputacion, int puntos, string tacticaFavorita, int? idEquipo, string rutaImagen)
        {
            Nombre = nombre;
            Apellido = apellido;
            Reputacion = reputacion;
            Puntos = puntos;
            TacticaFavorita = tacticaFavorita;
            IdEquipo = idEquipo;
            RutaImagen = rutaImagen;
        }

        // ToString para representar el objeto como cadena de texto
        public override string ToString()
        {
            return $"Jugador: {Nombre} {Apellido}, Puntos: {Puntos}, Equipo ID: {IdEquipo}, NombreCompleto: {NombreCompleto}, Reputacion: {Reputacion}";
        }
    }
}
