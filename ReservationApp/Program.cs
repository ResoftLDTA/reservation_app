using System;

namespace ReservationApp;

public class Program
{
    static void Main()
    {
        DbHotel dbHotel = DbController.CargarArchivo();
        Admin admin = new Admin("Juan Carlos", dbHotel);
        Frontend f = new Frontend(dbHotel, admin);
        f.Run();

        DbController.SaveFile(dbHotel);
    }
}