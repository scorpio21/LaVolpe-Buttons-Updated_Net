using System;
using System.Windows.Forms;

namespace LaVolpe.Buttons.Sample;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new SampleForm());
    }
}
