namespace AlagaTrack;

static class Program
{
    /// <summary>
    ///  The main entry point for the app
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Use predictable legacy scaling for fixed-coordinate WinForms layouts.
        Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new LoginPage());
    }    
}