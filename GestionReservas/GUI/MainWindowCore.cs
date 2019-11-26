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
            this.View.opConsultaC.Click += (sender, e) => this.ConsultaCliente();

        }
        /*
        void InsertaCliente()
        {
            Console.WriteLine("Inserta clienteeeeeee");
            var dlgInsertaCliente = new DlgInsertaCliente(this.Clientes);

            this.View.Hide();
            Console.WriteLine("pre if insertaCliente");
            if (dlgInsertaCliente.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine("dentro if insertaCliente");
                Cliente c = new Cliente(
                    dlgInsertaCliente.DNI, dlgInsertaCliente.Nombre, dlgInsertaCliente.Telefono, dlgInsertaCliente.Email, dlgInsertaCliente.DirPostal
                    );
                Console.WriteLine(c.ToString());
                this.Clientes.Add(c);
                this.Clientes.GuardarXml();
            }
            Console.WriteLine("DialogResult= " + DialogResult);
            if (!this.View.IsDisposed) { this.View.Show(); }
            else { Application.Exit(); }
        }
        */

        void InsertaReserva()
        {
            Console.WriteLine("Inserta reserva");
            var dlgInsertaReserva = new DlgInsertaReserva(this.Reservas, Clientes.List, Habitaciones.List);
            

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

                if (this.Reservas.comprobarId(newReserva.Id))
                {
                    string mensaje = "No se ha insertado la reserva, porque el ID ya pertenece a una reserva del registro";
                    string tittle = "Error al insertar";
                    MessageBox.Show(mensaje, tittle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    this.Reservas.Add(newReserva);

                    this.Reservas.GuardarXml();
                }
                
            }

            if (!this.View.IsDisposed) { this.View.Show(); }
            else { Application.Exit(); }
        }
        void ConsultaCliente()
        {
            Console.WriteLine("Consulta Clientes");
            var dlgConsultaCliente = new DlgConsultaCliente(this.Clientes);


            this.View.Hide();

            if (dlgConsultaCliente.ShowDialog() == DialogResult.OK) { }

            if (!this.View.IsDisposed) { this.View.Show(); }
            else { Application.Exit(); }

        }

        void ConsultaReserva()
        {
            Console.WriteLine("Consulta reservas");
            var dlgConsultaReserva = new DlgConsultaReserva(this.Reservas, this.Clientes.List);


            this.View.Hide();

            if(dlgConsultaReserva.ShowDialog() == DialogResult.OK) { }

            if (!this.View.IsDisposed) { this.View.Show(); }
            else{  Application.Exit();  }

        }


        public void Salir()
        {
            Console.WriteLine("MAINCORE guarda y sale");
            this.Reservas.GuardarXml();
            this.Clientes.GuardarXml();
            Application.Exit();
        }

        void Guardar()
        {
            Console.WriteLine("MAIN CORE dentro guardar");

            this.Reservas.GuardarXml();
            this.Clientes.GuardarXml();
        }

        void OnQuit()
        {
            Application.Exit();
        }

        public MainWindowView View { get; private set; }

        private RegistroReserva Reservas { get; set; }
        private RegistroClientes Clientes { get; set; }
        private RegistroHabitaciones Habitaciones { get; set; }

    }
}
