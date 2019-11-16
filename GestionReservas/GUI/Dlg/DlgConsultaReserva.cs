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

    public class DlgConsultaReserva : Form
    {

        public DlgConsultaReserva()
        {
            this.BuildGUI();
            this.CenterToScreen();
        }

        private void BuildGUI()
        {
  
            this.BuildStatus();
            this.BuildMenu();
            this.BuildPanelLista();

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
            this.Text = "Gestion de un hotel - Consulta reservas";

            this.ResumeLayout(true);
            this.ResizeWindow();
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem("&Archivo");
          //  this.mInsertar = new MenuItem("&Insertar");

            this.opSalir = new MenuItem("&Salir");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

        /*    this.OpInsertarAdaptadorTDT = new MenuItem("&Insertar Adaptador TDT");
            this.OpInsertarTelevisor = new MenuItem("&Insertar Televisor");
            this.OpInsertarRadio = new MenuItem("&Insertar Radio");
            this.OpInsertarReproductorDVD = new MenuItem("&Insertar Reproductor DVD");*/


            this.mArchivo.MenuItems.Add(this.opSalir);
         /*   this.mInsertar.MenuItems.Add(this.OpInsertarAdaptadorTDT);
            this.mInsertar.MenuItems.Add(this.OpInsertarTelevisor);
            this.mInsertar.MenuItems.Add(this.OpInsertarRadio);
            this.mInsertar.MenuItems.Add(this.OpInsertarReproductorDVD);*/

            this.mPpal.MenuItems.Add(this.mArchivo);
          //  this.mPpal.MenuItems.Add(this.mInsertar);

            this.Menu = mPpal;


        }


        private Panel BuildPanelDetalle()
        {
            var pnlDetalle = new Panel { Dock = DockStyle.Bottom };
            pnlDetalle.SuspendLayout();

            this.edDetalle = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Font(FontFamily.GenericMonospace, 12),
                ForeColor = Color.Navy,
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


        /*  private Panel BuildPanelGestion()
          {
              var pnlGestion = new Panel();
              pnlGestion.SuspendLayout();
              pnlGestion.Dock = DockStyle.Fill;
          }
          */
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

            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            var textCellTemplate5 = new DataGridViewTextBoxCell();
            var textCellTemplate6 = new DataGridViewTextBoxCell();
            var textCellTemplate7 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Coral;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Wheat;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Wheat;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Wheat;
            textCellTemplate4.Style.ForeColor = Color.Black;
            textCellTemplate5.Style.BackColor = Color.Wheat;
            textCellTemplate5.Style.ForeColor = Color.Black;
            textCellTemplate6.Style.BackColor = Color.Wheat;
            textCellTemplate6.Style.ForeColor = Color.Black;
            textCellTemplate7.Style.BackColor = Color.Wheat;
            textCellTemplate7.Style.ForeColor = Color.Black;

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

            var column3 = new DataGridViewTextBoxColumn  // fecha entrada
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha Entrada",
                Width = 15,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn // fecha salida
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha salida",
                Width = 15,
                ReadOnly = true
            };

            var column5 = new DataGridViewTextBoxColumn // garaje
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Garaje",
                Width = 15,
                ReadOnly = true
            };

            var column6 = new DataGridViewTextBoxColumn // total
            { 
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate6,
                HeaderText = "Total",
                Width = 15,
                ReadOnly = true
            };

            var column7 = new DataGridViewTextBoxColumn  //dni cliente
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate7,
                HeaderText = "Cliente",
                Width = 15,
                ReadOnly = true
            };
            var column8 = new DataGridViewTextBoxColumn  //modificar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate7,
                HeaderText = "Modificar",
                Width = 15,
                ReadOnly = true
            };
            var column9 = new DataGridViewTextBoxColumn  //borrar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate7,
                HeaderText = "Eliminar",
                Width = 15,
                ReadOnly = true
            };

            var column10 = new DataGridViewTextBoxColumn  //factura
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate7,
                HeaderText = "Factura",
                Width = 15,
                ReadOnly = true
            };

            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4, column5, column6, column7, column8, column9, column10
            });

            pnlLista.Controls.Add(this.GrdLista);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }


        private void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.GrdLista.Width = width;
            this.GrdLista.Height = 15;

          

            this.GrdLista.Columns[0].Width =
                                (int)System.Math.Floor(width * .05); // Num reserva
            this.GrdLista.Columns[1].Width =
                                (int)System.Math.Floor(width * .10); // id
            this.GrdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .10); // tipo
            this.GrdLista.Columns[3].Width =
                                (int)System.Math.Floor(width * .14); // Fecha entrada
            this.GrdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .14); // Fecha salida
            this.GrdLista.Columns[5].Width =
                               (int)System.Math.Floor(width * .10); // garaje
            this.GrdLista.Columns[6].Width =
                               (int)System.Math.Floor(width * .10); // Precio total
            this.GrdLista.Columns[7].Width =
                               (int)System.Math.Floor(width * .09); // DNI Cliente
            this.GrdLista.Columns[8].Width =
                               (int)System.Math.Floor(width * .06); // btn modificar
            this.GrdLista.Columns[9].Width =
                               (int)System.Math.Floor(width * .06); // btn borrar
            this.GrdLista.Columns[10].Width =
                               (int)System.Math.Floor(width * .06); // btn factura
        }



        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem mInsertar;
        public MenuItem opSalir;
        public MenuItem OpInsertarAdaptadorTDT;
        public MenuItem OpInsertarRadio;
        public MenuItem OpInsertarTelevisor;
        public MenuItem OpInsertarReproductorDVD;

        public StatusBar SbStatus;
        private Panel pnlPpal;
        private TextBox edDetalle;


        public DataGridView GrdLista;


        //   private MenuItem opInsertar; //hacer una por tipo de aparato

    }
}
