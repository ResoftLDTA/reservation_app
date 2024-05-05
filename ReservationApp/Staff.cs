namespace ReservationApp;

public class Staff
{
    private string _name;

    public string Name => _name;
    private DbHotel dbHotel;
    public DbHotel Db => dbHotel;

    public Staff (string name, DbHotel DbHotel)
    {
        _name = name;
        dbHotel = DbHotel;
    }
   
   public bool GetRoomsAvailability(Room room) 
   {    
    return !room.occupied;
   }

    public float GetBookingPrice(Booking booking) //No se vio necesario poner client y roomtype, pues ese calculo ya se hace en la clase booking
    {
        return booking.Price;
    }

    public Booking Book(Room room, Client client, Booking booking) 
    {
       
        Booking book = new Booking(client,room,DateTime.Now, booking.BookedNights);
        Db.bookings.Add(book);        
        return book;        
    }

      public void UndoBook(Booking bookToRemove) 
    {
         if(Db.bookings.Contains(bookToRemove))
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