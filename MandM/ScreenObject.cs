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
    public class ScreenObject
    {
        protected Point curPos = new Point(-1, -1);
        public virtual Point CurPos { get { return curPos; } set { curPos = value; } }

        public virtual void LoadResources()
        {
        }

        public virtual void Update(TimeSpan timeSinceLastFrame)
        {
        }

        public virtual void Draw(Canvas mainCanvas)
        {
        }

        public virtual void DestroyObject(Canvas mainCanvas)
        {
        }
    }
}
