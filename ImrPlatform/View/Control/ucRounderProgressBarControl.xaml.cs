using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace ImrPlatform.View.Control
{
    /// <summary>
    /// Interaction logic for RounderProgressBar.xaml
    /// </summary>
    public partial class ucRounderProgressBarControl : UserControl
    {
        private const string PERCENTS_TEXT = "{0}%";

        private delegate void VoidDelegete();
        private Timer timer;
        private bool loaded;
        private int progress;

        public ucRounderProgressBarControl()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            timer = new Timer(100);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
            loaded = true;
        }

        void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            rotationCanvas.Dispatcher.Invoke
                (
                new VoidDelegete(
                    delegate
                        {
                            SpinnerRotate.Angle += 30;
                            if (SpinnerRotate.Angle == 360)
                            {
                                SpinnerRotate.Angle = 0;
                            }
                        }
                    ),
                null
                );
            
        }

        private void tblPercents_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (loaded)
            {
                Canvas.SetLeft(tbPercents, (rotationCanvas.ActualHeight - tbPercents.ActualWidth) / 2);
                Canvas.SetTop(tbPercents, (rotationCanvas.ActualHeight - tbPercents.ActualHeight) / 2);   
            }
        }

        private void UpdateProgress()
        {
            tbPercents.Text = string.Format(PERCENTS_TEXT, progress);
        }

        public int Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                UpdateProgress();
            }
        }

    }
}
