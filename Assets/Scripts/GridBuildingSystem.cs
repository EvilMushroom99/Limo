using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;

    public bool RightArea { get; private set; }

    private Building temp;
    private Vector3 prevPos;
    private BoundsInt prevArea;

    private static Dictionary<TileType, TileBase> tileBases = new Dictionary<TileType, TileBase>();

    private void Awake()
    {
        current = this;

        string tilePath = @"TileMap/BuildingZone/";

        tileBases.Add(TileType.Empty, null);
        tileBases.Add(TileType.White, Resources.Load<TileBase>(tilePath + "White"));
        tileBases.Add(TileType.Green, Resources.Load<TileBase>(tilePath + "Green"));
        tileBases.Add(TileType.Red, Resources.Load<TileBase>(tilePath + "Red"));
    }

    void Update()
    {
        if (!temp)
        {
            return;
        }

        if (!temp.Placed)
        {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPos = gridLayout.LocalToCell(touchPos);

            if (prevPos != cellPos)
            {
                temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos + new Vector3(0.5f, 0.5f, 0f));
                prevPos = cellPos;
                FollowBuilding();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (temp.CanBePlaced())
            {
                temp.Place();
                temp = null;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearArea(prevArea);
            Destroy(temp.gameObject);
        }
    }

    public void InitializeWithBuilding(GameObject building)
    {
        Vector3 prevPosition = Vector3.zero;

        if (temp)
        {
            prevPosition = temp.transform.position;
            Destroy(temp.gameObject);
            temp = null;
        }

        temp = Instantiate(building, prevPosition, Quaternion.identity).GetComponent<Building>();
        FollowBuilding();
    }

    public void ClearArea(BoundsInt area)
    {
        TileBase[] toClear = new TileBase[area.size.x * area.size.y * area.size.z];
        FillTiles(toClear, TileType.Empty);
        tempTilemap.SetTilesBlock(prevArea, toClear);
    }

    public void ResetArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.White, mainTilemap);
    }

    private void FollowBuilding()
    {
        ClearArea(prevArea);

        temp.area.position = gridLayout.WorldToCell(temp.gameObject.transform.position);

        BoundsInt buildingArea = temp.area;
        TileBase[] baseArray = GetTilesBlock(buildingArea, mainTilemap);

        int size = baseArray.Length;
        TileBase[] tileArray = new TileBase[size];

        for (int i = 0; i < size; i++)
        {
            if (baseArray[i] == tileBases[TileType.White])
            {
                tileArray[i] = tileBases[TileType.Green];
                RightArea = true;
            }
            else
            {
                FillTiles(tileArray, TileType.Red);
                RightArea = false;
                break;
            }
        }

        tempTilemap.SetTilesBlock(buildingArea, tileArray);
        prevArea = buildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, mainTilemap);

        foreach (var b in baseArray)
        {
            if (b != tileBases[TileType.White])
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, TileType.Empty, tempTilemap);
        SetTilesBlock(area, TileType.Green, mainTilemap);
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach (var i in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(i.x, i.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    private static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int size = area.size.x * area.size.y * area.size.z;
        TileBase[] tileArray = new TileBase[size];
        FillTiles(tileArray, type);
        tilemap.SetTilesBlock(area, tileArray);
    }


    private static void FillTiles(TileBase[] array, TileType type)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = tileBases[type];
        }
    }

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red
    }

    //GameManager
    public void ShowBuildingZone()
    {
        if (mainTilemap.gameObject.activeInHierarchy)
        {
            mainTilemap.gameObject.SetActive(false);
        }
        else
        {
            mainTilemap.gameObject.SetActive(true);
        }
    }

    //OnMouse

    private void OnMouseDown()
    {
        temp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("down");
    }

    private void OnMouseDrag()
    {
        temp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("drag");
    }
}
