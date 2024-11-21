using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Avalonia.Controls.ApplicationLifetimes;

namespace NISA.Views
{
    public partial class CorrugatedHorn : UserControl
    {
       // Stacks to store undo/redo states
        private Stack<string[]> undoStack = new Stack<string[]>();
        private Stack<string[]> redoStack = new Stack<string[]>(); 

        private readonly string _defaultStlFilePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Horn.stl");

        private string _selectedStlFilePath;

        private double _translateX = 0, _translateY = 0, _translateZ = 0;
        private double _rotateX = 0, _rotateY = 0, _rotateZ = 0;

        private CancellationTokenSource _debounceCts = new(); // Fixed nullability warning

        public CorrugatedHorn()
        {
            InitializeComponent();
            _selectedStlFilePath = _defaultStlFilePath;

            // Set up slider event handlers with debouncing
            TranslateXSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _translateX, TranslateXSlider.Value));
            TranslateYSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _translateY, TranslateYSlider.Value));
            TranslateZSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _translateZ, TranslateZSlider.Value));
            RotateXSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _rotateX, RotateXSlider.Value));
            RotateYSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _rotateY, RotateYSlider.Value));
            RotateZSlider.PropertyChanged += (s, e) => DebouncedUpdate(() => UpdateValueAndRender(ref _rotateZ, RotateZSlider.Value));
        }

        private void DebouncedUpdate(Action updateAction)
        {
            _debounceCts?.Cancel();
            _debounceCts = new CancellationTokenSource();

            Task.Delay(300, _debounceCts.Token)
                .ContinueWith(t =>
                {
                    if (!t.IsCanceled)
                    {
                        Dispatcher.UIThread.Invoke(updateAction);
                    }
                });
        }

        private void UpdateValueAndRender(ref double target, double newValue)
        {
            target = newValue;
            RenderModelAsync();
        }

        private async void RenderModelAsync()
        {
            await Task.Run(() =>
            {
                string outputPngPath = Path.Combine(Path.GetTempPath(), "HornOutput.png");
                RenderStlToImage(_selectedStlFilePath, outputPngPath);

                Dispatcher.UIThread.Invoke(() =>
                {
                    if (File.Exists(outputPngPath))
                    {
                        MyImageControl.Source = new Bitmap(outputPngPath);
                    }
                });
            });
        }

        private void RenderStlToImage(string stlFilePath, string outputPngPath)
        {
            string scriptPath = Path.Combine(Path.GetTempPath(), "temp_model.scad");
            string scadScript = $@"
translate([{_translateX}, {_translateY}, {_translateZ}])
    rotate([{_rotateX}, {_rotateY}, {_rotateZ}])
        import(""{stlFilePath}"");";

            File.WriteAllText(scriptPath, scadScript);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/Applications/OpenSCAD.app/Contents/MacOS/OpenSCAD",
                    Arguments = $"-o {outputPngPath} {scriptPath}",
                    UseShellExecute = false,
                    RedirectStandardError = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                Debug.WriteLine($"OpenSCAD error: {error}");
                throw new Exception($"OpenSCAD rendering failed: {error}");
            }
        }

        private void OnBackButtonClick(object? sender, RoutedEventArgs e)
        {
            var mainWindow = (Avalonia.Application.Current?.ApplicationLifetime as Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            if (mainWindow is MainWindow window)
            {
                window.NavigateTo(new LandingPage());
            }
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            TextBoxX1.Text = TextBoxX2.Text = TextBoxX3.Text = TextBoxX4.Text = string.Empty;
            // TextBoxX5.Text = TextBoxX6.Text = TextBoxX7.Text = string.Empty;

            _translateX = _translateY = _translateZ = 0;
            _rotateX = _rotateY = _rotateZ = 0;

            TranslateXSlider.Value = TranslateYSlider.Value = TranslateZSlider.Value = 0;
            RotateXSlider.Value = RotateYSlider.Value = RotateZSlider.Value = 0;

            RenderModelAsync();
        }

        private void OnGenerateClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string modelScript = "cube([10, 10, 10]);";
                string outputPath = Path.Combine(Path.GetTempPath(), "output.png");

                string scriptPath = Path.Combine(Path.GetTempPath(), "temp_model.scad");
                File.WriteAllText(scriptPath, modelScript);

                RenderStlToImage(scriptPath, outputPath);

                if (File.Exists(outputPath))
                {
                    MyImageControl.Source = new Bitmap(outputPath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error generating model: {ex.Message}");
            }
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

        private void OnExportClick(object? sender, RoutedEventArgs e)
        {
            // Handle export logic
        }
        private void OnSubmitClick(object sender, RoutedEventArgs e)
        {

        }
        private void OnCutStlClick(object? sender, RoutedEventArgs e)
        {
            try
            {
                if (!File.Exists(_selectedStlFilePath))
                {
                    Debug.WriteLine("STL file not found.");
                    return;
                }

                string selectedPlane = GetSelectedPlane();
                string outputPngPath = Path.Combine(Path.GetTempPath(), "CutHornOutput.png");
                string scriptPath = Path.Combine(Path.GetTempPath(), "cut_model.scad");
                // Generate the OpenSCAD script for cuttin
                string scadScript = selectedPlane switch
                {
                    "XY" => $@"
                    difference() {{
                        import(""{_selectedStlFilePath}"");
                        translate([0, 0, -100]) cube([2000, 2000, 2000], true);  // Cut along Z-axis
                    }}",
                    "YZ" => $@"
                    difference() {{
                        import(""{_selectedStlFilePath}"");
                        translate([-1000, 0, 0]) cube([2000, 2000, 2000], true);  // Cut along X-axis
                    }}",
                    "XZ" => $@"
                    difference() {{
                        import(""{_selectedStlFilePath}"");
                        translate([0, -1000, 0]) cube([2000, 2000, 2000], true);  // Cut along Y-axis
                    }}",
                    _ => throw new Exception("Invalid plane selected.")
                };

                Debug.WriteLine("Generated OpenSCAD Script:");
                Debug.WriteLine(scadScript);

                // Save the script and render the result
                if (File.Exists(outputPngPath)) File.Delete(outputPngPath);
                File.WriteAllText(scriptPath, scadScript);

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "/Applications/OpenSCAD.app/Contents/MacOS/OpenSCAD",
                        Arguments = $"--imgsize=800,600 -o {outputPngPath} {scriptPath}",
                        UseShellExecute = false,
                        RedirectStandardError = true
                    }
                };

                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    string error = process.StandardError.ReadToEnd();
                    Debug.WriteLine($"OpenSCAD error: {error}");
                    throw new Exception($"OpenSCAD rendering failed: {error}");
                }

                // Force reload the image
                Dispatcher.UIThread.Invoke(() =>
                {
                    if (File.Exists(outputPngPath))
                    {
                        using (var stream = File.OpenRead(outputPngPath))
                        {
                            MyImageControl.Source = new Bitmap(stream);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Output PNG not found.");
                    }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cutting STL: {ex.Message}");
            }
        }





        private string GetSelectedPlane()
        {
            // Ensure that we safely handle possible null values
            return PlaneSelection.SelectedItem is ComboBoxItem selectedItem
                ? selectedItem.Content?.ToString() ?? "XY" // Default to "XY" if null
                : "XY"; // Default to "XY" if no item is selected
        }




    }
}


