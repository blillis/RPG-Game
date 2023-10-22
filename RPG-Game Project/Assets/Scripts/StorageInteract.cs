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
    // Start is called before the first frame update
    public override void Interact(Character character)
    {
        if(opened == false)
        {
            opened = true;
            closedStorage.SetActive(false);
            openStorage.SetActive(true);

            AudioManager.instance.Play(onOpenAudio);
        } else
        {
            opened = false;
            closedStorage.SetActive(true);
            openStorage.SetActive(false);

            AudioManager.instance.Play(onCloseAudio);
        }
    }
}
