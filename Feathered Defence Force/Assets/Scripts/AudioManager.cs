using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio")]
    public static float globalAudio;
    public static float globalMusic;
    public static float globalMisc;

    public List<AudioClip> audios;
    public GameObject audioSourcePrefab;
     List<GameObject> audioSources;
     List<TimerScript> audioTimers = new List<TimerScript>();

    public AnimationCurve linearCurve;
    public AnimationCurve pickupCurve;

    public List<MusicScriptable> musics;
     List<AudioSource> musicSources = new List<AudioSource>();
    public GameObject musicAttPrefab;
    public GameObject musicSourcePrefab;
    public MusicShowcaseObjects attObject;
    public float musicAttPhaseDelay;
    public TimerScript musicTimerScript = new TimerScript(0);
    private int musicidskip;
    int musicTimerPhase;
    int musicIdSkip;
    bool musicPlaying;
    private static bool startup = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        globalAudio = Manager.globalAudio;
        globalMusic = Manager.globalMusic;
        globalMisc = Manager.globalMisc;
        InstantiateMusic();
        PlayMusic(0, 1);
        if (startup)
        {
            startup = false;
            UpdateMusicVolumes();
        }

    }

    private void Update()
    {
        globalAudio = Manager.globalAudio; 
        globalMusic = Manager.globalMusic;
        globalMisc = Manager.globalMisc;

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

        if (musicPlaying)
        {
            if (musicTimerPhase < 4)
            {
                musicTimerScript.Update();

                if (musicTimerPhase == 1)
                {
                    for (int i = 0; i < musics.Count; ++i)
                    {

                        if (i != musicidskip)
                        {
                            float startvolume = musicSources[i].volume;
                            musicSources[i].volume = Mathf.Clamp(startvolume - musicTimerScript.Progress(), 0, 1) * globalAudio * globalMusic;
                        }
                        else
                        {
                            musicSources[i].volume = Mathf.Clamp(musicTimerScript.Progress(), 0, 1 * globalAudio * globalMusic);
                        }
                    }

                    attObject.AttributionObject.position = Vector3.Lerp
                        (
                            attObject.offScreenPoint.position,
                            attObject.onScreenPoint.position,
                            linearCurve.Evaluate(musicTimerScript.Progress())
                        );
                }
                else if (musicTimerPhase == 3)
                {
                    attObject.AttributionObject.position = Vector3.Lerp
                        (
                            attObject.onScreenPoint.position,
                            attObject.offScreenPoint.position,
                            linearCurve.Evaluate(musicTimerScript.Progress())
                        );
                }

                if (musicTimerScript.Check())
                {
                    musicTimerScript.Restart();
                    musicTimerPhase++;
                }
            }
        }
        #endregion
    }

    #region audio
    public static void PlayAudio(int audioID, float volume = 1)
    {
        volume = Mathf.Clamp01(volume);
        volume *= globalAudio * globalMisc;
        GameObject audioGameObject = Instantiate(instance.audioSourcePrefab, instance.transform.position, Quaternion.identity, instance.transform);
        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();

        audioSource.clip = instance.audios[audioID];
        instance.audioTimers.Add(new TimerScript(instance.audios[audioID].length));

        instance.audioSources.Add(audioGameObject);
        audioSource.volume = volume;
        audioSource.Play();
    }

    public static void PlayMusic(int musicID, float musicVolume = 1)
    {
        if (instance.musicIdSkip != musicID)
        {
            instance.musicPlaying = true;

            instance.musicIdSkip = musicID;

            instance.attObject.TextAttributionObject.text = instance.musics[musicID].musicName;
            instance.musicTimerScript.Start(instance.musicAttPhaseDelay);
            instance.musicTimerPhase = 1;
        }
    }

    private void InstantiateMusic()
    {
        for (int i = 0; i < musics.Count; i++)
        {
            GameObject newSource = Instantiate(musicSourcePrefab, transform.position, Quaternion.identity, transform);
            AudioSource audioSource = newSource.GetComponent<AudioSource>();
            audioSource.clip = musics[i].musicClip;
            audioSource.Play();
            audioSource.volume = 0;
            musicSources.Add(audioSource);
        }
    }

    public static void UpdateMusicVolumes()
    {
        for (int i = 0; i < instance.musics.Count; i++)
        {
            if (i == instance.musicidskip)
            {
                instance.musicSources[i].volume = 1 * globalAudio * globalMusic;
            }
        }

    }
    #endregion
}