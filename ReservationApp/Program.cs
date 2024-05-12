using System;

namespace ReservationApp;

public class Program
{
    static void Main()
    {
        DbHotel dbHotel = DbController.CargarArchivo();
        
        RoomType matrimonial = new RoomType("Matrimonial", 2, 1550);
        
        Admin admin = new Admin("Juan Carlos", dbHotel);
        admin.CreateRoom(matrimonial);
        admin.Book("Ricardo", 30012353, DateTime.Today, 5, matrimonial);
        
        Frontend f = new Frontend(dbHotel, admin);
        f.Run();

        DbController.SaveFile(dbHotel);
    }
}