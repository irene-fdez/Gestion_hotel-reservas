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

    public class DlgBuscarPorpersona : Form
    {

        public DlgBuscarPorpersona()
        {

            this.Clientes = RegistroClientes.RecuperarXml();
            this.Build();
            this.CenterToScreen();
            
            this.opVolver.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;


        }

        void Build()
        {

            this.BuildMenu();

            this.SuspendLayout();

            this.pnlInserta = new TableLayoutPanel
            {
                Size = new Size(442, 100), //Size(ancho, alto)
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(49, 66, 82),
            };

            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlHabitaciones = this.BuildClientesPanel();
            pnlInserta.Controls.Add(pnlHabitaciones);
            
            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Buscar por persona";

            Console.WriteLine(pnlHabitaciones.Height);

            this.Size = new Size(600, pnlHabitaciones.Height  + pnlBotones.Height);

            this.MinimumSize = new Size(600,
                pnlHabitaciones.Height +  pnlBotones.Height);

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }




        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem("&Archivo");
            this.opVolver = new MenuItem("&Volver");


            this.mArchivo.MenuItems.Add(this.opVolver);


            this.mPpal.MenuItems.Add(this.mArchivo);


            this.Menu = mPpal;


        }



        Panel BuildBotonesPanel()
        {

            var pnlBotones = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1,
                Dock = DockStyle.Right,

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
                Text = "&Ok",
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

        Panel BuildClientesPanel()
        {

            this.pnlClientes = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,

            };

            var lblClientes = new Label()
            {
                Text = "Cliente",
                Dock = DockStyle.Left,
                ForeColor = Color.White,
                Width = 150,
                TextAlign = ContentAlignment.TopRight,
            };
            this.cbcLIENTES = new ComboBox
            {
                Left = 0,
                Width = 250,
                Anchor = AnchorStyles.Bottom,
                DropDownStyle = ComboBoxStyle.DropDownList,
                DropDownWidth = 20,
            };
            string[] op = new string[Clientes.List != null ? Clientes.Count : 0];
            for (int i = 0; i < op.Length; i++)
            {
                Cliente cliente = this.Clientes[i];
                op[i] = cliente.DNI ;
            }
            cbcLIENTES.Items.AddRange(op);


            this.cbcLIENTES.Validating += (sender, cancelArgs) =>
            {
                bool invalid = false;
                var btAccept = (Button)this.AcceptButton;

                invalid = invalid || (cbcLIENTES.Text == "");

                if (invalid || cbcLIENTES.Text == "")
                {
                    string mensaje = "Debe seleccionar algún elemento";
                    MessageBox.Show(mensaje, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cbcLIENTES.Focus();
                }

                btAccept.Enabled = !invalid;
            };

            pnlClientes.Controls.Add(cbcLIENTES);
            pnlClientes.Controls.Add(lblClientes);


            return pnlClientes;
        }


        




        private Panel pnlClientes;
        private ComboBox cbcLIENTES;
        public string Cliente => this.cbcLIENTES.Text;
        private Panel pnlInserta;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opVolver;


        public RegistroClientes Clientes;

    }
}