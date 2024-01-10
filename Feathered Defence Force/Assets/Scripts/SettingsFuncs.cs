using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsFuncs : MonoBehaviour
{
    public UnityEngine.UI.Slider sliderGlobal, sliderMusic, sliderMisc;
    [SerializeField] private bool debug;
    private void Start()
    {
        if (debug)
        {
            ChangeGlobal(1);
            ChangeMusic(1);
            ChangeMisc(1);
        }

    }

    public void ChangeGlobal(float amount)
    {
        
        Manager.globalAudio = amount;
    }
    public void ChangeGlobal() 
    {
        Manager.globalAudio = sliderGlobal.value;
        if (Manager.instance != null)
        {
            Manager.UpdateMusicVolumes();
        }
        AudioManager.UpdateMusicVolumes();
        
    }
    public void ChangeMusic(float amount)
    {
        Manager.globalMusic = amount;
    }
    public void ChangeMusic() 
    {
        Manager.globalAudio = sliderGlobal.value;
        if (Manager.instance != null)
        {
            Manager.UpdateMusicVolumes();
        }
        AudioManager.UpdateMusicVolumes();
    }
    public void ChangeMisc(float amount)
    {
        Manager.globalMisc = amount;
    }
    public void ChangeMisc()
    {
        Manager.globalMisc = sliderMisc.value;
    }
}
