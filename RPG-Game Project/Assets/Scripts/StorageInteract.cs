using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteract : Interactable, IPersistant
{
    [SerializeField] GameObject closedStorage;
    [SerializeField] GameObject openStorage;
    [SerializeField] bool opened;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] AudioClip onCloseAudio;
    [SerializeField] ItemContainer itemContainer;
    // Start is called before the first frame update
    private void Start()
    {
        if (itemContainer == null)
        {
            Init();
        }
            
    }

    private void Init()
    {
        itemContainer = (ItemContainer)ScriptableObject.CreateInstance(typeof(ItemContainer));
        itemContainer.Init();
        
    }

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

    [Serializable]
    public class SaveLootItemData
    {
        public int itemId;
        public int count;

        public SaveLootItemData(int id, int c)
        {
            itemId = id;
            count = c;
        }
    }

    [Serializable]
    public class ToSave
    {
        public List<SaveLootItemData> itemDatas;

        public ToSave()
        {
            itemDatas = new List<SaveLootItemData>();
        }
    }

    public string Read()
    {
        ToSave toSave = new ToSave();

        for(int i = 0; i < itemContainer.slots.Count; i++)
        {
            if (itemContainer.slots[i].item == null)
            {
                toSave.itemDatas.Add(new SaveLootItemData(-1, 0));
            }
            else
            {
                toSave.itemDatas.Add(new SaveLootItemData(
                    itemContainer.slots[i].item.id, 
                    itemContainer.slots[i].count
                    )
                    );
            }
        }

        return JsonUtility.ToJson(toSave);
    }

    public void Load(string jsonString)
    {
        if (jsonString == "" || jsonString == "{}" || jsonString == null) { return; } 

        if (itemContainer == null)
        {
            Init();
        }

        ToSave toLoad = JsonUtility.FromJson<ToSave>(jsonString);
        for(int i = 0; i <toLoad.itemDatas.Count; i++)
        {
            if (toLoad.itemDatas[i].itemId == -1)
            {
                itemContainer.slots[i].Clear();
            }
            else
            {
                itemContainer.slots[i].item = GameManager.instance.itemDB.items[toLoad.itemDatas[i].itemId];
                itemContainer.slots[i].count = toLoad.itemDatas[i].count;
            }
        }
    }
}
