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
        wave = 29;
        population = 3000;
        wood = 3000;
        food = 3000;

        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    private void Update()
    {
        if (!gameover)
        {
            if (!spawningTime && totalSlimes <= 0 && warTime)
            {
                if (wave != 1)
                {
                    AudioManager.Instance.PlayWaveCompleted();
                    UIManager.Instance.WaveTarget("Wave Completed");
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
        if (wave == 31 || gameover)
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
                if (food >= 5 && wood >= 5)
                {
                    return true;
                }
                break;
            case 1://Sawnmill
                if (food >= 5 && wood >= 5)
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
                if (population >= 30 && wood >= 20)
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
                if (population >= 250 && wood >= 250)
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
                SpawnSlimes(1, 1f, "Common");
                break;

            case 2:
                SpawnSlimes(6, 2f, "Common");
                break;

            case 3:
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(1, 2f, "Archer");
                break;

            case 4:
                SpawnSlimes(4, 2f, "Common");
                SpawnSlimes(2, 2f, "Archer");
                break;

            case 5:
                SpawnSlimes(1, 2f, "Tank");
                SpawnSlimes(4, 2f, "Common");
                break;
            case 6:
                SpawnSlimes(1, 2f, "Tank");
                SpawnSlimes(3, 3f, "Archer");
                SpawnSlimes(3, 2f, "Common");
                break;
            case 7:
                SpawnSlimes(3, 2f, "Tank");
                SpawnSlimes(3, 3f, "Archer");
                SpawnSlimes(3, 2f, "Rapid");
                break;
            case 8:
                SpawnSlimes(5, 2f, "Tank");
                SpawnSlimes(3, 2f, "Bomb");
                SpawnSlimes(3, 2f, "Rapid");
                SpawnSlimes(3, 2f, "Common");
                break;
            case 9:
                SpawnSlimes(20, 2f, "Bomb");
                break;
            case 10:
                SpawnSlimes(20, 2f, "Common");
                SpawnSlimes(8,  2f, "Archer");
                SpawnSlimes(8,  2f, "Bomb");
                break;
            case 11:
                SpawnSlimes(10, 2f, "Common");
                SpawnSlimes(5,  2f, "Archer");
                break;
            case 12:
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(5,  2f, "Common");
                SpawnSlimes(5,  2f, "Common");
                SpawnSlimes(5,  2f, "Common");
                SpawnSlimes(5,  2f, "Archer");
                SpawnSlimes(5, 2f, "Tank");
                break;
            case 13:
                SpawnSlimes(10, 2f, "Common");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(5, 2f, "Tank");
                break;
            case 14:
                SpawnSlimes(5,  2f, "Common");
                SpawnSlimes(5,  2f, "Archer");
                SpawnSlimes(15, 2f, "Bomb");
                SpawnSlimes(5, 2f, "Tank");
                break;
            case 15:
                SpawnSlimes(30, 1f, "Common");
                SpawnSlimes(5,  4f, "Archer");
                SpawnSlimes(5,  4f, "Bomb");
                SpawnSlimes(5, 2f, "Tank");
                break;
            case 16:
                SpawnSlimes(20, 1f, "Rapid");
                break;
            case 17:
                SpawnSlimes(5,  1f, "Rapid");
                SpawnSlimes(5,  2f, "Common");
                SpawnSlimes(5,  2f, "Archer");
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(5, 2f, "Tank");
                break;
            case 18:
                SpawnSlimes(15, 1f, "Rapid");
                SpawnSlimes(10, 2f, "Archer");
                break;
            case 19:
                SpawnSlimes(15, 1f, "Rapid");
                SpawnSlimes(10, 2f, "Bomb");
                break;
            case 20:
                SpawnSlimes(15, 1f, "Rapid");
                SpawnSlimes(10, 2f, "Bomb");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(15, 2f, "Common");
                break;
            case 21:
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(5,  2f, "Archer");
                break;
            case 22:
                SpawnSlimes(20, 2f, "Tank");
                SpawnSlimes(20,  2f, "Rapid");
                break;
            case 24:
                SpawnSlimes(15, 2f, "Tank");
                SpawnSlimes(15, 2f, "Rapid");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                break;
            case 25:
                SpawnSlimes(25, 2f, "Tank");
                SpawnSlimes(25, 2f, "Rapid");
                SpawnSlimes(25, 2f, "Archer");
                SpawnSlimes(20, 2f, "Common");
                SpawnSlimes(20, 2f, "Bomb");
                break;
            case 26:
                SpawnSlimes(50, 2f, "Tank");
                SpawnSlimes(20, 2f, "Bomb");
                break;
            case 27:
                SpawnSlimes(50, 2f, "Archer");
                SpawnSlimes(50, 2f, "Bomb");
                break;
            case 28:
                SpawnSlimes(50, 2f, "Tank");
                SpawnSlimes(50, 2f, "Rapid");
                SpawnSlimes(50, 2f, "Common");
                break;
            case 29:
                SpawnSlimes(50, 2f, "Tank");
                SpawnSlimes(50, 2f, "Rapid");
                SpawnSlimes(50, 2f, "Common");
                SpawnSlimes(50, 2f, "Archer");
                SpawnSlimes(50, 2f, "Bomb");
                break;
            case 30:
                SpawnSlimes(80, 2f, "Tank");
                SpawnSlimes(80, 2f, "Rapid");
                SpawnSlimes(80, 2f, "Common");
                SpawnSlimes(80, 2f, "Archer");
                SpawnSlimes(80, 2f, "Bomb");
                break;
            case 31:
                SpawnSlimes(1, 2f, "Boss");
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
