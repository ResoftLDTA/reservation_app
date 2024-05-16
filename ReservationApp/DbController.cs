using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace ReservationApp;

public class DbController
{
    private static string _fileName = "dbhotel.json";
    private static string _resoftFolderName = "Resoft";

    public static DbHotel ReadFile()
    {
        string userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Console.WriteLine($"Se detectó la ruta: {userDataPath}");

        string filePath = Path.Combine(userDataPath, _resoftFolderName, _fileName);

        try
        {
            using (StreamReader reader = File.OpenText(filePath))
            {
                string fileContent = reader.ReadToEnd();
                DbHotel dbhotel = JsonConvert.DeserializeObject<DbHotel>(fileContent);

                return dbhotel;
            }
        }
        catch (System.IO.FileNotFoundException)
        {
            Console.WriteLine("El archivo JSON no se pudo encontrar. Se procede a crear uno nuevo.");
        }
        catch (System.IO.IOException ex)
        {
            Console.WriteLine(
                $"Error de entrada/salida al leer el archivo JSON: {ex.Message}. Se creará un DBHotel nuevo.");
        }
        catch (JsonReaderException ex)
        {
            Console.WriteLine(
                $"Error al serializar el contenido del archivo JSON: {ex.Message}. Se creará un DBHotel nuevo.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error inesperado: {ex.Message}. Se creará un DBHotel nuevo.");
        }

        // En caso de error, devolver un objeto DbHotel con las habitaciones
        return CreateDefaultDbHotel();
    }

    public static DbHotel CreateDefaultDbHotel()
    {
        DbHotel dbHotel = new DbHotel();
        Admin admin = new Admin("Default", dbHotel);

        // Crear 10 habitaciones sencillas
        for (int i = 1; i <= 10; i++)
        {
            admin.CreateRoom(RoomType.Simple);
        }

        // Crear 10 habitaciones dobles
        for (int i = 1; i <= 10; i++)
        {
            admin.CreateRoom(RoomType.Double);
        }

        // Crear 10 habitaciones matrimoniales
        for (int i = 1; i <= 10; i++)
        {
            admin.CreateRoom(RoomType.Matrimonial);
        }

        return dbHotel;
    }

    public static void SaveFile(DbHotel dbHotel)
    {
        // Se obtiene la ruta de guardado de archivos y se crea la ruta donde va a estar almacenado dbhotel.json.
        string userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string filePath = Path.Combine(userDataPath, _resoftFolderName, _fileName);

        try
        {
            // Se crea la carpeta en caso de que no exista.
            Directory.CreateDirectory(Path.Combine(userDataPath, _resoftFolderName));

            string jsonContent = JsonConvert.SerializeObject(dbHotel);

            File.WriteAllText(filePath, jsonContent);
            Console.WriteLine("Datos guardados exitosamente en el archivo JSON.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error de entrada/salida al escribir en el archivo JSON: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error inesperado al guardar los datos en el archivo JSON: {ex.Message}");
        }
    }
}