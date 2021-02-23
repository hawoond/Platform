﻿using ImrPlatform.ViewModel;
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

namespace ImrPlatform.View.Main
{
    /// <summary>
    /// ucSideMainLeft.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ucOkCancelPopup : UserControl
    {
        public ucOkCancelPopup(ucPopup parent)
        {
            InitializeComponent();
            this.DataContext = MainViewModel.Instance;
        }
    }
}
