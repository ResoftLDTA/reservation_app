using Gtk;
using System;

namespace ReservationApp;

public class AdminWindow : Window
{
    private Admin _admin;

    public AdminWindow(string username) : base("Administrador - " + username)
    {
        _admin = new Admin(username, DbController.ReadFile());

        SetDefaultSize(800, 600);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate
        {
            DbController.SaveFile(_admin.Db);
            Application.Quit();
        };

        // Crear un Grid
        Grid grid = new Grid();
        grid.ColumnSpacing = 10;
        grid.RowSpacing = 10;
        grid.Margin = 10;

        // Añadir widgets específicos de administrador

        // Ejemplo de etiqueta de bienvenida
        Label welcomeLabel = new Label("Bienvenido, " + username);

        // Añadir el Grid a la ventana
        grid.Attach(welcomeLabel, 0, 0, 1, 1);

        Add(grid);
        ShowAll();
    }
}