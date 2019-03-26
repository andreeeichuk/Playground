using System.Collections.Generic;

[System.Serializable]
public class SaveClass
{
    public Dictionary<int, string> gameObjects = new Dictionary<int, string>();
    public Dictionary<int,CityStruct> cities = new Dictionary<int, CityStruct>();
}
