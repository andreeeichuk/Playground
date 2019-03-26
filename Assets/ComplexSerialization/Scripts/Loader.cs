using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Loader : MonoBehaviour
{
    Game game;
    
    private void Start()
    {
        game = Game.Instance;        
    }


    public void MakeLoad()
    {
        using (FileStream fs = new FileStream(Application.persistentDataPath + "/randomcities.sav", FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            game.SaveClass = (SaveClass)formatter.Deserialize(fs);
            Debug.Log(Game.Instance.SaveClass.cities.Count);
        }

        InstantiatePrefabs();
        //RestoreScriptsState();
    }

    void InstantiatePrefabs()
    {
        Dictionary<int, string> gameObjects = game.SaveClass.gameObjects;

        for (int i = 1; i < gameObjects.Count+1; i++)
        {
            GameObjectIdentifier instance = Instantiate(PrefabCollection.prefabs[gameObjects[i]]).GetComponent<GameObjectIdentifier>();
            instance.Set(new KeyValuePair<int,string>(i,gameObjects[i]));
            instance.IsLoaded = true;
            game.sceneGameObjects.Add(i, instance.gameObject);
        }

        game.SetPrefabIDCounter(gameObjects.Count);
    }

    //void RestoreScriptsState()
    //{
    //    RestoreCities(game, saveClass);
    //}

    //void RestoreCities(Game game, SaveClass saveClass)
    //{
    //   Dictionary<CityStruct> cities = saveClass.cities;

    //    for (int i = 1; i < cities.Count+1; i++)
    //    {
    //        City city = game.sceneGameObjects[cities[i-1].prefabID].GetComponent<City>();
    //        city.Load(cities[i-1]);
    //        game.cities.Add(cities[i - 1].prefabID, city);
    //    }
    //}
}
