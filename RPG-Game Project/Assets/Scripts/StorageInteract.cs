using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteract : Interactable
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject open;
    [SerializeField] bool opened;
    // Start is called before the first frame update
    public override void Interact(Character character)
    {
        
    }
}
