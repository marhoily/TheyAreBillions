using System.Diagnostics;
using Vanara.PInvoke;

namespace UI;

public static class Lib
{
    private const string Backup = @"C:\Users\marho\OneDrive\TheyAreBillions";
    private const string Saves = @"C:\Users\marho\OneDrive\Документы\My Games\They Are Billions\Saves";

    public static void LoadGame()
    {
        try
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
        catch
        {
            // ignore
        }

    }

    public static void Save()
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

    public static List<string>? GetSaveFiles() =>
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

    public static void Remove()
    {
        var frame = GetCurrentFrame();
        if (frame == null) return;
        Directory.Move(frame, Path.Combine(
            Path.GetDirectoryName(frame)!,
            "_" + Path.GetFileName(frame)));
    }

    public static string? GetCurrentFrame()
    {
        var last = Directory.GetDirectories(Backup).Last();
        return Directory
            .GetDirectories(last)
            .LastOrDefault(d => !Path.GetFileName(d).StartsWith("_"));
    }

    public static bool IsCurrentProcessTheyAreBillions()
    {
        var activeWindowHandle = User32.GetForegroundWindow();
        User32.GetWindowThreadProcessId(activeWindowHandle, out var threadProcessId);
        var activeProcess = Process.GetProcesses()
            .SingleOrDefault(p => p.Id == threadProcessId);
        return activeProcess?.ProcessName == "TheyAreBillions";
    }
}