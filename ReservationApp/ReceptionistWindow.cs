using Gtk;
using System;
using System.Collections.Generic;

namespace ReservationApp
{
    public class ReceptionistWindow : Window
    {
        private Receptionist _receptionist;
        private ListStore roomStore;
        private VBox mainArea; // Contenedor principal donde cambiaremos los subpaneles

        public ReceptionistWindow(string username) : base("Recepcionista - " + username)
        {
            _receptionist = new Receptionist(username, DbController.CargarArchivo());

            SetDefaultSize(1024, 768);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { Application.Quit(); };

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

            Button calculateBookingButton = new Button("Calcular Reserva");
            navBar.PackStart(calculateBookingButton, false, false, 10);

            // Crear el área principal
            mainArea = new VBox(false, 10)
            {
                Margin = 10
            };

            // Añadir etiqueta de bienvenida
            Label welcomeLabel = new Label("Bienvenido, " + username);
            mainArea.PackStart(welcomeLabel, false, false, 10);

            // Evento del botón "Ver disponibilidad"
            availabilityButton.Clicked += (sender, e) =>
            {
                LoadAvailabilityPanel();
            };

            // Evento del botón "Reservar Habitación"
            bookRoomButton.Clicked += (sender, e) =>
            {
                LoadBookRoomPanel();
            };

            // Evento del botón "Calcular Reserva"
            calculateBookingButton.Clicked += (sender, e) =>
            {
                LoadCalculateBookingPanel();
            };

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

            // Formularios para reservar una habitación
            Label nameLabel = new Label("Nombre:");
            Entry nameEntry = new Entry();
            Label roomIdLabel = new Label("ID de habitación:");
            Entry roomIdEntry = new Entry();
            Label dateInLabel = new Label("Fecha de entrada:");
            Entry dateInEntry = new Entry();
            Label dateOutLabel = new Label("Fecha de salida:");
            Entry dateOutEntry = new Entry();

            Button submitButton = new Button("Reservar");
            submitButton.Clicked += (sender, e) =>
            {
                // Lógica para añadir reserva
                string name = nameEntry.Text;
                int roomId = Int32.Parse(roomIdEntry.Text);
                DateTime dateIn = DateTime.Parse(dateInEntry.Text);
                DateTime dateOut = DateTime.Parse(dateOutEntry.Text);
                // _receptionist.Book(name, roomId, dateIn, dateOut);
            };

            // Añadir formularios al área principal
            mainArea.PackStart(nameLabel, false, false, 10);
            mainArea.PackStart(nameEntry, false, false, 10);
            mainArea.PackStart(roomIdLabel, false, false, 10);
            mainArea.PackStart(roomIdEntry, false, false, 10);
            mainArea.PackStart(dateInLabel, false, false, 10);
            mainArea.PackStart(dateInEntry, false, false, 10);
            mainArea.PackStart(dateOutLabel, false, false, 10);
            mainArea.PackStart(dateOutEntry, false, false, 10);
            mainArea.PackStart(submitButton, false, false, 10);

            mainArea.ShowAll();
        }

        private void LoadCalculateBookingPanel()
        {
            // Limpiar el área principal
            ClearMainArea();

            // Añadir título
            Label titleLabel = new Label
            {
                Text = "Calcular Reserva",
                Markup = "<span size='large'>Calcular Reserva</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Formularios para calcular una reserva
            Label nameLabel = new Label("Nombre:");
            Entry nameEntry = new Entry();
            Label roomIdLabel = new Label("ID de habitación:");
            Entry roomIdEntry = new Entry();
            Label dateInLabel = new Label("Fecha de entrada:");
            Entry dateInEntry = new Entry();
            Label dateOutLabel = new Label("Fecha de salida:");
            Entry dateOutEntry = new Entry();

            Button calculateButton = new Button("Calcular");
            calculateButton.Clicked += (sender, e) =>
            {
                // Lógica para calcular reserva (puedes añadir funcionalidad aquí)
                string name = nameEntry.Text;
                int roomId = Int32.Parse(roomIdEntry.Text);
                DateTime dateIn = DateTime.Parse(dateInEntry.Text);
                DateTime dateOut = DateTime.Parse(dateOutEntry.Text);

                // Muestra el resultado, puedes reemplazar esto con funcionalidad real
                MessageDialog resultDialog = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Info,
                    ButtonsType.Ok,
                    $"Calculando reserva para {name} en la habitación {roomId} desde {dateIn} hasta {dateOut}. Funcionalidad aún no implementada."
                );
                resultDialog.Run();
                resultDialog.Destroy();
            };

            // Añadir formularios al área principal
            mainArea.PackStart(nameLabel, false, false, 10);
            mainArea.PackStart(nameEntry, false, false, 10);
            mainArea.PackStart(roomIdLabel, false, false, 10);
            mainArea.PackStart(roomIdEntry, false, false, 10);
            mainArea.PackStart(dateInLabel, false, false, 10);
            mainArea.PackStart(dateInEntry, false, false, 10);
            mainArea.PackStart(dateOutLabel, false, false, 10);
            mainArea.PackStart(dateOutEntry, false, false, 10);
            mainArea.PackStart(calculateButton, false, false, 10);

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