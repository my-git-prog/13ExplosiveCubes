using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}
