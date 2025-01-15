using NHotkey.WindowsForms;
using Polly;


namespace UI
{
    public partial class MainForm : Form
    {
        private bool _isDirty;
        private Exception? _lastError;

        public MainForm()
        {
            var retryTillSuccess = new ResiliencePipelineBuilder().AddRetry(new()
            {
                ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                MaxRetryAttempts = int.MaxValue, // Retry indefinitely
                Delay = TimeSpan.FromMilliseconds(200),

            }).Build();

            InitializeComponent();
            HotkeyManager.Current.AddOrReplace("Error",
                Keys.F1,
                (_, _) =>
                {
                    try
                    {
                        _lastError = _lastError == null
                            ? new Exception("Error Error Error Error Error Error Error Error Error \r\nError \r\nError \r\nError \r\n")
                            : null;
                    }
                    catch (Exception x)
                    {
                        _lastError = x;
                    }
                });


            HotkeyManager.Current.AddOrReplace("Save",
                Keys.F5,
                (_, _) =>
                {
                    try
                    {
                        if (Lib.IsCurrentProcessTheyAreBillions())
                            {
                                Lib.Save();
                                _frameName.ForeColor = Color.Aqua;
                            }
                        _lastError = null;
                    }
                    catch (Exception x)
                    {
                        _lastError = x;
                    }
                });

            HotkeyManager.Current.AddOrReplace("Remove Last",
                Keys.Control | Keys.Alt | Keys.R,
                (_, _) =>
                {
                    try
                    {
                        if (Lib.IsCurrentProcessTheyAreBillions())
                            Lib.Remove();
                        _lastError = null;
                    }
                    catch (Exception x)
                    {
                        _lastError = x;
                    }
                });

            HotkeyManager.Current.AddOrReplace("LoadGame",
                Keys.F8,
                (_, _) =>
                {
                    try
                    {
                        if (Lib.IsCurrentProcessTheyAreBillions())
                            {
                                Lib.LoadGame();
                                _frameName.ForeColor = Color.Aqua;
                            }
                        _lastError = null;
                    }
                    catch (Exception x)
                    {
                        _lastError = x;
                    }
                });
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (_, _) =>
            {
                try
                {
                    RefreshState();
                    _frameName.ForeColor = _isDirty ?
                        Color.DarkSalmon : Color.AliceBlue;
                }
                catch (Exception x)
                {
                    _lastError = x;
                }
                _errorLabel.Text = _lastError?.Message ?? "";
            };
            timer.Start();
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