using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{ 
    [XmlRoot("CarType", Namespace = "vehicular_simulation", IsNullable = false)]
    public class CarType
    {
        public bool miniCar;
        public bool mediumCar;
        public bool bigCar;

        public int speedlimite;
        public int carcorner;
        public int acceleratorValue;
        public string acceleratorValue_text;

        public int brakeValue;
        public string brakeValue_text;

    }
}
