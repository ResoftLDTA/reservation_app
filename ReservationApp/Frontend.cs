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

        Console.WriteLine("Introduce la duración de la reserva en noches:");
        uint bookedNights;
        do
        {
            try
            {
                string unparsedBookedNights = Console.ReadLine();
                if (string.IsNullOrEmpty(unparsedBookedNights))
                {
                    throw new NullReferenceException("La duración de la reserva no puede estar vacía.");
                }
                bookedNights = uint.Parse(unparsedBookedNights);
                break;
            }
            catch (ArgumentNullException nullException)
            {
                Console.WriteLine(nullException.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("No has introducido un número válido de noches.");
            }
        } while (true);

        // Se muestran las opciones de habitaciones disponibles y solicitar al usuario que elija una.
        Console.WriteLine("Introduce el tipo de habitación deseado:");
        Console.WriteLine("1) Habitación Individual");
        Console.WriteLine("2) Habitación Doble");
        Console.WriteLine("3) Suite");

        RoomType desiredRoomType;
        do
        {
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    desiredRoomType = RoomType.Individual;
                    break;
                case "2":
                    desiredRoomType = RoomType.Doble;
                    break;
                case "3":
                    desiredRoomType = RoomType.Suite;
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    continue;
            }

            if (!_staff.GetRoomsAvailability(desiredRoomType))
            {
                Console.WriteLine("Lo siento, no hay habitaciones disponibles del tipo deseado.");
                continue;
            }

            // Calcula el precio de la reserva
            float bookingPrice = _staff.GetBookingPrice(clientId, desiredRoomType, bookedNights);


            // Donde se realiza la reserva
            Booking newBooking = _staff.Book(clientName, clientId, DateTime.Now, bookedNights, desiredRoomType);

            if (newBooking != null)
            {
                Console.WriteLine("Reserva creada exitosamente.");
            }
            else
            {
                Console.WriteLine("Error al crear la reserva.");
            }

            break; // Sale del bucle una vez que se ha realizado la reserva.
        } while (true);

    }
}