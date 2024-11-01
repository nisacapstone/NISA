using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;

// nadine was here

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

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            mainWindow?.NavigateTo(new CorrugatedHorn()); // Navigate to NewPage
        }

        private void OnCrossGuideCouplerClick(object? sender, PointerPressedEventArgs e)
        {

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            mainWindow?.NavigateTo(new NewPage()); // Navigate to NewPage
        }

    }
}
