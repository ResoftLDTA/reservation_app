using Gtk;
using System;

namespace ReservationApp;

public class ReceptionistWindow : Window
{
    private Receptionist _receptionist;

    public ReceptionistWindow(string username) : base("Recepcionista - " + username)
    {
        _receptionist = new Receptionist(username, DbController.CargarArchivo());

        SetDefaultSize(800, 600);
        SetPosition(WindowPosition.Center);
        DeleteEvent += delegate { Application.Quit(); };

        // Crear un Grid
        Grid grid = new Grid();
        grid.ColumnSpacing = 10;
        grid.RowSpacing = 10;
        grid.Margin = 10;

        // Añadir widgets específicos de recepcionista

        // Ejemplo de etiqueta de bienvenida
        Label welcomeLabel = new Label("Bienvenido, " + username);

        // Añadir el Grid a la ventana
        grid.Attach(welcomeLabel, 0, 0, 1, 1);

        Add(grid);
        ShowAll();
    }
}