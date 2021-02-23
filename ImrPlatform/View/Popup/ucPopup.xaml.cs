using System;
using System.Windows;
using static ImrPlatform.Common.EnumDef;

namespace ImrPlatform.View.Main
{
    /// <summary>
    /// ucSideMainLeft.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucPopup : Window
    {
        public ucPopup()
        {
            InitializeComponent();
        }

        public void SetFlag(POPUP_FLAG flag)
        {
            switch (flag)
            {
                case POPUP_FLAG.NONE:
                    {
                        break;
                    }
                case POPUP_FLAG.OK:
                    {
                        this.spPopupContent.Children.Add(new ucOkPopup(this));
                        break;
                    }
                case POPUP_FLAG.OK_CANCEL:
                    {
                        this.spPopupContent.Children.Add(new ucOkCancelPopup(this));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

        }
    }
}
