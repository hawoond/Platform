using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImrPlatform.Model
{
    public class UdtContentItem : IMRUtils.WPF.MomViewModel
    {
        private string sContentID;                  //컨텐츠 아이디
        private string sContentType;                //컨텐츠 타입 (App, Movie)
        private string sContentCategory;            //컨텐츠 카테고리
        private string sContentDesc;                //컨텐츠 설명
        private string sContentTitle;               //컨텐츠 이름
        private string sCopyWriterName;             //저작권자 이름
        private string sPrice;                      //컨텐츠 가격
        private string sStarRate;                   //컨텐츠 별점
        private string sDownloadRate;               //다운로드 횟수
        private ImageSource iThumbnail;             //썸네일

        public UdtContentItem()
        {
        }

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

        public string ContentCategory
        {
            get
            {
                return sContentCategory;
            }
            set
            {
                sContentCategory = value;
                this.OnPropertyChanged("ContentCategory");
            }
        }

        public string ContentDesc
        {
            get
            {
                return sContentDesc;
            }
            set
            {
                sContentDesc = value;
                this.OnPropertyChanged("ContentDesc");
            }
        }

        public string ContentTitle
        {
            get
            {
                return sContentTitle;
            }
            set
            {
                sContentTitle = value;
                this.OnPropertyChanged("ContentTitle");
            }
        }

        public string CopyWriterName
        {
            get
            {
                return sCopyWriterName;
            }
            set
            {
                sCopyWriterName = value;
                this.OnPropertyChanged("CopyWriterName");
            }
        }

        public string Price
        {
            get
            {
                return sPrice;
            }
            set
            {
                sPrice = value;
                this.OnPropertyChanged("Price");
            }
        }

        public string StarRate
        {
            get
            {
                return sStarRate;
            }
            set
            {
                sStarRate = value;
                this.OnPropertyChanged("StarRate");
            }
        }

        public string DownloadRate
        {
            get
            {
                return sDownloadRate;
            }
            set
            {
                sDownloadRate = value;
                this.OnPropertyChanged("DownloadRate");
            }
        }

        public ImageSource Thumbnail
        {
            get
            {
                return iThumbnail;
            }
            set
            {
                iThumbnail = value;
                this.OnPropertyChanged("Thumbnail");
            }
        }
    }
}
