using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteract : Interactable
{
    [SerializeField] GameObject closedStorage;
    [SerializeField] GameObject openStorage;
    [SerializeField] bool opened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] AudioClip onCloseAudio;
    [SerializeField] ItemContainer itemContainer;
    // Start is called before the first frame update
    public override void Interact(Character character)
    {
        
        if (opened == false)
        {
            Open(character);
        }
        else
        {
            Close(character);
        }
        
        
    }

    public void Open(Character character)
    {
        opened = true;
        closedStorage.SetActive(false);
        openStorage.SetActive(true);

        AudioManager.instance.Play(onOpenAudio);

        character.GetComponent<StorageInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        opened = false;
        closedStorage.SetActive(true);
        openStorage.SetActive(false);

        AudioManager.instance.Play(onCloseAudio);

        character.GetComponent<StorageInteractController>().Close();
    }
}
