using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestionReservas.Core;

namespace GestionReservas.GUI.Dlg
{
    public class DlgConsultaHabitacion : Form
    {
        public DlgConsultaHabitacion(RegistroReserva res, List<Cliente> cli)
        {
            this.MVC = new MainWindowCore();
            this.Habitaciones = RegistroHabitaciones.RecuperarXml();
            this.HabitacionesBuscar = this.Habitaciones;
            //this.reservas = RegistroReserva.RecuperarXml();
            this.Reservas = res;
            this.Clientes = cli;
            this.BuildGUI();
            this.CenterToScreen();

            this.GrdLista.Click += (sender, e) => ClickLista();

            this.opBuscar.Click += (sender, e) => this.BuscarVacios();
            
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
            this.Text = "Gestion de un hotel - Consulta por habitacion";

            this.Actualiza();
            this.ResumeLayout(true);
            this.ResizeWindow();
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();
            
            //OPCION ARCHIVO
            this.mArchivo = new MenuItem("&Archivo");
            this.mBuscar = new MenuItem("&Buscar");
            this.opBuscar = new MenuItem("&Buscar habitaciones vacias");
            this.opGuardar = new MenuItem("&Guardar");
            this.opSalir = new MenuItem("&Salir");
            this.opVolver = new MenuItem("&Volver");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

            this.mBuscar.MenuItems.Add(this.opBuscar);


            //AÑADIR MENU_ITEMS

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
            
            //botones
            
            var buttonCellTemplate5 = new DataGridViewButtonCell();
            var buttonCellTemplate6 = new DataGridViewButtonCell();

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
            

            buttonCellTemplate5.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate5.Style.ForeColor = Color.Black;
            buttonCellTemplate5.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate5.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            buttonCellTemplate6.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate6.Style.ForeColor = Color.Black;
            buttonCellTemplate6.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate6.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            var column0 = new DataGridViewTextBoxColumn //numero de reparacion
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                Width = 5,
                ReadOnly = true
            };
            
            var column1 = new DataGridViewTextBoxColumn // numero
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Numero",
                Width = 20,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn // Tipo
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Tipo",
                Width = 20,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn  // fecha renovacion
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha Renovacion",
                Width = 20,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn  //ultima reserva
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha última reserva",
                Width = 20,
                ReadOnly = true,
            };

