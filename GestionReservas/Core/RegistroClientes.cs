namespace GestionReservas.Core
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Collections;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public class RegistroClientes : ICollection<Cliente>
    {
        public const string ArchivoXML = "clientes.xml";
        public const string EtqClientes = "clientes";
        public const string EtqCliente = "cliente";
        public const string EtqDni = "dni";
        public const string EtqNombre = "nombre";
        public const string EtqTelefono = "telefono";
        public const string EtqEmail = "email";
        public const string EtqDireccionPostal = "direccionPostal";


        private List<Cliente> clientes;

        public List<Cliente> List
        {
            get { return this.clientes; }
        }

        public RegistroClientes()
        {
            this.clientes = new List<Cliente>();
        }

        public RegistroClientes(IEnumerable<Cliente> clientes) : this()
        {
            this.clientes.AddRange(clientes);
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

        

        public int Count
        {
            get { return this.clientes.Count; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Cliente cliente)
        {
            this.clientes.Add(cliente);
        }

        public void Clear()
        {
            this.clientes.Clear();
        }

        public bool Contains(Cliente cliente)
        {
            return this.clientes.Contains(cliente);
        }

        public void CopyTo(Cliente[] cliente, int i)
        {
            this.clientes.CopyTo(cliente, i);
        }

        public IEnumerator<Cliente> GetEnumerator()
        {
            foreach (var cliente in this.clientes)
            {
                yield return cliente;
            }
        }

        public bool Remove(Cliente cliente)
        {
            return this.clientes.Remove(cliente);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var cliente in this.clientes)
            {
                yield return cliente;
            }
        }

        public Cliente this[int i]
        {
            get { return this.clientes[i]; }
            set { this.clientes[i] = value; }
        }

        public override string ToString()
        {
            var toret = new StringBuilder();
            foreach (Cliente c in clientes)
            {
                toret.Append(c);
            }

            return toret.ToString();
        }

        public void GuardarXml()
        {
            this.GuardarXml(ArchivoXML);
        }

        public void GuardarXml(String n)
        {
            var doc = new XDocument();
            var root = new XElement(EtqClientes);

            foreach (Cliente c in clientes)
            {
                XElement cliente = new XElement(EtqCliente,
                                            new XAttribute(EtqDni, c.DNI),
                                            new XAttribute(EtqNombre, c.Nombre),
                                            new XAttribute(EtqTelefono, c.Telefono),
                                            new XAttribute(EtqEmail, c.Email),
                                            new XAttribute(EtqDireccionPostal, c.DireccionPostal)

                                            );

                root.Add(cliente);
            }
            doc.Add(root);
            doc.Save(n);
        }

        public static RegistroClientes RecuperarXml()
        {
            return RecuperarXml(ArchivoXML);
        }

        //Recupera los datos de un archivo xml
        public static RegistroClientes RecuperarXml(String n)
        {
            var toret = new RegistroClientes();
            try
            {
                var doc = XDocument.Load(n);
                Console.WriteLine("Cargando del fichero: " + n);
                if (doc.Root != null && doc.Root.Name == EtqClientes)
                {
                    var clientes = doc.Root.Elements(EtqCliente);
                    foreach (XElement cliente in clientes)
                    {
                        var c = GetClienteXML(cliente);
                        toret.Add(c);
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

        public static Cliente GetClienteXML(XElement c)
        {
            return new Cliente(
                (string)c.Attribute(EtqDni),
                (string)c.Attribute(EtqNombre),
                (int)c.Attribute(EtqTelefono),
                (string)c.Attribute(EtqEmail),
                (string)c.Attribute(EtqDireccionPostal)
            );
        }
    }


}

