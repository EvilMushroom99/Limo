using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

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

    //Resources
    [SerializeField] private Text population;
    [SerializeField] private Text wood;
    [SerializeField] private Text food;
    [SerializeField] private Text wave;

    //Buildings UIs
    [SerializeField] private GameObject inventoryG;
    [SerializeField] private GameObject inventory1;
    [SerializeField] private GameObject inventory2;
    [SerializeField] private GameObject inventory3;

    //Disable When is WarTime
    [SerializeField] private GameObject[] buttons;

    //Building List To Place
    [SerializeField] private GameObject[] buildings;

    private void Update()
    {
        if (!GameManager.Instance.warTime)
        {
            if (ObjectManager.Instance.IsThereAnyTower())
            {
                buttons[0].SetActive(true);
            }
            else
            {
                buttons[0].SetActive(false);
            }
        }
    }

    public void SetAmounts(int pop, int wd, int fd, int wv)
    {
        population.text = "" + pop;
        wood.text = "" + wd;
        food.text = "" + fd;
        wave.text = "" + wv;
    }

    public void ShowResourcesBuildings()
    {
        if (!inventory1.activeInHierarchy)
        {
            inventory1.SetActive(true);
            inventory2.SetActive(false);
            inventory3.SetActive(false);
            inventoryG.SetActive(false);
        }
        else
        {
            inventory1.SetActive(false);
            inventoryG.SetActive(true);
        }
    }

    public void ShowTowersBuildings()
    {
        if (!inventory2.activeInHierarchy)
        {
            inventory2.SetActive(true);
            inventory1.SetActive(false);
            inventory3.SetActive(false);
            inventoryG.SetActive(false);
        }
        else
        {
            inventory2.SetActive(false);
            inventoryG.SetActive(true);
        }
    }

    public void ShowDefenseBuildings()
    {
        if (!inventory3.activeInHierarchy)
        {
            inventory3.SetActive(true);
            inventory1.SetActive(false);
            inventory2.SetActive(false);
            inventoryG.SetActive(false);
        }
        else
        {
            inventory3.SetActive(false);
            inventoryG.SetActive(true);
        }
    }

    public void BackToInventoryG()
    {
        inventory1.SetActive(false);
        inventory2.SetActive(false);
        inventory3.SetActive(false);
        inventoryG.SetActive(true);
    }

    public void GetBuilding(int building)
    {
        if (GameManager.Instance.CheckResources(building))
        {
            GridBuildingSystem.current.InitializeWithBuilding(buildings[building]);
        }
    }

    public void EnableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }

    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }

        inventory1.SetActive(false);
        inventory2.SetActive(false);
        inventory3.SetActive(false);
    }

}
