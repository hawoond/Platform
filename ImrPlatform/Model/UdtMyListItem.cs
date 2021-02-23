using IMRUtils.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ImrPlatform.Model
{
    public class UdtMyListItem : MomViewModel
    {
        private string sContentID;
        private string sContentName;
        private ImageSource sContentThumbnail;
        private string sContentType;
        private string sEnterDtm;
        private Visibility vDeleterable;

        public string ContentID
        {
            get
            {
                return sContentID;
            }
            set
            {
                sContentID = value;
                this.OnPropertyChanged("ContentID");
            }
        }

        public string ContentName
        {
            get
            {
                if(sContentName.Length <= 3)
                {
                    return sContentName;
                }

                return sContentName.Substring(0, 3) + "...";
            }
            set
            {
                sContentName = value;
                this.OnPropertyChanged("ContentName");
            }
        }

        public ImageSource ContentThumbnail
        {
            get
            {
                return sContentThumbnail;
            }
            set
            {
                sContentThumbnail = value;
                this.OnPropertyChanged("ContentThumbnail");
            }
        }

        public string ContentType
        {
            get
            {
                return sContentType;
            }
            set
            {
                sContentType = value;
                this.OnPropertyChanged("ContentType");
            }
        }

        public string EnterDtm
        {
            get
            {
                DateTime dtm = DateTime.ParseExact(sEnterDtm, "yyyyMMdd HH:mm:ss",null);
                return dtm.ToString("yy/MM/dd");
            }
            set
            {
                sEnterDtm = value;
                this.OnPropertyChanged("EnterDtm");
            }
        }

        public Visibility Deleterable
        {
            get
            {
                return vDeleterable;
            }
            set
            {
                vDeleterable = value;
                this.OnPropertyChanged("Deleterable");
            }
        }
    }
}
