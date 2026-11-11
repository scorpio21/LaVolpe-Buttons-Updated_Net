using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Sample;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (args.Length == 2 && args[0].Equals("--snapshot", StringComparison.OrdinalIgnoreCase))
        {
            SaveSnapshot(args[1]);
            return;
        }

        Application.Run(new SampleForm());
    }

    private static void SaveSnapshot(string outputPath)
    {
        using var form = new SampleForm
        {
            StartPosition = FormStartPosition.Manual,
            Location = new Point(100, 100),
            ShowInTaskbar = false
        };

        form.CreateControl();
        form.Show();
        Application.DoEvents();

        using var windowBitmap = new Bitmap(form.Width, form.Height);
        using (var graphics = Graphics.FromImage(windowBitmap))
        {
            var deviceContext = graphics.GetHdc();
            try
            {
                if (!PrintWindow(form.Handle, deviceContext, PrintWindowFlags.RenderFullContent))
                    throw new InvalidOperationException("PrintWindow no pudo capturar el sample WinForms.");
            }
            finally
            {
                graphics.ReleaseHdc(deviceContext);
            }
        }

        var clientOrigin = new Point(0, 0);
        ClientToScreen(form.Handle, ref clientOrigin);
        GetWindowRect(form.Handle, out var windowRect);
        var crop = new Rectangle(
            clientOrigin.X - windowRect.Left,
            clientOrigin.Y - windowRect.Top,
            form.ClientSize.Width,
            form.ClientSize.Height);
        using var bitmap = windowBitmap.Clone(crop, PixelFormat.Format32bppArgb);
        Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(outputPath))!);
        bitmap.Save(outputPath, ImageFormat.Png);
    }

    [Flags]
    private enum PrintWindowFlags : uint
    {
        RenderFullContent = 2
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool PrintWindow(IntPtr handle, IntPtr deviceContext, PrintWindowFlags flags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr handle, out Rect rect);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ClientToScreen(IntPtr handle, ref Point point);
}
