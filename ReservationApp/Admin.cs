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

        // Obtenemos el primer día del mes pasado
        DateTime lastMonthStart = DateTime.Now.AddMonths(-1).AddDays(-DateTime.Now.Day + 1);

        // Obtenemos el primer día del mes actual
        DateTime currentMonthStart = DateTime.Now.AddDays(-DateTime.Now.Day + 1);

        // Filtramos las reservas que se hicieron el mes pasado
        var bookingsByGivenMonth = _db.bookings.Where(booking => booking.Start.Month == month).ToList();

        // Sumamos el precio de cada reserva
        foreach (var booking in bookingsByGivenMonth)
        {
            totalIncome += booking.Room.Type.Price * booking.BookedNights;
        }

        return totalIncome;
    }
    
    public void CrearHabitación(RoomType roomType)
    {
        _db.rooms.Append(new Room(roomType, (uint)(_db.rooms.Count + 1)));
    }
}
