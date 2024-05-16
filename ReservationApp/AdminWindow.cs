using Gtk;
using System;
using System.Linq;

namespace ReservationApp
{
    public class AdminWindow : Window
    {
        private Admin _admin;
        private VBox _mainArea;

        public AdminWindow(string username) : base("Administrador - " + username)
        {
            _admin = new Admin(username, DbController.ReadFile());
            SetDefaultSize(800, 600);
            SetPosition(WindowPosition.Center);
            DeleteEvent += delegate { DbController.SaveFile(_admin.Db); Application.Quit(); };

            // Crear un HBox para dividir la ventana en una barra de navegación y un área de contenido
            HBox mainContainer = new HBox(false, 10);

            // Crear VBox para la barra de navegación
            VBox navBar = new VBox(false, 10) { Margin = 10 };

            // Etiqueta de bienvenida
            Label welcomeLabel = new Label("Bienvenido, " + username);
            navBar.PackStart(welcomeLabel, false, false, 10);

            // Botón para volver a inicio
            Button homeButton = new Button("Inicio") { Expand = false };
            navBar.PackStart(homeButton, false, false, 10);

            // Botón para mostrar ventas por mes
            Button salesButton = new Button("Mostrar Ventas por Mes") { Expand = false };
            navBar.PackStart(salesButton, false, false, 10);

            // Añadir eventos de los botones
            homeButton.Clicked += (sender, e) => ReturnToMainWindow();
            salesButton.Clicked += (sender, e) => ShowMonthlySales();

            // Crear el área principal de contenido
            _mainArea = new VBox(false, 10) { Margin = 10 };

            // Añadir VBox y mainArea al HBox principal
            mainContainer.PackStart(navBar, false, false, 0);
            mainContainer.PackStart(_mainArea, true, true, 0);

            // Añadir el HBox principal a la ventana
            Add(mainContainer);
            ShowAll();
        }

        private void ShowMonthlySales()
        {
            ClearMainArea();

            // Crear y configurar el TreeView
            TreeView salesTreeView = new TreeView();
            ListStore salesStore = new ListStore(typeof(string), typeof(string));  // Cambié el segundo tipo a string para formatear los decimales

            // Definir las columnas
            TreeViewColumn monthColumn = new TreeViewColumn { Title = "Mes" };
            CellRendererText monthCell = new CellRendererText();
            monthColumn.PackStart(monthCell, true);
            monthColumn.AddAttribute(monthCell, "text", 0);

            TreeViewColumn incomeColumn = new TreeViewColumn { Title = "Ingresos" };
            CellRendererText incomeCell = new CellRendererText();
            incomeColumn.PackStart(incomeCell, true);
            incomeColumn.AddAttribute(incomeCell, "text", 1);

            // Añadir las columnas al TreeView
            salesTreeView.AppendColumn(monthColumn);
            salesTreeView.AppendColumn(incomeColumn);
            salesTreeView.Model = salesStore;

            // Añadir ventas por mes al ListStore
            for (uint month = 1; month <= 12; month++)
            {
                float income = _admin.CalculateIncome(month);
                salesStore.AppendValues(GetMonthName(month), income.ToString("F2"));  // Formatear con dos decimales
            }

            // Añadir el TreeView dentro de un ScrolledWindow para la área principal
            ScrolledWindow scrolledWindow = new ScrolledWindow();
            scrolledWindow.Add(salesTreeView);
            _mainArea.PackStart(scrolledWindow, true, true, 10);

            _mainArea.ShowAll();
        }

        private void ClearMainArea()
        {
            foreach (Widget widget in _mainArea)
            {
                _mainArea.Remove(widget);
                widget.Destroy();
            }
        }

        private string GetMonthName(uint month)
        {
            // Método auxiliar para obtener el nombre del mes
            DateTime date = new DateTime(1, (int)month, 1);
            return date.ToString("MMMM");
        }

        private void ReturnToMainWindow()
        {
            // Guardar datos antes de salir
            DbController.SaveFile(_admin.Db);

            // Destruir la ventana actual
            this.Destroy();

            // Crear una nueva instancia de MainWindow y mostrarla
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}