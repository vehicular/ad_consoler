using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("Radar", Namespace = "vehicular_simulation", IsNullable = false)]
    public class Radar
    {
        public int D_radar_equipment;
        public int D_radar_value;
        public string D_radar_value_text;
        public string D_radar_accuracy;
        public string D_radar_speed;
        public string D_radar_range;

    }
}
