using ac.Models;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ac.Controller
{
    /// <summary>
    /// Struct representing a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }

    public static class ActivityController
    {
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        private static bool _isWorking;
        private static BackgroundWorker _worker;
        private static DispatcherTimer _timer;

        public static void SetActivityContext(UISettings settings)
        {
            _timer = new DispatcherTimer();
            if (settings.IsActive)
            {
                // Application was activated.
                if (settings.TimedExecutionSeconds > 0)
                {
                    _timer.Interval = new TimeSpan(0, 0, settings.TimedExecutionSeconds);
                    _timer.Tick += (s, e) =>
                    {
                        _isWorking = false;
                        _worker.CancelAsync();
                        _worker.Dispose();
                        _timer.Stop();
                    };
                    _timer.Start();
                }

                _isWorking = true;

                _worker.DoWork += (s, e) =>
                {
                    while (_isWorking)
                    {
                        GenerateMouseMovements(settings.IsRandomizingMovement);
                        Thread.Sleep(1000);
                    }
                };
                _worker.RunWorkerAsync();
            }
            else
            {
                _isWorking = false;
                _worker.CancelAsync();
                _worker.Dispose();
                _timer.Stop();
            }
        }


        private static void GenerateMouseMovements(bool isRandomizingMovement)
        {
            GetCursorPos(out POINT position);
            SetCursorPos(position.X + 12, position.Y + 10);
        }
    }
}
