using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;



namespace NISA.Views
{
    public partial class LandingPage : UserControl
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void OnCorrugatedHornClick(object? sender, PointerPressedEventArgs e)
        {
            // Use a reference to the main window or a navigation service to navigate
          // Console.WriteLine("Button clicked!"); // Debug message

            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
            mainWindow?.NavigateTo(new NewPage()); // Navigate to NewPage
        }

        private void OnCrossGuideCouplerClick(object? sender, PointerPressedEventArgs e)
        {
            // Use a reference to the main window or a navigation service to navigate
           // Console.WriteLine("Button clicked!"); // Debug message

            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
            mainWindow?.NavigateTo(new NewPage()); // Navigate to NewPage
        }

    }
}
