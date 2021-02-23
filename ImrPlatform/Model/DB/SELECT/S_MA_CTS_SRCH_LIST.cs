using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityConnecter.Models.DB.SELECT
{
    public class S_MA_CTS_SRCH_LIST
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
        /// 컨텐츠 타입
        /// </summary>
        public string CONS_TP
        {
            get;
            set;
        }

        /// <summary>
        /// 컨텐츠 카테고리
        /// </summary>
        public string CONS_CT
        {
            get;
            set;
        }

        /// <summary>
        /// 컨텐츠 설명
        /// </summary>
        public string CONS_DOC
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
        /// 저작권자 이름
        /// </summary>
        public string COPY_NM
        {
            get;
            set;
        }

        /// <summary>
        /// 컨텐츠 가격
        /// </summary>
        public string CONS_PRICE
        {
            get;
            set;
        }

        /// <summary>
        /// 별점
        /// </summary>
        public string STAR_RATE
        {
            get;
            set;
        }

        /// <summary>
        /// 다운로드 횟수
        /// </summary>
        public string DOWN_RATE
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
    }
}
