using ImrPlatform.ViewModel;
using System;
using System.Collections.Generic;
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

namespace ImrPlatform.View.Contents
{
    /// <summary>
    /// ucReviewWrite.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucReviewWrite : UserControl
    {
        public ucReviewWrite()
        {
            InitializeComponent();
            this.DataContext = ContentsViewModel.Instance;
        }

        /// <summary>
        /// 리뷰 등록 커맨드 바인딩
        /// </summary>
        public static readonly DependencyProperty RegistReviewBindCommandProperty =
        DependencyProperty.Register("RegistReviewBindCommand", typeof(ICommand),
        typeof(ucReviewWrite), new PropertyMetadata(null));

        public ICommand RegistReviewBindCommand
        {
            get
            {
                return (ICommand)GetValue(RegistReviewBindCommandProperty);
            }
            set
            {
                SetValue(RegistReviewBindCommandProperty, value);
            }
        }

        /// <summary>
        /// 리뷰 등록 커맨드 파라미터
        /// </summary>
        public static readonly DependencyProperty RegistReviewBindCommParamProperty =
        DependencyProperty.Register("RegistReviewBindCommParam", typeof(string),
        typeof(ucReviewWrite), new PropertyMetadata(null));

        public string RegistReviewBindCommParam
        {
            get
            {
                return (string)GetValue(RegistReviewBindCommParamProperty);
            }
            set
            {
                SetValue(RegistReviewBindCommParamProperty, value);
            }
        }

        
        public static readonly DependencyProperty ReviewDocTextProperty = DependencyProperty.Register("ReviewDoc", typeof(string), typeof(ucReviewWrite), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 리뷰 입력 텍스트 바인딩
        /// </summary>
        public string ReviewDoc
        {
            get
            {
                return (string)GetValue(ReviewDocTextProperty);
            }
            set
            {
                SetValue(ReviewDocTextProperty, value);
            }
        }
    }
}
