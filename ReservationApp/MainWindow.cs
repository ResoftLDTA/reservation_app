using Gtk;
using System;

namespace ReservationApp;

public class MainWindow : Window
{
    public MainWindow() : base("ReservationApp - Inicio de Sesión")
    {
        SetDefaultSize(400, 200);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Application.Quit(); };

        // Crear un Grid
        Grid grid = new Grid();
        grid.ColumnSpacing = 10;
        grid.RowSpacing = 10;
        grid.Margin = 10;

        // Etiqueta y entrada para el nombre del usuario
        Label nameLabel = new Label("Nombre de usuario:");
        Entry nameEntry = new Entry();

        // Etiqueta y ComboBox para seleccionar el rol
        Label roleLabel = new Label("Selecciona tu rol:");
        ComboBoxText roleComboBox = new ComboBoxText();
        roleComboBox.AppendText("Recepcionista");
        roleComboBox.AppendText("Administrador");
        roleComboBox.Active = 0; // Selecciona el primer elemento por defecto

        // Botón para iniciar sesión
        Button loginButton = new Button("Iniciar Sesión");
        loginButton.Clicked += (sender, e) =>
        {
            string username = nameEntry.Text;
            string role = roleComboBox.ActiveText; // Obtener el texto seleccionado

            // Validación simple
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageDialog errorMsg = new MessageDialog(
                    this,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "Por favor ingrese el nombre de usuario."
                );
                errorMsg.Run();
                errorMsg.Destroy();
                return;
            }

            if (role == "Recepcionista")
            {
                new ReceptionistWindow(username).Show();
            }
            else if (role == "Administrador")
            {
                new AdminWindow(username).Show();
            }

            // Cerrar la ventana de inicio de sesión
            this.Destroy();
        };

        // Añadir widgets al Grid
        grid.Attach(nameLabel, 0, 0, 1, 1);
        grid.Attach(nameEntry, 1, 0, 2, 1);

        grid.Attach(roleLabel, 0, 1, 1, 1);
        grid.Attach(roleComboBox, 1, 1, 2, 1);

        grid.Attach(loginButton, 0, 2, 3, 1);

        // Añadir Grid a la ventana
        Add(grid);

        ShowAll();
    }
}