using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
    public class ExceptionMsg
    {
        public enum EXP_TYPE
        {
            ERROR,
            INFO
        }

        public ExceptionMsg( )
        {
        }

        public static void Show(string des, EXP_TYPE tp)
        {
            if( tp == EXP_TYPE.ERROR )
                Log.Write( "EXCEPTION_ERROR: " + des );
            else if (tp == EXP_TYPE.INFO)
                Log.Write("EXCEPTION_INFO: " + des);
        }
    }
}