            var column5 = new DataGridViewButtonColumn  //reservas pasadas
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate5,
                HeaderText = "Resevas pasadas",
                Width = 20,
                ReadOnly = true,
            };


            var column6 = new DataGridViewButtonColumn  //reservas pendientes
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate6,
                HeaderText = "Reservas pendientes",
                Width = 20,
                ReadOnly = true
            };

            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4, column5, column6
            });

            //this.GrdLista.SelectionChanged += (sender, e) => this.FilaSeleccionada();

            pnlLista.Controls.Add(this.GrdLista);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }

        /*private void FilaSeleccionada()
        {

            try
            {
                int fila = System.Math.Max(0, this.GrdLista.CurrentRow.Index);
                int posicion = this.GrdLista.CurrentCellAddress.X;

                if (posicion == 7 && this.ReservasBuscar.Count > fila)
                {
                    this.edDetalle.Text = this.ReservasBuscar[fila].Cliente.ToString();
                    this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                    this.edDetalle.SelectionLength = 0;
                }
                else if(posicion < 7){
                    this.edDetalle.Text = this.ReservasBuscar[fila].DatosEconomicosReserva();
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
        }*/


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
                                (int)System.Math.Floor(width * .16); // id
            this.GrdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .16); // tipo
            this.GrdLista.Columns[3].Width =
                                (int)System.Math.Floor(width * .16); // Fecha entrada
            this.GrdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .16); // Fecha salida
            this.GrdLista.Columns[5].Width =
                               (int)System.Math.Floor(width * .16); // garaje
            this.GrdLista.Columns[6].Width =
                               (int)System.Math.Floor(width * .16); // Precio total
        
        }

        void BuscarVacios()
        {
            Console.WriteLine("HOMNREEEE");
            var dlgBuscarVaciosPorPiso = new DlgBuscarVaciosPorPiso();
            this.Hide();
            if (dlgBuscarVaciosPorPiso.ShowDialog() == DialogResult.OK)
            {
                List<Habitacion> habitacionesVacias = new List<Habitacion>();
                var piso = dlgBuscarVaciosPorPiso.Piso;
                if (piso == "")
                { 
                    
                    this.HabitacionesBuscar.List.ForEach(habitacion =>
                    {
                     DateTime today = DateTime.Today;
                     List<Reserva> reservas = Reservas.List
                         .Where(element => element.NumeroHabitacion == habitacion.Numero 
                                           && element.FechaEntrada <= today && element.FechaSalida >= today).ToList();
                     if (reservas.Count == 0)
                     {
                         habitacionesVacias.Add(habitacion);
                     }
                    }); 
                    this.HabitacionesBuscar = new RegistroHabitaciones(habitacionesVacias);
                    this.Actualiza();
                }
                else
                {
                    this.HabitacionesBuscar.List.ForEach(habitacion =>
                    {
                        if (habitacion.Numero.Substring(0, 1) == piso)
                        {
                            DateTime today = DateTime.Today;
                            List<Reserva> reservas = Reservas.List
                                .Where(element => element.NumeroHabitacion == habitacion.Numero
                                                  && element.FechaEntrada <= today && element.FechaSalida >= today)
                                .ToList();
                            if (reservas.Count == 0)
                            {
                                habitacionesVacias.Add(habitacion);
                            }
                        }
                    });
                }
                this.HabitacionesBuscar = new RegistroHabitaciones(habitacionesVacias);
                this.Actualiza();

            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }
        }
        
        
        void Actualiza()
        {
            Console.WriteLine("dentro actualiza");

            // var consultRes = new DlgConsultaReserva();


            int numElementos = this.HabitacionesBuscar.Count;
            Console.WriteLine("NUMERO DE HABITACIONES: " + numElementos);

            this.SbStatus.Text = ("Numero de habitaciones: " + numElementos);

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
            Habitacion habitacion = this.HabitacionesBuscar[numFila];


            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = habitacion.Numero; //aaaammddhhh
            fila.Cells[2].Value = habitacion.Tipo; 
            fila.Cells[3].Value = habitacion.FechaRenova.ToString("dd/MM/yyyy");
            fila.Cells[4].Value = habitacion.UltimaReserva.ToString("dd/MM/yyyy");
            fila.Cells[5].Value = "*";
            fila.Cells[6].Value = "*";


            foreach (DataGridViewCell celda in fila.Cells)
            {
                if (celda.ColumnIndex < 5)
                {
                    celda.ToolTipText = habitacion.ToString();
                }
            }
        }



        public void ClickLista()
        {
            try
            {
                Console.WriteLine("clickLista : " + this.GrdLista.CurrentCell.ColumnIndex);

                if (this.GrdLista.CurrentCell.ColumnIndex == 5)
                {
                    int fila = this.GrdLista.CurrentCell.RowIndex;
                    //this.Modificar((string)this.GrdLista.Rows[fila].Cells[1].Value);
                    this.reservasPasadas();
                }
                else if (this.GrdLista.CurrentCell.ColumnIndex == 6)
                {
                    this.reservasPendientes();
                }

                this.Actualiza();
            }
            catch(Exception) { }
        }


        public void reservasPasadas()
        {
            //return this.Id.Substring(8, 3);
            Console.WriteLine("dentro reservas pasadas");
            //IDHABITACION
            var idHabitacion = (string)this.GrdLista.CurrentRow.Cells[1].Value;
            List<Reserva> reservas = new List<Reserva>();
            
            //CONSEGUIR ID RESERVA
            //CONSEGUIMOS EL DIA
            DateTime today = DateTime.Today;
            
            //BUSCAR EN REPARACIONES LAS QUE TIENEN EN SU ID EL ID DE LAS HABITACIONES
            //Y QUE A LA VEZ SU FECHA ES MENOR QUE LA DE HOY
            Console.WriteLine("noelia");
            Console.WriteLine(Reservas[0].FechaSalida);
            foreach (Reserva r in Reservas)
            {
                var idHabitacionReserva = r.NumeroHabitacion;
                if (idHabitacionReserva == idHabitacion &&  r.FechaSalida<today)
                {
                    reservas.Add(r);
                }
            }
            
        }
        
        public void reservasPendientes()
        {

            Console.WriteLine("dentro eliminar");
            var id = (string)this.GrdLista.CurrentRow.Cells[1].Value;

            //Dialogo de confirmación de eiminación
            DialogResult result;
            string mensaje = "¿Está seguro de que desea eliminar la reserva con ID(" + id + "), del registro de reservas?";
            string tittle = "Eliminar reserva";

            result = MessageBox.Show(mensaje, tittle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                //this.Reservas.Remove(this.Reservas.getReserva(id));
                //this.ReservasBuscar.Remove(this.ReservasBuscar.getReserva(id));
            }
        }

       
        
        public void Salir()
        {
            Application.Exit();
        }
        
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem mBuscar;
        public MenuItem opBuscar;
        public MenuItem opGuardar;
        public MenuItem opSalir;
        public MenuItem opVolver;

        public StatusBar SbStatus;
        private Panel pnlPpal;
        private TextBox edDetalle;
        public DataGridView GrdLista;
        
        private readonly List<Cliente> Clientes;
        private RegistroReserva Reservas;
        private RegistroHabitaciones Habitaciones { get; set; }
        private RegistroHabitaciones HabitacionesBuscar;
        private MainWindowCore MVC;
       
    }
}