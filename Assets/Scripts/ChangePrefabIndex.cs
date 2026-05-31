using UnityEngine;

public class ChangePrefabIndex : MonoBehaviour
{
    [SerializeField] private ModelImageChanger currentIndex;
    [SerializeField] private PlaneTrackingMode prefab;

    public void ChangingPrefabIndex()
    {
        prefab.prefabIndex = currentIndex.currentIndex;
    }
}
