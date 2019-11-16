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
            Console.Write("main core");
            this.Clientes = RegistroClientes.RecuperarXml();
            this.Reservas = RegistroReserva.RecuperarXml();

            this.View = new MainWindowView();
           // this.AddReserva = new DlgInsertaReserva(Clientes.List);
           /* try { 
                this.Actualiza();
            }catch(Exception e)
            {
                Console.WriteLine("\nerror actualiza, " + e);
            }*/
            this.View.FormClosed += (sender, e) => this.OnQuit();
            this.View.opSalir.Click += (sender, e) => this.Salir();

       //     try
        //    {
                this.View.btnAddReserva.Click += (sender, e) => this.InsertaReserva();
                this.View.btnConsultaReserva.Click += (sender, e) => this.ConsultaReserva();
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine("\nerror btnAdd, msg:" + e);
            //    }


        }

        void InsertaReserva()
        {
            Console.WriteLine("Inserta reserva");
            var dlgInsertaReserva = new DlgInsertaReserva(Clientes.List);
            

            this.View.Hide();

            if (dlgInsertaReserva.ShowDialog() == DialogResult.OK)
            {
                Reserva reserva = null;

                //   Habitacion h = this.Habitacion.getHabitacion(dlgInsertaReserva.NumHabitacion);
                Cliente c = this.Clientes.getCliente(dlgInsertaReserva.DniCliente);

                //obtener el cliente a partir del DNI con getCliente(DNI) del registro de clientes
                   Reserva newReserva = reserva.Crear(
                       dlgInsertaReserva.NumHabitacion, dlgInsertaReserva.Tipo, c, dlgInsertaReserva.FechaEntrada, 
                       dlgInsertaReserva.FechaSalida, dlgInsertaReserva.Garaje, dlgInsertaReserva.PrecioDia, dlgInsertaReserva.Iva
                     );

                this.Reservas.Add(newReserva);

                this.Actualiza();
            }

            this.View.Show();
        }

        void ConsultaReserva()
        {
            Console.WriteLine("Consulta reservas");
            var dlgConsultaReserva = new DlgConsultaReserva();

            this.View.Hide();

            if (dlgConsultaReserva.ShowDialog() == DialogResult.OK)
            {
               

            }

            this.View.Show();
        }



        void Actualiza()
        {
            Console.WriteLine("dentro actualiza");
            Console.WriteLine("---"+this.Reservas.List);

            int numElementos = this.Reservas.Count;
            this.View.SbStatus.Text = ("Numero de reservas: " + numElementos);
            for (int i = 0; i < numElementos; i++)
            {
                if (this.View.GrdLista.Rows.Count <= i)
                {
                    this.View.GrdLista.Rows.Add();
                }
                this.ActualizaFilaDeLista(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.View.GrdLista.Rows.Count - numElementos;
            for (; numExtra > 0; --numExtra)
            {
                this.View.GrdLista.Rows.RemoveAt(numElementos);
            }
        }

        private void ActualizaFilaDeLista(int numFila)
        {
            if (numFila < 0
              || numFila > this.View.GrdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(numFila));
            }

            DataGridViewRow fila = this.View.GrdLista.Rows[numFila];
            Reserva reserva = this.Reservas.List[numFila];

            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = reserva.Id; //aaaammddhhh
            fila.Cells[2].Value = reserva.FechaEntrada;
            fila.Cells[3].Value = reserva.FechaSalida;
            fila.Cells[4].Value = reserva.Garaje;
            fila.Cells[5].Value = reserva.CalcularTotal();
            fila.Cells[6].Value = reserva.ObtenerDniCliente();

            foreach (DataGridViewCell celda in fila.Cells)
            {
                celda.ToolTipText = reserva.ToString();
            }
        }


        void Salir()
        {
            this.Reservas.GuardarXml();
            Application.Exit();
        }

        void OnQuit()
        {
            Application.Exit();
        }

        public MainWindowView View { get; private set; }
        public DlgInsertaReserva AddReserva { get; private set; }

        private RegistroReserva Reservas { get; set; }
        private RegistroClientes Clientes { get; set; }

    }
}
