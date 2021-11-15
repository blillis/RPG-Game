using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteract : Interactable
{
    [SerializeField] GameObject closedStorage;
    [SerializeField] GameObject openStorage;
    [SerializeField] bool opened;
    // Start is called before the first frame update
    public override void Interact(Character character)
    {
        if(opened == false)
        {
            opened = true;
            closedStorage.SetActive(false);
            openStorage.SetActive(true);
        }
    }
}
