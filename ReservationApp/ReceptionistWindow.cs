using Gtk;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ReservationApp
{
    public class ReceptionistWindow : Window
    {
        private Receptionist _receptionist;
        private ListStore roomStore;
        private VBox mainArea; // Contenedor principal donde cambiaremos los subpaneles

        private List<RoomType> _roomTypes = new List<RoomType>
        {
            RoomType.Simple, RoomType.Double, RoomType.Matrimonial
        };

        public ReceptionistWindow(string username) : base("Recepcionista - " + username)
        {
            _receptionist = new Receptionist(username, DbController.CargarArchivo());
            SetDefaultSize(1024, 768);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate
            {
                DbController.SaveFile(_receptionist.Db);
                Application.Quit();
            };

            // Crear un VBox para dividir la ventana en una barra de navegación y una área principal
            VBox mainContainer = new VBox(false, 0);

            // Crear HBox para añadir la barra lateral y el área principal
            HBox contentBox = new HBox(false, 0);

            // Crear barra lateral de navegación
            VBox navBar = new VBox(false, 10)
            {
                Margin = 10
            };

            // Añadir elementos a la barra lateral
            Label headerLabel = new Label("RE SOFT S.A.S.")
            {
                Markup = "<span size='xx-large' weight='bold'>RE SOFT S.A.S.</span>",
                Justify = Justification.Center
            };
            navBar.PackStart(headerLabel, false, false, 20);

            Label hotelLabel = new Label("Hotel XXXX");
            navBar.PackStart(hotelLabel, false, false, 10);

            Button homeButton = new Button("Inicio");
            navBar.PackStart(homeButton, false, false, 10);

            Button availabilityButton = new Button("Ver disponibilidad");
            navBar.PackStart(availabilityButton, false, false, 10);

            Button bookRoomButton = new Button("Reservar Habitación");
            navBar.PackStart(bookRoomButton, false, false, 10);

            Button loadBookingsButton = new Button("Ver reservas activas");
            navBar.PackStart(loadBookingsButton, false, false, 10);

            // Crear el área principal
            mainArea = new VBox(false, 10)
            {
                Margin = 10
            };

            // Añadir etiqueta de bienvenida
            Label welcomeLabel = new Label("Bienvenido, " + username);
            mainArea.PackStart(welcomeLabel, false, false, 10);

            // Evento del botón "Ver disponibilidad"
            availabilityButton.Clicked += (sender, e) => { LoadAvailabilityPanel(); };

            // Evento del botón "Reservar Habitación"
            bookRoomButton.Clicked += (sender, e) => { LoadBookRoomPanel(); };

            // Evento del botón "Ver reservas activas"
            loadBookingsButton.Clicked += (sender, e) => { LoadBookingListPanel(); };

            // Añadir barra lateral y área principal al contenedor principal
            contentBox.PackStart(navBar, false, false, 0);
            contentBox.PackStart(mainArea, true, true, 0);

            // Añadir contenedor principal a la ventana
            mainContainer.PackStart(contentBox, true, true, 0);
            Add(mainContainer);

            ShowAll();
        }

        private TreeView CreateRoomTreeView()
        {
            // Configurar el store para el TreeView
            roomStore = new ListStore(typeof(string), typeof(int), typeof(string));
            TreeView treeView = new TreeView(roomStore);

            // Definir las columnas
            TreeViewColumn roomTypeColumn = new TreeViewColumn { Title = "Tipo de habitación" };
            CellRendererText roomTypeCell = new CellRendererText();
            roomTypeColumn.PackStart(roomTypeCell, true);
            roomTypeColumn.AddAttribute(roomTypeCell, "text", 0);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "ID" };
            CellRendererText idCell = new CellRendererText();
            idColumn.PackStart(idCell, true);
            idColumn.AddAttribute(idCell, "text", 1);

            TreeViewColumn stateColumn = new TreeViewColumn { Title = "Estado" };
            CellRendererText stateCell = new CellRendererText();
            stateColumn.PackStart(stateCell, true);
            stateColumn.AddAttribute(stateCell, "text", 2);

            // Añadir las columnas al TreeView
            treeView.AppendColumn(roomTypeColumn);
            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(stateColumn);

            return treeView;
        }

        private void LoadRoomData()
        {
            // Limpiar datos anteriores
            roomStore.Clear();

            // Añadir datos al ListStore
            foreach (Room room in _receptionist.Db.Rooms)
            {
                roomStore.AppendValues(room.Type.Type, room.Id, room.occupied ? "Ocupada" : "Disponible");
            }
        }

        private void LoadAvailabilityPanel()
        {
            // Limpiar el área principal
            ClearMainArea();

            // Añadir título
            Label titleLabel = new Label
            {
                Text = "Ver habitaciones",
                Markup = "<span size='large'>Ver habitaciones</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Crear la tabla de habitaciones
            TreeView roomTreeView = CreateRoomTreeView();

            // Añadir la tabla de habitaciones al área principal
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(roomTreeView);
            mainArea.PackStart(scrolledWindow, true, true, 10);

            // Cargar los datos
            LoadRoomData();
            mainArea.ShowAll();
        }

        private void LoadBookRoomPanel()
        {
            // Limpiar el área principal
            ClearMainArea();

            // Añadir título
            Label titleLabel = new Label
            {
                Text = "Reservar Habitación",
                Markup = "<span size='large'>Reservar Habitación</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Crear un HBox para dividir en dos columnas
            HBox columnsBox = new HBox(false, 20);

            // Crear las dos columnas como VBoxes
            VBox column1 = new VBox(false, 10);
            VBox column2 = new VBox(false, 10);

            // Componentes de la Columna 1
            Label nameLabel = new Label("Nombre:");
            Entry nameEntry = new Entry();
            Label dateInLabel = new Label("Fecha de entrada:");
            Calendar dateInCalendar = new Calendar();
            Label roomTypeLabel = new Label("Tipo de habitación:");
            ComboBoxText roomTypeComboBox = new ComboBoxText();
            foreach (var roomType in _roomTypes)
            {
                roomTypeComboBox.AppendText(roomType.Type);
            }

            Label costLabel = new Label("Costo de la reserva: $0.00");

            // Añadir componentes a la columna 1
            column1.PackStart(nameLabel, false, false, 10);
            column1.PackStart(nameEntry, false, false, 10);
            column1.PackStart(dateInLabel, false, false, 10);
            column1.PackStart(dateInCalendar, false, false, 10);
            column1.PackStart(roomTypeLabel, false, false, 10);
            column1.PackStart(roomTypeComboBox, false, false, 10);
            column1.PackStart(costLabel, false, false, 10);

            // Componentes de la Columna 2
            Label clientIdLabel = new Label("ID del cliente:");
            Entry clientIdEntry = new Entry();
            Label dateOutLabel = new Label("Fecha de salida:");
            Calendar dateOutCalendar = new Calendar();
            Button submitButton = new Button("Reservar");

            // Añadir el evento del botón para reservar
            submitButton.Clicked += (sender, e) =>
            {
                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(nameEntry.Text) || string.IsNullOrWhiteSpace(clientIdEntry.Text) ||
                    roomTypeComboBox.Active == -1 || !uint.TryParse(clientIdEntry.Text, out uint clientId))
                {
                    MessageDialog errorDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error,
                        ButtonsType.Ok, "Por favor completa todos los campos correctamente.");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }

                // Validar que las fechas sean válidas
                DateTime dateIn = new DateTime(dateInCalendar.Date.Year, dateInCalendar.Date.Month + 1,
                    dateInCalendar.Date.Day);
                DateTime dateOut = new DateTime(dateOutCalendar.Date.Year, dateOutCalendar.Date.Month + 1,
                    dateOutCalendar.Date.Day);
                if (dateOut <= dateIn)
                {
                    MessageDialog errorDialog = new MessageDialog(this, DialogFlags.Modal, MessageType.Error,
                        ButtonsType.Ok, "La fecha de salida debe ser posterior a la fecha de entrada.");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }

                // Extraer datos del formulario
                string name = nameEntry.Text;
                RoomType roomType = _roomTypes.First(roomType => roomType.Type == roomTypeComboBox.ActiveText);
                uint bookedNights = (uint)(dateOut - dateIn).Days;

                // Añade la lógica para reservar la habitación
                _receptionist.Book(name, clientId, dateIn, bookedNights, roomType);
            };

            // Añadir componentes a la columna 2
            column2.PackStart(clientIdLabel, false, false, 10);
            column2.PackStart(clientIdEntry, false, false, 10);
            column2.PackStart(dateOutLabel, false, false, 10);
            column2.PackStart(dateOutCalendar, false, false, 10);
            column2.PackStart(submitButton, false, false, 10);

            // Añadir las columnas al HBox
            columnsBox.PackStart(column1, true, true, 10);
            columnsBox.PackStart(column2, true, true, 10);

            // Añadir el HBox con las dos columnas al área principal
            mainArea.PackStart(columnsBox, true, true, 10);

            // Método para calcular el precio
            void CalculatePrice()
            {
                if (roomTypeComboBox.Active == -1)
                {
                    costLabel.Text = "Costo de la reserva: $0.00";
                    return;
                }

                DateTime dateIn = new DateTime(dateInCalendar.Date.Year, dateInCalendar.Date.Month + 1,
                    dateInCalendar.Date.Day);
                DateTime dateOut = new DateTime(dateOutCalendar.Date.Year, dateOutCalendar.Date.Month + 1,
                    dateOutCalendar.Date.Day);

                if (dateOut <= dateIn)
                {
                    costLabel.Text = "Costo de la reserva: $0.00";
                    return;
                }

                uint bookedNights = (uint)(dateOut - dateIn).Days;
                RoomType roomType = _roomTypes.First(rt => rt.Type == roomTypeComboBox.ActiveText);
                double cost = bookedNights * roomType.Price; // Suponiendo que RoomType tiene una propiedad CostPerNight

                costLabel.Text = $"Costo de la reserva: ${cost:F2}";
            }

            // Añadir eventos para los componentes que afectan el precio
            dateInCalendar.DaySelected += (sender, e) => CalculatePrice();
            dateOutCalendar.DaySelected += (sender, e) => CalculatePrice();
            roomTypeComboBox.Changed += (sender, e) => CalculatePrice();

            mainArea.ShowAll();
        }

        private void LoadBookingListPanel()
        {
            // Limpiar el área principal
            ClearMainArea();

            // Añadir título
            Label titleLabel = new Label
            {
                Text = "Lista de reservas",
                Markup = "<span size='large'>Lista de Reservas</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Crear un contenedor para mostrar las reservas y el total
            VBox invoiceBox = new VBox();

            // Crear ScrolledWindow y agregar invoiceBox
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(invoiceBox);
            scrolledWindow.VscrollbarPolicy = PolicyType.Automatic;
            scrolledWindow.HscrollbarPolicy = PolicyType.Automatic;

            mainArea.PackStart(scrolledWindow, true, true, 10);

            // Iterar sobre las reservas y agregarlas a la factura
            double totalAmount = 0;
            foreach (var booking in _receptionist.Db.Bookings)
            {
                // Crear un marco para la reserva
                Frame bookingFrame = new Frame();
                bookingFrame.BorderWidth = 5;
                bookingFrame.ShadowType = ShadowType.Out;

                // Detalles de la reserva
                Label bookingIdLabel = new Label($"Reserva #{booking.Id}");
                Label clientLabel = new Label($"Cliente: {booking.Client.Name}");
                Label roomLabel = new Label($"Habitación: {booking.Room.Id}");
                Label startLabel = new Label($"Inicio: {booking.Start}");
                Label endLabel = new Label($"Fin: {booking.End}");
                Label priceLabel = new Label($"Precio Total: {booking.Price}");

                // Crear una disposición para los detalles de la reserva
                VBox bookingDetailsBox = new VBox();
                bookingDetailsBox.PackStart(bookingIdLabel, false, false, 0);
                bookingDetailsBox.PackStart(clientLabel, false, false, 0);
                bookingDetailsBox.PackStart(roomLabel, false, false, 0);
                bookingDetailsBox.PackStart(startLabel, false, false, 0);
                bookingDetailsBox.PackStart(endLabel, false, false, 0);
                bookingDetailsBox.PackStart(priceLabel, false, false, 0);

                // Agregar los detalles de la reserva al marco
                bookingFrame.Add(bookingDetailsBox);

                // Agregar el marco a la factura
                invoiceBox.PackStart(bookingFrame, false, false, 10);

                // Agregar el precio total al total general
                totalAmount += booking.Price;
            }

            // Mostrar el total general
            Label totalLabel = new Label($"Total General: {totalAmount}");
            invoiceBox.PackEnd(totalLabel, false, false, 10);

            // Mostrar todo
            mainArea.ShowAll();
        }

        private void ClearMainArea()
        {
            foreach (Widget widget in mainArea)
            {
                mainArea.Remove(widget);
                widget.Destroy();
            }
        }
    }
}