﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionReservas.GUI.Dlg;

namespace GestionReservas.GUI
{

    using System.Windows.Forms;
    using System.Drawing;

    public class MainWindowView : Form
    {
        public MainWindowView()
        {
            this.BuildGUI();
            this.CenterToScreen();
        }

        private void BuildGUI()
        {
            this.BuildStatus();
            this.BuildMenu();

            this.SuspendLayout();
            this.pnlPanel = new Panel()
            {
                Size = new Size(442, 440), //Size(alto,ancho)
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(49, 66, 82),
           };


            this.pnlPanel.SuspendLayout();
            this.Controls.Add(pnlPanel);

            var pnlReserva = this.BuildReserva();
            this.pnlPanel.Controls.Add(pnlReserva);

            var BtnAddReserva = this.BuildBtnAdd();
            this.pnlPanel.Controls.Add(BtnAddReserva);

            var BtnConsultaReserva = this.BuildBtnConsulta();
            this.pnlPanel.Controls.Add(BtnConsultaReserva);


            this.pnlPanel.ResumeLayout(false);

            this.MinimumSize = new Size(600, 400);
            this.MaximumSize = new Size(600, 400);
            this.MaximizeBox = false;
          //  this.Resize += (obj, e) => this.ResizeWindow();
            this.Text = "Gestión de un hotel";

            this.ResumeLayout(false);
          //  this.ResizeWindow();
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

        Panel BuildReserva()
        {
            var pnlReserva = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,

            };

            var lblReserva = new Label()
            {
                Text = "Gestion de reservas",
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
            pnlReserva.MaximumSize = new Size(int.MaxValue, 50);

            return pnlReserva;
        }




        Panel BuildBtnAdd()
        {
            this.pnlBotonAdd = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 1,
              //  Dock = DockStyle.Fill,
                Location = new Point(Left, this.pnlPanel.Top+70),
                Width = this.pnlPanel.Width,
            };

            this.btnAddReserva = new Button()
            {
                Text = "&Añadir reserva",
                DialogResult = DialogResult.Yes,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(69, 93, 117),
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Silver,
            };
          
            this.pnlBotonAdd.Left = (this.pnlPanel.Width / 2) - (this.pnlBotonAdd.Width / 3);

            this.pnlBotonAdd.Controls.Add(btnAddReserva);

            return this.pnlBotonAdd;

        }

        Panel BuildBtnConsulta()
        {
            this.pnlBotonConsult = new TableLayoutPanel()
            {
                ColumnCount = 1,
                RowCount = 1,
             //   Dock = DockStyle.Top,
                Location = new Point(Left, this.pnlBotonAdd.Top+ this.pnlBotonAdd.Height+20),
                Width = this.pnlPanel.Width,


            };


            this.btnConsultaReserva = new Button()
            {
                Text = "&Consultar reservas",
                DialogResult = DialogResult.Yes,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(69, 93, 117),
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.Silver,
            };
           


            this.pnlBotonConsult.Left = (this.pnlPanel.Width / 2) - (this.pnlBotonConsult.Width / 3);

            this.pnlBotonConsult.Controls.Add(btnConsultaReserva);


            return this.pnlBotonConsult;

        }




        public Button btnAddReserva;
        public Button btnConsultaReserva;

        private Panel pnlBotonAdd;
        private Panel pnlBotonConsult;

        private MainMenu mPpal;
        public MenuItem mArchivo;
      //  public MenuItem mInsertar;
        public MenuItem opSalir;
      /*  public MenuItem OpInsertarAdaptadorTDT;
        public MenuItem OpInsertarRadio;
        public MenuItem OpInsertarTelevisor;
        public MenuItem OpInsertarReproductorDVD;*/

        public StatusBar SbStatus;
        private Panel pnlPanel;

        public DataGridView GrdLista;


    }

}