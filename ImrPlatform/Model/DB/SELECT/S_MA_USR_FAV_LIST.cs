using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityConnecter.Models.DB.SELECT
{
    public class S_MA_USR_FAV_LIST
    {
        /// <summary>
        /// 컨텐츠 아이디
        /// </summary>
        public string CONS_ID
        {
            get;
            set;
        }

        /// <summary>
        /// 컨텐츠 이름
        /// </summary>
        public string CONS_NM
        {
            get;
            set;
        }

        /// <summary>
        /// 썸네일 경로
        /// </summary>
        public string CONS_THUMB_PATH
        {
            get;
            set;
        }

        /// <summary>
        /// 컨텐츠 타입
        /// </summary>
        public string CONS_TP
        {
            get;
            set;
        }

        /// <summary>
        /// 등록 시간
        /// </summary>
        public string FAV_DTM
        {
            get;
            set;
        }
    }
}
