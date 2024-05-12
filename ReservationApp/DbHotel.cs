using System.Collections.Generic;

namespace ReservationApp;

public class DbHotel
{
    /* Clase para almacenar la información de las habitaciones y reservas. Su constructor está vacío, y solo sirve
     para inicializar las listas. */
    public List<Room> rooms;
    public List<Booking> bookings;

    public DbHotel()
    {
        rooms = new List<Room>();
        bookings = new List<Booking>();
    }
}