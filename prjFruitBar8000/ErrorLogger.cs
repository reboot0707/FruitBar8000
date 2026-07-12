using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public static class ErrorLogger
{
    public static void Log(Exception ex)
    {
        try
        {
            string logDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "FruitBar8000",
                "Logs");

            Directory.CreateDirectory(logDirectory);

            string logPath = Path.Combine(
                logDirectory,
                $"error-{DateTime.Now:yyyy-MM-dd}.log");

            string content =
                $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]{Environment.NewLine}" +
                $"Type: {ex.GetType().FullName}{Environment.NewLine}" +
                $"Message: {ex.Message}{Environment.NewLine}" +
                $"StackTrace:{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}" +
                new string('-', 80) +
                Environment.NewLine;

            File.AppendAllText(logPath, content, Encoding.UTF8);
        }
        catch
        {
            // 避免「記錄錯誤時又發生錯誤」覆蓋原本的例外。
            return;
        }
    }

    public static void Show(Exception ex)
    {
        Log(ex);

        MessageBox.Show(
            "程式執行時發生錯誤，詳細資訊已寫入錯誤紀錄。",
            "執行錯誤",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
    }
}