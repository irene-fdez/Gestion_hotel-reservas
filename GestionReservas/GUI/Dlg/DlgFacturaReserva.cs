 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionReservas.Core;


namespace GestionReservas.GUI.Dlg
{
    using System.Windows.Forms;
    using System.Drawing;

    public class DlgFacturaReserva : Form
    {

        public DlgFacturaReserva(Reserva reserva, List<Cliente> clientes, int numReserva)
        {

            this.Reservas = new RegistroReserva(clientes);
            this.reserva = reserva;
            this.cliente = reserva.Cliente;
            this.NumReserva = numReserva;

            this.Build();
            this.CenterToScreen();

            var DCR = new DlgConsultaReserva(this.Reservas, clientes);
            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; DCR.Salir(); };
            this.opVolver.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;


        }

        void Build()
        {

            this.BuildStatus();
            this.BuildMenu();

            this.SuspendLayout();

            this.pnlInserta = new TableLayoutPanel
            {
                Size = new Size(442, 440), //Size(ancho, alto)
                Dock = DockStyle.Fill,
                //BackColor = Color.FromArgb(49, 66, 82),
                BackColor = Color.GhostWhite,
                    

            };

            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlFactura = this.BuildFacturaPanel();
            pnlInserta.Controls.Add(pnlFactura);

            var pnlEspacio = this.BuildEspacioPanel();
            pnlInserta.Controls.Add(pnlEspacio);

            var pnlDatosFactura = this.BuildDatosFactura();
            pnlInserta.Controls.Add(pnlDatosFactura);

            var pnlDatosCliente = this.BuildDatosCliente();
            pnlInserta.Controls.Add(pnlDatosCliente);

            var pnlContenidoFactura = this.BuildContenidoFactura();
            pnlInserta.Controls.Add(pnlContenidoFactura);

            var pnlImportesTotales = this.BuildImportesTotales();
            pnlInserta.Controls.Add(pnlImportesTotales);

               

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Factura reserva";

            Console.WriteLine(pnlContenidoFactura.Width);


            this.MinimumSize = new Size(pnlContenidoFactura.Width + 70,
                        pnlFactura.Height + pnlEspacio.Height*2 + pnlDatosFactura.Height + pnlDatosCliente.Height + pnlContenidoFactura.Height + pnlImportesTotales.Height + 100);

            this.MaximumSize = new Size(pnlContenidoFactura.Width + 70, 
                pnlFactura.Height + pnlEspacio.Height * 2 + pnlDatosFactura.Height + pnlDatosCliente.Height + pnlContenidoFactura.Height + pnlImportesTotales.Height + 200);

            this.Size = new Size(pnlContenidoFactura.Width + 70, 
                        pnlFactura.Height + pnlEspacio.Height*2 + pnlDatosFactura.Height + pnlDatosCliente.Height + pnlContenidoFactura.Height + pnlImportesTotales.Height + 100 );

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }


        private void BuildStatus()
        {
            this.SbStatus = new StatusBar();
            this.SbStatus.Dock = DockStyle.Bottom;
            this.Controls.Add(this.SbStatus);
        }

        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            

            this.mArchivo = new MenuItem("&Archivo");
            this.opVolver = new MenuItem("&Volver");

            this.opSalir = new MenuItem("&Salir");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

            this.mArchivo.MenuItems.Add(this.opVolver);
            this.mArchivo.MenuItems.Add(this.opSalir);

            this.mPpal.MenuItems.Add(this.mArchivo);

