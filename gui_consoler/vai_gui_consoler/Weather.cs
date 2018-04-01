using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("Weather", Namespace = "vehicular_simulation", IsNullable = false)]
    public class Weather
    {
        public int dayandnight;
        public int time;
        public string sunAngle;

        public bool sunRadio;
        public int sunValue;
        public string sunValue_text;
        
        public bool runRadio;
        public int runValue;
        public string runValue_text;

        public bool snowRadio;
        public int snowValue;
        public string snowValue_text;

        public bool fogRadio;
        public int fogValue;
        public string fogValue_text; 

    }
}
