using ac.Models;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ac.Controller
{
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
        static readonly char[] SignOptions = { '+', '-' };
        const int CURSOR_POSITION_INCREMENT = 12;

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        private static bool _isWorking;
        private static BackgroundWorker _worker;
        private static DispatcherTimer _timer;

        public static bool SetActivityContext(UISettings settings)
        {
            if (settings.IsActive)
            {
                _worker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true
                };
                if (settings.TimedExecutionSeconds > 0)
                {
                    _timer = new DispatcherTimer
                    {
                        Interval = new TimeSpan(0, 0, settings.TimedExecutionSeconds)
                    };
                    _timer.Tick += (s, e) =>
                    {
                        settings.IsActive = false;
                        SetActivityContext(settings);
                    };
                    _timer.Start();
                }

                _isWorking = true;

                _worker.DoWork += (s, e) =>
                {
                    while (_isWorking)
                    {
                        GenerateMouseMovements(settings.IsRandomizingMovement);
                        Thread.Sleep(new Random().Next(1000, 2000));
                    }
                };
                _worker.RunWorkerAsync();
                return true;
            }
            else
            {
                _isWorking = false;
                _worker.CancelAsync();
                _worker.Dispose();
                _timer.Stop();
                return false;
            }
        }


        private static void GenerateMouseMovements(bool isRandomizingMovement)
        {
            int operatorValue = SignOptions[new Random().Next(0, SignOptions.Length)];
            GetCursorPos(out POINT position);

            if (!isRandomizingMovement)
            {
                if (operatorValue == '+')
                {
                    SetCursorPos(position.X + CURSOR_POSITION_INCREMENT, position.Y + CURSOR_POSITION_INCREMENT);
                }
                else
                {
                    SetCursorPos(position.X - CURSOR_POSITION_INCREMENT, position.Y - CURSOR_POSITION_INCREMENT);
                }
            }
            else
            {
                if (operatorValue == '+')
                {
                    SetCursorPos(position.X + new Random().Next(1, CURSOR_POSITION_INCREMENT), position.Y + new Random().Next(1, CURSOR_POSITION_INCREMENT));
                }
                else
                {
                    SetCursorPos(position.X - new Random().Next(1, CURSOR_POSITION_INCREMENT), position.Y - new Random().Next(1, CURSOR_POSITION_INCREMENT));
                }
            }
        }
    }
}
