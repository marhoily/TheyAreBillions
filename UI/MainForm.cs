using System.Diagnostics;
using NHotkey.WindowsForms;
using Polly;
using Vanara.PInvoke;

namespace UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            var retryTillSuccess = new ResiliencePipelineBuilder().AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                MaxRetryAttempts = int.MaxValue, // Retry indefinitely
                Delay = TimeSpan.FromMilliseconds(200),

            }).Build();
            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Save",
                Keys.F5,
                (_, _) =>
                {
                    if (IsCurrentProcessTheyAreBillions())
                        retryTillSuccess.Execute(Save);
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
            var saveFiles = GetSaveFiles()?.Take(2);
            if (saveFiles == null)
            {
                _folderName.Text = @"<N/A>";
                MarkClean();
                _frameName.Text = "";
                return;
            }
            var currentFrame = GetCurrentFrame();
            if (currentFrame == null)
            {
                MarkClean();
                var frame = saveFiles.First();
                _frameName.Text = Path.GetFileNameWithoutExtension(frame);
                return;
            }
            _folderName.Text = Path.GetFileName(Path.GetDirectoryName(currentFrame));
            var backupFiles = Directory.GetFiles(currentFrame);
            var m = saveFiles.Zip(backupFiles)
                .All(x => File.ReadAllBytes(x.First)
                    .SequenceEqual(File.ReadAllBytes(x.Second)));
            if (m) MarkClean(); else MarkDirty();
            _frameName.Text = Path.GetFileName(currentFrame);
        }

        private void MarkDirty()
        {
            _isDirty.Text = "Dirty";
            _frameName.ForeColor = Color.DarkSalmon;
        }

        private void MarkClean()
        {
            _isDirty.Text = "Clean";
            _frameName.ForeColor = Color.AliceBlue;
        }


        private const string Backup = @"C:\Users\marho\OneDrive\TheyAreBillions";
        private const string Saves = @"C:\Users\marho\OneDrive\Документы\My Games\They Are Billions\Saves";
        private static void LoadGame()
        {
            var saveFiles = GetSaveFiles();
            if (saveFiles != null)
                foreach (var f in saveFiles)
                    File.Delete(f);
            var frame = GetCurrentFrame();
            if (frame == null) return;
            foreach (var f in Directory.GetFiles(frame))
                File.Copy(f, f.Replace(frame, Saves), true);
        }

        private static void Save()
        {
            var files = GetSaveFiles();
            if (files == null) return;
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

        private static List<string>? GetSaveFiles() =>
            GetCurrentSaves(4) ??
            GetCurrentSaves(2);

        private static List<string>? GetCurrentSaves(int num)
        {
            var enumerateFiles = Directory.EnumerateFiles(Saves)
                .ToArray();
            var result = enumerateFiles
                .Where(f => !f.EndsWith("steam_autocloud.vdf"))
                .TakeLast(num)
                .ToList();
            if (result.Count == 0) return null;
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
            if (frame == null) return;
            Directory.Move(frame, Path.Combine(
                Path.GetDirectoryName(frame)!,
                "_" + Path.GetFileName(frame)));
        }

        private static string? GetCurrentFrame()
        {
            var last = Directory.GetDirectories(Backup).Last();
            return Directory
                .GetDirectories(last)
                .LastOrDefault(d => !Path.GetFileName(d).StartsWith("_"));
        }

        private static bool IsCurrentProcessTheyAreBillions()
        {
            var activeWindowHandle = User32.GetForegroundWindow();
            User32.GetWindowThreadProcessId(activeWindowHandle, out var threadProcessId);
            var activeProcess = Process.GetProcesses()
                .SingleOrDefault(p => p.Id == threadProcessId);
            return activeProcess?.ProcessName == "TheyAreBillions";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Location = new Point(8960, 2525);
            TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
        }
    }
}