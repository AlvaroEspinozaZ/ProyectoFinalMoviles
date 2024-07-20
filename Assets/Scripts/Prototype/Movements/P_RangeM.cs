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
        if (myHealth.characterHealth <= 0)
        {
            _rgb.isKinematic = true;
            if (animator != null)
                animator.SetBool("IsDead", true);

        }
        else
        {
            HandleMovementAndAttack();
        }
    }

    public override void Attack(float timeToAttack, Health enemy)
    {
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
                if (bullet != null)
                {
                    bullets[id].transform.position = transform.position;
                    bullets[id].gameObject.SetActive(true);
                    if (enemy.gameObject != null)
                    {
                        bullets[id].SetEnemy(enemy, damage);
                        bullets[id].gameObject.transform.DOMove(enemy.gameObject.transform.position, intervalAttack).SetEase(Ease.InOutCubic);
                    }                 
                }
                id = (id + 1) % 5;
                timeToLastHit = Time.time;

            }
        }
   
    }
    public override void HandleMovementAndAttack()
    {
        //base.HandleMovementAndAttack();
        if (visionSensor.isObjectDetected)
        {
            if (visionSensor.objectCollision != null)
            {
                float distance = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(visionSensor.objectCollision.transform.position.x, 0, visionSensor.objectCollision.transform.position.z));
                if (visionSensor.isTower)
                {
                    Attack(intervalAttack, visionSensor.currentEnemy);
                }
                else if (distance <= visionSensor.stopDistance)
                {
                    if (animator != null)
                    {
                        animator.SetBool("IsMove", false);
                        animator.SetBool("IsAttak", true);
                    }
                    Attack(intervalAttack, visionSensor.currentEnemy);
                }
                else
                {
                    if (animator != null)
                    {
                        animator.SetBool("IsMove", true);
                        animator.SetBool("IsAttak", false);
                    }
                }
            }
            else visionSensor.isObjectDetected = false;


        }
        else
        {
            if (animator != null)
            {
                animator.SetBool("IsMove", false);
                animator.SetBool("IsAttak", false);
            }
        }

        if (visionSensor.isCurrentMove)
        {
            if (animator != null)
            {
                animator.SetBool("IsMove", true);
                animator.SetBool("IsAttak", false);
            }

        }
    }
}
