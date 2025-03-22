using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour, IUiWindow
{
    
    public void Show()
    {
        Debug.Log($"Showed menu {this.name}");
    }
}