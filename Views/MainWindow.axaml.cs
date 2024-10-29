using System;
using System.Reflection.Metadata;
using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;

namespace NISA.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void NavigateTo(object view)
        {
            ContentArea.Content = view;
        }

    private void ButtonOnClick(object? sender, RoutedEventArgs e)
    {
    
        MainWindow.NavigateTo(); // Navigate to Page2

    }
}