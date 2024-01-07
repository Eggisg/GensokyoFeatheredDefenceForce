using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

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
    int musicTimerPhase;
    int musicIdSkip;
    bool musicPlaying;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        globalAudio = Manager.globalAudio;
        globalMusic = Manager.globalMusic;
        globalMisc = Manager.globalMisc;
        InstantiateMusic();
        PlayMusic(0, 1);

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
                        if (i != musicIdSkip)
                        {
                            float startVolume = musicSources[i].volume;
                            musicSources[i].volume = Mathf.Clamp(startVolume - musicTimerScript.Progress(), 0, 1) * globalAudio;
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
                            Instance.linearCurve.Evaluate(musicTimerScript.Progress())
                        );
                }
                else if (musicTimerPhase == 3)
                {
                    attObject.AttributionObject.position = Vector3.Lerp
                        (
                            attObject.onScreenPoint.position,
                            attObject.offScreenPoint.position,
                            Instance.linearCurve.Evaluate(musicTimerScript.Progress())
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
        GameObject audioGameObject = Instantiate(Instance.audioSourcePrefab, Instance.transform.position, Quaternion.identity, Instance.transform);
        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();

        audioSource.clip = Instance.audios[audioID];
        Instance.audioTimers.Add(new TimerScript(Instance.audios[audioID].length));

        Instance.audioSources.Add(audioGameObject);
        audioSource.volume = volume;
        audioSource.Play();
    }

    public static void PlayMusic(int musicID, float musicVolume = 1)
    {
        if (Instance.musicIdSkip != musicID)
        {
            Instance.musicPlaying = true;

            Instance.musicIdSkip = musicID;

            Instance.attObject.TextAttributionObject.text = Instance.musics[musicID].musicName;
            Instance.musicTimerScript.Start(Instance.musicAttPhaseDelay);
            Instance.musicTimerPhase = 1;
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
    #endregion
}