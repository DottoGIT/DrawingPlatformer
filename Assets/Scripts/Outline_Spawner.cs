using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Spawner : Outline
{
    [SerializeField] GameObject SpawnObj;
    List<GameObject> spawnedObjects = new List<GameObject>();

    override public void SpawnPlatform()
    {
        spawnedObjects.Add(Instantiate(SpawnObj, transform.position, Quaternion.identity));
        myAudio.Play();
    }

    public override void DestroyPlatforms()
    {
        foreach(var obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
