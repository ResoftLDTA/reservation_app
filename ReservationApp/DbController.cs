using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ReservationApp;

public class DbController
{
    public static DbHotel CargarArchivo()
    {
        string fileName = "dbhotel.json";
        string userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Console.WriteLine($"Se detectó la ruta: {userDataPath}");

        string filePath = Path.Combine(userDataPath, fileName);

        try
        {

            if (Path.Exists(filePath))
            {
                using (FileStream fs = File.OpenRead(userDataPath))
                {
                    string fileContent = JsonConvert.SerializeObject(fs);
                    DbHotel dbhotel = JsonConvert.DeserializeObject<DbHotel>(fileContent);

                    return dbhotel;
                }
            }
            else
            {
                {
                    using (FileStream fs = File.Create(userDataPath))
                    {
                        return new DbHotel();
                    }
                }
            }
        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine("El archivo JSON no se pudo encontrar.");
        }
        catch (System.IO.IOException ex)
        {
            Console.WriteLine($"Error de entrada/salida al leer el archivo JSON: {ex.Message}");
        }
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"Error al leer el contenido del archivo JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error inesperado: {ex.Message}");
        }

        // En caso de error, devolver un objeto DbHotel vacío
        return new DbHotel();
    }


    static void CargarArchivoPrueba()
    {
        // Nombre de la carpeta que deseas crear
        string nombreCarpeta = "MiCarpeta";

        try
        {
            // Llamar al metodo para crear la carpeta
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
            Console.WriteLine($"Ocurrio un error al intentar crear el archivo: {ex.Message}");
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
                using (File.Create(rutaArchivo))
                {
                }
            }
            else
            {
                throw new InvalidOperationException($"El archivo '{nombreArchivo}' ya existe en {rutaArchivo}");
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Ocurrio un error al intentar crear el archivo: {ex.Message}");
        }
    }
}



