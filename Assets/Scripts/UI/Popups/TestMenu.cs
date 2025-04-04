
using UnityEngine;

public class TestMenu : MonoBehaviour, IConfigurableUiWindow
{
   
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Configure(UIResource resource)
    {
        gameObject.SetActive(true);
    }
}
