using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionReservas.Core;
using GestionReservas.GUI;


namespace GestionReservas.GUI.Dlg
{
    using System.Windows.Forms;
    using System.Drawing;

    public class DlgConsultaCliente : Form
    {

        public DlgConsultaCliente(RegistroClientes cli)
        {
            this.MVC = new MainWindowCore();
            this.Clientes = cli;
            this.BuildGUI();
            this.CenterToScreen();


            
            this.GrdLista.Click += (sender, e) => ClickLista();


            this.opGuardar.Click += (sender, e) => this.Guardar();
            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; this.Salir(); };
            this.opVolver.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;
        }

        private void BuildGUI()
        {
            this.BuildStatus();
            this.BuildMenu();

            // this.BuildPanelLista();

            this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(49, 66, 82),

            };

            this.pnlPpal.SuspendLayout();
            this.Controls.Add(this.pnlPpal);
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
            this.pnlPpal.Controls.Add(this.BuildPanelDetalle());
            this.pnlPpal.ResumeLayout(false);

            this.MinimumSize = new Size(1000, 400);
            this.Resize += (obj, e) => this.ResizeWindow();
            this.Text = "Gestion de un hotel - Consulta Cliente";

            this.Actualiza();
            this.ResumeLayout(true);
            this.ResizeWindow();
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();
            this.mArchivo = new MenuItem("&Archivo");
            this.opGuardar = new MenuItem("&Guardar");
            this.opSalir = new MenuItem("&Salir");
            this.opVolver = new MenuItem("&Volver");
            this.opSalir.Shortcut = Shortcut.CtrlQ;
            this.mBuscar = new MenuItem("&Buscar");
            
            this.opBuscarAll = new MenuItem("&Buscar todos");
            this.opBuscarPendientes = new MenuItem("&Buscar pendientes");


            this.mBuscar.MenuItems.Add(this.opBuscarAll);
            this.mBuscar.MenuItems.Add(this.opBuscarPendientes);
            this.mArchivo.MenuItems.Add(this.opVolver);
            this.mArchivo.MenuItems.Add(this.opGuardar);
            this.mArchivo.MenuItems.Add(this.opSalir);

            this.mPpal.MenuItems.Add(this.mArchivo);
            this.mPpal.MenuItems.Add(this.mBuscar);

