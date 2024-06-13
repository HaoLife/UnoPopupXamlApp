using Microsoft.UI;
using Uno.Extensions.Navigation.Regions;
using Uno.UI;
using Uno.UI.Extensions;

namespace UnoPopupXamlApp.Extensions;
public static class RegionExtensions
{
    public static async ValueTask ShowTotasAsync(this IRegion that, string content, string title = null, long millisecond = 2000, InfoBarSeverity severity = InfoBarSeverity.Informational, bool isClosable = false)
    {
        var dispatcher = that.Services.GetRequiredService<IDispatcher>();
        var alertName = "alert";

        await dispatcher.ExecuteAsync(async () =>
        {
            var window = that.Services.GetRequiredService<Window>();
            var shell = window.Content as FrameworkElement;
            if (shell == null) throw new Exception("Window.Content 必须继承 FrameworkElement");

            //var shell = window.Content as Shell;
            if (!(shell.FindName(alertName) is StackPanel alert))
            {
                var root = shell.FindFirstDescendant<Grid>();
                if (root == null) throw new Exception("Window.Content 的子元素必须包含 Grid 布局");

                root.Children.Add(new StackPanel()
                {
                    Name = alertName,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Bottom,

                    Spacing = 5,
                    Margin = new Thickness(10),
                });

                alert = shell.FindName(alertName) as StackPanel;
            }




            var name = Guid.NewGuid().ToString();
            var infobar = new InfoBar()
            {
                Name = name,
                Message = content,
                Title = title,
                IsClosable = isClosable,
                IsOpen = true,
                Severity = severity,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            alert.Children.Add(infobar);


            await Task.Delay(TimeSpan.FromMilliseconds(millisecond));

            alert.Children.Remove(a => a.Equals(infobar));

        });
    }
}
