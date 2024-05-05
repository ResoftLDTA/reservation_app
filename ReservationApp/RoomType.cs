namespace ReservationApp;

public class RoomType
{
    private string _type;
    /* este sería el getter de _type. Para hacer referencia a él, se haría de esta manera:
     
        roomType.Type       <--- donde roomType sería una instancia de RoomType.
        
     La idea de hacerlo de esta manera es que el parámetro _type sea de sólo lectura, y no haya manera de modificarlo.
     
     NOTA: A la hora de usar esta clase uno accede a Type, pero C# lo que realmente hace es llamar a una función que
     devuelve _type. Por eso se indica el '=>'.
     
     */
    public string Type => _type;
    
    private uint _peopleCapacity;
    
    /* De nuevo, para acceder a _peopleCapacity, se haría usando
     
        RoomType.PeopleCapacity     
     */
    public uint PeopleCapacity => _peopleCapacity;
    
    private float _price;
    public float Price => _price;

    public RoomType(string type, uint peopleCapacity, float price)
    {
        _type = type;
        _peopleCapacity = peopleCapacity;
        _price = price;
    }

    public void SetPrice(Admin admin, float newPrice) => _price = newPrice;
    
}