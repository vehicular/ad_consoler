using System;
using System.Collections.Generic;
using System.Text;

namespace vehicular_simulation
{
    public class LanguageText
    {
        public static bool isEnglish = false;

        #region
        public static string label1
        {
            get
            {
                if (isEnglish)
                    return "Start Position";
                else
                    return "开始地址";
            }
        }
        public static string label2
        {
            get
            {
                if (isEnglish)
                    return "End Position";
                else
                    return "终止地址";
            }
        }
        public static string Label_language_option {
            get
            {
                if (isEnglish)
                    return "Select Language";
                else
                    return "语言选择";
            }
        }

        public static string Set_software
        {
            get
            {
                if (isEnglish)
                    return "Set Software";
                else
                    return "软件设置";
            }
        }
        public static string Start
        {
            get
            {
                if (isEnglish)
                    return "Start";
                else
                    return "开始启动";
            }
        }
        public static string tabPage1
        {
            get
            {
                if (isEnglish)
                    return "Select Position";
                else
                    return "地址选择";
            }
        }
        public static string tabPage2
        {
            get
            {
                if (isEnglish)
                    return "Select Weather";
                else
                    return "天气选择";
            }

        }
        public static string tabPage3
        {
            get
            {
                if (isEnglish)
                    return "Select Car Type";
                else
                    return "车子类型选择";
            }
        }
        public static string tabPage4
        {
            get
            {
                if (isEnglish)
                    return "Select Equipment";
                else
                    return "设备选择";
            }
        }

        public static string groupBox4
        {
            get
            {
                if (isEnglish)
                    return "Select Day or Night";
                else
                    return "昼夜选择";
            }
        }
        public static string weatherGroup
        {
            get
            {
                if (isEnglish)
                    return "Weather Select";
                else
                    return "天气选择";
            }
        }
        public static string label20
        {
            get
            {
                if (isEnglish)
                    return "Time Select";
                else
                    return "时间选择";
            }
        }
        public static string label21
        {
            get
            {
                if (isEnglish)
                    return "Sun Angle";
                else
                    return "太阳角度";
            }
        }
        public static string sunRadio
        {
            get
            {
                if (isEnglish)
                    return "Sun";
                else
                    return "晴天";
            }
        }
        public static string sunGroup
        {
            get
            {
                if (isEnglish)
                    return "Sun Degree";
                else
                    return "晴天程度";
            }
        }
        public static string label55
        {
            get
            {
                if (isEnglish)
                    return "Cloudy";
                else
                    return "阴天";
            }
        }
        public static string label58
        {
            get
            {
                if (isEnglish)
                    return "Medium Sun";
                else
                    return "中度太阳";
            }
        }

        public static string label59
        {
            get
            {
                if (isEnglish)
                    return "Scorching Sun";
                else
                    return "烈日";
            }
        }
        public static string rainRadio
        {
            get
            {
                if (isEnglish)
                    return "Rain";
                else
                    return "下雨";
            }
        }
        public static string rainGroup
        {
            get
            {
                if (isEnglish)
                    return "Rain Degree";
                else
                    return "下雨程度";
            }
        }
        public static string label3
        {
            get
            {
                if (isEnglish)
                    return "Light rain";
                else
                    return "小雨";
            }
        }
        public static string label9
        {
            get
            {
                if (isEnglish)
                    return "Moderate Rain";
                else
                    return "中雨";
            }
        }
        public static string label4
        {
            get
            {
                if (isEnglish)
                    return "Heavy Rain";
                else
                    return "大雨";
            }
        }
        public static string snowRadio
        {
            get
            {
                if (isEnglish)
                    return "Snow";
                else
                    return "下雪";
            }
        }
        public static string snowGroup
        {
            get
            {
                if (isEnglish)
                    return "Snow Degree";
                else
                    return "下雪程度";
            }
        }

