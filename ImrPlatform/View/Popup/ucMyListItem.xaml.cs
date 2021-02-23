using ImrPlatform.Communication;
using ImrPlatform.Model;
using ImrPlatform.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ImrPlatform.View.Popup
{
    /// <summary>
    /// ucSideMainLeft.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucMyListItem : UserControl
    {
        public ucMyListItem()
        {
            InitializeComponent();
            //this.DataContext = MainViewModel.Instance;
        }

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // 데이터 삭제
            CallQuery callQuery = new CallQuery();

            DataSet dsResult = new DataSet();

            dsResult = callQuery.D_MA_USR_FAV_SIN(MainViewModel.LoginUser.USER_NO, ((UdtMyListItem)this.DataContext).ContentID);

            // 구매내역 갱신
            MainViewModel.Instance.InitMyList();
        }
    }
}
