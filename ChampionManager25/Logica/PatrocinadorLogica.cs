using ChampionManager25.Entidades;
using ChampionManager25.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChampionManager25.Logica
{
    public class PatrocinadorLogica
    {
        private PatrocinadorDatos _datos = new PatrocinadorDatos();

        // Llamada al método que trae el listado de los patrocinadores
        public List<Patrocinador> MostrarListaPatrocinadores()
        {
            return _datos.MostrarListaPatrocinadores();
        }

        // Llamada al método que añade un nuevo patrocinador contratado
        public void AnadirUnPatrocinador(int patrocinador, int cantidad, int duracion, int equipo, int manager)
        {
            _datos.AnadirUnPatrocinador(patrocinador, cantidad, duracion, equipo, manager);
        }

        // Llamada al método que muestra los patrocinadores contratados
        public Patrocinador PatrocinadoresContratados(int manager, int equipo)
        {
            return _datos.PatrocinadoresContratados(manager, equipo);
        }

        // Llamada al método que devuelve el nombre de un Patrocinador
        public string NombrePatrocinador(int patrocinador)
        {
            return _datos.NombrePatrocinador(patrocinador);
        }

        // Llamada al método para restar 1 año al Patrocinador contratado
        public void RestarAnioPatrocinador(int patrocinador)
        {
            _datos.RestarAnioPatrocinador(patrocinador);
        }

        // Llamada al método para elimina un Patrocinador contratado
        public void CancelarPatrocinador(int patrocinador)
        {
            _datos.CancelarPatrocinador(patrocinador);
        }
    }
}
