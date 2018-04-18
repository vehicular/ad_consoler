using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Utility;

namespace vehicular_simulation
{
    public partial class FormMain : Form
    {
        public delegate void DelegateInsertMessage(MessageBoxIcon type, string msg);
        public DelegateInsertMessage insertMessageDelegate;

        public delegate void DelegateControlButtonRun(bool isEnable);
        public DelegateControlButtonRun controlButtonRunDelegate;

        #region SetForm1
        #region InitComponent
        private SetPath _setpath = new SetPath();
        private SYS_CFG sysCfg;
        private SetPath setPath;
        //private Form2 form2;
        private string SYSTEM_PATH ;
        private string dataPath ;
        private byte[] cardata=new byte[1024];        

        private Location _locationObject;
        private Weather _weatherObject;
        private CarType _carTypeObject;

        private Lidar _lidarObject;
        private string LidarPath;

        private Radar _radarObject;
        private string RadarPath;

        private IMUClass _imuObject;
        private string IMUPath;

        private GPSClass _gpsObject;
        private string GPSPath;
       
        private List<string> startList;
        private List<string> endList;
        private string ecuipPath;

        //AutoSizeFormClass asc = new AutoSizeFormClass();//进行窗口收放的类实例化
        #endregion

        #region ReadXML_file
        private void readMethod() {
            _locationObject= Program.ReadObject<Location>(setPath.locationPath);
            _weatherObject= Program.ReadObject<Weather>(setPath.weatherPath);
            _carTypeObject= Program.ReadObject<CarType>(setPath.carTypePath);           
        }
        //loaduserdata
        private void loadUserData()
        {
            if( MapPoints.ContainsKey(sysCfg.D_startPosition))
                this.startPosition.Text=sysCfg.D_startPosition;
            if (MapPoints.ContainsKey(sysCfg.D_endPosition))
                this.endPosition.Text=sysCfg.D_endPosition;

            this.checkBox_testinglooproute.Checked = sysCfg.IsTestingLoop;
            this.dayRadio.Checked = sysCfg.D_dayRadio;
            this.nightRadio.Checked = sysCfg.D_nightRadio;

            this.time.SelectedIndex=sysCfg.D_time  ;
            this.sunAngle.Text=sysCfg.D_sunAngle ;

            this.sunRadio.Checked = sysCfg.D_sunRadio;
            this.sunValue.Value = sysCfg.D_sunValue;
            this.sunValue_text.Text = sysCfg.D_sunValue_text;
            this.rainRadio.Checked=sysCfg.D_rainRadio  ;
            this.rainValue.Value=sysCfg.D_rainValue  ;
            this.rainValue_text.Text=sysCfg.D_rainValue_text ;
            this.snowRadio.Checked=sysCfg.D_snowRadio  ;
            this.snowValue.Value=sysCfg.D_snowValue  ;
            this.snowValue_text.Text=sysCfg.D_snowValue_text  ;
            this.fogRadio.Checked=sysCfg.D_fogRadio  ;
            this.fogValue.Value=sysCfg.D_fogValue  ;
            this.fogValue_text.Text=sysCfg.D_fogValue_text  ;

            this.miniCar.Checked = sysCfg.D_miniCar;
            this.mediumCar.Checked = sysCfg.D_mediumCar;
            this.bigCar.Checked = sysCfg.D_bigCar;
            this.speedlimite.SelectedIndex = sysCfg.D_speedlimite;
            this.carcorner.SelectedIndex = sysCfg.D_carcorner;
            this.acceleratorValue.Value = sysCfg.D_acceleratorValue;
            this.acceleratorValue_text.Text = sysCfg.D_acceleratorValue_text;
            this.brakeValue.Value = sysCfg.D_brakeValue;
            this.brakeValue_text.Text = sysCfg.D_brakeValue_text;
            
            this.lidar_equipment_textBox.Text = sysCfg.D_lidar_equipment_textBox;
            this.lidar_equipment_listBox.SelectedIndex = sysCfg.D_lidar_equipment_listBox;
            
            this.lidar_value.Value = sysCfg.D_lidar_value;
            this.lidar_value_text.Text = sysCfg.D_lidar_value_text;
            this.lidar_vertical_range.Text = sysCfg.D_lidar_vertical_range;
            this.lidar_horizontal_range.Text = sysCfg.D_lidar_horizontal_range;
            this.lidar_accuracy.Text = sysCfg.D_lidar_accuracy;
            this.lidar_frequency.SelectedIndex = sysCfg.D_lidar_frequency;
            
            this.radar_equipment_textBox.Text = sysCfg.D_radar_equipment_textBox;
            this.radar_equipment_listBox.SelectedIndex = sysCfg.D_radar_equipment_listBox;

            this.radar_value.Value = sysCfg.D_radar_value ;
            this.radar_value_text.Text = sysCfg.D_radar_value_text ;
            this.radar_accuracy.Text = sysCfg.D_radar_accuracy ;
            this.radar_speed.Text = sysCfg.D_radar_speed ;
            this.radar_range.Text = sysCfg.D_radar_range ;
            
            this.imu_equipment_textBox.Text = sysCfg.D_imu_equipment_textBox;
            this.imu_equipment_listBox.SelectedIndex = sysCfg.D_imu_equipment_listBox;

            this.imu_roll_angle.Text = sysCfg.D_imu_roll_angle ;
            this.imu_pitch_angle.Text = sysCfg.D_imu_pitch_angle ;
            this.imu_heading_angle.Text = sysCfg.D_imu_heading_angle ;
            this.imu_three_axis_acceleration.Text = sysCfg.D_imu_three_axis_acceleration ;
            this.imu_three_axis_angular_velocity.Text = sysCfg.D_imu_three_axis_angular_velocity;
            this.imu_three_axis_geomagnetic.Text = sysCfg.D_imu_three_axis_geomagnetic ;
            
            this.gps_equipment_textBox.Text = sysCfg.D_gps_equipment_textBox;
            this.gps_equipment_listBox.SelectedIndex = sysCfg.D_gps_equipment_listBox;
            
            this.gps_horizontal_positioning_accuracy.Text = sysCfg.D_gps_horizontal_positioning_accuracy ;
            this.gps_time_precision.Text = sysCfg.D_gps_velocity_accuracy ;
            this.gps_velocity_accuracy.Text = sysCfg.D_gps_time_precision ;                        
        }
        //init 初始化gui将上一次选择的信息设置在界面上
        private void Init()
        { 
            this.rainValue_text.Text = this.rainValue.Value.ToString();
            this.snowValue_text.Text = this.snowValue.Value.ToString();
            this.fogValue_text.Text = this.fogValue.Value.ToString();
            this.lidar_value_text.Text = this.lidar_value.Value.ToString();
            this.sunValue_text.Text = this.sunValue.Value.ToString();
            
            time.SelectedIndex = 0;
            this.weatherGroup.Enabled = true;

            SunGroupEnable();
            RainGroupEnable();
            SnowGroupEnable();
            FogGroupEnable();

            this.lidarGroup.Enabled = true;
            this.mediumCar.Checked = false;
            this.bigCar.Checked = false;

            lidar_equipment_textBox.Text = lidar_equipment_listBox.Text;
            radar_equipment_textBox.Text = radar_equipment_listBox.Text;
            imu_equipment_textBox.Text = imu_equipment_listBox.Text;
            gps_equipment_textBox.Text = gps_equipment_listBox.Text;

            acceleratorValue_text.Text = acceleratorValue.Value.ToString();
            brakeValue_text.Text = brakeValue.Value.ToString();

            string logFile = "log";
            SetupLogger("Logs", logFile + "_" + DateTime.Now.ToString("ddMMyyyyHHmmssfffff") + ".txt");
            
            ConsoleLogger.WriteToLog("Finished Form Init", true);
        }
        #endregion

