using NHotkey.WindowsForms;
using System.Diagnostics;
using Vanara.PInvoke;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Save",
                Keys.F5,
                (s, e) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        Save();
                });

            HotkeyManager.Current.AddOrReplace("Remove Last",
                Keys.Control | Keys.Alt | Keys.R,
                (s, e) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        Remove();
                });

            HotkeyManager.Current.AddOrReplace("Load",
                Keys.F8,
                (s, e) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        Load();
                });

        }


        private const string Backup = @"C:\Users\marho\OneDrive\TheyAreBillions";
        private const string Saves = @"C:\Users\marho\OneDrive\Документы\My Games\They Are Billions\Saves";
        private void Load()
        {
            foreach (var f in GetSaveFiles())
                File.Delete(f);
            string frame = GetCurrentFrame();
            foreach (var f in Directory.GetFiles(frame))
            {
                File.Copy(f, f.Replace(frame, Saves), true);
            }
        }

        private void Save()
        {
            var files = GetSaveFiles();
            var target = Path.Combine(Backup,
                Path.GetFileNameWithoutExtension(files[0]));
            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);
            var frame = Path.Combine(target,
                Directory.GetDirectories(target).Length.ToString("D3"));
            Directory.CreateDirectory(frame);
            foreach (var f in files)
            {
                File.Copy(f, Path.Combine(frame, Path.GetFileName(f)), false);
            }
        }

        private static List<string> GetSaveFiles()
        {
            var files = GetCurrentSaves(4)
                        ?? GetCurrentSaves(2)
                        ?? throw new InvalidOperationException();
            return files;
        }

        private static List<string>? GetCurrentSaves(int num)
        {
            var result = Directory.EnumerateFiles(Saves)
                .Where(f => !f.EndsWith("steam_autocloud.vdf"))
                .TakeLast(num)
                .ToList();
            var prefix = Path.GetFileNameWithoutExtension(result[0]);
            return AllFilesHaveSamePrefix(result, prefix) ? result : null;
        }

        private static bool AllFilesHaveSamePrefix(List<string> files, string prefix)
        {
            foreach (var f in files)
            {
                if (!Path.GetFileNameWithoutExtension(f).StartsWith(prefix))
                    return false;
            }
            return true;
        }

        private void Remove()
        {
            string frame = GetCurrentFrame();
            Directory.Move(frame, Path.Combine(
                Path.GetDirectoryName(frame)!,
                "_" + Path.GetFileName(frame)));
        }

        private static string GetCurrentFrame()
        {
            return Directory
                .GetDirectories(Directory.GetDirectories(Backup).Last())
                .Last(d => !Path.GetFileName(d).StartsWith("_"));
        }

        private static bool IsCurrentProcessTheyAreBillions()
        {
            HWND activeWindowHandle = User32.GetForegroundWindow();
            User32.GetWindowThreadProcessId(activeWindowHandle, out var threadProcessId);
            var activeProcess = Process.GetProcesses()?
                .SingleOrDefault(p => p.Id == threadProcessId);
            return activeProcess?.ProcessName == "TheyAreBillions";
        }
    }
}