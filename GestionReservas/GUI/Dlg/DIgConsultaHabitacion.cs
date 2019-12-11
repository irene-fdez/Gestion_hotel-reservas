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

    public class DIgConsultaHabitacion :Form
    {
        public DIgConsultaHabitacion(RegistroHabitaciones habi)
        {
            this.MVC = new MainWindowCore();
            this.Habitaciones = habi;
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
            this.Text = "Gestion de un hotel - Consulta habitaciones";

            this.Actualiza();
            this.ResumeLayout(true);
            this.ResizeWindow();
        }

        private void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.GrdLista.Width = width;
            this.GrdLista.Height = 15;



            this.GrdLista.Columns[0].Width =
                (int) System.Math.Floor(width * .04); 
            this.GrdLista.Columns[1].Width =
                (int) System.Math.Floor(width * .08); 
            this.GrdLista.Columns[2].Width =
                (int) System.Math.Floor(width * .20); 
            this.GrdLista.Columns[3].Width =
                (int) System.Math.Floor(width * .20); 
            this.GrdLista.Columns[4].Width =
                (int) System.Math.Floor(width * .20); 
            this.GrdLista.Columns[5].Width =
                (int) System.Math.Floor(width * .14); 
            this.GrdLista.Columns[6].Width =
                (int) System.Math.Floor(width * .14); 
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
            var pnlLista = new Panel();
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
            var textCellTemplate6 = new DataGridViewTextBoxCell();
            var textCellTemplate7 = new DataGridViewTextBoxCell();

            //botones
            var buttonCellTemplate8 = new DataGridViewButtonCell();
            var buttonCellTemplate9 = new DataGridViewButtonCell();
            var buttonCellTemplate10 = new DataGridViewButtonCell();

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

            textCellTemplate6.Style.BackColor = colorCeldasDatos;
            textCellTemplate6.Style.ForeColor = Color.Black;
            textCellTemplate6.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate7.Style.BackColor = colorCeldasDatos;
            textCellTemplate7.Style.ForeColor = Color.Black;
            textCellTemplate7.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //botones
            buttonCellTemplate8.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate8.Style.ForeColor = Color.Black;
            buttonCellTemplate8.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate8.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            buttonCellTemplate9.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate9.Style.ForeColor = Color.Black;
            buttonCellTemplate9.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate9.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            buttonCellTemplate10.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate10.Style.ForeColor = Color.Black;
            buttonCellTemplate10.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate10.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

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
                HeaderText = "Id",
                Width = 20,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn // tipo
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Tipo",
                Width = 15,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn  // Fecha Renovacion
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha Renovacion",
                Width = 15,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn // Fecha Ultima reserva
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha Ultima reserva",
                Width = 15,
                ReadOnly = true
            };

           
            var column5 = new DataGridViewButtonColumn  //modificar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate8,
                HeaderText = "Modificar",
                Width = 15,
                ReadOnly = true,
            };

            var column6 = new DataGridViewButtonColumn  //borrar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate8,
                HeaderText = "Eliminar",
                Width = 15,
                ReadOnly = true,
            };


            
            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4,  column5, column6, 
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

         if (posicion == 7 && this.Habitaciones.Count > fila)
         {
             this.edDetalle.Text = this.Habitaciones[fila].ToString();
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

 
 void Actualiza()
 {
     Console.WriteLine("dentro actualiza");

     // var consultRes = new DlgConsultaReserva();


     int numElementos = this.Habitaciones.Count;
     Console.WriteLine("nº Habitaciones: " + numElementos);

     this.SbStatus.Text = ("Numero de Habitaciones: " + numElementos);

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
     Habitacion habitacion = this.Habitaciones[numFila];


     fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
     fila.Cells[1].Value = habitacion.Numero; //aaaammddhhh
     fila.Cells[2].Value = habitacion.Tipo; 
     fila.Cells[3].Value = habitacion.FechaRenova.ToString("dd/MM/yyyy");
     fila.Cells[4].Value = habitacion.UltimaReserva.ToString("dd/MM/yyyy");
     fila.Cells[5].Value = "*";
     fila.Cells[6].Value = "*";


     foreach (DataGridViewCell celda in fila.Cells)
     {
         if (celda.ColumnIndex < 7)
         {
             celda.ToolTipText = habitacion.ToString();
         }
         else if(celda.ColumnIndex == 7)
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
             this.Modificar((string)this.GrdLista.Rows[fila].Cells[1].Value);
         }
         else if (this.GrdLista.CurrentCell.ColumnIndex == 6)
         {
             this.Eliminar();
         }
        
         this.Actualiza();
     }
     catch(Exception) { }
 }
        
 
 
 
 public void Eliminar()
 {

     Console.WriteLine("dentro eliminar");
     var id = (string)this.GrdLista.CurrentRow.Cells[1].Value;

     //Dialogo de confirmación de eiminación
     DialogResult result;
     string mensaje = "¿Está seguro de que desea eliminar la habitacion con ID(" + id + "), del registro de habitaciones?";
     string tittle = "Eliminar reserva";

     result = MessageBox.Show(mensaje, tittle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

     if (result == DialogResult.Yes)
     {
         this.Habitaciones.Remove(this.Habitaciones.getHabitacion(id));
     }
 }
 
 

 
 public void Modificar(string id)
 {
     //A partir de la clave de la entidad Reserva, obtenemos la reserva a modificar
     Habitacion HabitaModif = this.Habitaciones.getHabitacion(id);

     Console.WriteLine("numero de habitacion a modificar" + HabitaModif);
     var dlgModificar = new DIgModificaHabitacion(HabitaModif);

     this.Hide();
     if (dlgModificar.ShowDialog() == DialogResult.OK)
     {
         this.Habitaciones.Remove(HabitaModif);

         
         DateTime fechaRenova = dlgModificar.FechaRenova;
         DateTime ultimaReserva = dlgModificar.UltimaReserva;
         
         
         Habitacion.Tipos parsedTipo = default(Habitacion.Tipos);
         var element =dlgModificar.TipoHabitacion;
         if(element != null)
         {
             // Try to parse
             Enum.TryParse<Habitacion.Tipos>(element, out parsedTipo);
         }
         
         Habitacion Habi = new Habitacion(id, parsedTipo, fechaRenova,ultimaReserva , 
             HabitaModif.Wifi, HabitaModif.CajaFuerte, HabitaModif.MiniBar, HabitaModif.Baño, HabitaModif.Cocina,HabitaModif.Tv);
                
         this.Habitaciones.Add(Habi);
         this.Actualiza();

     }

     if (!this.IsDisposed) { this.Show(); }
     else { Application.Exit(); }

 }

 
 void Guardar()
 {
     // this.regRes.GuardarXml();
     this.Habitaciones.GuardarXml();
 }

 public void Salir()
 {
     Console.WriteLine("guarda y sale");
     this.Habitaciones.GuardarXml();
     Application.Exit();
 }
 
 
 
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opGuardar;
        public MenuItem opSalir;
        public MenuItem opVolver;
        public MenuItem mBuscar;


        public StatusBar SbStatus;
        private Panel pnlPpal;
        private TextBox edDetalle;
        public DataGridView GrdLista;


      
        private RegistroHabitaciones Habitaciones;
        private MainWindowCore MVC;

    }
}