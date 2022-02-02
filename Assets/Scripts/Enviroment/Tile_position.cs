using SuperTiled2Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile_position : MonoBehaviour
{
    UI_Pause ui_Pause;

    [Header("Map Dependencies")]
    [SerializeField] DetailsMap detailsMap;

    [Header("Tower Dependencies")]
    [SerializeField] string towerPlacementType = "Interactable";
    [SerializeField] GameObject towerRing;

    [Header("Map Settings")]
    [SerializeField] Vector2Int mapOfsset;
    [SerializeField] Vector2 towerOffset;

    Tilemap map;
    TileBase towerTile;
    TileBase interactableTile;

    Vector3Int lastPos;
    HoverTile hoverTile;
    Vector3Int? prevPos = null;

    SuperTile hoveredTile;
    Vector3Int gridPosition;

    UI_TowerSelectButton towerSelectButton;
    bool shown;
    bool notInteractable = true;

    void Awake()
    {
        ui_Pause = FindObjectOfType<UI_Pause>().GetComponent<UI_Pause>();
        towerSelectButton = FindObjectOfType<UI_TowerSelectButton>().GetComponent<UI_TowerSelectButton>();

        map = GetComponent<Tilemap>();
        TileBase[] tiles = map.GetTilesBlock(map.cellBounds);

        bool baseFound = false;
        bool interactableFound = false;
        foreach (var tile in tiles)
        {
            var t = tile as SuperTile;
            if (t.m_Type == "BASE" && !baseFound)
            {
                towerTile = t;
                baseFound = true;
            }
            else if (t.m_Type == towerPlacementType && !interactableFound)
            {
                interactableTile = t;
                interactableFound = true;
            }

            if (baseFound && interactableFound)
            {
                break;
            }
        }

        if (detailsMap.HoverTileEnabled)
        {
            hoverTile = new HoverTile(detailsMap.Map, detailsMap.Tile, lastPos, detailsMap.DelayTime);
        }
    }

    void Update()
    {
        if (ui_Pause.GetPausedState() == false)
        {
            Vector3Int _gridPosition = getGridPosition();
            SuperTile _hoveredTile = map.GetTile<SuperTile>(_gridPosition);

            if (tileIsNotNull(_hoveredTile) && tileIsInteractable(_hoveredTile))
            {
                notInteractable = false;
                if (Input.GetMouseButtonDown(0))
                {
                    shown = true;

                    hoveredTile = _hoveredTile;
                    gridPosition = _gridPosition;

                    towerSelectButton.EnablePanel(getWorldPosition(_gridPosition));

                    if (detailsMap.HoverTileEnabled) hoverTile.Show(_gridPosition);

                    if (prevPos != null)
                    {
                        if (prevPos != _gridPosition)
                        {
                            hoverTile.DeleteAtPosition(this, (Vector3Int)prevPos);
                            prevPos = _gridPosition;
                        }
                    }
                    else
                    {
                        prevPos = _gridPosition;
                    }

                }
            }
            else
            {
                notInteractable = true;
            }
        }
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && shown && notInteractable)
        {

            if (towerSelectButton.GetButtonState() && towerSelectButton.GetPosition() != (Vector3)getWorldPosition(gridPosition))
            {
                hoverTile.DeleteAtPosition(this, (Vector3Int)prevPos);

                towerSelectButton.DisablePanel();
                shown = false;
            }
            else if (!towerSelectButton.hovering)
            {
                hoverTile.DeleteAtPosition(this, (Vector3Int)prevPos);

                towerSelectButton.DisablePanel();
                shown = false;
            }
        }
    }

    public void BuildTower()
    {
        instantiateTower(hoveredTile, gridPosition);
        shown = false;
    }

    public void DeleteHoverTile()
    {
        if (detailsMap.HoverTileEnabled) 
        {
            hoverTile.DeleteAtPosition(this, gridPosition);
        }
    }

    Vector3Int getGridPosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int gridPosition = map.WorldToCell(mousePosition);
        Vector3Int pos = new Vector3Int(gridPosition.x + mapOfsset.x, gridPosition.y + mapOfsset.y, gridPosition.z);

        return pos;
    }

    bool tileIsNotNull(SuperTile tile)
    {
        return tile != null;
    }

    bool tileIsInteractable(SuperTile tile)
    {
        return tile.m_Type == towerPlacementType;
    }

    public void instantiateTower(SuperTile tile, Vector3Int gridPos)
    {
        if (tileIsInteractable(tile))
        {
            map.SetTile(gridPos, towerTile);

            var pos = getWorldPosition(gridPos);

            Instantiate(towerRing, pos, towerRing.transform.rotation);
        }
    }

    public void setInteractableTile(Vector3 worldPos)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPos);
        Vector3Int pos = new Vector3Int(gridPosition.x + mapOfsset.x, gridPosition.y + mapOfsset.y, gridPosition.z);

        map.SetTile(pos, interactableTile);
    }

    Vector2 getWorldPosition(Vector3Int gridPos)
    {
        var mpos = map.CellToWorld(gridPos);
        var pos = new Vector2(mpos.x + towerOffset.x, mpos.y + towerOffset.y);

        return pos;
    }
}
