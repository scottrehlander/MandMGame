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
using System.Windows.Threading;
using System.Diagnostics;

namespace MandM
{
    public partial class MainPage : UserControl
    {
        DispatcherTimer gameLoopTimer = new DispatcherTimer();
        DateTime timeFrameEnded;
        TimeSpan timeSinceLastFrame = new TimeSpan(0);
        TimeSpan timeSinceLastMandM = new TimeSpan(0, 0, 0);

        Eater e;
        MandM[] mAndMs = new MandM[5];

        private int numMissed = 0;

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            // Set some application settings
            Application.Current.Host.Settings.EnableFrameRateCounter = true;
            Application.Current.Host.Settings.MaxFrameRate = 40;

            // Start the main game loop
            //gameLoopTimer.Interval = new TimeSpan(0,0,0,0,5);
            gameLoopTimer.Tick += new EventHandler(gameLoopTimer_Tick);
            timeFrameEnded = DateTime.Now;
            LoadResources();
            gameLoopTimer.Start();
        }

        // Game Loop
        void gameLoopTimer_Tick(object sender, EventArgs e)
        {
            timeSinceLastFrame = DateTime.Now - timeFrameEnded;
            
            // Update will update all of the positions and calculate collisions
            Update(timeSinceLastFrame);
            
            // Draw will actually do the drawing to the screen
            Draw(cvsMain);
            
            timeFrameEnded = DateTime.Now;
        }

        private void LoadResources()
        {   
            e = new Eater();
            e.LoadResources();
        }

        bool createdOne = false;
        private void CreateNewMandMs(TimeSpan timeSinceLastFrame)
        {
            if (timeSinceLastMandM > new TimeSpan(0, 0, 0, 0, 660))
            {
                for (int i = 0; i < mAndMs.Length; i++)
                {
                    if (mAndMs[i] == null || mAndMs[i].FinishedCleanup)
                    {
                        if (mAndMs[i] != null)
                        {
                            mAndMs[i].Reset();
                        }
                        else
                        {
                            mAndMs[i] = new MandM(new Random().Next(25, 500));
                            mAndMs[i].LoadResources();
                        }

                        timeSinceLastMandM = new TimeSpan(0, 0, 0);
                        break;
                    }
                }
            }

            timeSinceLastMandM += timeSinceLastFrame;
        }

        private void Update(TimeSpan timeSinceLastFrame)
        {

            CreateNewMandMs(timeSinceLastFrame);
            
            // Loop throw all of the MandMs and update their positions
            for (int i = 0; i < mAndMs.Length; i++)
            {
                if (mAndMs[i] == null) continue;

                mAndMs[i].Update(timeSinceLastFrame);
            }

            DetectCollisions();
        }

        private void DetectCollisions()
        {
            for (int i = 0; i < mAndMs.Length; i++)
            {
                if (mAndMs[i] == null) continue;

                if (mAndMs[i].FinishedFall)
                {
                    if (!mAndMs[i].FinishedCleanup)
                    {
                        // Check that the guy is in range... if not, you lose
                        if ((mAndMs[i].CurPos.X > e.CurPos.X && mAndMs[i].CurPos.X < e.CurPos.X + 100))
                        {
                            mAndMs[i].DestroyObject(cvsMain);
                            mAndMs[i].FinishedCleanup = true;
                        }
                        else
                        {
                            // Missed it
                            if (!mAndMs[i].FinishedCleanup && mAndMs[i].FinishedLeeway)
                            {
                                numMissed++;
                                tbNumMissed.Text = numMissed.ToString();

                                mAndMs[i].DestroyObject(cvsMain);
                                mAndMs[i].FinishedCleanup = true;
                            }
                        }
                    }
                }

            }
        }

        private void Draw(Canvas mainCanvas)
        {
            // Update the main character if he is moved
            e.Update(timeSinceLastFrame);
            
            // Draw the main character
            e.Draw(mainCanvas);

            // Draw the MandMs
            for (int i = 0; i < mAndMs.Length; i++)
            {
                if (mAndMs[i] == null) break;

                if(!mAndMs[i].FinishedCleanup)
                    mAndMs[i].Draw(mainCanvas);
            } 
            
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            // Handle Keypresses
            if (e.Key == Key.Right)
                KeyboardMonitor.RightKeyDown = true;
            if (e.Key == Key.Left)
                KeyboardMonitor.LeftKeyDown = true;
        }

        private void UserControl_KeyUp(object sender, KeyEventArgs e)
        {
            //Handle keypresses
            if (e.Key == Key.Right)
                KeyboardMonitor.RightKeyDown = false;
            if (e.Key == Key.Left)
                KeyboardMonitor.LeftKeyDown = false;
        }
    }
}
