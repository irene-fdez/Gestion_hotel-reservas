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
    class DlgInsertaCliente : Form
    {
        public DlgInsertaCliente(RegistroClientes cli)
        {
            this.Clientes = cli;
            this.Build();
            this.CenterToScreen();
        }

        private void Build()
        {
            //--------------------------------
            /*
            this.BuildStatus();
            this.BuildMenu();
             */
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

        }

        Panel BuildClientePanel()
        {
            this.pnlCliente = new Panel()
            {
                Dock = DockStyle.Fill,
                MaximumSize = new Size(int.MaxValue, 30),
                Height = 30,
            };

            var lblReserva = new Label()
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
            pnlCliente.Controls.Add(lblReserva);

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

        private RegistroClientes Clientes;

        private TableLayoutPanel pnlInserta;
        private Panel pnlCliente;
        private Panel pnlEspacio;
    }
}
