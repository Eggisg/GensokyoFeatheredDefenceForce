using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager manager;



    public List<Transform> waypoints;
    public AnimationCurve curve;
    public AnimationCurve linearCurve;
    public AnimationCurve pickupCurve;

    #region audio
    [Header("Audio")]
    public float globalAudio;
    public List<AudioClip> audios;
    public GameObject audioSourcePreFab;
    public List<GameObject> audioSources;
    public List<TimerScript> audioTimers;

    public List<MusicScriptable> musics;
    #endregion

    #region ParticalSystem
    [Header("Particles")]
    public List<GameObject> particlePrefabs;
    public List<ParticleSystem> particleSystems;
    public List<TimerScript> particleTimers;
    #endregion

    private void Awake()
    {
        manager = this; 
    }
    void Start()
    {
        
    }

    void Update()
    {
        #region audio
        for (int i = 0; i < audioTimers.Count; ++i)
        {
            audioTimers[i].Update();

            if (audioTimers[i].Check())
            {
                Destroy(audioSources[i]);
                audioSources.RemoveAt(i);
                audioTimers.RemoveAt(i);
            }
        }
        #endregion
        for (int i = 0; i < particleTimers.Count; ++i)
        {
            particleTimers[i].Update();

            if (particleTimers[i].Check())
            {
                Destroy(particleSystems[i].gameObject);
                particleSystems.RemoveAt(i);
                particleTimers.RemoveAt(i);
            }
        }

    }

    #region audio
    public static void PlayAudio(int mID, float volume = 1)
    {
        volume = Mathf.Clamp01(volume);
        volume *= manager.globalAudio;
        GameObject mGameObject = Instantiate(manager.audioSourcePreFab, manager.transform.position, Quaternion.identity, manager.transform);
        AudioSource mAudioSource = mGameObject.GetComponent<AudioSource>();

        //diff
        mAudioSource.clip = manager.audios[mID];
        manager.audioTimers.Add(new TimerScript(manager.audios[mID].length));

        manager.audioSources.Add(mGameObject);
        mAudioSource.volume = volume;
        mAudioSource.Play();

    }


    /// <summary>
    /// plays a requested audioclip on a new object
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="volume"></param>
    public static void PlayAudio(AudioClip clip, float volume = 1)
    {
        volume = Mathf.Clamp01(volume);
        volume *= manager.globalAudio;
        GameObject mGameObject = Instantiate(manager.audioSourcePreFab, manager.transform.position, Quaternion.identity, manager.transform);
        AudioSource mAudioSource = mGameObject.GetComponent<AudioSource>();


        //diff
        mAudioSource.clip = clip;
        manager.audioTimers.Add(new TimerScript(clip.length));


        manager.audioSources.Add(mGameObject);
        mAudioSource.volume = volume;
        mAudioSource.Play();

    }

    public static void PlayMusic(int mID, float volume = 1)
    {
        PlayAudio(manager.musics[mID].musicClip, volume);

    }

    #endregion

    #region Particle
    public static void PlayParticle(int mID, Vector3 mPosition)
    {
        GameObject mObject = Instantiate(manager.particlePrefabs[mID], mPosition, Quaternion.Euler(new Vector3(180, 0, 0)), manager.transform);
        ParticleSystem mParticleSystem = mObject.GetComponent<ParticleSystem>();

        manager.particleSystems.Add(mParticleSystem);
        manager.particleTimers.Add(new TimerScript(mParticleSystem.main.startLifetime.constantMax));

    }
    #endregion
}
