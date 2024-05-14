using System;

namespace ReservationApp;

public class Program
{
    static void Main()
    {
        Admin admin = new Admin("Juan Carlos", DbController.CargarArchivo());
        Frontend f = new Frontend(admin);
        f.Run();

        DbController.SaveFile(admin.Db);
    }
}