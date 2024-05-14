using System;

namespace ReservationApp;

public class Frontend
{
    private Staff _staff;

    public Frontend(Staff staff)
    {
        _staff = staff;
    }

    public void Run()
    {
        bool run = true;
        while (run)
        {
            Menu();
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListBookings();
                    break;
                case "2":
                    CreateBooking();
                    break;
                case "3":
                    CancelBooking();
                    break;
                case "4":
                    run = false;
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    break;
            }
        }
        
    }

    private void Menu()
    {
        Console.WriteLine("Bienvenido a Reservation App.");
        Console.WriteLine("1) Para listar las reservas");
        Console.WriteLine("2) Para crear una reserva");
        Console.WriteLine("3) Para cancelar una reserva");
        Console.WriteLine("4) Para salir");
    }

    private void ListBookings()
    {
        foreach (Booking booking in _staff.Db.Bookings)
        {
            Console.WriteLine("**********************************");
            Console.WriteLine($"Reserva no. {booking.Id}");
            Console.WriteLine($"Cliente {booking.Client.Name}, id {booking.Client.Id}");
            Console.WriteLine($"Habitación no. {booking.Room.Id}, tipo {booking.Room.Type.Type}");
            Console.WriteLine($"Reserva desde {booking.Start.ToShortDateString()} hasta el {booking.End.ToShortDateString()}");
            Console.WriteLine($"Estuvo {booking.BookedNights} noches, para un costo total de {booking.Price}\n\n");
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
                    desiredRoomType = RoomType.Simple;
                    break;
                case "2":
                    desiredRoomType = RoomType.Double;
                    break;
                case "3":
                    desiredRoomType = RoomType.Matrimonial;
                    break;
                default:
                    Console.WriteLine("Opción no válida");
                    continue;
            }

            if (!_staff.GetRoomsAvailability(desiredRoomType))
            {
                Console.WriteLine("Lo siento, no hay habitaciones disponibles del tipo deseado.");
                break;
            }
            
            // Donde se realiza la reserva
            Booking newBooking = _staff.Book(clientName, clientId, DateTime.Now, bookedNights, desiredRoomType);
            Console.WriteLine("Reserva creada exitosamente.");

            break; // Sale del bucle una vez que se ha realizado la reserva.
        } while (true);
    }

    private void CancelBooking()
    {
        Console.WriteLine("Introduce el ID de la reserva que deseas cancelar:");
        int bookingId;
        do
        {
            try
            {
                string unparsedBookingId = Console.ReadLine();
                if (string.IsNullOrEmpty(unparsedBookingId))
                {
                    throw new NullReferenceException("El ID de la reserva no puede estar vacío.");
                }

                bookingId = int.Parse(unparsedBookingId);
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

        // Llamar al método de cancelar reserva en el backend
        _staff.CancelBooking(bookingId);
    }
}