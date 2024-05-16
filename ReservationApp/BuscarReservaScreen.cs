using Gdk;
using Gtk;

class BuscarReservaScreen : Gtk.Window
{
    Frame leftFrame;
    Frame rightFrame;

    public BuscarReservaScreen() : base("RESOFT S.A.S.")
    {
        SetDefaultSize(685, 481);

        // Aplicar estilos CSS
        CssProvider cssProvider = new CssProvider();
        cssProvider.LoadFromData(@"
            * {
                background-color: #6E7691; /* Fondo oscuro para todos los elementos */
                color: #ECEFF4; /* Color de texto general */
                font-family: 'Sans';
                font-size: 12px;
            }

            button {
                background-color: #6E7691; /* Color de fondo para botones */
                font-weight: bold; /* Fuente en negrita para botones */
                border-radius: 10px; /* Bordes redondeados para botones */
        
            }

            label{
                background-color: #6E7691; 
            }

            rightFrame{
                background-color: #6E7291; 
            }
        ");
        StyleContext.AddProviderForScreen(Screen.Default, cssProvider, uint.MaxValue);

        Box mainBox = new Box(Orientation.Vertical, 2);
        mainBox.BorderWidth = 10;
        HBox hbox = new HBox(false, 10);

        leftFrame = new Frame();
        rightFrame = new Frame();

        hbox.PackStart(leftFrame, false, false, 10);
        hbox.PackStart(rightFrame, true, true, 10);

        mainBox.PackStart(hbox, true, true, 10);
        Add(mainBox);

        SetupTreeView();
        SetupLeftPanel();
    }

    private void SetupLeftPanel()
    {
        Box buttonBox = new Box(Orientation.Vertical, 10);
        buttonBox.BorderWidth = 10;

        string[] labels = { "Inicio", "Buscar Reserva", "Realizar Reserva", "Facturaci�n" };
        foreach (string label in labels)
        {
            Button button = new Button(label);
            button.SetSizeRequest(150, 50);
            buttonBox.PackStart(button, false, false, 0);
        }

        leftFrame.Add(buttonBox);
    }

    private void SetupTreeView()
    {
        TreeView treeView = new TreeView();
        treeView.RulesHint = true;

        treeView.AppendColumn("ID Cliente", new CellRendererText(), "text", 0);
        treeView.AppendColumn("Tipo de Habitaci�n", new CellRendererText(), "text", 1);
        treeView.AppendColumn("N�mero de Noches", new CellRendererText(), "text", 2);

        ListStore store = new ListStore(typeof(string), typeof(string), typeof(string));
        store.AppendValues("2346920", "Matrimonial", "Mayo 3 - 24");
        store.AppendValues("485279", "Doble", "Abril 26 - Mayo 7");

        treeView.Model = store;

        rightFrame.Add(treeView);
    }
}
