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
    /// ucContentsMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucContentsMain : Window
    {
        public ucContentsMain(string sConsId)
        {
            InitializeComponent();
            this.DataContext = ContentsViewModel.Instance;
            ContentsViewModel.Instance.GetConsId(sConsId);
        }

        private void Carousel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ContentsViewModel.Instance.CurrentCarouselData = (string)e.AddedItems[0];
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            
            base.OnClosed(e);
        }
    }
}
