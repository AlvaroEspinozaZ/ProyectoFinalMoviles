using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Health enemy;
    [SerializeField] private int damage;
    public float timeToDeath = 1.1f;
    private void OnEnable()
    {
        StartCoroutine(Descativas());
    }
    public void SetEnemy(Health enemy, int damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            if (other.gameObject.GetComponent<Health>() == enemy)
            {
                //Llamar evento de RecivirDaño
                enemy.eventTakeDamage?.Invoke(damage);
                enemy.eventTakeDamageUI?.Invoke();
                if (enemy.isDeath)
                {
                    if (enemy.gameObject != null)
                    {
                        enemy.eventDead?.Invoke(timeToDeath);
                    }
                }
                gameObject.SetActive(false);
            }

        }

    }
    
    IEnumerator Descativas()
    {
        yield return new WaitForSecondsRealtime(2f);
        gameObject.SetActive(false);
    }
}
