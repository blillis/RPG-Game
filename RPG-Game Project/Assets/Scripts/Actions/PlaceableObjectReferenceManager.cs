using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObjectReferenceManager : MonoBehaviour
{
    public PlaceableObjectManager placeableObjectManager;

    public void Place(Item item, Vector3Int pos)
    {
        if (placeableObjectManager == null)
        {
            Debug.LogWarning("No placeableObjectManager reference detected");
            return;
        }

        placeableObjectManager.Place(item, pos);
    }

    internal void PickUp(Vector3Int gridPosition)
    {
        if (placeableObjectManager == null)
        {
            Debug.LogWarning("No placeableObjectManager reference detected");
            return;
        }

        placeableObjectManager.PickUp(gridPosition);
    }

    public bool Check(Vector3Int pos)
    {
        if (placeableObjectManager == null)
        {
            Debug.LogWarning("No placeableObjectManager reference detected");
            return false;
        }

        return placeableObjectManager.Check(pos);
    }
}
