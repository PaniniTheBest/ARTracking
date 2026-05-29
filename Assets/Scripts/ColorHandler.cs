using UnityEngine;

public class ColorHandler : MonoBehaviour
{
    [SerializeField] public Color changerColor;
    [SerializeField] private MeshRenderer colorRenderer;
    private void Update()
    {
        colorRenderer.material.color = changerColor;
    }
}
