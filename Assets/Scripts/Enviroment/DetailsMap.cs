using UnityEngine;
using UnityEngine.Tilemaps;

public class DetailsMap : MonoBehaviour
{
    [Header("Dependencies")]
    [Tooltip("This object's tile map.")]
    public Tilemap Map;
    [Tooltip("Tile to be used as hover tile.")]
    public Tile Tile;

    [Header("Tile Settings")]
    [Tooltip("Time to wait before deleting the hover tile.")]
    public float DelayTime;
    
    [Tooltip("True: show the hover tile when interactable tile is pressed")]
    public bool HoverTileEnabled;
}
