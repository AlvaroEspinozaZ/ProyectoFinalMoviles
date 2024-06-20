using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    public Animator animator; 
    public VisionSensorPrimitive visionSensor;
    private Health myHealth;
    Rigidbody _rgb;
    private float tiempoUltimoAtaque;
    public float intervaloAtaque = 1.5f;
 
    private void Start()
    {
        _rgb = GetComponent<Rigidbody>();
        myHealth = GetComponent<Health>();
    }
    private void Update()
    {
        if (myHealth.characterHealth <= 0)
        {
            _rgb.isKinematic = true;
            animator.SetBool("IsDead", true);

        }
        else
        {
            HandleMovementAndAttack();
        }
    }

    private void HandleMovementAndAttack()
    {
        if (visionSensor.isObjectDetected)
        {
            float distance = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(visionSensor.objectCollision.transform.position.x, 0, visionSensor.objectCollision.transform.position.z));
            if (distance <= visionSensor.stopDistance)
            {
                animator.SetBool("IsMove", false);
                animator.SetBool("IsAttak", true);
                Atacar(intervaloAtaque, visionSensor.currentEnemy);
            }
            else
            {
                animator.SetBool("IsMove", true);
                animator.SetBool("IsAttak", false);
            }
        }
        else
        {
            animator.SetBool("IsMove", false);
            animator.SetBool("IsAttak", false);
        }

        if (visionSensor.isCurrentMove)
        {
            animator.SetBool("IsMove", true);
            animator.SetBool("IsAttak", false);
        }
    }
    void Atacar(float timeToAttack, Health enemy)
    {
        if (enemy.GetComponent<VisionSensorPrimitive>()!= null)
        {
            VisionSensorPrimitive tmp = enemy.GetComponent<VisionSensorPrimitive>();
            if(tmp.currentEnemy == null)
            {
                tmp.objectCollision = gameObject;
                tmp.currentEnemy = myHealth;
                tmp.isObjectDetected = true;
            }
        }
        if ((Time.time - tiempoUltimoAtaque) % (timeToAttack + 1) >= timeToAttack)
        {

            enemy.characterHealth -= 10;
            if (enemy.characterHealth <= 0)
            {
                enemy.Muerte(1.1f);

            }
            tiempoUltimoAtaque = Time.time;
            
        }        
    }
}
