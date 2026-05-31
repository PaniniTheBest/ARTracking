using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ClearObjectsInScene : MonoBehaviour
{
    public void DestroyObjectsByLayerName(int targetLayer)
    {
        // Find every active GameObject currently in the scene
        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == targetLayer)
            {
                // Skip AR planes
                if (obj.GetComponent<ARPlane>() != null) continue;

                Destroy(obj);
            }
        }
    }
}
