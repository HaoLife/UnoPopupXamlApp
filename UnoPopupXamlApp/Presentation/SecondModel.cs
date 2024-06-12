using System.Xml.Linq;
using Uno.Extensions.Navigation.Regions;
using UnoPopupXamlApp.Extensions;

namespace UnoPopupXamlApp.Presentation;

public partial record SecondModel(Entity Entity, IRegion Region)
{
    public async Task GoToSecond()
    {
        await Region.ShowTotasAsync($"测试内容 {DateTime.Now}", title: "测试标题");


    }
}
