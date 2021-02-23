using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImrPlatform.Common
{
    public class EnumDef
    {
        public enum POPUP_FLAG
        {
            NONE,
            OK,
            OK_CANCEL
        }

        public enum MYPAGE_FLAG
        {
            PURCHASE,
            REVIEW,
            MYLIST
        }

        public class SERVICE
        {
            public const int SINGLE_ROW = 0;
            //public enum S_CV_CTS_INFO_SIN
            //{
            //    CONS_ID,
            //    CONS_NM,
            //    CONS_PREV_PATH,
            //    CONS_THUMB_PATH,
            //    STAR_RATE,
            //    CONS_PRICE,
            //    COPY_DOC,
            //    CONS_SIZE,
            //    CONS_DOC,
            //    COPY_NM,
            //    COPY_PHONE,
            //    COPY_MAIL_ADDRESS,
            //    COPY_HOME_PAGE,
            //    CONS_TP,
            //    CONS_CAT
            //}
        }
    }
}
