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

            // Create main container
            HBox mainContainer = new HBox(false, 0);

            // Create sidebar
            VBox sidebar = new VBox(false, 10)
            {
                Margin = 10
            };

            // Add elements to sidebar
            Label headerLabel = new Label("RE SOFT S.A.S.")
            {
                Markup = "<span size='xx-large' weight='bold'>RE SOFT S.A.S.</span>",
                Justify = Justification.Center
            };
            sidebar.PackStart(headerLabel, false, false, 20);
            
            Label hotelLabel = new Label("Hotel XXXX");
            sidebar.PackStart(hotelLabel, false, false, 10);

            Button homeButton = new Button("Inicio");
            sidebar.PackStart(homeButton, false, false, 10);

            Button availabilityButton = new Button("Ver disponibilidad");
            sidebar.PackStart(availabilityButton, false, false, 10);

            // Create main area
            VBox mainArea = new VBox(false, 10)
            {
                Margin = 10
            };

            // Add welcome label
            Label welcomeLabel = new Label("Bienvenido, " + username);
            mainArea.PackStart(welcomeLabel, false, false, 10);

            // Add title label
            Label titleLabel = new Label
            {
                Text = "Ver habitaciones",
                Markup = "<span size='large'>Ver habitaciones</span>"
            };
            mainArea.PackStart(titleLabel, false, false, 10);

            // Create room table
            TreeView roomTreeView = CreateRoomTreeView();

            // Add room table to main area
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(roomTreeView);
            mainArea.PackStart(scrolledWindow, true, true, 10);

            // Add sidebar and main area to the main container
            mainContainer.PackStart(sidebar, false, false, 0);
            mainContainer.PackStart(mainArea, true, true, 0);

            // Add main container to the window
            Add(mainContainer);
            ShowAll();
        }

        private TreeView CreateRoomTreeView()
        {
            // Set up the store for the tree view
            roomStore = new ListStore(typeof(string), typeof(int), typeof(int), typeof(int), typeof(string));
            TreeView treeView = new TreeView(roomStore);

            // Define the columns
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

            // Append the columns to the tree view
            treeView.AppendColumn(roomTypeColumn);
            treeView.AppendColumn(roomNumberColumn);
            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(stateColumn);

            // Load data into the store
            LoadRoomData();

            return treeView;
        }

        private void LoadRoomData()
        {
            foreach (Room room in _receptionist.Db.Rooms)
            {
                roomStore.AppendValues(room.Type.Type, room.Id, room.occupied);
            }
        }
    }
}