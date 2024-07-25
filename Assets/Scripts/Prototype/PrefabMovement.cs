using UnityEngine;
using DG.Tweening;
public class PrefabMovement : MonoBehaviour
{
    protected Animator animator;
    protected VisionSensorPrimitive visionSensor;
    public int damage = 10;
    protected Health myHealth;
    protected Rigidbody _rgb;
    protected float timeToLastHit;
    public float intervalAttack = 1.5f;
    public float timeToDeath = 1.1f;
    public virtual void Start()
    {
        _rgb = GetComponent<Rigidbody>();
        myHealth = GetComponent<Health>();
        visionSensor = GetComponent<VisionSensorPrimitive>();
        animator = GetComponent<Animator>();
    }
    public virtual void Update()
    {
        if (myHealth.isDeath)
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

    public virtual void HandleMovementAndAttack()
    {
        if (visionSensor.isObjectDetected)
        {
            if (visionSensor.objectCollision != null)
            {
                float distance = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(visionSensor.objectCollision.transform.position.x, 0, visionSensor.objectCollision.transform.position.z));
                if (visionSensor.isTower)
                {
                    //Attack(intervalAttack, visionSensor.currentEnemy);
                    Debug.Log("Te vi");
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
    public virtual void Attack(float timeToAttack, Health enemy)
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
            if ((Time.time - timeToLastHit) % (timeToAttack + 1) >= timeToAttack && tmp != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.gameObject.transform.position);

                tmp.counterAttack?.Invoke(distance, tmp, myHealth);
           
                if (enemy.gameObject != null)
                {
                    //Llamar evento de RecivirDaño
                    enemy.eventTakeDamage?.Invoke(damage);
                    enemy.eventTakeDamageUI?.Invoke();

                    if (enemy.isDeath)
                    {
                        //Llamar evento de Muerte
                        enemy.eventDead?.Invoke(timeToDeath);
                        //enemy.Died(timeToDeath);
                    }
                }
                timeToLastHit = Time.time;
            }
        }
    }

    private void OnEnable()
    {
           
    }
    private void OnDisable()
    {
        
    }
    void UpdateLife(Health enemy)
    {
        
    }
}
