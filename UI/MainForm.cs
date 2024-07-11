using NHotkey.WindowsForms;
using Polly;
using Shared;

namespace UI
{
    public partial class MainForm : Form
    {
        private bool _isDirty;

        public MainForm()
        {
            var retryTillSuccess = new ResiliencePipelineBuilder().AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                MaxRetryAttempts = int.MaxValue, // Retry indefinitely
                Delay = TimeSpan.FromMilliseconds(200),

            }).Build();

            var ssm = new SaveStateManager(
                () => _isDirty, UpdateColor, 
                () => retryTillSuccess.Execute(Lib.Save));

            InitializeComponent();

            HotkeyManager.Current.AddOrReplace("Save",
                Keys.F5,
                (_, _) =>
                {
                    if (Lib.IsCurrentProcessTheyAreBillions())
                        ssm.RequestSave();
                });

            HotkeyManager.Current.AddOrReplace("Remove Last",
                Keys.Control | Keys.Alt | Keys.R,
                (_, _) =>
                {
                    if (Lib.IsCurrentProcessTheyAreBillions())
                        Lib.Remove();
                });

            HotkeyManager.Current.AddOrReplace("LoadGame",
                Keys.F8,
                (_, _) =>
                {
                    if (Lib.IsCurrentProcessTheyAreBillions())
                        Lib.LoadGame();
                });
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (_, _) =>
            {
                RefreshState();
                ssm.Tick();
            };
            timer.Start();
        }

        private void UpdateColor(SaveStateManager.ColorCode color)
        {
            _frameName.ForeColor = color switch
            {
                SaveStateManager.ColorCode.Clean => Color.AliceBlue,
                SaveStateManager.ColorCode.SaveRequested => Color.Aqua,
                SaveStateManager.ColorCode.Dirty => Color.DarkSalmon,
                _ => Color.Red
            };
        }

        private void RefreshState()
        {
            var saveFiles = Lib.GetSaveFiles()?.Take(2);
            if (saveFiles == null)
            {
                _folderName.Text = @"<N/A>";
                _isDirty = false;
                _frameName.Text = "";
                return;
            }
            var currentFrame = Lib.GetCurrentFrame();
            if (currentFrame == null)
            {
                _isDirty = false;
                var frame = saveFiles.First();
                _frameName.Text = Path.GetFileNameWithoutExtension(frame);
                return;
            }
            _folderName.Text = Path.GetFileName(Path.GetDirectoryName(currentFrame));
            var backupFiles = Directory.GetFiles(currentFrame);
            _isDirty = !saveFiles.Zip(backupFiles)
                .All(x => File.ReadAllBytes(x.First)
                    .SequenceEqual(File.ReadAllBytes(x.Second)));
            _frameName.Text = Path.GetFileName(currentFrame);
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