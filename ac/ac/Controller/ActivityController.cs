using ac.Models;
using System;
using System.Windows.Threading;

namespace ac.Controller
{
    public static class ActivityController
    {
        private static UISettings _settings;

        public static void SetActivityContext(UISettings settings)
        {
            _settings = settings;
            DispatcherTimer timer = new DispatcherTimer();
            if (_settings.IsActive)
            {
                // Application was activated.
                if (_settings.TimedExecutionSeconds > 0)
                {
                    timer.Interval = new TimeSpan(0, 0, _settings.TimedExecutionSeconds);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                }
            }
            else
            {

            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            GenerateMouseMovements(_settings.IsRandomizingMovement);
        }

        private static void GenerateMouseMovements(bool isRandomizingMovement)
        {
            throw new NotImplementedException();
        }
    }
}
