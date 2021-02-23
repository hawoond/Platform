using ImrPlatform.ViewModel;
using System.Windows.Controls;

namespace ImrPlatform.View.Main
{
    /// <summary>
    /// ucMain.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucMain : UserControl
    {
        public ucMain()
        {
            InitializeComponent();
            this.DataContext = MainViewModel.Instance;
        }

        private void ListBox_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
