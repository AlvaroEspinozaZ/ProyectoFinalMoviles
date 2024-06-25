using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Health enemy;
    [SerializeField] private int damage;

    public void SetEnemy(Health enemy, int damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Health>()== enemy)
        {            
            enemy.characterHealth -= damage;
            enemy.UpdateCharacterUI();
            if (enemy.characterHealth <= 0)
            {
                if (enemy.gameObject != null)
                {
                    enemy.Died(1.1f);
                }
            }
            gameObject.SetActive(false);
        }
    }

}
