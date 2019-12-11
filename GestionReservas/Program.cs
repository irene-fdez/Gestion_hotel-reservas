using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionReservas.GUI;
using GestionReservas.Core;


namespace GestionReservas
{
    using System.Windows.Forms;

    class Program
    {
        static void Main(string[] args)
        {

            var mainForm = new MainWindowCore().View;
            Application.Run(mainForm);

            var rc = new RegistroClientes();
            rc.Add(new Cliente("12345678A", "Carlos Perez", 65698745, "carlos@email.com", "av. Otero Pedrayo, 2. Ourense"));
            rc.Add(new Cliente("23456789B", "Juan Lopez", 652369874, "juan@email.com", "c/Rio Sil, 14. Ourense"));
            rc.Add(new Cliente("34567890C", "Maria Diaz", 689546231, "maria@email.com", "c/ Serrano, 28. MAdrid"));

            
            var rh = new RegistroHabitaciones();
            rh.Add(new Habitacion("001", Habitacion.Tipos.matrimoniales, DateTime.Today, DateTime.Today ,true,true,true,true,true,true));
            rh.Add(new Habitacion("002", Habitacion.Tipos.doble, DateTime.Today, DateTime.Today,true,false,true,true,true,true));
            rh.Add(new Habitacion("003", Habitacion.Tipos.individuales, DateTime.Today, DateTime.Today,true,true,false,true,true,true));
            rh.Add(new Habitacion("004", Habitacion.Tipos.individuales, DateTime.Today, DateTime.Today,true,true,true,false,true,true));


            rc.GuardarXml();
            //rh.GuardarXml();
            Console.ReadLine();
        }

    }

}
