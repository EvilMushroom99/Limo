using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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

    //Info
    [SerializeField] private Text slimesLeft;
    [SerializeField] private GameObject waveAnim;
    [SerializeField] private GameObject info;

    //Control
    [SerializeField] Animator fade;

    //Resources
    [SerializeField] private Text population;
    [SerializeField] private Text wood;
    [SerializeField] private Text food;
    [SerializeField] private Text wave;

    //Buildings UIs
    [SerializeField] private GameObject inventory1;
    [SerializeField] private GameObject inventory2;
    [SerializeField] private GameObject inventory3;

    //Disable When is WarTime
    [SerializeField] private GameObject[] buttons;

    //Building List To Place
    [SerializeField] private GameObject[] buildings;

    //Building Info
    [SerializeField] private Text buildingName;
    [SerializeField] private Text buildingResource1;
    [SerializeField] private Text buildingResource2;
    [SerializeField] private GameObject buildingWood;
    [SerializeField] private GameObject buildingFood;
    [SerializeField] private GameObject buildingPop;

    //Tutorial
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject[] tutorials;
    private int pageCount = 0;
    
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
        else
        {
            slimesLeft.text = GameManager.Instance.totalSlimes.ToString();
        }
    }

    public void NextPage()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        if (pageCount < 4)
        {
            pageCount += 1;
            tutorials[pageCount - 1].SetActive(false);
            tutorials[pageCount].SetActive(true);
        }
    }

    public void PreviusPage()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        if (pageCount > 0)
        {
            pageCount -= 1;
            tutorials[pageCount].SetActive(true);
            tutorials[pageCount + 1].SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        tutorial.SetActive(false);
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
        AudioManager.Instance.PlayMenuSelectionBuilding();
        buildingName.text = "";
        inventory1.SetActive(true);
        inventory2.SetActive(false);
        inventory3.SetActive(false);
        buildingWood.SetActive(false);
        buildingFood.SetActive(false);
        buildingPop.SetActive(false);
        buildingResource1.gameObject.SetActive(false);
        buildingResource2.gameObject.SetActive(false);
        info.SetActive(false);
    }

    public void ShowDefenseBuildings()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        buildingName.text = "";
        inventory1.SetActive(false);
        inventory2.SetActive(true);
        inventory3.SetActive(false);
        buildingWood.SetActive(false);
        buildingFood.SetActive(false);
        buildingPop.SetActive(false);
        buildingResource1.gameObject.SetActive(false);
        buildingResource2.gameObject.SetActive(false);
        info.SetActive(false);
    }

    public void ShowTowersBuildings()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        buildingName.text = "";
        inventory1.SetActive(false);
        inventory2.SetActive(false);
        inventory3.SetActive(true);
        buildingWood.SetActive(false);
        buildingFood.SetActive(false);
        buildingPop.SetActive(false);
        buildingResource1.gameObject.SetActive(false);
        buildingResource2.gameObject.SetActive(false);
        info.SetActive(false);
    }

    public void GetBuilding(int building)
    {
        buildingName.text = buildings[building].name;
        buildingResource1.gameObject.SetActive(true);
        buildingResource2.gameObject.SetActive(true);

        Building currentBuilding = buildings[building].GetComponent<Building>();
        buildingResource1.text = currentBuilding.GetWood().ToString();
        buildingWood.SetActive(true);

        if (currentBuilding.GetFood() != 0)
        {
            buildingResource2.text = currentBuilding.GetFood().ToString();
            buildingFood.SetActive(true);
            buildingPop.SetActive(false);
        }
        else if (currentBuilding.GetPop() != 0)
        {
            buildingResource2.text = currentBuilding.GetPop().ToString();
            buildingPop.SetActive(true);
            buildingFood.SetActive(false);
        }
        else
        {
            buildingFood.SetActive(false);
            buildingPop.SetActive(false);
            buildingResource2.text = "";
        }


        if (GameManager.Instance.CheckResources(building))
        {
            AudioManager.Instance.PlayMenuSelection();
            GridBuildingSystem.current.InitializeWithBuilding(buildings[building]);
        }
        else
        {
            AudioManager.Instance.PlayMenuSelectionBuildingDeny();
        }
    }

    public void GetInfo(string title, string description)
    {
        info.transform.GetChild(0).GetComponent<Text>().text = title;
        info.transform.GetChild(1).GetComponent<Text>().text = description;
        info.SetActive(true);
    }

    public void DisableInfo()
    {
        info.SetActive(false);
    }

    public void EnableButtons()
    {
        buildingName.text = "";
        buildingResource1.text = "";
        buildingResource2.text = "";

        inventory1.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }

        buildingWood.SetActive(false);
        buildingFood.SetActive(false);
        buildingPop.SetActive(false);
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

    public void WaveTarget(string description)
    {
        StartCoroutine(WaveAnim(description));
    }

    public void PauseGame()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
        }
    }

    public void NormalSpeed()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        AudioManager.Instance.PlayMenuSelectionBuilding();
    }

    public void Speedx2()
    {
        AudioManager.Instance.PlayMenuSelectionBuilding();
        if (Time.timeScale != 2)
        {
            Time.timeScale = 2;
        }
    }

    public void ReturnToMenu()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1;
        }
        AudioManager.Instance.PlayMenuSelectionBuilding();
        StartCoroutine(Transition());
    }

    IEnumerator WaveAnim(string description)
    {
        waveAnim.SetActive(true);
        waveAnim.GetComponent<Text>().text = description;
        yield return new WaitForSeconds(5.2f);
        waveAnim.SetActive(false);
    }

    IEnumerator Transition()
    {
        fade.Play("Close");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
