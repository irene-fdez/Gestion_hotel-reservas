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
     
    public class DIgModificaHabitacion : Form
    {
        public DIgModificaHabitacion(Habitacion habitacion)
        {
            
            this.Habitaciones = new RegistroHabitaciones();
            this.habitacion = habitacion;
            

            this.Build();
            this.CenterToScreen();

            var DCR = new DIgConsultaHabitacion(this.Habitaciones);
            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; DCR.Salir(); };
            this.opVolver.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;


        }
        
        void Build()
        {
           /* this.BuildStatus();*/
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

            var pnlHabitacion = this.BuildHabitacionPanel();
            pnlInserta.Controls.Add(pnlHabitacion);

            var pnlEspacio = this.BuildEspacioPanel();
            pnlInserta.Controls.Add(pnlEspacio);


            //tipo Habitacion
            var pnltipoHabitacion = this.BuildTipoHabitacion();
            pnlInserta.Controls.Add(pnltipoHabitacion);

          
            
            //fecha renova
            var dtpDateRenova = this.BuildFechaRenova();
            pnlInserta.Controls.Add(pnlDateRenova);
            
            //fecha ultima renovacion
            var dtpDateLastReserva = this.BuildDateLastReserva();
            pnlInserta.Controls.Add(pnlDateLastReserva);

            //panel combo box
            var pnlBuildCheckBox = this.BuildCheckBox();
            pnlInserta.Controls.Add(pnlBuildCheckBox);

            //panel Wifi
            var pnlBuildWifi = this.BuildPanelWifi();
            pnlInserta.Controls.Add(pnlBuildWifi);
            
            //panel caja fuerte
            var pnlBuildCajaFuerte = this.BuildPanelCajaFuerte();
            pnlInserta.Controls.Add(pnlBuildCajaFuerte);

            //panel mini bar
            var pnlBuildMiniBar = this.BuildPanelMiniBar();
            pnlInserta.Controls.Add(pnlBuildMiniBar);
            
            //panel baño
            var pnlBuildBaño = this.BuildPanelBaño();
            pnlInserta.Controls.Add(pnlBuildBaño);
            
            //panel Cocina
            var pnlBuildCocina = this.BuildPanelCocina();
            pnlInserta.Controls.Add(pnlBuildCocina);
            
            //panel tv
            var pnlBuildTv = this.BuildPanelTv();
            pnlInserta.Controls.Add(pnlBuildTv);
            
            //Id habitacion
            var pnlnum = this.BuildID();
            pnlInserta.Controls.Add(pnlnum);
            //Tipo reserva
         /*   var pnlTipo = this.BuilTipo();
            pnlInserta.Controls.Add(pnlTipo);*/

            

            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Modificar Habitacion";

            Console.WriteLine(pnlHabitacion.Height);

            this.Size = new Size(600,
                      100 + pnlHabitacion.Height + pnlEspacio.Height + pnlnum.Height  
                      + pnltipoHabitacion.Height +
                      dtpDateRenova.Height + dtpDateLastReserva.Height 
                      + pnlBuildCheckBox.Height + pnlWifi.Height +pnlBuildCajaFuerte.Height +
                      pnlBuildMiniBar.Height+
                      pnlBuildBaño.Height + 
                      pnlBuildCocina.Height +
                      pnlBuildCocina.Height
                      + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                      100 + pnlHabitacion.Height + pnlEspacio.Height + pnlnum.Height  
                      + pnltipoHabitacion.Height +
                      dtpDateRenova.Height + dtpDateLastReserva.Height 
                      + pnlBuildCheckBox.Height + pnlWifi.Height +pnlBuildCajaFuerte.Height +
                      pnlBuildMiniBar.Height+
                      pnlBuildBaño.Height +
                      pnlBuildCocina.Height +
                      pnlBuildCocina.Height
                      + pnlBotones.Height);

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
        
        Panel BuildHabitacionPanel()
        {

            this.pnlHabitacion = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,
            };

            var lblHabitacion = new Label()
            {
                Text = "Datos de la Habitacion",
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

            pnlHabitacion.Controls.Add(lblEspacio);
            pnlHabitacion.Controls.Add(lblHabitacion);

            return pnlHabitacion;
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
                Text = this.habitacion.Numero,
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
           /* cbTipoHabitacion.SelectedValueChanged += (sender, e) =>
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

               
            };*/
            

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
  Panel BuildFechaRenova()
        {
            this.pnlDateRenova = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlHabitacion.Top + this.pnlHabitacion.Height + 10),
            };
        //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

            var lblDateIn = new Label()
            {
                Location = new Point(Left, (this.pnlHabitacion.Top + this.pnlHabitacion.Height + 10)),
                Text = "Fecha de Renovacion:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.dtpDateRenova = new DateTimePicker()
            {
                Left = 0,
                Width = 250,
                MinDate = DateTime.Today,
                Value = DateTime.Today,
                Anchor = AnchorStyles.Bottom,
                Enabled = false
            };




            pnlDateRenova.MaximumSize = new Size(int.MaxValue, dtpDateRenova.Height * 2);

            pnlDateRenova.Controls.Add(this.dtpDateRenova);
            pnlDateRenova.Controls.Add(lblDateIn);

            return pnlDateRenova;
        }
        
        
        
         Panel BuildDateLastReserva()
        {
            this.pnlDateLastReserva = new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlDateRenova.Top + this.pnlDateRenova.Height + 10),
            };
        //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

            var lblDateLastReserva = new Label()
            {
                Location = new Point(Left, (this.pnlDateRenova.Top + this.pnlDateRenova.Height + 10)),
                Text = "Fecha de Ultima renovacion:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };

            this.dtpDateLastReserva = new DateTimePicker()
            {
                Left = 0,
                Width = 250,
                MinDate = DateTime.Today,
                Value = DateTime.Today,
                Anchor = AnchorStyles.Bottom,
                Enabled = false
            };
            
            pnlDateLastReserva.MaximumSize = new Size(int.MaxValue, dtpDateLastReserva.Height * 2);

            pnlDateLastReserva.Controls.Add(this.dtpDateLastReserva);
            pnlDateLastReserva.Controls.Add(lblDateLastReserva);

            return pnlDateLastReserva;
        }
         Panel BuildCheckBox()
         {
             this.pnlBuildCheckBox = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlDateLastReserva.Top + this.pnlDateLastReserva.Height + 10),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblCondiciones = new Label()
             {
                 Location = new Point(Left, (this.pnlDateLastReserva.Top + this.pnlDateLastReserva.Height + 10)),
                 Text = "Condiciones:",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             
             

             pnlBuildCheckBox.MaximumSize = new Size(int.MaxValue, lblCondiciones.Height * 2);

             pnlBuildCheckBox.Controls.Add(lblCondiciones);
             

             return pnlBuildCheckBox;
         }

        
         Panel BuildPanelWifi()
         {
             this.pnlWifi = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlBuildCheckBox.Top + this.pnlBuildCheckBox.Height + 5),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblWifi = new Label()
             {
                 Location = new Point(Left, (this.pnlBuildCheckBox.Top + this.pnlBuildCheckBox.Height + 5)),
                 Text = "Wifi : ",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbWifi = new CheckBox()
             {
                 Left = 180
             };


              pnlWifi.MaximumSize = new Size(int.MaxValue, cbWifi.Height * 2);

             pnlWifi.Controls.Add(this.cbWifi);
             pnlWifi.Controls.Add(lblWifi);
             

             return pnlWifi;
         }

         Panel BuildPanelCajaFuerte()
         {
             this.pnlCajaFuerte = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlWifi.Top + this.pnlWifi.Height + 5),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblCajaFuerte = new Label()
             {
                 Location = new Point(Left, (this.pnlWifi.Top + this.pnlWifi.Height + 5)),
                 Text = "Caja Fuerte:",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbCajaFuerte = new CheckBox()
             {
                 Left = 180,
                /* Width = 250,*/
                 /*Anchor = AnchorStyles.Bottom*/
             };
             

             pnlCajaFuerte.MaximumSize = new Size(int.MaxValue, cbCajaFuerte.Height * 2);

             pnlCajaFuerte.Controls.Add(this.cbCajaFuerte);
             pnlCajaFuerte.Controls.Add(lblCajaFuerte);
             

             return pnlCajaFuerte;
         }
         
         Panel BuildPanelMiniBar()
         {
             this.pnlMiniBar = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlCajaFuerte.Top + this.pnlCajaFuerte.Height + 10),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblMiniBar = new Label()
             {
                 Location = new Point(Left, (this.pnlCajaFuerte.Top + this.pnlCajaFuerte.Height + 10)),
                 Text = "Mini-Bar:",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbMiniBar = new CheckBox()
             {
                 Left = 180,
                /* Width = 250,
                 Anchor = AnchorStyles.Bottom*/
             };
             

             pnlMiniBar.MaximumSize = new Size(int.MaxValue, cbMiniBar.Height * 2);

             pnlMiniBar.Controls.Add(this.cbMiniBar);
             pnlMiniBar.Controls.Add(lblMiniBar);
             

             return pnlMiniBar;
         }


         Panel BuildPanelBaño()
         {
             this.pnlBaño = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlBuildCheckBox.Top + this.pnlBuildCheckBox.Height + 10),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblBaño = new Label()
             {
                 Location = new Point(Left, (this.pnlBuildCheckBox.Top + this.pnlBuildCheckBox.Height + 10)),
                 Text = "Baño:",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbBaño = new CheckBox()
             {
                 Left = 180,
                 /* Width = 250,
                  Anchor = AnchorStyles.Bottom*/
             };
             

             pnlBaño.MaximumSize = new Size(int.MaxValue, cbBaño.Height * 2);

             pnlBaño.Controls.Add(this.cbBaño);
             pnlBaño.Controls.Add(lblBaño);
             

             return pnlBaño;
         }
         
         Panel BuildPanelCocina()
         {
             this.pnlCocina = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlBaño.Top + this.pnlBaño.Height + 10),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblCocina = new Label()
             {
                 Location = new Point(Left, (this.pnlBaño.Top + this.pnlBaño.Height + 10)),
                 Text = "Cocina :",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbCocina = new CheckBox()
             {
                 Left = 180,
                 /* Width = 250,
                  Anchor = AnchorStyles.Bottom*/
             };
             

             pnlCocina.MaximumSize = new Size(int.MaxValue, cbBaño.Height * 2);

             pnlCocina.Controls.Add(this.cbCocina);
             pnlCocina.Controls.Add(lblCocina);
             

             return pnlCocina;
         }

         
         Panel BuildPanelTv()
         {
             this.pnlTv = new Panel()
             {
                 Dock = DockStyle.Fill,
                 Location = new Point(Left, this.pnlCocina.Top + this.pnlCocina.Height + 10),
             };
             //    Console.WriteLine("comienzo dateIn: " + (this.pnlTipo.Top + this.pnlTipo.Height + 10));

             var lblTv = new Label()
             {
                 Location = new Point(Left, (this.pnlCocina.Top + this.pnlCocina.Height + 10)),
                 Text = "Tv :",
                 Dock = DockStyle.Left,
                 ForeColor = Color.White,
                 Width = 150,
                 TextAlign = ContentAlignment.TopRight,
             };

             this.cbTv = new CheckBox()
             {
                 Left = 180,
                 /* Width = 250,
                  Anchor = AnchorStyles.Bottom*/
             };
             

             pnlTv.MaximumSize = new Size(int.MaxValue, cbTv.Height * 2);

             pnlTv.Controls.Add(this.cbTv);
             pnlTv.Controls.Add(lblTv);
             

             return pnlTv;
         }

        private ComboBox cbTipoHabitacion;
        private ComboBox cbNumHabitacionList;
        private NumericUpDown numeroHabitacion;
        private TextBox tbId;

        private DateTimePicker dtpDateRenova;
        private DateTimePicker dtpDateLastReserva;
        private CheckBox cbWifi;
        private CheckBox cbCajaFuerte;
        private CheckBox cbMiniBar;
        private CheckBox cbBaño;
        private CheckBox cbCocina;
        private CheckBox cbTv;


        private Panel pnlHabitacion;
        private Panel pnlEspacio;
        private Panel pnlTipoHabitacion;
        private Panel pnlNumHabitacion;
        private Panel pnlInserta;
        private Panel pnlId;
        private Panel pnlDateRenova;
        private Panel pnlDateLastReserva;
        private Panel pnlBuildCheckBox;
        private Panel pnlWifi;
        private Panel pnlCajaFuerte;
        private Panel pnlMiniBar;
        private Panel pnlBaño;
        private Panel pnlCocina;
        private Panel pnlTv;

        public string TipoHabitacion => this.cbTipoHabitacion.Text;
        public string Id => this.tbId.Text;
        public string NumHabitacion => this.numeroHabitacion.Text;
        public DateTime FechaRenova => this.dtpDateRenova.Value;
        public DateTime UltimaReserva => this.dtpDateLastReserva.Value;
        public bool Wifi => this.cbWifi.Checked;
        public bool CajaFuerte => this.cbCajaFuerte.Checked;
        public bool MiniBar => this.cbMiniBar.Checked;
        public bool Baño => this.cbBaño.Checked;
        public bool Cocina => this.cbCocina.Checked;
        public bool Tv => this.cbTv.Checked;
      

        public StatusBar SbStatus;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opVolver;
        public MenuItem opSalir;
        public MenuItem mBuscar;
        public List<Habitacion> rgHab = null;
        private Habitacion habitacion;
        private RegistroHabitaciones Habitaciones;
    }

}