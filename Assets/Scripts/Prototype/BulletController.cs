using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private Health enemy;
    [SerializeField] private int damage;
    private Rigidbody rgb;
    public float timeToDeath = 1.1f;
    [SerializeField] private Health current;
    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        current.eventDead += EliminatedBullet;
    }
    private void OnEnable()
    {
        StartCoroutine(Descativas());
    }
    public void SetFather(Health my)
    {
        this.current = my;
    }
    public void SetEnemy(Health enemy, int damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }
    public void SetVelocity(Vector3 target, float intervalAttack)
    {
        rgb.velocity = target.normalized * intervalAttack;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {
            if (other.gameObject.GetComponent<Health>() == enemy)
            {
                if (!current.isDeath)
                {
                    //Llamar evento de RecivirDaño
                    enemy.eventTakeDamage?.Invoke(damage);
                    enemy.eventTakeDamageUI?.Invoke();
                    if (enemy.isDeath)
                    {
                        if (enemy.gameObject != null)
                        {
                            PrefabMovement prefabMovement = enemy.GetComponent<PrefabMovement>();
                            //Llamar evento de Muerte
                            enemy.clearList?.Invoke(prefabMovement);
                            enemy.eventDead?.Invoke(timeToDeath);
                        }
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
    void EliminatedBullet(float time)
    {
        Destroy(gameObject,time+1);
    }
}
