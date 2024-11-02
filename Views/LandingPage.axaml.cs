using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Interactivity;
using System;
using NISA.Views;
using Avalonia.Controls.ApplicationLifetimes;

namespace NISA.Views
{
    public partial class LandingPage : UserControl
    {
        public LandingPage()
        {
            InitializeComponent();

            // Attach mouse event handlers
            CorrugatedHornBorder.AddHandler(PointerEnteredEvent, OnBorderPointerEnter);
            CorrugatedHornBorder.AddHandler(PointerExitedEvent, OnBorderPointerLeave);

            CrossGuideCouplerBorder.AddHandler(PointerEnteredEvent, OnBorderPointerEnter);
            CrossGuideCouplerBorder.AddHandler(PointerExitedEvent, OnBorderPointerLeave);
        }

        private void OnBorderPointerEnter(object ? sender, PointerEventArgs e)
        {
            if (sender is Border border)
            {
                // Scale up the border when hovered
                border.RenderTransform = new ScaleTransform(1.1, 1.1); // Scale to 110%
            }
        }

        private void OnBorderPointerLeave(object ? sender, PointerEventArgs e)
        {
            if (sender is Border border)
            {
                // Reset the scale when the pointer leaves
                border.RenderTransform = new ScaleTransform(1.0, 1.0); // Reset to 100%
            }
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

