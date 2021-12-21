using System.ComponentModel;

namespace ac.Models
{
    public class UISettings : INotifyPropertyChanged
    {
        private bool _isActive;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (value != _isActive)
                {
                    _isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }

        private bool _isRandomizingMovement;
        public bool IsRandomizingMovement
        {
            get
            {
                return _isRandomizingMovement;
            }
            set
            {
                if (value != _isRandomizingMovement)
                {
                    _isRandomizingMovement = value;
                    OnPropertyChanged(nameof(IsRandomizingMovement));
                }
            }
        }

        private int _timedExecutionSeconds;
        public int TimedExecutionSeconds
        {
            get
            {
                return _timedExecutionSeconds;
            }
            set
            {
                if (value != _timedExecutionSeconds)
                {
                    _timedExecutionSeconds = value;
                    OnPropertyChanged(nameof(TimedExecutionSeconds));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
