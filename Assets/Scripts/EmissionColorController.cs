using UnityEngine;

public class EmissionColorController : MonoBehaviour
{
    [SerializeField] private float intensity = 2.5f;
    [SerializeField] public Color changerColor;
    [SerializeField] private Renderer colorRenderer;

    private void Start()
    {
        changerColor *= intensity;
        colorRenderer.material.EnableKeyword("_EMISSION");
    }
    private void Update()
    {
        colorRenderer.material.SetColor("_EmissionColor", changerColor);
    }
}
