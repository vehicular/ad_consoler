using System;
using System.Collections.Generic;
using System.Text;

namespace vehicular_simulation
{
    class LanguageText_Form2
    {
        public static bool isEnglish = false;

        public static string label2
        {
            get
            {
                if (isEnglish)
                    return "Purpose IP";
                else
                    return "目的IP";
            }
        }
        public static string label3
        {
            get
            {
                if (isEnglish)
                    return "Destination port";
                else
                    return "目的端口号";
            }
        }
        public static string label4
        {
            get
            {
                if (isEnglish)
                    return "Local IP";
                else
                    return "本地IP";
            }
        }
        public static string label5
        {
            get
            {
                if (isEnglish)
                    return "Local port number";
                else
                    return "本地端口号";
            }
        }
        public static string label1
        {
            get
            {
                if (isEnglish)
                    return "Language selection";
                else
                    return "语言选择";
            }
        }
        public static string reset
        {
            get
            {
                if (isEnglish)
                    return "To reset";
                else
                    return "重新设置";
            }
        }
        public static string confirm
        {
            get
            {
                if (isEnglish)
                    return "Confirm";
                else
                    return "确定";
            }
        }

        public static string cancel
        {
            get
            {
                if (isEnglish)
                    return "Cancel";
                else
                    return "取消";
            }
        }
    }
}
