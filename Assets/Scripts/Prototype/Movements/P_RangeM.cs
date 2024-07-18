using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class P_RangeM : PrefabMovement
{
    [Header("RangeAttack")]
    [SerializeField] Ease easeDOT;
    [SerializeField] BulletController bullet;
    [SerializeField] BulletController[] bullets;
    int id;
    public override void Start()
    {
        base.Start();
        bullets = new BulletController[5];
        if (bullet != null)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = Instantiate(bullet);
                bullets[i].gameObject.SetActive(false);
            }
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Attack(float timeToAttack, Health enemy)
    {
        //base.Attack(timeToAttack, enemy);

        if (enemy.GetComponent<VisionSensorPrimitive>() != null)
        {
            VisionSensorPrimitive tmp = enemy.GetComponent<VisionSensorPrimitive>();
            if (tmp.currentEnemy == null)
            {
                tmp.objectCollision = gameObject;
                tmp.currentEnemy = myHealth;
                tmp.isObjectDetected = true;
            }
            if ((Time.time - timeToLastHit) % (timeToAttack + 1) >= timeToAttack && tmp!=null)
            {
                bullets[id].transform.position = transform.position;
                bullets[id].gameObject.SetActive(true);
                bullets[id].SetEnemy(enemy, damage);
                bullets[id].gameObject.transform.DOMove(enemy.gameObject.transform.position, intervalAttack).SetEase(Ease.InOutCubic);
                id = (id + 1) % 5;
                timeToLastHit = Time.time;
                //Debug.Log("Jaja");

            }
        }
   
    }
    public override void HandleMovementAndAttack()
    {
        base.HandleMovementAndAttack();
    }
}
