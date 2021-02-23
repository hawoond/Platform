using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ImrPlatform.View.ViewCommon
{
    /// <summary>
    /// ucInfoBox.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucInfoBox : UserControl
    {
        public ucInfoBox()
        {
            InitializeComponent();
            RootLayout.DataContext = this;
        }

        public static readonly DependencyProperty InfoTitleTextProperty =
            DependencyProperty.Register("InfoTitleText", typeof(string),
                typeof(ucInfoBox), new PropertyMetadata(string.Empty));

        public string InfoTitleText
        {
            get
            {
                return (string)GetValue(InfoTitleTextProperty);
            }
            set
            {
                SetValue(InfoTitleTextProperty, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty InfoDocTextProperty =
            DependencyProperty.Register("InfoDocText", typeof(string),
                typeof(ucInfoBox), new PropertyMetadata(string.Empty));

        public string InfoDocText
        {
            get
            {
                return (string)GetValue(InfoDocTextProperty);
            }
            set
            {
                SetValue(InfoDocTextProperty, value);
            }
        }
    }
}
