using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionReservas.Core;
using GestionReservas.GUI.Dlg;

namespace GestionReservas.GUI
{

    using System.Windows.Forms;
    using System.Drawing;

    public class MainWindowCore: Form
    {

        public MainWindowCore()
        {

            this.View = new MainWindowView();
            Console.Write("main core");
            this.Clientes = RegistroClientes.RecuperarXml();
            this.Habitaciones = RegistroHabitaciones.RecuperarXml();
            this.Reservas = new RegistroReserva(this.Clientes.List);
         

            this.View.FormClosed += (sender, e) => this.OnQuit();
            this.View.opGuardar.Click += (sender, e) => this.Guardar();
            this.View.opSalir.Click += (sender, e) => this.Salir();

            this.View.btnAddReserva.Click += (sender, e) => this.InsertaReserva();
            this.View.btnConsultaReserva.Click += (sender, e) => this.ConsultaReserva();
           
        }

        void InsertaReserva()
        {
            Console.WriteLine("Inserta reserva");
            var dlgInsertaReserva = new DlgInsertaReserva(Clientes.List, Habitaciones.List);
            

            this.View.Hide();

            if (dlgInsertaReserva.ShowDialog() == DialogResult.OK)
            {

                Habitacion h = this.Habitaciones.getHabitacion(dlgInsertaReserva.NumHabitacion);
                Cliente c = this.Clientes.getCliente(dlgInsertaReserva.DniCliente);

                //obtener el cliente a partir del DNI con getCliente(DNI) del registro de clientes
                   Reserva newReserva =  new Reserva(
                       h, dlgInsertaReserva.Tipo, c, dlgInsertaReserva.FechaEntrada, 
                       dlgInsertaReserva.FechaSalida, dlgInsertaReserva.Garaje, dlgInsertaReserva.PrecioDia, dlgInsertaReserva.Iva
                     );

                this.Reservas.Add(newReserva);

                this.Reservas.GuardarXml();
            }

            if (this.presionadoSalir)
            {
                Application.Exit();
            }

            this.View.Show();
        }

        void ConsultaReserva()
        {
            Console.WriteLine("Consulta reservas");
            var dlgConsultaReserva = new DlgConsultaReserva(this.Reservas, this.Clientes.List);


            this.View.Hide();

            if(dlgConsultaReserva.ShowDialog() == DialogResult.OK) { }

            if (this.presionadoSalir)
            {
                Application.Exit();
            }
            else
            {
                this.View.Show();
            }
        }





    /*    public void Eliminar(DlgConsultaReserva dlg, string id)
        {

            Console.WriteLine("dentro eliminar");
     

            //Dialogo de confirmación de eiminación
            DialogResult result;
            string mensaje = "¿Está seguro de que desea eliminar la reserva con Id( " + id + " ), del registro de reservas?";
            string tittle = "Eliminar reserva";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show(mensaje, tittle, buttons);

            if (result == DialogResult.Yes)
            {

                Console.WriteLine(this.Reservas.Count);
                Console.WriteLine("reserva a eliminar: " + id);
                Console.WriteLine(dlg.Reservas.Remove(dlg.Reservas.getReserva(id)));
                Console.WriteLine(this.Reservas.Count);
                Console.WriteLine(dlg.Reservas.Count);

            }


            
        }

        public void Modificar(string id)
        {
            var dlgConsultaReserva = new DlgConsultaReserva(this.Reservas.List, this.Clientes.List);
            //A partir de la clave de la entidad Reserva, obtenemos la reserva a modificar
            Reserva ResModif = this.Reservas.getReserva(id);
            Cliente c = ResModif.Cliente;

               var dlgModificar = new DlgModificarCuenta(ResModif);
               if (dlgModificar.ShowDialog() == DialogResult.OK)
               {
                   this.Reservas.Remove(ResModif);

                   string tipo = dlgModificar.Tipo;
                   DateTime fechaEntrada = dlgModificar.FechaEntrada;
                   DateTime fechaSalida = dlgModificar.FechaSalida;
                   string garaje = dlgModificar.Garaje;

                   Reserva r = new Reserva(id, tipo, c, fechaEntrada, fechaSalida, garaje, ResModif.PrecioDia, ResModif.IVA, ResModif.Total);


                   this.Reservas.Add(r);

               }
        }*/


        public void PulsadoSalir()
        {
            Console.WriteLine("pulsado salir");
            this.presionadoSalir = true;
            this.Salir();
        }


        void Salir()
        {
            Console.WriteLine("guarda y sale");
            this.Reservas.GuardarXml();
            Application.Exit();
        }

        void Guardar()
        {
            Console.WriteLine("dentro guardar");

            this.Reservas.GuardarXml();
        }

        void OnQuit()
        {
            Application.Exit();
        }

        public MainWindowView View { get; private set; }

        private RegistroReserva Reservas { get; set; }
        private RegistroClientes Clientes { get; set; }
        private RegistroHabitaciones Habitaciones { get; set; }

        private bool presionadoSalir = false;
        

    }
}
