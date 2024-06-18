using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    public Animator animator; 
    public VisionSensorPrimitive visionSensor;
    DOTController _nPCController;
    private Health myHealth;
    Rigidbody _rgb;
    private float tiempoUltimoAtaque;
    public float intervaloAtaque = 1.5f;
    private void Start()
    {
        _nPCController = GetComponent<DOTController>();
        _rgb = GetComponent<Rigidbody>();
        myHealth = GetComponent<Health>();
    }
    private void Update()
    {
        if (myHealth.characterHealth <= 0)
        {
            _rgb.isKinematic = true;
            animator.SetBool("IsDead", true);
            _nPCController.Muerte(1.5f);

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
        
        if ((Time.time - tiempoUltimoAtaque) % (timeToAttack + 1) >= timeToAttack)
        {

            enemy.characterHealth -= 10;
            tiempoUltimoAtaque = Time.time;
            Debug.Log("ataco");
        }
        Debug.Log(enemy.characterHealth);
    }
}
