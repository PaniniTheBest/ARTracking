using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for the Image component

public class ModelImageChanger : MonoBehaviour
{
    [Header("References")]
    public Image targetImage;       // Drag your UI Image component here
    public Sprite[] imageList;      // Drag your multiple Sprites here

    public int currentIndex = 0;

    // Call this method via a UI Button OnClick() event
    public void ShowNextImage()
    {
        if (imageList.Length == 0 || targetImage == null) return;

        // Advance to the next index and loop back to 0 if at the end
        currentIndex = (currentIndex + 1) % imageList.Length;
        // Assign the new sprite
        targetImage.sprite = imageList[currentIndex];
        Debug.Log($"current Index : {currentIndex}");
    }
}
