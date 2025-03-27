using System;

public interface IUiWindow
{
    void Show();

}

public interface IPresentableUiWindow<TPresenter> : IUiWindow where TPresenter : IPresenter
{
    void SetUpPresenter(TPresenter presenter);
}

public interface IConfigurableUiWindow : IUiWindow
{
    void Configure(UIResource resource);
}

