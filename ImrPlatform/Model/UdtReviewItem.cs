using IMRUtils.WPF;

namespace ImrPlatform.Model
{
    public class UdtReviewItem : MomViewModel
    {
        private string mUserName;
        private string mReviewDoc;
        private string mReviewDate;
        private string mReviewStarCount;

        public string UserName
        {
            get
            {
                return mUserName;
            }
            set
            {
                this.mUserName = value;
                this.OnPropertyChanged("UserName");
            }
        }

        public string ReviewDoc
        {
            get
            {
                return mReviewDoc;
            }
            set
            {
                this.mReviewDoc = value;
                this.OnPropertyChanged("ReviewDoc");
            }
        }

        public string ReviewDate
        {
            get
            {
                return mReviewDate;
            }
            set
            {
                this.mReviewDate = value;
                this.OnPropertyChanged("ReviewDate");
            }
        }


        public string ReviewStarCount
        {
            get
            {
                return mReviewStarCount;
            }
            set
            {
                mReviewStarCount = string.Empty;
                int nCount = 0;
                try
                {
                    nCount = int.Parse(value);
                }
                catch
                {
                    nCount = 0;
                }
                finally
                {
                    for (int i = 0; i < nCount; i++)
                    {
                        mReviewStarCount += "★";
                    }
                }
                this.OnPropertyChanged("ReviewStarCount");
            }
        }

        public UdtReviewItem()
        {
            this.UserName = string.Empty;
            this.ReviewDate = string.Empty;
            this.ReviewDoc = string.Empty;
            this.ReviewStarCount = string.Empty;
        }
    }
}
