using System;

namespace ReservationApp;

public class Frontend
{
    private DbHotel _dbHotel;
    private Staff _staff;

    public Frontend(DbHotel dbHotel, Staff staff)
    {
        _dbHotel = dbHotel;
        _staff = staff;
    }

    public void Run()
    {
        Menu();
        ListBookings();
    }

    private void Menu()
    {
        Console.WriteLine("Bienvenido a Reservation App.");
        Console.WriteLine("1) Para listar las reservas");
        Console.WriteLine("2) Para crear una reserva");
    }

    private void ListBookings()
    {
        foreach (Booking booking in _dbHotel.Bookings)
        {
            Console.WriteLine("**********************************");
            Console.WriteLine($"Reserva no. {_dbHotel.Bookings.IndexOf(booking)}");
            Console.WriteLine($"Cliente {booking.Client.Name}, id {booking.Client.Id}\n");
            Console.WriteLine($"Habitación no. {booking.Room.Id}, tipo {booking.Room.Type.Type}\n”");
            Console.WriteLine($"Reserva desde {booking.Start.Date} hasta el {booking.End.Date}\n");
            Console.WriteLine($"Estuvo {booking.BookedNights}, para un costo total de {booking.Price}\n\n");
        }
    }

    private void CreateBooking()
    {
        Console.WriteLine("Introduce el ID del cliente:");
        uint clientId;
        do
        {
            try
            {
                string unparsedClientID = Console.ReadLine();
                if (unparsedClientID == null)
                {
                    throw new NullReferenceException("El ID del cliente no puede estar vacío.");
                }
                clientId = uint.Parse(unparsedClientID);
                break;
            }
            catch (ArgumentNullException nullException)
            {
                Console.WriteLine(nullException.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un ID válido.");
            }
        } while (true);

        Console.WriteLine("Introduce el nombre del cliente:");
        string clientName;

        do
        {
            try
            {
                clientName = Console.ReadLine();
                if (clientName == null)
                {
                    throw new NullReferenceException("El nombre del cliente no puede estar vacío.");
                }
                break;
            }
            catch (NullReferenceException nullReferenceException)
            {
                Console.WriteLine(nullReferenceException.Message);
            }
        } while (true);

        Client client = new Client(clientId, clientName);
    }
}