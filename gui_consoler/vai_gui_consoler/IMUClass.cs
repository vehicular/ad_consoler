using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("IMUClass", Namespace = "vehicular_simulation", IsNullable = false)]
    public class IMUClass
    {
        public int D_imu_equipment;
        public string D_imu_roll_angle;
        public string D_imu_pitch_angle;
        public string D_imu_heading_angle;
        public string D_imu_three_axis_acceleration;
        public string D_imu_three_axis_angular_velocity;
        public string D_imu_three_axis_geomagnetic;

    }
}
