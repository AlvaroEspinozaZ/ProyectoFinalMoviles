using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float characterHealth = 100f;
    void Update()
    {
        if (characterHealth <= 0)
        {
            Destroy(gameObject, 2.5f);

        }
    }
}
