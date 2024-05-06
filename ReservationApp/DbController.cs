using System;
using System.IO;
namespace ReservationApp;

public class DbController
{
<<<<<<< HEAD
    static void Main()
    {
        // Nombre de la carpeta que deseas crear
        string nombreCarpeta = "MiCarpeta";

        try
        {
            // Llamar al m�todo para crear la carpeta
            CrearCarpetaEnDirectorioUsuario(nombreCarpeta);

            Console.WriteLine($"La carpeta '{nombreCarpeta}' se ha creado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurri� un error al intentar crear la carpeta: {ex.Message}");
        }

        // Nombre del archivo que deseas crear
        string nombreArchivo = "MiArchivo.txt";

        try
        {
            // Llamar al m�todo para crear el archivo
            CrearArchivoEnCarpeta(nombreArchivo);

            Console.WriteLine($"El archivo '{nombreArchivo}' se ha creado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurri� un error al intentar crear el archivo: {ex.Message}");
        }
    }

    static void CrearCarpetaEnDirectorioUsuario(string nombreCarpeta)
    {
        // Obtener la ruta base del directorio del usuario actual
        string rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        // Concatenar la ruta base con el nombre de la carpeta que deseas crear
        string rutaCarpeta = Path.Combine(rutaBase, nombreCarpeta);

        // Verificar si la carpeta no existe antes de intentar crearla
        if (!Directory.Exists(rutaCarpeta))
        {
            // Crear la carpeta
            Directory.CreateDirectory(rutaCarpeta);
        }
        else
        {
            throw new InvalidOperationException($"La carpeta '{nombreCarpeta}' ya existe en {rutaCarpeta}");
        }
    }

    static void CrearArchivoEnCarpeta(string nombreArchivo)
    {
        try
        {
            // Obtener la ruta base del directorio del usuario actual
            string rutaBase = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            // Concatenar la ruta base con el nombre de la carpeta "MiCarpeta"
            string rutaCarpeta = Path.Combine(rutaBase, "MiCarpeta");

            // Verificar si la carpeta "Archivos" no existe antes de intentar crearla
            if (!Directory.Exists(rutaCarpeta))
            {
                // Crear la carpeta "Archivos"
                Directory.CreateDirectory(rutaCarpeta);
            }

            // Concatenar la ruta de la carpeta "Archivos" con el nombre del archivo
            string rutaArchivo = Path.Combine(rutaCarpeta, nombreArchivo);

            // Verificar si el archivo no existe antes de intentar crearlo
            if (!File.Exists(rutaArchivo))
            {
                // Crear el archivo y cerrarlo inmediatamente
                using (File.Create(rutaArchivo)) { }
            }
            else
            {
                throw new InvalidOperationException($"El archivo '{nombreArchivo}' ya existe en {rutaArchivo}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocurri� un error al intentar crear el archivo: {ex.Message}");
        }
    }


=======
    String path = "~/hotel.db";
>>>>>>> 18633ec9dad06d2f2428efdfeb0dfcf1699dbce7
}