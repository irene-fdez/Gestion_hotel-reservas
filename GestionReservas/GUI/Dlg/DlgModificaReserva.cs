﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionReservas.Core;


namespace GestionReservas.GUI.Dlg
{
    using System.Windows.Forms;
    using System.Drawing;

    public class DlgModificaReserva : Form
    {

        public DlgModificaReserva(Reserva reserva, List<Cliente> clientes)
        {
            
            this.Reservas = new RegistroReserva(clientes);
            this.reserva = reserva;
            

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
                BackColor = Color.FromArgb(49, 66, 82),
            };

            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlReserva = this.BuildReservaPanel();
            pnlInserta.Controls.Add(pnlReserva);

            var pnlEspacio = this.BuildEspacioPanel();
            pnlInserta.Controls.Add(pnlEspacio);

            //Habitacion
            var pnlnumHabitacion = this.BuildID();
            pnlInserta.Controls.Add(pnlnumHabitacion);

            //Cliente
            var pnldniCliente = this.BuildCliente();
            pnlInserta.Controls.Add(pnldniCliente);

            //Tipo reserva
            var pnlTipo = this.BuilTipo();
            pnlInserta.Controls.Add(pnlTipo);
            

            //F_entrada
            var pnlDateIn = this.BuildFechaEntrada();
            pnlInserta.Controls.Add(pnlDateIn);

            //F_salida
            var pnlDateOut = this.BuildFechaSalida();
            pnlInserta.Controls.Add(pnlDateOut);

            //Garaje
            var pnlGaraje = this.BuildGaraje();
            pnlInserta.Controls.Add(pnlGaraje);

            //Precio/dia
            var pnlPrecioDia = this.BuildPrecioDia();
            pnlInserta.Controls.Add(pnlPrecioDia);

            //Iva
            var pnlIva = this.BuildIva();
            pnlInserta.Controls.Add(pnlIva);

            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Modificar reserva";

            Console.WriteLine(pnlReserva.Height);

            this.Size = new Size(600,
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnlTipo.Height + pnlDateIn.Height + dtpDateOut.Height +
                      pnlGaraje.Height + pnlPrecioDia.Height + pnlIva.Height + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnlTipo.Height + pnlDateIn.Height + dtpDateOut.Height +
                      pnlGaraje.Height + pnlPrecioDia.Height + pnlIva.Height + pnlBotones.Height);

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

            this.mBuscar = new MenuItem("&Buscar");

            this.mArchivo.MenuItems.Add(this.opVolver);
            this.mArchivo.MenuItems.Add(this.opSalir);

            this.mPpal.MenuItems.Add(this.mArchivo);
            this.mPpal.MenuItems.Add(this.mBuscar);

            this.Menu = mPpal;


        }



        Panel BuildBotonesPanel()
        {

            var pnlBotones = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Top,

            };

            var btCierra = new Button()
            {
                Text = "&Cancelar",
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(69, 93, 117),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Silver,

            };

            var btGuarda = new Button()
            {
                Text = "&Guardar",
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(69, 93, 117),
                Font = new Font("Microsoft Sans Serif", 9, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Silver,

            };

            pnlBotones.Controls.Add(btGuarda);
            pnlBotones.Controls.Add(btCierra);

            this.AcceptButton = btGuarda;
            this.CancelButton = btCierra;

            pnlBotones.Controls.Add(btGuarda);
            pnlBotones.Controls.Add(btCierra);


            return pnlBotones;
        }

        Panel BuildReservaPanel()
        {

            this.pnlReserva = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,

            };

            var lblReserva = new Label()
            {
                Text = "Datos de la reserva",
                Dock = DockStyle.Top,
                Font = new Font("Microsoft Sans Serif", 18, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.TopCenter,

            };

            var lblEspacio = new Label()
            {
                Text = "",
                Dock = DockStyle.Top,

            };
            pnlReserva.Controls.Add(lblEspacio);
            pnlReserva.Controls.Add(lblReserva);


            return pnlReserva;
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




        Panel BuildID()
        {

            this.pnlId = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Height = 35,

            };

            var lblId = new Label
            {
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Text = "Id:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                // Left = 20,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.tbId = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.reserva.Id,
                ReadOnly = true,
                BackColor = Color.FromArgb(185, 216, 244),
                Enabled = false,
                ForeColor = Color.Black,
                TextAlign = HorizontalAlignment.Center,

            };


            pnlId.Controls.Add(tbId);
            pnlId.Controls.Add(lblId);

            return pnlId;

        }

