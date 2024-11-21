using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Collections.Generic;


namespace NISA.Views
{
    public partial class NewPage : UserControl
    {
        // Stacks to store undo/redo states
        private Stack<string[]> undoStack = new Stack<string[]>();
        private Stack<string[]> redoStack = new Stack<string[]>(); 
        public NewPage()
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
        private void OnRedoClick(object? sender, RoutedEventArgs e)
        {
            if (redoStack.Count > 0)
            {
                // Restore the state from the redo stack
                var redoState = redoStack.Pop();
                TextBoxX1.Text = redoState[0];
                TextBoxX2.Text = redoState[1];
                TextBoxX3.Text = redoState[2];
                TextBoxX4.Text = redoState[3];

                // Optionally, clear the redo stack if you don't want to keep multiple redo states
                // redoStack.Clear();
            }
        }

        private void OnUndoClick(object? sender, RoutedEventArgs e)
        {
             // Save the current state to the redo stack before modifying it
            string[] currentState = new string[]
            {
                TextBoxX1?.Text ?? string.Empty,
                TextBoxX2?.Text ?? string.Empty,
                TextBoxX3?.Text ?? string.Empty,
                TextBoxX4?.Text ?? string.Empty
            };

            redoStack.Push(currentState); // Push the current state to redo stack

            // Clear the textboxes (reset)
            if (TextBoxX1 != null) TextBoxX1.Text = string.Empty;
            if (TextBoxX2 != null) TextBoxX2.Text = string.Empty;
            if (TextBoxX3 != null) TextBoxX3.Text = string.Empty;
            if (TextBoxX4 != null) TextBoxX4.Text = string.Empty;

            // Clear the undo stack since we performed a reset
            undoStack.Clear();
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

        }
    }
}
