using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        void Build()
        {
            this.SuspendLayout();

            var pnlInserta = new TableLayoutPanel {
                Size = new Size(442, 440), //Size(alto,ancho)
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(49, 66, 82),
            };

            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlReserva = this.BuildReservaPanel();
            pnlInserta.Controls.Add(pnlReserva);


            var pnlTipo = this.BuilTipo();
            pnlInserta.Controls.Add(pnlTipo);

            //Cliente

            //F_entrada

            //F_salida

            //Garaje

            //Precio/dia

            //Iva

            var pnlNumSerie = this.BuildNumSeriePanel();
            pnlInserta.Controls.Add(pnlNumSerie);
            
            var pnlBanda = this.BuildBandaPanel();
            pnlInserta.Controls.Add(pnlBanda);

            var pnlPrecioPiezas = this.BuildPrecioPiezasPanel();
            pnlInserta.Controls.Add(pnlPrecioPiezas);

            var pnlTiempoRep = this.BuildDuracionPanel();
            pnlInserta.Controls.Add(pnlTiempoRep);

            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Gestion de un hotel - Añadir reserva";
            this.Size = new Size(440,
                    pnlReserva.Height + pnlNumSerie.Height + pnlTipo.Height +
                    pnlBanda.Height + pnlPrecioPiezas.Height + pnlTiempoRep.Height + pnlBotones.Height + 15);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
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

            var pnlReserva = new Panel();
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

            pnlReserva.Dock = DockStyle.Fill;
            pnlReserva.MaximumSize = new Size(int.MaxValue, 30);

            return pnlReserva;
        }



        //Panel BuildHabitacion() { }

        Panel BuilTipo()
        {
            var pnlTipo = new Panel()
            {
                Dock = DockStyle.Top,
            };

            this.tbTipo = new TextBox() { Dock = DockStyle.Fill };

            var lblTipo = new Label()
            {
                Text = "Tipo:",
                Dock = DockStyle.Left,
                ForeColor = Color.White,


            };

            this.tbTipo.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Modelo);

                invalid = invalid || !char.IsLetter(this.Modelo[0]);

                if (invalid)
                {
                    this.tbTipo.Text = "Inserte un tipo";
                }

                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };

            pnlTipo.MaximumSize = new Size(int.MaxValue, tbTipo.Height * 2);

            pnlTipo.Controls.Add(this.tbTipo);
            pnlTipo.Controls.Add(lblTipo);

            return pnlTipo;
        }

        Panel BuildNumSeriePanel()
        {
            var pnlNumSerie = new Panel();
            this.nudNumSerie = new NumericUpDown
            {
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                Minimum = 1
            };

            var lbNumSerie = new Label()
            {
                Text = "Número de serie:",
                Dock = DockStyle.Left,
                Size = new Size(102, 15),
                Location = new Point(45, 111),
                AutoSize = true,
                TabIndex = 43,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)))
            };
            pnlNumSerie.Controls.Add(this.nudNumSerie);
            pnlNumSerie.Controls.Add(lbNumSerie);
            pnlNumSerie.Dock = DockStyle.Top;
            pnlNumSerie.MaximumSize = new Size(int.MaxValue, nudNumSerie.Height * 2);

            return pnlNumSerie;
        }

     

        Panel BuildBandaPanel()
        {
            this.pnlBanda = new Panel { Dock = DockStyle.Top };

            this.rbAM = new RadioButton()
            {
                Text = "AM",
                Dock = DockStyle.Top,
                Checked = true
            };

            this.rbFM = new RadioButton()
            {
                Text = "FM",
                Dock = DockStyle.Top

            };

            this.rbAM_FM = new RadioButton()
            {
                Text = "AM/FM",
                Dock = DockStyle.Top

            };

            var lbBanda = new Label
            {
                Text = "Banda: ",
                Dock = DockStyle.Left,
            };


            this.pnlBanda.Controls.Add(this.rbAM);
            this.pnlBanda.Controls.Add(this.rbFM);
            this.pnlBanda.Controls.Add(this.rbAM_FM);
            this.pnlBanda.Controls.Add(lbBanda);
            this.pnlBanda.MaximumSize = new Size(int.MaxValue, this.rbAM.Height * 3);

            return this.pnlBanda;
        }

        String rbSelected()
        {
            var botonElegido = this.pnlBanda.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);

            return botonElegido.Text;
        }


        Panel BuildPrecioPiezasPanel()
        {
            var pnlPrecioPiezas = new Panel();
            this.nudPrecioPiezas = new NumericUpDown
            {
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                Minimum = 1
            };

            var lbPrecioPiezas = new Label
            {
                Text = "Precio de las piezas: ",
                Dock = DockStyle.Left
            };

            pnlPrecioPiezas.Controls.Add(this.nudPrecioPiezas);
            pnlPrecioPiezas.Controls.Add(lbPrecioPiezas);
            pnlPrecioPiezas.Dock = DockStyle.Top;
            pnlPrecioPiezas.MaximumSize = new Size(int.MaxValue, nudPrecioPiezas.Height * 2);

            return pnlPrecioPiezas;
        }
        Panel BuildDuracionPanel()
        {
            var pnlDuracion = new Panel();
            this.nudDuracion = new NumericUpDown
            {
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                Minimum = 1
            };

            var lbDuracion = new Label
            {
                Text = "Tiempo de reparación: ",
                Dock = DockStyle.Left
            };

            pnlDuracion.Controls.Add(this.nudDuracion);
            pnlDuracion.Controls.Add(lbDuracion);
            pnlDuracion.Dock = DockStyle.Top;
            pnlDuracion.MaximumSize = new Size(int.MaxValue, nudDuracion.Height * 2);

            return pnlDuracion;
        }
        private NumericUpDown nudNumSerie;
        private TextBox tbTipo;
        private NumericUpDown nudDuracion;
        private NumericUpDown nudPrecioPiezas;
        //private ComboBox cbBanda;
        private RadioButton rbAM;
        private RadioButton rbFM;
        private RadioButton rbAM_FM;
        private Panel pnlBanda;
        private Panel pnlTipo;


        public int NumSerie => (int)this.nudNumSerie.Value;
        public string Modelo => this.tbTipo.Text;
        public double Duracion => System.Convert.ToDouble(this.nudDuracion.Value);
        public double PrecioPiezas => System.Convert.ToDouble(this.nudPrecioPiezas.Value);
        public string Banda => this.rbSelected();
    }
}