        Panel BuildCliente()
        {
            this.pnlDniCliente = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlId.Top + this.pnlId.Height + 10),
                Height = 35,

            };

            var lblCliente = new Label
            {
                Location = new Point(Left, this.pnlId.Top + this.pnlId.Height + 10),
                Text = "Cliente:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.tbCliente = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.reserva.Cliente.DNI,
                ReadOnly = true,
                BackColor = Color.FromArgb(185, 216, 244),
                Enabled = false,
                TextAlign = HorizontalAlignment.Center,
            };


            pnlDniCliente.Controls.Add(tbCliente);
            pnlDniCliente.Controls.Add(lblCliente);

            return pnlDniCliente;

        }


        Panel BuilTipo()
        {
            this.pnlTipo = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlId.Top + this.pnlId.Height + 10),

            };

            var lblTipo = new Label()
            {
                Location = new Point(Left, this.pnlId.Top + this.pnlId.Height + 10),
                Text = "Tipo:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.tbTipo = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.reserva.Tipo,
                ReadOnly = true,
                BackColor = Color.FromArgb(185, 216, 244),
                Enabled = false,
                ForeColor = Color.Black,
                TextAlign = HorizontalAlignment.Center,
            };

            this.tbTipo.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Tipo);

                invalid = invalid || this.tbTipo.Text == "";

                if (invalid || this.tbTipo.Text == "")
                {

                    string mensaje = "El campo no puede estar vacio";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.tbTipo.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            this.tbTipo.Focus();
            pnlTipo.MaximumSize = new Size(int.MaxValue, tbTipo.Height * 2);

            pnlTipo.Controls.Add(this.tbTipo);
            pnlTipo.Controls.Add(lblTipo);

            return pnlTipo;
        }



        Panel BuildFechaEntrada()
        {
            this.pnlDateIn = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlTipo.Top + this.pnlTipo.Height + 10),

            };
           // Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

            var lblDateIn = new Label()
            {
                Location = new Point(Left, (this.pnlTipo.Top + this.pnlTipo.Height + 10)),
                Text = "Fecha de entrada:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.dtpDateIn = new DateTimePicker()
            {
                Left = 0,
                Width = 250,
                Value = this.reserva.FechaEntrada,
                Anchor = AnchorStyles.Bottom,

            };

            this.dtpDateIn.Validating += (sender, cancelArgs) =>
            {

                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value); ;

                if (invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value))
                {
                    string mensaje = "La fecha de salida debe ser mayor o igual de la de entrada";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dtpDateIn.Focus();
                }

                btAccept.Enabled = !invalid;

            };

            pnlDateIn.MaximumSize = new Size(int.MaxValue, dtpDateIn.Height * 2);

            pnlDateIn.Controls.Add(this.dtpDateIn);
            pnlDateIn.Controls.Add(lblDateIn);

            return pnlDateIn;
        }


        Panel BuildFechaSalida()
        {
            this.pnlDateOut = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlDateIn.Top + this.pnlDateIn.Height + 10),
            };

            var lblDateOut = new Label()
            {
                Location = new Point(Left, (this.pnlDateIn.Top + this.pnlDateIn.Height + 10)),
                Text = "Fecha de salida:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.dtpDateOut = new DateTimePicker()
            {
                Left = 0,
                Width = 250,
                Value = this.reserva.FechaSalida,
                Anchor = AnchorStyles.Bottom,

            };

            this.dtpDateOut.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = false;

                invalid = invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value);

                if (invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value))
                {
                    string mensaje = "La fecha de salida debe ser mayor o igual de la de entrada";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dtpDateOut.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            

            pnlDateOut.MaximumSize = new Size(int.MaxValue, dtpDateOut.Height * 2);

            pnlDateOut.Controls.Add(this.dtpDateOut);
            pnlDateOut.Controls.Add(lblDateOut);

            return pnlDateOut;
        }



        Panel BuildGaraje()
        {

            this.pnlGaraje = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlDateOut.Top + this.pnlDateOut.Height + 10),
                Height = 35,
            };

            var lblGaraje = new Label
            {
                Text = "Garaje: ",
                Dock = DockStyle.Left,
                Location = new Point(Left, this.pnlDateOut.Top + this.pnlDateOut.Height + 10),
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.cbGaraje = new ComboBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 20,
            };

            cbGaraje.Items.Add("SI");
            cbGaraje.Items.Add("NO");

            //  Console.WriteLine("id: "+this.reserva.Id +"\ncb: "+this.reserva.Garaje);
            this.validarFechaSalida();



            this.cbGaraje.SelectedIndex = (this.reserva.Garaje == "SI" ? 0 : 1);

            this.pnlGaraje.Controls.Add(cbGaraje);
            this.pnlGaraje.Controls.Add(lblGaraje);


            return this.pnlGaraje;
        }

        Panel BuildPrecioDia()
        {
            this.pnlPrecioDia = new Panel()
            {
                Dock = DockStyle.Top,
                Location = new Point(Left, this.pnlGaraje.Top + this.pnlGaraje.Height + 10),
                Height = 35,
            };

            var lblPrecioDia = new Label()
            {
                Text = "Pecio/Dia: ",
                Dock = DockStyle.Left,
                Location = new Point(Left, this.pnlGaraje.Top + this.pnlGaraje.Height + 10),
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.numPrecioDia = new NumericUpDown
            {
                Value = (decimal)this.reserva.PrecioDia,
                TextAlign = HorizontalAlignment.Center,
                //  Dock = DockStyle.Fill,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                ReadOnly = true,
                BackColor = Color.FromArgb(185, 216, 244),
                Enabled = false,

            };


            this.pnlPrecioDia.Controls.Add(this.numPrecioDia);
            this.pnlPrecioDia.Controls.Add(lblPrecioDia);

            //   this.pnlPrecioDia.MaximumSize = new Size(int.MaxValue, numPrecioDia.Height * 2);

            return this.pnlPrecioDia;
        }

        Panel BuildIva()
        {
            this.pnlIva = new Panel()
            {
                Dock = DockStyle.Top,
                Location = new Point(Left, this.pnlPrecioDia.Top + this.pnlPrecioDia.Height + 10),
                Height = 35
            };

            var lblIva = new Label()
            {
                Text = "IVA: ",
                Dock = DockStyle.Left,
                Location = new Point(Left, this.pnlPrecioDia.Top + this.pnlPrecioDia.Height + 10),
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.numIva = new NumericUpDown
            {
                Value = this.reserva.IVA,
                TextAlign = HorizontalAlignment.Center,
                //  Dock = DockStyle.Fill,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                ReadOnly = true,
                BackColor = Color.FromArgb(185, 216, 244),
                Enabled = false,

            };


            this.pnlIva.Controls.Add(this.numIva);
            this.pnlIva.Controls.Add(lblIva);

            //    this.pnlIva.MaximumSize = new Size(int.MaxValue, numIva.Height * 2);

            return this.pnlIva;
        }

        void validarFechaSalida()
        {
            this.dtpDateOut.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = false;

                invalid = invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value);

                if (invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value))
                {
                    string mensaje = "La fecha de salida debe ser mayor o igual de la de entrada";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dtpDateOut.Focus();
                }

                btAccept.Enabled = !invalid;
            };
        }

        private TextBox tbId;
        private TextBox tbCliente;
        private TextBox tbTipo;
        private DateTimePicker dtpDateIn;
        private DateTimePicker dtpDateOut;
        private ComboBox cbGaraje;
        private NumericUpDown numPrecioDia;
        private NumericUpDown numIva;


        private Panel pnlReserva;
        private Panel pnlEspacio;
        private Panel pnlTipo;
        private Panel pnlDateIn;
        private Panel pnlDateOut;
        private Panel pnlGaraje;
        private Panel pnlPrecioDia;
        private Panel pnlIva;
        private Panel pnlId;
        private Panel pnlDniCliente;

        private Panel pnlInserta;

        public string DniCliente => this.tbCliente.Text;
        public string Id => this.tbId.Text;
        public string Tipo => this.tbTipo.Text;
        public DateTime FechaEntrada => this.dtpDateIn.Value;
        public DateTime FechaSalida => this.dtpDateOut.Value;
        public string Garaje => this.cbGaraje.Text;
        public double PrecioDia => System.Convert.ToDouble(this.numPrecioDia.Value);
        public int Iva => (int)this.numIva.Value;


        public StatusBar SbStatus;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opVolver;
        public MenuItem opSalir;
        public MenuItem mBuscar;
        public List<Cliente> rgCli = null;
        public List<Habitacion> rgHab = null;

        private Reserva reserva;
        public RegistroReserva Reservas;

    }
}