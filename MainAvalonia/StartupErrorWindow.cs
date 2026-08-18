using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace MainAvalonia
{
    public class StartupErrorWindow : Window
    {
        public StartupErrorWindow(string message)
        {
            Title = "Curse Of The Azure Bonds - Startup Error";
            Width = 640;
            Height = 300;
            CanResize = false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Content = new Border
            {
                Padding = new Thickness(24),
                Child = new TextBlock
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 15,
                },
            };
        }
    }
}
