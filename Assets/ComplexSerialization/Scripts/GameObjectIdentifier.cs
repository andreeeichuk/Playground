using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script can only be on root objects of prefabs
public class GameObjectIdentifier : MonoBehaviour, IPersistent
{
    Saver saver;
    SaveClass saveClass;

    public int GameObjectID { get; set; }
    public string PrefabName { get; set; }
    public bool IsLoaded { get; set; }

    private void Start()
    {
        saver = FindObjectOfType<Saver>();
        AddToPersistentList();
        saveClass = Game.Instance.SaveClass;
    }

    public void AddToPersistentList()
    {
        saver.AddEntity(this);
    }

    public void Save()
    {
        if (saveClass.gameObjects.ContainsKey(GameObjectID))
        {
            saveClass.gameObjects[GameObjectID] = PrefabName;
        }
        else
        {
            saveClass.gameObjects.Add(GameObjectID, PrefabName);
        }
    }

    public void Set(KeyValuePair<int, string> pair)
    {        
        GameObjectID = pair.Key;
        PrefabName = pair.Value;
    }

    public void Load()
    {
       
    }
}
