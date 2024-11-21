using System;
using System.Diagnostics;
using System.IO;

namespace MyAvaloniaApp.Helpers
{
    public class OpenSCADHelper
    {
        public static void GenerateModel(string modelScript, string outputPath)
        {
            // Save the model script to a .scad file
            string scriptPath = Path.Combine(Path.GetTempPath(), "temp_model.scad");
            File.WriteAllText(scriptPath, modelScript);

            // Setup OpenSCAD command to generate output
            Process process = new Process();
            process.StartInfo.FileName = "/Applications/OpenSCAD.app/Contents/MacOS/OpenSCAD";
            process.StartInfo.Arguments = $"-o {outputPath} {scriptPath}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;

            process.Start();
            process.WaitForExit();

            // Handle errors if any
            if (process.ExitCode != 0)
            {
                string error = process.StandardError.ReadToEnd();
                throw new Exception($"OpenSCAD error: {error}");
            }
        }
    }
}
