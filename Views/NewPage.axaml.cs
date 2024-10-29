using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;


namespace NISA.Views
{
    public partial class NewPage : UserControl
    {
        public NewPage()
        {
            InitializeComponent();
        }
        private void OnBackButtonClick(object? sender, RoutedEventArgs e)
        {
            // Optionally, add back navigation logic
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
            mainWindow?.NavigateTo(new LandingPage()); // Navigate to NewPage 
        }
    }
}
