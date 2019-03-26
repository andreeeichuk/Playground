using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceTypes : MonoBehaviour
{    
    void Start()
    {
        Bober bober = new Bober("Ostap");

        Bober ostap = bober;
        Bober reference = bober;
        reference = new Bober("Pavlo");

        Debug.Log(ostap.name);
    }
    
    class Bober
    {
        public string name;
        public Bober(string n)
        {
            name = n;
        }
    }
}
