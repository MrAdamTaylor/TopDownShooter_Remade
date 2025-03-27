using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AuthorsMenu : MonoBehaviour, IConfigurableUiWindow
{
    [SerializeField] private Text _text;
    [SerializeField] private Button _button;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Configure(UIResource resource)
    {
        ScriptableObject config = resource.ApplyingConfigs.First();
        
        if (config is not GameAuthorsConfig authorsConfig)
        {
            throw new InvalidDataException($"{nameof(authorsConfig)} must be {nameof(GameAuthorsConfig)}");
        }

        _text.text = authorsConfig.Text;
        _button.onClick.AddListener(Hide);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnDestroy()
    {
        
    }
}
