using UnityEngine;

public class SkinColorController : MonoBehaviour
{
    [SerializeField] public float intensity = 1.0f;
    [SerializeField] public Color changerColor;
    [SerializeField] private SkinnedMeshRenderer colorRenderer;

    private void Start()
    {
        changerColor *= intensity;
        //colorRenderer.material.EnableKeyword("_MAINCOLOR");

    }
    private void Update()
    {
        if (colorRenderer == null) return;
        else
        {
            colorRenderer.material.SetColor("_MainColor", changerColor);
        }
    }
}
