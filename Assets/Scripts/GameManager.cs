using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private bool gameover = false;
    private bool spawningTime;
    public bool warTime { get; private set; }

    public int totalSlimes;
    [SerializeField] private GameObject[] spawns;

    private int population;
    private int wood;
    private int food;
    public int wave;

    private void Start()
    {

        AudioManager.Instance.PlayGameplayTheme();
        wave = 0;
        population = 30;
        wood = 30;
        food = 30;

        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    private void Update()
    {
        if (!gameover)
        {
            if (!spawningTime && totalSlimes <= 0 && warTime)
            {
                if (wave != 50)
                {
                    AudioManager.Instance.PlayWaveCompleted();
                    UIManager.Instance.WaveTarget("Wave Completed");
                    AudioManager.Instance.PlayGameplayTheme();
                }
                else
                {
                    AudioManager.Instance.PlayWaveCompleted();
                    UIManager.Instance.WaveTarget("You have SURVIVED!");
                    gameover = true;
                }
                warTime = false;
                totalSlimes = 0;
                UIManager.Instance.EnableButtons();
                ObjectManager.Instance.ShowLifeOfBuildings();
                ObjectManager.Instance.CollectPopulation();
                ObjectManager.Instance.CollectWood();
                ObjectManager.Instance.CollectFood();
            }

            if (!ObjectManager.Instance.IsThereAnyTower() && !EnoughResources() && !warTime)
            {
                gameover = true;
                warTime = false;
                UIManager.Instance.WaveTarget("GameOver");
                AudioManager.Instance.PlayWarDrums();
            }
            else if(!ObjectManager.Instance.IsThereAnyTower() && warTime) 
            {
                gameover = true;
                warTime = false;
                UIManager.Instance.WaveTarget("GameOver");
                AudioManager.Instance.PlayWarDrums();
            }
        }
    }

    public void IncreasePopulation(int amount)
    {
        population += amount;
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void DecreasePopulation(int amount)
    {
        if (population - amount < 0)
        {
            population = 0;
        }
        else
        {
            population -= amount;
        }
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void IncreaseWood(int amount)
    {
        wood += amount;
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void DecreaseWood(int amount)
    {
        if (wood - amount < 0)
        {
            wood = 0;
        }
        else
        {
            wood -= amount;
        }
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void IncreaseFood(int amount)
    {
        food += amount;
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void DecreaseFood(int amount)
    {
        if (food - amount < 0)
        {
            food = 0;
        }
        else
        {
            food -= amount;
        }
        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    public void IncreaseWave()
    {
        if (wave == 50 || gameover)
        {
            return;
        }

        UIManager.Instance.DisableButtons();
        wave += 1;
        warTime = true;
        UIManager.Instance.SetAmounts(population, wood, food, wave);
        ObjectManager.Instance.ShowLifeOfBuildings();
        WaveConfig();
        UIManager.Instance.WaveTarget("Wave " + wave.ToString());
        AudioManager.Instance.PlayWarDrums();
        AudioManager.Instance.PlayWarMusic();
    }

    public bool EnoughResources()
    {
        if (wood > 5 && food > 5 && population > 5)
        {
            return true;
        }

        return false;
    }

    public bool CheckResources(int building)
    {
        switch (building)
        {
            case 0://House
                if (food >= 10 && wood >= 5)
                {
                    return true;
                }
                break;
            case 1://Sawnmill
                if (food >= 10 && wood >= 5)
                {
                    return true;
                }
                break;
            case 2://Wheat
                if (wood >= 5)
                {
                    return true;
                }
                break;
            case 3://ArcherTower
                if (population >= 5 && wood >= 5)
                {
                    return true;
                }
                break;
            case 4://FireTower
                if (population >= 15 && wood >= 10)
                {
                    return true;
                }
                break;
            case 5://Palisade
                if (wood >= 2)
                {
                    return true;
                }
                break;
            case 6://SpikeTrap
                if (wood >= 2)
                {
                    return true;
                }
                break;
            case 7://VoidTower
                if (population >= 50 && wood >= 50)
                {
                    return true;
                }
                break;

        }

        return false;
    }

    private void WaveConfig()
    {
        switch (wave)
        {
            case 1:
                SpawnSlimes(2, 1f, "Common");
                break;

            case 2:
                SpawnSlimes(4, 2f, "Common");
                break;

            case 3:
                SpawnSlimes(6, 2f, "Common");
                break;

            case 4:
                SpawnSlimes(8, 2f, "Common");
                break;

            case 5:
                SpawnSlimes(10, 2f, "Common");
                break;
            case 6:
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 7:
                SpawnSlimes(7, 2f, "Common");
                SpawnSlimes(7, 2f, "Common");
                break;
            case 8:
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 9:
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(3, 2f, "Common");
                break;
            case 10:
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 11:
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(5,  2f, "Common");
                break;
            case 12:
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Common");
                break;
            case 13:
                SpawnSlimes(12, 2f, "Archer");
                SpawnSlimes(12, 2f, "Common");
                break;
            case 14:
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                break;
            case 15:
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                break;
            case 16:
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 17:
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                SpawnSlimes(5,  2f, "Archer");
                SpawnSlimes(10, 2f, "Common");
                break;
            case 18:
                SpawnSlimes(25, 2f, "Archer");
                SpawnSlimes(25, 2f, "Common");
                break;
            case 19:
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(10, 2f, "Common");
                break;
            case 20:
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                SpawnSlimes(15, 2f, "Common");
                break;
            case 21:
                SpawnSlimes(10, 2f, "Tank");
                break;
            case 22:
                SpawnSlimes(6, 2f, "Tank");
                SpawnSlimes(6, 2f, "Tank");
                SpawnSlimes(6, 2f, "Tank");
                break;
            case 23:
                SpawnSlimes(10, 2f, "Tank");
                SpawnSlimes(20, 2f, "Archer");
                break;
            case 24:
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                break;
            case 25:
                SpawnSlimes(3, 2f, "Tank");
                SpawnSlimes(3, 2f, "Tank");
                SpawnSlimes(3, 2f, "Tank");
                SpawnSlimes(4, 2f, "Archer");
                SpawnSlimes(4, 2f, "Archer");
                SpawnSlimes(4, 2f, "Archer");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 26:
                SpawnSlimes(5, 2f, "Tank");
                SpawnSlimes(5, 2f, "Tank");
                SpawnSlimes(5, 2f, "Tank");
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Archer");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Common");
                break;
            case 27:
                SpawnSlimes(20, 2f, "Tank");
                SpawnSlimes(20, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                break;
            case 28:
                SpawnSlimes(30, 2f, "Tank");
                SpawnSlimes(20, 2f, "Archer");
                SpawnSlimes(25, 2f, "Common");
                break;
            case 29:
                SpawnSlimes(10, 2f, "Tank");
                SpawnSlimes(10, 2f, "Tank");
                SpawnSlimes(10, 2f, "Tank");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Common");
                SpawnSlimes(10, 2f, "Common");
                SpawnSlimes(10, 2f, "Common");
                break;
            case 30:
                SpawnSlimes(30, 2f, "Common");
                SpawnSlimes(30, 2f, "Common");
                SpawnSlimes(30, 2f, "Common");
                break;
            case 31:
                SpawnSlimes(30, 1f, "Bomb");
                break;
            case 32:
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(15, 2f, "Tank");
                break;
            case 33:
                SpawnSlimes(36, 2f, "Bomb");
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(20, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                break;
            case 34:
                SpawnSlimes(15, 2f, "Bomb");
                SpawnSlimes(15, 2f, "Bomb");
                SpawnSlimes(15, 2f, "Bomb");
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                SpawnSlimes(15, 2f, "Common");
                break;
            case 35:
                SpawnSlimes(45, 2f, "Bomb");
                SpawnSlimes(30, 2f, "Tank");
                SpawnSlimes(30, 2f, "Archer");
                SpawnSlimes(30, 2f, "Common");
                break;
            case 36:
                SpawnSlimes(20, 2f, "Bomb");
                SpawnSlimes(20, 2f, "Bomb");
                SpawnSlimes(20, 2f, "Bomb");
                SpawnSlimes(20, 2f, "Tank");
                SpawnSlimes(20, 2f, "Tank");
                SpawnSlimes(20, 2f, "Archer");
                SpawnSlimes(20, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                SpawnSlimes(20, 2f, "Common");
                break;
            case 37:
                SpawnSlimes(120, 2f, "Bomb");
                SpawnSlimes(120, 2f, "Common");
                break;
            case 38:
                SpawnSlimes(60, 2f, "Bomb");
                SpawnSlimes(30, 1f, "Tank");
                break;
            case 39:
                SpawnSlimes(60, 2f, "Bomb");
                SpawnSlimes(30, 1f, "Tank");
                SpawnSlimes(25, 2f, "Archer");
                break;
            case 40:
                SpawnSlimes(60, 2f, "Bomb");
                SpawnSlimes(30, 1f, "Tank");
                SpawnSlimes(25, 2f, "Archer");
                SpawnSlimes(80, 2f, "Common");
                break;
            case 41:
                SpawnSlimes(200, 1f, "Rapid");
                break;
            case 42:
                SpawnSlimes(150, 2f, "Rapid");
                SpawnSlimes(30,  1f, "Tank");
                SpawnSlimes(20,  2f, "Bomb");
                break;
            case 43:
                SpawnSlimes(50, 2f, "Rapid");
                SpawnSlimes(30, 1f, "Tank");
                SpawnSlimes(20, 2f, "Bomb");
                SpawnSlimes(25, 2f, "Archer");
                SpawnSlimes(80, 2f, "Common");
                break;
            case 44:
                SpawnSlimes(50, 1f, "Rapid");
                SpawnSlimes(50, 1f, "Tank");
                SpawnSlimes(40, 2f, "Bomb");
                SpawnSlimes(50, 2f, "Archer");
                SpawnSlimes(40, 2f, "Common");
                break;
            case 45:
                SpawnSlimes(25, 10f, "Rapid");
                SpawnSlimes(25, 10f, "Rapid");
                SpawnSlimes(25, 13f, "Rapid");
                SpawnSlimes(25, 10f, "Tank");
                SpawnSlimes(25, 10f, "Tank");
                SpawnSlimes(25, 13f, "Tank");
                SpawnSlimes(20, 10f, "Bomb");
                SpawnSlimes(20, 12f, "Bomb");
                SpawnSlimes(20, 12f, "Bomb");
                SpawnSlimes(25, 12f, "Archer");
                SpawnSlimes(25, 12f, "Archer");
                SpawnSlimes(25, 12f, "Archer");
                SpawnSlimes(40, 12f, "Common");
                SpawnSlimes(40, 12f, "Common");
                SpawnSlimes(40, 12f, "Common");
                break;
            case 46:
                SpawnSlimes(30, 11f, "Rapid");
                SpawnSlimes(30, 11f, "Rapid");
                SpawnSlimes(30, 13f, "Rapid");
                SpawnSlimes(30, 11f, "Tank");
                SpawnSlimes(30, 11f, "Tank");
                SpawnSlimes(30, 13f, "Tank");
                SpawnSlimes(30, 11f, "Bomb");
                SpawnSlimes(30, 12f, "Bomb");
                SpawnSlimes(30, 12f, "Bomb");
                SpawnSlimes(30, 12f, "Archer");
                SpawnSlimes(30, 12f, "Archer");
                SpawnSlimes(30, 12f, "Archer");
                SpawnSlimes(30, 12f, "Common");
                SpawnSlimes(30, 12f, "Common");
                SpawnSlimes(30, 12f, "Common");
                break;
            case 47:
                SpawnSlimes(40, 11f, "Rapid");
                SpawnSlimes(40, 11f, "Rapid");
                SpawnSlimes(40, 10f, "Rapid");
                SpawnSlimes(40, 11f, "Tank");
                SpawnSlimes(40, 11f, "Tank");
                SpawnSlimes(40, 10f, "Tank");
                SpawnSlimes(40, 11f, "Bomb");
                SpawnSlimes(40, 12f, "Bomb");
                SpawnSlimes(40, 10f, "Bomb");
                SpawnSlimes(40, 12f, "Archer");
                SpawnSlimes(40, 12f, "Archer");
                SpawnSlimes(40, 10f, "Archer");
                SpawnSlimes(40, 12f, "Common");
                SpawnSlimes(40, 12f, "Common");
                SpawnSlimes(40, 10f, "Common");
                break;
            case 48:
                SpawnSlimes(50, 12f, "Rapid");
                SpawnSlimes(50, 12f, "Rapid");
                SpawnSlimes(50, 12f, "Rapid");
                SpawnSlimes(50, 12f, "Tank");
                SpawnSlimes(50, 12f, "Tank");
                SpawnSlimes(50, 12f, "Tank");
                SpawnSlimes(50, 12f, "Bomb");
                SpawnSlimes(50, 12f, "Bomb");
                SpawnSlimes(50, 12f, "Bomb");
                SpawnSlimes(50, 12f, "Archer");
                SpawnSlimes(50, 12f, "Archer");
                SpawnSlimes(50, 12f, "Archer");
                SpawnSlimes(50, 12f, "Common");
                SpawnSlimes(50, 12f, "Common");
                SpawnSlimes(50, 12f, "Common");
                break;
            case 49:
                SpawnSlimes(25, 2f, "Rapid");
                SpawnSlimes(25, 2f, "Tank");
                SpawnSlimes(25, 2f, "Bomb");
                SpawnSlimes(25, 2f, "Archer");
                SpawnSlimes(25, 2f, "Common");
                break;
            case 50:
                SpawnSlimes(1, 1f, "Boss");
                break;
        }
    }

    IEnumerator Spawn(int slimeCount, float time, string slimeType)
    {
        spawningTime = true;
        for (int i = 0; i < slimeCount; i++)
        {
            GameObject cslim = Instantiate(Resources.Load($"Slimes/{slimeType}Slime") as GameObject);
            int index = Random.Range(0, 12);
            cslim.transform.position = spawns[index].transform.position;
            totalSlimes += 1;
            yield return new WaitForSeconds(time);
        }
        spawningTime = false;
    } 

    private void SpawnSlimes(int slimeCount, float time, string slimeType)
    {
        StartCoroutine(Spawn(slimeCount, time, slimeType));
    }
}
