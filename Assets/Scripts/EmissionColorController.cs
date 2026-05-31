using UnityEngine;

public class EmissionColorController : MonoBehaviour
{
    [SerializeField] public float intensity = 1.5f;
    [SerializeField] public Color changerColor;
    [SerializeField] private Renderer colorRenderer;

    private void Start()
    {
        changerColor *= intensity;
        colorRenderer.material.EnableKeyword("_EMISSION");
    }
    private void Update() 
    {
        if (colorRenderer == null) return;
        else 
        {
            colorRenderer.material.SetColor("_EmissionColor", changerColor);
        }
    }
}
