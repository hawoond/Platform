using ImrPlatform.Model;
using ImrPlatform.View.Popup;
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
using static IMRUtils.OverrideType;

namespace ImrPlatform.View.Main
{
    /// <summary>
    /// ucSideMainLeft.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucSideMainLeft : UserControl
    {
        public ucSideMainLeft()
        {
            InitializeComponent();
            this.RootLayout.DataContext = this;
        }

        /// <summary>
        /// 바인딩용
        /// </summary>
        public static readonly DependencyProperty OpenCommandProperty =
        DependencyProperty.Register("OpenCommand", typeof(ICommand),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public ICommand OpenCommand
        {
            get
            {
                return (ICommand)GetValue(OpenCommandProperty);
            }
            set
            {
                SetValue(OpenCommandProperty, value);
            }
        }

        public static readonly DependencyProperty OpenPopupProperty =
        DependencyProperty.Register("OpenPopup", typeof(ICommand),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public ICommand OpenPopup
        {
            get
            {
                return (ICommand)GetValue(OpenPopupProperty);
            }
            set
            {
                SetValue(OpenPopupProperty, value);
            }
        }

        public static readonly DependencyProperty PrchListProperty =
        DependencyProperty.Register("PrchList", typeof(MTObservableCollection<UdtMyListItem>),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public MTObservableCollection<UdtMyListItem> PrchList
        {
            get
            {
                return (MTObservableCollection<UdtMyListItem>)GetValue(PrchListProperty);
            }
            set
            {
                SetValue(PrchListProperty, value);
            }
        }

        public static readonly DependencyProperty RevListProperty =
        DependencyProperty.Register("RevList", typeof(MTObservableCollection<UdtMyListItem>),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public MTObservableCollection<UdtMyListItem> RevList
        {
            get
            {
                return (MTObservableCollection<UdtMyListItem>)GetValue(RevListProperty);
            }
            set
            {
                SetValue(RevListProperty, value);
            }
        }

        public static readonly DependencyProperty FavListProperty =
        DependencyProperty.Register("FavList", typeof(MTObservableCollection<UdtMyListItem>),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public MTObservableCollection<UdtMyListItem> FavList
        {
            get
            {
                return (MTObservableCollection<UdtMyListItem>)GetValue(FavListProperty);
            }
            set
            {
                SetValue(FavListProperty, value);
            }
        }

        public static readonly DependencyProperty IsLoginProperty =
        DependencyProperty.Register("IsLogin", typeof(bool),
        typeof(ucSideMainLeft), new PropertyMetadata(false));

        public bool IsLogin
        {
            get
            {
                return (bool)GetValue(IsLoginProperty);
            }
            set
            {
                SetValue(IsLoginProperty, value);
            }
        }

        public static readonly DependencyProperty UserNMProperty =
        DependencyProperty.Register("UserNM", typeof(string),
        typeof(ucSideMainLeft), new PropertyMetadata(string.Empty));
        public string UserNM
        {
            get
            {
                return (string)GetValue(UserNMProperty);
            }
            set
            {
                SetValue(UserNMProperty, value);
            }
        }

        public static readonly DependencyProperty UserNOProperty =
        DependencyProperty.Register("UserNO", typeof(string),
        typeof(ucSideMainLeft), new PropertyMetadata(string.Empty));

        public string UserNO
        {
            get
            {
                return (string)GetValue(UserNOProperty);
            }
            set
            {
                SetValue(UserNOProperty, value);
            }
        }

        public static readonly DependencyProperty MyListRowSelectProperty =
        DependencyProperty.Register("MyListRowSelect", typeof(UdtMyListItem),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public UdtMyListItem MyListRowSelect
        {
            get
            {
                return (UdtMyListItem)GetValue(MyListRowSelectProperty);
            }
            set
            {
                SetValue(MyListRowSelectProperty, value);
            }
        }

        public static readonly DependencyProperty LoginCommandProperty =
        DependencyProperty.Register("LoginCommand", typeof(ICommand),
        typeof(ucSideMainLeft), new PropertyMetadata(null));

        public ICommand LoginCommand
        {
            get
            {
                return (ICommand)GetValue(LoginCommandProperty);
            }
            set
            {
                SetValue(LoginCommandProperty, value);
                //MessageBox.Show(UserNM);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string test;
            test = "";
        }
    }
}
