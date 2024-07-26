using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitysController : MonoBehaviour
{
    [SerializeField] private StrategySO[] listArmy;
    [SerializeField] private int maxCantSpawm=4;

    void Start()
    {
        for(int i = 0; i< listArmy.Length; i++)
        {
            listArmy[i].MaxSpawn = maxCantSpawm;
        }
    }

}
