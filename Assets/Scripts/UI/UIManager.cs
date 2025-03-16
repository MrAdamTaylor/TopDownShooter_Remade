
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static class BundleConstants
    {
        
    }
    
    
    private BundleSystem _bundleSystem;
    private Dictionary<string, GameObject> _uiPrefabs;

    public void Initialize(BundleSystem bundleSystem)
    {
        _bundleSystem = bundleSystem;
        _uiPrefabs = new Dictionary<string, GameObject>();
    }


    public void GetOrCreate(string bundleName)
    {
        Debug.Log($"Try Get or Create bundle name {bundleName}");
        try
        {
            if (_bundleSystem == null)
            {
                throw new NullReferenceException("Bundle system is null");
            }

            if (_bundleSystem.IsEmpty)
            {
                throw new InvalidOperationException("Ресурсы не загружены. Словарь бандлов пуст.");
            }

            if (!_bundleSystem.IsExistBundle(bundleName))
            {
                throw new KeyNotFoundException($"Ресурс с ключом для '{bundleName}' не найден.");
            }

            UIResource resource = _bundleSystem.GetResource(bundleName);
            
            //Это надо заранее закешировать и загрузить асинхронно!
            GameObject prefab = Resources.Load<GameObject>(resource.Path);
            
            GameObject instance = Instantiate(prefab, this.transform, false);
            Debug.Log($"Создание бандла прошло успешно!");
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError($"Ошибка: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            // Обработка ошибок, связанных с пустым словарем
            Debug.LogError($"Ошибка: {ex.Message}");
        }
        catch (KeyNotFoundException ex)
        {
            // Обработка ошибок, связанных с отсутствием ключа
            Debug.LogError($"Ошибка: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Обработка других неожиданных ошибок
            Debug.LogError($"Неизвестная ошибка: {ex.Message}");
        }
        
    }
}
