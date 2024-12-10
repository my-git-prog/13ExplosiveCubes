using System;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public event Action Clicked;
    private void OnMouseUpAsButton()
    {
        Clicked?.Invoke();
    }
}
