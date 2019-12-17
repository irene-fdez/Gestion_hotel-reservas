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

    public class DlgConsultaReserva : Form
    {

        public DlgConsultaReserva(RegistroReserva res, List<Cliente> cli)
        {
            this.MVC = new MainWindowCore();
            this.Reservas = res;
            this.ReservasBuscar = this.Reservas;
            this.Clientes = cli;
            this.BuildGUI();
            this.CenterToScreen();
            this.RegClientes = RegistroClientes.RecuperarXml();
            this.Habitaciones = RegistroHabitaciones.RecuperarXml();



            this.GrdLista.Click += (sender, e) => ClickLista();

            this.opBuscarAll.Click += (sender, e) => this.BuscarTodos();
            this.opBuscarPendientes.Click += (sender, e) => this.BuscarFiltrado();
            this.opBuscarPasadas.Click += (sender, e) => this.BusquedaPasadasHotel();
            this.opBuscarPorPersona.Click += (sender, e) => this.BuscarPorP();
            this.opGuardar.Click += (sender, e) => this.Guardar();
            this.opSalir.Click += (sender, e) => { this.DialogResult = DialogResult.Cancel; this.Salir(); };
            this.opVolver.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;

            this.opInsertarH.Click += (sender, e) => this.InsertaHabitacion();
            this.opConsultaH.Click += (sender, e) => this.ConsultaHabitaciones();
            this.opBuscarPorHabitacion.Click += (sender, e) => this.BusquedaPorHabitacion();

            this.opConsultaC.Click += (sender, e) => this.ConsultaCliente();


        }

        private void BuildGUI()
        {
            this.BuildStatus();
            this.BuildMenu();

            // this.BuildPanelLista();

            this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(49, 66, 82),

            };

            this.pnlPpal.SuspendLayout();
            this.Controls.Add(this.pnlPpal);
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
            this.pnlPpal.Controls.Add(this.BuildPanelDetalle());
            this.pnlPpal.ResumeLayout(false);

            this.MinimumSize = new Size(1000, 400);
            this.Resize += (obj, e) => this.ResizeWindow();
            this.Text = "Gestion de un hotel - Consulta reservas";

            this.Actualiza();
            this.ResumeLayout(true);
            this.ResizeWindow();
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();
            this.mArchivo = new MenuItem("&Archivo");
            this.opGuardar = new MenuItem("&Guardar");
            this.opSalir = new MenuItem("&Salir");
            this.opVolver = new MenuItem("&Volver");
            this.opSalir.Shortcut = Shortcut.CtrlQ;
            this.mBuscar = new MenuItem("&Buscar");
            
            this.opBuscarPorPersona = new MenuItem("&Buscar reserva por persona");
            this.opBuscarAll = new MenuItem("&Buscar todos");
            this.opBuscarPendientes = new MenuItem("&Buscar pendientes");
            this.opBuscarPasadas = new MenuItem("&Buscar pasadas");
            this.opBuscarPorHabitacion = new MenuItem("&Buscar por habitacion");

            this.mCliente = new MenuItem("&Cliente");
            //this.opInsertarC = new MenuItem("&Insertar");
            this.opConsultaC = new MenuItem("&Consulta");


            this.mHabitaciones = new MenuItem("&Habitacion");
            this.opInsertarH = new MenuItem("&Insertar");
            this.opConsultaH = new MenuItem("&Consulta");

            this.mArchivo.MenuItems.Add(this.opGuardar);
            this.mArchivo.MenuItems.Add(this.opSalir);

            //this.mCliente.MenuItems.Add(this.opInsertarC);
            this.mCliente.MenuItems.Add(this.opConsultaC);

            this.mHabitaciones.MenuItems.Add(this.opInsertarH);
            this.mHabitaciones.MenuItems.Add(this.opConsultaH);

     

            this.mBuscar.MenuItems.Add(this.opBuscarAll);
            this.mBuscar.MenuItems.Add(this.opBuscarPasadas);
            this.mBuscar.MenuItems.Add(this.opBuscarPendientes);
            this.mBuscar.MenuItems.Add(this.opBuscarPorPersona);
            this.mBuscar.MenuItems.Add(this.opBuscarPorHabitacion);
            this.mArchivo.MenuItems.Add(this.opVolver);
            this.mArchivo.MenuItems.Add(this.opGuardar);
            this.mArchivo.MenuItems.Add(this.opSalir);

            this.mPpal.MenuItems.Add(this.mArchivo);
            this.mPpal.MenuItems.Add(this.mCliente);
            this.mPpal.MenuItems.Add(this.mHabitaciones);
            this.mPpal.MenuItems.Add(this.mBuscar);


            this.Menu = mPpal;
        }




        private Panel BuildPanelDetalle()
        {
            var pnlDetalle = new Panel {
                Dock = DockStyle.Bottom,
                Height = 110

            };
            pnlDetalle.SuspendLayout();

            this.edDetalle = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Font(FontFamily.GenericMonospace, 10),
                ForeColor = Color.Black,
                //BackColor = Color.LightGray,
                BackColor = Color.FromArgb(55, 95, 132),
            };

            
            pnlDetalle.Controls.Add(this.edDetalle);
            pnlDetalle.ResumeLayout(false);
            return pnlDetalle;
        }

        private void BuildStatus()
        {
            this.SbStatus = new StatusBar();
            this.SbStatus.Dock = DockStyle.Bottom;
            this.Controls.Add(this.SbStatus);
        }

        private Panel BuildPanelLista()
        {
            var pnlLista = new Panel
            {
                Dock = DockStyle.Fill
            };
            
            pnlLista.SuspendLayout();
            pnlLista.Dock = DockStyle.Fill;

            // Crear gridview
            this.GrdLista = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackColor = Color.FromArgb(113, 162, 208),
            };

            this.GrdLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.GrdLista.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(49, 66, 82);
            this.GrdLista.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.GrdLista.ColumnHeadersHeight = 30;
            this.GrdLista.ColumnHeadersDefaultCellStyle.Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Regular);

            //texto
            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            var textCellTemplate5 = new DataGridViewTextBoxCell();
            var textCellTemplate6 = new DataGridViewTextBoxCell();
            var textCellTemplate7 = new DataGridViewTextBoxCell();

            //botones
            var buttonCellTemplate8 = new DataGridViewButtonCell();
            var buttonCellTemplate9 = new DataGridViewButtonCell();
            var buttonCellTemplate10 = new DataGridViewButtonCell();

            //texto
            Color colorCeldasDatos = Color.PapayaWhip;

            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;

            textCellTemplate1.Style.BackColor = Color.DarkSalmon; 
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            textCellTemplate2.Style.BackColor = colorCeldasDatos;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate2.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            textCellTemplate3.Style.BackColor = colorCeldasDatos;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate4.Style.BackColor = colorCeldasDatos;
            textCellTemplate4.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate5.Style.BackColor = colorCeldasDatos;
            textCellTemplate5.Style.ForeColor = Color.Black;
            textCellTemplate5.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate6.Style.BackColor = colorCeldasDatos;
            textCellTemplate6.Style.ForeColor = Color.Black;
            textCellTemplate6.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            textCellTemplate7.Style.BackColor = colorCeldasDatos;
            textCellTemplate7.Style.ForeColor = Color.Black;
            textCellTemplate7.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //botones
            buttonCellTemplate8.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate8.Style.ForeColor = Color.Black;
            buttonCellTemplate8.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate8.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            buttonCellTemplate9.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate9.Style.ForeColor = Color.Black;
            buttonCellTemplate9.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate9.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            buttonCellTemplate10.Style.BackColor = colorCeldasDatos;
            buttonCellTemplate10.Style.ForeColor = Color.Black;
            buttonCellTemplate10.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            buttonCellTemplate10.Style.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

            var column0 = new DataGridViewTextBoxColumn //numero de reparacion
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                Width = 5,
                ReadOnly = true
            };

            var column1 = new DataGridViewTextBoxColumn // id
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Id",
                Width = 20,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn // tipo
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Tipo",
                Width = 15,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn  // fecha entrada
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha Entrada",
                Width = 15,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn // fecha salida
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha salida",
                Width = 15,
                ReadOnly = true
            };

            var column5 = new DataGridViewTextBoxColumn // garaje
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Garaje",
                Width = 15,
                ReadOnly = true
            };

            var column6 = new DataGridViewTextBoxColumn // total
            { 
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate6,
                HeaderText = "Total",
                Width = 15,
                ReadOnly = true
            };

            var column7 = new DataGridViewTextBoxColumn  //dni cliente
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate7,
                HeaderText = "Cliente",
                Width = 15,
                ReadOnly = true
            };
            var column8 = new DataGridViewButtonColumn  //modificar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate8,
                HeaderText = "Modificar",
                Width = 15,
                ReadOnly = true,
            };

            var column9 = new DataGridViewButtonColumn  //borrar
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate8,
                HeaderText = "Eliminar",
                Width = 15,
                ReadOnly = true,
            };


            var column10 = new DataGridViewButtonColumn  //factura
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = buttonCellTemplate10,
                HeaderText = "Factura",
                Width = 15,
                ReadOnly = true
            };

            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4, column5, column6, column7, column8, column9, column10
            });

            this.GrdLista.SelectionChanged += (sender, e) => this.FilaSeleccionada();

            pnlLista.Controls.Add(this.GrdLista);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }

        private void FilaSeleccionada()
        {

            try
            {
                var grdListaCurrentRow = this.GrdLista.CurrentRow;
                if (grdListaCurrentRow != null)
                {
                    int fila = System.Math.Max(0, grdListaCurrentRow.Index);
                    int posicion = this.GrdLista.CurrentCellAddress.X;

                    if (posicion == 7 && this.ReservasBuscar.Count > fila)
                    {
                        this.edDetalle.Text = this.ReservasBuscar[fila].Cliente.ToString();
                        this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                        this.edDetalle.SelectionLength = 0;
                    }
                    else if(posicion < 7){
                        this.edDetalle.Text = this.ReservasBuscar[fila].DatosEconomicosReserva();
                        this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                        this.edDetalle.SelectionLength = 0;
                    }
                    else 
                    {
                        this.edDetalle.Clear();
                    }
                }
            }
            catch(Exception)
            {
                this.edDetalle.Clear();
            }
            

            return;
        }


        private void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.GrdLista.Width = width;
            this.GrdLista.Height = 15;

          

            this.GrdLista.Columns[0].Width =
                                (int)System.Math.Floor(width * .03); // Num reserva
            this.GrdLista.Columns[1].Width =
                                (int)System.Math.Floor(width * .10); // id
            this.GrdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .10); // tipo
            this.GrdLista.Columns[3].Width =
                                (int)System.Math.Floor(width * .13); // Fecha entrada
            this.GrdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .13); // Fecha salida
            this.GrdLista.Columns[5].Width =
                               (int)System.Math.Floor(width * .06); // garaje
            this.GrdLista.Columns[6].Width =
                               (int)System.Math.Floor(width * .09); // Precio total
            this.GrdLista.Columns[7].Width =
                               (int)System.Math.Floor(width * .09); // DNI Cliente
            this.GrdLista.Columns[8].Width =
                               (int)System.Math.Floor(width * .09); // btn modificar
            this.GrdLista.Columns[9].Width =
                               (int)System.Math.Floor(width * .09); // btn borrar
            this.GrdLista.Columns[10].Width =
                               (int)System.Math.Floor(width * .09); // btn factura
        }


        void ConsultaCliente()
        {
            Console.WriteLine("Consulta Clientes");
            var dlgConsultaCliente = new DlgConsultaCliente(this.RegClientes);


            this.Hide();

            if (dlgConsultaCliente.ShowDialog() == DialogResult.OK) { }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }

        }
        void BuscarTodos()
        {

                Console.WriteLine("holaaa");
                this.ReservasBuscar = this.Reservas;
                this.Actualiza();
        }

        void BuscarFiltrado()
        {
            DateTime today = DateTime.Today;
            DateTime finish = today.AddDays(5);
            Console.WriteLine("AQUII");
            List<Reserva> prueba = this.ReservasBuscar.Where(element =>  element.FechaEntrada >= today && element.FechaEntrada <= finish).ToList();
            prueba.ForEach(Console.WriteLine);
            Console.WriteLine("entra");
            Console.WriteLine(prueba);
            this.ReservasBuscar = new RegistroReserva(prueba,this.Clientes);
            Console.WriteLine(this.ReservasBuscar.List);
            this.Actualiza();
        }

        void BuscarPorP()
        {
            var dlgBuscarPorPersona = new DlgBuscarPorpersona();
            this.Hide();
            if (dlgBuscarPorPersona.ShowDialog() == DialogResult.OK)
            {
                RegistroClientes registroClientes = RegistroClientes.RecuperarXml();
                Cliente c = registroClientes.getCliente(dlgBuscarPorPersona.Cliente);
                DateTime today = DateTime.Today;
                List<Reserva> reserv = Reservas.List.Where(element => element.Cliente.DNI.Equals(c.DNI) && !(element.FechaEntrada <= today && element.FechaSalida > today)).ToList();
                
                this.ReservasBuscar = new RegistroReserva(reserv,this.Clientes);
                this.Actualiza();

            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }
            
        }
        void Actualiza()
        {
            Console.WriteLine("dentro actualiza");

            // var consultRes = new DlgConsultaReserva();


            int numElementos = this.ReservasBuscar.Count;
            Console.WriteLine("NUMERO DE RESERVAS: " + numElementos);

            this.SbStatus.Text = ("Numero de reservas: " + numElementos);

            for (int i = 0; i < numElementos; i++)
            {
                if (this.GrdLista.Rows.Count <= i)
                {
                    this.GrdLista.Rows.Add();
                }
                this.ActualizaFilaDeLista(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.GrdLista.Rows.Count - numElementos - 1;
            for (; numExtra >= 0; --numExtra)
            {
                this.GrdLista.Rows.RemoveAt(numElementos);
            }
            Console.WriteLine("fin");
        }

        private void ActualizaFilaDeLista(int numFila)
        {
            Console.WriteLine("dentro ActualizaFilaDeLista");

            if (numFila < 0
              || numFila > this.GrdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(numFila));
            }

            DataGridViewRow fila = this.GrdLista.Rows[numFila];
            Reserva reserva = this.ReservasBuscar[numFila];
            Console.WriteLine(fila);


            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = reserva.Id; //aaaammddhhh
            fila.Cells[2].Value = reserva.Tipo; 
            fila.Cells[3].Value = reserva.FechaEntrada.ToString("dd/MM/yyyy");
            fila.Cells[4].Value = reserva.FechaSalida.ToString("dd/MM/yyyy");
            fila.Cells[5].Value = reserva.Garaje;
            fila.Cells[6].Value = reserva.TotalConIva();

            try
            {
                fila.Cells[7].Value = reserva.Cliente.DNI;
            }
            catch (NullReferenceException) {
                fila.Cells[7].Value = " ";
            }

            fila.Cells[8].Value = "*";
            fila.Cells[9].Value = "*";
            fila.Cells[10].Value = "*";


            foreach (DataGridViewCell celda in fila.Cells)
            {
                if (celda.ColumnIndex < 7)
                {
                    celda.ToolTipText = reserva.ToString();
                }
                else if(celda.ColumnIndex == 7)
                {
                    try
                    {
                        celda.ToolTipText = reserva.Cliente.ToString();
                    }
                    catch (NullReferenceException) { }
                }
            }
        }



        public void ClickLista()
        {
            try
            {
                Console.WriteLine("clickLista : " + this.GrdLista.CurrentCell.ColumnIndex);

                if (this.GrdLista.CurrentCell.ColumnIndex == 8)
                {
                    int fila = this.GrdLista.CurrentCell.RowIndex;
                    this.Modificar((string)this.GrdLista.Rows[fila].Cells[1].Value);
                }
                else if (this.GrdLista.CurrentCell.ColumnIndex == 9)
                {
                    this.Eliminar();
                }
                else if (this.GrdLista.CurrentCell.ColumnIndex == 10)
                {
                    int fila = this.GrdLista.CurrentCell.RowIndex;
                    this.Factura((string)this.GrdLista.Rows[fila].Cells[1].Value);
                }

                this.Actualiza();
            }
            catch(Exception) { }
        }


        public void Eliminar()
        {

            Console.WriteLine("dentro eliminar");
            var id = (string)this.GrdLista.CurrentRow.Cells[1].Value;

            //Dialogo de confirmación de eiminación
            DialogResult result;
            string mensaje = "¿Está seguro de que desea eliminar la reserva con ID(" + id + "), del registro de reservas?";
            string tittle = "Eliminar reserva";

            result = MessageBox.Show(mensaje, tittle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Reservas.Remove(this.Reservas.getReserva(id));
                this.ReservasBuscar.Remove(this.ReservasBuscar.getReserva(id));
            }

            this.Reservas.GuardarXml();
        }

        public void Modificar(string id)
        {
            //A partir de la clave de la entidad Reserva, obtenemos la reserva a modificar
            Reserva ResModif = this.Reservas.getReserva(id);
            Cliente c = ResModif.Cliente;

            var dlgModificar = new DlgModificaReserva(ResModif, this.Clientes);

            this.Hide();
            if (dlgModificar.ShowDialog() == DialogResult.OK)
            {
                this.Reservas.Remove(ResModif);
                this.ReservasBuscar.Remove(ResModif);

                string tipo = dlgModificar.Tipo;
                DateTime fechaEntrada = dlgModificar.FechaEntrada;
                DateTime fechaSalida = dlgModificar.FechaSalida;
                string garaje = dlgModificar.Garaje;

                Reserva r = new Reserva(id, tipo, c, fechaEntrada, fechaSalida, garaje, ResModif.PrecioDia, ResModif.IVA, ResModif.Total);
                
                this.Reservas.Add(r);
                this.ReservasBuscar.Add(r);
                this.Actualiza();

            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }

        }

        public void Factura(string id)
        {
            //A partir de la clave de la entidad Reserva, obtenemos la reserva a modificar
            Reserva reservaFactura = this.Reservas.getReserva(id);
            var numRegRes = int.Parse((string)this.GrdLista.CurrentRow.Cells[0].Value);

            
            var dlgFactura = new DlgFacturaReserva(reservaFactura, this.Clientes, numRegRes);

            this.Hide();

            if (dlgFactura.ShowDialog() == DialogResult.OK)
            {
            
            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }
        }

        public void InsertaHabitacion()
        {
            Console.WriteLine("Inserta Habitacion");
            var dlgInsertaHabitacion = new DIgInsertaHabitaciones(this.Habitaciones, Habitaciones.List);


            this.Hide();

            if (dlgInsertaHabitacion.ShowDialog() == DialogResult.OK)
            {

                // obtener a apartir de un string un enum
                Habitacion.Tipos parsedTipo = default(Habitacion.Tipos);
                var element = dlgInsertaHabitacion.TipoHabitacion;
                if (element != null)
                {
                    // Try to parse
                    Enum.TryParse<Habitacion.Tipos>(element, out parsedTipo);
                }

                string numero3DigitosProvisional = (String)dlgInsertaHabitacion.NumHabitacion;
                string numero3Digitos = numero3DigitosProvisional.ToString().PadLeft(3, '0');

                Console.WriteLine("---------------->"+numero3Digitos);

                Console.WriteLine("Numero de habitacion " + numero3Digitos + "\ntipo " + parsedTipo + "\n Wfi " + dlgInsertaHabitacion.Wifi
                                  + "\n cj " + dlgInsertaHabitacion.CajaFuerte + "\n minib " + dlgInsertaHabitacion.MiniBar + "\n Baño " + dlgInsertaHabitacion.Baño + "\n Cocina " + dlgInsertaHabitacion.Cocina
                                  + "\n Tv ");
                //obtener el cliente a partir del DNI con getCliente(DNI) del registro de clientes
                Habitacion newHabitacion = new Habitacion(
                     numero3Digitos, parsedTipo,
                     dlgInsertaHabitacion.FechaRenova,
                     dlgInsertaHabitacion.UltimaReserva, Convert.ToBoolean(dlgInsertaHabitacion.Wifi),
                     Convert.ToBoolean(dlgInsertaHabitacion.CajaFuerte),
                     Convert.ToBoolean(dlgInsertaHabitacion.MiniBar),
                     Convert.ToBoolean(dlgInsertaHabitacion.Baño),
                     Convert.ToBoolean(dlgInsertaHabitacion.Cocina),
                     Convert.ToBoolean(dlgInsertaHabitacion.Tv)
                );

                if (this.Reservas.comprobarId(newHabitacion.Numero))
                {
                    string mensaje = "No se ha insertado la reserva, porque el ID ya pertenece a una reserva del registro";
                    string tittle = "Error al insertar";
                    MessageBox.Show(mensaje, tittle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    this.Habitaciones.Add(newHabitacion);

                    this.Habitaciones.GuardarXml();
                }

            }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }
        }


        public void ConsultaHabitaciones()
        {
            Console.WriteLine("Consulta Habitaciones");
            var dlgConsultaHabitaicon = new DIgConsultaHabitacion(this.Habitaciones,this.Reservas,0);


            this.Hide();

            if (dlgConsultaHabitaicon.ShowDialog() == DialogResult.OK) { }

            if (!this.IsDisposed) { this.Show(); }
            else { Application.Exit(); }

        }

        void Guardar()
        {
            // this.regRes.GuardarXml();
            this.Reservas.GuardarXml();
        }

        public void Salir()
        {
            Console.WriteLine("guarda y sale");
            this.Reservas.GuardarXml();
            Application.Exit();
        }

        void BusquedaPasadasHotel()
        {
            DateTime today = DateTime.Today;
            Console.WriteLine("BusquedaPasadasHotel");
            List<Reserva> reservas = new List<Reserva>();
            foreach (Reserva r in Reservas)
            {
                if (r.FechaSalida<today)
                {
                    reservas.Add(r);
                }
            }

            this.ReservasBuscar = new RegistroReserva(new RegistroReserva(reservas, Clientes),this.Clientes);
            this.Actualiza();
        }
        
        void BusquedaPorHabitacion()
        {
            Console.WriteLine("Consulta por habitaciones");
            var dlgConsultaHabitacion= new DIgConsultaHabitacion(Habitaciones,Reservas,1);


            this.Hide();

            if(dlgConsultaHabitacion.ShowDialog() == DialogResult.OK) { }

            if (!this.IsDisposed) { this.Show(); }
            else{  Application.Exit();  }
        }



        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem opGuardar;
        public MenuItem opSalir;
        public MenuItem opVolver;
        public MenuItem mBuscar;
        public MenuItem opBuscarAll;
        public MenuItem opBuscarPendientes;
        public MenuItem opBuscarPasadas;
        public MenuItem opBuscarPorPersona;
        public MenuItem opBuscarPorHabitacion;
        public MenuItem mCliente;
        public MenuItem opInsertarC;
        public MenuItem opConsultaC;
        public MenuItem mHabitaciones;
        public MenuItem opInsertarH;
        public MenuItem opConsultaH;


        public StatusBar SbStatus;
        private Panel pnlPpal;
        private TextBox edDetalle;
        public DataGridView GrdLista;


        private readonly List<Cliente> Clientes;

        private RegistroReserva Reservas;
        private RegistroReserva ReservasBuscar;
        private RegistroClientes RegClientes;
        private RegistroHabitaciones Habitaciones;
        private MainWindowCore MVC;

    }

}
