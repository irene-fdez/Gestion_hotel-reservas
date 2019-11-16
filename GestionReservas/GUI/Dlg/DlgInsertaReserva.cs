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

    public class DlgInsertaReserva : Form
    {

        public DlgInsertaReserva()
        {
            this.Build();
        }

        public DlgInsertaReserva(List<Cliente> clientesList)
        {
            this.rgCli = clientesList;
            this.Build();
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
            var pnlnumHabitacion = this.BuildHabitacion();
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

            this.Text = "Gestion de un hotel - Añadir reserva";

            Console.WriteLine(pnlReserva.Height);

            this.Size = new Size(600,
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnlTipo.Height + pnlDateIn.Height + dtpDateOut.Height +
                      pnlGaraje.Height + pnlPrecioDia.Height + pnlIva.Height + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnlTipo.Height + pnlDateIn.Height + dtpDateOut.Height +
                      pnlGaraje.Height + pnlPrecioDia.Height + pnlIva.Height + pnlBotones.Height);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
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
            // this.mInsertar = new MenuItem("&Insertar");

            this.opSalir = new MenuItem("&Salir");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

            this.mArchivo.MenuItems.Add(this.opSalir);

            this.mPpal.MenuItems.Add(this.mArchivo);

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




        Panel BuildHabitacion()
        {
            this.pnlNumHabitacion = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Height = 35,

            };

            var lblHabitacion = new Label
            {
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Text = "Habitacion:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                // Left = 20,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.cbNumHabitacionList = new ComboBox
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 20,
            };


            //obtener los numeros de todas las habitaciones
            //  cbNumHabitacionList.Items.Add("AM");

            pnlNumHabitacion.Controls.Add(cbNumHabitacionList);
            pnlNumHabitacion.Controls.Add(lblHabitacion);

            return pnlNumHabitacion;

        }

        Panel BuildCliente()
        {
            this.pnlDniCliente = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlNumHabitacion.Top + this.pnlNumHabitacion.Height + 10),
                Height = 35,

            };

            var lblCliente = new Label
            {
                Location = new Point(Left, this.pnlNumHabitacion.Top + this.pnlNumHabitacion.Height + 10),
                Text = "Cliente:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,

            };

            this.cbDniClienteList = new ComboBox
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 20,

            };

            string[] op = new string[this.rgCli != null ? this.rgCli.Count : 0];
            for (int i = 0; i < op.Length; i++)
            {
                Cliente cliente = this.rgCli[i];
                op[i] = cliente.DNI ;

            }
            cbDniClienteList.Items.AddRange(op);

            pnlDniCliente.Controls.Add(cbDniClienteList);
            pnlDniCliente.Controls.Add(lblCliente);

            return pnlDniCliente;

        }


        Panel BuilTipo()
        {
            this.pnlTipo = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlNumHabitacion.Top + this.pnlNumHabitacion.Height + 10),

            };

            var lblTipo = new Label()
            {
                Location = new Point(Left, this.pnlNumHabitacion.Top + this.pnlNumHabitacion.Height + 10),
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

            };

            this.tbTipo.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Tipo);

                invalid = invalid || !char.IsLetter(this.Tipo[0]);

                if (invalid)
                {
                    this.tbTipo.Text = "¿ Tipo ?";
                }

                btAccept.Enabled = !invalid;
                //  cancelArgs.Cancel = invalid;
            };

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
            Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

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
                MinDate = DateTime.Today,
                Value = DateTime.Today,
                Anchor = AnchorStyles.Bottom,


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
                MinDate = DateTime.Today,
                Value = DateTime.Today,
                Anchor = AnchorStyles.Bottom,


            };

            this.dtpDateOut.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = false;

                invalid = invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value);

                if (invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value))
                {
                    MessageBox.Show("La fecha de salida debe ser mayor o igual de la de entrada");
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

            this.pnlGaraje.Controls.Add(cbGaraje);
            this.pnlGaraje.Controls.Add(lblGaraje);

            //  this.pnlGaraje.MaximumSize = new Size(int.MaxValue, this.rbSI.Height * 2 + 5);

            return this.pnlGaraje;
        }

        Panel BuildPrecioDia()
        {
            this.pnlPrecioDia = new Panel()
            {
                Dock = DockStyle.Top,
                Location = new Point(Left, this.pnlGaraje.Top + this.pnlGaraje.Height + 10),
                Height = 35
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
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                //  Dock = DockStyle.Fill,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,

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
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                //  Dock = DockStyle.Fill,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,

            };


            this.pnlIva.Controls.Add(this.numIva);
            this.pnlIva.Controls.Add(lblIva);

            //    this.pnlIva.MaximumSize = new Size(int.MaxValue, numIva.Height * 2);

            return this.pnlIva;
        }


        private ComboBox cbNumHabitacionList;
        private ComboBox cbDniClienteList;
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
        private Panel pnlNumHabitacion;
        private Panel pnlDniCliente;

        private Panel pnlInserta;

        public string DniCliente => this.cbDniClienteList.Text;
        public string NumHabitacion => this.cbNumHabitacionList.Text;
        public string Tipo => this.tbTipo.Text;
        public DateTime FechaEntrada => this.dtpDateIn.Value;
        public DateTime FechaSalida => this.dtpDateOut.Value;
        public string Garaje => this.cbGaraje.Text;
        public double PrecioDia => System.Convert.ToDouble(this.numPrecioDia.Value);
        public int Iva => (int)this.numIva.Value;


        public StatusBar SbStatus;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        //  public MenuItem mInsertar;
        public MenuItem opSalir;
        public List<Cliente> rgCli = null;



    }
}