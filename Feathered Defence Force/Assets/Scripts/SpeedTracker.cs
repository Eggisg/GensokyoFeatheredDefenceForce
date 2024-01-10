using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedTracker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedText;
    private void Start()
    {
        SpeedHandlerScript.AddFunction(UpdateText);
    }
    private void UpdateText()
    {
        speedText.text = $"Speed: {Time.timeScale}x";
    }
}
