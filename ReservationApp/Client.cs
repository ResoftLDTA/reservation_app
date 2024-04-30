namespace ReservationApp;

public class Client
{
private int _clientID;
public int GetClientID() => _clientID;
public int ClientID => _clientID; //ClientID es una propiedad de solo lectura
private string _clientName;

public string GetClientName() => _clientName;
public string ClientName => _clientName;

}