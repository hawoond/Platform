using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ImrPlatform.View.Contents
{
    /// <summary>
    /// ucReview.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucReview : UserControl
    {
        public ucReview()
        {
            InitializeComponent();
            this.DataContext = this.RootLayout;
        }


        /// <summary>
        /// 리뷰 등록 커맨드 바인딩
        /// </summary>
        public static readonly DependencyProperty RegistReviewCommandProperty =
        DependencyProperty.Register("RegistReviewCommand", typeof(ICommand),
        typeof(ucReview), new PropertyMetadata(null));

        public ICommand RegistReviewCommand
        {
            get
            {
                return (ICommand)GetValue(RegistReviewCommandProperty);
            }
            set
            {
                SetValue(RegistReviewCommandProperty, value);
            }
        }

        /// <summary>
        /// 리뷰 등록 커맨드 파라미터
        /// </summary>
        public static readonly DependencyProperty RegistReviewCommParamProperty =
        DependencyProperty.Register("RegistReviewCommParam", typeof(string),
        typeof(ucReview), new PropertyMetadata(null));

        public string RegistReviewCommParam
        {
            get
            {
                return (string)GetValue(RegistReviewCommParamProperty);
            }
            set
            {
                SetValue(RegistReviewCommParamProperty, value);
            }
        }

        public static readonly DependencyProperty ReviewDocTextProperty = DependencyProperty.Register("ReviewDoc2", typeof(string), typeof(ucReview), new PropertyMetadata(string.Empty));
        /// <summary>
        /// 리뷰 입력 텍스트 바인딩
        /// </summary>
        public string ReviewDoc2
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
