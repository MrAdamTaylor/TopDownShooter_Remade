
using UnityEngine;

public static class TransformUtility 
{
    /// <summary>
    /// Удаляет все дочерние объекты, не трогая родительский.
    /// </summary>
    public static void DestroyAllChildren(this Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(parent.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// Сбрасывает локальный трансформ.
    /// </summary>
    public static void ResetLocal(this Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
    }
}
