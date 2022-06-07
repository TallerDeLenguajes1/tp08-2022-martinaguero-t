// using System.IO;

namespace TP8;

class Program{

    static int Main(string[] args){

        Console.WriteLine("====> Indexador de carpeta: ");
        Console.WriteLine("Advertencia: no probar con carpetas que contengan archivos sin un nombre y una extensión");
        
        Console.WriteLine("Ingrese el path de una carpeta: ");

        string? path = Console.ReadLine();

        string rutaArchivoCSV = @"D:\Facultad\2do\Taller_de_Lenguajes_I\Repositorios\TPS\tp08-2022-martinaguero-t\TP8\nombresArchivos2.csv";
                
        if(!File.Exists(rutaArchivoCSV)){
            File.Create(rutaArchivoCSV);
        }

        StreamWriter sr = new StreamWriter(rutaArchivoCSV);

        mostrarContenidoDirectorioYGuardar(path,sr);

        sr.Close();

        Console.Read();

        return 0;
    }

    static bool buscarDirectorio(string path){

        if(!Directory.Exists(path)){
            return false;
        } else {
            return true;
        }

    }

    static void mostrarContenidoDirectorioYGuardar(string path, StreamWriter sr){

        if(buscarDirectorio(path)){

            List<string> archivos = Directory.GetFiles(path).ToList();
            List<string> subcarpetas = Directory.GetDirectories(path).ToList();
            

            if(archivos.Any() || subcarpetas.Any()){

                var separarRuta = path.Split("\\");
                Console.WriteLine("Mostrando el contenido de la carpeta " + separarRuta[separarRuta.Length-1]);
                // Se indica al usuario en qué directorio está

                short i = 1;
                // Indice para indicar el número de archivo o carpeta en una carpeta determinada. Se "reinicia" en cada llamada recursiva.

                foreach (string rutaArchivo in archivos)
                {   
                    separarRuta = rutaArchivo.Split("\\");

                    Console.WriteLine("File -> " + separarRuta[separarRuta.Length-1]);

                    var nombreYExtension = separarRuta[separarRuta.Length-1].Split(".");

                    sr.WriteLine(i +";"+ nombreYExtension[0] +";"+ nombreYExtension[1]);

                    i++;
                }

                foreach (string rutaSubcarpeta in subcarpetas)
                {   
                    separarRuta = rutaSubcarpeta.Split("\\");

                    Console.WriteLine("Folder -> " + separarRuta[separarRuta.Length-1]);

                    sr.WriteLine(i +";"+ separarRuta[separarRuta.Length-1]+";"+"ES CARPETA");
                    // Si es carpeta, no se guarda una extensión
                    
                    i++;
                }

                foreach (string rutaSubcarpeta in subcarpetas){
                    mostrarContenidoDirectorioYGuardar(rutaSubcarpeta,sr);
                }
            }
        }
    }
}
