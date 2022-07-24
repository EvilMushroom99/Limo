using UnityEngine;

public class Trees : MonoBehaviour
{
    public bool Placed { get; private set; }
    public BoundsInt area;

    private void Start()
    {
        CanBePlaced();
    }

    public void CanBePlaced()
    {
        transform.localPosition = GridBuildingSystem.current.gridLayout.CellToLocalInterpolated(transform.position - new Vector3(0.2f, 0.2f, 0f));
        area.position = GridBuildingSystem.current.gridLayout.WorldToCell(transform.position);
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;

        areaTemp.position = positionInt;
        transform.localPosition = GridBuildingSystem.current.gridLayout.CellToWorld(positionInt) + new Vector3(0.25f, 0.25f, 0f);

        if (GridBuildingSystem.current.CanTakeArea(areaTemp))
        {
            Place();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;

        areaTemp.position = positionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(area);
    }
}
