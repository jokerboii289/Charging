using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour  //script is attached to phone prefab
{
    public bool charged;
    // Start is called before the first frame update
    void Start()
    {
        charged = false;
    }
}
