using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Tool action/Place Object")]
public class PlaceObject : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReaderController tileMapReadController, Item item)
    {
        tileMapReadController.objectsManager.Place(item, gridPosition);
        return true;
    }
}
