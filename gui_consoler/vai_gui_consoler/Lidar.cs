using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("Lidar", Namespace = "vehicular_simulation", IsNullable = false)]
    public class Lidar
    {        
        public int D_lidar_equipment;
        public int D_lidar_value;
        public string D_lidar_value_text;

        public string D_lidar_vertical_range;
        public string D_lidar_horizontal_range;
        public string D_lidar_accuracy;
        public int D_lidar_frequency;

    }
}
