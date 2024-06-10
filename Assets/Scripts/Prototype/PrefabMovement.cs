using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    public Animator animator; 
    public VisionSensorPrimitive visionSensor; 
    public float characterHealth = 100f;
    NPCController _nPCController;
    Rigidbody _rgb;
    private void Start()
    {
        _nPCController = GetComponent<NPCController>();
        _rgb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (characterHealth <= 0)
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
}
