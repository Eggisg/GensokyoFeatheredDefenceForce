using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public Wave wave;
    public int waveIndex = 0;
    int spawnedEnemies = 0;
    public static float speedboost = 1;
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bossesPrefabs;
    public GameObject nextwaveButton;
    public TextMeshProUGUI enemiesText;
    public GameObject enemyContainer;
    public float multiplierspeed;

    [SerializeField] private List<NewEnemy> enemies = new List<NewEnemy>();
    [SerializeField] private List<NewEnemy> bossList = new List<NewEnemy>();
    [SerializeField] private List<NewEnemy> proportioned = new List<NewEnemy>();

    public float delay;
    TimerScript timer;
    bool spawning = false;
    bool waveactive = true;
    public bool debug = false;

    WaveManager instance;

    private void Awake()
    {
        instance = this;        
    }
    private void Start()
    {
        wave = Instantiate(wave);
        timer = new TimerScript(delay);
    }

    private void Update()
    {

        if (debug)
        {
            StartSpawning();
            debug = false;
        }

        if (waveactive)
        {
            enemiesText.text = $"Enemies - {GetRemainingEnemyCount()}";

            if (spawning)
            {
                timer.Update();
                if (timer.Check())
                {
                    timer.Start(delay * multiplierspeed);

                    if (spawnedEnemies < proportioned.Count)
                    {
                        proportioned[spawnedEnemies].gameObject.SetActive(true);
                        spawnedEnemies += 1;
                    }
                    else
                    {
                        spawning = false;
                    }
                }
            }
            else if (IsProportionedListEmpty())
            {
                waveactive = false;
                Manager.PlayMusic(3);
                nextwaveButton.SetActive(true);
            }
        }
    }

    public void RandomizeList()
    {
        enemies.Clear();
        bossList.Clear();

        System.Random random = new System.Random();
        int randomInt;

        for (int i = 0; i < wave.enemies; i++)
        {
            randomInt = random.Next(enemyPrefabs.Count);
            NewEnemy enemy = Instantiate(enemyPrefabs[randomInt], enemyContainer.transform).GetComponent<NewEnemy>();
            enemy.gameObject.SetActive(false);  // Set the initial state to inactive
            enemies.Add(enemy);
        }

        for (int i = 0; i < wave.bosses; i++)
        {
            randomInt = random.Next(bossesPrefabs.Count);
            NewEnemy boss = Instantiate(bossesPrefabs[randomInt], enemyContainer.transform).GetComponent<NewEnemy>();
            boss.gameObject.SetActive(false);  // Set the initial state to inactive
            bossList.Add(boss);
        }
    }

    private void RunProportionalInstantiation()
    {
        proportioned.Clear();

        int totalItems = enemies.Count + bossList.Count;
        float enemiesProportion = (float)enemies.Count / totalItems;

        for (int i = 0; i < totalItems; i++)
        {
            if (i < enemies.Count)
            {
                proportioned.Add(enemies[i]);
            }
            else
            {
                proportioned.Add(bossList[i - enemies.Count]);
            }
        }

        // Shuffle the proportioned list what is chatgpt smoking
        ShuffleList(proportioned);
    }

    // Helper method to shuffle a list
    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        System.Random rng = new System.Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public void StartSpawning()
    {
        waveIndex += 1;
        spawnedEnemies = 0;
        spawning = true;
        waveactive = true;
        nextwaveButton.SetActive(true);
        Destroy(enemyContainer);
        enemyContainer = new GameObject();
        RandomizeList();
        RunProportionalInstantiation();
        UpdateEnemyBossCounts(waveIndex);
        Manager.PlayMusic(4, 1);
    }

    private void UpdateEnemyBossCounts(int runAmount)
    {
        wave.enemies += 3 * runAmount;
        wave.bosses += runAmount;
        runAmount += runAmount * (int)(runAmount * 0.4f);
        speedboost += 0.1f;
        delay -= delay * 0.08f;
        
    }
    private bool IsProportionedListEmpty()
    {
        foreach (var item in proportioned)
        {
            if (item != null)
            {
                return false; // Found a non-null item, list is not empty
            }
        }

        return true; // All items are null, list is empty
    }
    public int GetRemainingEnemyCount()
    {
        int count = 0;

        foreach (var enemy in proportioned)
        {
            if (enemy != null)
            {
                count++;
            }
        }

        return count;
    }
}