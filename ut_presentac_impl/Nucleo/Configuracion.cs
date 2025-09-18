using lib_dominio.Nucleo;

namespace ut_presentac_imp.Nucleo
{
    public class Configuracion
    {
        private static Dictionary<string, string>? datos = null;

        public static string ObtenerValor(string clave)
        {
            if (datos == null)
                Cargar();

            if (datos != null && datos.ContainsKey(clave))
                return datos[clave];

            throw new Exception($"La clave '{clave}' no existe en el archivo JSON {DatosGenerales.ruta_json}");
        }

        public static void Cargar()
        {
            if (!File.Exists(DatosGenerales.ruta_json))
                throw new Exception($"No se encontró el archivo de configuración en {DatosGenerales.ruta_json}");

            StreamReader jsonStream = File.OpenText(DatosGenerales.ruta_json);
            var json = jsonStream.ReadToEnd();

            datos = JsonConversor.ConvertirAObjeto<Dictionary<string, string>>(json)!;
        }
    }
}


