using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;

    [SerializeField] private int pop;
    [SerializeField] private int wood;
    [SerializeField] private int food;
    [SerializeField] private int life;
    private int totalLife;

    [SerializeField] private GameObject buildingRange;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite red;
    [SerializeField] private SpriteRenderer currentSprite;
    [SerializeField] private Material baseMaterial;
    [SerializeField] private Material hitMaterial;
    [SerializeField] private GameObject hitExplotion;
    [SerializeField] private GameObject hitbox;
    private bool hit;
    private float hitTime = 0.25f;

    [SerializeField] private Slider slider;

    [SerializeField] private GameObject[] fires;

    private enum BuildingType
    {
        Tower,
        Trap,
        House,
        Sawmill,
        Wheat,
        Tree
    }

    [SerializeField] private BuildingType buildingType;

    private void Start()
    {
        totalLife = life;
    }

    private void Update()
    {
        if (hit)
        {
            hitTime -= Time.deltaTime;
            if (hitTime <= 0f)
            {
                hit = false;
            }
        }
        else
        {
            hitTime = 0.25f;
        }

        if (!GameManager.Instance.warTime)
        {
            hitbox.SetActive(false);
        }
        else if (!hitbox.activeInHierarchy)
        {
            hitbox.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        if (!Placed)
        {
            if (GridBuildingSystem.current.RightArea)
            {
                currentSprite.sprite = green;
            }
            else
            {
                currentSprite.sprite = red;
            }
        }
        else
        {
            if (hit)
            {
                currentSprite.material = hitMaterial;
            }
            else
            {
                currentSprite.material = baseMaterial;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        AudioManager.Instance.PlayBuildingHit();
        hit = true;
        hitTime = 0.25f;

        if (life <= 0)
        {
            AudioManager.Instance.PlayBuildingDestroyed();
            DestroyBuilding();
        }
        if (life <= totalLife / 1.5f)
        {
            fires[0].SetActive(true);
        }
        if (life <= totalLife / 3f)
        {
            for(int i = 0; i < fires.Length; i++)
            {
                fires[i].SetActive(true);
            }
        }

        slider.value = life;
    }

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;

        areaTemp.position = positionInt;
        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            return true;
        }

        AudioManager.Instance.PlayMenuSelectionBuildingDeny();
        return false;
    }

    public void Place()
    {
        if (buildingRange)
        {
            buildingRange.SetActive(false);
        }

        AudioManager.Instance.PlayBuildingPlaced();

        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;

        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(area);
        currentSprite.sprite = normal;

        GameManager.Instance.DecreasePopulation(pop);
        GameManager.Instance.DecreaseWood(wood);
        GameManager.Instance.DecreaseFood(food);

        ObjectManager.Instance.AddBuilding(gameObject);

        switch (buildingType)
        {
            case BuildingType.House:
                ObjectManager.Instance.AddPopBuilding(gameObject);
                break;

            case BuildingType.Sawmill:
                ObjectManager.Instance.AddWoodBuilding(gameObject); 
                break;

            case BuildingType.Wheat:
                ObjectManager.Instance.AddFoodBuilding(gameObject); 
                break;
        }

        slider.maxValue = life;
        slider.value = life;
    }

    public void DestroyBuilding()
    {
        GridBuildingSystem.current.ResetArea(area);

        switch (buildingType)
        {
            case BuildingType.House:
                ObjectManager.Instance.RemovePopBuilding(gameObject); 
                break;

            case BuildingType.Sawmill:
                ObjectManager.Instance.RemoveWoodBuilding(gameObject); 
                break;

            case BuildingType.Wheat:
                ObjectManager.Instance.RemoveFoodBuilding(gameObject); 
                break;
        }

        gameObject.SetActive(false);
    }

    public void ShowLife()
    {
        if (!slider.gameObject.activeInHierarchy)
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }

    public int GetFood()
    {
        return food;
    }

    public int GetWood()
    {
        return wood;
    }

    public int GetPop()
    {
        return pop;
    }
}