            this.Menu = mPpal;
        }


        Panel BuildFacturaPanel()
        {

            this.pnlFactura = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,

            };

            var lblReserva = new Label()
            {
                Text = "Factura del hotel", 
                Dock = DockStyle.Top,
                Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold, GraphicsUnit.Point),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,

            };

            var lblEspacio = new Label()
            {
                Text = "",
                Dock = DockStyle.Top,

            };
            pnlFactura.Controls.Add(lblEspacio);
            pnlFactura.Controls.Add(lblReserva);


            return pnlFactura;
        }


        Panel BuildEspacioPanel()
        {

            this.pnlEspacio = new Panel()
            {
                Height = 5
            };

            var lblEspacio = new Label()
            {
                Text = " ",
                Dock = DockStyle.Top,

            };
            pnlEspacio.Controls.Add(lblEspacio);

            pnlEspacio.Dock = DockStyle.Fill;
            pnlEspacio.MaximumSize = new Size(int.MaxValue, 5);

            return pnlEspacio;
        }


       
        Panel BuildDatosFactura()
        {

            this.pnlDatosFactura = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 2,
                Dock = DockStyle.Fill,
                Height = 50,
            //    BackColor = Color.Teal
                    
            };

            var lblNumFactura = new Label
            {
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 5),
                Text = "Factura #"+this.NumReserva +this.reserva.NumeroHabitacion,
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                Width = 150,
                TextAlign = ContentAlignment.TopLeft,

            };
            var lblFechaFactura = new Label
            {
                Location = new Point(Right, pnlEspacio.Top + pnlEspacio.Height + 10),
                Text = "Fecha expedicion: " + DateTime.Today.ToString("dd/MM/yyyy"),
                Dock = DockStyle.Right,
                ForeColor = Color.Black,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };
            var lblIdReserva = new Label
            {
                Location = new Point(Left, lblNumFactura.Top + lblNumFactura.Height + 5),
                Text = "Id Reserva: " + this.reserva.Id,
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                Width = 150,
                TextAlign = ContentAlignment.TopLeft,

            };
            this.pnlDatosFactura.Controls.Add(lblNumFactura);
            this.pnlDatosFactura.Controls.Add(lblFechaFactura);
            this.pnlDatosFactura.Controls.Add(lblIdReserva);



        return this.pnlDatosFactura;
        }


        Panel BuildDatosCliente()
        {
            Color colorDatosCliente = Color.LightSteelBlue;
            int ancho = 810;

            this.pnlDatosCliente = new Panel()
            {
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.FixedSingle,

            };

            var lblCliente = new Label
            {
                Location = new Point(Left, this.pnlDatosFactura.Top + 10),
                Text = "Factura para:",
                ForeColor = Color.Black,
                Width = ancho,
                TextAlign = ContentAlignment.TopLeft,

            };
            lblCliente.Font = new Font(lblCliente.Font, FontStyle.Bold);

            

            var lblNombre = new Label
            {
                Location = new Point(Left, lblCliente.Top + lblCliente.Height ),
                Text = "Nombre: " + this.reserva.Cliente.Nombre,
                ForeColor = Color.Black,
                Width = ancho,
                TextAlign = ContentAlignment.BottomLeft,
                BackColor = colorDatosCliente,


            };

            var lblDNI = new Label
            {
                Location = new Point(Left, lblNombre.Top + lblNombre.Height),
                Text = "DNI: " + this.reserva.Cliente.DNI,
                ForeColor = Color.Black,
                Width = ancho,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = colorDatosCliente,

            };

            var lblTelefono = new Label
            {
                Location = new Point(Left, lblDNI.Top + lblDNI.Height ),
                Text = "Telefono: " + this.reserva.Cliente.Telefono,
                ForeColor = Color.Black,
                Width = ancho,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = colorDatosCliente,


            };
            var lblEmail = new Label
            {
                Location = new Point(Left, lblTelefono.Top + lblTelefono.Height ),
                Text = "Email: " + this.reserva.Cliente.Email,
                ForeColor = Color.Black,
                Width = ancho,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = colorDatosCliente,


            };
            var lblDireccion = new Label
            {
                Location = new Point(Left, lblEmail.Top + lblEmail.Height ),
                Text = "Dirección postal: " + this.reserva.Cliente.DireccionPostal,
                ForeColor = Color.Black,
                Width = ancho,
                Height = 35,
                TextAlign = ContentAlignment.TopLeft,
                BackColor = colorDatosCliente,


            };

            this.pnlDatosCliente.Height = lblCliente.Height + lblDNI.Height + lblNombre.Height + lblTelefono.Height + lblEmail.Height + lblDireccion.Height;

            this.pnlDatosCliente.Controls.Add(lblCliente);
            this.pnlDatosCliente.Controls.Add(lblNombre);
            this.pnlDatosCliente.Controls.Add(lblDNI);
            this.pnlDatosCliente.Controls.Add(lblTelefono);
            this.pnlDatosCliente.Controls.Add(lblEmail);
            this.pnlDatosCliente.Controls.Add(lblDireccion);

            return this.pnlDatosCliente;
        }


        Panel BuildContenidoFactura()
        {
            string garaje;
            if (this.reserva.Garaje == "SI") { garaje = " con garaje"; }
            else { garaje = " sin garaje"; }

            int heightCampoDatos = 100;
   

            this.pnlContenidoFactura = new TableLayoutPanel()
            {
                ColumnCount = 6,
                RowCount = 2,
                Dock = DockStyle.Fill,
                //  BackColor = Color.White,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset,
                
                    
            };

            //campos descripcion de la reserva
            var lblConcepto = new Label
            {
                Text = "Concepto",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 150,
            };
            var lblDatosConcepto = new Label
            {
                Text = "\nHabitacion " +this.reserva.Tipo + garaje,
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopLeft,
                Height = heightCampoDatos,
                Width = 150,

            };


            //campos fecha de entrada
            var lblFechaEntrada = new Label
            {
                Text = "Fecha entrada",
                Dock = DockStyle.Right,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            var lblDatosFechaEntrada = new Label
            {
                Text = "\n" + this.reserva.FechaEntrada.ToString("dd/MM/yyyy"),
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,
                Height = heightCampoDatos,
            };

            //campos fecha de salida
            var lblFechaSalida = new Label
            {
                Text = "Fecha salida",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
            };
            var lblDatosFechaSalida = new Label
            {
                Text = "\n" + this.reserva.FechaSalida.ToString("dd/MM/yyyy"),
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,
                Height = heightCampoDatos,
            };

            //campos numero de dias
            var lblNumDias = new Label
            {
                Text = "Dias",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 50
            };
            var lblDatosNumDias = new Label
            {
                Text = "\n" + this.reserva.NumDias.ToString(),
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,
                Width = 50,
                Height = heightCampoDatos,
            };

            //campos precio/Dia
            var lblPrecioDia = new Label
            {
                Text = "Precio/Dia",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 80
            };
            var lblDatosPrecioDia = new Label
            {
                Text = "\n" + this.reserva.PrecioDia.ToString() +" €",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,
                Width = 80,
                Height = heightCampoDatos,
            };

            //campos importe
            var lblImporte = new Label
            {
                Text = "Importe",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 80
            };
            var lblDatosImporte = new Label
            {
                Text = "\n"+this.reserva.TotalSinIva().ToString() +" €",
                Dock = DockStyle.Left,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopCenter,
                Height = heightCampoDatos,
                Width = 80
            };

            this.pnlContenidoFactura.Height = lblConcepto.Height + lblDatosConcepto.Height;
            this.pnlContenidoFactura.Width = lblConcepto.Width + lblFechaEntrada.Width + lblFechaSalida.Width + lblNumDias.Width + lblPrecioDia.Width + lblImporte.Width;


            this.pnlContenidoFactura.Controls.Add(lblConcepto,0,0); //Add(control,columna,fila)
            this.pnlContenidoFactura.Controls.Add(lblDatosConcepto,0,1); 

            this.pnlContenidoFactura.Controls.Add(lblFechaEntrada,1,0);
            this.pnlContenidoFactura.Controls.Add(lblDatosFechaEntrada,1,1);

            this.pnlContenidoFactura.Controls.Add(lblFechaSalida,2,0);
            this.pnlContenidoFactura.Controls.Add(lblDatosFechaSalida,2,1);

            this.pnlContenidoFactura.Controls.Add(lblNumDias,3,0);
            this.pnlContenidoFactura.Controls.Add(lblDatosNumDias,3,1);

            this.pnlContenidoFactura.Controls.Add(lblPrecioDia,4,0);
            this.pnlContenidoFactura.Controls.Add(lblDatosPrecioDia,4,1);

            this.pnlContenidoFactura.Controls.Add(lblImporte,5,0);
            this.pnlContenidoFactura.Controls.Add(lblDatosImporte,5,1);

            return this.pnlContenidoFactura;
        }


        Panel BuildImportesTotales()
        {

            this.pnlImportesTotales = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 3,
                Dock = DockStyle.Right,
                Height = 50,

            };

            var lblTotalSinIva = new Label
            {
                Text = "Total sin IVA: ",
                Dock = DockStyle.Right,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight,

            };
            var lblDatosTotalSinIva = new Label
            {
                Text = this.reserva.TotalSinIva().ToString()+" €",
                ForeColor = Color.Black,
                Width = 70,
                TextAlign = ContentAlignment.MiddleRight,

            };

            var lblIva = new Label
            {
                Text = this.reserva.IVA +"% IVA: ",
                Dock = DockStyle.Right,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleRight,

            };
           
            var lblDatosIva = new Label
            {
                Text = (this.reserva.TotalSinIva()*(this.reserva.IVA/100.0)).ToString() + " €",
                ForeColor = Color.Black,
                Width = 70,
                TextAlign = ContentAlignment.MiddleRight,

            };


            var lblTotalConIva = new Label
            {
                Text = "Total con IVA: ",
                Dock = DockStyle.Right,
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.TopRight,
                    

            };

            var lblDatosTotalConIva = new Label
            {
                Text = this.reserva.TotalConIva().ToString() + " €",
                ForeColor = Color.Black,
                Width = 70,
                TextAlign = ContentAlignment.TopRight,

            };

            lblTotalConIva.Font = new Font(lblTotalConIva.Font, FontStyle.Bold);
            lblDatosTotalConIva.Font = new Font(lblDatosTotalConIva.Font, FontStyle.Bold);

            this.pnlImportesTotales.Height = lblTotalSinIva.Height * 4;

            this.pnlImportesTotales.Controls.Add(lblTotalSinIva, 0, 0); //Add(control,columna,fila)
            this.pnlImportesTotales.Controls.Add(lblDatosTotalSinIva, 1, 0);

            this.pnlImportesTotales.Controls.Add(lblIva, 0, 1);
            this.pnlImportesTotales.Controls.Add(lblDatosIva, 1, 1);

            this.pnlImportesTotales.Controls.Add(lblTotalConIva, 0, 3);
            this.pnlImportesTotales.Controls.Add(lblDatosTotalConIva, 1, 3);
            


            return this.pnlImportesTotales;
        }


        private Panel pnlFactura;
        private Panel pnlEspacio;
        private Panel pnlDatosFactura;
        private Panel pnlDatosCliente;
        private TableLayoutPanel pnlContenidoFactura;
        private TableLayoutPanel pnlImportesTotales;


        private Panel pnlInserta;



        public StatusBar SbStatus;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opVolver;
        public MenuItem opSalir;
        public List<Cliente> rgCli = null;
        public List<Habitacion> rgHab = null;

        private Reserva reserva;
        private Cliente cliente;

        private int NumReserva;
        public RegistroReserva Reservas;

    }
}