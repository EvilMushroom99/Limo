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

    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite green;
    [SerializeField] private Sprite red;
    [SerializeField] private SpriteRenderer currentSprite;

    [SerializeField] private Slider slider;

    private enum BuildingType
    {
        Tower,
        House,
        Sawmill,
        Wheat
    }

    [SerializeField] private BuildingType buildingType;

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
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            DestroyBuilding();
            gameObject.SetActive(false);
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

        return false;
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;

        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(area);
        //transform.GetChild(0).localPosition = Vector3.zero;
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
        GridBuildingSystem.current.ClearArea(area);

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
}
