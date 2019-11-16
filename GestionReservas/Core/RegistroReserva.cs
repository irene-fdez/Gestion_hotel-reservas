
namespace GestionReservas.Core
{

    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;

    class RegistroReserva 
    {

        //Etiquetas XML
        public const string EtqReservas = "reservas";
        public const string EtqReserva = "reserva";
        public const string EtqId = "id";
        public const string EtqTipo = "tipo";
        public const string EtqCliente = "cliente";
        public const string EtqDataIn = "fechaEntrada";
        public const string EtqDataOut = "fechaSalida";
        public const string EtqGaraje = "garaje";
        public const string EtqPrecioDia = "precioDia";
        public const string EtqIva = "IVA";
        public const string EtqTotal = "total";


        public const string EtqDni = "dni";
        public const string EtqNombre = "nombre";
        public const string EtqTlf = "telefono";
        public const string EtqEmail = "email";
        public const string EtqDireccion = "direccion";

        public const string archivoXML = "reservas.xml";

        private List<Reserva> reservas;



        public RegistroReserva()
        {
            this.reservas = new List<Reserva>();
           // this.regReserva = RegistroReserva.RecuperaXml();
        }

        public List<Reserva> List
        {
            get { return this.reservas; }
        }

        public int Count => this.reservas.Count;


        public void Add(Reserva r)
        {
            this.reservas.Add(r);
        }


        public void Remove(Reserva r)
        {
            this.reservas.Remove(r);
        }


        public Reserva getReserva(string id)
        {
            foreach(Reserva r in this.reservas)
            {
                if(r.Id == id)
                {
                    return r;
                }
            }
            return null;
        }

        public void GuardarXml()
        {
            this.GuardarXml(archivoXML);
        }


        //guarda los datos de la reserva en un archivo XML
        public void GuardarXml(String nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqReservas);

            foreach (Reserva r in reservas)
            {
                XElement reserva = new XElement(EtqReserva,
                                            new XAttribute(EtqId, r.Id),
                                            new XAttribute(EtqTipo, r.Tipo),
                                            new XAttribute(EtqCliente, r.Cliente), //ver como insertar todos los datos del cliente
                                            new XAttribute(EtqDataIn, r.FechaEntrada),
                                            new XAttribute(EtqDataOut, r.FechaSalida),
                                            new XAttribute(EtqGaraje, r.Garaje),
                                            new XAttribute(EtqPrecioDia, r.PrecioDia),
                                            new XAttribute(EtqIva, r.IVA)
               
                                            );

                root.Add(reserva);
            }
            doc.Add(root);
            doc.Save(nf);
        }

        public RegistroReserva RecuperaXml()
        {
            return RecuperaXml(archivoXML);
        }

        //Recupera los datos de la reserva de un archivo xml
        public RegistroReserva RecuperaXml(String nf)
        {
            var toret = new RegistroReserva();
            try
            {
                var doc = XDocument.Load(nf);
                Console.WriteLine("Cargando del fichero: " + nf);
                if (doc.Root != null && doc.Root.Name == EtqReservas)
                {
                    var reservas = doc.Root.Elements(EtqReserva);
                    foreach (XElement reserva in reservas)
                    {
                        var r = GetReservaXML(reserva);
                        toret.Add(r);
                    }
                }
            }
            catch (XmlException)
            {
                toret.Clear();
            }
            catch (IOException)
            {
                toret.Clear();
            }
            return toret;
        }

        public void Clear()
        {
            this.reservas.Clear();
        }

        private Cliente GetCliente(XElement element)
        {
            //    System.Console.WriteLine((int)element.Attribute(EtqNumSerie) +
            //      (string)element.Attribute(EtqModelo));

            return new Cliente(
                (string)element.Attribute(EtqDni),
                (string)element.Attribute(EtqNombre),
                (double)element.Attribute(EtqTlf),
                (string)element.Attribute(EtqEmail),
                (string)element.Attribute(EtqDireccion));
        }

        private Reserva GetReservaXML(XElement r)
        {

            Reserva toret = new Reserva(
                (string)r.Attribute(EtqId),
                (string)r.Attribute(EtqTipo),
                this.GetCliente(r), 
                (DateTime)r.Attribute(EtqDataIn),
                (DateTime)r.Attribute(EtqDataOut),
                (string)r.Attribute(EtqGaraje),
                (double)r.Attribute(EtqPrecioDia),
                (int)r.Attribute(EtqIva)
            );

            return toret;
        }


    }
}
