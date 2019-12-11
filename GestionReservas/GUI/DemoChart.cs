using System;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionReservas.GUI {
    using System.Drawing;
    using System.Windows.Forms;
    
    public class DemoChart: Form {
        public const int ChartCanvasSize = 512;

        /// <summary>
        /// Initializes a new <see cref="T:WinForms.DemoChart"/>.
        /// </summary>
        public DemoChart(List<int> valores, String tipo)
        {
            this.Build();

            if (tipo == "Months")
            {
                this.Chart.LegendX = "Meses";
                this.Chart.TypeValues = Chart.ChartValues.Months;

            }
            else
            {
                this.Chart.LegendX = "Años";
                this.Chart.TypeValues = Chart.ChartValues.Years;
            }

            this.Chart.LegendY = "Num. Reservas";

            this.Chart.Values = valores;

            this.Chart.Draw();
        }
        
        public DemoChart(List<int> valores, String tipo, List<string> indices)
        {
            this.Build();

            switch (tipo)
            {
                case "ClientesMes":
                    this.Chart.LegendX = "Clientes";
                    this.Chart.TypeValues = Chart.ChartValues.ClientesMes;
                    this.Chart.LegendY = "Num. Reservas mes";
                    break;
                case "ClientesAño":
                    this.Chart.LegendX = "Clientes";
                    this.Chart.TypeValues = Chart.ChartValues.ClientesAño;
                    this.Chart.LegendY = "Num. Reservas año";
                    break;
                case "HabMes":
                    this.Chart.LegendX = "Habitaciones";
                    this.Chart.TypeValues = Chart.ChartValues.HabMes;
                    this.Chart.LegendY = "Num. Reservas mes";
                    break;
                case "HabAño":
                    this.Chart.LegendX = "Habitaciones";
                    this.Chart.TypeValues = Chart.ChartValues.HabAño;
                    this.Chart.LegendY = "Num. Reservas año";
                    break;
                case "Comodidades":
                    this.Chart.LegendX = "Comodidades";
                    this.Chart.TypeValues = Chart.ChartValues.Comodidades;
                    this.Chart.LegendY = "Num. Habitaciones";
                    break;
                    
            }

            this.Chart.Values = valores;

            this.Chart.Indices = indices;

            this.Chart.Draw();
        }

        private void Build()
        {
            this.Chart = new Chart( width: ChartCanvasSize,
                height: ChartCanvasSize)
            {
                Dock = DockStyle.Fill,
            };

            this.Controls.Add( this.Chart );
            this.MinimumSize = new Size( ChartCanvasSize, ChartCanvasSize );
            this.Text = this.GetType().Name;
        }
        
        /// <summary>
        /// Gets the <see cref="Chart"/>.
        /// </summary>
        /// <value>The chart.</value>
        public Chart Chart {
            get; private set;
        }

    }
}