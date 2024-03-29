// Charts for WinForms (c) 2017 MIT License <baltasarq@gmail.com>

using System;
using System.Drawing.Design;
using System.Windows.Forms.DataVisualization.Charting;

namespace GestionReservas.GUI {
    using System.Linq;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;

    /// <summary>
    /// Draws a simple chart.
    /// Note that Mono's implementation of WinForms Chart is incomplete.
    /// </summary>
    public class Chart: PictureBox {
        public enum ChartType { Lines, Bars };

        public enum ChartValues
        {
            Months,
            Years,
            ClientesMes,
            ClientesAño,
            HabMes,
            HabAño,
            Comodidades
        };
        public FontFamily fontFamily = new FontFamily("Verdana");
        
        public Chart(int width, int height)
        {
            this.values = new List<int>();
            this.indices = new List<string>();
            this.Width = width;
            this.Height = height;
            this.FrameWidth = 50;

            this.Type = ChartType.Lines;

            this.AxisPen = new Pen( Color.DarkSlateBlue) { Width = 4 };
            this.DataPen = new Pen( Color.SlateBlue) { Width = 4 };
            this.DataFont = new Font( fontFamily, 10 );
            this.LegendFont = new Font( fontFamily, 12 );
            this.LegendPen = new Pen( Color.DarkSlateGray ) { Width = 1 };
            this.Build();
        }
        
        /// <summary>
        /// Redraws the chart
        /// </summary>
        public void Draw()
        {
            this.grf.DrawRectangle(
                            new Pen( this.BackColor ),
                            new Rectangle( 0, 0, this.Width, this.Height ) );
            this.DrawAxis();
            this.DrawData();
            this.DrawLegends();
            this.DrawButton();
        }

        private void DrawButton()
        {
            this.Button = new Button();
            Button.Location = new Point(this.FramedEndPosition.X - 50, FramedOrgPosition.Y-30);

            switch (this.TypeValues)
            {
                case ChartValues.Months:
                    Button.Text = "Por años";
                    break;
                case ChartValues.Years:
                    Button.Text = "Por meses";
                    break;
                case ChartValues.HabMes:
                case ChartValues.ClientesMes:
                    var año = DateTime.Today.Year;
                    Button.Text = "Año " + año;
                    break;
                case ChartValues.HabAño:
                case ChartValues.ClientesAño:
                    var mes = DateTime.Today.Month;
                    Button.Text = "Mes " + mes;
                    break;

            }

            if (this.TypeValues != ChartValues.Comodidades)
            {
                this.Controls.Add(Button);
            }
        }
        private void DrawLegends()
        {
            StringFormat verticalDrawFmt = new StringFormat {
                FormatFlags = StringFormatFlags.DirectionVertical
            };
            int legendXWidth = (int) this.grf.MeasureString(
                                                        this.LegendX,
                                                        this.LegendFont ).Width;
            int legendYHeight = (int) this.grf.MeasureString(
                                                        this.LegendY,
                                                        this.LegendFont,
                                                        new Size( this.Width,
                                                                  this.Height ),
                                                        verticalDrawFmt ).Height;

            this.grf.DrawString(
                    this.LegendX,
                    this.LegendFont,
                    this.LegendPen.Brush,
                    new Point( 
                        ( this.Width - legendXWidth ) / 2,
                        this.FramedEndPosition.Y + 20 ) );

            this.grf.DrawString(
                    this.LegendY,
                    this.LegendFont,
                    this.LegendPen.Brush,
                    new Point( 
                        this.FramedOrgPosition.X - ( this.FrameWidth / 2 ),
                        ( this.Height - legendYHeight ) / 2 ),
                    verticalDrawFmt );
        } 
        
        private void DrawData()
        {
            int numValues = this.values.Count;
            var p = this.DataOrgPosition;
            int xGap = this.GraphWidth / ( numValues + 1 );
            int baseLine = this.DataOrgPosition.Y;

            
            this.NormalizeData();
            for(int i = 0; i < numValues; ++i) {
                string tag = this.values[ i ].ToString();
                int tagWidth = (int) this.grf.MeasureString(
                                                        tag,
                                                        this.DataFont ).Width;
                int tagHeight = (int) this.grf.MeasureString(
                    tag,
                    this.DataFont ).Height;
                var nextPoint = new Point(
                    p.X + xGap, baseLine - this.normalizedData[ i ]
                );
                
                if ( this.Type == ChartType.Bars ) {
                    p = new Point( nextPoint.X, baseLine );
                }

               
                this.grf.DrawLine(this.DataPen, p, nextPoint);


                switch (this.TypeValues)
                {
                    case ChartValues.Months:
                        this.grf.DrawString((i + 1).ToString(), this.DataFont, this.AxisPen.Brush,
                            new Point(nextPoint.X - tagWidth, baseLine + 10));
                        break;
                    case ChartValues.Years:
                        this.grf.DrawString((2015 + i).ToString(), this.DataFont, this.AxisPen.Brush,
                            new Point(nextPoint.X - tagWidth, baseLine + 10));
                        break;
                    case ChartValues.HabMes:
                    case ChartValues.HabAño:
                    case ChartValues.ClientesAño:
                    case ChartValues.Comodidades:
                    case ChartValues.ClientesMes:
                        this.grf.DrawString(indices[i], this.DataFont, this.AxisPen.Brush,
                            new Point(nextPoint.X - tagWidth, baseLine + 10));
                        break;

                }
                    
                
                    this.grf.DrawString(tag,
                            this.DataFont,
                            this.DataPen.Brush,
                                new Point(nextPoint.X - tagWidth, nextPoint.Y - tagHeight));


                        p = nextPoint;
            }
        }
        