        private void SetupLogger(string logDir, string logFile)
        {
            // Setup Logger
            if (Directory.Exists(logDir))
            {
                ConsoleLogger.InitLogger(logDir + "/" + logFile);
            }
            else
            {
                Directory.CreateDirectory(logDir);
                ConsoleLogger.InitLogger(logDir + "/" + logFile);
            }
        }

        private void SunGroupEnable()
        {
            if (sunRadio.Checked)
            {
                sunGroup.Enabled = true;
            }
            else
            {
                sunGroup.Enabled = false;
            }
        }
        private void RainGroupEnable()
        {
            if (rainRadio.Checked)
            {
                rainGroup.Enabled = true;
            }
            else
            {
                rainGroup.Enabled = false;
            }
        }
        private void SnowGroupEnable()
        {
            if (snowRadio.Checked)
            {
                snowGroup.Enabled = true;
            }
            else
            {
                snowGroup.Enabled = false;
            }
        }
        private void FogGroupEnable()
        {
            if (fogRadio.Checked)
            {
                fogGroup.Enabled = true;
            }
            else
            {
                fogGroup.Enabled = false;
            }
        }
        #region SendMessageToUnity
        private string jsonText;
        private string ip = string.Empty;
        //将xml文件转换成json文件
        private  void XmlChangeToJson(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            jsonText = JsonConvert.SerializeXmlNode(doc);
            Console.WriteLine(jsonText);
        }
        //进行xml文件的解析
        public T DeXMLSerialize<T>(string xmlString)
        {
            T cloneObject = default(T);
            StringBuilder buffer = new StringBuilder();
            buffer.Append(xmlString);

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(buffer.ToString()))
            {
                Object obj = serializer.Deserialize(reader);
                cloneObject = (T)obj;
            }
            return cloneObject;
        }
        //发送数据到unity进行unity初始化操作
        public bool isSending_ALL_CONFIG = false; // jim tba: need state machine
        public void SendMessagetoUnity()
        {           
            XmlChangeToJson(dataPath);
            /*if (SocketClient.GetSocket().Connected)
            {
                SocketClient.GetSocket().Send(Encoding.UTF8.GetBytes(jsonText));
            }*/
            socketClient.SendMessage("<<"+ MessageCatalog.All_CONFIG +">>:"+jsonText);
            isSending_ALL_CONFIG = true;
        }
        private void setOrUpdateXml()
        {
            sysCfg.IsTestingLoop = this.checkBox_testinglooproute.Checked;
            #region
            sysCfg.D_startPosition = this.startPosition.Text;
            if( MapPoints.ContainsKey(this.startPosition.Text))
            {
                if (sysCfg.IsTestingLoop)
                {
                    sysCfg.D_startPosition_x = MapPoints[DictionaryXML.LOOP_DEFAULT].x;
                    sysCfg.D_startPosition_y = MapPoints[DictionaryXML.LOOP_DEFAULT].y;
                    sysCfg.D_startPosition_z = MapPoints[DictionaryXML.LOOP_DEFAULT].z;
                }
                else
                {
                sysCfg.D_startPosition_x = MapPoints[this.startPosition.Text].x;
                sysCfg.D_startPosition_y = MapPoints[this.startPosition.Text].y;
                sysCfg.D_startPosition_z = MapPoints[this.startPosition.Text].z;
                }
            }
            else
            {
                sysCfg.D_startPosition_x = 0.0f;
                sysCfg.D_startPosition_y = 0.0f;
                sysCfg.D_startPosition_z = 0.0f;
            }
            sysCfg.D_endPosition = this.endPosition.Text;
            if (MapPoints.ContainsKey(this.endPosition.Text))
            {
                if (sysCfg.IsTestingLoop)
                {
                    sysCfg.D_endPosition_x = DictionaryXML.LOOP_TAG;
                    sysCfg.D_endPosition_y = DictionaryXML.LOOP_TAG;
                    sysCfg.D_endPosition_z = DictionaryXML.LOOP_TAG;
                }
                else
                {
                sysCfg.D_endPosition_x = MapPoints[this.endPosition.Text].x;
                sysCfg.D_endPosition_y = MapPoints[this.endPosition.Text].y;
                sysCfg.D_endPosition_z = MapPoints[this.endPosition.Text].z;
                }
            }
            else
            {
                sysCfg.D_endPosition_x = 0.0f;
                sysCfg.D_endPosition_y = 0.0f;
                sysCfg.D_endPosition_z = 0.0f;
            }
            //sysCfg.D_selectLanguage = this.selectLanguage.SelectedIndex;
            //sysCfg.D_dayandnight = this.dayandright.SelectedIndex;
            sysCfg.D_dayRadio = this.dayRadio.Checked;
            sysCfg.D_nightRadio = this.nightRadio.Checked;
            sysCfg.D_time = this.time.SelectedIndex;
            sysCfg.D_sunAngle = this.sunAngle.Text;
            sysCfg.D_sunRadio = this.sunRadio.Checked;
            sysCfg.D_sunValue = this.sunValue.Value;
            sysCfg.D_sunValue_text = this.sunValue_text.Text;
            sysCfg.D_rainRadio = this.rainRadio.Checked;
            sysCfg.D_rainValue = this.rainValue.Value;
            sysCfg.D_rainValue_text = this.rainValue_text.Text;
            sysCfg.D_snowRadio = this.snowRadio.Checked;
            sysCfg.D_snowValue = this.snowValue.Value;
            sysCfg.D_snowValue_text = this.snowValue_text.Text;
            sysCfg.D_fogRadio = this.fogRadio.Checked;
            sysCfg.D_fogValue = this.fogValue.Value;
            sysCfg.D_fogValue_text = this.fogValue_text.Text;
            sysCfg.D_miniCar = this.miniCar.Checked;
            sysCfg.D_mediumCar = this.mediumCar.Checked;
            sysCfg.D_bigCar = this.bigCar.Checked;
            sysCfg.D_speedlimite = this.speedlimite.SelectedIndex;
            sysCfg.D_carcorner = this.carcorner.SelectedIndex;
            sysCfg.D_acceleratorValue = this.acceleratorValue.Value;
            sysCfg.D_acceleratorValue_text = this.acceleratorValue_text.Text;
            sysCfg.D_brakeValue = this.brakeValue.Value;
            sysCfg.D_brakeValue_text = this.brakeValue_text.Text;
            //sysCfg.D_lidar_equipment = this.lidar_equipment.SelectedIndex;
            sysCfg.D_lidar_equipment_textBox = this.lidar_equipment_textBox.Text;
            sysCfg.D_lidar_equipment_listBox = this.lidar_equipment_listBox.SelectedIndex;

            sysCfg.D_lidar_value = this.lidar_value.Value;
            sysCfg.D_lidar_value_text = this.lidar_value_text.Text;
            sysCfg.D_lidar_vertical_range = this.lidar_vertical_range.Text;
            sysCfg.D_lidar_horizontal_range = this.lidar_horizontal_range.Text;
            sysCfg.D_lidar_accuracy = this.lidar_accuracy.Text;
            sysCfg.D_lidar_frequency = this.lidar_frequency.SelectedIndex;
            //sysCfg.D_radar_equipment = this.radar_equipment.SelectedIndex;
            sysCfg.D_radar_equipment_textBox = this.radar_equipment_textBox.Text;
            sysCfg.D_radar_equipment_listBox = this.radar_equipment_listBox.SelectedIndex;

            sysCfg.D_radar_value = this.radar_value.Value;
            sysCfg.D_radar_value_text = this.radar_value_text.Text;
            sysCfg.D_radar_accuracy = this.radar_accuracy.Text;
            sysCfg.D_radar_speed = this.radar_speed.Text;
            sysCfg.D_radar_range = this.radar_range.Text;
            //sysCfg.D_imu_equipment = imu_equipment.SelectedIndex;

            sysCfg.D_imu_equipment_listBox = this.imu_equipment_listBox.SelectedIndex;
            sysCfg.D_imu_equipment_textBox = this.imu_equipment_textBox.Text;

            sysCfg.D_imu_roll_angle = imu_roll_angle.Text;
            sysCfg.D_imu_pitch_angle = imu_pitch_angle.Text;
            sysCfg.D_imu_heading_angle = imu_heading_angle.Text;
            sysCfg.D_imu_three_axis_acceleration = imu_three_axis_acceleration.Text;
            sysCfg.D_imu_three_axis_angular_velocity = imu_three_axis_angular_velocity.Text;
            sysCfg.D_imu_three_axis_geomagnetic = imu_three_axis_geomagnetic.Text;
            //sysCfg.D_gps_equipment = gps_equipment.SelectedIndex;
            sysCfg.D_gps_equipment_textBox = gps_equipment_textBox.Text;
            sysCfg.D_gps_equipment_listBox = gps_equipment_listBox.SelectedIndex;

            sysCfg.D_gps_horizontal_positioning_accuracy = gps_horizontal_positioning_accuracy.Text;
            sysCfg.D_gps_velocity_accuracy = gps_time_precision.Text;
            sysCfg.D_gps_time_precision = gps_velocity_accuracy.Text;
            #endregion;
                       
            #region
            _locationObject.startPosition = this.startPosition.SelectedIndex;
            _locationObject.endPosition = this.endPosition.SelectedIndex;

            //_weatherObject.dayandnight = this.dayandright.SelectedIndex;
            _weatherObject.dayRadio = this.dayRadio.Checked;
            _weatherObject.nightRadio = this.nightRadio.Checked;

            _weatherObject.time = this.time.SelectedIndex;
            _weatherObject.sunAngle = this.sunAngle.Text;
            _weatherObject.sunRadio = this.sunRadio.Checked;
            _weatherObject.sunValue = this.sunValue.Value;
            _weatherObject.sunValue_text = this.sunValue_text.Text;
            _weatherObject.runRadio = this.rainRadio.Checked;
            _weatherObject.runValue = this.rainValue.Value;
            _weatherObject.runValue_text = this.rainValue_text.Text;
            _weatherObject.snowRadio = this.snowRadio.Checked;
            _weatherObject.snowValue = this.snowValue.Value;
            _weatherObject.snowValue_text = this.snowValue_text.Text;
            _weatherObject.fogRadio = this.fogRadio.Checked;
            _weatherObject.fogValue = this.fogValue.Value;
            _weatherObject.fogValue_text = this.fogValue_text.Text;
            _carTypeObject.miniCar = this.miniCar.Checked;
            _carTypeObject.mediumCar = this.mediumCar.Checked;
            _carTypeObject.bigCar = this.bigCar.Checked;
            _carTypeObject.speedlimite = this.speedlimite.SelectedIndex;
            _carTypeObject.carcorner = this.carcorner.SelectedIndex;
            _carTypeObject.acceleratorValue = this.acceleratorValue.Value;
            _carTypeObject.acceleratorValue_text = this.acceleratorValue_text.Text;
            _carTypeObject.brakeValue = this.brakeValue.Value;
            _carTypeObject.brakeValue_text = this.brakeValue_text.Text;
            #endregion

            //setLidarObject();
            //setRadarObject();
            //setIMUObject();
            //setGPSObject();
            #region
            //_lidarObject.D_lidar_equipment = this.lidar_equipment.SelectedIndex;

            _lidarObject.D_lidar_equipment_textBox = this.lidar_equipment_textBox.Text;
            _lidarObject.D_lidar_equipment_listBox = this.lidar_equipment_listBox.SelectedIndex;
            
            _lidarObject.D_lidar_value = this.lidar_value.Value;
            _lidarObject.D_lidar_value_text = this.lidar_value_text.Text;
            _lidarObject.D_lidar_vertical_range = this.lidar_vertical_range.Text;
            _lidarObject.D_lidar_horizontal_range = this.lidar_horizontal_range.Text;
            _lidarObject.D_lidar_frequency = this.lidar_frequency.SelectedIndex;
            _lidarObject.D_lidar_accuracy = this.lidar_accuracy.Text;

            //_radarObject.D_radar_equipment = this.radar_equipment.SelectedIndex;
            _radarObject.D_radar_equipment_textBox = this.radar_equipment_textBox.Text;
            _radarObject.D_radar_equipment_listBox = this.radar_equipment_listBox.SelectedIndex;
            
            _radarObject.D_radar_value = this.radar_value.Value;
            _radarObject.D_radar_value_text = this.radar_value_text.Text;
            _radarObject.D_radar_accuracy = this.radar_accuracy.Text;
            _radarObject.D_radar_speed = this.radar_speed.Text;
            _radarObject.D_radar_range = this.radar_range.Text;

            //_imuObject.D_imu_equipment = this.imu_equipment.SelectedIndex;
            _imuObject.D_imu_equipment_textBox = this.imu_equipment_textBox.Text;
            _imuObject.D_imu_equipment_listBox = this.imu_equipment_listBox.SelectedIndex;

            _imuObject.D_imu_roll_angle = this.imu_roll_angle.Text;
            _imuObject.D_imu_pitch_angle = this.imu_pitch_angle.Text;
            _imuObject.D_imu_heading_angle = this.imu_heading_angle.Text;
            _imuObject.D_imu_three_axis_acceleration = this.imu_three_axis_acceleration.Text;
            _imuObject.D_imu_three_axis_angular_velocity = this.imu_three_axis_angular_velocity.Text;
            _imuObject.D_imu_three_axis_geomagnetic = this.imu_three_axis_geomagnetic.Text;

            //_gpsObject.D_gps_equipment = this.gps_equipment.SelectedIndex;
            _gpsObject.D_gps_equipment_listBox = this.gps_equipment_listBox.SelectedIndex;
            _gpsObject.D_gps_equipment_textBox = this.gps_equipment_textBox.Text;    
            
            _gpsObject.D_gps_horizontal_positioning_accuracy = this.gps_horizontal_positioning_accuracy.Text;
            _gpsObject.D_gps_velocity_accuracy = this.gps_time_precision.Text;
            _gpsObject.D_gps_time_precision = this.gps_velocity_accuracy.Text;
            #endregion

            //setTypePath();
            //setLidar();
            //setRadar();
            //setImu();
            //setGps();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            setOrUpdateXml();//保存当前选择的信息到xml文件中，替换上一次保存的信息
            Program.WriteObject<Lidar>(this._lidarObject);
            Program.WriteObject<Radar>(this._radarObject);
            Program.WriteObject<IMUClass>(this._imuObject);
            Program.WriteObject<GPSClass>(this._gpsObject);
            Program.WriteObject<SYS_CFG>(this.sysCfg);
            Program.WriteObject<Location>(this._locationObject);
            Program.WriteObject<CarType>(this._carTypeObject);
            Program.WriteObject<Weather>(this._weatherObject);

            //Program.WriteObject<SetPath>(this._setpath);
            //frm.Show();
            //SocketClient.tryConn();

            SendMessagetoUnity();
        }
        #endregion

