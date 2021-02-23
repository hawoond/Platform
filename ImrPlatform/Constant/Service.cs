using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImrPlatform.Constant
{
    public class Service
    {
        public class Type
        {
            public const string JSON = "JSON";
            public const string XML = "XML";
            public const string STRING_CHEESE = "STRING";
        }

        public class ServiceName
        {
            public class SELECT
            {
                public const string S01 = "QUERY_01";
                public const string S02 = "QUERY_02";
                public const string CO_ETC_VER_SIN = "S_CO_ETC_VER_SIN";
                public const string CO_USR_LGN_SIN = "S_CO_USR_LGN_SIN";
                public const string CV_CTS_INFO_SIN = "S_CV_CTS_INFO_SIN";
                public const string CV_CTS_REV_LIST = "S_CV_CTS_REV_LIST";
                public const string MA_CTS_SRCH_LIST = "S_MA_CTS_SRCH_LIST";
                public const string MA_USR_FAV_LIST = "S_MA_USR_FAV_LIST";
                public const string MA_USR_PRCH_LIST = "S_MA_USR_PRCH_LIST";
                public const string MA_USR_REV_LIST = "S_MA_USR_REV_LIST";
            }
            public class DELETE
            {
                public const string MA_USR_FAV_SIN = "D_MA_USR_FAV_SIN";
            }

            public class INSERT
            {
                public const string MA_USR_FAV_SIN = "I_MA_USR_FAV_SIN";
                public const string MA_USR_PRCH_SIN = "I_MA_USR_PRCH_SIN";
                public const string MA_USR_REV_SIN = "I_MA_USR_REV_SIN";
            }

            public class UPDATE
            {
                public const string MA_USR_REV_SIN = "U_MA_USR_REV_SIN";
            }
        }

        public class ConnectName
        {
            public const string TEST = "TEST";
        }
    }
}
