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
            this.CenterToScreen();
        }

        public DlgInsertaReserva(RegistroReserva Reservas, List<Cliente> clientesList, List<Habitacion> habitacionesList)
        {
            var MVC = new MainWindowCore();

            this.Reservas = Reservas;
            this.rgCli = clientesList;
            this.rgHab = habitacionesList;
            this.Build();
            this.CenterToScreen();


            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; MVC.Salir(); };
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


            //tipo Habitacion
            var pnltipoHabitacion = this.BuildTipoHabitacion();
            pnlInserta.Controls.Add(pnltipoHabitacion);

            //numero Habitacion
            var pnlnumHabitacion = this.BuildHabitacion();
            pnlInserta.Controls.Add(pnlnumHabitacion);

            //Cliente
            var pnldniCliente = this.BuildCliente();
            pnlInserta.Controls.Add(pnldniCliente);

            //Tipo reserva
         /*   var pnlTipo = this.BuilTipo();
            pnlInserta.Controls.Add(pnlTipo);*/

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
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnltipoHabitacion.Height + pnlDateIn.Height + dtpDateOut.Height +
                      pnlGaraje.Height + pnlPrecioDia.Height + pnlIva.Height + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                      100 + pnlReserva.Height + pnlEspacio.Height + pnlnumHabitacion.Height + pnldniCliente.Height + pnltipoHabitacion.Height + pnlDateIn.Height + dtpDateOut.Height +
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

        Panel BuildTipoHabitacion()
        {
            this.pnlTipoHabitacion = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Height = 35,
            };

            var lblTipoHabitacion = new Label
            {
                Location = new Point(Left, this.pnlEspacio.Top + this.pnlEspacio.Height + 10),
                Text = "Tipo de habitacion:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.cbTipoHabitacion = new ComboBox
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 20,
            };

            cbTipoHabitacion.Items.Add("matrimoniales");
            cbTipoHabitacion.Items.Add("doble");
            cbTipoHabitacion.Items.Add("individuales");


            Console.WriteLine("tipo habita select: "+cbTipoHabitacion.Text);
            //obtener los numeros de habitacion de un tipo en concreto 
            cbTipoHabitacion.SelectedValueChanged += (sender, e) =>
            {
                cbNumHabitacionList.Items.Clear();
                Console.WriteLine("tipo habita select" + cbTipoHabitacion.Text);
                foreach (Habitacion h in this.rgHab)
                {
                    //parseo combobox text to tipos enum 
                    Habitacion.Tipos parsedTipo = default(Habitacion.Tipos);
                    var element = cbTipoHabitacion.Text;
                    if(element != null)
                    {
                        // Try to parse
                        Enum.TryParse<Habitacion.Tipos>(element, out parsedTipo);
                    }

                    if (h.Tipo == parsedTipo)
                    {
                        cbNumHabitacionList.Items.Add(h.Numero);
                    }
                }

                if(cbTipoHabitacion.Text == "matrimoniales")
                {
                    numPrecioDia.Value = 42;
                }
                else if (cbTipoHabitacion.Text == "doble")
                {
                    numPrecioDia.Value = 38;
                }
                else
                {
                    numPrecioDia.Value = 31;
                }
            };
            

            this.cbTipoHabitacion.Validating += (sender, cancelArgs) =>
            {
                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (cbTipoHabitacion.Text == "");

                if (invalid || cbTipoHabitacion.Text == "")
                {
                    string mensaje = "Debe seleccionar algún elemento";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbTipoHabitacion.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlTipoHabitacion.Controls.Add(cbTipoHabitacion);
            pnlTipoHabitacion.Controls.Add(lblTipoHabitacion);

            return pnlTipoHabitacion;
        }


        Panel BuildHabitacion()
        {
            this.pnlNumHabitacion = new Panel
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlTipoHabitacion.Top + this.pnlTipoHabitacion.Height + 10),
                Height = 35,
            };

            var lblHabitacion = new Label
            {
                Location = new Point(Left, this.pnlTipoHabitacion.Top + this.pnlTipoHabitacion.Height + 10),
                Text = "Habitacion:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
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

            
            this.cbNumHabitacionList.Validating += (sender, cancelArgs) =>
            {
                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid ||(cbNumHabitacionList.Text == "");

                if (invalid || cbNumHabitacionList.Text == "")
                {
                    string mensaje = "Debe seleccionar algún elemento";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbNumHabitacionList.Focus();
                }

                btAccept.Enabled = !invalid;
            };

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


            this.cbDniClienteList.Validating += (sender, cancelArgs) =>
            {
                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (cbDniClienteList.Text == "");

                if (invalid || cbDniClienteList.Text == "")
                {
                    string mensaje = "Debe seleccionar algún elemento";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbDniClienteList.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlDniCliente.Controls.Add(cbDniClienteList);
            pnlDniCliente.Controls.Add(lblCliente);

            return pnlDniCliente;

        }


        Panel BuildFechaEntrada()
        {
            this.pnlDateIn = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlDniCliente.Top + this.pnlDniCliente.Height + 10),
            };
        //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

            var lblDateIn = new Label()
            {
                Location = new Point(Left, (this.pnlDniCliente.Top + this.pnlDniCliente.Height + 10)),
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



            this.dtpDateIn.Validating += (sender, cancelArgs) =>
            {

                var id = dtpDateIn.Value.Year.ToString() + dtpDateIn.Value.Month.ToString() + dtpDateIn.Value.Day.ToString() + cbNumHabitacionList.SelectedItem;

                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (this.Reservas.getReserva(id) != null) || (this.dtpDateOut.Value < this.dtpDateIn.Value); ;

                if (invalid)
                {
                    if (this.Reservas.getReserva(id) != null)
                    {
                        DialogResult result;
                        string mensaje = "Lo sentimos! Esta habitacion ya tiene una reserva es esa fecha";

                        result = MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dtpDateIn.Focus();


                    }

                    
                    if (invalid || (this.dtpDateOut.Value < this.dtpDateIn.Value))
                    {
                        string mensaje = "La fecha de salida debe ser mayor o igual de la de entrada";
                        MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.dtpDateOut.Focus();
                    }
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
                MinDate = DateTime.Today,
                Value = DateTime.Today,
                Anchor = AnchorStyles.Bottom,
            };

            this.validarFechaSalida();

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

            this.cbGaraje.Validating += (sender, cancelArgs) =>
            {
                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (cbGaraje.Text == "");

                if (invalid || cbGaraje.Text == "")
                {
                    string mensaje = "Debe seleccionar algún elemento";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbGaraje.Focus();
                }

                btAccept.Enabled = !invalid;
            };
            this.validarFechaSalida();

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
                TextAlign = HorizontalAlignment.Right,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                ReadOnly = true,
            };

            this.validarFechaSalida();

            this.pnlPrecioDia.Controls.Add(this.numPrecioDia);
            this.pnlPrecioDia.Controls.Add(lblPrecioDia);


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
                Value = 21,
                TextAlign = HorizontalAlignment.Right,
                Minimum = 1,
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                ReadOnly = true,
            };


            this.pnlIva.Controls.Add(this.numIva);
            this.pnlIva.Controls.Add(lblIva);

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

        private ComboBox cbTipoHabitacion;
        private ComboBox cbNumHabitacionList;
        private ComboBox cbDniClienteList;
        private DateTimePicker dtpDateIn;
        private DateTimePicker dtpDateOut;
        private ComboBox cbGaraje;
        private NumericUpDown numPrecioDia;
        private NumericUpDown numIva;

        private Panel pnlReserva;
        private Panel pnlEspacio;
        private Panel pnlTipoHabitacion;
        private Panel pnlDateIn;
        private Panel pnlDateOut;
        private Panel pnlGaraje;
        private Panel pnlPrecioDia;
        private Panel pnlIva;
        private Panel pnlNumHabitacion;
        private Panel pnlDniCliente;
        private Panel pnlInserta;

        public string TipoHabitacion => this.cbNumHabitacionList.Text;
        public string DniCliente => this.cbDniClienteList.Text;
        public string NumHabitacion => this.cbNumHabitacionList.Text;
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
        private RegistroReserva Reservas;

    }
}