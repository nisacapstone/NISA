using Avalonia.Controls;
using Avalonia.Interactivity;
using NISA.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using System.Collections.Generic;

namespace NISA.Views
{
    public partial class CorrugatedHorn : UserControl
    {
        // Stacks to store undo/redo states
        private Stack<string[]> undoStack = new Stack<string[]>();
        private Stack<string[]> redoStack = new Stack<string[]>();

        public CorrugatedHorn()
        {
            InitializeComponent();
        }

        private void OnBackButtonClick(object? sender, RoutedEventArgs e)
        {
            // Navigate to the LandingPage
#pragma warning disable CS8602
            var mainWindow = (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow as MainWindow; // Get the main window instance
#pragma warning restore CS8602
            mainWindow?.NavigateTo(new LandingPage()); // Navigate to LandingPage
        }

        // Allow only digits and backspace
        private void OnKeyDown(object sender, Avalonia.Input.KeyEventArgs e)
        {
            if (!(char.IsDigit((char)e.Key) || e.Key == Avalonia.Input.Key.Back))
            {
                e.Handled = true;  // Block the input if it's not a number or backspace
            }
        }

        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {
            // Implement your submit logic here
        }

        // Undo Click: Save current state, then reset
        private void OnUndoClick(object sender, RoutedEventArgs e)
        {
            // Save the current state to the redo stack before modifying it
            string[] currentState = new string[]
            {
                TextBoxX1?.Text ?? string.Empty,
                TextBoxX2?.Text ?? string.Empty,
                TextBoxX3?.Text ?? string.Empty,
                TextBoxX4?.Text ?? string.Empty,
                TextBoxX5?.Text ?? string.Empty,
                TextBoxX6?.Text ?? string.Empty,
                TextBoxX7?.Text ?? string.Empty
            };

            redoStack.Push(currentState); // Push the current state to redo stack

            // Clear the textboxes (reset)
            if (TextBoxX1 != null) TextBoxX1.Text = string.Empty;
            if (TextBoxX2 != null) TextBoxX2.Text = string.Empty;
            if (TextBoxX3 != null) TextBoxX3.Text = string.Empty;
            if (TextBoxX4 != null) TextBoxX4.Text = string.Empty;
            if (TextBoxX5 != null) TextBoxX5.Text = string.Empty;
            if (TextBoxX6 != null) TextBoxX6.Text = string.Empty;
            if (TextBoxX7 != null) TextBoxX7.Text = string.Empty;

            // Clear the undo stack since we performed a reset
            undoStack.Clear();
        }

        // Redo Click: Restore the state from the redo stack
        private void OnRedoClick(object sender, RoutedEventArgs e)
        {
            if (redoStack.Count > 0)
            {
                // Restore the state from the redo stack
                var redoState = redoStack.Pop();
                TextBoxX1.Text = redoState[0];
                TextBoxX2.Text = redoState[1];
                TextBoxX3.Text = redoState[2];
                TextBoxX4.Text = redoState[3];
                TextBoxX5.Text = redoState[4];
                TextBoxX6.Text = redoState[5];
                TextBoxX7.Text = redoState[6];

                // Optionally, clear the redo stack if you don't want to keep multiple redo states
                // redoStack.Clear();
            }
        }

        // Reset Click: Clears all textboxes, saving the current state for undo purposes
        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            // Save the current state before clearing
            string[] currentState = new string[]
            {
                TextBoxX1?.Text ?? string.Empty,
                TextBoxX2?.Text ?? string.Empty,
                TextBoxX3?.Text ?? string.Empty,
                TextBoxX4?.Text ?? string.Empty,
                TextBoxX5?.Text ?? string.Empty,
                TextBoxX6?.Text ?? string.Empty,
                TextBoxX7?.Text ?? string.Empty
            };

            undoStack.Push(currentState); // Push the current state to undo stack

            // Clear all text boxes
            if (TextBoxX1 != null) TextBoxX1.Text = string.Empty;
            if (TextBoxX2 != null) TextBoxX2.Text = string.Empty;
            if (TextBoxX3 != null) TextBoxX3.Text = string.Empty;
            if (TextBoxX4 != null) TextBoxX4.Text = string.Empty;
            if (TextBoxX5 != null) TextBoxX5.Text = string.Empty;
            if (TextBoxX6 != null) TextBoxX6.Text = string.Empty;
            if (TextBoxX7 != null) TextBoxX7.Text = string.Empty;

            // Clear redo stack on reset
            redoStack.Clear();
        }
    }
}
