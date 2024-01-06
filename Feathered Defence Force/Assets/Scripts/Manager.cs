using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class Manager : MonoBehaviour
{
	public static Manager instance;

	public int playerMoney;
	public TextMeshProUGUI moneytext;
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
	public List<AudioSource> musicSources;
	public GameObject musicAttPrefab;
	public GameObject musicSourcePrefab;
	public MusicShowcaseObjects attObject;
	public float musicAttPhaseDelay;
	public TimerScript musicTimerScript = new TimerScript(0);
	int musicTimerPhase;
	int musicidskip;
	bool musicPlaying;

	public int chimataStockManipulation = 0;
	#endregion

	#region ParticalSystem
	[Header("Particles")]
	public List<GameObject> particlePrefabs;
	public List<ParticleSystem> particleSystems;
	public List<TimerScript> particleTimers;
	#endregion

	#region misc
	public float bossSize, enemySize;
	#endregion

	private void Awake()
	{
		instance = this;
	}

	void Start()
	{
		//start all 3 of the musics
		InstanstiateMusic();
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
							musicSources[i].volume = (1 - musicTimerScript.Progress()) * globalAudio;
						}
						else
						{
							musicSources[i].volume = musicTimerScript.Progress();
						}
					}

					attObject.AttributionObject.position = Vector3.Lerp
						(
							attObject.offScreenPoint.position,
							attObject.onScreenPoint.position,
							linearCurve.Evaluate(musicTimerScript.Progress())
						);
				}
				if (musicTimerPhase == 3)
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

		#region Particle
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
		#endregion
	}

	#region audio
	public static void PlayAudio(int mID, float volume = 1)
	{
		volume = Mathf.Clamp01(volume);
		volume *= instance.globalAudio;
		GameObject mGameObject = Instantiate(instance.audioSourcePreFab, instance.transform.position, Quaternion.identity, instance.transform);
		AudioSource mAudioSource = mGameObject.GetComponent<AudioSource>();

		//diff
		mAudioSource.clip = instance.audios[mID];
		instance.audioTimers.Add(new TimerScript(instance.audios[mID].length));

		instance.audioSources.Add(mGameObject);
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
		volume *= instance.globalAudio;
		GameObject mGameObject = Instantiate(instance.audioSourcePreFab, instance.transform.position, Quaternion.identity, instance.transform);
		AudioSource mAudioSource = mGameObject.GetComponent<AudioSource>();

		//diff
		mAudioSource.clip = clip;
		instance.audioTimers.Add(new TimerScript(clip.length));

		instance.audioSources.Add(mGameObject);
		mAudioSource.volume = volume;
		mAudioSource.Play();
	}

	public static void PlayMusic(int mID, float mVolume = 1)
	{
		mVolume = Mathf.Clamp01(mVolume);
		mVolume *= instance.globalAudio;

		instance.musicPlaying = true;

		for (int i = 0; i < instance.musicSources.Count; i++)
		{
			instance.musicSources[i].volume = 0;
		}
		instance.musicSources[mID].volume = mVolume;
		instance.musicSources[mID].Play();
		instance.musicidskip = mID;

		instance.attObject.TextAttributionObject.text = instance.musics[mID].musicName;
		instance.musicTimerScript.Start(instance.musicAttPhaseDelay);
		instance.musicTimerPhase = 1;
	}

	private void InstanstiateMusic()
	{
		for (int i = 0; i < musics.Count; i++)
		{
			GameObject newsource = Instantiate(musicSourcePrefab, transform.position, Quaternion.identity, transform);
			AudioSource audioSource = newsource.GetComponent<AudioSource>();
			audioSource.clip = musics[i].musicClip;

			musicSources.Add(audioSource);
		}
	}

	public static void AddMonye(int monye)
	{
		if (UnityEngine.Random.Range(0, 101) <= 10)
		{
			monye += instance.chimataStockManipulation;
		}
		instance.playerMoney += monye;
		instance.moneytext.text = instance.playerMoney.ToString();
	}
	#endregion

	#region Particle
	public static void PlayParticle(int mID, Vector3 mPosition)
	{
		GameObject mObject = Instantiate(instance.particlePrefabs[mID], mPosition, Quaternion.Euler(new Vector3(180, 0, 0)), instance.transform);
		ParticleSystem mParticleSystem = mObject.GetComponent<ParticleSystem>();

		instance.particleSystems.Add(mParticleSystem);
		instance.particleTimers.Add(new TimerScript(mParticleSystem.main.startLifetime.constantMax));

	}
	#endregion
}
