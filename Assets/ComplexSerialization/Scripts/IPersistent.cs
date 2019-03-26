using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistent
{
    void AddToPersistentList();
    void Save();
    void Load();
}
