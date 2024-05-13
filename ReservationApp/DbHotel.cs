using System.Collections.Generic;

namespace ReservationApp;

public class DbHotel
{
    /* Clase para almacenar la información de las habitaciones y reservas. Su constructor está vacío, y solo sirve
     para inicializar las listas. */
    public List<Room> Rooms;
    public List<Booking> Bookings;

    public DbHotel()
    {
        Rooms = new List<Room>();
        Bookings = new List<Booking>();
    }
}