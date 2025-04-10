using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionManager25.Entidades
{
    public static class Configuracion
    {
        // Ruta base de los recursos, que puede ser dinámica según la base de datos
        public static string RutaRecursosBase { get; set; }

        // Método para inicializar la ruta de recursos
        public static void EstablecerRutaRecursos(string nombreBaseDatos)
        {
            // Asumiendo que el nombre de la base de datos es la carpeta dentro de Mis Documentos
            RutaRecursosBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ChampionsManager", nombreBaseDatos, "Recursos");
        }
    }
}
