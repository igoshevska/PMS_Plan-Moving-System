using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NBRM
{
    public class KursResult
    {
        [XmlRoot("dsKurs")]
        public class dsKurs
        {
            [XmlElement("KursZbir")]
            public List<KursZbir> KursZbir { get; set; }
        }

        public class KursZbir
        {
            [XmlElement("RBr")]
            public double RBr { get; set; }

            [XmlElement("Datum")]
            public DateTime Datum { get; set; }

            [XmlElement("Valuta")]
            public double Valuta { get; set; }

            [XmlElement("Oznaka")]
            public string Oznaka { get; set; }

            [XmlElement("Drzava")]
            public string Drzava { get; set; }

            [XmlElement("Nomin")]
            public double Nomin { get; set; }

            [XmlElement("Sreden")]
            public double Sreden { get; set; }

            [XmlElement("DrzavaAng")]
            public string DrzavaAng { get; set; }

            [XmlElement("DrzavaAl")]
            public string DrzavaAl { get; set; }
            [XmlElement("NazivMak")]
            public string NazivMak { get; set; }
            [XmlElement("ValutaNaziv_AL")]
            public string ValutaNaziv_AL { get; set; }
            [XmlElement("Datum_f")]
            public DateTime Datum_f { get; set; }

        }
    }
}
