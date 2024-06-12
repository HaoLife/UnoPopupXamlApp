using Uno.Extensions.Navigation.Regions;
using UnoPopupXamlApp.Extensions;

namespace UnoPopupXamlApp.Presentation;

public partial record MainModel
{
    private INavigator _navigator;
    private readonly IDispatcher dispatcher;
    private readonly IThemeService themeService;
    private readonly IRegion _region;

    public MainModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator
        , IDispatcher dispatcher
        , IThemeService themeService
        , IRegion region)
    {
        _navigator = navigator;
        this.dispatcher = dispatcher;
        this.themeService = themeService;
        this._region = region;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
    }

    public string? Title { get; }

    public IState<string> Name => State<string>.Value(this, () => string.Empty);

    public async Task GoToSecond()
    {
        var name = await Name;
        await _navigator.NavigateViewModelAsync<SecondModel>(this, data: new Entity(name!));

        //await _region.ShowTotasAsync("测试内容");

    }

    public async Task SwitchTheme()
    {
        await this.themeService.SetThemeAsync(this.themeService.IsDark ? AppTheme.Light : AppTheme.Dark);
    }

}