            this.Menu = mPpal;
        }




        private Panel BuildPanelDetalle()
        {
            var pnlDetalle = new Panel {
                Dock = DockStyle.Bottom,
                Height = 110

            };
            pnlDetalle.SuspendLayout();

            this.edDetalle = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Font(FontFamily.GenericMonospace, 10),
                ForeColor = Color.Black,
                //BackColor = Color.LightGray,
                BackColor = Color.FromArgb(55, 95, 132),
            };

            
            pnlDetalle.Controls.Add(this.edDetalle);
            pnlDetalle.ResumeLayout(false);
            return pnlDetalle;
        }

        private void BuildStatus()
        {
            this.SbStatus = new StatusBar();
            this.SbStatus.Dock = DockStyle.Bottom;
            this.Controls.Add(this.SbStatus);
        }

        private Panel BuildPanelLista()
        {
            var pnlLista = new Panel
            {
                Dock = DockStyle.Fill
            };
            
            pnlLista.SuspendLayout();
            pnlLista.Dock = DockStyle.Fill;

            // Crear gridview
            this.GrdLista = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackColor = Color.FromArgb(113, 162, 208),
            };

            this.GrdLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.GrdLista.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82);
            this.GrdLista.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.GrdLista.ColumnHeadersHeight = 30;
            this.GrdLista.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

            //texto
            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            var textCellTemplate5 = new DataGridViewTextBoxCell();


            //botones
            var buttonCellTemplate8 = new DataGridViewButtonCell();


            //texto
            Color colorCeldasDatos = Color.PapayaWhip;

            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;

            textCellTemplate1.Style.BackColor = Color.DarkSalmon; 
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            textCellTemplate2.Style.BackColor = colorCeldasDatos;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate2.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            textCellTemplate3.Style.BackColor = colorCeldasDatos;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate4.Style.BackColor = colorCeldasDatos;
            textCellTemplate4.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            textCellTemplate5.Style.BackColor = colorCeldasDatos;
            textCellTemplate5.Style.ForeColor = Color.Black;
            textCellTemplate5.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


            //botones
            buttonCellTemplate8.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate8.Style.ForeColor = Color.Black;
            buttonCellTemplate8.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate8.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);



            var column0 = new DataGridViewTextBoxColumn //numero de reparacion
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                Width = 5,
                ReadOnly = true
            };

            var column1 = new DataGridViewTextBoxColumn // id
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "DNI",
                Width = 20,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn // tipo
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Email",
                Width = 15,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn  // fecha entrada
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Nombre",
                Width = 15,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn // fecha salida
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Numero telef",
                Width = 15,
                ReadOnly = true
            };
            
            var column5 = new DataGridViewTextBoxColumn // fecha salida
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Calle",
                Width = 15,
                ReadOnly = true
            };


            var column8 = new DataGridViewButtonColumn  //modificar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate8,
                HeaderText = "Mostrar reservas",
                Width = 20,
                ReadOnly = true,
            };
            

            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4,column5,column8
            });

            this.GrdLista.SelectionChanged += (sender, e) => this.FilaSeleccionada();

            pnlLista.Controls.Add(this.GrdLista);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }

        private void FilaSeleccionada()
        {

            try
            {
                int fila = System.Math.Max(0, this.GrdLista.CurrentRow.Index);
                int posicion = this.GrdLista.CurrentCellAddress.X;

                if (posicion < 6 && this.Clientes.Count > fila)
                {
                    this.edDetalle.Text = this.Clientes[fila].ToString();
                    this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                    this.edDetalle.SelectionLength = 0;
                }
                else 
                {
                    this.edDetalle.Clear();
                }
            }
            catch(Exception)
            {
                this.edDetalle.Clear();
            }
            

            return;
        }


        private void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.GrdLista.Width = width;
            this.GrdLista.Height = 15;

          

            this.GrdLista.Columns[0].Width =
                                (int)System.Math.Floor(width * .03); // Num reserva
            this.GrdLista.Columns[1].Width =
                                (int)System.Math.Floor(width * .10); // id
            this.GrdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .20); // tipo
            this.GrdLista.Columns[3].Width =
                                (int)System.Math.Floor(width * .14); // Fecha entrada
            this.GrdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .14); // Fecha salida
            this.GrdLista.Columns[5].Width =
                (int)System.Math.Floor(width * .23); // Fecha salida
            this.GrdLista.Columns[6].Width =
                               (int)System.Math.Floor(width * .14); // btn modificar

        }

        
        void Actualiza()
        {
            Console.WriteLine("dentro actualiza");

            // var consultRes = new DlgConsultaReserva();


            int numElementos = this.Clientes.Count;
            Console.WriteLine("NUMERO DE Clientes: " + numElementos);

            this.SbStatus.Text = ("Numero de Clientes: " + numElementos);

            for (int i = 0; i < numElementos; i++)
            {
                if (this.GrdLista.Rows.Count <= i)
                {
                    this.GrdLista.Rows.Add();
                }
                this.ActualizaFilaDeLista(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.GrdLista.Rows.Count - numElementos;
            for (; numExtra > 0; --numExtra)
            {
                this.GrdLista.Rows.RemoveAt(numElementos);
            }
        }

        private void ActualizaFilaDeLista(int numFila)
        {
            Console.WriteLine("dentro ActualizaFilaDeLista");

            if (numFila < 0
              || numFila > this.GrdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(numFila));
            }

            DataGridViewRow fila = this.GrdLista.Rows[numFila];
            Cliente cliente = this.Clientes[numFila];


            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = cliente.DNI; //aaaammddhhh
            fila.Cells[2].Value = cliente.Email; 
            fila.Cells[3].Value = cliente.Nombre;
            fila.Cells[4].Value = cliente.Telefono;
            fila.Cells[5].Value = cliente.DireccionPostal;
            fila.Cells[6].Value = "*";



            foreach (DataGridViewCell celda in fila.Cells)
            {
                if (celda.ColumnIndex < 6)
                {
                    celda.ToolTipText = cliente.ToString();
                }
            }
        }



        public void ClickLista()
        {
            try
            {
                Console.WriteLine("clickLista : " + this.GrdLista.CurrentCell.ColumnIndex);

                if (this.GrdLista.CurrentCell.ColumnIndex == 6)
                {
                    int fila = this.GrdLista.CurrentCell.RowIndex;
                    this.Mostrar((string)this.GrdLista.Rows[fila].Cells[1].Value);
                }

                this.Actualiza();
            }
            catch(Exception) { }
        }


         

        public void Mostrar(string id)
        {
            Cliente cliente = this.Clientes.getCliente(id);
            Reservas = new RegistroReserva(Clientes.List);
            Console.WriteLine(cliente);
            /*//A partir de la clave de la entidad Reserva, obtenemos la reserva a modificar
            
            Reserva ResModif = this.Reservas.getReserva(id);
            Cliente c = ResModif.Cliente;

            var dlgModificar = new DlgModificaReserva(ResModif, this.Clientes);

            this.Hide();
            if (dlgModificar.ShowDialog() == DialogResult.OK)
            {
                this.Reservas.Remove(ResModif);
                this.ReservasBuscar.Remove(ResModif);

                string tipo = dlgModificar.Tipo;
                DateTime fechaEntrada = dlgModificar.FechaEntrada;
                DateTime fechaSalida = dlgModificar.FechaSalida;
                string garaje = dlgModificar.Garaje;

                Reserva r = new Reserva(id, tipo, c, fechaEntrada, fechaSalida, garaje, ResModif.PrecioDia, ResModif.IVA, ResModif.Total);
                
                this.Reservas.Add(r);
                this.ReservasBuscar.Add(r);
                this.Actualiza();

            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }
*/
        }




        void Guardar()
        {
            this.Clientes.GuardarXml();
        }

        public void Salir()
        {
            Console.WriteLine("guarda y sale");
            this.Clientes.GuardarXml();
            Application.Exit();
        }



        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opGuardar;
        public MenuItem opSalir;
        public MenuItem opVolver;
        public MenuItem mBuscar;
        public MenuItem opBuscarAll;
        public MenuItem opBuscarPendientes;



        public StatusBar SbStatus;
        private Panel pnlPpal;
        private TextBox edDetalle;
        public DataGridView GrdLista;


        private  RegistroClientes Clientes;
        private RegistroReserva Reservas;

        private MainWindowCore MVC;

    }

}
