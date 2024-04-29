namespace ReservationApp;

public class Booking
{

    private Client _client;
    public Client GetClient() => _client;
    private Room _room;
    public Room GetRoom() => _room;
    public Room Room => _room;
    private int _bookingId;
    
    public int GetBookingId() => _bookingId;

    public int BookingId => _bookingId;
    
    private bool _expired;
    public bool Getstate() => _expired;
    public bool expired => _expired;
   
    private int _bookedNights;

    public int GetBookedNights() => _bookedNights;
    public int BookedNights => _bookedNights;  
    private float price;

  

  
    
}

