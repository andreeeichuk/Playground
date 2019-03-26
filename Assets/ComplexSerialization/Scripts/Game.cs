using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }
    public SaveClass SaveClass { get; set; }

    [Header("Spawn")]
    public string cityNameSetter;
    public Vector3 cityPositionSetter;
    public int cityPopulationSetter;

    [Header("Trade Route")]
    public City origin;
    public City destination;
    

    public Saver saver;
    public Loader loader;
    public Dictionary<int, City> cities = new Dictionary<int, City>();

    public Dictionary<int, GameObject> sceneGameObjects = new Dictionary<int, GameObject>();

    static int prefabIDCounter = 0;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        SaveClass = new SaveClass();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            SpawnCity(cityNameSetter, cityPositionSetter, cityPopulationSetter);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            
            for (int i = 0; i < 5000; i++)
            {
                string someName = "City" + i.ToString();
                SpawnCity(someName, new Vector3(Random.Range(-12f,12f),0f, Random.Range(-10f, 10f)), Random.Range(100000,5000000));
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            saver.MakeSave();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            AddTradeRoute(origin, destination);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (var item in cities)
            {
                item.Value.Grow();
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            loader.MakeLoad();
        }
    }


    public void SpawnCity(string cityName, Vector3 position, int population)
    {
        City city = Instantiate(PrefabCollection.prefabs["city"], position, Quaternion.identity).GetComponent<City>();
        GameObjectIdentifier identifier = city.gameObject.GetComponent<GameObjectIdentifier>();

        city.CityName = cityName;
        city.Population = population;

        city.gameObject.name = cityName;

        identifier.GameObjectID = ++prefabIDCounter;
        identifier.PrefabName = "city";

        sceneGameObjects.Add(identifier.GameObjectID, city.gameObject);
        
        cities.Add(identifier.GameObjectID, city);
    }

    public void AddTradeRoute(City originCity, City destinationCity)
    {
        if (originCity == destinationCity)
            return;
        originCity.AddRoute(destinationCity);
    }

    public void SetPrefabIDCounter(int lastUsedId)
    {
        prefabIDCounter = lastUsedId;
    }
}
