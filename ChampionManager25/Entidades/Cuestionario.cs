using ChampionManager25.Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public class Cuestionario
    {
        ManagerLogica _logicaManager = new ManagerLogica();

        private List<Pregunta> preguntas = new List<Pregunta>();  // Lista de preguntas posibles
        private List<Pregunta> preguntasSeleccionadas = new List<Pregunta>();  // Preguntas seleccionadas aleatoriamente
        public int _puntosDirectiva = 0;
        public int _puntosFans = 0;
        public int _puntosJugadores = 0;
        private int _indicePreguntaActual = 0;

        List<string> periodistasDeportivos = new List<string>
        {
            "Álvaro Montalbán (La Crónica del Balón)",
            "Rubén Santisteban (Tiempo de Gol)",
            "Javier Requena (TV Fútbol Total)",
            "Pablo Llorente (El Esférico)",
            "Patricia Velarde (Pasión Futbolera)",
            "Sergio Expósito (Tácticas y Estrategia)",
            "César Palacios (Fútbol y Algo Más)",
            "Germán Sanchís (El Marcador Final)",
            "Valeria Benavides (La Mesa del Fútbol)",
            "Fernando Cifuentes (FutbolData)"
        };
        Random random = new Random();

        // Constructor
        public Cuestionario()
        {
            // Pregunta 1
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "Esta será su primera experiencia como entrenador en un club profesional. ¿Cómo piensa compensar la falta de experiencia y qué cree que puede aportar al equipo desde otra perspectiva?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "No tengo experiencia en los banquillos, pero sí muchas ganas, trabajo y un equipo técnico preparado para apoyar cada decisión.", PuntosDirectiva = 5, PuntosFans = 2, PuntosJugadores = -4 },
                new Respuesta { Texto = "La experiencia se gana con trabajo. Vengo con ideas frescas, energía y la ambición de construir un equipo competitivo desde el primer día.", PuntosDirectiva = 3, PuntosFans = 1, PuntosJugadores = -1 },
                new Respuesta { Texto = "Todos los grandes entrenadores tuvieron un inicio. Confío en mis conocimientos y en que los jugadores crean en el proyecto.", PuntosDirectiva = 0, PuntosFans = 0, PuntosJugadores = 1 }
            }
            });

            // Pregunta 2
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "Ha dirigido equipos con diferentes estilos de juego a lo largo de su carrera. ¿Qué modelo táctico intentará implementar aquí y cómo planea adaptarlo a la plantilla actual?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Buscaremos un juego equilibrado y competitivo, adaptándonos a las fortalezas del equipo sin perder nuestra identidad.", PuntosDirectiva = 3, PuntosFans = -2, PuntosJugadores = 3 },
                new Respuesta { Texto = "Queremos un equipo protagonista, con posesión y presión alta. Requiere trabajo, pero es la mejor forma de dominar los partidos.", PuntosDirectiva = 5, PuntosFans = 5, PuntosJugadores = -5 },
                new Respuesta { Texto = "El fútbol exige ser prácticos. Si el equipo rinde mejor defendiendo y saliendo rápido, ajustaremos el sistema sin problema.", PuntosDirectiva = 3, PuntosFans = -1, PuntosJugadores = 2 }
            }
            });

            // Pregunta 3
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "El club viene de una temporada con altibajos y muchas expectativas puestas en este nuevo proyecto. ¿Cómo piensa manejar la presión tanto de los aficionados como de la directiva para alcanzar los objetivos marcados?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "La presión es parte del fútbol. Hay que trabajar con ambición, pero también con paciencia para construir un equipo sólido.", PuntosDirectiva = 3, PuntosFans = 0, PuntosJugadores = 0 },
                new Respuesta { Texto = "Los resultados hablarán. Prometo trabajo y esfuerzo, pero el fútbol es impredecible y requiere tiempo para ver frutos.", PuntosDirectiva = -3, PuntosFans = -5, PuntosJugadores = 0 },
                new Respuesta { Texto = "Este club merece pelear arriba. No hay excusas, trabajaremos para que los hinchas se sientan orgullosos de su equipo.", PuntosDirectiva = 3, PuntosFans = 5, PuntosJugadores = 2 }
            }
            });

            // Pregunta 4
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "Uno de los retos más importantes en cualquier equipo es la gestión del vestuario. ¿Cómo afrontará el liderazgo del grupo y qué importancia le da al factor psicológico en la plantilla?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Los jugadores saben lo que se espera de ellos. Mi trabajo es sacar su mejor rendimiento, no hacer amigos en el vestuario.", PuntosDirectiva = 0, PuntosFans = 5, PuntosJugadores = -5 },
                new Respuesta { Texto = "Aquí nadie es imprescindible, pero todos son importantes. Habrá exigencia, pero también confianza para que den su mejor versión.", PuntosDirectiva = 2, PuntosFans = -1, PuntosJugadores = 0 },
                new Respuesta { Texto = "Un vestuario fuerte es clave. La comunicación y el respeto serán fundamentales para crear un equipo unido y competitivo.", PuntosDirectiva = 3, PuntosFans = 2, PuntosJugadores = 5 }
            }
            });

            // Pregunta 5
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "En los últimos años, el fútbol ha evolucionado mucho en cuanto a análisis de datos y tecnología aplicada al rendimiento. ¿Qué papel jugarán estas herramientas en su metodología de trabajo diario?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Usaremos análisis de datos y tecnología para optimizar entrenamientos y evitar lesiones. Queremos un equipo moderno.", PuntosDirectiva = 2, PuntosFans = -1, PuntosJugadores = 1 },
                new Respuesta { Texto = "Son un complemento importante, pero nunca reemplazarán la intuición y la experiencia en la toma de decisiones.", PuntosDirectiva = 5, PuntosFans = 2, PuntosJugadores = 3 },
                new Respuesta { Texto = "El fútbol es de los jugadores, no de los ordenadores. Prefiero confiar en mi experiencia antes que en los números.", PuntosDirectiva = -5, PuntosFans = -5, PuntosJugadores = 5 }
            }
            });

            // Pregunta 6
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "Muchos entrenadores tienen un esquema de juego preferido, pero también es clave la capacidad de adaptación. ¿Hasta qué punto está dispuesto a modificar su estilo en función de las características de los jugadores?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Tengo una idea clara, pero adaptaremos el sistema para potenciar las fortalezas del equipo sin perder nuestra identidad.", PuntosDirectiva = 4, PuntosFans = 2, PuntosJugadores = 5 },
                new Respuesta { Texto = "El fútbol exige flexibilidad. Si un cambio táctico nos da ventaja en un partido o una temporada, lo aplicaremos sin dudarlo.", PuntosDirectiva = 3, PuntosFans = 3, PuntosJugadores = -2 },
                new Respuesta { Texto = "Mi filosofía es innegociable. Los jugadores deberán ajustarse a mi idea y al modelo de juego que queremos para este equipo.", PuntosDirectiva = -2, PuntosFans = -2, PuntosJugadores = -5 }
            }
            });
            // Pregunta 7
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "El mercado de fichajes sigue abierto y hay rumores sobre posibles incorporaciones y salidas. ¿Hasta qué punto participará en las decisiones deportivas y qué perfil de jugador considera prioritario para reforzar la plantilla?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Trabajaremos con la dirección deportiva para reforzar el equipo sin precipitarnos, buscando lo mejor para el club.", PuntosDirectiva = 5, PuntosFans = 5, PuntosJugadores = 1 },
                new Respuesta { Texto = "Estoy contento con la plantilla. Si llega algún refuerzo, bien, pero confío en que con lo que tenemos podemos competir.", PuntosDirectiva = 2, PuntosFans = -5, PuntosJugadores = 5 },
                new Respuesta { Texto = "Si queremos ser competitivos, necesitamos refuerzos de calidad. He pedido jugadores que puedan marcar la diferencia.", PuntosDirectiva = -1, PuntosFans = 3, PuntosJugadores = -5 }
            }
            });

            // Pregunta 8
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "El fútbol moderno exige una gran exigencia física y táctica. ¿Cuál será su planteamiento en cuanto a la preparación física y el ritmo de entrenamientos durante la temporada?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Evitar sobrecargas es clave. Los jugadores ya saben cuidarse, no necesitan entrenamientos innecesariamente duros.", PuntosDirectiva = -5, PuntosFans = -5, PuntosJugadores = 5 },
                new Respuesta { Texto = "Seremos un equipo físico e intenso. Exigiré máxima intensidad en los entrenamientos para mantener un alto nivel.", PuntosDirectiva = 5, PuntosFans = 5, PuntosJugadores = -5 },
                new Respuesta { Texto = "Buscaremos equilibrio entre carga física y descanso para llegar fuertes a final de temporada, no solo al inicio.", PuntosDirectiva = 5, PuntosFans = 3, PuntosJugadores = 5 }
            }
            });

            // Pregunta 9
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "Cada afición tiene una identidad y una manera especial de vivir el fútbol. ¿Cómo describiría su idea de conexión con los hinchas y qué tipo de equipo quiere que vean en el campo?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Mi compromiso es que este equipo lo deje todo en el campo. Quiero que los hinchas se sientan orgullosos.", PuntosDirectiva = 2, PuntosFans = 2, PuntosJugadores = 0 },
                new Respuesta { Texto = "Queremos que la afición se sienta identificada con el equipo. Con actitud y entrega, ganaremos su respeto.", PuntosDirectiva = 2, PuntosFans = 5, PuntosJugadores = 0 },
                new Respuesta { Texto = "Los hinchas son importantes, pero nosotros debemos centrarnos en jugar. Si ganamos, nos apoyarán; si no, nos criticarán.", PuntosDirectiva = 0, PuntosFans = -5, PuntosJugadores = 0 }
            }
            });

            // Pregunta 10
            preguntas.Add(new Pregunta
            {
                Entrevistador = periodistasDeportivos[random.Next(periodistasDeportivos.Count)],
                Texto = "El calendario de la temporada es muy exigente, con competiciones nacionales e internacionales. ¿Cómo planea gestionar las rotaciones para mantener al equipo competitivo sin perder frescura física y mental?",
                Respuestas = new List<Respuesta>
            {
                new Respuesta { Texto = "Rotaremos cuando sea necesario, pero sin perder estabilidad. La clave es mantener a todos motivados.", PuntosDirectiva = 5, PuntosFans = 0, PuntosJugadores = 5 },
                new Respuesta { Texto = "Los mejores jugarán siempre. No soy partidario de muchas rotaciones porque los equipos necesitan continuidad.", PuntosDirectiva = 3, PuntosFans = 3, PuntosJugadores = 3 },
                new Respuesta { Texto = "Cada partido es una batalla diferente. Usaremos la plantilla inteligentemente para llegar fuertes a los momentos clave.", PuntosDirectiva = -5, PuntosFans = -5, PuntosJugadores = -5 }
            }
            });
        }

        // Seleccionar 5 preguntas aleatorias sin repetir
        public void SeleccionarPreguntas()
        {
            Random rand = new Random();
            preguntasSeleccionadas = preguntas.OrderBy(x => rand.Next()).Take(5).ToList();
        }

        // Obtener la pregunta actual
        public Pregunta? ObtenerPreguntaActual()
        {
            if (_indicePreguntaActual < preguntasSeleccionadas.Count)
            {
                return preguntasSeleccionadas[_indicePreguntaActual];
            }
            return null;
        }

        // Sumar puntos por la respuesta seleccionada
        public void SumarPuntos(Respuesta respuesta)
        {
            _puntosDirectiva += respuesta.PuntosDirectiva;
            _puntosFans += respuesta.PuntosFans;
            _puntosJugadores += respuesta.PuntosJugadores;
        }

        // Avanzar a la siguiente pregunta
        public void AvanzarPregunta()
        {
            _indicePreguntaActual++;
        }

        // Verificar si ya no quedan más preguntas
        public bool HayMasPreguntas()
        {
            return _indicePreguntaActual < preguntasSeleccionadas.Count;
        }

        // Obtener los puntos finales
        public void ObtenerResultadosFinales(int idManager)
        {
            // Actualizar puntos de Directiva, Fans y Jugadores en la base de datos.
            _logicaManager.ActualizarConfianza(idManager, _puntosDirectiva, _puntosFans, _puntosJugadores);
        }
    }
}
