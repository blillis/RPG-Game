using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool action/Seed Tile")]

public class SeedTile : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReaderController tileMapReadController)
    {
        if (tileMapReadController.cropsManager.Check(gridPosition) == false)
        {
            return false;
        }

        tileMapReadController.cropsManager.Seed(gridPosition);

        return true;
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        inventory.Remove(usedItem);
    }
}
