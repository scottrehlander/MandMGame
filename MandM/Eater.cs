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
    public class Eater : ScreenObject
    {
        EaterView eater;

        public override void LoadResources()
        {
            base.LoadResources();

            curPos = new Point(100, 297);
            eater = new EaterView();
            eater.SetValue(Canvas.TopProperty, curPos.Y);
        }

        public override void Draw(Canvas mainCanvas)
        {
            mainCanvas.Children.Remove(eater);
            mainCanvas.Children.Add(eater);

            eater.SetValue(Canvas.LeftProperty, curPos.X);
        }

        double amountToMove = 0;
        public override void Update(TimeSpan timeSinceLastFrame)
        {
            if (timeSinceLastFrame.TotalMilliseconds <= 0) return;

            if (KeyboardMonitor.RightKeyDown)
            {
                amountToMove = 400 * timeSinceLastFrame.TotalSeconds;
                curPos.X += amountToMove;
            }
            if (KeyboardMonitor.LeftKeyDown)
            {
                amountToMove = 400 * timeSinceLastFrame.TotalSeconds;
                curPos.X -= amountToMove;
            }
        }

    }
}
