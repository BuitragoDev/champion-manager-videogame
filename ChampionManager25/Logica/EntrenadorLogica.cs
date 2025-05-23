﻿using ChampionManager25.Datos;
using ChampionManager25.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Logica
{
    public class EntrenadorLogica
    {
        // Instanciar EntrenadorDatos dentro de la clase lógica
        private EntrenadorDatos _datos = new EntrenadorDatos();

        // Llamada al método que muestra el ranking de entrenadores
        public List<Entrenador> obtenerRankingEntrenadores(int miManager)
        {
            return _datos.obtenerRankingEntrenadores(miManager);
        }

        // Llamada al método para mostrar un entrenador
        public Entrenador MostrarEntrenador(int id)
        {
            return _datos.MostrarEntrenador(id);
        }

        // Llamada al método que actualiza los puntos de los entrenadores
        public void ActualizarResultadoManager(int entrenador, int puntos)
        {
            _datos.ActualizarResultadoManager(entrenador, puntos);
        }

        // Llamada al método que actualiza los detalles de un equipo
        public void EditarEntrenador(Entrenador entrenador)
        {
            _datos.EditarEntrenador(entrenador);
        }
    }
}
