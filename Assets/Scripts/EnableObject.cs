using UnityEngine;

public class EnableObject : MonoBehaviour
{
    private bool isOn = true;

    private void Start()
    {
        EnableThisObject(!isOn);
    }
    public void EnableThisObject(bool isOn)
    {
        gameObject.SetActive(isOn);
    }
}
