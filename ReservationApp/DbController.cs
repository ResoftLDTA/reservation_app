using System;
using System.IO;
namespace ReservationApp;

public class DbController
{
    static void Main()
    {
        // Nombre de la carpeta que deseas crear
        string nombreCarpeta = "MiCarpeta";

        try
        {
            // Llamar al método para crear la carpeta
            CrearCarpetaEnDirectorioUsuario(nombreCarpeta);

            Console.WriteLine($"La carpeta '{nombreCarpeta}' se ha creado exitosamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocurrió un error al intentar crear la carpeta: {ex.Message}");
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
}