namespace ReservationApp;

public class Client
{
    private uint _id;
    public uint Id => _id;

    private string _name;
    public string Name => _name;

    public Client(uint id, string name)
    {
        /* Client constructor.
         id: Client's id, like passport, CE, etc.
         name: Client's name.
         */

        _id = id;
        _name = name;
    }
}