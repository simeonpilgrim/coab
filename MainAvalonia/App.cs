using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Themes.Fluent;

namespace MainAvalonia
{
    public class App : Application
    {
        public override void Initialize()
        {
            Name = "Curse of the Azure Bonds";
            Styles.Add(new FluentTheme());
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = Program.StartupError == null
                    ? new MainWindow()
                    : new StartupErrorWindow(Program.StartupError);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
