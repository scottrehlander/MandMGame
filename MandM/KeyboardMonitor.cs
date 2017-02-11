using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MandM
{
    public static class KeyboardMonitor
    {
        static bool leftKeyDown = false;
        public static bool LeftKeyDown { get { return leftKeyDown; } set { leftKeyDown = value; } }

        static bool rightKeyDown = false;
        public static bool RightKeyDown { get { return rightKeyDown; } set { rightKeyDown = value; } }
    }
}
