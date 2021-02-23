using ImrPlatform.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityConnecter.Models.DB.SELECT
{
    public class S_CV_CTS_INFO_SIN
    {
        public string CONS_ID
        {
            get;
            set;
        }
        public string CONS_NM
        {
            get;
            set;
        }
        public string CONS_PREV_PATH
        {
            get;
            set;
        }
        public string CONS_THUMB_PATH
        {
            get;
            set;
        }
        public string STAR_RATE
        {
            get;
            set;
        }
        public string CONS_PRICE
        {
            get;
            set;
        }
        public string COPY_DOC
        {
            get;
            set;
        }
        public string CONS_SIZE
        {
            get;
            set;
        }
        public string CONS_DOC
        {
            get;
            set;
        }
        public string COPY_NM
        {
            get;
            set;
        }
        public string COPY_PHONE
        {
            get;
            set;
        }
        public string COPY_MAIL_ADDRESS
        {
            get;
            set;
        }
        public string COPY_HOME_PAGE
        {
            get;
            set;
        }
        public string CONS_TP
        {
            get;
            set;
        }
        public string CONS_CAT
        {
            get;
            set;
        }

        public string CONS_PATH
        {
            get;
            set;
        }
        //public S_CV_CTS_INFO_SIN(DataRow drResult)
        //{
        //    this = 

        //    this.CONS_ID = drResult["CONS_ID"].ToString();
        //    this.CONS_NM = drResult["CONS_NM"].ToString();
        //    this.CONS_PREV_PATH = drResult["CONS_PREV_PATH"].ToString();
        //    this.CONS_THUMB_PATH = drResult["CONS_THUMB_PATH"].ToString();
        //    this.STAR_RATE = drResult["STAR_RATE"].ToString();
        //    this.CONS_PRICE = drResult["CONS_PRICE"].ToString();
        //    this.COPY_DOC = drResult["COPY_DOC"].ToString();
        //    this.CONS_SIZE = drResult["CONS_SIZE"].ToString();
        //    this.CONS_DOC = drResult["CONS_DOC"].ToString();
        //    this.COPY_NM = drResult["COPY_NM"].ToString();
        //    this.COPY_PHONE = drResult["COPY_PHONE"].ToString();
        //    this.COPY_MAIL_ADDRESS = drResult["COPY_MAIL_ADDRESS"].ToString();
        //    this.COPY_HOME_PAGE = drResult["COPY_HOME_PAGE"].ToString();
        //    this.CONS_TP = drResult["CONS_TP"].ToString();
        //    this.CONS_CAT = drResult["CONS_CAT"].ToString();
        //}


        //public void KOO(DataRow drResult)
        //{
        //    string sPropName = string.Empty;
        //    string sPropValue = string.Empty;
        //    for (int i = 0; i < this.GetType().GetProperties().Length; i++)
        //    {
        //        sPropName = this.GetType().GetProperties()[i].Name;
        //        sPropValue = drResult[this.GetType().GetProperties()[i].Name].ToString();
        //        this.GetType().GetProperties()[i].SetValue(sPropName, sPropValue);
        //    }
        //}
    }
}
