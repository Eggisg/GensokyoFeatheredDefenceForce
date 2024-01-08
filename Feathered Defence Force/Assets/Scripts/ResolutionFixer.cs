using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResolutionFixer : MonoBehaviour
{
    // Set the desired aspect ratio (16:9)
    private float targetAspectRatio = 16f / 9f;

    private void Start()
    {
        // Call the method once at the beginning to set the initial resolution
        Screen.SetResolution(1920, 1080, Screen.fullScreen); //suck shit other resolution people, you dont exist
        Screen.fullScreen = true;
    }

    private void Update()
    {

    }
    private void AdjustResolution()
    {
        // Calculate the current aspect ratio
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Check if the current aspect ratio is different from the target
        if (currentAspectRatio != targetAspectRatio)
        {
            // Calculate the new width based on the target aspect ratio
            int newWidth = Mathf.RoundToInt(Screen.height * targetAspectRatio);

            // Set the new resolution
            Screen.SetResolution(newWidth, Screen.height, Screen.fullScreen);
        }
        
    }
}
