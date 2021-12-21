using ac.Models;
using System.ComponentModel;

namespace ac.ViewModels
{
    internal class UISettingsViewModel : INotifyPropertyChanged
    {
        private UISettings _uiSettings;
        
        public UISettingsViewModel()
        {
            _uiSettings = new UISettings();
        }

        public UISettings UISettingsContainer
        {
            get
            {
                return _uiSettings;
            }
            set
            {
                _uiSettings = value;
                OnPropertyChanged(nameof(UISettingsContainer));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
