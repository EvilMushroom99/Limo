using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private List<GameObject> Buildings = new List<GameObject>();
    private List<GameObject> PopBuildings = new List<GameObject>();
    private List<GameObject> WoodBuildings = new List<GameObject>();
    private List<GameObject> FoodBuildings = new List<GameObject>();

    public static ObjectManager Instance;
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

    public GameObject GetClosestBuilding(Vector2 slimePosition)
    {
        GameObject closest = null;
        float lastDistance = 999999999999999999f;

        for(int i = 0; i < Buildings.Count; i++)
        {

            if (Buildings[i].activeInHierarchy)
            {

                if (Buildings[i].CompareTag("Trap"))
                {
                    if (Buildings[i].transform.GetChild(0).gameObject.activeInHierarchy)
                    {
                        float currentDistance = Vector2.Distance(Buildings[i].transform.position, slimePosition);

                        if (currentDistance < lastDistance)
                        {
                            lastDistance = currentDistance;
                            closest = Buildings[i];
                        }
                    }
                }
                else
                {
                    float currentDistance = Vector2.Distance(Buildings[i].transform.position, slimePosition);

                    if (currentDistance < lastDistance)
                    {
                        lastDistance = currentDistance;
                        closest = Buildings[i];
                    }
                }
            }
        }

        return closest;
    }

    public void AddBuilding(GameObject newBuilding)
    {
        Buildings.Add(newBuilding);
    }

    public void AddPopBuilding(GameObject newPopBuilding)
    {
        PopBuildings.Add(newPopBuilding);
    }

    public void RemovePopBuilding(GameObject newPopBuilding)
    {
        PopBuildings.Remove(newPopBuilding);
    }

    public void AddWoodBuilding(GameObject newWoodBuilding)
    {
        WoodBuildings.Add(newWoodBuilding);
    }

    public void RemoveWoodBuilding(GameObject newWoodBuilding)
    {
        WoodBuildings.Remove(newWoodBuilding);
    }

    public void AddFoodBuilding(GameObject newFoodBuilding)
    {
        FoodBuildings.Add(newFoodBuilding);
    }

    public void RemoveFoodBuilding(GameObject newFoodBuilding)
    {
        FoodBuildings.Remove(newFoodBuilding);
    }

    public void CollectPopulation()
    {
        for(int i = 0; i < PopBuildings.Count; i++)
        {
            PopBuildings[i].GetComponentInChildren<HouseController>().GetResources();
        }
    }

    public void CollectWood()
    {
        for (int i = 0; i < WoodBuildings.Count; i++)
        {
            WoodBuildings[i].GetComponentInChildren<SawmillController>().GetResources();
        }
    }

    
    public void CollectFood()
    {
        for (int i = 0; i < FoodBuildings.Count; i++)
        {
            FoodBuildings[i].GetComponentInChildren<WheatController>().GetResources();
        }
    }

    public void ShowLifeOfBuildings()
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            Buildings[i].GetComponent<Building>().ShowLife();
        }
    }

    public void DestroyBuildings()
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (!Buildings[i].activeInHierarchy)
            {
                GameObject building = Buildings[i];
                Buildings.Remove(building);
                Destroy(building);
            }
        }
    }

    public bool IsThereAnyTower()
    {
        for(int i = 0; i < Buildings.Count; i++)
        {
            if (Buildings[i].CompareTag("Tower"))
            {
                if (Buildings[i].activeInHierarchy)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
