using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HoverTile
{
    public Tilemap Map;
    public Tile Tile;
    public Vector3Int? Position;
    public float DelayTime;

    public HoverTile(Tilemap map, Tile tile, Vector3Int position, float delayTime)
    {
        Map = map;
        Tile = tile;
        Position = position;
        DelayTime = delayTime;
    }

    /*    public void AddComponents(Tilemap map, Tile tile, Vector3Int? position)
        {
            Map = map;
            Tile = tile;
            Position = position;
        }*/

    public void DeleteAtPosition(MonoBehaviour monoBehaviour, Vector3Int pos)
    {
        if (Position != null)
        {
            monoBehaviour.StartCoroutine(DelayFunction(DelayTime, pos));
        }
    }

    IEnumerator DelayFunction(float delay, Vector3Int pos)
    {
        yield return new WaitForSeconds(delay);
        Map.SetTile(pos, null);
        Map.RefreshAllTiles();
    }

    void SetHoverTileToMap(Vector3Int pos)
    {
        Map.SetTile(pos, Tile);
    }

    public void Show(Vector3Int position)
    {

        SetHoverTileToMap(position);
        Position = position;
    }

}
