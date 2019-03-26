using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollection : MonoBehaviour
{
    public GameObject city;

    public static Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        prefabs.Add("city", city);
    }
}
