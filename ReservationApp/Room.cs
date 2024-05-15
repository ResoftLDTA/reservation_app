namespace ReservationApp;

public class Room
{
    /* When state is false,*/
    private RoomType _type;
    public RoomType Type => _type;

    private uint _id;
    public uint Id => _id;

    public bool occupied;

    public Room(RoomType type, uint id)
    {
        _type = type;
        _id = id;
        occupied = false;
    }
}