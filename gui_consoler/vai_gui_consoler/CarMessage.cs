using System;
using System.Collections.Generic;
using System.Text;

namespace vehicular_simulation
{
    [Serializable]
    public class CarMessage
    {

        public float x;
        public float y;
        public float yaw;

        public float speed;

        public float s;
        public float d;

        public JSONObject previous_path_x;
        public JSONObject previous_path_y;
        //public 


        public float end_path_s;
        public float end_path_d;

        public string sensor_fusion;



    }
}
