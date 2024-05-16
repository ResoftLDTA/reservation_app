using Gtk;
using Gdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApp
{

    class ReservaMuestraScreen : Gtk.Window
    {
        public ReservaMuestraScreen() : base("RESOFT S.A.S.")
        {
           
            SetDefaultSize(600, 400);
            DeleteEvent += delegate { Application.Quit(); };

            // Crear un Gtk.Box vertical para los dos elementos principales
            VBox mainBox = new VBox(false, 2);
            Add(mainBox);

            // Primer elemento: Imagen
            Image image1 = new Image("Up.jpeg");
            mainBox.PackStart(image1, true, true, 0);

            // Segundo elemento: Un Gtk.Fixed para la imagen de fondo y los botones
            Fixed fixedContainer = new Fixed();
            mainBox.PackStart(fixedContainer, true, true, 0);

            // Establecer la imagen de fondo
            Image backgroundImage = new Image("Down.jpeg");
            fixedContainer.Put(backgroundImage, 0, 0);

            // Crear un Gtk.Box para los botones con fondo azul
            VBox buttonBox = new VBox(true, 1);
            buttonBox.SetSizeRequest(250, 300);
            fixedContainer.Put(buttonBox, 225, 100); // Posiciona el box de botones sobre la imagen

            // Aplicar CSS para el fondo azul y los botones
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromData(@"
            box { background-color: #545C7B; border-radius: 5px; padding: 10px; }
            button { background-color: #000000; color: white; border-radius: 5px; border: none; padding: 10px; }
            label{
                background-color: #545C7B; 
            }
            .hotel {background-color: #545C7B; color: white;}
        ");

            StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, uint.MaxValue);

            Label hotel = new Label("Hotel XXXX");
            buttonBox.PackStart(hotel, true, true, 10);

            // Crear y añadir los botones
            string[] buttonLabels = { "Inicio", "Buscar Reserva", "Realizar Reserva", "Facturacion" };
            foreach (var label in buttonLabels)
            {
                Button button = new Button(label);
                buttonBox.PackStart(button, true, true, 10);
            }
        }

    }
}
