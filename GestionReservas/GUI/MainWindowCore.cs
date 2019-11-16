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
            this.View = new MainWindowView();
        //    try { 
        //        this.Actualiza();
        //    }catch(Exception e)
        //    {
        //        Console.WriteLine("\nerror actualiza, " + e);
        //    }
            this.View.FormClosed += (sender, e) => this.OnQuit();
            this.View.opSalir.Click += (sender, e) => this.Salir();

       //     try
        //    {
                this.View.btnAddReserva.Click += (sender, e) => this.InsertaReserva();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("\nerror btnAdd, msg:" + e);
        //    }
            

        }

        void InsertaReserva()
        {
            Console.WriteLine("Inserta reserva");
            var dlgInsertaReserva = new DlgInsertaReserva();

            this.View.Hide();

            if (dlgInsertaReserva.ShowDialog() == DialogResult.OK)
            {
                Reserva reserva = null;

             /*   Reserva new_reserva = reserva.Crear(
                    dlgInsertaReserva.habita, dlgInsertaReserva.tipo, dlgInsertaReserva.cliente, dlgInsertaReserva.FindForm, 
                    dlgInsertaReserva.fOut, dlgInsertaReserva.garaje, dlgInsertaReserva.precioDia, dlgInsertaReserva.Iva
                  );*/

                this.RegistroReserva.Add(reserva);

                this.Actualiza();
            }

            this.View.Show();
        }

        void Actualiza()
        {
            Console.WriteLine("dentro actualiza");
            Console.WriteLine("---"+this.RegistroReserva.List);

            int numElementos = this.RegistroReserva.Count;
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
            Reserva reserva = this.RegistroReserva.List[numFila];

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
            this.RegistroReserva.GuardarXml();
            Application.Exit();
        }

        void OnQuit()
        {
            Application.Exit();
        }

        public MainWindowView View { get; private set; }

        private RegistroReserva RegistroReserva { get; set; }
    }
}
