using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteractController : MonoBehaviour
{
    ItemContainer targetItemContainer;
    [SerializeField] StoragePanel itemStoragePanel;

    public void Open()
    {
        itemStoragePanel.gameObject.SetActive(true);
    }

    public void Close()
    {
        itemStoragePanel.gameObject.SetActive(false);
    }
}
