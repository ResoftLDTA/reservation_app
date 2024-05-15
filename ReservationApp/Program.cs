using Gtk;

namespace ReservationApp
{
    public class Program
    {
        public static void Main()
        {
            Application.Init();
            BuscarReservaScreen search = new BuscarReservaScreen()
            ReservaMuestraScreen screen = new ReservaMuestraScreen();
            screen.ShowAll();
            Application.Run();
        }
    }
}