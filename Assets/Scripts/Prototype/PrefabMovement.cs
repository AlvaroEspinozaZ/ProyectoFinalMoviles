using UnityEngine;

public class PrefabMovement : MonoBehaviour
{
    public Animator animator; 
    public VisionSensorPrimitive visionSensor; 
    public float characterHealth = 100f; 

    private void Update()
    {
        if (characterHealth <= 0)
        {
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
            float distance = Vector3.Distance(transform.position, visionSensor.objectCollision.transform.position);

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
    }
}
