namespace ReservationApp;

public class Program
{
    static void Main()
    {
        DbController.CargarArchivo();
        Console.WriteLine("Hey, frontend!");
    }
}