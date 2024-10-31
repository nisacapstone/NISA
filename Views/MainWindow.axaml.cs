using System;
using System.Reflection.Metadata;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;

namespace NISA.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // ShowSplashScreen();
            // Load the LandingPage by default
            NavigateTo(new LandingPage());
        }

        public void NavigateTo(UserControl view)
        {
            ContentArea.Content = view; // Set the new view
        }
        // private async void ShowSplashScreen()
        // {
        //     var splash = new SplashScreen();
        //     Content = splash; // Set the splash screen as the content
        //     await splash.ShowSplashScreenAsync(); // Show splash for a few seconds
        //     Content = new LandingPage(); // After splash, load the main content
        // }
    }
}