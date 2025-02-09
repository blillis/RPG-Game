using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeAgent))]
public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] float spawnArea_height = 1f;
    [SerializeField] float spawnArea_width = 1f;

    [SerializeField] GameObject[] spawn;
    int length;
    [SerializeField] float probability = 0.1f;
    [SerializeField] int spawnCount = 1;

    [SerializeField] bool oneTime = false;

    List<SpawnedObject> spawnedObjects;

    private void Start()
    {
        length = spawn.Length;

        if (oneTime == false)
        {
            TimeAgent timeAgent = GetComponent<TimeAgent>();
            timeAgent.onTimeTick += Spawn;
            spawnedObjects = new List<SpawnedObject>();
        }
        else
        {
            Spawn();
            Destroy(gameObject);
        }

    }

    public void SpawnedObjectDestroyed(SpawnedObject spawnedObject)
    {
        spanwedObjects.Remove(spawnedObject);

    }

    void Spawn()
    {
        if (Random.value > probability) { return; }

        for (int i = 0; i < spawnCount; i++)
        {
            int id = Random.Range(0, length);
            GameObject go = Instantiate(spawn[id]);
            Transform t = go.transform;

            if (oneTime == false)
            {
                t.SetParent(transform);
                SpawnedObject spawnedObject = t.GetComponent<SpawnedObject>();
                spawnedObjects.Add(spawnedObject);
                spawnedObject.objId = id;
            }

            Vector3 position = transform.position;
            position.x += UnityEngine.Random.Range(-spawnArea_width, spawnArea_width);
            position.y += UnityEngine.Random.Range(-spawnArea_height, spawnArea_height);

            t.position = position;
        }
    }

    public class ToSave
    {

        public List<SpawnedObject.SaveSpawnedObjectData> spawnedObjectDatas;

        public ToSave()
        {
            spawnedObjectDatas = new List<SpawnedObject.SaveSpawnedObjectData>();
        }
    }

    string Read()
    {
        ToSave toSave = new ToSave();

        for (int i = 0; i < spawnedObjects.Count; i++)
        {
            toSave.spawnedObjectDatas.Add(
                new SpawnedObject.SaveSpawnedObjectData(
                    spawnedObjects[i].objId,
                    spawnedObjects[i].transform.position
                    )
                );
        }
        return JsonUtility.ToJson(toSave);
    }

    public void Load(string json)
    {
        if(json == "" || json == "{}" || json == null) { return; }

        ToSave toLoad = JsonUtility.FromJson<ToSave>(json);

        for (int i = 0; i < toLoad.spawnedObjectDatas.Count; i++)
        {
            SpawnedObject.SaveSpawnedObjectData data = toLoad.spawnedObjectDatas[i];
            GameObject go = Instantiate(spawn[data.objectId]);
            go.transform.position = data.worldPosition;
            go.GetComponent<SpawnedObject>().objId = data.objectId;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnArea_width * 2, spawnArea_height * 2));
    }
}
