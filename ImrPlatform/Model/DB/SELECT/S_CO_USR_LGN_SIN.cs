using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityConnecter.Models.DB.SELECT
{
    public class S_CO_USR_LGN_SIN
    {
        public S_CO_USR_LGN_SIN()
        {
        }

        public S_CO_USR_LGN_SIN(string uSER_NM, string uSER_NICK, string uSER_BIRTH, string uSER_PHONE, string uSER_MAIL_ADDRESS, string uSER_NO, string uSER_GROUP)
        {
            USER_NM = uSER_NM;
            USER_NICK = uSER_NICK;
            USER_BIRTH = uSER_BIRTH;
            USER_PHONE = uSER_PHONE;
            USER_MAIL_ADDRESS = uSER_MAIL_ADDRESS;
            USER_NO = uSER_NO;
            USER_GROUP = uSER_GROUP;
        }

        public string USER_NM
        {
            get;
            set;
        }
        public string USER_NICK
        {
            get;
            set;
        }
        public string USER_BIRTH
        {
            get;
            set;
        }
        public string USER_PHONE
        {
            get;
            set;
        }
        public string USER_MAIL_ADDRESS
        {
            get;
            set;
        }
        public string USER_NO
        {
            get;
            set;
        }
        public string USER_GROUP
        {
            get;
            set;
        }
    }
}
