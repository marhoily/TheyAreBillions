using NHotkey.WindowsForms;
using System.Diagnostics;
using System.Windows.Forms;
using Vanara.PInvoke;


ApplicationConfiguration.Initialize();

Application.Run(new WinFormsApp1.Form1());


//keyboardHookManager.RegisterHotkey(
//    0x60, () =>
//{

//});
//keyboardHookManager.Start();

Console.ReadLine();