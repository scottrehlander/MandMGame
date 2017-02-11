using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace MandM
{
    public partial class MandMViewSb : UserControl, INotifyPropertyChanged
    {

        Duration dropTime = new Duration(new TimeSpan(0, 0, 2));
        public Duration DropTime { get { return dropTime; } set { dropTime = value; if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DropTime")); } }

        public MandMViewSb()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
