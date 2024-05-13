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
            using (FileStream fs = File.OpenRead(userDataPath))
            {
                string fileContent = JsonConvert.SerializeObject(fs);
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
            Console.WriteLine($"Error de entrada/salida al leer el archivo JSON: {ex.Message}. Se creará un DBHotel nuevo.");
        }
        catch (JsonReaderException ex)
        {
            Console.WriteLine($"Error al serializar el contenido del archivo JSON: {ex.Message}. Se creará un DBHotel nuevo.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Se produjo un error inesperado: {ex.Message}. Se creará un DBHotel nuevo.");
        }

        // En caso de error, devolver un objeto DbHotel con las habitaciones
        return CreateDefaultDbHotel();
    }

    private static DbHotel CreateDefaultDbHotel()
    {
        DbHotel dbHotel = new DbHotel();
        Admin admin = new Admin("Default", dbHotel);

        // Crear 10 habitaciones sencillas
        for (int i = 1; i <= 10; i++)
        {
            RoomType simple =  new RoomType("Simple", 1, 100000);
            admin.CreateRoom(simple);
        }

        // Crear 10 habitaciones dobles
        for (int i = 1; i <= 10; i++)
        {
            RoomType doble = new RoomType("Doble", 2, 180000);
            admin.CreateRoom(doble);
        }

        // Crear 10 habitaciones matrimoniales
        for (int i = 1; i <= 10; i++)
        {
            RoomType matrimonial = new RoomType("Matrimonial", 4, 250000);
            admin.CreateRoom(matrimonial);
        }

        return dbHotel;
    }

    public static void SaveFile(DbHotel dbHotel)
    {
        string fileName = "dbhotel.json";
        string userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string filePath = Path.Combine(userDataPath, fileName);

        try
        {
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