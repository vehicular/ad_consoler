using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace vehicular_simulation
{
    [XmlRoot("SYS_CFG", Namespace = "vehicular_simulation", IsNullable = false)]
    public class Software
    {
        public string D_aimIP;
        public string D_aim_duan;

        public string D_localIP;
        public string D_local_duan;

        public int D_selectLanguage;

    }
}
