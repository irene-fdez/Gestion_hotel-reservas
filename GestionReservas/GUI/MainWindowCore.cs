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
        public enum ViewChart {Months, Years, ClientesMes, ClientesAño, HabMes, HabAño, Comodidades};
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

            
            
            this.View.opConsultaC.Click += (sender, e) => this.ConsultaCliente();


            
            this.View.btnAddReserva.Click += (sender, e) => this.InsertaReserva();
            this.View.btnConsultaReserva.Click += (sender, e) => this.ConsultaReserva();
           
            
            this.View.opOcupacionGeneral.Click += (sender, e) => this.ViewChartOcupacionGeneral();
            this.View.opComodidadesHabitacion.Click += (sender, e) => this.ViewChartComodidadesHabitacion();
            this.View.opOcupacionCliente.Click += (sender, e) => this.ViewChartOcupacionCliente();
            this.View.opOcupacionHabitacion.Click += (sender, e) => this.ViewChartOcupacionHab();
        }

        
         private void ViewChartOcupacionGeneral()
        {
            this.ChartView = ViewChart.Months;
            List<int> valoresMes = this.Reservas.getReservasPorMes();
            DemoChart demoMonths = new DemoChart(valoresMes, this.ChartView.ToString());
            demoMonths.Show();

            demoMonths.Chart.Button.Click += (sender, e) => demoMonths.Close();
            demoMonths.Chart.Button.Click += (sender, e) => this.ViewYears();
        }

        private void ViewYears()
        {
            this.ChartView = ViewChart.Years;
            List<int> valoresAño = this.Reservas.getReservasPorAño();
            DemoChart demoYears = new DemoChart(valoresAño, this.ChartView.ToString());
            demoYears.Show();
            
            demoYears.Chart.Button.Click += (sender, e) => demoYears.Close();
            demoYears.Chart.Button.Click += (sender, e) => this.ViewChartOcupacionGeneral();
        }

        private void ViewChartComodidadesHabitacion()
        {
            this.ChartView = ViewChart.Comodidades;
            Dictionary<string, int> habitacionesPorComodidad = this.Habitaciones.getHabitacionesComodidad();
            List<string> comodidades = new List<string>();
            List<int> valores = new List<int>();
            foreach (var h in habitacionesPorComodidad)
            {
                comodidades.Add(h.Key);
                valores.Add(h.Value);
            }
            DemoChart demoComodidades = new DemoChart(valores, this.ChartView.ToString(), comodidades);
            demoComodidades.Show();
        }

        private void ViewChartOcupacionCliente()
        {
            this.ChartView = ViewChart.ClientesMes;
            Dictionary<string, int> ocupacionCliente = this.Reservas.getReservasPorClienteMes();
            List<string> clientes = new List<string>();
            List<int> valores = new List<int>();
            foreach (var c in ocupacionCliente)
            {
                clientes.Add(c.Key);
                valores.Add(c.Value);
            }
            DemoChart demoClientes = new DemoChart(valores, this.ChartView.ToString(), clientes);
            demoClientes.Show();
            demoClientes.Chart.Button.Click += (sender, e) => demoClientes.Close();
            demoClientes.Chart.Button.Click += (sender, e) => this.ViewChartOcupacionClienteAño();
        }
        
        private void ViewChartOcupacionClienteAño()
        {
            this.ChartView = ViewChart.ClientesAño;
            Dictionary<string, int> ocupacionCliente = this.Reservas.getReservasPorClienteAño();
            List<string> clientes = new List<string>();
            List<int> valores = new List<int>();
            foreach (var c in ocupacionCliente)
            {
                clientes.Add(c.Key);
                valores.Add(c.Value);
            }
            DemoChart demoClientes = new DemoChart(valores, this.ChartView.ToString(), clientes);
            demoClientes.Show();
            demoClientes.Chart.Button.Click += (sender, e) => demoClientes.Close();
            demoClientes.Chart.Button.Click += (sender, e) => this.ViewChartOcupacionCliente();
        }
        
        private void ViewChartOcupacionHab()
        {
            this.ChartView = ViewChart.HabMes;
            Dictionary<string, int> ocupacionHab = this.Reservas.getReservasPorHabitacionMes();
            List<string> hab = new List<string>();
            List<int> valores = new List<int>();
            foreach (var h in ocupacionHab)
            {
                hab.Add(h.Key);
                valores.Add(h.Value);
            }
            DemoChart demoHabitaciones = new DemoChart(valores, this.ChartView.ToString(), hab);
            demoHabitaciones.Show();
            demoHabitaciones.Chart.Button.Click += (sender, e) => demoHabitaciones.Close();
            demoHabitaciones.Chart.Button.Click += (sender, e) => this.ViewChartOcupacionHabAño();
        }
        
        private void ViewChartOcupacionHabAño()
        {
            this.ChartView = ViewChart.HabAño;
            Dictionary<string, int> ocupacionHab = this.Reservas.getReservasPorHabitacionAño();
            List<string> hab = new List<string>();
            List<int> valores = new List<int>();
            foreach (var h in ocupacionHab)
            {
                hab.Add(h.Key);
                valores.Add(h.Value);
            }
            DemoChart demoHab = new DemoChart(valores, this.ChartView.ToString(), hab);
            demoHab.Show();
            demoHab.Chart.Button.Click += (sender, e) => demoHab.Close();
            demoHab.Chart.Button.Click += (sender, e) => this.ViewChartOcupacionHab();
        }
   

        void InsertaReserva()
        {
            Console.WriteLine("Inserta reserva");
            var Habita = RegistroHabitaciones.RecuperarXml();

            var dlgInsertaReserva = new DlgInsertaReserva(this.Reservas, Clientes.List, Habita.List);
            

            this.View.Hide();

            if (dlgInsertaReserva.ShowDialog() == DialogResult.OK)
            {

                Habitacion h = this.Habitaciones.getHabitacion(dlgInsertaReserva.NumHabitacion);

                this.Clientes = RegistroClientes.RecuperarXml();
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
        private ViewChart ChartView { get; set; }

    }
}
