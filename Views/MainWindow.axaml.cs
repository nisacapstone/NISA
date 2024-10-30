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
            // Load the LandingPage by default
            NavigateTo(new LandingPage());
        }

        public void NavigateTo(UserControl view)
        {
            ContentArea.Content = view; // Set the new view
        }
    }
}