        private void DrawAxis()
        {
            // Y axis
            this.grf.DrawLine( this.AxisPen,
                               this.FramedOrgPosition,
                               new Point(
                                        this.FramedOrgPosition.X,
                                        this.FramedEndPosition.Y ) );
                                        
            // X axis
            this.grf.DrawLine( this.AxisPen,
                               new Point(
                                        this.FramedOrgPosition.X,
                                        this.FramedEndPosition.Y ),
                               this.FramedEndPosition );
        }

        private void Build()
        {
			Bitmap bmp = new Bitmap( this.Width, this.Height );
			this.Image = bmp;
            this.grf = Graphics.FromImage( bmp );
        }
        
        private void NormalizeData()
        {
            int maxHeight = this.DataOrgPosition.Y - this.FrameWidth;
            int maxValue = this.values.Max();

            this.normalizedData = this.values.ToArray();

            for(int i = 0; i < this.normalizedData.Length; ++i) {
                if (maxValue != 0)
                {
                    this.normalizedData[i] =
                        (this.values[i] * maxHeight) / maxValue;
                }
                else
                {
                    this.normalizedData[i] = this.values[i] * maxHeight;
                }
            }
            
            return;
        }
        
        /// <summary>
        /// Gets or sets the values used as data.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<int> Values {
            get {
                return this.values.ToArray();
            }
            set {
                this.values.Clear();
                this.values.AddRange( value );
            }
        }
        
        public IEnumerable<string> Indices {
            get {
                return this.indices.ToArray();
            }
            set {
                this.indices.Clear();
                this.indices.AddRange( value );
            }
        }

        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Point"/>.</value>
        public Point DataOrgPosition {
            get {
                int margin = (int) ( this.AxisPen.Width * 2 );
                
                return new Point(
                    this.FramedOrgPosition.X + margin,
                    this.FramedEndPosition.Y - margin );
            }
        }
        
        /// <summary>
        /// Gets or sets the width of the frame around the chart.
        /// </summary>
        /// <value>The width of the frame.</value>
        public int FrameWidth {
            get; set;
        }
        
        /// <summary>
        /// Gets the framed origin.
        /// </summary>
        /// <value>The origin <see cref="Point"/>.</value>
        public Point FramedOrgPosition {
            get {
                return new Point( this.FrameWidth, this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the framed end.
        /// </summary>
        /// <value>The end <see cref="Point"/>.</value>
        public Point FramedEndPosition {
            get {
                return new Point( this.Width - this.FrameWidth,
                                  this.Height - this.FrameWidth );
            }
        }
        
        /// <summary>
        /// Gets the width of the graph.
        /// </summary>
        /// <value>The width of the graph.</value>
        public int GraphWidth {
            get {
                return this.Width - ( this.FrameWidth * 2 );
            }
        }
        
        /// <summary>
        /// Gets or sets the pen used to draw the axis.
        /// </summary>
        /// <value>The axis <see cref="Pen"/>.</value>
        public Pen AxisPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the pen used to draw the data.
        /// </summary>
        /// <value>The data <see cref="Pen"/>.</value>
        public Pen DataPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the data font.
        /// </summary>
        /// <value>The data <see cref="Font"/>.</value>
        public Font DataFont {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the legend for the x axis.
        /// </summary>
        /// <value>The legend for axis x.</value>
        public string LegendX {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the legend for the y axis.
        /// </summary>
        /// <value>The legend for axis y.</value>
        public string LegendY {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the font for legends.
        /// </summary>
        /// <value>The <see cref="Font"/> for legends.</value>
        public Font LegendFont {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the pen for legends.
        /// </summary>
        /// <value>The <see cref="Pen"/> for legends.</value>
        public Pen LegendPen {
            get; set;
        }
        
        /// <summary>
        /// Gets or sets the type of the chart.
        /// </summary>
        /// <value>The <see cref="ChartType"/>.</value>
        public ChartType Type {
            get; set;
        }

        public Button Button { get; set; }

        public ChartValues TypeValues { get; set; }



        private Graphics grf;
        private List<int> values;
        private List<string> indices;
        private int[] normalizedData;
    }
}
