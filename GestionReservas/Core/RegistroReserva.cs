
namespace GestionReservas.Core
{

    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public class RegistroReserva : ICollection<Reserva>
    {

        //Etiquetas XML
        public const string EtqReservas = "reservas";
        public const string EtqReserva = "reserva";
        public const string EtqId = "id";
        public const string EtqTipo = "tipo";
        public const string EtqCliente = "cliente";
        public const string EtqClienteDNI = "clienteDNI";
        public const string EtqDataIn = "fechaEntrada";
        public const string EtqDataOut = "fechaSalida";
        public const string EtqGaraje = "garaje";
        public const string EtqPrecioDia = "precioDia";
        public const string EtqIva = "IVA";
        public const string EtqTotal = "total";



        public const string archivoXML = "reservas.xml";

        private List<Reserva> reservas;

        public List<Reserva> List
        {
            get { return this.reservas; }
        }


        public RegistroReserva(List<Cliente> client)
        {
            this.reservas = new List<Reserva>();
            this.clientes = client;
            this.RecuperarXml();
        }

        public RegistroReserva(IEnumerable<Reserva> reservas, List<Cliente> client) : this(client)
        {
            this.reservas.AddRange(reservas);
        }

        public int Count => this.reservas.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Reserva r)
        {
            this.reservas.Add(r);
        }

        public void Clear()
        {
            this.reservas.Clear();
        }



        public Reserva getReserva(string id)
        {
            foreach (Reserva r in this.reservas)
            {
                if (r.Id == id)
                {
                    return r;
                }
            }
            return null;
        }

        public List<String> getIds()
        {
            List<String> ids = new List<string>();
            foreach (Reserva r in this.reservas)
            {
                ids.Add(r.Id);
            }
            return ids;
        }

        public bool comprobarId(string id)
        {
            bool toret = false;
            foreach (Reserva r in this.reservas)
            {
                if (r.Id == id)
                {
                    toret = true;
                }
            }

            return toret;
        }

        public bool Contains(Reserva reserva)
        {
            return this.reservas.Contains(reserva);
        }




        public void CopyTo(Reserva[] reserva, int i)
        {
            this.reservas.CopyTo(reserva, i);
        }

        public IEnumerator<Reserva> GetEnumerator()
        {
            foreach (var reserva in this.reservas)
            {
                yield return reserva;
            }
        }

        public bool Remove(Reserva reserva)
        {
            return this.reservas.Remove(reserva);
        }

        
        public Reserva this[int i]
        {
            get { return this.reservas[i]; }
            set { this.reservas[i] = value; }
        }

        public override string ToString()
        {
            var toret = new StringBuilder();
            foreach (Reserva r in reservas)
            {
                toret.Append(r);
            }

            return toret.ToString();
        }


        public Cliente getCliente(string dni)
        {
            foreach (Cliente c in this.clientes)
            {
                if (c.DNI == dni)
                {
                    return c;
                }
            }
            return null;
        }


        public void GuardarXml()
        {
            Console.WriteLine("GuardaXml reserva");
            this.GuardarXml(archivoXML);
        }


        //guarda los datos de la reserva en un archivo XML
        public void GuardarXml(String nf)
        {
            Console.WriteLine("Escribe en el fichero: " + nf);

            var doc = new XDocument();
            var root = new XElement(EtqReservas);

            foreach (Reserva r in reservas)
            {
                XElement reserva = new XElement(EtqReserva,
                                            new XElement(EtqId, r.Id),
                                            new XElement(EtqTipo, r.Tipo),
                                            new XElement(EtqCliente, new XElement(EtqClienteDNI, r.Cliente.DNI)),
                                            new XElement(EtqDataIn, r.FechaEntrada),
                                            new XElement(EtqDataOut, r.FechaSalida),
                                            new XElement(EtqGaraje, r.Garaje),
                                            new XElement(EtqPrecioDia, r.PrecioDia),
                                            new XElement(EtqIva, r.IVA),
                                            new XElement(EtqTotal, r.TotalConIva())
                                        );

                root.Add(reserva);
            }
            doc.Add(root);
            doc.Save(nf);
        }

        public void RecuperarXml()
        {
            this.RecuperarXml(archivoXML);
        }

        //Recupera los datos de la reserva de un archivo xml
        public void RecuperarXml(String nf)
        {

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
                        this.Add(r);
                    }
                }
            }
            catch (XmlException)
            {
                Clear();
            }
            catch (IOException)
            {
                Clear();
            }

        }
        
        private Reserva GetReservaXML(XElement r)
        {
            Reserva toret = new Reserva(
                (string)r.Element(EtqId),
                (string)r.Element(EtqTipo),
                this.getCliente((string)r.Element(EtqCliente)),
                (DateTime)r.Element(EtqDataIn),
                (DateTime)r.Element(EtqDataOut),
                (string)r.Element(EtqGaraje),
                (double)r.Element(EtqPrecioDia),
                (int)r.Element(EtqIva),
                (double)r.Element(EtqTotal)
            );

            return toret;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private List<Cliente> clientes;

    }
}
