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
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private bool spawningTime;
    public bool warTime { get; private set; }

    public int totalSlimes;
    [SerializeField] private GameObject[] spawns;

    private int population;
    private int wood;
    private int food;
    private int wave;

    private void Start()
    {
        wave = 0;
        population = 20;
        wood = 20;
        food = 20;

        UIManager.Instance.SetAmounts(population, wood, food, wave);
    }

    private void Update()
    {
        if(!spawningTime && totalSlimes <= 0 && warTime)
        {
            warTime = false;
            totalSlimes = 0;
            UIManager.Instance.EnableButtons();
            ObjectManager.Instance.ShowLifeOfBuildings();
            ObjectManager.Instance.CollectPopulation();
            ObjectManager.Instance.CollectWood();
            ObjectManager.Instance.CollectFood();
            //GridBuildingSystem.current.ShowBuildingZone();
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
        if (wave == 10)
        {
            return;
        }
        UIManager.Instance.DisableButtons();
        wave += 1;
        warTime = true;
        UIManager.Instance.SetAmounts(population, wood, food, wave);
        ObjectManager.Instance.ShowLifeOfBuildings();
        //GridBuildingSystem.current.ShowBuildingZone();
        WaveConfig();
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
                if (population >= 5 && wood >= 5)
                {
                    return true;
                }
                break;
            case 2://Wheat
                if (population >= 5 && wood >= 5)
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
                if (population >= 5 && wood >= 5)
                {
                    return true;
                }
                break;
            case 5://Palisade
                if (wood >= 1)
                {
                    return true;
                }
                break;
            case 6://SpikeTrap
                if (wood >= 3)
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
                SpawnSlimes(3, 2f, "Common");
                break;

            case 2:
                SpawnSlimes(7, 2f, "Common");
                break;

            case 3:
                SpawnSlimes(3, 2f, "Common");
                SpawnSlimes(2, 2f, "Archer");
                break;

            case 4:
                SpawnSlimes(5, 2f, "Common");
                SpawnSlimes(5, 2f, "Archer");
                break;

            case 5:
                SpawnSlimes(8, 2f, "Common");
                SpawnSlimes(2, 2f, "Archer");
                SpawnSlimes(3, 2f, "Bomb");
                break;
            case 6:
                SpawnSlimes(12, 2f, "Common");
                SpawnSlimes(7, 2f, "Archer");
                SpawnSlimes(6, 2f, "Bomb");
                break;
            case 7:
                SpawnSlimes(20, 2f, "Common");
                SpawnSlimes(10, 2f, "Archer");
                SpawnSlimes(10, 2f, "Bomb");
                break;
            case 8:
                SpawnSlimes(30, 2f, "Common");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(10, 2f, "Bomb");
                break;
            case 9:
                SpawnSlimes(40, 1f, "Common");
                SpawnSlimes(15, 2f, "Archer");
                SpawnSlimes(20, 1.5f, "Bomb");
                break;
            case 10:
                SpawnSlimes(50, 1f, "Common");
                SpawnSlimes(30, 2f, "Archer");
                SpawnSlimes(25, 1.5f, "Bomb");
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
