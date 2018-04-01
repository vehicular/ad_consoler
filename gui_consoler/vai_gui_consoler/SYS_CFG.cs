using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("SYS_CFG", Namespace = "vehicular_simulation", IsNullable = false)]
    public class SYS_CFG
    {
        public string lastDataPath;
        public string D_startPosition;//开始位置
        public string D_endPosition;//终止位置
        public int D_selectLanguage;//语言选择

        public bool D_rainRadio;//雨天
        public int D_rainValue;//雨天值
        public string D_rainValue_text;//雨天值文本
        public bool D_snowRadio;//下雪
        public int D_snowValue;//下雪值
        public string D_snowValue_text;//下雪值文本
        public bool D_fogRadio;//雾天
        public int D_fogValue;//雾天值
        public string D_fogValue_text;//雾天值文本
        public int D_dayandnight;//白天夜晚选择
        public int D_time;//时间选择
        public string D_sunAngle;//太阳角度选择
        public bool D_sunRadio;//晴天选择
        public int D_sunValue;//晴天值选择
        public string D_sunValue_text;//晴天值文本

        public int D_lidar_equipment;//雷达设备选择
        public int D_lidar_value;//雷达值选择
        public string D_lidar_value_text;//雷达值文本
        
        public string D_lidar_vertical_range;
        public string D_lidar_horizontal_range;
        public string D_lidar_accuracy;
        public int D_lidar_frequency;

       
        public int D_radar_equipment;//radar设备选择
        public int D_radar_value;//radar值选择
        public string D_radar_value_text;//radar值文本
        public string D_radar_accuracy;//radar上下夹角范围
        public string D_radar_speed;//radar频率
        public string D_radar_range;//radar误差

        public int D_imu_equipment;//imu设备选择
        public string D_imu_roll_angle;//横滚角
        public string D_imu_pitch_angle;//俯仰角
        public string D_imu_heading_angle;//航向角
        public string D_imu_three_axis_acceleration;//三轴加速度
        public string D_imu_three_axis_angular_velocity;//三轴角速度
        public string D_imu_three_axis_geomagnetic;//三周地磁

        public int D_gps_equipment;//gps设备选择
        public string D_gps_horizontal_positioning_accuracy;//水平定位精度
        public string D_gps_velocity_accuracy;//速度精度
        public string D_gps_time_precision;//时间精度

        public bool D_miniCar;//小型汽车
        public bool D_mediumCar;//中型汽车
        public bool D_bigCar;//大型汽车
        public int D_speedlimite;//车速限制
        public int D_carcorner;//方向盘转角
        public int D_acceleratorValue;//油门设置
        public string D_acceleratorValue_text;//油门文本

        public int D_brakeValue;//刹车设置
        public string D_brakeValue_text;//刹车文本

    }
}
