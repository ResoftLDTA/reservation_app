using System;
using System.Linq;

namespace ReservationApp;

public class Admin : Staff
{
    public Admin(string name, DbHotel db) : base(name, db)
    {
        // Constructor de la clase Admin que llama al constructor de la clase base (Staff)
    }

    public float CalculateIncome(uint month)
    {
        float totalIncome = 0;

        // Filtramos las reservas que se hicieron el mes pasado
        var bookingsByGivenMonth = _db.Bookings.Where(booking => booking.Start.Month == month).ToList();

        // Sumamos el precio de cada reserva
        foreach (var booking in bookingsByGivenMonth)
        {
            totalIncome += booking.Room.Type.Price * booking.BookedNights;
        }

        return totalIncome;
    }

    public void CreateRoom(RoomType roomType)
    {
        _db.Rooms.Add(new Room(roomType, (uint)(_db.Rooms.Count + 1)));
    }
}