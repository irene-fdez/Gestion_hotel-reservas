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
            
            this.View.opInsertarH.Click += (sender, e) => this.InsertaHabitacion();
            this.View.opConsultaH.Click += (sender, e) => this.ConsultaHabitaciones();
            
            this.View.btnAddReserva.Click += (sender, e) => this.InsertaReserva();
            this.View.btnConsultaReserva.Click += (sender, e) => this.ConsultaReserva();
           
        }

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
                       h, dlgInsertaReserva.TipoHabitacion, c, dlgInsertaReserva.FechaEntrada, 
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

        
        void InsertaHabitacion()
        {
            Console.WriteLine("Inserta Habitacion");
            var dlgInsertaHabitacion = new DIgInsertaHabitaciones(this.Habitaciones, Habitaciones.List);
            

            this.View.Hide();

            if (dlgInsertaHabitacion.ShowDialog() == DialogResult.OK)
            {

                Habitacion h = this.Habitaciones.getHabitacion(dlgInsertaHabitacion.NumHabitacion);

                // obtener a apartir de un string un enum
                Habitacion.Tipos parsedTipo = default(Habitacion.Tipos);
                var element =dlgInsertaHabitacion.TipoHabitacion;
                if(element != null)
                {
                    // Try to parse
                    Enum.TryParse<Habitacion.Tipos>(element, out parsedTipo);
                }
                 
                Console.WriteLine("Numero de habitacion " + dlgInsertaHabitacion.NumHabitacion + "\ntipo " +parsedTipo  + "\n Wfi " + dlgInsertaHabitacion.Wifi 
                                  + "\n cj " + dlgInsertaHabitacion.CajaFuerte + "\n minib " + dlgInsertaHabitacion.MiniBar + "\n Baño " + dlgInsertaHabitacion.Baño + "\n Cocina " + dlgInsertaHabitacion.Cocina
                                  + "\n Tv " );
                //obtener el cliente a partir del DNI con getCliente(DNI) del registro de clientes
                Habitacion newHabitacion =  new Habitacion(
                    dlgInsertaHabitacion.NumHabitacion , parsedTipo , 
                     dlgInsertaHabitacion.FechaRenova, 
                    dlgInsertaHabitacion.UltimaReserva, Convert.ToBoolean(dlgInsertaHabitacion.Wifi), 
                     Convert.ToBoolean(dlgInsertaHabitacion.CajaFuerte),
                     Convert.ToBoolean(dlgInsertaHabitacion.MiniBar),
                     Convert.ToBoolean(dlgInsertaHabitacion.Baño),
                     Convert.ToBoolean(dlgInsertaHabitacion.Cocina),
                     Convert.ToBoolean( dlgInsertaHabitacion.Tv)
                );

                if (this.Reservas.comprobarId(newHabitacion.Numero))
                {
                    string mensaje = "No se ha insertado la reserva, porque el ID ya pertenece a una reserva del registro";
                    string tittle = "Error al insertar";
                    MessageBox.Show(mensaje, tittle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    this.Habitaciones.Add(newHabitacion);

                    this.Habitaciones.GuardarXml();
                }
                
            }

            if (!this.View.IsDisposed) { this.View.Show(); }
            else { Application.Exit(); }
        }
        
        
        void ConsultaHabitaciones()
        {
            Console.WriteLine("Consulta Habitaciones");
            var dlgConsultaHabitaicon= new DIgConsiultaHabitacion(this.Habitaciones);


            this.View.Hide();

            if(dlgConsultaHabitaicon.ShowDialog() == DialogResult.OK) { }

            if (!this.View.IsDisposed) { this.View.Show(); }
            else{  Application.Exit();  }

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

    }
}
