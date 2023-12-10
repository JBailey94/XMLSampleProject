//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Serialization;

//namespace XmlSampleProject
//{
//    [XmlRoot(ElementName = "Root")]
//    public class CustomerData
//    {
//        [XmlArray(ElementName = "Customers")]
//        [XmlArrayItem(ElementName = "Customer")]
//        public Customer[] Customers { get; set; }

//        [XmlArray(ElementName = "Orders")]
//        [XmlArrayItem(ElementName = "Order")]
//        public Order[] Orders { get; set; }
//    }

//    public class Customer
//    {
//        [XmlAttribute]
//        public string? CustomerID { get; set; } 
//        public string? CompanyName { get; set; }
//        public string? ContactName { get; set; }
//        public string? ContactTitle { get; set; }
//        public string? PhoneticName { get; set; }
//        public FullAddress? FullAddress { get; set; }
//    }

//    public class FullAddress
//    {
//        public string? Address { get; set; }
//        public string? City { get; set; }
//        public string? Region { get; set; }
//        public string? PostalCode { get; set; }
//        public string? Country { get; set; }
//    }
  
//    //public class Order
//    //{
//    //    public string? CustomerId { get; set; }
//    //    public int? EmployeeId { get; set; }
//    //    public string? OrderDate { get; set; }
//    //    public string? RequiredDate { get; set; }

//    //    [XmlAnyAttribute]
//    //    public string? Parametric { get; set; }

//    //}

//    public class ShipInfo
//    {
//        [XmlAttribute]
//        public string? ShippedDate { get; set; }
//        public int? ShipVia { get; set; }
//        public double? Freight { get; set; }
//        public string? ShipName { get; set; }
//        public string? ShipAddress { get; set; }
//        public string? ShipCity { get; set; }
//        public string? ShipRegion { get; set; }
//        public int? ShipPostalCode { get; set; }
//        public string? ShipCountry { get; set; }

//        [XmlAnyAttribute]
//        public string? Parametric { get; set; }
//    }
//}
