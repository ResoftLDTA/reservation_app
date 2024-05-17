using System;
using Gtk;
namespace ReservationApp;

public class Program
{
    static void Main()
    {
        DbHotel dbHotel = new DbHotel();
        Application.Init();
        MainWindow win = new MainWindow();
        win.Show();
        Application.Run();
    }
}