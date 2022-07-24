using System.Collections;
using System.Collections.Generic;
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
        population = 100;
        wood = 200;
        food = 100;

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
        Debug.Log("Increasing Population" + " Amount = " + amount);
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
            case 5:
                if (wood >= 1)
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
                SpawnSlimes(30, 2f);
                break;

            case 2:
                SpawnSlimes(1, 2f);
                break;

            case 3:
                SpawnSlimes(1, 2f);
                break;

            case 4:
                SpawnSlimes(1, 2f);
                break;

            case 5:
                SpawnSlimes(1, 2f);
                break;
        }
    }

    IEnumerator Spawn(int slimeCount, float time)
    {
        spawningTime = true;
        for (int i = 0; i < slimeCount; i++)
        {
            GameObject cslim = Instantiate(Resources.Load("Slimes/Common") as GameObject);
            int index = Random.Range(0, 4);
            cslim.transform.position = spawns[index].transform.position;
            totalSlimes += 1;
            yield return new WaitForSeconds(time);
        }
        spawningTime = false;
    } 

    private void SpawnSlimes(int slimeCount, float time)
    {
        StartCoroutine(Spawn(slimeCount, time));
    }
}
