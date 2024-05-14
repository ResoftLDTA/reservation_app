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
        foreach (Room room in _db.Rooms)
        {
            if (room.Type.Type == roomType.Type && room.occupied == false)
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

    public Booking Book(string clientName, uint clientId, DateTime startDate, uint bookedNights,
        RoomType desiredRoomType)
    {
        // Se busca una habitación del tipo deseado que no esté ocupada y se obtiene un array de habitaciones.
        var availableRoom = _db.Rooms.Where(room => (room.Type.Type == desiredRoomType.Type) && (room.occupied == false))
            .ToArray();

        if (availableRoom.Length > 0) // Verificar si hay al menos una habitación disponible
        {
            Room room = availableRoom[0];
            Booking book = new Booking(new Client(clientId, clientName), room, DateTime.Now, bookedNights,
                (uint)(_db.Bookings.Count + 1));
            Db.Bookings.Add(book);
            return book;
        }

        throw new Exception("No hay habitaciones disponibles del tipo deseado.");
    }

    public void UndoBook(Booking bookToRemove)
    {
        if (Db.Bookings.Contains(bookToRemove))
        {
            Db.Bookings.Remove(bookToRemove);
            Console.WriteLine("Reserva deshecha");
        }
        else
        {
            Console.WriteLine("La reserva especificada no existe");
        }
    }

    public void CancelBooking(int bookingId)
    {
        // Buscar la reserva por su ID
        Booking bookingToRemove = Db.Bookings.FirstOrDefault(booking => booking.Id == bookingId);

        if (bookingToRemove != null)
        {
            // Eliminar la reserva de la lista
            Db.Bookings.Remove(bookingToRemove);
            Console.WriteLine("Reserva cancelada exitosamente.");
        }
        else
        {
            Console.WriteLine("La reserva especificada no existe.");
        }
    }
}