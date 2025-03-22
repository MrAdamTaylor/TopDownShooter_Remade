public interface IUiWindow
{
    void Show(IPresenter presenter = null);
    void Configure(UIResource resource);
}