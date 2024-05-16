using Gtk;
using System;
using System.Collections.Generic;

namespace ReservationApp
{
    public class ReceptionistWindow : Window
    {
        private Receptionist _receptionist;
        private ListStore roomStore;

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

            // Crear el área principal
            VBox mainArea = new VBox(false, 10)
            {
                Margin = 10
            };

            // Añadir etiqueta de bienvenida
            Label welcomeLabel = new Label("Bienvenido, " + username);
            mainArea.PackStart(welcomeLabel, false, false, 10);

            // Añadir título
            Label titleLabel = new Label
            {
                Text = "Ver habitaciones",
                Markup = "<span size='large'>Ver habitaciones</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Crear la tabla de habitaciones
            TreeView roomTreeView = CreateRoomTreeView();

            // Añadir tabla de habitaciones al área principal
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(roomTreeView);
            mainArea.PackStart(scrolledWindow, true, true, 10);

            // Añadir barra lateral y área principal al contenedor principal
            contentBox.PackStart(navBar, false, false, 0);
            contentBox.PackStart(mainArea, true, true, 0);

            // Añadir contenedor principal a la ventana
            mainContainer.PackStart(contentBox, true, true, 0);
            Add(mainContainer);

            // Cargar datos en la tabla
            LoadRoomData();

            ShowAll();
        }

        private TreeView CreateRoomTreeView()
        {
            // Configurar el store para el TreeView
            roomStore = new ListStore(typeof(string), typeof(int), typeof(int), typeof(string));
            TreeView treeView = new TreeView(roomStore);

            // Definir las columnas
            TreeViewColumn roomTypeColumn = new TreeViewColumn { Title = "Tipo de habitación" };
            CellRendererText roomTypeCell = new CellRendererText();
            roomTypeColumn.PackStart(roomTypeCell, true);
            roomTypeColumn.AddAttribute(roomTypeCell, "text", 0);

            TreeViewColumn roomNumberColumn = new TreeViewColumn { Title = "Número de habitación" };
            CellRendererText roomNumberCell = new CellRendererText();
            roomNumberColumn.PackStart(roomNumberCell, true);
            roomNumberColumn.AddAttribute(roomNumberCell, "text", 1);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "ID" };
            CellRendererText idCell = new CellRendererText();
            idColumn.PackStart(idCell, true);
            idColumn.AddAttribute(idCell, "text", 2);

            TreeViewColumn stateColumn = new TreeViewColumn { Title = "Estado" };
            CellRendererText stateCell = new CellRendererText();
            stateColumn.PackStart(stateCell, true);
            stateColumn.AddAttribute(stateCell, "text", 3);

            // Añadir las columnas al TreeView
            treeView.AppendColumn(roomTypeColumn);
            treeView.AppendColumn(roomNumberColumn);
            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(stateColumn);

            return treeView;
        }

        private void LoadRoomData()
        {
            // Añadir datos al ListStore
            foreach (Room room in _receptionist.Db.Rooms)
            {
                roomStore.AppendValues(room.Type.Type, room.Id, room.occupied);
            }
        }
    }
}