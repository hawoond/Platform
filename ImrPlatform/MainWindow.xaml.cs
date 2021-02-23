using ImrPlatform.View.Contents;
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

namespace ImrPlatform
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    (new ucContentsMain("StarTrek")).Show();
        //}

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            e.Handled = true;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            imgExit.Source = new BitmapImage(new Uri("pack://application:,,/Image/icon_platform_close2.png"));
        }

        private void ImgExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            imgExit.Source = new BitmapImage(new Uri("pack://application:,,/Image/icon_platform_close1.png"));
        }
    }
}
