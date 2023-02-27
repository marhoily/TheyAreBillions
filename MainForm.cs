using System.Diagnostics;
using NHotkey.WindowsForms;
using Vanara.PInvoke;

namespace TheyAreBillions
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Save",
                Keys.F5,
                (_, _) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        Save();
                });

            HotkeyManager.Current.AddOrReplace("Remove Last",
                Keys.Control | Keys.Alt | Keys.R,
                (_, _) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        Remove();
                });

            HotkeyManager.Current.AddOrReplace("LoadGame",
                Keys.F8,
                (_, _) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        LoadGame();
                });
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (_, _) => RefreshState();
            timer.Start();
        }

        private void RefreshState()
        {
            var saveFiles = GetCurrentSaves(2)!;
            var currentFrame = GetCurrentFrame();
            var backupFiles = Directory.GetFiles(currentFrame);
            var m = saveFiles.Zip(backupFiles)
                .All(x => File.ReadAllBytes(x.First)
                    .SequenceEqual(File.ReadAllBytes(x.Second)));
            _match.Text = m ? "clean" : "dirty";
            _name.Text = Path.GetFileName(currentFrame);
        }


        private const string Backup = @"C:\Users\marho\OneDrive\TheyAreBillions";
        private const string Saves = @"C:\Users\marho\OneDrive\Документы\My Games\They Are Billions\Saves";
        private static void LoadGame()
        {
            foreach (var f in GetSaveFiles())
                File.Delete(f);
            var frame = GetCurrentFrame();
            foreach (var f in Directory.GetFiles(frame))
                File.Copy(f, f.Replace(frame, Saves), true);
        }

        private static void Save()
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

        private static List<string> GetSaveFiles() =>
            GetCurrentSaves(4) ??
            GetCurrentSaves(2) ??
            throw new InvalidOperationException();

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

        private static void Remove()
        {
            var frame = GetCurrentFrame();
            Directory.Move(frame, Path.Combine(
                Path.GetDirectoryName(frame)!,
                "_" + Path.GetFileName(frame)));
        }

        private static string GetCurrentFrame() =>
            Directory
                .GetDirectories(Directory.GetDirectories(Backup).Last())
                .Last(d => !Path.GetFileName(d).StartsWith("_"));

        private static bool IsCurrentProcessTheyAreBillions()
        {
            var activeWindowHandle = User32.GetForegroundWindow();
            User32.GetWindowThreadProcessId(activeWindowHandle, out var threadProcessId);
            var activeProcess = Process.GetProcesses()
                .SingleOrDefault(p => p.Id == threadProcessId);
            return activeProcess?.ProcessName == "TheyAreBillions";
        }
    }
}