public class UIConfigurator
{
    UIManager _uiManager;
    
    public UIConfigurator(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void Configure<TUIWindow>(TUIWindow uiWindow, UIResource resource) where TUIWindow : class, IUiWindow
    {
        switch (uiWindow)
        {
            case IPresentableUiWindow<IPresenter> presentableUiWindow:
                MainMenuPresenter menuPresenter = new MainMenuPresenter(_uiManager, resource.ApplyingConfigs);
                presentableUiWindow.SetUpPresenter(menuPresenter);
                break;
            case IConfigurableUiWindow configurableUiWindow:
                configurableUiWindow.Configure(resource);
                break;
        }
    }
}