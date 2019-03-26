using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Saver : MonoBehaviour
{
    List<IPersistent> persistentEntities = new List<IPersistent>();
    
    public void AddEntity(IPersistent entity)
    {
        persistentEntities.Add(entity);
    }

    public void MakeSave()
    {
        foreach (var item in persistentEntities)
        {
            item.Save();
        }

        using (FileStream fs = new FileStream(Application.persistentDataPath + "/randomcities.sav", FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, Game.Instance.SaveClass);
        }
    }
        
}
