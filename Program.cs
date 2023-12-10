using System.Linq.Expressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace XmlSampleProject
{
    public class Parametric
    {
        [XmlAttribute("Index")]
        public string? Index { get; set; }
        [XmlAttribute("Group")]
        public string? Group { get; set; }
        [XmlAttribute("XPath")]
        public string? XPath { get; set; }
        [XmlAttribute("Sequence")]
        public string? Sequence { get; set; }
    }

    public class OrderCollection
    {
        [XmlArray("Array")]
        [XmlArrayItem(ElementName = "Order")]
        public List<Order> Orders { get; set; }
    }
    public class ShipInfoCollection
    {
        [XmlArray("Array")]
        [XmlArrayItem(ElementName = "ShipInfo")]
        public List<ShipInfo> ShipInfos { get; set; }
    }

    public class Order
    {
        public string CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public ShipInfo ShipInfo { get; set; }
        public Parametric Parametric { get; set; }
    }

    public class ShipInfo
    {
        [XmlAttribute("ShippedDate")]
        public DateTime ShippedDate { get; set; }
        public int ShipVia { get; set; }
        public double Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public Parametric Parametric { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Parametric> parametricData = GetParametricData();
            OrderCollection orders = PopulateModelFromParametric<OrderCollection>(Resources.CustomerData, parametricData[0]);
            ShipInfoCollection shipInfos = PopulateModelFromParametric<ShipInfoCollection>(Resources.CustomerData, parametricData[1]);

            Console.WriteLine("Hello, World!");
        }

        /// <summary>
        /// Builds and returns a collection generically.
        /// </summary>
        /// <typeparam name="T">The collection model to populate.</typeparam>
        /// <param name="OriginalXml">The original xml string.</param>
        /// <param name="parametric">Parametric that contains xpath, sequence, and group.</param>
        /// <returns>ItemCollection that contains deserialized data</returns>
        static T PopulateModelFromParametric<T>(string OriginalXml, Parametric parametric) where T : new()
        {
            T model = new T();

            // load and setup navigation for the original xml document
            StringReader stringReader = new StringReader(OriginalXml);
            XDocument originalDocument = XDocument.Load(stringReader);

            // construct a new XDocument tree from parametric xPath
            XDocument constructedDocument = new XDocument();
            XElement rootElement = new XElement(model.GetType().Name);
            XElement arrayElement = new XElement("Array");

            // constructs a new XDocument subtree from original based on the Xpath
            IEnumerable<XElement> elements = originalDocument.XPathSelectElements(parametric.XPath);
            foreach (XElement element in elements)
            {
                // build Parametric element
                XElement parametricElement = new XElement("Parametric");
                XAttribute IndexAttribute = new XAttribute("Index", parametric.Index);
                XAttribute SequenceAttribute = new XAttribute("Sequence", parametric.Sequence);
                XAttribute XPathAttribute = new XAttribute("XPath", parametric.XPath);
                XAttribute GroupAttribute = new XAttribute("Group", parametric.Group);

                // add attributes to Parametric element
                parametricElement.Add(IndexAttribute);
                parametricElement.Add(SequenceAttribute);
                parametricElement.Add(GroupAttribute);
                parametricElement.Add(XPathAttribute);
                element.Add(parametricElement);
                arrayElement.Add(element);
            }

            rootElement.Add(arrayElement);
            constructedDocument.Add(rootElement);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (XmlReader xmlReader = constructedDocument.CreateReader()) 
            {
                model = (T) xmlSerializer.Deserialize(xmlReader);
            }

            return model;
        }


        /// <summary>
        /// Simulates a parametric database call.
        /// </summary>
        /// <returns>List of parametric data</returns>
        static List<Parametric> GetParametricData()
        {
            List<Parametric> parametrics = new List<Parametric>()
            {
                new Parametric() { Index = "1", XPath = "Root/Orders/Order", Sequence = "10", Group = "gOrder"},
                new Parametric() { Index = "2", XPath = "Root/Orders/Order/ShipInfo", Sequence = "15", Group = "gOrder" }
            };

            return parametrics;
        }
    }
}