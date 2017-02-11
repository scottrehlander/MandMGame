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
    public class MandM : ScreenObject
    {
        MandMViewSb mAndM;
        bool finishedFall = false;
        public bool FinishedFall { get { return finishedFall; } set { finishedFall = value; } }

        bool finishedLeeway = false;
        public bool FinishedLeeway { get { return finishedLeeway; } set { finishedLeeway = value; } }

        bool finishedCleanup = false;
        public bool FinishedCleanup { get { return finishedCleanup; } set { finishedCleanup = value; } }

        public MandM(double xPosition)
        {
            curPos.X = xPosition;
        }

        public override void LoadResources()
        {
            mAndM = new MandMViewSb();
            mAndM.SetValue(Canvas.LeftProperty, curPos.X);
        }

        public void Reset()
        {
            finishedFall = false;
            finishedCleanup = false;
            finishedLeeway = false;
        }

        void mAndM_OnFallFinished()
        {
            FinishedFall = true;
            finishedLeeway = false;
            mAndM.leewayStoryboard.Completed -= new EventHandler(leewayStoryboard_Completed);
            mAndM.leewayStoryboard.Completed += new EventHandler(leewayStoryboard_Completed);
            mAndM.leewayStoryboard.Begin();
        }

        void leewayStoryboard_Completed(object sender, EventArgs e)
        {
            FinishedLeeway = true;
        }

        public override void Update(TimeSpan timeSinceLastFrame)
        {

        }

        public override void Draw(Canvas mainCanvas)
        {
            if (!mainCanvas.Children.Contains(mAndM))
            {
                mainCanvas.Children.Add(mAndM);

                finishedLeeway = false;

                // Set a random droptime
                int rand = new Random().Next(1500, 5000);
                mAndM.DropTime = new Duration(new TimeSpan(0, 0, 0, 0, rand));
                mAndM.myStoryboard.Begin();
                mAndM.myStoryboard.Completed -= new EventHandler(myStoryboard_Completed);
                mAndM.myStoryboard.Completed += new EventHandler(myStoryboard_Completed);
            }
        }

        void myStoryboard_Completed(object sender, EventArgs e)
        {
            mAndM_OnFallFinished();
        }

        public override void DestroyObject(Canvas mainCanvas)
        {
            mainCanvas.Children.Remove(mAndM);
        }
    }
}
