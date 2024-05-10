using System;
using System.Linq;

namespace ReservationApp;

public class Staff
{
    protected string _name;

    public string Name => _name;
    protected DbHotel _db;
    public DbHotel Db => _db;

    public Staff(string name, DbHotel db)
    {
        _name = name;
        _db = db;
    }

    public bool GetRoomsAvailability(RoomType roomType)
        /* Este método devuelve True si hay una habitación disponible del tipo roomType. Falso si no hay una disponible. */
    {
        foreach (var room in _db.rooms)
        {
            if (room.Type == roomType && room.occupied == false)
            {
                return true;
            }
        }

        return false;
    }

    public float GetBookingPrice(uint clientId, RoomType roomType, uint bookedNights)
    //No se vio necesario poner client y roomtype, pues ese calculo ya se hace en la clase booking
    /* Ricardo Respuesta: Aunque esto ya se haga en la clase booking, se tendría que crear un objeto booking, que luego tendrá un ID
     asignado en la lista de reservas, y al no ser una reserva efectiva puede generarnos problemas. Podría cambiarse la 
     lógica del funcionamiento de la clase bookigns, pero por el momento creo que lo ideal sería copiarse el método con 
     los mismos parámetros. */
    {
        return roomType.Price * bookedNights;
    }

    public Booking Book(string clientName, uint clientId, DateTime startDate, uint bookedNights, RoomType desiredRoomType)
    {
        // Se busca una habitación del tipo deseado que no esté ocupada y se obtiene un array de habitaciones.
        var availableRoom = _db.rooms.Where(room => (room.Type == desiredRoomType) && (room.occupied == false)).ToArray();
        Room room = availableRoom[0];
        Booking book = new Booking(new Client(clientId, clientName), room, DateTime.Now, bookedNights, (uint)(_db.bookings.Count + 1));
        Db.bookings.Add(book);
        return book;
    }

    public void UndoBook(Booking bookToRemove)
    {
        if (Db.bookings.Contains(bookToRemove))
        {
            Db.bookings.Remove(bookToRemove);
            Console.WriteLine("Reserva deshecha");
        }
        else
        {
            Console.WriteLine("La reserva especificada no existe");
        }
    }
}