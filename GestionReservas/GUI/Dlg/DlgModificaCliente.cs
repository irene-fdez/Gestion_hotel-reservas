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
    class DlgModificaCliente : Form
    {
        //constructor
        public DlgModificaCliente(Cliente cli)
        {
            this.Cli = cli;
            this.Build();
            this.CenterToScreen();

            var RegClientes = RegistroClientes.RecuperarXml();
            var DCC = new DlgConsultaCliente(RegClientes);
            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; DCC.Salir(); };
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

            var pnlCliente = this.BuildClientePanel();
            pnlInserta.Controls.Add(pnlCliente);

            var pnlEspacio = this.BuildEspacioPanel();
            pnlInserta.Controls.Add(pnlEspacio);

            //DNI
            var pnlDNI = this.BuildDNI();
            pnlInserta.Controls.Add(pnlDNI);

            //Nombre
            var pnlNombre = this.BuildNombre();
            pnlInserta.Controls.Add(pnlNombre);

            //Telefono
            var pnlTelef = this.BuildTelefono();
            pnlInserta.Controls.Add(pnlTelef);


            //Email
            var pnlEmail = this.BuildEmail();
            pnlInserta.Controls.Add(pnlEmail);


            //Direccion Postal
            var pnlDirPostal = this.BuildDirPostal();
            pnlInserta.Controls.Add(pnlDirPostal);


            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Añadir reserva";

            Console.WriteLine(pnlCliente.Height);

            this.Size = new Size(600,
                      60 + pnlCliente.Height + pnlEspacio.Height + pnlDNI.Height + pnlNombre.Height + pnlTelef.Height +
                      pnlEmail.Height + pnlDirPostal.Height + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                      60 + pnlCliente.Height + pnlEspacio.Height + pnlDNI.Height + pnlNombre.Height + pnlTelef.Height +
                      pnlEmail.Height + pnlDirPostal.Height + pnlBotones.Height);

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

        Panel BuildClientePanel()
        {

            this.pnlCliente = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,
            };

            var lblCliente = new Label()
            {
                Text = "Datos del cliente",
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

            pnlCliente.Controls.Add(lblEspacio);
            pnlCliente.Controls.Add(lblCliente);

            return pnlCliente;
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


        Panel BuildDNI()
        {
            this.pnlDNI = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
            };

            var lblDNI = new Label()
            {
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Text = "DNI:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.tbDNI = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.Cli.DNI
            };

            this.tbDNI.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.DNI);

                invalid = invalid || this.tbDNI.Text == "";

                if (invalid || this.tbDNI.Text == "")
                {

                    string mensaje = "El campo no puede estar vacio";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.tbDNI.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlDNI.MaximumSize = new Size(int.MaxValue, tbDNI.Height * 2);

            pnlDNI.Controls.Add(this.tbDNI);
            pnlDNI.Controls.Add(lblDNI);

            return pnlDNI;
        }

        Panel BuildNombre()
        {
            this.pnlNombre = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlDNI.Top + this.pnlDNI.Height + 10),
            };

            var lblNombre = new Label()
            {
                Location = new Point(Left, this.pnlDNI.Top + this.pnlDNI.Height + 10),
                Text = "Nombre:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.tbNombre = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.Cli.Nombre
            };

            this.tbNombre.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Nombre);

                invalid = invalid || this.tbNombre.Text == "";

                if (invalid || this.tbNombre.Text == "")
                {

                    string mensaje = "El campo no puede estar vacio";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.tbNombre.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlNombre.MaximumSize = new Size(int.MaxValue, tbNombre.Height * 2);

            pnlNombre.Controls.Add(this.tbNombre);
            pnlNombre.Controls.Add(lblNombre);

            return pnlNombre;
        }

        Panel BuildTelefono()
        {
            this.pnlTelef = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlNombre.Top + this.pnlNombre.Height + 10),
            };

            var lblTelef = new Label()
            {
                Location = new Point(Left, this.pnlNombre.Top + this.pnlNombre.Height + 10),
                Text = "Telefono:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.mtbTelef = new MaskedTextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Mask = "000000000",
                Text = this.Cli.Telefono.ToString()
            };

            pnlTelef.MaximumSize = new Size(int.MaxValue, mtbTelef.Height * 2);

            pnlTelef.Controls.Add(this.mtbTelef);
            pnlTelef.Controls.Add(lblTelef);

            return pnlTelef;
        }

        Panel BuildEmail()
        {

            this.pnlEmail = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlTelef.Top + this.pnlTelef.Height + 10),
            };

            var lblEmail = new Label()
            {
                Location = new Point(Left, this.pnlTelef.Top + this.pnlTelef.Height + 10),
                Text = "Email:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };


            this.tbEmail = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.Cli.Email
            };

            this.tbEmail.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Email);

                invalid = invalid || this.tbEmail.Text == "";

                if (invalid || this.tbEmail.Text == "")
                {

                    string mensaje = "El campo no puede estar vacio";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.tbEmail.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlEmail.MaximumSize = new Size(int.MaxValue, tbEmail.Height * 2);

            pnlEmail.Controls.Add(this.tbEmail);
            pnlEmail.Controls.Add(lblEmail);

            return pnlEmail;
        }

        Panel BuildDirPostal()
        {
            this.pnlDirPostal = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlEmail.Top + this.pnlEmail.Height + 10),
            };

            var lblDirPostal = new Label()
            {
                Location = new Point(Left, this.pnlEmail.Top + this.pnlEmail.Height + 10),
                Text = "DirPostal:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.tbDirPostal = new TextBox()
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                Text = this.Cli.DireccionPostal
            };

            this.tbDirPostal.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.DirPostal);

                invalid = invalid || this.tbDirPostal.Text == "";

                if (invalid || this.tbDirPostal.Text == "")
                {

                    string mensaje = "El campo no puede estar vacio";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.tbDirPostal.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlDirPostal.MaximumSize = new Size(int.MaxValue, tbDirPostal.Height * 2);

            pnlDirPostal.Controls.Add(this.tbDirPostal);
            pnlDirPostal.Controls.Add(lblDirPostal);

            return pnlDirPostal;
        }


        private Cliente Cli;

        private TextBox tbDNI;


        private Panel pnlCliente;
        private Panel pnlEspacio;
        private Panel pnlDNI;

        private Panel pnlInserta;

        public string DNI => this.tbDNI.Text;
        public string Nombre => this.tbNombre.Text;

        public long Telefono => Convert.ToInt64(this.mtbTelef.Text);
        public string Email => this.tbEmail.Text;

        public string DirPostal => this.tbDirPostal.Text;

        public StatusBar SbStatus;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opVolver;
        public MenuItem opSalir;
        public MenuItem mBuscar;
        private Panel pnlNombre;
        private TextBox tbNombre;
        private Panel pnlEmail;
        private TextBox tbEmail;
        private Panel pnlTelef;
        private Panel pnlDirPostal;
        private TextBox tbDirPostal;

        private MaskedTextBox mtbTelef;
        public string Mask { get; set; }

        private RegistroClientes RegClientes;

    }
}

