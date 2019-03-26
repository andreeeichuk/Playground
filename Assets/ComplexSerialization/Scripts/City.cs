using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour, IPersistent
{
    Saver saver;
    public GameObjectIdentifier Identifier {get; private set;}

    public string CityName { get; set; }
    public int Population { get; set; }
    public float Growth { get; set; }
    public List<int> TradeRoutes { get; set; }

    private void Awake()
    {
        Growth = 0.02f;
        TradeRoutes = new List<int>();
    }

    private void Start()
    {
        saver = FindObjectOfType<Saver>();
        Identifier = GetComponent<GameObjectIdentifier>();
        AddToPersistentList();
        if (Identifier.IsLoaded)
            Load();
    }

    public void Grow()
    {
        Population = Population + (int)(Population * Growth);
    }

    public void AddRoute(City city)
    {
        city.TradeRoutes.Add(Identifier.GameObjectID);
        TradeRoutes.Add(city.Identifier.GameObjectID);
        Growth += 0.01f;
        city.Growth += 0.01f;
    }

    public void AddToPersistentList()
    {
        saver.AddEntity(this);
    }

    public void Save()
    {
        SaveClass saveClass = Game.Instance.SaveClass;
        CityStruct cityStruct = new CityStruct()
        {           
            cityName = CityName,
            growth = Growth,
            population = Population,
            vector3 = transform.position
        };        

        if (TradeRoutes.Count > 0)
        {
            int[] ids = new int[TradeRoutes.Count];
            for (int i = 0; i < ids.Length; i++)
            {
                ids[i] = TradeRoutes[i];
            }
            cityStruct.cityTradeRouteIDs = ids;
        }


        // а взагалі можна зробити так, щоб saveClass не зберігався постійно в пам'яті,
        // а створювався тільки під час збереження або завантаження і видалявся після завершення
        // цього процесу
        if (saveClass.cities.ContainsKey(Identifier.GameObjectID))
        {
            saveClass.cities[Identifier.GameObjectID] = cityStruct;
        }
        else
        {
            saveClass.cities.Add(Identifier.GameObjectID, cityStruct);
        }
    }

    public void Load()
    {
        CityStruct cityStruct = Game.Instance.SaveClass.cities[Identifier.GameObjectID];

        CityName = cityStruct.cityName;
        gameObject.name = CityName;
        Population = cityStruct.population;
        Growth = cityStruct.growth;
        if (cityStruct.cityTradeRouteIDs!=null)
        {
            TradeRoutes.AddRange(cityStruct.cityTradeRouteIDs);
        }
        transform.position = cityStruct.vector3;
    }
}   
