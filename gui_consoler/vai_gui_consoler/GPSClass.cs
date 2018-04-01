using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("GPSClass", Namespace = "vehicular_simulation", IsNullable = false)]
    public   class GPSClass
    {
        public int D_gps_equipment;
        public string D_gps_horizontal_positioning_accuracy;
        public string D_gps_velocity_accuracy;
        public string D_gps_time_precision;       
    }
}