        public static string label5
        {
            get
            {
                if (isEnglish)
                    return "Light Snow";
                else
                    return "小雪";
            }
        }
        public static string label10
        {
            get
            {
                if (isEnglish)
                    return "Moderate Snow";
                else
                    return "中雪";
            }
        }
        public static string label6
        {
            get
            {
                if (isEnglish)
                    return "Heavy Snow";
                else
                    return "大雪";
            }
        }
        public static string fogRadio
        {
            get
            {
                if (isEnglish)
                    return "Fog";
                else
                    return "雾天";
            }
        }
        public static string fogGroup
        {
            get
            {
                if (isEnglish)
                    return "Concentration of Fog";
                else
                    return "雾的浓度";
            }
        }
        public static string label7
        {
            get
            {
                if (isEnglish)
                    return "Light Fog";
                else
                    return "小雾";
            }
        }
        public static string label11
        {
            get
            {
                if (isEnglish)
                    return "Moderate Fog";
                else
                    return "中雾";
            }
        }
        public static string label8
        {
            get
            {
                if (isEnglish)
                    return "Heavy Fog";
                else
                    return "大雾";
            }
        }

        public static string groupBox1
        {
            get
            {
                if (isEnglish)
                    return "Select Car";
                else
                    return "车子类型选择";
            }
        }
        public static string miniCar
        {
            get
            {
                if (isEnglish)
                    return "Small car";
                else
                    return "小型汽车";
            }
        }
        public static string mediumCar
        {
            get
            {
                if (isEnglish)
                    return "Medium car";
                else
                    return "中型汽车";
            }
        }
        public static string bigCar
        {
            get
            {
                if (isEnglish)
                    return "Large car";
                else
                    return "大型汽车";
            }
        }
        public static string CarGroup
        {
            get
            {
                if (isEnglish)
                    return "Selection of Automobile Parameters";
                else
                    return "汽车参数选择";
            }
        }
        public static string label40
        {
            get
            {
                if (isEnglish)
                    return "Speed Limit";
                else
                    return "车速限制";
            }
        }
        public static string label41
        {
            get
            {
                if (isEnglish)
                    return "Steering Wheel Angle";
                else
                    return "方向盘转角";
            }
        }
        public static string label42
        {
            get
            {
                if (isEnglish)
                    return "Throttle";
                else
                    return "油门";
            }
        }

        public static string groupBox2
        {
            get
            {
                if (isEnglish)
                    return "Throttle setting";
                else
                    return "油门设置";
            }
        }
        public static string label45
        {
            get
            {
                if (isEnglish)
                    return "Small Throttle";
                else
                    return "小油门";
            }
        }
        public static string label46
        {
            get
            {
                if (isEnglish)
                    return "Middle Throttle";
                else
                    return "中油门";
            }
        }
        public static string label47
        {
            get
            {
                if (isEnglish)
                    return "Big Throttle";
                else
                    return "大油门";
            }
        }
        public static string label43
        {
            get
            {
                if (isEnglish)
                    return "Brake";
                else
                    return "刹车";
            }
        }
        public static string groupBox3
        {
            get
            {
                if (isEnglish)
                    return "Brake Setting";
                else
                    return "刹车设置";
            }
        }
        public static string label48
        {
            get
            {
                if (isEnglish)
                    return "Light Brake";
                else
                    return "轻微刹车";
            }
        }
        public static string label51
        {
            get
            {
                if (isEnglish)
                    return "Medium Brake";
                else
                    return "中度刹车";
            }
        }

        public static string label52
        {
            get
            {
                if (isEnglish)
                    return "Heavy Brake";
                else
                    return "大力刹车";
            }
        }
        public static string lidar
        {
            get
            {
                if (isEnglish)
                    return "Lidar";
                else
                    return "激光雷达";
            }
        }
        public static string radar
        {
            get
            {
                if (isEnglish)
                    return "Radar";
                else
                    return "毫米波雷达";
            }
        }
        public static string imu
        {
            get
            {
                if (isEnglish)
                    return "IMU";
                else
                    return "IMU";
            }
        }
        public static string gps
        {
            get
            {
                if (isEnglish)
                    return "GPS";
                else
                    return "GPS";
            }
        }

