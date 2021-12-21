using ac.Controller;
using ac.ViewModels;
using System;
using System.Windows;

namespace ac
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnTrigger_Click(object sender, RoutedEventArgs e)
        {
            UISettingsViewModel context = DataContext as UISettingsViewModel;
            context.UISettingsContainer.IsActive = !context.UISettingsContainer.IsActive;

            context.UISettingsContainer.IsActive = ActivityController.SetActivityContext(context.UISettingsContainer);
        }
    }
}
