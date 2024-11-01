using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;


namespace NISA.Views
{
   public partial class CorrugatedHorn : UserControl
   {
       public CorrugatedHorn()
       {
           InitializeComponent();
       }


       private void OnBackButtonClick(object? sender, RoutedEventArgs e)
       {
           // Optionally, add back navigation logic
#pragma warning disable CS8602 
           var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
#pragma warning restore CS8602 
           mainWindow?.NavigateTo(new LandingPage()); // Navigate to NewPage
       }

        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            // Clear all text boxes
        TextBoxX1.Text = string.Empty;
        TextBoxX2.Text = string.Empty;
        TextBoxX3.Text = string.Empty;
        TextBoxX4.Text = string.Empty;
        TextBoxX5.Text = string.Empty;
        TextBoxX6.Text = string.Empty;
        TextBoxX7.Text = string.Empty;

        }
   }
}
