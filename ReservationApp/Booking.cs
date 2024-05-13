using System;

namespace ReservationApp;

public class Booking
{
    private uint _id;
    public uint Id => _id;

    private Client _client;
    public Client Client => _client;

    private Room _room;
    public Room Room => _room;

    private DateTime _start;
    public DateTime Start => _start;

    private DateTime _end;
    public DateTime End => _end;

    private bool _expired;

    public bool GetExpiredStatus()
    {
        if (_end.Date == DateTime.Today)
        {
            _expired = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    private uint _bookedNights;
    public uint BookedNights => _bookedNights;

    private float _price;
    public float Price => _price;

    public Booking(Client client, Room room, DateTime start, uint bookedNights, uint id)
    {
        _id = id;
        _client = client;
        _room = room;
        _start = start;
        _end = start + TimeSpan.FromDays(bookedNights);
        _expired = false;
        _price = _room.Type.Price * bookedNights;
    }
}