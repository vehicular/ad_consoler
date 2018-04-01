using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Xml;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

namespace vehicular_simulation
{
     public class Program
    {
        #region InitProgram
        private static string SYSTEM_PATH = "";
        private static string Path = "";
        public static bool EXE_isOpen = false;
        private static SYS_CFG _sysCfg;
        private static SetPath _setPath;
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {           
            try
            {
                JudgeUnityIsOpen();
                Process process = new Process();
                //process.StartInfo.FileName = "C:/Users/Administrator/Desktop/cube.exe";
                //EXE_isOpen= process.Start();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            SYSTEM_PATH = Application.StartupPath;
            Path = SYSTEM_PATH + "/data/";            
            try
            {
                _sysCfg = ReadObject<SYS_CFG>();
            }
            catch (Exception ex)
            {
                CreateSysCfg();
                Console.WriteLine(ex.ToString());                
            }
            //if (IsSys_CfgNull==true)
            //{
            //    _sysCfg = null;
            //}
            try
            {
                _setPath = ReadObject<SetPath>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);           
            Application.Run(new FormMain(_sysCfg, _setPath));         
            //WriteSysCfg();              
        }       
        //判断unity是否已经开启了
        private static void JudgeUnityIsOpen()
        {
            Process[] proArraylist = Process.GetProcesses();
            foreach (var item in proArraylist)
            {
                if (item.ProcessName == "cube")
                {
                    item.Kill();
                }                
            }
        }

        #region ReadAndWriteFile
        //创建xml文件
        private static void CreateSysCfg()
        {
            //if (File.Exists(SYSTEM_PATH + "\\vehicular.ini"))
            //{
            //    return;
            //}
            XmlSerializer db = new XmlSerializer(typeof(SYS_CFG));
            TextWriter dbWriter = new StreamWriter(SYSTEM_PATH + "\\vehicular.ini");
            _sysCfg = new SYS_CFG();
            //_sysCfg.D_acceleratorValue = 0;
            _sysCfg.lastDataPath = SYSTEM_PATH + @"\vehicular\UserData";
            //sysCfg.dataPath = SYSTEM_PATH + "\\vehicular.ini";
            db.Serialize(dbWriter, _sysCfg);
            dbWriter.Close();
        }        
        private static  FileStream fs;
        public static T ReadObject<T>()
        {
            XmlSerializer sys = new XmlSerializer(typeof(T));
            switch (typeof(T).ToString())
            {
                case "vehicular_simulation.SYS_CFG":
                    fs = new FileStream(System.IO.Path.Combine(SYSTEM_PATH, "vehicular.ini"), FileMode.Open);
                    break;
                case "vehicular_simulation.SetPath":
                    fs = new FileStream(System.IO.Path.Combine(SYSTEM_PATH, "Init.ini"), FileMode.Open);
                    break;
                //case "vehicular_simulation.Software":
                //    fs = new FileStream(System.IO.Path.Combine(SYSTEM_PATH, "software.dcf"), FileMode.Open);
                //    break;
                default:
                    break;
            }
            //sys.UnknownNode += new XmlNodeEventHandler(SerializerUnknownNode);
            //sys.UnknownAttribute += new XmlAttributeEventHandler(SerializerUnknownAttribute);
            T _message = (T)sys.Deserialize(fs);
            fs.Close();
            return _message;
        }
        //读取xml文件里的数据
        public static T ReadObject<T>(String type)
        {
            XmlSerializer sys = new XmlSerializer(typeof(T));
            fs = new FileStream(Path +"/"+ type, FileMode.Open);
            T _message = (T)sys.Deserialize(fs);
            fs.Close();
            return _message;
        }
        private static TextWriter _writer;
        //将数据写入xml文件中
        public static void WriteObject<T>(T type)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            switch (typeof(T).ToString())
            {
                case "vehicular_simulation.SetPath":
                    _writer = new StreamWriter(SYSTEM_PATH + "/Init.ini");
                    break;
              
                case "vehicular_simulation.Location":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/Location/location.dcf");
                    break;
                case "vehicular_simulation.Weather":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/Weather/weather.dcf");
                    break;
                case "vehicular_simulation.CarType":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/CarType/carType.dcf");
                    break;

                case "vehicular_simulation.SYS_CFG":
                    _writer = new StreamWriter(SYSTEM_PATH + "/vehicular.ini");
                    break;
                case "vehicular_simulation.Radar":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/Radar/radar.dcf");
                    break;
                case "vehicular_simulation.Lidar":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/Lidar/lidar.dcf");
                    break;
                case "vehicular_simulation.IMUClass":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/IMU/imu.dcf");
                    break;
                case "vehicular_simulation.GPSClass":
                    _writer = new StreamWriter(SYSTEM_PATH + "/data/GPS/gps.dcf");
                    break;
                case "vehicular_simulation.Software":
                    _writer = new StreamWriter(SYSTEM_PATH + "/software.dcf");
                    break;
                default:
                    break;
            }
            if (type != null)
                serializer.Serialize(_writer, type);
            _writer.Close();
        }       
        #endregion
        private static void SerializerUnknownNode(object sender, XmlNodeEventArgs e)
        {

        }
        private static void SerializerUnknownAttribute(object sender, XmlAttributeEventArgs e)
        {

        }
    }
}