        public static string label32
        {
            get
            {
                if (isEnglish)
                    return "Equipment Selection";
                else
                    return "设备选择";
            }
        }
        public static string lidarGroup
        {
            get
            {
                if (isEnglish)
                    return "Device Attribute Setting";
                else
                    return "设备属性设置";
            }
        }
        public static string label25
        {
            get
            {
                if (isEnglish)
                    return "Ray Range";
                else
                    return "射线范围";
            }
        }
        //TODO:
        //public static string label24
        //{
        //    get
        //    {
        //        if (isEnglish)
        //            return "Select Car";
        //        else
        //            return "米";
        //    }
        //}
        //public static string miniCar
        //{
        //    get
        //    {
        //        if (isEnglish)
        //            return "Small car";
        //        else
        //            return "米";
        //    }
        //}
        public static string label24
        {
            get
            {
                if (isEnglish)
                    return "Rotation Speed";
                else
                    return "旋转速度";
            }
        }
        public static string label34
        {
            get
            {
                if (isEnglish)
                    return "Horizontal Angular Resolution";
                else
                    return "水平角分辨率";
            }
        }
        public static string label16
        {
            get
            {
                if (isEnglish)
                    return "Vertical Field of Vision";
                else
                    return "垂直视野范围";
            }
        }
        public static string label30
        {
            get
            {
                if (isEnglish)
                    return "Horizontal Field of Vision";
                else
                    return "水平视野范围";
            }
        }
        public static string label17
        {
            get
            {
                if (isEnglish)
                    return "Angular Resolution";
                else
                    return "角分辨率";
            }
        }
        public static string label22
        {
            get
            {
                if (isEnglish)
                    return "Accuracy";
                else
                    return "精度";
            }
        }

        public static string label15
        {
            get
            {
                if (isEnglish)
                    return "Equipment Selection";
                else
                    return "设备选择";
            }
        }
        public static string radarGroup
        {
            get
            {
                if (isEnglish)
                    return "Device Attribute Setting";
                else
                    return "设备属性设置";
            }
        }
        public static string label44
        {
            get
            {
                if (isEnglish)
                    return "Ray Range";
                else
                    return "射线范围";
            }
        }
        public static string label14
        {
            get
            {
                if (isEnglish)
                    return "Upper And Lower Angle Range";
                else
                    return "上下夹角范围";
            }
        }
        public static string label19
        {
            get
            {
                if (isEnglish)
                    return "Frequency";
                else
                    return "频率";
            }
        }
        public static string label18
        {
            get
            {
                if (isEnglish)
                    return "Deviation";
                else
                    return "误差";
            }
        }
        public static string label56
        {
            get
            {
                if (isEnglish)
                    return "Equipment Selection";
                else
                    return "设备选择";
            }
        }
        public static string imuGroup
        {
            get
            {
                if (isEnglish)
                    return "Device Attribute Setting";
                else
                    return "设备属性设置";
            }
        }

        public static string label23
        {
            get
            {
                if (isEnglish)
                    return "Roll Angle";
                else
                    return "横滚角";
            }
        }
        public static string label57
        {
            get
            {
                if (isEnglish)
                    return "Pitching Angle";
                else
                    return "俯仰角";
            }
        }
        public static string label37
        {
            get
            {
                if (isEnglish)
                    return "Heading Angle";
                else
                    return "航向角";
            }
        }
        public static string label29
        {
            get
            {
                if (isEnglish)
                    return "Three Axis Acceleration";
                else
                    return "三轴加速度";
            }
        }
        public static string label28
        {
            get
            {
                if (isEnglish)
                    return "Three Axis Angular Velocity";
                else
                    return "三轴角速度";
            }
        }
        public static string label31
        {
            get
            {
                if (isEnglish)
                    return "Three Axis Geomagnetism";
                else
                    return "三轴地磁";
            }
        }
        public static string label53
        {
            get
            {
                if (isEnglish)
                    return "Equipment Selection";
                else
                    return "设备选择";
            }
        }
        public static string gpsGroup
        {
            get
            {
                if (isEnglish)
                    return "Device Attribute Setting";
                else
                    return "设备属性设置";
            }
        }

        public static string label54
        {
            get
            {
                if (isEnglish)
                    return "Horizontal Positioning Accuracy";
                else
                    return "水平定位精度";
            }
        }
        public static string label38
        {
            get
            {
                if (isEnglish)
                    return "Speed Precision";
                else
                    return "速度精度";
            }
        }
        public static string label39
        {
            get
            {
                if (isEnglish)
                    return "Time Precision";
                else
                    return "时间精度";
            }
        }
        #endregion
        //public static string HelloWorld
        //{
        //    get
        //    {
        //        if (isEnglish)
        //            return "hello world";
        //        else
        //            return "nihao";
        //    }
        //}
        //public static string Button1Text
        //{
        //    get
        //    {
        //        if (isEnglish)
        //            return "hello";
        //        else
        //            return "nihao";
        //    }
        //}
    }
}
