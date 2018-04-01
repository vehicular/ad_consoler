using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("Location", Namespace = "vehicular_simulation", IsNullable = false)]
    public   class Location
    {
        public int startPosition;
        public int endPosition;

    }
}
