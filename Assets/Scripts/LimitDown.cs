using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitDown : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().eventTakeDamage?.Invoke(300);
        }
    }
}
