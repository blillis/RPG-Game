using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool action/Plough")]

public class PloughTile : ToolAction
{
    [SerializeField] List<TileBase> canPlow;

    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReaderController tileMapReaderController, Item item)
    {
        TileBase tileToPlow = tileMapReaderController.GetTileBase(gridPosition);

        if (canPlow.Contains(tileToPlow) == false)
        {
            return false;
        }
        tileMapReaderController.cropsManager.Plow(gridPosition);

        return true;
    }
}