        #region ChangeSelectEvent
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if( MapPoints.ContainsKey(startPosition.Text) )
            {
                string path = SYSTEM_PATH + MapPoints[startPosition.Text].image_path;
                if( path != null && File.Exists(path))
                {
                    pictureBox1.Image = Image.FromFile(path);
                }
            }
            /*endPosition.Items.Clear();
            foreach (var item in endList)
            {
                if (item != startPosition.Text)
                {
                    endPosition.Items.Add(item);
                }
            }*/
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MapPoints.ContainsKey(endPosition.Text))
            {
                string path = SYSTEM_PATH + MapPoints[endPosition.Text].image_path;
                if (path != null && File.Exists(path))
                {
                    pictureBox2.Image = Image.FromFile(path);
                }
            }
            /*startPosition.Items.Clear();
            foreach (var item in startList)
            {                
                    startPosition.Items.Add(item);                
            }*/
        }
        private void time_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_sunAngle();            
        }
        private void Update_sunAngle() {
            if (int.Parse(time.Text) >= 6 && int.Parse(time.Text) <= 18)
            {
                sunAngle.Text = ((90 / 6) * (int.Parse(time.Text) - 6)).ToString();
            }
            else
            {
                sunAngle.Text = "0";
            }
        }

        private void sunRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (sunRadio.Checked)
            {
                sunGroup.Enabled = true;
            }
            else
            {
                sunGroup.Enabled = false;
            }
        }
        private void rainRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (rainRadio.Checked)
            {
                rainGroup.Enabled = true;
            }
            else
            {
                rainGroup.Enabled = false;
            }
        }
        private void snowRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (snowRadio.Checked)
            {
                snowGroup.Enabled = true;
            }
            else
            {
                snowGroup.Enabled = false;
            }
        }
        private void fogRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (fogRadio.Checked)
            {
                fogGroup.Enabled = true;
            }
            else
            {
                fogGroup.Enabled = false;
            }
        }        
        private void lidarValue_Scroll(object sender, ScrollEventArgs e)
        {
            lidar_value_text.Text = lidar_value.Value.ToString();
        }
        private void runValue_Scroll(object sender, ScrollEventArgs e)
        {
            rainValue_text.Text = rainValue.Value.ToString();
        }
        private void snowValue_Scroll(object sender, ScrollEventArgs e)
        {
            snowValue_text.Text = snowValue.Value.ToString();
        }
        private void fogValue_Scroll(object sender, ScrollEventArgs e)
        {
            fogValue_text.Text = fogValue.Value.ToString();
        }                
        private void lidar_speed_Scroll(object sender, ScrollEventArgs e)
        {
            //lidar_speed_text.Text = lidar_speed.Value.ToString();
        }
        private void lidar_Resolving_power_Scroll(object sender, ScrollEventArgs e)
        {
            //lidar_Resolving_power_text.Text = lidar_Resolving_power.Value.ToString();
        }
        private void radar_value_Scroll(object sender, ScrollEventArgs e)
        {
            radar_value_text.Text = radar_value.Value.ToString();
        }
        #endregion

        #region SelectEquipment        
        private void lidarLoad(Lidar _lidar)
        {
            this.lidar_value.Value = _lidar.D_lidar_value;
            this.lidar_value_text.Text = _lidar.D_lidar_value_text;
            this.lidar_vertical_range.Text = _lidar.D_lidar_vertical_range;
            this.lidar_horizontal_range.Text = _lidar.D_lidar_horizontal_range;
            this.lidar_accuracy.Text = _lidar.D_lidar_accuracy;
        }    
        private void radarLoad(Radar _radar)
        {
            this.radar_value.Value = _radar.D_radar_value;
            this.radar_value_text.Text = _radar.D_radar_value_text;
            this.radar_accuracy.Text = _radar.D_radar_accuracy;
            this.radar_speed.Text = _radar.D_radar_speed;
            this.radar_range.Text = _radar.D_radar_range;
        }            
        private void IMUClassLoad(IMUClass _imuclass)
        {
            imu_roll_angle.Text = _imuclass.D_imu_roll_angle;
            imu_pitch_angle.Text = _imuclass.D_imu_pitch_angle;
            imu_heading_angle.Text = _imuclass.D_imu_heading_angle;
            imu_three_axis_acceleration.Text = _imuclass.D_imu_three_axis_acceleration;
            imu_three_axis_angular_velocity.Text = _imuclass.D_imu_three_axis_angular_velocity;
            imu_three_axis_geomagnetic.Text = _imuclass.D_imu_three_axis_geomagnetic;
        }        
        private void GPSClassLoad(GPSClass _gpsclass)
        {
            gps_horizontal_positioning_accuracy.Text = _gpsclass.D_gps_horizontal_positioning_accuracy;
            gps_velocity_accuracy.Text = _gpsclass.D_gps_velocity_accuracy;
            gps_time_precision.Text = _gpsclass.D_gps_time_precision;
        }
       
        private object[] day = new object[] {6,7,8,9,10,11,12,13,14,15,16,17,18 };
        private object[] night = new object[] { 19, 20, 21, 22, 23, 24, 1, 2, 3, 4, 5 };

        private void miniCar_CheckedChanged(object sender, EventArgs e)
        {
            if (miniCar.Checked)
            {
                CarGroup.Enabled = true;
                mediumCar.Checked = false;
                bigCar.Checked = false;
            }
            else
            {
                CarGroup.Enabled = false;
            }
        }
        private void mediumCar_CheckedChanged(object sender, EventArgs e)
        {
            if (mediumCar.Checked)
            {
                CarGroup.Enabled = true;
                miniCar.Checked = false;
                bigCar.Checked = false;
            }
            else
            {
                CarGroup.Enabled = false;
            }
        }
        private void bigCar_CheckedChanged(object sender, EventArgs e)
        {
            if (bigCar.Checked)
            {
                CarGroup.Enabled = true;
                miniCar.Checked = false;
                mediumCar.Checked = false;
            }
            else
            {
                CarGroup.Enabled = false;
            }
        }
        #endregion

        #region MouseHover_Display
        private void startPosition_MouseHover(object sender, EventArgs e)
        {            
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Began to address";
            }
            else
            {
                Reminder.Text = "开始地址";
            }
        }
        private void endPosition_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Terminate the address";
            }
            else
            {
                Reminder.Text = "终止地址";
            }
        }
        private void selectLanguage_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choose which language to use in the current system";
            }
            else
            {
                Reminder.Text = "在当前系统中选择哪种语言";
            }
        }
        private void dayandright_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "The choice of day and night";
            }
            else
            {
                Reminder.Text = "白天和夜晚的选择";
            }
        }
        private void time_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "select time";
            }
            else
            {
                Reminder.Text = "时间选择";
            }
        }
        private void sunAngle_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "The sun Angle automatically makes modifications based on time";
            }
            else
            {
                Reminder.Text = "太阳角度根据时间选择自动做出修改";
            }
        }
        private void sunRadio_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choose a sunny day";
            }
            else
            {
                Reminder.Text = "选择晴天";
            }
        }
        private void sunValue_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Sunshine degree";
            }
            else
            {
                Reminder.Text = "晴天程度值";
            }
        }
        private void rainValue_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Rainy day value";
            }
            else
            {
                Reminder.Text = "雨天程度值";
            }
        }
        private void rainRadio_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choose a rainy day";
            }
            else
            {
                Reminder.Text = "选择雨天";
            }
        }
        private void snowRadio_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Select the snow";
            }
            else
            {
                Reminder.Text = "选择下雪";
            }
        }
        private void snowValue_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Snows selection";
            }
            else
            {
                Reminder.Text = "下雪程度选择";
            }
        }
        private void fogRadio_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Fog choice";
            }
            else
            {
                Reminder.Text = "雾天选择";
            }
        }

        private void fogValue_MouseHover(object sender, EventArgs e)
        {
            //Reminder.Text = GetTextFromLib(34);
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choice of foggy level";
            }
            else
            {
                Reminder.Text = "雾天程度选择";
            }
        }
        private void miniCar_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choose a small car";
            }
            else
            {
                Reminder.Text = "选择小型汽车";
            }
        }
        private void mediumCar_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Choose medium car";
            }
            else
            {
                Reminder.Text = "选择中型汽车";
            }
        }
        private void bigCar_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Select large vehicles";
            }
            else
            {
                Reminder.Text = "选择大型汽车";
            }
        }
        private void speedlimite_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Selective speed limit";
            }
            else
            {
                Reminder.Text = "选择车速限制";
            }
        }
        private void carcorner_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Select the steering wheel Angle";
            }
            else
            {
                Reminder.Text = "选择方向盘转角";
            }
        }
        private void acceleratorValue_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Select the throttle size";
            }
            else
            {
                Reminder.Text = "选择油门大小";
            }
        }
        private void brakeValue_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Selective brake size";
            }
            else
            {
                Reminder.Text = "选择刹车大小";
            }
        }

        private void gps_equipment_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "GPS Equipment selection";
            }
            else
            {
                Reminder.Text = "GPS设备选择";
            }
        }
        private void gps_horizontal_positioning_accuracy_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "GPS Horizontal positioning accuracy selection";
            }
            else
            {
                Reminder.Text = "GPS 水平定位精度选择";
            }
        }
        private void gps_velocity_accuracy_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "GPS Speed accuracy selection";
            }
            else
            {
                Reminder.Text = "GPS 速度精度选择";
            }
        }
        private void gps_time_precision_TextChanged(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "GPS Time accuracy selection";
            }
            else
            {
                Reminder.Text = "GPS 时间精度选择";
            }
        }
        private void lidar_equipment_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Equipment selection";
            }
            else
            {
                Reminder.Text = "Lidar设备选择";
            }
        }
        private void lidar_value_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Gamma range setting";
            }
            else
            {
                Reminder.Text = "Lidar 射线范围设置";
            }
        }
        private void lidar_frequency_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Radar frequency";
            }
            else
            {
                Reminder.Text = "Lidar 雷达的频率";
            }
        }
        private void lidar_accuracy_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Accuracy of choice";
            }
            else
            {
                Reminder.Text = "Lidar 的精度选择";
            }
        }
        private void lidar_vertical_range_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Vertical field selection";
            }
            else
            {
                Reminder.Text = "Lidar 垂直视野范围选择";
            }
        }
        private void lidar_horizontal_range_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar Horizontal field of vision selection";
            }
            else
            {
                Reminder.Text = "Lidar 水平视野范围选择";
            }
        }
        private void lidar_value_text_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Lidar The text that is displayed after the ray range selection";
            }
            else
            {
                Reminder.Text = "Lidar 射线范围选择之后显示的文本";
            }
        }
        private void radar_equipment_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Radar Equipment selection";
            }
            else
            {
                Reminder.Text = "Radar 设备选择";
            }
        }
        private void radar_value_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Radar Ray range selection";
            }
            else
            {
                Reminder.Text = "Radar 射线范围选择";
            }
        }
        private void radar_range_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Radar Select the upper and lower Angle range";
            }
            else
            {
                Reminder.Text = "Radar 上下夹角范围选择";
            }
        }
        private void radar_speed_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Radar Frequency selection";
            }
            else
            {
                Reminder.Text = "Radar 频率的选择";
            }
        }
        private void radar_accuracy_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "Radar Choice of error";
            }
            else
            {
                Reminder.Text = "Radar 误差的选择";
            }
        }
        private void imu_equipment_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU Equipment selection";
            }
            else
            {
                Reminder.Text = "IMU 设备的选择";
            }
        }
        private void imu_roll_angle_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU The choice of roll Angle";
            }
            else
            {
                Reminder.Text = "IMU 横滚角的选择";
            }
        }
        private void imu_pitch_angle_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU The choice of pitch Angle";
            }
            else
            {
                Reminder.Text = "IMU 俯仰角的选择";
            }
        }
        private void imu_heading_angle_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU Choice of heading Angle";
            }
            else
            {
                Reminder.Text = "IMU 航向角的选择";
            }
        }
        private void imu_three_axis_acceleration_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU Three axis acceleration choices";
            }
            else
            {
                Reminder.Text = "IMU 三轴加速度选择";
            }
        }
        private void imu_three_axis_angular_velocity_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU Choice of triaxial angular velocity";
            }
            else
            {
                Reminder.Text = "IMU 三轴角速度选择";
            }
        }
        private void imu_three_axis_geomagnetic_MouseHover(object sender, EventArgs e)
        {
            if (LanguageText.isEnglish)
            {
                Reminder.Text = "IMU Selection of triaxial magnetic field";
            }
            else
            {
                Reminder.Text = "IMU 三轴地磁的选择";
            }
        }
        #endregion

        SocketClient socketClient;
        public FormMain(SYS_CFG sysCfg, SetPath setPath)
        {
            this.sysCfg = sysCfg;
            this.setPath = setPath;
            startList = new List<string>() ;
            endList = new List<string>() ;
            SYSTEM_PATH = Application.StartupPath;
            dataPath = SYSTEM_PATH + "\\usrdata.dcf";
            ecuipPath = SYSTEM_PATH + "\\ECUip.dcf";           

            ReadStartEndPosition(setPath.StartEndPosition);
            InitializeComponent();

            ReadECUip(ecuipPath);
            //TabSet();//reset tabControl
            // Generate Init points
            //Utility.DictionaryXML.Create();

            // Read MapPoints to Dictionary
            bool isRead = Utility.DictionaryXML.Read(MapPoints);
            if(isRead == false || MapPoints.Count < 2 || 
                MapPoints.ContainsKey(DictionaryXML.LOOP_DEFAULT) == false)
            {
                string message = "Must have a valid Map Points File. System will auto generate a default one. Please restart this application!";
                string caption = "Error Detected";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, caption, buttons);
                Utility.DictionaryXML.Create();
                return;
            }
            // Show name of points on GUI
            this.startPosition.Items.AddRange(new List<string>(MapPoints.Keys).ToArray());
            this.endPosition.Items.AddRange(new List<string>(MapPoints.Keys).ToArray());

            readMethod();
            loadUserData();
            Init();

            insertMessageDelegate = new DelegateInsertMessage(InsertMessage);
            InsertMessage(MessageBoxIcon.Information, "test");
            controlButtonRunDelegate = new DelegateControlButtonRun(ControlButtonRun);
                        
            socketClient = new SocketClient(this);
                       
            this.textBox_ipaddress.Text = GetIP4Address().ToString();
            this.textBox_port.Text = "13010";            
        }

        public void CreateECUipFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);            
            sw.Write("192.168.31.92|4567");            
            sw.Flush();            
            sw.Close();
            fs.Close();
        }

        public void ReadECUip(string path)
        {            
            if (!File.Exists(path))
            {
                CreateECUipFile(path);
            }            
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string pointMessage;

            while ((pointMessage = sr.ReadLine()) != null)
            {
                string[] str = pointMessage.Split('|');

                textBox_ecuipaddress.Text = str[0];
                textBox_ecuport.Text = str[1];
                textBox_lidarip.Text = str[0];
            }

        }

        public void ReadStartEndPosition(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string pointMessage;
            while ((pointMessage = sr.ReadLine()) != null)
            {                
                string[] str = pointMessage.Split('|');
                for (int i = 0; i < str.Length; i++)
                {
                    startList.Add(str[i]);
                    endList.Add(str[i]);
                }               
            }
        }

        public static IPAddress GetIP4Address()
        {
            IPAddress IP4Address = IPAddress.Loopback;
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily == AddressFamily.InterNetwork)
                {
                    IP4Address = IPA;
                    break;
                }
            }
            return IP4Address;
        }

        private void Set_software_Click(object sender, EventArgs e)
        {
            //form2 = new Form2(this);
            //form2.Show();            
        }

        #region UpdateAndChangeSize
        public void SetLanguage()
        {
            label1.Text = LanguageText.label1;
            label2.Text = LanguageText.label2;
            button_loadallconfig.Text = LanguageText.Start;

            groupBox4.Text = LanguageText.groupBox4;
            weatherGroup.Text = LanguageText.weatherGroup;
            label20.Text = LanguageText.label20;
            label21.Text = LanguageText.label21;
            sunRadio.Text = LanguageText.sunRadio;
            sunGroup.Text = LanguageText.sunGroup;
            label55.Text = LanguageText.label55;
            label58.Text = LanguageText.label58;

            label59.Text = LanguageText.label59;
            rainRadio.Text = LanguageText.rainRadio;
            rainGroup.Text = LanguageText.rainGroup;
            label3.Text = LanguageText.label3;
            label9.Text = LanguageText.label9;
            label4.Text = LanguageText.label4;
            snowRadio.Text = LanguageText.snowRadio;
            snowGroup.Text = LanguageText.snowGroup;

            label5.Text = LanguageText.label5;
            label10.Text = LanguageText.label10;
            label6.Text = LanguageText.label6;
            fogRadio.Text = LanguageText.fogRadio;
            fogGroup.Text = LanguageText.fogGroup;
            label7.Text = LanguageText.label7;
            label11.Text = LanguageText.label11;
            label8.Text = LanguageText.label8;

            groupBox1.Text = LanguageText.groupBox1;
            miniCar.Text = LanguageText.miniCar;
            mediumCar.Text = LanguageText.mediumCar;
            bigCar.Text = LanguageText.bigCar;
            CarGroup.Text = LanguageText.CarGroup;
            label40.Text = LanguageText.label40;
            label41.Text = LanguageText.label41;
            label42.Text = LanguageText.label42;

            groupBox2.Text = LanguageText.groupBox2;
            label45.Text = LanguageText.label45;
            label46.Text = LanguageText.label46;
            label47.Text = LanguageText.label47;
            label43.Text = LanguageText.label43;
            groupBox3.Text = LanguageText.groupBox3;
            label48.Text = LanguageText.label48;
            label51.Text = LanguageText.label51;

            label52.Text = LanguageText.label52;
            label32.Text = LanguageText.label32;
            lidarGroup.Text = LanguageText.lidarGroup;
            label25.Text = LanguageText.label25;
            
            label16.Text = LanguageText.label16;
            label30.Text = LanguageText.label30;
            label17.Text = LanguageText.label17;
            label22.Text = LanguageText.label22;

            label15.Text = LanguageText.label15;
            radarGroup.Text = LanguageText.radarGroup;
            label44.Text = LanguageText.label44;
            label14.Text = LanguageText.label14;
            label19.Text = LanguageText.label19;
            label18.Text = LanguageText.label18;
            label56.Text = LanguageText.label56;
            imuGroup.Text = LanguageText.imuGroup;

            label23.Text = LanguageText.label23;
            label57.Text = LanguageText.label57;
            label37.Text = LanguageText.label37;
            label29.Text = LanguageText.label29;
            label28.Text = LanguageText.label28;
            label31.Text = LanguageText.label31;
            label53.Text = LanguageText.label53;
            gpsGroup.Text = LanguageText.gpsGroup;

            label54.Text = LanguageText.label54;
            label38.Text = LanguageText.label38;
            label39.Text = LanguageText.label39;
        }
                
        private void Form1_Load(object sender, EventArgs e)
        {/*
            asc.controllInitializeSize(this);

            //设置DrawMode 为 OwnerDrawFixed 可以再可视化编辑里设置
            this.tabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;

            //将tabcontrol的drawitem 重写 交给自己写的DrawItem方法
            //this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl2_DrawItem);

            this.SuspendLayout();
            this.tabControl2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabLeft_DrawItem);
            this.ResumeLayout(false);
            */
        }
        //private void tabControl2_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        //{

        //    Font fntTab;
        //    Brush bshBack;
        //    Brush bshFore;
        //    if (e.Index == this.tabControl2.SelectedIndex)
        //    {
        //        fntTab = new Font(e.Font, FontStyle.Bold);
        //        bshBack = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, SystemColors.Control, SystemColors.Control, System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
        //        bshBack = new SolidBrush(Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(151)))), ((int)(((byte)(248))))));
        //        bshFore = Brushes.White;
        //    }
        //    else
        //    {
        //        fntTab = e.Font;
        //        bshBack = new SolidBrush(Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200))))));
        //        bshFore = new SolidBrush(Color.Black);
        //    }
        //    string tabName = tabControl2.TabPages[e.Index].Text;
        //    StringFormat sftTab = new StringFormat();
        //    e.Graphics.FillRectangle(bshBack, e.Bounds);
        //    Rectangle recTab = e.Bounds;
        //    recTab = new Rectangle(recTab.X, recTab.Y + 4, recTab.Width, recTab.Height - 4);
        //    e.Graphics.DrawString(tabName, fntTab, bshFore, recTab, sftTab);
          
        //}

        /*private void Form1_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }*/

        #endregion

        #endregion

        private void ControlButtonRun(bool isEnable)
        {
            this.button_run.Enabled = isEnable;
        }

        private void InsertMessage(MessageBoxIcon type, string msg)
        {
            if (msg == null)
                msg = "";

            System.Windows.Forms.ListViewItem listViewItem1 = null;

            if (type == MessageBoxIcon.Information)
            {
                listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "INFORMATION",
            msg}, -1, System.Drawing.Color.Black, System.Drawing.Color.Empty, null);

            }
            else if (type == MessageBoxIcon.Error)
            {
                listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "ERROR",
            msg}, -1, System.Drawing.Color.Red, System.Drawing.Color.Empty, null);

            }
            else if (type == MessageBoxIcon.Warning)
            {
                listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "WARNING",
            msg}, -1, System.Drawing.Color.Orange, System.Drawing.Color.Empty, null);

            }
            else
            {
                listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "",
            msg}, -1, System.Drawing.Color.Black, System.Drawing.Color.Empty, null);

            }
            if (listViewItem1 != null)
                this.listView_message.Items.Insert(0, listViewItem1);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lidar_equipment_textBox.Text = lidar_equipment_listBox.Text;
            switch (lidar_equipment_listBox.Text)
            {
                case "Velodyne HDL-32e":
                    LidarPath = setPath.lidar_Velodyne_HDL_32e_Path;
                    break;
                case "Velodyne VLP-16":
                    LidarPath = setPath.lidar_Velodyne_VLP_16_Path;
                    break;
                case "Hokuyo TOP-URG":
                    LidarPath = setPath.lidar_Hokuyo_TOP_URG_Path;
                    break;
                case "Hokuyo 3D-URG":
                    LidarPath = setPath.lidar_Hokuyo_3D_URG_Path;
                    break;
                case "SICK LMS511":
                    LidarPath = setPath.lidar_SICK_LMS511_Path;
                    break;
                case "IBEO 8L Single":
                    LidarPath = setPath.lidar_IBEO_8L_Single_Path;
                    break;
                case "NULL":
                    LidarPath = null;
                    break;
                default:
                    break;
            }
            if (LidarPath == "NULL")
            {
                lidarGroup.Enabled = false;
                return;
            }
            else
            {
                lidarGroup.Enabled = true;
                _lidarObject = Program.ReadObject<Lidar>(LidarPath);
                lidarLoad(_lidarObject);
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            radar_equipment_textBox.Text = radar_equipment_listBox.Text;
            switch (radar_equipment_listBox.Text)
            {
                case "Vehicular":
                    RadarPath = setPath.radar_Vehicular_Path;
                    break;
                case "Delphi RADAR":
                    RadarPath = setPath.radar_Delphi_RADAR_Path;
                    break;
                case "Accessories":
                    RadarPath = setPath.radar_Accessories_Path;
                    break;
                case "Smartmicro RADAR":
                    RadarPath = setPath.radar_Smartmicro_RADAR_Path;
                    break;
                case "NULL":
                    RadarPath  = null;
                    break;
                default:
                    break;
            }
            if (RadarPath == "NULL")
            {
                this.radarGroup.Enabled = false;
                return;
            }            
            else
            {
                this.radarGroup.Enabled = true;
                _radarObject = Program.ReadObject<Radar>(RadarPath);
                radarLoad(_radarObject);
            }

        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            imu_equipment_textBox.Text = imu_equipment_listBox.Text;
            switch (imu_equipment_listBox.Text)
            {
                case "Memsic_VG440":
                    IMUPath = setPath.imu_Memsic_VG440_Path;
                    break;
                case "Xsens_MTi_300":
                    IMUPath = setPath.imu_Xsens_MTi_300_Path;
                    break;
                case "MicroStrain_3DM_GX5_15":
                    IMUPath = setPath.imu_MicroStrain_3DM_GX5_15_Path;
                    break;
                case "NULL":
                    IMUPath = null;
                    break;
                default:
                    break;
            }
            if (IMUPath == "NULL")
            {
                this.imuGroup.Enabled = false;
                return;
            }            
            else
            {
                this.imuGroup.Enabled = true;
                _imuObject = Program.ReadObject<IMUClass>(IMUPath);
                IMUClassLoad(_imuObject);
            }
        }
        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            gps_equipment_textBox.Text = gps_equipment_listBox.Text;
            switch (gps_equipment_listBox.Text)
            {
                case "Javad_Delta_3(TTY1)":
                    GPSPath = setPath.gps_Javad_Delta_3_Path;
                    break;
                case "Garmin_GPS_18x_LVC":
                    GPSPath = setPath.gps_Garmin_GPS_18x_LVC_Path;
                    break;
                case "Serial_GNSS":
                    GPSPath = setPath.gps_Serial_GNSS_Path;
                    break;
                case "NULL":
                    GPSPath = null;
                    break;
                default:
                    break;
            }
            if (gps_equipment_listBox.Text == "NULL")
            {
                this.gpsGroup.Enabled = false;
                return;
            }
            else
            {
                this.gpsGroup.Enabled = true;
                _gpsObject = Program.ReadObject<GPSClass>(GPSPath);
                GPSClassLoad(_gpsObject);
            }
        }


        //选择白天触发的操作
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = this.time.Items.Count - 1; i >= 0; i--)
            {
                this.time.Items.RemoveAt(i);
            }
            this.time.Items.AddRange(day);
            this.time.SelectedIndex = 0;
            Update_sunAngle();
        }
        //选择黑夜触发的操作
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = this.time.Items.Count - 1; i >= 0; i--)
            {
                this.time.Items.RemoveAt(i);
            }
            this.time.Items.AddRange(night);
            this.time.SelectedIndex = 0;
            Update_sunAngle();
        }

        private void lidar_equipment_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void acceleratorValue_Scroll(object sender, ScrollEventArgs e)
        {
            acceleratorValue_text.Text = acceleratorValue.Value.ToString();
        }

        private void brakeValue_Scroll(object sender, ScrollEventArgs e)
        {
            brakeValue_text.Text = brakeValue.Value.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
            //Dispose();
        }

        Dictionary<string, MapPositionDictionary.Vector3> MapPoints = new Dictionary<string, MapPositionDictionary.Vector3>();

        private void button_connect_Click(object sender, EventArgs e)
        {            
            this.button_connect.Enabled = false;
            this.button_disconnect.Enabled = true;
            this.textBox_ipaddress.Enabled = false;
            this.textBox_port.Enabled = false;
            this.button_ecuconnect.Enabled = true;
            this.button_run.Enabled = true;
            this.button_loadallconfig.Enabled = true;
            this.button_routeplan.Enabled = true;
            this.button_trigger.Enabled = true;

            this.groupBox_sendrequest.Enabled = true;

            int port = 8888;
            try
            {
                port = int.Parse(this.textBox_port.Text);
            }
            catch(Exception)
            {
                port = 8888; // jim tba: report error
            }
            //Console.WriteLine(this.textBox_ipaddress.Text);
            socketClient.Start(this.textBox_ipaddress.Text, port);
            
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void button_request_Click(object sender, EventArgs e)
        {
            socketClient.SendMessage(this.textBox_sendtestingmsg.Text);
        }

        /*delegate void ControlButtonRunDelegate(bool ie);
        public void ContorlButtonRun(bool isEnable)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ControlButtonRunDelegate(ContorlButtonRun));
            }
            else
            {
                this.button_run.Enabled = isEnable;
            }
        }*/

        delegate void DisconnectCallback();
        public void Disconnect()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new DisconnectCallback(Disconnect));
            }
            else
            {
                this.button_connect.Enabled = true;
                this.button_disconnect.Enabled = false;
                this.textBox_ipaddress.Enabled = true;
                this.textBox_port.Enabled = true;
                this.groupBox_sendrequest.Enabled = false;
                this.button_ecuconnect.Enabled = false;
                this.button_trigger.Enabled = false;
                this.button_run.Enabled = false;
                this.button_loadallconfig.Enabled = false;
                this.button_routeplan.Enabled = false;

                socketClient.Disconnect();

                FormReset();
            }
        }

        private void FormReset()
        {
            isSending_CAR_RUN = false;
            IsArrived = false;
            IsError = false;
        }

        public bool isSending_CAR_RUN = false;
        private void button_ignon_Click(object sender, EventArgs e)
        {
            socketClient.SendMessage("<<" + MessageCatalog.CAR_RUN + ">>:ON");

            isSending_CAR_RUN = true;

            IsError = false;

            BackgroundWorker checkStatus = new BackgroundWorker();
            checkStatus.DoWork += new DoWorkEventHandler(CheckStatus_DoWork);
            checkStatus.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CheckState_Completed);
            IsArrived = false;
            checkStatus.RunWorkerAsync("<<" + MessageCatalog.CHECK_STATUS + ">>:ON");
        }
        public bool _isArrived;
        public bool IsArrived
        {
            set
            {
                lock(this)
                {
                    this._isArrived = value;
                }
            }
            get
            {
                return _isArrived;
            }
        }
        public bool _isError;
        public bool IsError
        {
            set
            {
                lock (this)
                {
                    this._isError = value;
                }
            }
            get
            {
                return _isError;
            }
        }

        void CheckStatus_DoWork(object sender, DoWorkEventArgs e)
        {
            // Extract the argument.
            string msg = (string)e.Argument;
            while (!IsArrived && !IsError)
            {
                socketClient.SendMessage(msg);
                Thread.Sleep(1000);
            }
        }
        void CheckState_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            Invoke(insertMessageDelegate, new object[] { MessageBoxIcon.Information, "Finished RUN" });
        }

        private void button_ecuconnect_Click(object sender, EventArgs e)
        {
            string[] ecu = this.textBox_ecuipaddress.Text.Split('.');
            string[] sim = this.textBox_ipaddress.Text.Split('.');

            bool isSameNet = false;
            try
            {
                if (ecu[0] == sim[0] && ecu[1] == sim[1] && ecu[2] == sim[2])
                    isSameNet = true;
            }
            catch
            {
                isSameNet = false;
            }
            if(!isSameNet)
            {
                if (DialogResult.OK == MessageBox.Show(
                    "Please confirm IP address are correct. Click \"OK\" to continue posting.\nClick \"Cancel\" to modify the IP.",
                    "Network Confirmation",
                     MessageBoxButtons.OKCancel,
                     MessageBoxIcon.Warning,
                     MessageBoxDefaultButton.Button1))
                {
                    isSameNet = true;
                }
            }

            if (isSameNet)
            {
                socketClient.SendMessage("<<" + MessageCatalog.ECU_IP_PORT + ">>:" +
                    this.textBox_ecuipaddress.Text + ":" +
                    this.textBox_ecuport.Text + ":" +
                    this.textBox_lidarip.Text + ":" +
                    this.textBox_lidarport.Text);
            }
        }

        private void button_routeplan_Click(object sender, EventArgs e)
        {
            int startId = 0;
            int endId = 0;
            if (this.checkBox_testinglooproute.Checked == true)
            {
                Utility.MapPositionDictionary.Vector3 startEndPosition = MapPoints[DictionaryXML.LOOP_DEFAULT];
                sysCfg.D_startPosition_x = startEndPosition.x;
                sysCfg.D_startPosition_y = startEndPosition.y;
                sysCfg.D_startPosition_z = startEndPosition.z;
                sysCfg.D_endPosition_x = DictionaryXML.LOOP_TAG;
                sysCfg.D_endPosition_y = DictionaryXML.LOOP_TAG;
                sysCfg.D_endPosition_z = DictionaryXML.LOOP_TAG;
            }
            else
            {
            sysCfg.D_startPosition = this.startPosition.Text;
            sysCfg.D_endPosition = this.endPosition.Text;

                Utility.MapPositionDictionary.Vector3 startP = MapPoints[sysCfg.D_startPosition];
                sysCfg.D_startPosition_x = startP.x;
                sysCfg.D_startPosition_y = startP.y;
                sysCfg.D_startPosition_z = startP.z;
                startId = startP.id;
                Utility.MapPositionDictionary.Vector3 endP = MapPoints[sysCfg.D_endPosition];
                sysCfg.D_endPosition_x = endP.x;
                sysCfg.D_endPosition_y = endP.y;
                sysCfg.D_endPosition_z = endP.z;
                endId = endP.id;
            }
            sysCfg.IsTestingLoop = this.checkBox_testinglooproute.Checked;
            socketClient.SendMessage("<<" + MessageCatalog.NEW_ROUTE + ">>:" +
                sysCfg.D_startPosition_x +"," + sysCfg.D_startPosition_y + "," +
                sysCfg.D_startPosition_z + ":" +
                sysCfg.D_endPosition_x + "," + sysCfg.D_endPosition_y + "," +
                sysCfg.D_endPosition_z + ":" +
                startId + "-" + endId);
        }

        private void textBox_ecuipaddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (languageChange.Text == "English")
            {
                LanguageText.isEnglish = true;                
            }
            else
            {
                LanguageText.isEnglish = false;
            }
            SetLanguage();
        }

        private void button_trigger_Click(object sender, EventArgs e)
        {
            socketClient.SendMessage("<<" + MessageCatalog.DATA_COLLECT + ">>:" +
                "A/M" + ":" +
                "vehicle");
        }

        private void button_resetselfip_Click(object sender, EventArgs e)
        {
            this.textBox_ipaddress.Text = GetIP4Address().ToString();
            this.textBox_port.Text = "13010";
        }

        private void comboBox_ecutype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*delegate void SetMessageCallback(string message);
        public void SetMessage(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SetMessageCallback(SetMessage), message);
            }
            else
            {
                ListBoxClientInformations.Items.Add(message);
            }
        }*/
    }
}
