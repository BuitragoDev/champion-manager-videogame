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
    public partial class UC_PantallaCarga : UserControl
    {
        public UC_PantallaCarga()
        {
            InitializeComponent();

            string[] frases = {
                "El fútbol es simple, pero es difícil jugar de forma simple. – Johan Cruyff",
                "Al fútbol siempre debe jugarse de manera atractiva, debes jugar de manera ofensiva, debe ser un espectáculo. – Johan Cruyff",
                "El fútbol es el ballet de las masas. – Dmitri Shostakovich",
                "Al fútbol no se juega con los pies, se juega con la cabeza. – Luis Aragonés",
                "El fútbol es mucho más que un deporte, es un arte que nos une y nos da sentido. – Pelé"
            };

            // Crear una instancia de la clase Random
            Random random = new Random();

            // Obtener un índice aleatorio entre 0 y el tamaño del array menos 1
            int indiceAleatorio = random.Next(frases.Length);

            // Asignar la frase aleatoria al TextBox
            txtFraseAleatoria.Text = frases[indiceAleatorio];
        }
    }
}
